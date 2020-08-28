using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Win32;
using ProgrammingParadigms;
using static System.Math;


namespace DomainAbstractions
{
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
        // public delegate double LambdaDelegate(List<double> operands);
        // public LambdaDelegate Lambda; // optional

        public Func<double, double, double, double, double, double, double> Lambda { private get; set; }

        // Ports
        // The IDatFlow<string> implemented interface is the formulaText where the formula can be passed in at runtime (optional)
        private List<IDataFlowB<double>> operands;
        private IDataFlow<double> result;

        /// <summary>
        /// <para>Applies an formula (described by a lambda) on inputs of type T and returns an output of type T.</para>
        /// </summary>
        public Formula()
        {
        }



        // Private fields
        private const int MaxLambdaDelegateParameters = 6;
        // These inputs lists have two uses:
        // 1 is to always have 6 values to pass to the Lambda even if some inputs are unwired
        // 2 we use them to see if inputs have actually changed
        // 3 we copy the IDataFlowB inputs into them for easy access
        private List<double> inputs = new List<double>();

        // This list maps the reduced lambda parameters to the input lambda parameters. For example input lambda expression is "(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x) => c*e"  reduced lambda expression is "(c,e) => e*c" and mapping is { 2, 4 }
        private List<int> reducedInputsMapping = new List<int>();



        int loopCounter = 0;
        bool infiniteLoopStop;

        private void operandsPostWiringInitialize()
        {
            operands.Last().DataChanged += OperandChanged;
        }

        private void OperandChanged()
        {
            if (InstanceName == "MultipleRow1_Formula1")
            {
            }
            // if no inputs are changed then dont change the output -- this allows a Formula to be wired in a loop in a DataFlow
            bool change = false;
            int Index = 0;
            foreach (IDataFlowB<double> operand in operands)
            {
                if (inputs.Count < Index + 1) inputs.Add(double.NaN);
                double newValue = operand.Data;
                double oldValue = inputs[Index];
                inputs[Index] = newValue;
                if (DoubleEq(newValue, oldValue)) { change = true; }
                Index++;
            }


            if (change)
            {
                loopCounter++;
                if (loopCounter == 10) infiniteLoopStop = true;
                if (!infiniteLoopStop) SomethingChanged();
                loopCounter--;
                if (loopCounter == 0) infiniteLoopStop = false;
            }
        }




        private string _inputLambda;
        string IDataFlow<string>.Data
        {
            get => _inputLambda;
            set
            {
                _inputLambda = RemoveUnusedLambdaParameters(value, out reducedInputsMapping);
                _inputLambda = AddDummyParameters(_inputLambda, MaxLambdaDelegateParameters);
                while (reducedInputsMapping.Count < MaxLambdaDelegateParameters) reducedInputsMapping.Add(0);
                Compile(_inputLambda);
                if (inputs.Count == 0) OperandChanged(); // If formula changed first, this will get the input values from the input operand ports for the first time
                SomethingChanged();
            }
        }




        private void SomethingChanged()
        {
            double output;
            if (Lambda != null)
            {
                output = Lambda(inputs[reducedInputsMapping[0]], inputs[reducedInputsMapping[1]], inputs[reducedInputsMapping[2]], inputs[reducedInputsMapping[3]], inputs[reducedInputsMapping[4]], inputs[reducedInputsMapping[5]]);
            }
            else
            {
                output = double.NaN;
            }
            if (!DoubleEq(result.Data, output)) result.Data = output;
        }




        private async void Compile(string lambda)
        {
            // double x = Sin(1.0);

            var options = ScriptOptions.Default;
            options = options.AddImports("System.Math");
            try
            {
                Lambda = await CSharpScript.EvaluateAsync<Func<double, double, double, double, double, double, double>>(lambda, options);
            }
            catch (CompilationErrorException e)
            {
                Lambda = null;
            }
        }



