using System;
using System.Collections.Generic;


using DomainAbstractions;
using ProgrammingParadigms;
using Libraries;

namespace Application
{
    public class Application
    {
        private MainWindow mainWindow;  // use this version for calculatorNRows with the tests - we let the diagram instantiate the MainWindow and then copy to this reference so we Run it
        private CalculatorNRows testCalculator;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Initialize();
            app.mainWindow.WireTo(app.testCalculator, "appStart");   // This is just for running the tests. They dont run yet - they use appStart IEvent which is an output port of MainWindow when the window is loaded
            app.mainWindow.Run();
        }

        private Application()
        {
            // These are all the different versions of the calculator
            // Some use auto-gnerated code from their respective diagram
            // uncomment one
            testCalculator = new CalculatorNRows(out mainWindow);
            // mainWindow = CalculatorNRows();   // replaced with the tested version
            // mainWindow = Calculator10Rows();
            // mainWindow = Calculator2ARows();
            // mainWindow = Calculator2Rows();
            // mainWindow = Calculator2RowHandWired();
            // mainWindow = Calculator1Row(); 
            // mainWindow = CalculatorBasic();
            // mainWindow = CalculatorBasicHandWired(); // fails
            // mainWindow = HelloWorld();
            // new TransformOperator("^", "Pow", rightAssociative: true); // testing only
            new NumberFormatting(); // Testing only
        }

        private Application Initialize()
        {
            Wiring.PostWiringInitialize();
            return this;
        }



        private class CalculatorNRows : IEvent
        {
            // using a class for CalculatorRows instead of a method allows us to encapsulate acceptance tests with the application wiring
            // It also allows us to wire this class to the mainWindow so that after the mainWindow is loaded, it sends us an event to make the tests run (at run-time)


            Vertical _rows;
            StringConcat _labelsConcatenator;



