using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ProgrammingParadigms;


namespace DomainAbstractions
{
    /// <summary>
    /// Combines/Splits json strings from/to its children
    /// Works in both directions by pushing
    /// If any one child changes a new 
    /// </summary>
    class JsonCombine : IBidirectionalDataflow<string> // parent
    {
        public string InstanceName { get; set; } = "Default";


        // Ports
        // IBidirectionalDataflow<string> parent
        private List<IBidirectionalDataflow<string>> children;



        private void childrenPostWiringInitialize()
        {
            children.Last().BPushToA += ChildChangedHandler;
            cache.Add(null);
        }


        private List<string> cache = new List<string>();
        private bool fields;


        public JsonCombine(bool fields = false )
        {
            this.fields = fields;
        }




        private void ChildChangedHandler(object sender, string json)
        {
            if (children == null) throw new Exception("How did this happen?");
            // Find which of our children called us
            int i = 0;
            while (i < children.Count)
            {
                if (Object.ReferenceEquals(sender, children[i]))
                {
                    break;
                }
                i++;
            }
            cache[i] = json;
            BPushToA.Invoke(this, SerializeCache());
        }



        private string SerializeCache()
        {
            if (fields)
            {
                return $"{{{String.Join(",",cache) }}}\n";
            }
            else
            {
                return JsonConvert.SerializeObject(cache);
            }
        }



        /*
        string IBidirectionalDataflow<string>.APullFromB()
        {
            int i = 0;
            foreach (IBidirectionalDataflow<string> c in children)
            {
                cache[i] = c.APullFromB();
                i++;
            }
            return DeserializeCache();
        }
        */



        // implement the IBidirectionalDataflow interface. We are the B side. We need to support push in both directions but not Pull

        private event PutData<string> BPushToA;
        event PutData<string> IBidirectionalDataflow<string>.BPushToA { add { BPushToA += value; } remove { BPushToA -= value; } }




        void IBidirectionalDataflow<string>.APushToB(string json)
        {
            if (json != null)
            {
                if (fields)
                {
                    string[] strs = json.Split(new char[] { '{', '}', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    strs = strs[0].Split(',');
                    int i = 0;
                    foreach (string s in strs)
                    {
                        cache[i] = s;
                        i++;
                    }
                }
                else
                {
                    Newtonsoft.Json.Linq.JArray obj = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(json);
                    string[] arr = obj.ToObject<string[]>();
                    cache = arr.ToList<string>();
                }
                {
                    int i;
                    for (i=0; i < children.Count; i++)
                    {
                        if (i < cache.Count)
                        {
                            if (cache[i] != null) children[i].APushToB(cache[i]);

                        }
                    }
                }
            }
        }
    }
}
