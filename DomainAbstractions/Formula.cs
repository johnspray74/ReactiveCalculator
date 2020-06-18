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
    /// <para>Applies a formula (described by a lambda) on inputs of type T and returns an output of type T.</para>
    /// <para>Ports:</para>
    /// <para>2. List&lt;IDataFlowB&lt;T&gt;&gt; operands: The input operands for the operation.</para>
    /// <para>3. IDataFlow&lt;T&gt; Result: The output from the formula.</para>
    /// </summary>
    public class Formula<T> : IEvent
    {
        // Properties
        public string InstanceName = "Default";
        public delegate T LambdaDelegate(List<T> operands);
        public LambdaDelegate Lambda;

        // Private fields
        private T result = default;

        // Ports
        private List<IDataFlowB<T>> operands;
        private IDataFlow<T> Result;

        /// <summary>
        /// <para>Applies an formula (described by a lambda) on inputs of type T and returns an output of type T.</para>
        /// </summary>
        public Formula()
        {
        }


        void IEvent.Execute()
        {
            List<T> operandList = new List<T>();
            foreach (var dataFlowB in operands)
            {
                operandList.Add(dataFlowB.Data);
            }

            if (Result != null) Result.Data = Lambda(operandList);
        }
    }
}
