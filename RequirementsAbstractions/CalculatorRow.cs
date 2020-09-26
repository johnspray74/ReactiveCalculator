using System;
using System.Collections.Generic;
using System.Windows;

using DomainAbstractions;
using ProgrammingParadigms;
using Libraries;

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
    class CalculatorRow : IUI, IDataFlowB<string>, IDataFlowB<double>, ITestCalculatorRow
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
            WireInternals();
        }





        // port IUI parent
        IUI internalIUI;
        UIElement IUI.GetWPFElement()
        {
            return internalIUI.GetWPFElement();
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
        // Our class, the containing implements an IDataFlowB, which is an output. Multiple external objects will be wired to our output - (we have no way of knowing about them). 
        // Implementing an IDataFlowB always involves a get and an event. The get is for the outside world to get our result. The event allows us to tell the outside world there is a new result
        // implement the event in the interface (standard explicit implementation of an Interface event)
        event DataChangedDelegate resultChangedEvent;
        event DataChangedDelegate IDataFlowB<double>.DataChanged { add { resultChangedEvent += value; } remove { resultChangedEvent += value; } }
        // implement the get
        // The internal wiring implements IDataFlowB, which is an output. 
        private DataFlowConnector<double> internalResult;  // This is the object in the internal wiring that implements IDataFlowB. To get its data use resultConnector.Data. To know when there is a new result, attach a handler to resultConnector.DataChanged
        double IDataFlowB<double>.Data { get => internalResult.Data; } // When the outside world asks for our data, get it directly from the internal wiring, which is a 
        // implement the handler. It is registered to the internal wiring's resultConnector by the WireInternals method below. The handler notifies the outside world there is a new result so the outside world can get it using the above getter.
        private void resultChangedHandler()
        {
            resultChangedEvent?.Invoke();
        }




        // port List<IDataFlowB<double>> operands;
        private Formula internalFormula;





        // port IDataFlowB<string> labelsCommaSeparated
        // Wire our outer boundary, to the inner wiring. 
        // direction of dataflow is from the outside world into the internal wiring.
        // But internal wiring is a private field of IDataFlowB which must be wired to something that implements IdataFlowB
        // Although data is into the internal wiring, the WireTo is the opposite direction, as is always the case with IDataFlowB.
        // Two posibilities
        // Method 1 : use a DataFlowConnector object (or a DataFlowBNull). Our boundary interface then has an event handler that gives the data to the connector
        // Method 2 : An object that implements IDataFlowB<string> will be wired to us on the outside. We already have a reference to it in labelsCommaSeparated after that wiring is done. So wire the inner object directly to the object outside. The outside object is not wired to us when we are being constructed, so need to do it after construction and external wiring is done.
        // private IDataFlow<string> labelsCommaSeparatedInternalConnector = new DataFlowConnector<string>() { InstanceName = @"{this.InstanceName}_labelsCommaSeparatedInternalConnector" };  // method 1
        private IDataFlowB<string> labelsCommaSeparatedInternal; // method 2 // This is the object in interal wiring that has a field of type idataFlowB that must be wired to somethiing that implements IDataFlowB on the outside
        /*
        private void labelsCommaSeparatedChangedHandler()  // method 1
        {
            labelsCommaSeparatedInternalConnector.Data = labelsCommaSeparated.Data;
        }
        */






        private void WireInternals()
        {
            // Note WireInternals has been designed to be tolerant of when it is called relative to the when the external wiring is done. It can be called from the constructor, or even at runtime.
            if (internalWiringDone) throw new Exception("Please don't call WireInternals more than once");

            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind
            Horizontal id_fbb4036677904e0f8e202f89c9bbb336 = new Horizontal() { Ratios = new int[] { 1, 2, 2, 1, 3 }, MinWidths = new int[] { 50, 200, 520 } };
            TextBox labelText = new TextBox() { InstanceName = "labelText", FontSize = 25 };
            TextBox formulaText = new TextBox() { InstanceName = "formulaText", FontSize = 25 };
            Text resultText = new Text() { InstanceName = "resultText", FontSize = 25 };
            TextBox unitsText = new TextBox() { InstanceName = "unitsText", FontSize = 25 };
            TextBox descriptionText = new TextBox() { InstanceName = "descriptionText", FontSize = 25 };
            DataFlowConnector<string> id_d880f7031b0a4740ab84c54c3d05b2ac = new DataFlowConnector<string>() { };
            StringFormat<string> sf1 = new StringFormat<string>(literal: "({1})=>{0}") { InstanceName = "sf1" };
            DataFlowBNull<string> id_377d19f628da4a85956099dcdd66a3a0 = new DataFlowBNull<string>() { };
            Formula formula = new Formula() { InstanceName = "formula" };
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            NumberToString id_c4e62c93b07f48e3a8a9ff46a9ac5760 = new NumberToString() { NanDisplay = "" };            // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind
            /*
            DataFlowBNull<string> id_b9e566abb4cc42d1a7d3927615231c50 = new DataFlowBNull<string>() { InstanceName = "Default" };
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<string> id_65f22d8aa160470e8da02d0fce01edca = new DataFlowConnector<string>() { InstanceName = "Default" };
            Formula formula = new Formula() { InstanceName = "formula" };
            Horizontal id_6fe26e8021c64d8dad4e5b6016f7b659 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            NumberToString id_4c9cb86bce4544fe90c628e9eaecbcec = new NumberToString() { InstanceName = "Default" };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf1" };
            Text resultText = new Text() { InstanceName = "resultText", FontSize=25 };
            TextBox descriptionText = new TextBox() { InstanceName = "descriptionText", FontSize=25 };
            TextBox formulaText = new TextBox() { InstanceName = "formulaText", FontSize=25 };
            TextBox labelText = new TextBox() { InstanceName = "labelText", FontSize=25 };
            TextBox unitsText = new TextBox() { InstanceName = "unitsText", FontSize=25 };
            */

            // This code prepends the CalculatorRow instancename to the instanceNames of all the abstractions inside a CalculatorRow 
            // This code could be autogenerated too
            dfc1.InstanceName = $"{InstanceName}_{dfc1.InstanceName}";
            id_377d19f628da4a85956099dcdd66a3a0.InstanceName = $"{InstanceName}_{id_377d19f628da4a85956099dcdd66a3a0.InstanceName}";
            id_d880f7031b0a4740ab84c54c3d05b2ac.InstanceName = $"{InstanceName}_{id_d880f7031b0a4740ab84c54c3d05b2ac.InstanceName}";
            formula.InstanceName = $"{InstanceName}_{formula.InstanceName}";
            id_fbb4036677904e0f8e202f89c9bbb336.InstanceName = $"{InstanceName}_{id_fbb4036677904e0f8e202f89c9bbb336.InstanceName}";
            id_c4e62c93b07f48e3a8a9ff46a9ac5760.InstanceName = $"{InstanceName}_{id_c4e62c93b07f48e3a8a9ff46a9ac5760.InstanceName}";
            sf1.InstanceName = $"{InstanceName}_{sf1.InstanceName}";
            resultText.InstanceName = $"{InstanceName}_{resultText.InstanceName}";
            descriptionText.InstanceName = $"{InstanceName}_{descriptionText.InstanceName}";
            formulaText.InstanceName = $"{InstanceName}_{formulaText.InstanceName}";
            labelText.InstanceName = $"{InstanceName}_{labelText.InstanceName}";
            unitsText.InstanceName = $"{InstanceName}_{unitsText.InstanceName}";

            // BEGIN AUTO-GENERATED WIRING FOR CalculatorRow.xmind
            id_fbb4036677904e0f8e202f89c9bbb336.WireTo(labelText, "children");
            id_fbb4036677904e0f8e202f89c9bbb336.WireTo(formulaText, "children");
            id_fbb4036677904e0f8e202f89c9bbb336.WireTo(resultText, "children");
            id_fbb4036677904e0f8e202f89c9bbb336.WireTo(unitsText, "children");
            id_fbb4036677904e0f8e202f89c9bbb336.WireTo(descriptionText, "children");
            labelText.WireTo(id_d880f7031b0a4740ab84c54c3d05b2ac, "textOutput");
            formulaText.WireTo(sf1, "textOutput");
            id_c4e62c93b07f48e3a8a9ff46a9ac5760.WireTo(resultText, "output");
            sf1.WireTo(id_377d19f628da4a85956099dcdd66a3a0, "inputs");
            sf1.WireTo(formula, "output");
            formula.WireTo(dfc1, "result");
            dfc1.WireTo(id_c4e62c93b07f48e3a8a9ff46a9ac5760, "outputs");            // END AUTO-GENERATED WIRING FOR CalculatorRow.xmind

            /*
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(labelText, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (labelText).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(formulaText, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (formulaText).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(resultText, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (Text (resultText).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(unitsText, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (unitsText).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(descriptionText, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (descriptionText).child)
            labelText.WireTo(id_65f22d8aa160470e8da02d0fce01edca, "textOutput"); // (TextBox (labelText).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_65f22d8aa160470e8da02d0fce01edca).input)
            formulaText.WireTo(sf1, "textOutput"); // (TextBox (formulaText).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf1).input0)
            sf1.WireTo(id_b9e566abb4cc42d1a7d3927615231c50, "inputs"); // (StringFormat<string> (sf1).inputs) -- [IDataFlowB<string>] --> (DataFlowBNull<string> (id_b9e566abb4cc42d1a7d3927615231c50).output)
            sf1.WireTo(formula, "output"); // (StringFormat<string> (sf1).output) -- [IDataFlow<string>] --> (Formula (formula).formula)
            formula.WireTo(dfc1, "result"); // (Formula (formula).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            dfc1.WireTo(id_4c9cb86bce4544fe90c628e9eaecbcec, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).input)
            id_4c9cb86bce4544fe90c628e9eaecbcec.WireTo(resultText, "output"); // (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).output) -- [IDataFlow<string>] --> (Text (resultText).textInput)
            */

            internalIUI = id_fbb4036677904e0f8e202f89c9bbb336;

            // id_b9e566abb4cc42d1a7d3927615231c50.WireTo(labelsCommaSeparatedInternalConnector);  // method 1
            labelsCommaSeparatedInternal = id_377d19f628da4a85956099dcdd66a3a0; // method 2
            label1 = labelText;
            internalFormula = formula;
            internalResult = dfc1;




            label1.DataChanged += labelChangedHandler;

            // The output result from the formula in the internal wiring goes into a connector so it can fan out to many places.
            // We use the IdataflowB on this connector. IDataFlowB is implemented by the connector and exposes to us a getter so we can get the result. It also exposes an event which we can register to to know when there is a result
            // We register to that event now so we can relay the result to out outside border:
            internalResult.DataChanged += resultChangedHandler;

            // labelsCommaSeparated.DataChanged += labelsCommaSeparatedChangedHandler; // method 1



            internalWiringDone = true;  
            operandsPostWiringInitialize();   // Wire up any operands we already have wired externally. Note that operands can also be wired externally at run-time
            labelsCommaSeparatedPostWiringInitialize();  // Tolerate external wiring being done before or after internal wiring to remove temporal coupling




            internalFormulaText = formulaText;
            internalResultText = resultText;
            internalUnit = unitsText;
            internalDescription = descriptionText;

        }



        private bool internalWiringDone = false;
        private int nOperandsWired = 0;




        private void operandsPostWiringInitialize()
        {
            // Wire up any operands we already have wired externally. Note that operands can also be wired externally at run-time
            // We get called both by WireInternal above when it is finished and by the WireTo method every time our operands are externally wired
            // we need to wait until our internal wiring is done. Our operands may get wired externally before or after our internal wiring is done, sometimes both
            // The bool internalWiringDone and the counter nOperandsWired provide the logic for us to handle any order and so not expose any temporal coupling to the outside world
            if (internalWiringDone)
            {
                while (operands!=null && nOperandsWired < operands.Count)
                {
                    IDataFlowB<double> operand = operands[nOperandsWired];
                    internalFormula.WireTo(operand); // method2 operands is a list of IDataFlowB<doubles> which have by now been wired to multiple external implementors of IDataFlowB<double>. Wire the internal formulas operands to the same places.
                    nOperandsWired++;
                }
            }
        }



        private void labelsCommaSeparatedPostWiringInitialize()
        {
            // we are called both when internal wiring is complete and when labelsCommaSeparated is wired externally 
            // wait until both internal and external wiring is done - removes temporal coupling being exposed on the outside
            if (internalWiringDone && labelsCommaSeparated!=null)
            {
                labelsCommaSeparatedInternal.WireTo(labelsCommaSeparated);  // method 2  labelsCommaSeparated has already been wired to an external implmentor of IDataFlowB, wire the interal wiring to that same external place. When the event goes off, only the internal wiring one will react and get the data.
            }
        }


        // This must be called after a new row is created and completely wired
        public CalculatorRow Initialize()
        {
            labelChangedHandler();  // Send out our blank label. This allows the external wiring to react to our presence at run-time (example case we are a newly added row, we wont have the commaSeparatedValue at our input until any label is changed to push it through the application wiring
            return this;  // support fluent pattern
        }






        // Testing interface
        // This interface faces upward, which means it is intended to be used by our client who also used our public interface to construct us
        // This interface allows the client to insert test data into its wiring

        TextBox internalFormulaText;
        Text internalResultText;
        TextBox internalUnit;
        TextBox internalDescription;

        void ITestCalculatorRow.EnterFormula(string formula)
        {
            ((TextBox)internalFormulaText).Text = formula;
        }

        void ITestCalculatorRow.EnterLabel(string label)
        {
            ((TextBox)label1).Text = label;
        }

        string ITestCalculatorRow.ReadResult()
        {
            return ((IDataFlow<string>)internalResultText).Data; 
        }

        void ITestCalculatorRow.EnterUnit(string unit)
        {
            ((TextBox)internalUnit).Text = unit;
        }

        void ITestCalculatorRow.EnterDescription(string description)
        {
            ((TextBox)internalDescription).Text = description;
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
            return (object) new CalculatorRow() { InstanceName = InstanceName };
        }
    }




    interface ITestCalculatorRow
    {
        void EnterFormula(string formula);
        void EnterLabel(string label);
        string ReadResult();
        void EnterUnit(string unit);
        void EnterDescription(string description);
    }

}
