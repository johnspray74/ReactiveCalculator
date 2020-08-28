using System;
using System.Collections.Generic;
using System.Windows;

using Libraries;
using ProgrammingParadigms;
using DomainAbstractions;
using ALASandbox.ProgrammingParadigms;

namespace RequirementsAbstractions
{
    // TBD
    // Move the generated wiring code to its own function so that it can be called at runtime
    // Call the wiring code in PostWiringInitialize (will need to change the way PostWiring works for internal objects)
    // Work out how to append our InstanceName to all the intername instance names automatically


    /// <summary>
    /// Implements a row of a calculator
    /// Consists of 5 UI elements arrange horizontally: label, formula, result, units, description 
    /// The user enters a label in the label field which labels the output e.g. "velocity"
    /// The user enters a formula e.g. 2+3 e.g. velocity*time consisting of literal values, the labels (of other rows) and operators, including C# math library operators e.g. Sqrt(velocity)
    /// The input operands is a list of inputs which should have the results of all other rows wired to it in the order of the labels in the labelsCommaSeparated.
    /// </summary>
    class CalculatorRow : IUI, IDataFlowB<string>, IDataFlowB<double>
    {
        // properties
        public string InstanceName { get; set; }




        // Ports
        // (implemented) IUI parent out parent UI instance
        // (implemented) IDataFlowB<string> label (output) the value the user enters into the label field
        // (implemented) IDataFlowB<double> result (output) the result of the calculation
        List<IDataFlowB<double>> operands;
        IDataFlowB<string> labelsCommaSeparated; // (input) a comma separated list of all the labels corresponding one-to-one with the operands inputs



        /// <summary>
        /// Implements a row of a calculator
        /// Consists of 5 UI elements arrange horizontally: label, formula, result, units, description 
        /// The user enters a label in the label field which labels the output e.g. "velocity"
        /// The user enters a formula e.g. 2+3 e.g. velocity*time consisting of literal values, the labels (of other rows) and operators, including C# math library operators e.g. Sqrt(velocity)
        /// The input operands is a list of inputs which should have the results of all other rows wired to it in the order of the labels in the labelsCommaSeparated.
        /// </summary>
        public CalculatorRow()
        {
        }





        // port IUI parent
        IUI iuiInternal;
        UIElement IUI.GetWPFElement()
        {
            return iuiInternal.GetWPFElement();
        }




        // port IDataFlowB<string> label implementation
        private IDataFlowB<string> label1; // This is the label textbox in the internal wiring that implements IDataFlowB. To get its data use label1.Data. To know when there is a new label, attach a handler to label1.DataChanged

        event DataChangedDelegate labelChangedEvent;
        event DataChangedDelegate IDataFlowB<string>.DataChanged { add { labelChangedEvent += value; } remove { labelChangedEvent += value; } }
        // implement the get of our border interface through to the internal textBox for the label
        string IDataFlowB<string>.Data { get => label1.Data; }

        // implement the handler that is registered to the internal label textbox output and then invokes our external output. Note the registering to this handler is done in PostWiringInitialize below.
        private void labelChangedHandler()
        {
            labelChangedEvent?.Invoke();
        }





        // port IDataFlowB<double> result
        // The internal wiring implements IDataFlowB, which is an output. Our class, the containing class also implements IDataFlowB, which is an output. Multiple external objects will be wired to our output - (we have no way of knowing about them, and cant rewire them anyway). We need to implement IDataFlowB<operand>.
        private DataFlowConnector<double> resultConnector;  // This is the object in the internal wiring that implements IDataFlowB. To get its data use resultConnector.Data. To know when there is a new result, attach a handler to resultConnector.DataChanged

        // Implementing an IDataFlowB always involves a get and an event. 
        // implement the event (standard explicit implementation of an Interface event) plus invoke the event when the result changes
        event DataChangedDelegate resultChangedEvent;
        event DataChangedDelegate IDataFlowB<double>.DataChanged { add { resultChangedEvent += value; } remove { resultChangedEvent += value; } }

