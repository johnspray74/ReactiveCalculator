using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// <para>Applies a lambda on an input of type T1 and returns an output of type T2.</para>
    /// <para>Ports:</para>
    /// <para>1. IDataFlow&lt;T1&gt; input: The input to the lambda.</para>
    /// <para>2. IDataFlow&lt;T2&gt; output: The output from the lambda.</para>
    /// </summary>
    public class Apply<T1, T2> : IDataFlow<T1>
    {
        // Properties
        public string InstanceName = "Default";
        public delegate T2 LambdaDelegate(T1 x);
        public LambdaDelegate Lambda;

        // Private fields
        private T1 lastInput = default;
        private T2 storedValue;

        // Ports
        private IDataFlow<T2> output;

        /// <summary>
        /// <para>Applies a lambda on an input of type T1 and returns an output of type T2.</para>
        /// </summary>
        public Apply() { }

        // IDataFlow<T1> implementation
        T1 IDataFlow<T1>.Data
        {
            get => lastInput;
            set
            {
                try
                {
                    lastInput = value;
                    storedValue = Lambda(lastInput);
                }
                catch (Exception e)
                {
                }

                if (output != null && storedValue != null) output.Data = storedValue;
            } 
        }

    }
}