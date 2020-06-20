using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// Converts any kind of IDataFlow to an IEvent. 
    /// The Generic Type 'T' should be assigned when it is instantiated.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IDataFlow<T> input: input data
    /// 2. IEvent eventOutput: output event
    /// </summary>
    public class NumberToString : IDataFlow<double>
    {
        // Properties
        public string InstanceName = "Default";
        public string NanDisplay = "";

        // Ports
        private IDataFlow<string> output;

        /// <summary>
        /// Converts any kind of IDataFlow to an IEvent.
        /// </summary>
        public NumberToString() { }


        // Private fields
        private double input = default;



        // IDataFlow<T> implementation -----------------------------------------------------------------
        double IDataFlow<double>.Data
        {
            get => input;
            set
            {
                if (double.IsNaN(value))
                {
                    output.Data = NanDisplay;
                }
                else
                {
                    output.Data = value.ToString();
                }
            }
        }
    }
}