        // implement the get
        double IDataFlowB<double>.Data { get => resultConnector.Data; }

        // implement the handler that is registered to the internal wiring output result and then invokes our external output. Note the registering to this handler is done in PostWiringInitialize below.
        private void resultChangedHandler()
        {
            resultChangedEvent?.Invoke();
        }




        // port List<IDataFlowB<double>> operands;
        private Formula formulaInternal;





        // port IDataFlowB<string> labelsCommaSeparated
        // Wire our outer boundary, to the inner wiring. Inner wiring requires us to have an object that implements IDataFlowB<string> 
        // Two posibilities
        // Method 1 : use a DataFlowConnector object. Our boundary interface then has an event handler that gives the data to the connector
        // Method 2 : An object that implements IDataFlowB<string> will be wired to us from outside. We will have a reference to it in labelsCommaSeparated after that wiring is done. So wire the inner object directly to the object outside by copying the reference. The outside object is not wired to us when we are being constructed, so need to do it on the PostWiring event.

        // private IDataFlow<string> labelsCommaSeparatedInternalConnector = new DataFlowConnector<string>() { InstanceName = @"{this.InstanceName}_labelsCommaSeparatedInternalConnector" };  // method 1
        private IDataFlowB<string> labelsCommaSeparatedInternal; // method 2

        /*
        private void labelsCommaSeparatedChangedHandler()  // method 1
        {
            labelsCommaSeparatedInternalConnector.Data = labelsCommaSeparated.Data;
        }
        */




        private void PostWiringInitialize()
        {
            WireInternals();
        }


