using System;
using System.Collections.Generic;


namespace ALASandbox.ProgrammingParadigms
{
    /// <summary>
    /// Allows a FactoryMethod pattern to be wired.
    /// FactoryMethod pattern variant is a an abstraction instance knows when to instantiate another abstraction, but only application knows which abstraction that should be.
    /// </summary>
    interface IFactoryMethod
    {
        object FactoryMethod(string InstanceName);
    }
}
