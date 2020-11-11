using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using ProgrammingParadigms;
using CalculatorFunctions;


namespace DomainAbstractions
{
    // This one is used for runtime formula coming into the formula port
    using LambdaType = Func<double[], double>;

    // These are all used for a compiletime formula passed to us directly by the application
    using LambdaType0 = Func<double>;
    using LambdaType1 = Func<double, double>;
    using LambdaType2 = Func<double, double, double>;
    using LambdaType3 = Func<double, double, double, double>;
    using LambdaType4 = Func<double, double, double, double, double>;
    using LambdaType5 = Func<double, double, double, double, double, double>;
    using LambdaType6 = Func<double, double, double, double, double, double, double>;


    /// <summary>
    /// <para>Applies a formula (described by a lambda) on inputs of type double and returns an output of type double.</para>
    /// <para>The lambda can be configured by the application at design-time by setting the Lambda property e.g. Lambda=(P1,P2)&eq;&gt;P1+P2.</para>
    /// <para>Or, the lambda can be received at run-time on the first port described below (in the same string format).</para>
    /// <para>The parameters of the lambda correspond one-to-one with the operands port, which is a list of inputs</para>
    /// <para>The lambda parameters must be a string or alphas or underbar, or a null string.</para>
    /// <para>The lambda can have any number of parameters but the formula part of the lambda can only use a maximum of six of them</para>
    /// <para>Example lambda would be (,b,c,d,e,f,,,,g,h,i,j,k,l,,)&=;&gt;b*d*f*h*j*l</para>
    /// <para>Ports:</para>
    /// <para>1. IDataFlow&lt;string&gt; Formula: (Implemented Port) A string containing the lambda that can change at run-time.</para>
    /// <para>2. List&lt;IDataFlowB&lt;double&gt;&gt; operands: Where the input data comes from for the operands of the formula.</para>
    /// <para>3. IDataFlow&lt;double&gt; Result: The output from evaluation of the lambda.</para>
    /// </summary>
    public class Formula : IDataFlow<string> 
    {


        // Properties
        public string InstanceName { get; set; } = "Default";
        // public delegate double LambdaDelegate(double?[] operands);
        // public LambdaDelegate Lambda; // optional



        // Note the following lambda function uses 6 parameters. make sure MaxLambdaParamters is also 6
        // TBD figure out how to make this a variable number of parameters, then much of the code inside this abstraction could be simplified.
        public LambdaType Lambda { private get; set; }

        // These are for lambda expression passed in at compiletime to allow full compiletime checking
        // Support lambda up to 6 parameters
        public LambdaType0 Lambda0 { private get; set; }
        public LambdaType1 Lambda1 { private get; set; }
        public LambdaType2 Lambda2 { private get; set; }
        public LambdaType3 Lambda3 { private get; set; }
        public LambdaType4 Lambda4 { private get; set; }
        public LambdaType5 Lambda5 { private get; set; }
        public LambdaType6 Lambda6 { private get; set; }




        // Ports
        // The IDatFlow<string> implemented interface is the formulaText where the formula can be passed in at runtime (optional)

        private List<IDataFlowB<double?>> operands;
        
        private IDataFlow<double?> result;

        /// <summary>
        /// <para>Evaluates a formula (described by a lambda string) using a list of inputs of type double? and gives the result to the output of type double?</para>
        /// </summary>
        public Formula()
        {
        }



        // Private fields
        private const int MaxLambdaDelegateParameters = 6;
        // These operandShadows have two uses:
        // 1 we use them as shadow copies of the last value we had on our operand input to see if they have actually changed
        // 2 we copy the IDataFlowB operands into them for easier access
        private List<double?> operandShadows = new List<double?>();

        // This list maps which operands inputs are actually used by the formula e.g. "(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x) => c*e"  operandsused[2] and operandsUsed[4] are set to true.
        private List<bool> operandsUsed = new List<bool>();



        int loopCounter = 0;

        private void operandsPostWiringInitialize()
        {
            operands.Last().DataChanged += OperandChanged;
        }

        private void OperandChanged()
        {
            // if no inputs are changed then dont change the output -- this allows a Formula to be wired in a loop in a DataFlow
            if (operands == null) return;
            bool change = false;
            int index = 0;
            foreach (IDataFlowB<double?> operand in operands)
            {
                // if we have had new operands wired to us, increase the number of shadows
                if (operandShadows.Count < index + 1) operandShadows.Add(null);
                if (!doubleEq(operand.Data, operandShadows[index])) { change = true; }
                operandShadows[index] = operand.Data;
                index++;
            }


            if (change)
            {
                // In case the user enters a formula that refers to itself, e.g. label="x"  formyla="x+1", we let it iterate 10 times then stop 
                // later, we can allow this to go more times if it is converging
                loopCounter++;  
                if (loopCounter < 10) SomethingChanged();
                loopCounter--;
            }
        }




