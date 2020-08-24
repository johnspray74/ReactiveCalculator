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
    public class ConvertToEvent<T> : IDataFlow<T>
    {
        // Properties
        public string InstanceName { get; set; } = "Default";


        // Ports
        private IEvent eventOutput;


        // Private fields
        private T data = default;

        /// <summary>
        /// Converts any kind of IDataFlow to an IEvent.
        /// </summary>
        public ConvertToEvent() { }

        // IDataFlow<T> implementation -----------------------------------------------------------------
        T IDataFlow<T>.Data
        {
            get => data;
            set
            {
                data = value;
                eventOutput.Execute();
            }
        }
    }
}
