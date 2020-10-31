using System;
using System.Collections.Generic;

using ProgrammingParadigms;
using Libraries;

namespace DomainAbstractions
{

    /// <summary>
    /// example
    /// WiringMethod = (newInstance) => { 
    ///     rows.WireTo(newInstance); 
    ///     labelsConcatenator.WireTo(newInstance, "inputs");
    ///     newInstance.WireTo(labelsConcatenatorConnector, "labelsCommaSeparated");
    /// },
    /// CrossWiringMethod = (instance1, instance2) => { instance2.WireFrom(instance1, "operands"); }
    /// </summary>
    class Multiple : IEvent
    {
        // properties
        public string InstanceName { get; set; }

        public delegate void WiringDelegate(object instance);
        public WiringDelegate WiringMethod { private get; set; }

        public delegate void CrossWiringDelegate(object instance1, object instance2);
        public CrossWiringDelegate CrossWiringMethod { private get; set; }


        public delegate void PostWiringDelegate(object instance);
        public PostWiringDelegate PostWiringInitializeMethod { private get; set; }

        public ConstructorCallbackDelegate ConstructorCallbackMethod { private get; set; }


        // ports
        // IEvent (implemented) Add another instance dynamically at run time
        private IFactoryMethod factory;




        private List<object> instances = new List<object>();

        public Multiple(int N)
        {
            this.N = N;
        }

        private int N;

        public void Generate()
        {
            for (int i = 0; i < N; i++)
            {
                object o = factory.FactoryMethod(InstanceName + i.ToString(), ConstructorCallbackMethod);
                instances.Add(o);
                WiringMethod(o);
            }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    CrossWiringMethod(instances[i], instances[j]);
        }



        // implement IEvent input port 
        void IEvent.Execute()
        {
            object fo = factory.FactoryMethod(InstanceName + instances.Count.ToString(), ConstructorCallbackMethod);
            instances.Add(fo);
            WiringMethod(fo);
            N = instances.Count;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i==N-1 || j==N-1) CrossWiringMethod(instances[i], instances[j]);
            PostWiringInitializeMethod(fo);
        }
    }
}
