using System.Collections.Generic;
using Libraries;

namespace ProgrammingParadigms
{
    // Note the two interfaces below, IdataFlow and IDataFlowB, and the DataFlowConnector are all considered part of the same programming paradigm abstraction - data-flow. 
    // Thats why all three are in a common file

    /// <summary>
    /// A data-flow programming paradigm with a single scalar value with a primitive data type.
    /// The data flow paradigm is meant to be used as a push paradigm (data is pushed from the source to the destination)
    /// An example usage would be to wire a FileBrowser which is an IDataFlow source, to a FileReaderWriter, which is an IDataFlow destination. When the FileBrowser outputs a filepath, FileReaderWriter knows where it can read in a file.
    /// Currently is synchronous only (TBD asynchronous version configurable per domain abstraction port)
    /// OR think of it as an event with data, the receiver may store the data so can read it at any time after the data is sent.
    /// Unidirectional - every line on the diagram is one direction implying a sender and receiver.
    /// You can have multiple sources wired to one destination (fanin). The semantics is that the last sender's data is used.
    /// Analogous to Reactive Extensions without the duality with Iteration - the stream only uses hot observables, and never completes.
    /// Simple implementation is the IDataFlow interface. The wired source assigns a value to the Data in the interface which calls the 'set' implementaion in the wired destination.
    /// The get should not normally be needed in the interface at all - it is not used when data is transferred from source to destination - the receiver should not need to implement it. However it is there to allow the source to see what the last value it sent is. The current value may also have been sent by a different source (using fanin).
    /// Note the source can only be wired to one destination (no fanout). The destination can we wired from multiple sources (fanin allowed)
    /// Finally note that the source domain abstraction could choose to have a list of IDataFlow ports but this is not usual done becasue it complicates the sender abstraction. But it would allow the sender to have fanout. Normally fanout is accomplished by the alternative method of using a connector - see below.
    /// Note that this programming paradigm does not attempt to identify the sender to the receiver. That would create coupling and ALA is a zero coupling architecture.
    /// </summary>    
    /// <typeparam name="T">Generic data type</typeparam>
    public interface IDataFlow<T>
    {
        T Data { get; set; }
    }




    /// <summary>
    /// This second method of implementing the data-flow programming paradigm (using IDataFlowB) is more complicated, but overcomes a limitation in the C# language in the case where
    /// a domain abstraction needs to have more than one receive port. C# doesn't allow you to implement IDataFlow twice for a given type. To overcome this C# language limitation, the receiver uses multiple IDataFlowB fields instead of implementing IDataFlowB.
    /// So the implemetation is the reverse of IDataFlow - with IDataFlow, the sender requires the interface and the receiver implements the interface, but with IDataFlowB, the sender implements the interface and the receiver requires the interface.
    /// The interface itself is implemented with a get. The receiver can get the data at any time, but we want to keep the 'push' semantics of the paradigm, so a C# event is included in the interface to notify the receiver when the data changes.
    /// The receiver domain abstraction code should register a handler on this C# event if it wants to be notified (to keep the 'push' paradigm). This needs to be done at initialization time.
    /// To do that, the receiver domain abstraction should have a private function called xxxPostWiringInitialize(). xxx should be exactly the name of the IDataFlowB field.
    /// When the receiver is wired, xxxPostWiringInitialize will be called by the wiring engine.  e.g. always use this pattern of three lines of code when using IDataFlowB:
    /// 
    /// private IDataFlowB xxx;  // receive port 
    /// private void xxxPostWringInitialize() {xxx.DataChanged += xxxHandler;}
    /// private xxxHandler() { something = xxx.Data }
    /// 
    /// The sender code looks like this:
    /// 
    /// public event DataChangedDelegate DataChanged;  // implement the IDataFlowB's event
    /// T IDataFlowB&ltT&gt.Data { get &eq&gt Data; }  // implement the IDataFlowB's get
    /// DataChanged?.Invoke();    // notify destination to get the new data
    /// 
    /// Note however that the sender can only implement one IDataFlowB of a given type. You can't have the above three lines of code twice in one domain abstraction. So you can't have multiple send ports using IdataFlowB of a given type.
    /// For this reason, domain abstractions that are data flow sources usually do not use IDataFlowB, they use IDataFlow fields. 
    /// To wire a sender that uses an IDataFlow field to a receiver that implements an IDataFlow is straighforward.
    /// To wire a sender that uses an IDataFlow field to a receiver that uses an IDataFlowB field, simply use a connector (see below).
    /// So in general IDataFlowB should only be implemented by DataFlowConnector (below), not by domain abstractions.
    /// Unfortunately, when wiring up instances of two domain abstractions with this data-flow programming paradigm, you must know if the receiver implements IDataFlow or has a field of type IDataFlowB. If the latter, wire the sender to an instance of a connector, then wire the receiver to the same connector.
    /// (A future tool will take care of this automatically)
    /// IDataFlowB allows the receiver to have a 'list port', a port to which N senders can be wired (in an explicit order). Unlike with IDataFlow, this allows the destination to know which source is which.
    /// </summary>
    /// <typeparam name="T">Generic data type</typeparam>
    public interface IDataFlowB<T>
    {
        T Data { get; }
        event DataChangedDelegate DataChanged;
    }
    public delegate void DataChangedDelegate();




    /// <summary>
    /// The primary reason for the existence of the DataFlowConnector is discussed above in the explanation of the IDataFlowB Interface.
    /// 1. It allows a sender that has an IDataFlow field to be wired to a receiver that has an IDataFlowB field.
    /// However it also solves several other use cases.
    /// 2. It allows a sender that does not support fanout to be wired to multiple receivers (because the connector supports fanout). Furthermore, the receivers can be a mix of ones that implement IDataFlow and ones that have IDataFlowB fields.
    /// 3. You can think of it as an implementation of a semi-global variable, with the scope of the variable being the line connections on the diagram to and from the connector.
    /// The data is stored in the connector. Senders can change its value at any time and receivers can read its value at any time. 
    /// </summary>
    /// <typeparam name="T">Generic data type</typeparam>
    public class DataFlowConnector<T> : IDataFlow<T>, IDataFlowB<T>
    {
        // Properties
        public string InstanceName = "Default";
        public T Data;

        // Private fields

        // Ports
        // IDataFlow<T> input (implemented interface)
        // IDataflowB<T> outputs supporting fanout via IDataFlowB (implemented interface)
        private List<IDataFlow<T>> outputs = new List<IDataFlow<T>>(); // more outputs supporting fanout via IDataFlow

        /// <summary>
        /// Fans out a data flow to multiple data flows, or connect IDataFlow and IDataFlowB
        /// </summary>
        public DataFlowConnector() { }

        // IDataFlow<T> implementation ---------------------------------
        T IDataFlow<T>.Data
        {
            get => Data;
            set
            {
                Data = value;
                foreach (var f in outputs) f.Data = value;
                DataChanged?.Invoke();
            }
        }

        // IDataFlowB<T> implementation ---------------------------------
        public event DataChangedDelegate DataChanged;
        T IDataFlowB<T>.Data { get => Data; }
    }
}
