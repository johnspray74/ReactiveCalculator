using ProgrammingParadigms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainAbstractions
{
    /// <summary>
    /// Does nother but pass an IDataFlow through
    /// So what use is it
    /// Well an IdataFlowB output can connect to List of IdataFlowB input list, conveerting it to a non list input to make things simpler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DataFlowBNull<T> : IDataFlowB<T>
    {
        public string InstanceName = "Default";

        // ports
        // (implemented) IDateFlowB (output) 
        private IDataFlowB<T> input;  // (input) 


        // input interface 

        private void PostWiringInitialize()
        {
            input.DataChanged += InputChangedHandler;
        }



        private T _data;

        private void InputChangedHandler() 
        {
            _data = input.Data;   
            DataChanged?.Invoke();  // on the output
        }


        // output interface 

        public event DataChangedDelegate DataChanged;

        T IDataFlowB<T>.Data
        {
            get => _data;
        }



    }
}
