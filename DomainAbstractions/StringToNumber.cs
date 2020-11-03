using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// TBD T should be Nullable and we should pass out null if there is a exception during the conversion
    /// <summary>
    /// Converts string to number of type int or double. 
    /// The Generic Type 'T' should be specified to define the output port type and should be a numeris ValueType.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IDataFlow<string> input: input 
    /// 2. IDataFlow<T> output
    /// </summary>
    public class StringToNumber<T> : IDataFlow<string>
    {
        // Properties
        public string InstanceName { get; set; } = "Default";

        // Ports
        private IDataFlow<T> output;

        /// <summary>
        /// Converts string to double or int
        /// </summary>
        public StringToNumber() 
        {
            // type conversion tests to see how to convert to a gneric type
            // type conversion test left here to throw exception earlier if T is not compatible compatible
            double d = 0.0;
            string s = "0";
            T t;
            t = (T)ConvertValue<T,double>(d);
            t = (T)ConvertValue<T,string>(s);
        }



        private T ConvertValue<T, U>(U value) where U : System.IConvertible
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }



        private string data;





        // IDataFlow<T> implementation -----------------------------------------------------------------
        string IDataFlow<string>.Data
        {
            get => data;
            set
            {
                data = value;
                try
                {
                    output.Data = (T)ConvertValue<T,string>(data);
                }
                catch
                {
                    output.Data = default(T);
                }
            }
        }
    }
}
