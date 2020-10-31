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
            // type conversion left here to ensure type T is compatible
            double d = 0.0;
            string s = "0";
            T t;
            t = (T)ConvertValue<T>(d);
            t = (T)ConvertValue<T,double>(d);
            t = (T)ConvertValue<T,string>(s);
        }



        private T ConvertValue<T, U>(U value) where U : System.IConvertible
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        private T ConvertValue<T>(double value)
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
                /*
                if (!double.TryParse(value, out double temp)) temp = double.NaN;
                output.Data = (T)System.Convert.ChangeType(temp, typeof(T));
                */

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
