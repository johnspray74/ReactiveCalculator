using ProgrammingParadigms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainAbstractions
{
    /// <summary>
    /// Outputs the input strings concatenated together
    /// Whenever an input strings changes, a new output is pushed.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. List<IDataFlowB<string>> inputs: inputs (indefinite number of string inputs)
    /// 2. IDataFlow<string> output: output
    /// </summary>



    public class StringConcat
    {
        // Properties ---------------------------------------------------------------
        public string InstanceName = "Default";
        public string Separator = "";

        // Ports ---------------------------------------------------------------
        private List<IDataFlowB<string>> inputs;
        private IDataFlow<string> output;

        // Private fields ---------------------------------------------------------------

        /// <summary>
        /// Outputs a boolean value of true when all of its inputs are true. Null inputs are treated as false.
        /// </summary>
        public StringConcat() { }

        private void PostWiringInitialize()
        {
            foreach (IDataFlowB<string> inputPort in inputs)
            {

                inputPort.DataChanged += () =>
                {
                    var result = "";
                    bool first = true;
                    foreach (IDataFlowB<string> input in inputs)
                    {
                        if (!first) result += Separator;
                        first = false;
                        result += input.Data;
                    }
                    output.Data = result;
                };
            }
        }
    }
}
