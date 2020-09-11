using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Libraries
{
    public static class Wiring
    {

        public delegate void PostWiringInitializeDelegate();
        public delegate void OutputDelegate(string output);

        private static string firstPortName;
        public static event OutputDelegate Output;

        /// <summary>
        /// Important method that wires and connects all the classes and interfaces together to run the application.
        /// If object A (this) has a private field of an interface, and object B implements the interface, then wire them together. Returns this for fluent style programming.
        /// ------------------------------------------------------------------------------------------------------------------
        /// WireTo methods five important keys:
        /// 1. wires match interfaces, A (calls interface) and B (implements)
        /// 2. interfaces must be all private for matching to happen
        /// 3. can wire multiple matching interfaces
        /// 4. wires in order form top to bottom of not yet wired
        /// 5. can ovveride order by specifying port names as second parameter
        /// 6. looks for list as well (be careful of blocking other interfaces from wiring)
        /// ------------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="A">
        /// The object on which the method is called is the object being wired from
        /// </param> 
        /// <param name="B">The object being wired to (must implement the interface)</param> 
        /// <returns></returns>
        /// <remarks>
        /// If A has two private fields of the same interface, the first compatible B object wired goes to the first one and the second compatible B object wired goes to the second.
        /// If A has multiple private interfaces of different types, only the first matching interface that B implements will be wired.
        /// In other words, by default, only one interface is wired between A and B
        /// To override this behaviour you can get give multiple interfaces in A a prefix "Pn_" where n is 0..9:
        /// Then a single wiring operation will wire all fieldnames with a consistent port prefix to the same B.
        /// These remarks apply only to single fields, not Lists.
        /// e.g.
        /// private IOneable client1Onabale;
        /// private ITwoable client1Twoable;
        /// private IThreeable client2;
        /// Clearly we want to wire two different clients. But if the first client wired implements all three interfaces, it will be wired to all three fields.
        /// So name the field like this:
        /// private IOneable P1_clientOnabale;
        /// private ITwoable P1_clientTwoable;
        /// private IThreeable P2_client;
        /// </remarks>
        public static T WireTo<T>(this T A, object B, string APortName = null, bool reverse = false)
        {
            // achieve the following via reflection
            // A.field = (<type of interface>)B;
            // or
            // A.list.Add( (<type of interface>)B );

            string multiportExceptionMessage = $"The following wiring failed because the two instances are already wired together by another port.";
            multiportExceptionMessage += "\nPlease use a new WireTo with this additional port name specified:";

            // Get the two instance name first for the Debug Output WriteLines
            var AinstanceName = A?.GetType().GetProperties().FirstOrDefault(f => f.Name == "InstanceName")?.GetValue(A);
            if (AinstanceName == null) AinstanceName = A?.GetType().GetFields().FirstOrDefault(f => f.Name == "InstanceName")?.GetValue(A);
            var BinstanceName = B?.GetType().GetProperties().FirstOrDefault(f => f.Name == "InstanceName")?.GetValue(B);
            if (BinstanceName == null) BinstanceName = B?.GetType().GetFields().FirstOrDefault(f => f.Name == "InstanceName")?.GetValue(B);

            string DiagnosticLine = $"Failed to wire {A?.GetType().Name}[{AinstanceName}].{APortName} to {B?.GetType().Name}[{BinstanceName}]";

            if (A == null)
            {
                throw new ArgumentException("A cannot be null "+DiagnosticLine);
            }
            if (B == null)
            {
                throw new ArgumentException("B cannot be null "+DiagnosticLine);
            }


            var BType = B.GetType();
            var AfieldInfos = A.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(f => (APortName == null || f.Name == APortName) && (!reverse ^ EndsIn_B(f.Name))).ToList(); // find the fields that the name meets all criteria
                                                                                                                   // TODO: when not reverse ports ending in _B should be excluded

            //if (A.GetType().Name=="DragDrop" && AinstanceName=="Initial Tag")
            //{ // for breakpoint
            //}
            var wiredSomething = false;
            firstPortName = null;
            foreach (var BimplementedInterface in BType.GetInterfaces()) // consider every interface implemented by B 
            {
                var AfieldInfo = AfieldInfos.FirstOrDefault(f => f.FieldType == BimplementedInterface && f.GetValue(A) == null); // find the first field in A that matches the interface type of B
                                                                                                                                 // TODO: the list case below should have the SamePort constraint as well

                // look for normal private fields first
                if (AfieldInfo != null)  // there is a match
                {

                    if (SamePort(AfieldInfo.Name))
                    {
                        if (wiredSomething)
                        {
                            // string exceptionMessage = multiportExceptionMessage + "\n" + Logging.WriteToWiringLog(A, B, AfieldInfo, save: false);
                            // throw new Exception(exceptionMessage);
                        }

                        AfieldInfo.SetValue(A, B);  // do the wiring
                        wiredSomething = true;
                        Debug.WriteLine($"{A.GetType().Name}[{AinstanceName}].{AfieldInfo.Name} wired to {BType.Name}[{BinstanceName}]");
                        // see if there is a PostWiring function associated with the port and call it.
                        var m = A.GetType().GetMethod($"{AfieldInfo.Name}PostWiringInitialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (m != null)
                        {
                            PostWiringInitializeDelegate handler = (PostWiringInitializeDelegate)Delegate.CreateDelegate(typeof(PostWiringInitializeDelegate), A, m);
                            handler();
                        }

                        // Logging.WriteToWiringLog(A, B, AfieldInfo);
                    }
                    continue;  // could be more than one interface to wire
                }

                // do the same as above for private fields that are a list of the interface of the matching type
                foreach (var AlistFieldInfo in AfieldInfos)
                {
                    if (!AlistFieldInfo.FieldType.IsGenericType) //not matching interface
                    {
                        continue;
                    }
                    var AListFieldValue = AlistFieldInfo.GetValue(A);

                    var AListGenericArguments = AlistFieldInfo.FieldType.GetGenericArguments();
                    if (AListGenericArguments.Length != 1) continue;    // A list should only have one type anyway 
                    if (AListGenericArguments[0].IsAssignableFrom(BimplementedInterface)) // JRS: There was some case where == didn't work, maybe in the gamescoring application
                    {
                        if (AListGenericArguments[0] != BimplementedInterface)
                        {
                            var g = AListGenericArguments[0];
                            if (g != typeof(object)) throw new Exception("Different types");
                            continue;
                        }
                        if (AListFieldValue == null)
                        {
                            var listType = typeof(List<>);
                            Type[] listParam = { BimplementedInterface };
                            AListFieldValue = Activator.CreateInstance(listType.MakeGenericType(listParam));
                            if (wiredSomething)
                            {

                                // string exceptionMessage = multiportExceptionMessage + "\n" + Logging.WriteToWiringLog(A, B, AListFieldValue, save: false);
                                // throw new Exception(exceptionMessage);
                            }

                            AlistFieldInfo.SetValue(A, AListFieldValue);
                        }

                        AListFieldValue.GetType().GetMethod("Add").Invoke(AListFieldValue, new[] { B });
                        wiredSomething = true;
                        // see if there is a PostWiringInitialize function associated with the port and call it.
                        var m = A.GetType().GetMethod($"{AlistFieldInfo.Name}PostWiringInitialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (m != null)
                        {
                            PostWiringInitializeDelegate handler = (PostWiringInitializeDelegate)Delegate.CreateDelegate(typeof(PostWiringInitializeDelegate), A, m);
                            handler();
                        }
                        Debug.WriteLine($"{A.GetType().Name}[{AinstanceName}].{AlistFieldInfo.Name} wired to {BType.Name}[{BinstanceName}]");
                        // Logging.WriteToWiringLog(A, B, AlistFieldInfo);
                        break;
                    }

                }
            }


            // Logging.WriteToWiringLog();

            if (!reverse && !wiredSomething)
            {
                if (APortName != null)
                {
                    // a specific port was specified so see if the port was already wired
                    var AfieldInfo = AfieldInfos.FirstOrDefault();
                    if (AfieldInfo?.GetValue(A) != null) throw new Exception($"Port already wired {A.GetType().Name}[{AinstanceName}].{APortName} to {BType.Name}[{BinstanceName}]");
                }
                throw new Exception(DiagnosticLine);
            }

            var method = A.GetType().GetMethod("PostWiringInitialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                PostWiringInitializeDelegate handler = (PostWiringInitializeDelegate)Delegate.CreateDelegate(typeof(PostWiringInitializeDelegate), A, method);
                PostWiringInializeAddHandler(handler);                
               
            }
            method = B.GetType().GetMethod("PostWiringInitialize", System.Reflection.BindingFlags.NonPublic);
            if (method != null)
            {
                PostWiringInitializeDelegate handler = (PostWiringInitializeDelegate)Delegate.CreateDelegate(typeof(PostWiringInitializeDelegate), B, method);
                PostWiringInializeAddHandler(handler);
            }
            return A;
        }

        /// <summary>
        /// Wire B to A and returns A. Used to wire objects to input ports of A.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A">The object being wire to</param>
        /// <param name="B">The object being wired from</param>
        /// <returns>A</returns>
        public static object WireFrom<T>(this object A, T B, string APortName = null)
        {
            B.WireTo(A, APortName);
            return A;
        }


        /// <summary>
        /// The SamePort function always returns true the first time it is called (for a given A and B) but on the second and subsequent calls
        /// it only returns true if the name has the same Px_ prefix as the first.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool SamePort(string name)
        {
            if (name.Length >= 3 && name[0] == 'P' && name[2] == '_' && name[1] >= '0' && name[1] <= '9')
            {
                string portName = name.Substring(0, 3);
                if (firstPortName == null)
                {
                    firstPortName = portName;
                }
                return portName.Equals(firstPortName);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool EndsIn_B(string s)
        {
            if (s == null) return false;
            var rv = s.Length > 2 && s.EndsWith("_B");
            if (rv)
            {
            }
            return s.Length > 2 && s.EndsWith("_B");
        }

        public static T IfWireTo<T>(this T A, bool condition, object B, string portName = null)
        {
            if (condition) A.WireTo(B, APortName: portName);
            return A;
        }

        public static T IfWireFrom<T>(this T A, bool condition, object B, string portName = null)
        {
            if (condition) A.WireFrom(B, APortName: portName);
            return A;
        }



        // PostWiringInitialize section

        // list of delegates alternative to a single event. This allows ordering of the handlers by adding to the list
        // Note that every delgate in the list is a single function, not a multicast. This allows us to see is handlers are already in the list more easily
        private static List<PostWiringInitializeDelegate> PostWiringInitializeDelegateList = new List<PostWiringInitializeDelegate>();

        private static void PostWiringInializeAddHandler(PostWiringInitializeDelegate handler)
        {
            // we can be called by PostWiringInitialize when a PostWiringInitialize function does some wiring.
            // Put new handlers at the end of the list (but make sure they are not in the list first)
            {
                if (!PostWiringInitializeDelegateList.Contains(handler)) PostWiringInitializeDelegateList.Add(handler);
            }
        }



        private static int PostWiringInitializeIndex = 0; // This Index marks where delegates have been invoked up to in the list
        public static void PostWiringInitialize()
        {

            for ( ; PostWiringInitializeIndex < PostWiringInitializeDelegateList.Count; PostWiringInitializeIndex++)  // cant use foreach because collection is modified during the looping
            {
                PostWiringInitializeDelegateList[PostWiringInitializeIndex].Invoke();
                // don't ever remove them from the list becasue we need to ensure they never get put into the list again, or called again if PostWiringInitialize is done again.
            }

        }



/*
        public static void PostWiringCallMeBack(PostWiringInitializeDelegate handler)
        {
            PostWiringInializeAddHandler(handler);
        }
*/


        private static void WriteLine(string output)
        {
            Output?.Invoke(output);
        }


        public static T DeleteWireTo<T>(this T A, object B, string APortName = null, bool reverse = false)
        {
            if (A == null)
            {
                throw new ArgumentException("A cannot be null");
            }
            if (B == null)
            {
                throw new ArgumentException("B cannot be null");
            }

            // achieve the following via reflection
            // A.field = null;
            // A.list.Remove( (<type of interface>)B );

            // Get the two instance name first for the Debug Output WriteLines
            var AinstanceName = A.GetType().GetProperties().FirstOrDefault(f => f.Name == "instanceName")?.GetValue(A);
            if (AinstanceName == null) AinstanceName = A.GetType().GetFields().FirstOrDefault(f => f.Name == "instanceName")?.GetValue(A);
            var BinstanceName = B.GetType().GetProperties().FirstOrDefault(f => f.Name == "instanceName")?.GetValue(B);
            if (BinstanceName == null) BinstanceName = B.GetType().GetFields().FirstOrDefault(f => f.Name == "instanceName")?.GetValue(B);


            var BType = B.GetType();
            var AfieldInfos = A.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(f => (APortName != null || f.Name == APortName) && (!reverse ^ EndsIn_B(f.Name))).ToList(); // find the fields that the name meets all criteria

            foreach (var BimplementedInterface in BType.GetInterfaces()) // consider every interface implemented by B 
            {
                var AfieldInfo = AfieldInfos.FirstOrDefault(f => f.FieldType == BimplementedInterface && f.GetValue(A) != null); // find the first field in A that matches the interface type of B
                                                                                                                                 // TODO: the list case below should have the SamePort constraint as well

                // look for normal private fields first
                if (AfieldInfo != null)  // there is a match
                {

                    if (SamePort(AfieldInfo.Name))
                    {
                        AfieldInfo.SetValue(A, null);  // do the wiring
                    }
                    continue;  // could be more than one interface to wire
                }

                // do the same as above for private fields that are a list of the interface of the matching type
                foreach (var AlistFieldInfo in AfieldInfos)
                {
                    if (!AlistFieldInfo.FieldType.IsGenericType) //not matching interface
                    {
                        continue;
                    }
                    var AListFieldValue = AlistFieldInfo.GetValue(A);

                    var AListGenericArguments = AlistFieldInfo.FieldType.GetGenericArguments();
                    if (AListGenericArguments.Length != 1) continue;    // A list should only have one type anyway 
                    if (AListGenericArguments[0].IsAssignableFrom(BimplementedInterface)) // JRS: There was some case where == didn't work, maybe in the gamescoring application
                    {
                        if (AListGenericArguments[0] != BimplementedInterface)
                        {
                            var g = AListGenericArguments[0];
                            if (g != typeof(object)) throw new Exception("Different types");
                            continue;
                        }
                        if (AListFieldValue == null)
                        {
                            var listType = typeof(List<>);
                            Type[] listParam = { BimplementedInterface };
                            AListFieldValue = Activator.CreateInstance(listType.MakeGenericType(listParam));

                            AlistFieldInfo.SetValue(A, AListFieldValue);
                        }

                        AListFieldValue.GetType().GetMethod("Remove").Invoke(AListFieldValue, new[] { B });
                        break;
                    }

                }
            }

            return A;
        }

    }
}
