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
    /// <para>Applies an operation (described by a lambda) on inputs of type T and returns an output of type T.</para>
    /// <para>Ports:</para>
    /// <para>1. IEvent startOperation: The event that starts the operation.</para>
    /// <para>2. List&lt;IDataFlowB&lt;T&gt;&gt; operands: The input operands for the operation.</para>
    /// <para>3. IDataFlow&lt;T&gt; operationResultOutput: The output from the operation.</para>
    /// </summary>
    public class Operation<T> : IEvent
    {
        // Properties
        public string InstanceName = "Default";
        public delegate T OperationDelegate(List<T> operands);
        public OperationDelegate Lambda;

        // Private fields
        private T operationResult = default;

        // Ports
        private List<IDataFlowB<T>> operands;
        private IDataFlow<T> operationResultOutput;

        /// <summary>
        /// <para>Applies an operation (described by a lambda) on inputs of type T and returns an output of type T.</para>
        /// </summary>
        public Operation()
        {

        }

        private void Push()
        {
            if (operationResultOutput != null) operationResultOutput.Data = operationResult;
        }

        void IEvent.Execute()
        {
            List<T> operandList = new List<T>();
            foreach (var dataFlowB in operands)
            {
                operandList.Add(dataFlowB.Data);
            }

            operationResult = Lambda(operandList);
            Push();
        }
    }
}