        private bool DoubleEq(double a, double b)
        {
            if (double.IsNaN(a) && double.IsNaN(b)) return true;
            return a == b;
        }



        // private more abstract support functions ============================================================================================================================


        private void PostWiringInitialize()
        {
            // Debug.WriteLine("RunningTestRemoveUnusedLambdaParameters");
            // TestRemoveUnusedLambdaParameters();
            // Debug.WriteLine("RunningTestAddDummyParameters");
            // TestAddDummyParameters()

        }


        /// <summary>
        /// Remove unused lambda parameters
        /// For example input lambda expression is "(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x) => c*e" (because, for example, we might be in an application that wires more than 6 inputs)
        /// The lambda delegate has a maximum of 6 parameters.
        /// This function removes unused parameters so it returns, for the above example, "(c,e) => e*c" 
        /// This function also has to output a mapping of the new parameter list to the (example) 26 operand inputs e.g. { 2, 4 }
        /// </summary>
        /// <param name="lambda"></param>
        /// <param name="reducedOperandsMapping"></param>
        /// <returns></returns>
        private string RemoveUnusedLambdaParameters(string lambda, out List<int> reducedOperandsMapping)
        {
            string[] s;
            s = lambda.Split(new string[] { "=>" }, StringSplitOptions.None);
            List<string> parameters = s[0].Split(',').ToList();
            for (int i = 0; i<parameters.Count; i++)
            {
                string str = parameters[i];
                char[] arr = str.Where(c => char.IsLetter(c) || c == '_').ToArray();
                parameters[i] = new string(arr);
            }
            List<string> identifiers = FindIdentifiers(s[1]).Distinct().ToList();
            List<string> reducedParameters = new List<string>();
            reducedOperandsMapping = new List<int>();
            foreach (string p in identifiers)
            {
                int i = parameters.IndexOf(p);
                if (i >= 0)
                {
                    reducedParameters.Add(parameters[i]);
                    reducedOperandsMapping.Add(i);
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < reducedParameters.Count; i++)
            {
                sb.Append(reducedParameters[i]);
                if (i < reducedParameters.Count - 1) sb.Append(",");
            }

            return $"({sb}) =>{s[1]}";
        }


        private List<string> FindIdentifiers(string formula)
        {
            List<string> rv = new List<string>();
            string pattern = @"[a-zA-Z_]+";
            Regex rgx = new Regex(pattern);
            foreach (Match match in rgx.Matches(formula))
            {
                rv.Add(match.Value);
            }
            return rv;
        }






        private void TestRemoveUnusedLambdaParameters()
        {
            List<int> reducedOperands;
            assertStringEq(RemoveUnusedLambdaParameters("(a,b) => a+b", out reducedOperands), "(a,b) => a+b");
            assertListEq(reducedOperands, new List<int> { 0, 1 });
            assertStringEq(RemoveUnusedLambdaParameters("(a,b,c,d,e,f,g,h,i,j) => a+b", out reducedOperands), "(a,b) => a+b");
            assertListEq(reducedOperands, new List<int> { 0, 1 });
            assertStringEq(RemoveUnusedLambdaParameters("(a,b,c,d,e,f,g,h,i,j) => b + a", out reducedOperands), "(b,a) => b + a");
            assertListEq(reducedOperands, new List<int> { 1, 0 });
            assertStringEq(RemoveUnusedLambdaParameters("(a,b,c,d,e,f,g,h,i,j) => Sqrt(b)", out reducedOperands), "(b) => Sqrt(b)");
            assertListEq(reducedOperands, new List<int> { 1 });
            assertStringEq(RemoveUnusedLambdaParameters(" ( _fr_ed_ , Frog, c, d, e, f, g, h, i, j )  =>  Frog / Frog + _fr_ed_ - Frog * _fr_ed_ ", out reducedOperands), "(Frog,_fr_ed_) =>  Frog / Frog + _fr_ed_ - Frog * _fr_ed_ ");
            assertListEq(reducedOperands, new List<int> { 1, 0 });
            assertStringEq(RemoveUnusedLambdaParameters("(a,b,c,d,e,f,g,h,i,j) => c+j+i+h+f+e+d+b", out reducedOperands), "(c,j,i,h,f,e,d,b) => c+j+i+h+f+e+d+b");
            assertListEq(reducedOperands, new List<int> { 2, 9, 8, 7, 5, 4, 3, 1 });
            assertStringEq(RemoveUnusedLambdaParameters("(a,b,c,d,e,f,g,h,i,j,k) => a+b+c+d+e+f+g+h+i+j", out reducedOperands), "(a,b,c,d,e,f,g,h,i,j) => a+b+c+d+e+f+g+h+i+j");
            assertListEq(reducedOperands, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            assertStringEq(RemoveUnusedLambdaParameters("(,a) => a", out reducedOperands), "(a) => a");
            assertListEq(reducedOperands, new List<int> { 1 });
        }



        private void assertListEq<T>(List<T> a, List<T> b)
        {
            if (!a.SequenceEqual(b))
            {
                string sa = string.Join(",", a.ToArray());
                string sb = string.Join(",", b.ToArray());
                throw new Exception($"Failed: {sa} should equal {sb}");
            }
        }



        private string AddDummyParameters(string input, int n)
        {
            // we tolerate less than six parameters - add dummy parameters to make up to six for the lambda
            // examples
            // (a,b,c) -> (a,b,c,_P3,_P4,_P5)
            // (a,,,) ->  (a,_P1,_P2,_P3,_P4,_P5)
            // () -> (_P0,_P1,_P2,_P3,_P4,_P5)
            // (,,,,,) ->  (_P0,_P1,_P2,_P3,_P4,_P5)
            // (a,b,c,d,e) ->  (a,b,c,d,e,_P5) 
            // (a,b,c,d,e,) ->  (a,b,c,d,e,_P5) 

            // nasty code follows to get correct - there must be a better way to do this
            string rv = input;
            try
            {
                int pos = 0;
                for (int i = 0; i < n; i++)
                {
                    int nextpos;
                    int stoppos = rv.IndexOf(')', 0);
                    nextpos = rv.IndexOf(',', pos, stoppos-pos);
                    if (nextpos == -1)
                    {
                        nextpos = rv.IndexOf(')', pos);
                    }
                    // is there an alpha paramter name
                    if (!rv.Substring(pos, nextpos - pos).Any(c => char.IsLetter(c)))
                    {
                        rv = rv.Insert(nextpos, "_P" + i.ToString());
                        nextpos += 3;
                    }
                    if (i < n - 1 && rv[nextpos] == ')') rv = rv.Insert(nextpos, ",");
                    pos = nextpos + 1;
                }
            }
            catch (Exception e)
            {
                rv = "";
            }
            return rv;
        }


        private void assertStringEq(string a, string b)
        {
            if (a != b) throw new Exception($"Failed: {a} should equal {b}");
        }

        private void TestAddDummyParameters()
        {
            assertStringEq(AddDummyParameters("(a,b,c)",6), "(a,b,c,_P3,_P4,_P5)");
            assertStringEq(AddDummyParameters("(a,b,c,,,)",6), "(a,b,c,_P3,_P4,_P5)");
            assertStringEq(AddDummyParameters("(a , , , )",6), "(a , _P1, _P2, _P3,_P4,_P5)");
            assertStringEq(AddDummyParameters("()",6), "(_P0,_P1,_P2,_P3,_P4,_P5)");
            assertStringEq(AddDummyParameters("(,,,,,)",6), "(_P0,_P1,_P2,_P3,_P4,_P5)");
            assertStringEq(AddDummyParameters("(a,b,c,d,e)",6), "(a,b,c,d,e,_P5)");
            assertStringEq(AddDummyParameters("(a,b,c,d,e,)=>a+b",6), "(a,b,c,d,e,_P5)=>a+b");
        }


    }
}
