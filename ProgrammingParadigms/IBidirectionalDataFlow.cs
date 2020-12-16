using Newtonsoft.Json;


namespace ProgrammingParadigms
{
    /// <summary>
    // This is a DataFlow interface that works in both directions. 
    // Assume an object A uses the interface and an object B implements it:
    // Each direction can be initiated by either end (push or pull)
    // A can push with APushToB and B can push with BPushToA
    // //  A can pull with APullFromB and B can pull with BPullFromA (not prefered)
    // Fan-in (many A wired to one B) is possible
    // // fan-in behaviour undefined for B pulls from A (probably will get last A that registered a handler)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IBidirectionalDataflow<T>
    {

        event PutData<T> BPushToA;
        // event GetData<T> BPullFromA;

        void APushToB(T data);

        // T APullFromB();
    }

    delegate void PutData<T>(object sender, T data);
    // delegate T GetData<T>(object sender);

}