        private string _inputLambda;
        string IDataFlow<string>.Data
        {
            get => _inputLambda;
            set
            {
                _inputLambda = ReplaceParameterLabelsWithArray(value, out operandsUsed);
                Compile(_inputLambda);
                if (operandShadows.Count == 0) OperandChanged(); // If formula input changes before any of the operand inputs change, this will get the input values from the input operand ports for the first time
                SomethingChanged();
            }
        }




        private void SomethingChanged()
        {
            bool nullInput = false;
            double? output;
            double[] unboxedOperands = new double[operandShadows.Count];
            if (Lambda != null)
            {
                for (int i = 0; i < operandShadows.Count; i++)
                {
                    // Note its is posible for the operandsused list to be empty if no labels are ever entered
                    if (i<operandsUsed.Count && operandsUsed[i])
                    {
                        if (operandShadows[i].HasValue)
                        {
                            unboxedOperands[i] = operandShadows[i].Value;
                        }
                        else
                        {
                            nullInput = true;
                        }
                    }
                    else   // operand not used, give it zero
                    {
                        unboxedOperands[i] = 0;
                    }
                }
                if (nullInput)
                {
                    output = null;
                }
                else
                {
                    output = Lambda(unboxedOperands);
                }
            }
            else
            if (Lambda2 != null)  // TBD repeat this pattern for all 7 Lambda expression types
            {
                if (unboxOperands(2, out unboxedOperands))
                {
                    output = Lambda2(unboxedOperands[0], unboxedOperands[1]);
                }
                else
                {
                    output = null;
                }
            }
            else
            {
                output = null;
            }
            if (!doubleEq(result.Data, output)) result.Data = output;
        }





        private bool unboxOperands(int n, out double[] unboxedOperands)
        {
            unboxedOperands = new double[n];
            for (int i = 0; i < n; i++)
            {
                if (i < operandShadows.Count)
                {
                    if (operandShadows[i].HasValue)
                    {
                        unboxedOperands[i] = operandShadows[i].Value;
                    }
                    else
                    {
                        return false;
                    }
                }
                else  // no mapping means no more lambda parameters are used by the expression - just pass in zero
                {
                    return false;
                }
            }
            return true;
        }




        private async void Compile(string formula)
        {
            if (formula.IndexOf("=>") == formula.Length - 2)
            {
                Lambda = null;
                return;
            }
            var options = ScriptOptions.Default;
            options = options.AddImports("System.Math");
            options = options.AddReferences(typeof(CalcFunctions).Assembly);  // This adds our own functions, e.g. Fact which are in the CalculatorFunctions project
            options = options.AddImports("CalculatorFunctions.CalcFunctions");
            try
            {
                Lambda = await CSharpScript.EvaluateAsync<LambdaType>(formula, options);
            }
            catch (CompilationErrorException e)
            {
                Lambda = null;
            }
        }



        private bool doubleEq(double? a, double? b)
        {
            if (!a.HasValue && !b.HasValue) return true;
            return a == b;
        }



        // private more abstract support functions ============================================================================================================================


        /// <summary>
        /// Replace Parameter Labels With Array elements
        /// For example input lambda expression is "(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x) => c*e" 
        /// This function returns, for the above example, "(P)=>P[2]*P[4]" 
        /// The paremeters used by the expressions, c & e are the number 2 and number 4 inputs
        /// This function allows us to match the resulting lambda to Func(double[],double)
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns>Lambda modified to use an array for its parameters</returns>
        private string ReplaceParameterLabelsWithArray(string lambda, out List<bool> operandsUsed)
        {
            string[] lambdaHalves;
            operandsUsed = new List<bool>();
            lambdaHalves = lambda.Split(new string[] { "=>" }, StringSplitOptions.None);
            if (lambdaHalves.Length < 2) return lambda;  // if there was only a formula, not a lambda expression e.g. 1+2 just return the same formula
            List<string> parameters = lambdaHalves[0].Substring(1,lambdaHalves[0].Length-2).Split(',').ToList();
            int i = 0;
            foreach (string p in parameters)
            {
                operandsUsed.Add(false);
                if (p.Length > 0)
                {
                    Regex regex = new Regex(@"\b" + p + @"\b");
                    if (regex.IsMatch(lambdaHalves[1]))
                    {
                        lambdaHalves[1] = regex.Replace(lambdaHalves[1], $"P[{i}]");
                        operandsUsed[i] = true;
                    }
                    i++;
                }
            }
            return $"(P)=>{lambdaHalves[1]}";
        }






        private void assertStringEq(string a, string b)
        {
            if (a != b) throw new Exception($"Failed: {a} should equal {b}");
        }




        private void PostWiringInitialize()
        {
            // Debug.WriteLine("RunningTestRemoveUnusedLambdaParameters");
            // TestRemoveUnusedLambdaParameters();
            // Debug.WriteLine("RunningTestAddDummyParameters");
            // TestAddDummyParameters();
        }
    }
}



