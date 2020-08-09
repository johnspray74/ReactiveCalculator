using DomainAbstractions;
using Libraries;
using ProgrammingParadigms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace StoryAbstractions
{

    /// <summary>
    /// Implements a row of a calculator
    /// Consists of 5 UI elements arrange horizontally: label, formula, result, units, description 
    /// The user enters a label in the label field which labels the output e.g. "velocity"
    /// The user enters a formula e.g. 2+3 e.g. velocity*time consisting of literal values, the labels (of other rows) and operators, including C# math library operators e.g. Sqrt(velocity)
    /// The input operands is a list of inputs which should have the results of all other rows wired to it in the order of the labels in the labelsCommaSeparated.
    /// </summary>
    class CalculatorRow : IUI, IDataFlowB<string>, IDataFlowB<double>
    {
        // Ports
        // (implemented) IDataFlowB<string> label (output) the value the user enters into the label field
        // (implemented) IDataFlowB<double> result (output) the result of the calculation

        List<IDataFlowB<double>> operands;
        IDataFlowB<string> labelsCommaSeparated; // (input) a comma separated list of all the labels corresponding one-to-one with the operands inputs

        public CalculatorRow()
        {
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<string> id_65f22d8aa160470e8da02d0fce01edca = new DataFlowConnector<string>() { InstanceName = "Default" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Horizontal id_6fe26e8021c64d8dad4e5b6016f7b659 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1, 2, 2, 1, 3 }, MinWidths = new int[] { 50, 200, 520 } };
            DataFlowBNull<string> id_b9e566abb4cc42d1a7d3927615231c50 = new DataFlowBNull<string>() { InstanceName = "Default" };
            NumberToString id_4c9cb86bce4544fe90c628e9eaecbcec = new NumberToString() { InstanceName = "Default" };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}") { InstanceName = "sf1" };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize = 50 };
            TextBox Description1 = new TextBox() { InstanceName = "Description1", FontSize = 50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize = 50 };
            TextBox Label1 = new TextBox() { InstanceName = "Label1", FontSize = 50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize = 50 };
            // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorRow.xmind

            // BEGIN AUTO-GENERATED WIRING FOR CalculatorRow.xmind
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Label1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Label1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(FormulaText1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (FormulaText1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Result1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (Text (Result1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Units1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Units1).child)
            id_6fe26e8021c64d8dad4e5b6016f7b659.WireTo(Description1, "children"); // (Horizontal (id_6fe26e8021c64d8dad4e5b6016f7b659).children) -- [List<IUI>] --> (TextBox (Description1).child)
            Label1.WireTo(id_65f22d8aa160470e8da02d0fce01edca, "textOutput"); // (TextBox (Label1).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_65f22d8aa160470e8da02d0fce01edca).input)
            FormulaText1.WireTo(sf1, "textOutput"); // (TextBox (FormulaText1).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf1).input0)
            id_b9e566abb4cc42d1a7d3927615231c50.WireTo(sf1, "output"); // (IDataFlowBNull (id_b9e566abb4cc42d1a7d3927615231c50).output) -- [List<IDataFlowB<string>>] --> (StringFormat<string> (sf1).inputs)
            sf1.WireTo(Formula1, "output"); // (StringFormat<string> (sf1).output) -- [IDataFlow<string>] --> (Formula (Formula1).formula)
            Formula1.WireTo(dfc1, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            Formula1.WireTo(dfc1, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).result)
            dfc1.WireTo(id_4c9cb86bce4544fe90c628e9eaecbcec, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).input)
            id_4c9cb86bce4544fe90c628e9eaecbcec.WireTo(Result1, "output"); // (NumberToString (id_4c9cb86bce4544fe90c628e9eaecbcec).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
            // END AUTO-GENERATED WIRING FOR CalculatorRow.xmind



            iuiInternal = id_6fe26e8021c64d8dad4e5b6016f7b659;

            id_b9e566abb4cc42d1a7d3927615231c50.WireTo(labelsCommaSeparatedInternalConnector);  // method 1
            labelsCommaSeparatedInternal = id_b9e566abb4cc42d1a7d3927615231c50; // method 2
        }







        // IDataFlowB<string> implementation
        event DataChangedDelegate IDataFlowB<string>.DataChanged { add { } remove { } }
        string IDataFlowB<string>.Data { get => text; }

        double IDataFlowB<double>.Data => throw new NotImplementedException();
        event DataChangedDelegate IDataFlowB<double>.DataChanged { add { } remove { } }

        IUI iuiInternal;



        UIElement IUI.GetWPFElement()
        {
            return iuiInternal.GetWPFElement();
        }




        // IDataFlowB<string> labelsCommaSeparated
        // Wire our outer boundary, to the inner wiring. Inner wiring requires us to have an object that implements IDataFlowB<string> 
        // Two posibilities
        // Method 1 : use a DataFlowConnector object. Our boundary interface will then have an event handler that gives the data to the connector
        // Method 2 : An object that implements IDataFlowB<string> will be wired to our to us from outside. We will have a reference to it in labelsCommaSeparated after that wiring is done later. So wire the inner object directly to the object outside by copying the reference. The outside object is not wired to us when we are being constructed, so need to do it on the PostWiring event.

        private void PostWiringInitialize()
        {
            labelsCommaSeparated.DataChanged += labelsCommaSeparatedChangedHandler; // method 1
            labelsCommaSeparatedInternal.WireTo(labelsCommaSeparated);  // method 2
        }


        private IDataFlow<string> labelsCommaSeparatedInternalConnector = new DataFlowConnector<string>();  // method 1
        IDataFlowB<string> labelsCommaSeparatedInternal; // method 2

        private void labelsCommaSeparatedChangedHandler()  // method 1
        {
            labelsCommaSeparatedInternalConnector.Data = labelsCommaSeparated.Data;
        }





    }
}
