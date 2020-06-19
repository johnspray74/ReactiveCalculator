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
    public class StringToNumber : IDataFlow<string>
    {
        // Properties
        public string InstanceName = "Default";

        // Ports
        private IDataFlow<double> output;

        /// <summary>
        /// Converts any kind of IDataFlow to an IEvent.
        /// </summary>
        public StringToNumber() { }


        // Private fields
        private string data = default;



        // IDataFlow<T> implementation -----------------------------------------------------------------
        string IDataFlow<string>.Data
        {
            get => data;
            set
            {
                if (!double.TryParse(value, out double temp)) temp = double.NaN;
                output.Data = temp;
            }
        }
    }
}
