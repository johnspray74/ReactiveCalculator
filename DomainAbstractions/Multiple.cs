using ALASandbox.ProgrammingParadigms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainAbstractions
{







    /// <summary>
    /// example 
    /// WiringMethod = (newInstance) => {
    ///     rows.WireTo(newInstance);
    ///     newInstance.WireTo(labelsConcatenator,"label");
    ///     labelsConcatenatorConnector.WireTo(newInstance);
    /// }
    /// CrossWiringMethod = (instance1, instance2) => {
    ///     instance2.WireFrom(instance1,"operands"); // wire instance2 result to instance1 operands (B type interfaces so use WireFrom)
    /// }
    /// </summary>
    class Multiple
    {
        // properties
        public string InstanceName { get; set; }

        public delegate void WiringDelegate(object instance);
        public WiringDelegate WiringMethod { private get; set; }

        public delegate void CrossWiringDelegate(object instance1, object instance2);
        public CrossWiringDelegate CrossWiringMethod { private get; set; }

        // public delegate object FactoryDelegate();
        // public FactoryDelegate FactoryMethod { private get; set; }

        // ports
        private IFactoryMethod factory;




        private List<object> instances = new List<object>();

        public Multiple(int N)
        {
            this.N = N;
        }

        private int N;

        private void PostWiringInitialize()
        {
            for (int i = 0; i < N; i++)
            {
                object o = factory.FactoryMethod(InstanceName + i.ToString());
                instances.Add(o);
                WiringMethod(o);
            }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    CrossWiringMethod(instances[i], instances[j]);
        }

    }
}
