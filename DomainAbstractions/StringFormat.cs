using ProgrammingParadigms;
using System.Collections.Generic;
using System.Windows.Data;

namespace DomainAbstractions
{
    /// <summary>
    /// Similiar to other formatting string functions found in other languages.
    /// Takes a string property which contains C# style data insertion points
    /// e.g. "Data={1}, Moredata={2}"
    /// Has a port which is a list of IDataFlows that are converted to strings and inserted at the insertion points 
    /// according to their index numbers, so the ordering of the connections shown in the diagram are important.
    /// </summary>
    public class StringFormat<T> : IDataFlow<T>
    {
        // properties
        public string InstanceName = "Default";

        // ports
        // The IDataFlow implemented interface is input0
        private List<IDataFlowB<T>> inputs = new List<IDataFlowB<T>>();
        private IDataFlow<string> output;

        // private fields
        private string format;


        /// <summary>
        /// Formats a string based on a literal string and some input parameters.
        /// </summary>
        /// <param name="literal">the literal string</param>
        public StringFormat(string literal)
        {
            format = literal;
        }

        private void PostWiringInitialize()
        {
            foreach (var f in inputs)
            {
                f.DataChanged += DataChanged;
            }
        }

        private object _Para0;

        T IDataFlow<T>.Data 
        { 
            get => (T)_Para0;
            set
            {
                _Para0 = value;
                DataChanged();
            }
        }

        private void DataChanged()
        {
            object[] paras = new object[inputs.Count+1];
            paras[0] = _Para0; 
            for (var i = 0; i < inputs.Count; i++)
            {
                paras[i+1] = inputs[i].Data;
            }
            output.Data = string.Format(format, paras);
        }        
    }
}
