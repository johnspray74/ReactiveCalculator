namespace Libraries
{
    /// <summary>
    /// Pattern that for use as an argument of the constructor of any class.
    /// It allows the constructor to first call a callback method that can be used to set properties in the new object before the rest of the constructor code executes.
    /// In the constructor, make sure the object is sufficiently ready to have the properties set, then call the callback, then do the rest of the construction code.
    /// example class:
    /// 
    /// class Example
    /// {
    ///     public Example(ConstructorCallbackDelegate ConstructorCallbackMethod = null) 
    ///     {
    ///         // construction code
    ///         ConstructorCallbackMethod?.Invoke();
    ///         // finish construction, can now use example property 
    ///     };
    ///     public string example {get; set; }
    /// }
    /// 
    /// Example of use
    /// instead of:
    /// 
    /// new Example() { example = "example" };
    /// 
    /// new Example(()=>{ example = "example"});
    /// 
    /// </summary>
    /// <param name="instance"></param>
    public delegate void ConstructorCallbackDelegate(object instance);

}