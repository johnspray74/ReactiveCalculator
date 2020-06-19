using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// <para>Applies a formula (described by a lambda) on inputs of type double and returns an output of type double.</para>
    /// <para>The formula can be configured by the application at design-time by setting the Formula property e.g. Formula="(P1,P2)&eq;&gt;P1+P2".</para>
    /// <para>Or, the formula can be received at run-time on the third port described below (in the same string format).</para>
    /// <para>Ports:</para>
    /// <para>1. IDataFlow&lt;string&gt; Formula: (Implemented Port) A string containing the formula that can change at run-time.</para>
    /// <para>2. List&lt;IDataFlowB&lt;double&gt;&gt; operands: The input operands for the operation.</para>
    /// <para>3. IDataFlow&lt;double&gt; Result: The output from the formula.</para>
    /// </summary>
    public class Formula : IDataFlow<string>
    {
        // Properties
        public string InstanceName = "Default";
        // public delegate double LambdaDelegate(List<double> operands);
        // public LambdaDelegate Lambda; // optional
        public Func<double, double, double, double, double, double, double> Lambda;

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
        private string formulaText;


        private void PostWiringInitialize()
        {
            foreach (var operand in operands)
            {
                operand.DataChanged += DataChanged;
            }
        }

        private void DataChanged()
        {
            /*
            var operandList = new List<double>();
            foreach (var operand in operands)
            {
                operandList.Add(operand.Data);
            }
            */
            double operand0 = double.NaN;
            double operand1 = double.NaN;
            double operand2 = double.NaN;
            double operand3 = double.NaN;
            double operand4 = double.NaN;
            double operand5 = double.NaN;
            if (operands.Count >= 1) operand0 = operands[0].Data;
            if (operands.Count >= 2) operand1 = operands[1].Data;
            if (operands.Count >= 3) operand2 = operands[2].Data;
            if (operands.Count >= 4) operand3 = operands[3].Data;
            if (operands.Count >= 5) operand4 = operands[4].Data;
            if (operands.Count >= 6) operand5 = operands[5].Data;
            // if (Result != null)
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
        }

        private async void Compile()
        {
            try
            {
                Lambda = await CSharpScript.EvaluateAsync<Func<double, double, double, double, double, double, double>>(formulaText);
            }
            catch (CompilationErrorException e)
            {
                Lambda = null;
            }
        }



        string IDataFlow<string>.Data
        {
            get => formulaText;
            set
            {
                formulaText = value;
                Compile();
                DataChanged();
            }
        }


    }
}
