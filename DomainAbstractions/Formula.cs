using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using ProgrammingParadigms;
using static System.Math;


namespace DomainAbstractions
{
    /// <summary>
    /// <para>Applies a formula (described by a lambda) on inputs of type double and returns an output of type double.</para>
    /// <para>The formula can be configured by the application at design-time by setting the Lambda property e.g. Lambda=(P1,P2)&eq;&gt;P1+P2.</para>
    /// <para>Or, the formula can be received at run-time on the first port described below (in the same string format).</para>
    /// <para>Ports:</para>
    /// <para>1. IDataFlow&lt;string&gt; Formula: (Implemented Port) A string containing the formula that can change at run-time.</para>
    /// <para>2. List&lt;IDataFlowB&lt;double&gt;&gt; operands: The input operands for the operation.</para>
    /// <para>3. IDataFlow&lt;double&gt; Result: The output from the formula.</para>
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
        private string _formulaText;
        // These operand fields have two uses:
        // 1 is to always have 6 values to pass to the Lambda even if some inputs are unwired
        // 2 we use them to see if inputs have actually changed
        double operand0 = double.NaN;
        double operand1 = double.NaN;
        double operand2 = double.NaN;
        double operand3 = double.NaN;
        double operand4 = double.NaN;
        double operand5 = double.NaN;
        int loopCounter = 0;
        bool infiniteLoopStop;

        private void PostWiringInitialize()
        {
            foreach (var operand in operands)
            {
                operand.DataChanged += OperandChanged;
            }
            // TestAddDummyParameters();
        }

        private void OperandChanged()
        {
            // if no inputs are changed then dont change the output -- this allows a Formula to be wired in a loop in a DataFlow
            bool change = false;
            if (operands.Count >= 1) { if (operand0 != operands[0].Data && !(double.IsNaN(operand0) && double.IsNaN(operands[0].Data))) { change = true; operand0 = operands[0].Data; } }
            if (operands.Count >= 2) { if (operand1 != operands[1].Data && !(double.IsNaN(operand1) && double.IsNaN(operands[1].Data))) { change = true; operand1 = operands[1].Data; } }
            if (operands.Count >= 3) { if (operand2 != operands[2].Data && !(double.IsNaN(operand2) && double.IsNaN(operands[2].Data))) { change = true; operand2 = operands[2].Data; } }
            if (operands.Count >= 4) { if (operand3 != operands[3].Data && !(double.IsNaN(operand3) && double.IsNaN(operands[3].Data))) { change = true; operand3 = operands[3].Data; } }
            if (operands.Count >= 5) { if (operand4 != operands[4].Data && !(double.IsNaN(operand4) && double.IsNaN(operands[4].Data))) { change = true; operand4 = operands[4].Data; } }
            if (operands.Count >= 6) { if (operand5 != operands[5].Data && !(double.IsNaN(operand5) && double.IsNaN(operands[5].Data))) { change = true; operand5 = operands[5].Data; } }
            // if (Result != null)
            if (change)
            {
                loopCounter++;
                if (loopCounter == 10) infiniteLoopStop = true;
                if (!infiniteLoopStop) DataChanged();
                loopCounter--;
                if (loopCounter == 0) infiniteLoopStop = false;
            }
        }



        private void DataChanged()
        {
                if (Lambda != null)
                {
                    result.Data = Lambda(operand0, operand1, operand2, operand3, operand4, operand5);
                }
                else
                {
                    result.Data = double.NaN;
                }
        }

        private async void Compile()
        {
            // double x = Sin(1.0);

            var options = ScriptOptions.Default;
            options = options.AddImports("System.Math");
            try
            {
                Lambda = await CSharpScript.EvaluateAsync<Func<double, double, double, double, double, double, double>>(_formulaText, options);
            }
            catch (CompilationErrorException e)
            {
                Lambda = null;
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


        string IDataFlow<string>.Data
        {
            get => _formulaText;
            set
            {
                _formulaText = AddDummyParameters(value, 6);
                Compile();
                DataChanged();
            }
        }


    }
}
