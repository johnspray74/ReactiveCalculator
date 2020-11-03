using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// Converts number to string 
    /// We would like to use a gneric parameter t as the type of the input constrained to a int, double, decimal, etc, however C# doesn't support this yet.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IDataFlow<decimal?> input
    /// 2. IdataFlow<string> output
    /// </summary>
    public class NumberToString<T> : IDataFlow<T>
    {
        // Properties
        public string InstanceName { get; set; } = "Default";
        public string NaNDisplay { get; set; } = "";

        // Ports
        private IDataFlow<string> output;

        /// <summary>
        /// Converts double to string (null string if Nan)
        /// </summary>
        public NumberToString() { }


        // Private fields
        private T input = default;



        // IDataFlow<T> implementation -----------------------------------------------------------------
        T IDataFlow<T>.Data
        {
            get => input;
            set
            {
                if (value==null)
                {
                    output.Data = NaNDisplay;
                }
                else
                {
                    output.Data = value.ToString();
                }
            }
        }
    }
}
