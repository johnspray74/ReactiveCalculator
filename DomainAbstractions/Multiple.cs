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
    class Multiple : IEvent, IBidirectionalDataflow<string> // addRow, persistence
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
        // IEvent addRow : Add another instance dynamically at run time
        private IFactoryMethod factory;




        private List<object> instances = new List<object>();

        public Multiple(int N)
        {
            this.N = N;
        }

        private int N;

        public void Generate()
        {
            int start = instances.Count;
            for (int i = start; i < N; i++)
            {

                object o = factory.FactoryMethod(InstanceName + i.ToString(), ConstructorCallbackMethod);
                instances.Add(o);
                WiringMethod(o);
                PostWiringInitializeMethod(o);
            }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i >= start || j >= start) CrossWiringMethod(instances[i], instances[j]);
            OutputForPersistence();
        }



        // implement IEvent input port 
        void IEvent.Execute()
        {
            N++;
            Generate();
            /*
            object fo = factory.FactoryMethod(InstanceName + instances.Count.ToString(), ConstructorCallbackMethod);
            instances.Add(fo);
            WiringMethod(fo);
            N = instances.Count;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i==N-1 || j==N-1) CrossWiringMethod(instances[i], instances[j]);
            */
        }


        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Implement IBidirectionalDataflow,string> peristence

        void IBidirectionalDataflow<string>.APushToB(string data)
        {
            if (data.Length > 0) // we will get null string here if nothing was ever enetered in the field
            {
                char[] separators = { ':', '"' };
                string[] strs = data.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (strs[0] == InstanceName)
                {
                    suppressLoop = true;
                    int n;
                    if (int.TryParse(strs[1], out n))
                    {
                        N = n;
                        Generate();
                    }
                }
                else
                {
                    throw new System.Exception("Wrong Json name");
                }
            }
        }

        private bool suppressLoop = true;



        private event PutData<string> BPushToA;
        event PutData<string> IBidirectionalDataflow<string>.BPushToA { add { BPushToA += value; } remove { BPushToA -= value; } }



        private void OutputForPersistence()
        {
            if (!suppressLoop) BPushToA?.Invoke(this, $"\"{InstanceName}\":\"{N}\""); // output Json for persistence
            suppressLoop = false;
        }

    }





}

