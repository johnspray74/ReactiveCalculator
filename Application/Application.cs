using System;
using System.Collections.Generic;


using DomainAbstractions;
using RequirementsAbstractions;
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
            // mainWindow = Calculator1Row(); 
            // mainWindow = CalculatorBasic();
            // mainWindow = CalculatorBasicHandWired(); // fails
            // mainWindow = HelloWorld();

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


            // Vertical _rows;
            // StringConcat _labelsConcatenator;



            public CalculatorNRows(out MainWindow mw)
            {
                // BEGIN AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows.xmind
                StringConcat labelsConcatenator = new StringConcat() { InstanceName = "labelsConcatenator", Separator = "," };
                DataFlowConnector<string> labelsConcatenatorConnector = new DataFlowConnector<string>() { InstanceName = "labelsConcatenatorConnector" };
                MainWindow mainWindow = new MainWindow(title: "Reactive calculator") { InstanceName = "mainWindow" };
                Vertical id_774b91d547a34269a9b14400438d24e7 = new Vertical() { };
                Text id_8dedb2a5818245f490eb46302a51cbb8 = new Text(text: "Reactive Calculator") { FontSize = 25 };
                TextBox id_7310204398eb490b90fe27c885886e6c = new TextBox() { Text = "Title your calculator here", FontSize = 25 };
                Horizontal id_9f0934ecea404b979ae4bf98cfe3fd31 = new Horizontal() { Ratios = new int[] { 1, 2, 2, 1, 3 }, MinWidths = new int[] { 50, 200, 520 } };
                Vertical rows = new Vertical() { InstanceName = "rows" };
                Horizontal id_56f71a8bf9b543138031d280c9c6cb61 = new Horizontal() { Ratios = new int[] { 1, 8 }, MinWidths = new int[] { 50 } };
                Text id_ab6d48edd2f34e1593d761dbd7420e23 = new Text(text: "Label") { };
                Text id_e9172bcd0ef84fc1a581ba80e027c55b = new Text(text: "Formula") { };
                Text id_a09b5fe4a2d44d3584295ee76e80f5cb = new Text(text: "Result") { };
                Text id_cf8640f96ffc4acc82a699ad1740fbf1 = new Text(text: "Units") { };
                Text id_073415f85b664d2bb36f3f22c24624fd = new Text(text: "Description") { };
                Button id_499e9b1d64e34ff19a7b509c81bd0de4 = new Button(title: "Add row") { };
                Space id_edff86a66153472a924599db53e0d9fb = new Space() { };
                Multiple MultipleRow = new Multiple(N: 4) { InstanceName = "MultipleRow", WiringMethod = (newInstance) => { rows.WireTo(newInstance); labelsConcatenator.WireTo(newInstance, "inputs"); newInstance.WireTo(labelsConcatenatorConnector, "labelsCommaSeparated"); testCalculatorRows.Add((ITestCalculatorRow)newInstance); }, CrossWiringMethod = (instance1, instance2) => { instance2.WireFrom(instance1, "operands"); }, PostWiringInitializeMethod = delegate (object instance) { rows.AddRows(); ((CalculatorRow)instance).Initialize(); } };
                CalculatorRowFactory id_eedae0deb9b144e8ac452ebb67c4c867 = new CalculatorRowFactory() { };
                // END AUTO-GENERATED INSTANTIATIONS FOR CalculatorNRows.xmind

                // BEGIN AUTO-GENERATED WIRING FOR CalculatorNRows.xmind
                mainWindow.WireTo(id_774b91d547a34269a9b14400438d24e7, "iuiStructure");
                id_774b91d547a34269a9b14400438d24e7.WireTo(id_8dedb2a5818245f490eb46302a51cbb8, "children");
                id_774b91d547a34269a9b14400438d24e7.WireTo(id_7310204398eb490b90fe27c885886e6c, "children");
                id_774b91d547a34269a9b14400438d24e7.WireTo(id_9f0934ecea404b979ae4bf98cfe3fd31, "children");
                id_774b91d547a34269a9b14400438d24e7.WireTo(rows, "children");
                id_774b91d547a34269a9b14400438d24e7.WireTo(id_56f71a8bf9b543138031d280c9c6cb61, "children");
                id_9f0934ecea404b979ae4bf98cfe3fd31.WireTo(id_ab6d48edd2f34e1593d761dbd7420e23, "children");
                id_9f0934ecea404b979ae4bf98cfe3fd31.WireTo(id_e9172bcd0ef84fc1a581ba80e027c55b, "children");
                id_9f0934ecea404b979ae4bf98cfe3fd31.WireTo(id_a09b5fe4a2d44d3584295ee76e80f5cb, "children");
                id_9f0934ecea404b979ae4bf98cfe3fd31.WireTo(id_cf8640f96ffc4acc82a699ad1740fbf1, "children");
                id_9f0934ecea404b979ae4bf98cfe3fd31.WireTo(id_073415f85b664d2bb36f3f22c24624fd, "children");
                id_56f71a8bf9b543138031d280c9c6cb61.WireTo(id_499e9b1d64e34ff19a7b509c81bd0de4, "children");
                id_56f71a8bf9b543138031d280c9c6cb61.WireTo(id_edff86a66153472a924599db53e0d9fb, "children");
                id_499e9b1d64e34ff19a7b509c81bd0de4.WireTo(MultipleRow, "eventButtonClicked");
                MultipleRow.WireTo(id_eedae0deb9b144e8ac452ebb67c4c867, "factory");
                labelsConcatenator.WireTo(labelsConcatenatorConnector, "output");
                // END AUTO-GENERATED WIRING FOR CalculatorNRows.xmind

                // These are used to solve compiler error "Cannot use local variable 'rows' before it is declared" in the lambda functions in the wiring code above if they reference 'rows' and 'labelsConcetenator' instead;
                //  _rows = rows;  
                // _labelsConcatenator = labelsConcatenator;  // 

                // This tell MultipleRow object to go ahead an create calculator rows (each of which will use the lambdas above to wire into the rest of the application)
                MultipleRow.Generate();


                // these are used for testing the application to set the title and press the button
                title = id_7310204398eb490b90fe27c885886e6c;
                // title = id_b84a8eee3a554afaad9fa90ac6b594f9;
                addRowButton = id_499e9b1d64e34ff19a7b509c81bd0de4;
                // addRowButton = id_803db86064414b379608f65bc07098bc;

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
                testCalculatorRows[6].EnterFormula("0.5*mass*(vel*vel)");
                testCalculatorRows[6].EnterUnit("J");
                testCalculatorRows[6].EnterDescription("kinetic energy of satellite");
                assertStringEq(testCalculatorRows[6].ReadResult().Substring(0, 10), "3036875190");

                addRowButton.Click();
                testCalculatorRows[7].EnterLabel("g");
                testCalculatorRows[7].EnterFormula("G*me/(re*re)");
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
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<double> dfc2 = new DataFlowConnector<double>() { InstanceName = "dfc2" };
            DataFlowConnector<double> dfc3 = new DataFlowConnector<double>() { InstanceName = "dfc3" };
            DataFlowConnector<double> dfc4 = new DataFlowConnector<double>() { InstanceName = "dfc4" };
            DataFlowConnector<double> dfc5 = new DataFlowConnector<double>() { InstanceName = "dfc5" };
            DataFlowConnector<double> dfc6 = new DataFlowConnector<double>() { InstanceName = "dfc6" };
            DataFlowConnector<string> id_1e8454038c6349a8a94ab5adff6f3fe1 = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_5a8fd3747a7f49ad9b89740300b25273 = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6692306d5a004363a0f9b3f32d9a684f = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6e3f7a805e2c4e3b901024f90be0cbbb = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_89d1674a1437470d8de31a4a8b194171 = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_df02c9bd22e54c0ca23cf5ab01893bea = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_f18e4fe0e68b4fc6ba9e060799048fe6 = new DataFlowConnector<string>() { InstanceName = "Default" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Formula Formula2 = new Formula() { InstanceName = "Formula2" };
            Formula Formula3 = new Formula() { InstanceName = "Formula3" };
            Formula Formula4 = new Formula() { InstanceName = "Formula4" };
            Formula Formula5 = new Formula() { InstanceName = "Formula5" };
            Formula Formula6 = new Formula() { InstanceName = "Formula6" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_3cdf1b1c29524751b3b4e9e0ab35e49f = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_62cb709a6e8f4af8812307ef103fb600 = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_86ce618fc8f44a2ca2484f6136f215dd = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_ab03c41f8dca400bb0e82d4a28c34f0b = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_db098085b69a4606ad521ce181c7792b = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            Horizontal id_f87c619494c14ad1bd6f67715c741cac = new Horizontal() { InstanceName = "Default", Ratios = new int[] { 1,2,2,1,3 }, MinWidths = new int[] { 50,200,520 } };
            NumberToString id_28e75bb388914ef192e8c6046e3e6ab0 = new NumberToString() { InstanceName = "Default" };
            NumberToString id_2c52c6f6a829412e9ff552742beec11b = new NumberToString() { InstanceName = "Default" };
            NumberToString id_4a3aabde62304c5fa5d34d610d7e3239 = new NumberToString() { InstanceName = "Default" };
            NumberToString id_52f1dffb17db4abf96224faaa5baf4d7 = new NumberToString() { InstanceName = "Default" };
            NumberToString id_99af5ce20c07486f8a5d07d6d11f5a4a = new NumberToString() { InstanceName = "Default" };
            NumberToString id_a1c68c7d54d74033b59a294accc0320b = new NumberToString() { InstanceName = "Default" };
            StringConcat id_d00e2f96bebf45d3a23bc3b1b0776f22 = new StringConcat() { InstanceName = "Default", Separator="," };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf1" };
            StringFormat<string> sf2 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf2" };
            StringFormat<string> sf3 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf3" };
            StringFormat<string> sf4 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf4" };
            StringFormat<string> sf5 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf5" };
            StringFormat<string> sf6 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf6" };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=50 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=50 };
            Text id_93a237ff714b48748a4ba10ede42d2dc = new Text("Description" ) { InstanceName = "Default", FontSize=50 };
            Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=50 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=50 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize=50 };
            Text Result2 = new Text() { InstanceName = "Result2", FontSize=50 };
            Text Result3 = new Text() { InstanceName = "Result3", FontSize=50 };
            Text Result4 = new Text() { InstanceName = "Result4", FontSize=50 };
            Text Result5 = new Text() { InstanceName = "Result5", FontSize=50 };
            Text Result6 = new Text() { InstanceName = "Result6", FontSize=50 };
            TextBox Description1 = new TextBox() { InstanceName = "Description1", FontSize=50 };
            TextBox Description2 = new TextBox() { InstanceName = "Description2", FontSize=50 };
            TextBox Description3 = new TextBox() { InstanceName = "Description3", FontSize=50 };
            TextBox Description4 = new TextBox() { InstanceName = "Description4", FontSize=50 };
            TextBox Description5 = new TextBox() { InstanceName = "Description5", FontSize=50 };
            TextBox Description6 = new TextBox() { InstanceName = "Description6", FontSize=50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize=50 };
            TextBox FormulaText2 = new TextBox() { InstanceName = "FormulaText2", FontSize=50 };
            TextBox FormulaText3 = new TextBox() { InstanceName = "FormulaText3", FontSize=50 };
            TextBox FormulaText4 = new TextBox() { InstanceName = "FormulaText4", FontSize=50 };
            TextBox FormulaText5 = new TextBox() { InstanceName = "FormulaText5", FontSize=50 };
            TextBox FormulaText6 = new TextBox() { InstanceName = "FormulaText6", FontSize=50 };
            TextBox Label1 = new TextBox() { InstanceName = "Label1", FontSize=50 };
            TextBox Label2 = new TextBox() { InstanceName = "Label2", FontSize=50 };
            TextBox Label3 = new TextBox() { InstanceName = "Label3", FontSize=50 };
            TextBox Label4 = new TextBox() { InstanceName = "Label4", FontSize=50 };
            TextBox Label5 = new TextBox() { InstanceName = "Label5", FontSize=50 };
            TextBox Label6 = new TextBox() { InstanceName = "Label6", FontSize=50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize=50 };
            TextBox Units2 = new TextBox() { InstanceName = "Units2", FontSize=50 };
            TextBox Units3 = new TextBox() { InstanceName = "Units3", FontSize=50 };
            TextBox Units4 = new TextBox() { InstanceName = "Units4", FontSize=50 };
            TextBox Units5 = new TextBox() { InstanceName = "Units5", FontSize=50 };
            TextBox Units6 = new TextBox() { InstanceName = "Units6", FontSize=50 };
            Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator4Rows.xmind
                                                                                    
            // BEGIN AUTO-GENERATED WIRING FOR Calculator4Rows.xmind
            mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_3cdf1b1c29524751b3b4e9e0ab35e49f, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_62cb709a6e8f4af8812307ef103fb600, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_86ce618fc8f44a2ca2484f6136f215dd, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_ab03c41f8dca400bb0e82d4a28c34f0b, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_db098085b69a4606ad521ce181c7792b, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_db098085b69a4606ad521ce181c7792b).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_f87c619494c14ad1bd6f67715c741cac, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_93a237ff714b48748a4ba10ede42d2dc, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_93a237ff714b48748a4ba10ede42d2dc).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Label1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Label1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(FormulaText1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (FormulaText1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Result1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (Text (Result1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Units1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Units1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Description1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Description1).child)
            Label1.WireTo(id_6e3f7a805e2c4e3b901024f90be0cbbb, "textOutput"); // (TextBox (Label1).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_6e3f7a805e2c4e3b901024f90be0cbbb).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_6e3f7a805e2c4e3b901024f90be0cbbb, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_6e3f7a805e2c4e3b901024f90be0cbbb).outputsB)
            FormulaText1.WireTo(sf1, "textOutput"); // (TextBox (FormulaText1).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf1).input0)
            sf1.WireTo(Formula1, "output"); // (StringFormat<string> (sf1).output) -- [IDataFlow<string>] --> (Formula (Formula1).formula)
            Formula1.WireTo(dfc1, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            Formula1.WireTo(dfc1, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula2.WireTo(dfc1, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula4.WireTo(dfc1, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula5.WireTo(dfc1, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula6.WireTo(dfc1, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula3.WireTo(dfc1, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            dfc1.WireTo(id_a1c68c7d54d74033b59a294accc0320b, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).input)
            id_a1c68c7d54d74033b59a294accc0320b.WireTo(Result1, "output"); // (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Label2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (Label2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(FormulaText2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (FormulaText2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Result2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (Text (Result2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Units2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (Units2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Description2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (Description2).child)
            Label2.WireTo(id_6692306d5a004363a0f9b3f32d9a684f, "textOutput"); // (TextBox (Label2).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_6692306d5a004363a0f9b3f32d9a684f).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_6692306d5a004363a0f9b3f32d9a684f, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_6692306d5a004363a0f9b3f32d9a684f).outputsB)
            FormulaText2.WireTo(sf2, "textOutput"); // (TextBox (FormulaText2).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf2).input0)
            sf2.WireTo(Formula2, "output"); // (StringFormat<string> (sf2).output) -- [IDataFlow<string>] --> (Formula (Formula2).formula)
            Formula2.WireTo(dfc2, "result"); // (Formula (Formula2).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc2).input)
            Formula1.WireTo(dfc2, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula2.WireTo(dfc2, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula3.WireTo(dfc2, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula4.WireTo(dfc2, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula5.WireTo(dfc2, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula6.WireTo(dfc2, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            dfc2.WireTo(id_2c52c6f6a829412e9ff552742beec11b, "outputs"); // (DataFlowConnector<double> (dfc2).outputs) -- [IDataFlow<T>] --> (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).input)
            id_2c52c6f6a829412e9ff552742beec11b.WireTo(Result2, "output"); // (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).output) -- [IDataFlow<string>] --> (Text (Result2).textInput)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Label3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (Label3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(FormulaText3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (FormulaText3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Result3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (Text (Result3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Units3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (Units3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Description3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (Description3).child)
            Label3.WireTo(id_df02c9bd22e54c0ca23cf5ab01893bea, "textOutput"); // (TextBox (Label3).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_df02c9bd22e54c0ca23cf5ab01893bea).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_df02c9bd22e54c0ca23cf5ab01893bea, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_df02c9bd22e54c0ca23cf5ab01893bea).outputsB)
            FormulaText3.WireTo(sf3, "textOutput"); // (TextBox (FormulaText3).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf3).input0)
            sf3.WireTo(Formula3, "output"); // (StringFormat<string> (sf3).output) -- [IDataFlow<string>] --> (Formula (Formula3).formula)
            Formula3.WireTo(dfc3, "result"); // (Formula (Formula3).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc3).input)
            Formula1.WireTo(dfc3, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula2.WireTo(dfc3, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula3.WireTo(dfc3, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula4.WireTo(dfc3, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula5.WireTo(dfc3, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula6.WireTo(dfc3, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            dfc3.WireTo(id_28e75bb388914ef192e8c6046e3e6ab0, "outputs"); // (DataFlowConnector<double> (dfc3).outputs) -- [IDataFlow<T>] --> (NumberToString (id_28e75bb388914ef192e8c6046e3e6ab0).input)
            id_28e75bb388914ef192e8c6046e3e6ab0.WireTo(Result3, "output"); // (NumberToString (id_28e75bb388914ef192e8c6046e3e6ab0).output) -- [IDataFlow<string>] --> (Text (Result3).textInput)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Label4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (Label4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(FormulaText4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (FormulaText4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Result4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (Text (Result4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Units4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (Units4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Description4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (Description4).child)
            Label4.WireTo(id_f18e4fe0e68b4fc6ba9e060799048fe6, "textOutput"); // (TextBox (Label4).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_f18e4fe0e68b4fc6ba9e060799048fe6).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_f18e4fe0e68b4fc6ba9e060799048fe6, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_f18e4fe0e68b4fc6ba9e060799048fe6).outputsB)
            FormulaText4.WireTo(sf4, "textOutput"); // (TextBox (FormulaText4).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf4).input0)
            sf4.WireTo(Formula4, "output"); // (StringFormat<string> (sf4).output) -- [IDataFlow<string>] --> (Formula (Formula4).formula)
            Formula4.WireTo(dfc4, "result"); // (Formula (Formula4).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc4).input)
            Formula1.WireTo(dfc4, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula2.WireTo(dfc4, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula3.WireTo(dfc4, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula4.WireTo(dfc4, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula5.WireTo(dfc4, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula6.WireTo(dfc4, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            dfc4.WireTo(id_52f1dffb17db4abf96224faaa5baf4d7, "outputs"); // (DataFlowConnector<double> (dfc4).outputs) -- [IDataFlow<T>] --> (NumberToString (id_52f1dffb17db4abf96224faaa5baf4d7).input)
            id_52f1dffb17db4abf96224faaa5baf4d7.WireTo(Result4, "output"); // (NumberToString (id_52f1dffb17db4abf96224faaa5baf4d7).output) -- [IDataFlow<string>] --> (Text (Result4).textInput)
            id_db098085b69a4606ad521ce181c7792b.WireTo(Label5, "children"); // (Horizontal (id_db098085b69a4606ad521ce181c7792b).children) -- [List<IUI>] --> (TextBox (Label5).child)
            id_db098085b69a4606ad521ce181c7792b.WireTo(FormulaText5, "children"); // (Horizontal (id_db098085b69a4606ad521ce181c7792b).children) -- [List<IUI>] --> (TextBox (FormulaText5).child)
            id_db098085b69a4606ad521ce181c7792b.WireTo(Result5, "children"); // (Horizontal (id_db098085b69a4606ad521ce181c7792b).children) -- [List<IUI>] --> (Text (Result5).child)
            id_db098085b69a4606ad521ce181c7792b.WireTo(Units5, "children"); // (Horizontal (id_db098085b69a4606ad521ce181c7792b).children) -- [List<IUI>] --> (TextBox (Units5).child)
            id_db098085b69a4606ad521ce181c7792b.WireTo(Description5, "children"); // (Horizontal (id_db098085b69a4606ad521ce181c7792b).children) -- [List<IUI>] --> (TextBox (Description5).child)
            Label5.WireTo(id_89d1674a1437470d8de31a4a8b194171, "textOutput"); // (TextBox (Label5).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_89d1674a1437470d8de31a4a8b194171).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_89d1674a1437470d8de31a4a8b194171, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_89d1674a1437470d8de31a4a8b194171).outputsB)
            FormulaText5.WireTo(sf5, "textOutput"); // (TextBox (FormulaText5).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf5).input0)
            sf5.WireTo(Formula5, "output"); // (StringFormat<string> (sf5).output) -- [IDataFlow<string>] --> (Formula (Formula5).formula)
            Formula5.WireTo(dfc5, "result"); // (Formula (Formula5).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc5).input)
            Formula5.WireTo(dfc5, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            Formula1.WireTo(dfc5, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            Formula2.WireTo(dfc5, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            Formula3.WireTo(dfc5, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            Formula4.WireTo(dfc5, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            Formula6.WireTo(dfc5, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc5).outputsB)
            dfc5.WireTo(id_99af5ce20c07486f8a5d07d6d11f5a4a, "outputs"); // (DataFlowConnector<double> (dfc5).outputs) -- [IDataFlow<T>] --> (NumberToString (id_99af5ce20c07486f8a5d07d6d11f5a4a).input)
            id_99af5ce20c07486f8a5d07d6d11f5a4a.WireTo(Result5, "output"); // (NumberToString (id_99af5ce20c07486f8a5d07d6d11f5a4a).output) -- [IDataFlow<string>] --> (Text (Result5).textInput)
            id_f87c619494c14ad1bd6f67715c741cac.WireTo(Label6, "children"); // (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).children) -- [List<IUI>] --> (TextBox (Label6).child)
            id_f87c619494c14ad1bd6f67715c741cac.WireTo(FormulaText6, "children"); // (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).children) -- [List<IUI>] --> (TextBox (FormulaText6).child)
            id_f87c619494c14ad1bd6f67715c741cac.WireTo(Result6, "children"); // (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).children) -- [List<IUI>] --> (Text (Result6).child)
            id_f87c619494c14ad1bd6f67715c741cac.WireTo(Units6, "children"); // (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).children) -- [List<IUI>] --> (TextBox (Units6).child)
            id_f87c619494c14ad1bd6f67715c741cac.WireTo(Description6, "children"); // (Horizontal (id_f87c619494c14ad1bd6f67715c741cac).children) -- [List<IUI>] --> (TextBox (Description6).child)
            Label6.WireTo(id_1e8454038c6349a8a94ab5adff6f3fe1, "textOutput"); // (TextBox (Label6).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_1e8454038c6349a8a94ab5adff6f3fe1).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_1e8454038c6349a8a94ab5adff6f3fe1, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_1e8454038c6349a8a94ab5adff6f3fe1).outputsB)
            FormulaText6.WireTo(sf6, "textOutput"); // (TextBox (FormulaText6).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf6).input0)
            sf6.WireTo(Formula6, "output"); // (StringFormat<string> (sf6).output) -- [IDataFlow<string>] --> (Formula (Formula6).formula)
            Formula6.WireTo(dfc6, "result"); // (Formula (Formula6).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc6).input)
            Formula6.WireTo(dfc6, "operands"); // (Formula (Formula6).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            Formula1.WireTo(dfc6, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            Formula2.WireTo(dfc6, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            Formula3.WireTo(dfc6, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            Formula4.WireTo(dfc6, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            Formula5.WireTo(dfc6, "operands"); // (Formula (Formula5).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc6).outputsB)
            dfc6.WireTo(id_4a3aabde62304c5fa5d34d610d7e3239, "outputs"); // (DataFlowConnector<double> (dfc6).outputs) -- [IDataFlow<T>] --> (NumberToString (id_4a3aabde62304c5fa5d34d610d7e3239).input)
            id_4a3aabde62304c5fa5d34d610d7e3239.WireTo(Result6, "output"); // (NumberToString (id_4a3aabde62304c5fa5d34d610d7e3239).output) -- [IDataFlow<string>] --> (Text (Result6).textInput)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "output"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).input)
            sf1.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf1).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf2.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf2).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf3.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf3).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf4.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf4).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf5.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf5).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf6.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf6).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            // END AUTO-GENERATED WIRING FOR Calculator4Rows.xmind
            return mainWindow;
        }


        private MainWindow Calculator2Rows()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator2Rows.xmind
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<double> dfc2 = new DataFlowConnector<double>() { InstanceName = "dfc2" };
            DataFlowConnector<string> id_5a8fd3747a7f49ad9b89740300b25273 = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6692306d5a004363a0f9b3f32d9a684f = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6e3f7a805e2c4e3b901024f90be0cbbb = new DataFlowConnector<string>() { InstanceName = "Default" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Formula Formula2 = new Formula() { InstanceName = "Formula2" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default" };
            Horizontal id_3cdf1b1c29524751b3b4e9e0ab35e49f = new Horizontal() { InstanceName = "Default" };
            Horizontal id_62cb709a6e8f4af8812307ef103fb600 = new Horizontal() { InstanceName = "Default" };
            NumberToString id_2c52c6f6a829412e9ff552742beec11b = new NumberToString() { InstanceName = "Default" };
            NumberToString id_a1c68c7d54d74033b59a294accc0320b = new NumberToString() { InstanceName = "Default" };
            StringConcat id_d00e2f96bebf45d3a23bc3b1b0776f22 = new StringConcat() { InstanceName = "Default", Separator="," };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf1" };
            StringFormat<string> sf2 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf2" };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=50 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=50 };
            Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=50 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=50 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize=50 };
            Text Result2 = new Text() { InstanceName = "Result2", FontSize=50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize=50 };
            TextBox FormulaText2 = new TextBox() { InstanceName = "FormulaText2", FontSize=50 };
            TextBox Label1 = new TextBox() { InstanceName = "Label1", FontSize=50 };
            TextBox Label2 = new TextBox() { InstanceName = "Label2", FontSize=50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize=50 };
            TextBox Units2 = new TextBox() { InstanceName = "Units2", FontSize=50 };
            Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator2Rows.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator2Rows.xmind
            mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_3cdf1b1c29524751b3b4e9e0ab35e49f, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_62cb709a6e8f4af8812307ef103fb600, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_96b879e17b4346e4b98484224e65d582, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_96b879e17b4346e4b98484224e65d582).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Label1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Label1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(FormulaText1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (FormulaText1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Result1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (Text (Result1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Units1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Units1).child)
            Label1.WireTo(id_6e3f7a805e2c4e3b901024f90be0cbbb, "textOutput"); // (TextBox (Label1).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_6e3f7a805e2c4e3b901024f90be0cbbb).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_6e3f7a805e2c4e3b901024f90be0cbbb, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_6e3f7a805e2c4e3b901024f90be0cbbb).outputsB)
            FormulaText1.WireTo(sf1, "textOutput"); // (TextBox (FormulaText1).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf1).input0)
            sf1.WireTo(Formula1, "output"); // (StringFormat<string> (sf1).output) -- [IDataFlow<string>] --> (Formula (Formula1).formula)
            Formula1.WireTo(dfc1, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            Formula1.WireTo(dfc1, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula2.WireTo(dfc1, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            dfc1.WireTo(id_a1c68c7d54d74033b59a294accc0320b, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).input)
            id_a1c68c7d54d74033b59a294accc0320b.WireTo(Result1, "output"); // (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Label2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (Label2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(FormulaText2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (FormulaText2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Result2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (Text (Result2).child)
            id_62cb709a6e8f4af8812307ef103fb600.WireTo(Units2, "children"); // (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).children) -- [List<IUI>] --> (TextBox (Units2).child)
            Label2.WireTo(id_6692306d5a004363a0f9b3f32d9a684f, "textOutput"); // (TextBox (Label2).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_6692306d5a004363a0f9b3f32d9a684f).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_6692306d5a004363a0f9b3f32d9a684f, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_6692306d5a004363a0f9b3f32d9a684f).outputsB)
            FormulaText2.WireTo(sf2, "textOutput"); // (TextBox (FormulaText2).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf2).input0)
            sf2.WireTo(Formula2, "output"); // (StringFormat<string> (sf2).output) -- [IDataFlow<string>] --> (Formula (Formula2).formula)
            Formula2.WireTo(dfc2, "result"); // (Formula (Formula2).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc2).input)
            Formula1.WireTo(dfc2, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula2.WireTo(dfc2, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            dfc2.WireTo(id_2c52c6f6a829412e9ff552742beec11b, "outputs"); // (DataFlowConnector<double> (dfc2).outputs) -- [IDataFlow<T>] --> (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).input)
            id_2c52c6f6a829412e9ff552742beec11b.WireTo(Result2, "output"); // (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).output) -- [IDataFlow<string>] --> (Text (Result2).textInput)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "output"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).input)
            sf1.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf1).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf2.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf2).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            // END AUTO-GENERATED WIRING FOR Calculator2Rows.xmind
            return mainWindow;
        }


        private MainWindow Calculator1Row()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator1Row.xmind
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default" };
            Horizontal id_3cdf1b1c29524751b3b4e9e0ab35e49f = new Horizontal() { InstanceName = "Default" };
            NumberToString id_a1c68c7d54d74033b59a294accc0320b = new NumberToString() { InstanceName = "Default" };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=50 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=50 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=50 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize=50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize=50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize=50 };
            Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator1Row.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator1Row.xmind
            mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_3cdf1b1c29524751b3b4e9e0ab35e49f, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_6be1dbef5dd042ba88554b4482b16079, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_6be1dbef5dd042ba88554b4482b16079).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_ccc54bcd38e14c10a5ba59d851191cc4, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_ccc54bcd38e14c10a5ba59d851191cc4).child)
            id_24914ab245484fe1b70af8020ca2e831.WireTo(id_39a7a11c94da4b338a92b2235b8e96d1, "children"); // (Horizontal (id_24914ab245484fe1b70af8020ca2e831).children) -- [List<IUI>] --> (Text (id_39a7a11c94da4b338a92b2235b8e96d1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(FormulaText1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (FormulaText1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Result1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (Text (Result1).child)
            id_3cdf1b1c29524751b3b4e9e0ab35e49f.WireTo(Units1, "children"); // (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).children) -- [List<IUI>] --> (TextBox (Units1).child)
            FormulaText1.WireTo(Formula1, "textOutput"); // (TextBox (FormulaText1).textOutput) -- [IDataFlow<string>] --> (Formula (Formula1).formula)
            Formula1.WireTo(dfc1, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc1).input)
            dfc1.WireTo(id_a1c68c7d54d74033b59a294accc0320b, "outputs"); // (DataFlowConnector<double> (dfc1).outputs) -- [IDataFlow<T>] --> (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).input)
            id_a1c68c7d54d74033b59a294accc0320b.WireTo(Result1, "output"); // (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
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



        private MainWindow CalculatorBasicHandWired()
        {
            MainWindow mainWindow = new MainWindow("Calculator");
            var Result1 = new Text() { FontSize = 50 };
            var Formula1 = new Formula() { Lambda = (x, y, P2, P3, P4, P5) => x + y };

            mainWindow
                .WireTo(new Vertical()
                    .WireTo(new TextBox() { FontSize = 50 }
                        .WireTo(new StringToNumber()
                            .WireTo(new DataFlowConnector<double>()
                                .WireFrom(Formula1
                                    .WireTo(new StringFormat<double>("Ans={0}")
                                        .WireTo(Result1)
                                    )
                                )
                            )
                        )
                    )
                    .WireTo(new TextBox() { FontSize = 50 }
                        .WireTo(new StringToNumber()
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