            public CalculatorNRows(out MainWindow mw)
            {
                // These Ratios and MinWidths are for the columns of the calculator
                // Label, Formula, TextBook, Result, Units, Description, Format, Digits
                int[] Ratios = new int[] { 4, 8, 8, 8, 4, 8, 1 };
                int[] MinWidths = new int[] { 50, 100, 100, 100, 50, 50, 57 };  // 57 is just big enough to show the Fmt enum selections
                int FontSize = 25;

                // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows.xmind
                Button id_803db86064414b379608f65bc07098bc = new Button("Add row" ) { InstanceName = "Default", FontSize=FontSize };
                CalculatorRowFactory id_012306911dbe485c91ecd24bd35b2420 = new CalculatorRowFactory() { InstanceName = "Default" };
                DataFlowConnector<string> labelsConcatenatorConnector = new DataFlowConnector<string>() { InstanceName = "labelsConcatenatorConnector" };
                Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default", Ratios = Ratios, MinWidths = MinWidths };
                Horizontal id_aa2f23f75c79479e88ccf7ed0ed6c2cc = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,8 }, MinWidths = new int[] { 50 } };
                MainWindow mainWindow = new MainWindow("Reactive Calculator" ) { InstanceName = "mainWindow" };
                Multiple MultipleRow = new Multiple(N:4 ) { InstanceName = "MultipleRow", ConstructorCallbackMethod = (instance) => {   ((CalculatorRow)instance).FontSize = FontSize;  ((CalculatorRow)instance).Ratios = Ratios;   ((CalculatorRow)instance).MinWidths = MinWidths; }, WiringMethod = (newInstance) => {  _rows.WireTo(newInstance); _labelsConcatenator.WireTo(newInstance,"inputs");   newInstance.WireTo(labelsConcatenatorConnector,"labelsCommaSeparated");  testCalculatorRows.Add((ITestCalculatorRow)newInstance); }, CrossWiringMethod = (instance1,instance2) => { instance2.WireFrom(instance1,"operands"); }, PostWiringInitializeMethod = delegate(object instance) {   _rows.AddRows();  ((CalculatorRow)instance).Initialize();}  };
                Space id_68d3e779ba0d4f78ad48db2ed468608c = new Space() { InstanceName = "Default" };
                StringConcat labelsConcatenator = new StringConcat() { InstanceName = "labelsConcatenator", Separator="," };
                Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_93a237ff714b48748a4ba10ede42d2dc = new Text("Description" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_a72464a6a1a8426887ca40b886b5567e = new Text("Textbook" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_f9b8d9329de5407b93a1834afeaf5de6 = new Text("Fmt" ) { InstanceName = "Default", FontSize=FontSize };
                Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Reactive Calculator" ) { InstanceName = "Default", FontSize=FontSize };
                TextBox id_b84a8eee3a554afaad9fa90ac6b594f9 = new TextBox() { InstanceName = "Default", Text="Title your calculation here", FontSize=15 };
                Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
                Vertical rows = new Vertical() { InstanceName = "rows" };
                // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows.xmind

                // BEGIN AUTO-GENERATED WIRING FOR CalculatorNRows.xmind
                mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
                labelsConcatenator.WireTo(labelsConcatenatorConnector, "output"); // (StringConcat (labelsConcatenator).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).input)
                id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
                id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_b84a8eee3a554afaad9fa90ac6b594f9, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (TextBox (id_b84a8eee3a554afaad9fa90ac6b594f9).child)
                id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
                id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(rows, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Vertical (rows).Child)
                id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_aa2f23f75c79479e88ccf7ed0ed6c2cc, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).Child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_a72464a6a1a8426887ca40b886b5567e, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_a72464a6a1a8426887ca40b886b5567e).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_93a237ff714b48748a4ba10ede42d2dc, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_93a237ff714b48748a4ba10ede42d2dc).child)
                id_24914ab245484fe1b70af8020ca2e831.WireTo(id_f9b8d9329de5407b93a1834afeaf5de6, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_f9b8d9329de5407b93a1834afeaf5de6).child)
                id_aa2f23f75c79479e88ccf7ed0ed6c2cc.WireTo(id_803db86064414b379608f65bc07098bc, "children"); // (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).children) -- [List<IUI>] --> (Button (id_803db86064414b379608f65bc07098bc).child)
                id_aa2f23f75c79479e88ccf7ed0ed6c2cc.WireTo(id_68d3e779ba0d4f78ad48db2ed468608c, "children"); // (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).children) -- [List<IUI>] --> (Space (id_68d3e779ba0d4f78ad48db2ed468608c).child)
                id_803db86064414b379608f65bc07098bc.WireTo(MultipleRow, "eventButtonClicked"); // (Button (id_803db86064414b379608f65bc07098bc).eventButtonClicked) -- [IEvent] --> (Multiple (MultipleRow).addRow)
                MultipleRow.WireTo(id_012306911dbe485c91ecd24bd35b2420, "factory"); // (Multiple (MultipleRow).factory) -- [IFactoryMethod] --> (CalculatorRowFactory (id_012306911dbe485c91ecd24bd35b2420).factory)
                // END AUTO-GENERATED WIRING FOR CalculatorNRows.xmind

                // These are used to solve compiler error "Cannot use local variable 'rows' before it is declared" in the lambda functions in the wiring code above if they reference 'rows' and 'labelsConcetenator' instead;
                _rows = rows;  
                _labelsConcatenator = labelsConcatenator;  // 

                // This tell MultipleRow object to go ahead an create calculator rows (each of which will use the lambdas above to wire into the rest of the application)
                MultipleRow.Generate();


                // these are used for testing the application to set the title and press the button
                title = id_b84a8eee3a554afaad9fa90ac6b594f9;
                addRowButton = id_803db86064414b379608f65bc07098bc;

                mw = mainWindow;
            }


            // Testing code for the application. Basic strategy is to inject into the UI input opbects and test the results at the UI output objects
            // To do this we will need to access the calculatorRow objects, each of which represents a calculator (ui text boxes for label, formula, result) so we need to put them in a list




            private List<ITestCalculatorRow> testCalculatorRows = new List<ITestCalculatorRow>();
            TextBox title;
            ITestButton addRowButton;

            public void Test()
            {
                title.Text = "Calculate kinetic and potential energy of a satellite";
                // assertStringEq(id_b84a8eee3a554afaad9fa90ac6b594f9.Text, "Calculate kinetic and potential energy of a satellite");

                testCalculatorRows[0].EnterLabel("re");
                testCalculatorRows[0].EnterFormula("6.37e6");
                testCalculatorRows[0].EnterUnit("m");
                testCalculatorRows[0].EnterDescription("radius earth");
                assertStringEq(testCalculatorRows[0].ReadResult(), "6370000");

                testCalculatorRows[1].EnterLabel("me");
                testCalculatorRows[1].EnterFormula("5.98e24");
                testCalculatorRows[1].EnterUnit("kg");
                testCalculatorRows[1].EnterDescription("mass earth");
                assertStringEq(testCalculatorRows[1].ReadResult(), "5.98E+24");

                testCalculatorRows[2].EnterLabel("G");
                testCalculatorRows[2].EnterFormula("6.673e-11");
                testCalculatorRows[2].EnterUnit("Nm2/kg2");
                testCalculatorRows[2].EnterDescription("gravitational constant");
                testCalculatorRows[2].EnterFormatDigits("4");
                assertStringEq(testCalculatorRows[2].ReadResult(), "6.673E-11");

                testCalculatorRows[3].EnterLabel("alt");
                testCalculatorRows[3].EnterFormula("200e3");
                testCalculatorRows[3].EnterUnit("m");
                testCalculatorRows[3].EnterDescription("altitude of satellite");
                assertStringEq(testCalculatorRows[3].ReadResult(), "200000");

                addRowButton.Click();
                testCalculatorRows[4].EnterLabel("mass");
                testCalculatorRows[4].EnterFormula("100");
                testCalculatorRows[4].EnterUnit("kg");
                testCalculatorRows[4].EnterDescription("mass of satellite");
                assertStringEq(testCalculatorRows[4].ReadResult(), "100");

                addRowButton.Click();
                testCalculatorRows[5].EnterLabel("vel");
                testCalculatorRows[5].EnterFormula("Sqrt(G*me/(re+alt))");
                testCalculatorRows[5].EnterUnit("m/s");
                testCalculatorRows[5].EnterDescription("velocity of satellite");
                assertStringEq(testCalculatorRows[5].ReadResult().Substring(0,4), "7793");

                addRowButton.Click();
                testCalculatorRows[6].EnterLabel("ke");
                testCalculatorRows[6].EnterFormula("0.5*mass*vel^2");
                testCalculatorRows[6].EnterUnit("J");
                testCalculatorRows[6].EnterDescription("kinetic energy of satellite");
                assertStringEq(testCalculatorRows[6].ReadResult().Substring(0, 10), "3036875190");

                addRowButton.Click();
                testCalculatorRows[7].EnterLabel("g");
                testCalculatorRows[7].EnterFormula("G*me/re^2");
                testCalculatorRows[7].EnterUnit("m/s2");
                testCalculatorRows[7].EnterDescription("gravitational acceleration at earth's surface");
                assertStringEq(testCalculatorRows[7].ReadResult().Substring(0, 4), "9.83");

                addRowButton.Click();
                testCalculatorRows[8].EnterLabel("pe");
                testCalculatorRows[8].EnterFormula("mass*alt*g");
                testCalculatorRows[8].EnterUnit("J");
                testCalculatorRows[8].EnterDescription("potential energy of satellite");
                assertStringEq(testCalculatorRows[8].ReadResult().Substring(0, 9), "196685996");

                addRowButton.Click();
                testCalculatorRows[9].EnterLabel("");
                testCalculatorRows[9].EnterFormula("ke/pe");
                testCalculatorRows[9].EnterUnit("");
                testCalculatorRows[9].EnterDescription("ratio kinetic to potential energy");
                assertStringEq(testCalculatorRows[9].ReadResult().Substring(0, 2), "15");

                addRowButton.Click();
                testCalculatorRows[10].EnterLabel("");
                testCalculatorRows[10].EnterFormula("ke/mass/g + alt");
                testCalculatorRows[10].EnterUnit("m");
                testCalculatorRows[10].EnterDescription("equiv altitude of total energy");
                assertStringEq(testCalculatorRows[10].ReadResult().Substring(0, 7), "3288044");

            }





            private void assertStringEq(string a, string b)
            {
                if (a != b) throw new Exception($"Failed: {a} should equal {b}");
            }



            // We want to run the tests at run-time (after the MianWindow.Run) does not return until the application closes
            // so this interface allows the test class to be wired to the mainWindow's appStart port 
            void IEvent.Execute()
            {
                Test();
            }
        }





        /*

                public void CalculatorNRows()
                {
                    Vertical rows = new Vertical() { InstanceName = "rows" };
                    StringConcat labelsConcatenator = new StringConcat() { InstanceName = "labelsConcatenator", Separator = "," };
                    // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows2.xmind
                    Button id_803db86064414b379608f65bc07098bc = new Button("Add row") { InstanceName = "Default", FontSize = 25 };
                    CalculatorRowFactory id_012306911dbe485c91ecd24bd35b2420 = new CalculatorRowFactory() { InstanceName = "Default" };
                    DataFlowConnector<string> labelsConcatenatorConnector = new DataFlowConnector<string>() { InstanceName = "labelsConcatenatorConnector" };
                    Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1, 2, 2, 1, 3 }, MinWidths = new int[] { 50, 200, 520 } };
                    Horizontal id_aa2f23f75c79479e88ccf7ed0ed6c2cc = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1, 8 }, MinWidths = new int[] { 50 } };
                    Multiple MultipleRow = new Multiple(N: 4) { InstanceName = "MultipleRow", WiringMethod = (newInstance) => { rows.WireTo(newInstance); labelsConcatenator.WireTo(newInstance, "inputs"); newInstance.WireTo(labelsConcatenatorConnector, "labelsCommaSeparated"); }, CrossWiringMethod = (instance1, instance2) => { instance2.WireFrom(instance1, "operands"); }, PostWiringInitializeMethod = delegate (object instance) { rows.AddRows(); ((CalculatorRow)instance).Initialize(); } };
                    Space id_68d3e779ba0d4f78ad48db2ed468608c = new Space() { InstanceName = "Default" };
                    Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units") { InstanceName = "Default", FontSize = 25 };
                    Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula") { InstanceName = "Default", FontSize = 25 };
                    Text id_93a237ff714b48748a4ba10ede42d2dc = new Text("Description") { InstanceName = "Default", FontSize = 25 };
                    Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label") { InstanceName = "Default", FontSize = 25 };
                    Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result") { InstanceName = "Default", FontSize = 25 };
                    Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Reactive Calculator") { InstanceName = "Default", FontSize = 25 };
                    TextBox id_b84a8eee3a554afaad9fa90ac6b594f9 = new TextBox() { InstanceName = "Default", Text = "Title your calculation here", FontSize = 15 };
                    Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
                    // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows2.xmind
                    // ((CalculatorRow)newInstance).WireInternals();

                    // BEGIN AUTO-GENERATED WIRING FOR CalculatorNRows2.xmind
                    mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
                    labelsConcatenator.WireTo(labelsConcatenatorConnector, "output"); // (@StringConcat (labelsConcatenator).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).input)
                    id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
                    id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_b84a8eee3a554afaad9fa90ac6b594f9, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (TextBox (id_b84a8eee3a554afaad9fa90ac6b594f9).child)
                    id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
                    id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(rows, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (@Vertical (rows).Child)
                    id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_aa2f23f75c79479e88ccf7ed0ed6c2cc, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).Child)
                    id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
                    id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
                    id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
                    id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
                    id_24914ab245484fe1b70af8020ca2e831.WireTo(id_93a237ff714b48748a4ba10ede42d2dc, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_93a237ff714b48748a4ba10ede42d2dc).child)
                    id_aa2f23f75c79479e88ccf7ed0ed6c2cc.WireTo(id_803db86064414b379608f65bc07098bc, "children"); // (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).children) -- [List<IUI>] --> (Button (id_803db86064414b379608f65bc07098bc).child)
                    id_aa2f23f75c79479e88ccf7ed0ed6c2cc.WireTo(id_68d3e779ba0d4f78ad48db2ed468608c, "children"); // (Horizontal (id_aa2f23f75c79479e88ccf7ed0ed6c2cc).children) -- [List<IUI>] --> (Space (id_68d3e779ba0d4f78ad48db2ed468608c).child)
                    id_803db86064414b379608f65bc07098bc.WireTo(MultipleRow, "eventButtonClicked"); // (Button (id_803db86064414b379608f65bc07098bc).eventButtonClicked) -- [IEvent] --> (Multiple (MultipleRow).addRow)
                    MultipleRow.WireTo(id_012306911dbe485c91ecd24bd35b2420, "factory"); // (Multiple (MultipleRow).factory) -- [IFactoryMethod] --> (CalculatorRowFactory (id_012306911dbe485c91ecd24bd35b2420).factory)
                    // END AUTO-GENERATED WIRING FOR CalculatorNRows2.xmind
                    MultipleRow.Generate();
                }
        */

        

        private MainWindow Calculator10Rows()
        {
            MainWindow mainWindow = new MainWindow("Calculator"); 
            Vertical rows = new Vertical() { InstanceName = "rows" };
            StringConcat labelsConcatenator = new StringConcat() { InstanceName = "labelsConcatenator", Separator = "," };
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator10Rows.xmind
            CalculatorRowFactory id_012306911dbe485c91ecd24bd35b2420 = new CalculatorRowFactory() { InstanceName = "Default" };
            DataFlowConnector<string> labelsConcatenatorConnector = new DataFlowConnector<string>() { InstanceName = "labelsConcatenatorConnector" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Multiple MultipleRow = new Multiple(N:10 ) { InstanceName = "MultipleRow", WiringMethod = (newInstance) => { rows.WireTo(newInstance); labelsConcatenator.WireTo(newInstance,"inputs"); newInstance.WireTo(labelsConcatenatorConnector,"labelsCommaSeparated"); }, CrossWiringMethod = (instance1,instance2) => { instance2.WireFrom(instance1,"operands"); } };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=25 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=25 };
            Text id_93a237ff714b48748a4ba10ede42d2dc = new Text("Description" ) { InstanceName = "Default", FontSize=25 };
            Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=25 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=25 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Reactive Calculator" ) { InstanceName = "Default", FontSize=25 };
            TextBox id_b84a8eee3a554afaad9fa90ac6b594f9 = new TextBox() { InstanceName = "Default", Text="Title your calculation here", FontSize=15 };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator10Rows.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator10Rows.xmind
            mainWindow.WireTo(rows, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (@Vertical (rows).child)
            labelsConcatenator.WireTo(labelsConcatenatorConnector, "output"); // (@StringConcat (labelsConcatenator).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).input)
            rows.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (@Vertical (rows).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            rows.WireTo(id_b84a8eee3a554afaad9fa90ac6b594f9, "children"); // (@Vertical (rows).children) -- [List<IUI>] --> (TextBox (id_b84a8eee3a554afaad9fa90ac6b594f9).child)
            rows.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (@Vertical (rows).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_93a237ff714b48748a4ba10ede42d2dc, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_93a237ff714b48748a4ba10ede42d2dc).child)
            MultipleRow.WireTo(id_012306911dbe485c91ecd24bd35b2420, "factory"); // (Multiple (MultipleRow).factory) -- [IFactoryMethod] --> (CalculatorRowFactory (id_012306911dbe485c91ecd24bd35b2420).factory)
            // END AUTO-GENERATED WIRING FOR Calculator10Rows.xmind
            MultipleRow.Generate();
            return mainWindow;
        }



        private MainWindow Calculator2ARows()
        {
            var mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator2ARows.xmind
            CalculatorRow Row1 = new CalculatorRow() { InstanceName = "Row1" };
            CalculatorRow Row2 = new CalculatorRow() { InstanceName = "Row2" };
            DataFlowConnector<string> labelsConcatenatorConnector = new DataFlowConnector<string>() { InstanceName = "labelsConcatenatorConnector" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            StringConcat labelsConcatenator = new StringConcat() { InstanceName = "labelsConcatenator", Separator="," };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=50 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=50 };
            Text id_93a237ff714b48748a4ba10ede42d2dc = new Text("Description" ) { InstanceName = "Default", FontSize=50 };
            Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=50 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=50 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Vertical rows = new Vertical() { InstanceName = "rows" };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator2ARows.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator2ARows.xmind
            mainWindow.WireTo(rows, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (rows).child)
            rows.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (rows).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            rows.WireTo(Row1, "children"); // (Vertical (rows).children) -- [List<IUI>] --> (CalculatorRow (Row1).child)
            rows.WireTo(Row2, "children"); // (Vertical (rows).children) -- [List<IUI>] --> (CalculatorRow (Row2).child)
            rows.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (rows).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_93a237ff714b48748a4ba10ede42d2dc, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_93a237ff714b48748a4ba10ede42d2dc).child)
            labelsConcatenator.WireTo(Row1, "inputs"); // (StringConcat (labelsConcatenator).inputs) -- [IDataFlowB<string>] --> (CalculatorRow (Row1).label)
            Row2.WireTo(Row1, "operands"); // (CalculatorRow (Row2).operands) -- [IDataFlowB<double>] --> (CalculatorRow (Row1).result)
            Row1.WireTo(Row1, "operands"); // (CalculatorRow (Row1).operands) -- [IDataFlowB<double>] --> (CalculatorRow (Row1).result)
            labelsConcatenator.WireTo(Row2, "inputs"); // (StringConcat (labelsConcatenator).inputs) -- [IDataFlowB<string>] --> (CalculatorRow (Row2).label)
            Row2.WireTo(Row2, "operands"); // (CalculatorRow (Row2).operands) -- [IDataFlowB<double>] --> (CalculatorRow (Row2).result)
            Row1.WireTo(Row2, "operands"); // (CalculatorRow (Row1).operands) -- [IDataFlowB<double>] --> (CalculatorRow (Row2).result)
            labelsConcatenator.WireTo(labelsConcatenatorConnector, "output"); // (StringConcat (labelsConcatenator).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).input)
            Row2.WireTo(labelsConcatenatorConnector, "labelsCommaSeparated"); // (CalculatorRow (Row2).labelsCommaSeparated) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).outputsB)
            Row1.WireTo(labelsConcatenatorConnector, "labelsCommaSeparated"); // (CalculatorRow (Row1).labelsCommaSeparated) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (labelsConcatenatorConnector).outputsB)
            // END AUTO-GENERATED WIRING FOR Calculator2ARows.xmind
            // Row1.WireInternals();
            // Row2.WireInternals();
            return mainWindow;
        }





        private MainWindow Calculator4Rows()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator4Rows.xmind
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator4Rows.xmind
                                                                                    
            // BEGIN AUTO-GENERATED WIRING FOR Calculator4Rows.xmind
            // END AUTO-GENERATED WIRING FOR Calculator4Rows.xmind
            return mainWindow;
        }


        private MainWindow Calculator2Rows()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator2Rows.xmind
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator2Rows.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator2Rows.xmind
            // END AUTO-GENERATED WIRING FOR Calculator2Rows.xmind
            return mainWindow;
        }


        private MainWindow Calculator1Row()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator1Row.xmind
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator1Row.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator1Row.xmind
            // END AUTO-GENERATED WIRING FOR Calculator1Row.xmind
            return mainWindow;
        }


        private MainWindow CalculatorBasic()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorBasic.xmind
            // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorBasic.xmind

            // BEGIN AUTO-GENERATED WIRING FOR CalculatorBasic.xmind
            // END AUTO-GENERATED WIRING FOR CalculatorBasic.xmind

            return mainWindow;
        }






        private MainWindow Calculator2RowHandWired()
        {
            // To understand this code, you need the wiring diagram of the two row calculator

            // first instantiate instances or abstraction we need to give names to for wiring. The rest can be anonymous.
            StringConcat stringConcat = new StringConcat() { Separator = "," };
            DataFlowConnector<string> stringConcatConnector = new DataFlowConnector<string>(); // Connectors are needed when there is fan-out or fan-in in the diagram
            stringConcat.WireTo(stringConcatConnector, "output");
            Formula[] formulas = { new Formula(), new Formula() }; // instantiate both the formulas up-front because we need to cross wire them


            MainWindow mainWindow = new MainWindow("Calculator")
                .WireTo(new Vertical()
                    .WireTo(WireRow(stringConcat, stringConcatConnector, formulas[0], formulas))
                    .WireTo(WireRow(stringConcat, stringConcatConnector, formulas[1], formulas))
                    );
            return mainWindow;
        }


        private Horizontal WireRow(StringConcat stringConcat, DataFlowConnector<string> stringConcatConnector, Formula formula, Formula[] formulas)
        {
            // To understand this code, you need the wiring diagram of the two row calculator

            // first instantiate objects we need to give names to for wiring.  The rest can be anonymous.
            Text result = new Text(); 

            // Wire up a calculator row
            Horizontal row = new Horizontal()
                .WireTo(new TextBox()
                    .WireTo(new DataFlowConnector<string>()
                        .WireFrom(stringConcat, "inputs")
                    )
                )
                .WireTo(new TextBox()
                    .WireTo(new StringFormat<string,string>("({1})=>{0}")
                        .WireTo(stringConcatConnector, "inputs")
                        .WireTo(formula
                            .WireTo(new DataFlowConnector<double>()
                                .WireFrom(formulas[0], "operands")
                                .WireFrom(formulas[1], "operands")
                                .WireTo(new NumberToString<double?>()
                                    .WireTo(result)
                                )
                            )
                        )
                    )
                )
                .WireTo(result)
                .WireTo(new TextBox());
            return row;
        }

                    





        private MainWindow CalculatorBasicHandWired()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            var Result1 = new Text() { FontSize = 50 };
            var Formula1 = new Formula() { Lambda2 = (x, y) => x + y };            

            mainWindow
                .WireTo(new Vertical()
                    .WireTo(new TextBox() { FontSize = 50 }
                        .WireTo(new StringToNumber<double>()
                            .WireTo(new DataFlowConnector<double>()
                                .WireFrom(Formula1
                                    .WireTo(new StringFormat<double,double>("Ans={0}")
                                        .WireTo(Result1)
                                    )
                                )
                            )
                        )
                    )
                    .WireTo(new TextBox() { FontSize = 50 }
                        .WireTo(new StringToNumber<double>()
                            .WireTo(new DataFlowConnector<double>()
                                .WireFrom(Formula1)
                            )
                        )
                    )
                    .WireTo(Result1)
                );
            return mainWindow;
        }





        private MainWindow HelloWorld()
        {
            MainWindow mainWindow = new MainWindow("MainWindow title");
            mainWindow.WireTo(new Text("Hello world.") { FontSize = 200 });
            return mainWindow;
        }




    }
}
