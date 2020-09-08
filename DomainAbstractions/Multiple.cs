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

        public delegate void WiringDelegate(IFactoryObject instance);
        public WiringDelegate WiringMethod { private get; set; }

        public delegate void CrossWiringDelegate(IFactoryObject instance1, IFactoryObject instance2);
        public CrossWiringDelegate CrossWiringMethod { private get; set; }


        public delegate void PostWiringDelegate(IFactoryObject instance);
        public PostWiringDelegate PostWiringInitializeMethod { private get; set; }



        // ports
        // IEvent (implemented) Add another instance dynamically at run time
        private IFactoryMethod factory;




        private List<IFactoryObject> instances = new List<IFactoryObject>();

        public Multiple(int N)
        {
            this.N = N;
        }

        private int N;

        public void Generate()
        {
            for (int i = 0; i < N; i++)
            {
                IFactoryObject o = factory.FactoryMethod(InstanceName + i.ToString());
                instances.Add(o);
                WiringMethod(o);
            }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    CrossWiringMethod(instances[i], instances[j]);
            foreach (IFactoryObject fo in instances) fo.WireInternals();
        }



        // implement IEvent input port 
        void IEvent.Execute()
        {
            IFactoryObject fo = factory.FactoryMethod(InstanceName + instances.Count.ToString());
            instances.Add(fo);
            WiringMethod(fo);
            N = instances.Count;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i==N-1 || j==N-1) CrossWiringMethod(instances[i], instances[j]);
            // WireInternals gets called again for all instances to update themselves for any external wiring changes
            foreach (IFactoryObject o in instances) o.WireInternals();    // even the ones already wired get to do some extra wiring
            PostWiringInitializeMethod(fo);
        }
    }
}
