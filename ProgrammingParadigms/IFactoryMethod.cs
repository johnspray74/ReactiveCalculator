using Libraries;


namespace ProgrammingParadigms
{
    /// <summary>
    /// Allows a FactoryMethod pattern to be wired.
    /// FactoryMethod pattern variant is an abstraction instance knows WHEN to instantiate another abstraction, but NOT WHAT abstraction to instantiate.
    /// Wire a "xyzFactory" class that implements this interface that instantiates an xyz.
    /// The ConstructorCallbackMethod is useful because you cant pass any parameter into the constructor (because you dont know what class is being instantiated)
    /// So instead you can pass in a generic call back function. The new instantiated class should call this function as one of the first things it does in its constructor (if its not null)
    /// The Application can then provide a function that can set properties of the class
    /// </summary>
    interface IFactoryMethod
    {
        object FactoryMethod(string InstanceName, ConstructorCallbackDelegate ConstructorCallbackMethod = null);
    }


}