        // This Cant be called in the constructor because the last part of it needs to be done after the application wiring to/from us completed
        // This can be called either in the PostWiringInitialize or even later during runtime
        private void WireInternals()
        {
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind
            DataFlowBNull<string> id_b9e566abb4cc42d1a7d3927615231c50 = new DataFlowBNull<string>() { InstanceName = "dfbn" };
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<string> id_65f22d8aa160470e8da02d0fce01edca = new DataFlowConnector<string>() { InstanceName = "dfc2" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Horizontal id_6fe26e8021c64d8dad4e5b6016f7b659 = new Horizontal() { InstanceName = "horizontal", Ratios = new int[] { 1, 2, 2, 1, 3 }, MinWidths = new int[] { 50, 200, 520 } };
            NumberToString id_4c9cb86bce4544fe90c628e9eaecbcec = new NumberToString() { InstanceName = "numberToString" };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}") { InstanceName = "sf1" };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize = 50 };
            TextBox Description1 = new TextBox() { InstanceName = "Description1", FontSize = 50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize = 50 };
            TextBox Label1 = new TextBox() { InstanceName = "Label1", FontSize = 50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize = 50 };
            // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind


            id_b9e566abb4cc42d1a7d3927615231c50.InstanceName = $"{InstanceName}_{id_b9e566abb4cc42d1a7d3927615231c50.InstanceName}";
            dfc1.InstanceName = $"{InstanceName}_{dfc1.InstanceName}";
            dfc1.InstanceName = $"{InstanceName}_{dfc1.InstanceName}";
            id_65f22d8aa160470e8da02d0fce01edca.InstanceName = $"{InstanceName}_{id_65f22d8aa160470e8da02d0fce01edca.InstanceName}";
            Formula1.InstanceName = $"{InstanceName}_{Formula1.InstanceName}";
            id_6fe26e8021c64d8dad4e5b6016f7b659.InstanceName = $"{InstanceName}_{id_6fe26e8021c64d8dad4e5b6016f7b659.InstanceName}";
            id_4c9cb86bce4544fe90c628e9eaecbcec.InstanceName = $"{InstanceName}_{id_4c9cb86bce4544fe90c628e9eaecbcec.InstanceName}";
            sf1.InstanceName = $"{InstanceName}_{sf1.InstanceName}";
            Result1.InstanceName = $"{InstanceName}_{Result1.InstanceName}";
            Description1.InstanceName = $"{InstanceName}_{Description1.InstanceName}";
            FormulaText1.InstanceName = $"{InstanceName}_{FormulaText1.InstanceName}";
            Label1.InstanceName = $"{InstanceName}_{Label1.InstanceName}";
            Units1.InstanceName = $"{InstanceName}_{Units1.InstanceName}";

            // BEGIN AUTO-GENERATED WIRING FOR CalculatorRow.xmind
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Label1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Label1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(FormulaText1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (FormulaText1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Result1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (Text (Result1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Units1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Units1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Description1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Description1).child)
            Label1.WireTo(id_65f22d8aa160470e8da02d0fce01edca, "textOutput"); // (TextBox (Label1).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_65f22d8aa160470e8da02d0fce01edca).input)
            FormulaText1.WireTo(sf1, "textOutput"); // (TextBox (FormulaText1).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf1).input0)
            // id_b9e566abb4cc42d1a7d3927615231c50.WireTo(sf1, "output"); // (DataFlowBNull<string> (id_b9e566abb4cc42d1a7d3927615231c50).output) -- [List<IDataFlowB<string>>] --> (StringFormat<string> (sf1).inputs)
            sf1.WireTo(id_b9e566abb4cc42d1a7d3927615231c50, "inputs"); // (DataFlowBNull<string> (id_b9e566abb4cc42d1a7d3927615231c50).output) -- [List<IDataFlowB<string>>] --> (StringFormat<string> (sf1).inputs)
            sf1.WireTo(Formula1, "output"); // (StringFormat<string> (sf1).output) -- [IDataFlow<string>] --> (Formula (Formula1).formula)
            Formula1.WireTo(dfc1, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            dfc1.WireTo(id_4c9cb86bce4544fe90c628e9eaecbcec, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).input)
            id_4c9cb86bce4544fe90c628e9eaecbcec.WireTo(Result1, "output"); // (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
            // END AUTO-GENERATED WIRING FOR CalculatorRow.xmind



            iuiInternal = id_6fe26e8021c64d8dad4e5b6016f7b659;

            // id_b9e566abb4cc42d1a7d3927615231c50.WireTo(labelsCommaSeparatedInternalConnector);  // method 1
            labelsCommaSeparatedInternal = id_b9e566abb4cc42d1a7d3927615231c50; // method 2
            formulaInternal = Formula1;
            label1 = Label1;
            resultConnector = dfc1;




            label1.DataChanged += labelChangedHandler;
            resultConnector.DataChanged += resultChangedHandler;
            // labelsCommaSeparated.DataChanged += labelsCommaSeparatedChangedHandler; // method 1
            labelsCommaSeparatedInternal.WireTo(labelsCommaSeparated);  // method 2  labelsCommaSeparated has already been wired to an external implmentor of IDataFlowB, wire the interal wiring to that same external place. When the event goes off, only the internal wiring one will react and get the data.
            foreach (IDataFlowB<double> operand in operands) formulaInternal.WireTo(operand); // method2 operands is a list of IDataFlowB<doubles> which have by now been wired to multiple external implementors of IDataFlowB<double>. Wire the internal formulas operands to the same places.
        }

    }


    
    /// <summary>
    /// This class allows CalculatorRow to be manufactured as needed in an ALA wiring diagram.
    /// If CalculatorRows are static in the diagram just use CalculatorRow directly
    /// </summary>
    class CalculatorRowFactory : IFactoryMethod
    {
        public string InstanceName { get; set; } = "CalculatorRowFactory";

        object IFactoryMethod.FactoryMethod(string InstanceName)
        {
            return new CalculatorRow() { InstanceName = InstanceName };
        }
    }
}
