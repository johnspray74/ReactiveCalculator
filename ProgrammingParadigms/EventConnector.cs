using System;
using System.Collections.Generic;

namespace ProgrammingParadigms
{
    /// <summary>
    /// Events or observer pattern (publish/subscribe) without data.
    /// An example usage would be to wire a Button, which is an IEvent sender, to a FileBrowser, which is an IEvent receiver. When the button is pressed, the FileBrowser opens.
    /// Currently is synchronous only (TBD asynchronous version configurable per domain abstraction port)
    /// Analogous to Reactive Extensions without the duality with Iteration - the flow only uses hot observables, and never completes.
    /// Simple implementation is the IEvent interface. Wired sender calls a synchronous method (called Execute). Wired receiver implements a synchronous method (called Execute)
    /// Note the sender can only be wired to one receiver (no fanout). The receiver can we wired from multiple senders (fanin allowed)
    /// Finally note that the sender can choose to have a list of event port but this is not usual becasue it complicates the sender abstraction. But it would allow the sender to have fanout. Normally fanout is accomplished by the alternative method of using a connector - see below.
    /// Note that unlike C# events, this programming paradigm does not attempt to identify the sender to the receiver. That would create coupling and ALA is a zero coupling architecture.
    /// </summary>
    public interface IEvent
    {
        void Execute();
    }





    /// <summary>
    /// This second methiod of implementing the Event programming paradigm (using IEventB) is more complicated, but overcomes a limitation in the C# language in the case where
    /// a domain abstraction needs to have more than one receive port. (C# doesn't allow you to implement IEvent twice) To overcome this C# language limitation, the receiver has IEventB fields instead of implementing IEvent.
    /// So the implemetation is the reverse of IEvent - with IEventB, the sender implements the interface and the receiver requires the interface.
    /// The interface itself is implemented as a C# event. So don't confuse the ALA programming paradigm, IEvent and IEventB, with C# events. IEventB uses a C# event, IEvent uses a simple Execute method.
    /// The receiver domain abstraction code must register a handler on this C# event. This needs to be done at initialization time.
    /// To do that, the receiver domain abstraction should have a private function called xxxPostWiringInitialize(). xxx should be exactly the name of the IEventB field.
    /// When the receiver is wired, xxxPostWiringInitialize will be called by the wiring engine.  e.g. always use this pattern of three lines of code when using IEventB:
    /// 
    /// private IEventB xxx;  // receive port 
    /// private void xxxPostWringInitialize() {xxx.EventHappend += xxxHandler;}
    /// private xxxHandler() { code to handle the event }
    /// 
    /// The sender code looks like this:
    /// 
    /// public event ExecuteDelegate EventHappened;  // implement IEventB
    /// EventHappened?.Invoke();    // send event one IEventB
    /// 
    /// Note however that the sender can only implement one IEventB. You can't have the above two lines of code twice in one domain abstraction. So it you can't have multiple send ports using IEventB.
    /// For this reason, domain abstractions that are senders usually do not use IEventB, they use IEvent fields. 
    /// To wire a sender that uses an IEvent field to a receiver that implements an IEvent is straighforward.
    /// To wire a sender that uses an IEvent field to a receiver that uses an IEventB field, simply use a connector (see below).
    /// So unfortunately, when wiring up instances of two abstraction with this IEvent programming paradigm, you must know if the reciever implements IEvent or has a field of type IEventB. If the latter, wire the sender to an instance of a connector, then wire the receiver to the same connector.
    /// </summary>
    public interface IEventB
    {
        event ExecuteDelegate EventHappened;
    }

    public delegate void ExecuteDelegate();








    /// <summary>
    /// The primary reason for the existence of the EventCconnector is discussed above in the explanation of the IEventB Interface.
    /// 1. It allows a sender the has an IEvent field to be wired to a receiver that has an IEventB field.
    /// However it also solves several other use cases.
    /// 2. It allows a sender that does not support fanout to be wired to multiple receivers (because the connector supports fanout). Furthermore, the receivers can be a mix of ones that implement IEvent and ones that have IEventB fields.
    /// 3. many Connectors can be chain wired using the complete port to wire each connector to the next in the chain. This allows events to be generated along the chain in an explicit order effectively giving us ordered execution of sets of events. 
    /// </summary>
    public class EventConnector : IEvent, IEventB
    {
        // Properties
        public string InstanceName { get; set; }

        // outputs
        private List<IEvent> fanoutList = new List<IEvent>();
        private IEvent complete;

        /// <summary>
        /// Fans out an IEvent to multiple IEvents, or connect IEvent and IEventB
        /// </summary>
        public EventConnector() { }

        // IEvent implementation ------------------------------------
        void IEvent.Execute()
        {
            foreach (var fanout in fanoutList) fanout.Execute();
            EventHappened?.Invoke();
            complete?.Execute();
        }

        // IEventB implementation --------------------------------------
        public event ExecuteDelegate EventHappened;
    }
}
