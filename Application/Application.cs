using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Libraries;
using ProgrammingParadigms;
using DomainAbstractions;

namespace Application
{
    public class Application
    {
        private MainWindow mainWindow = new MainWindow("AUT Workshop 2020");

        private Application Initialize()
        {
            Wiring.PostWiringInitialize();
            return this;
        }

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Initialize().mainWindow.Run();
        }

        private Application()
        {
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator.xmind
            DataFlowConnector<double> dfc1 = new DataFlowConnector<double>() { InstanceName = "dfc1" };
            DataFlowConnector<double> dfc2 = new DataFlowConnector<double>() { InstanceName = "dfc2" };
            DataFlowConnector<double> dfc3 = new DataFlowConnector<double>() { InstanceName = "dfc3" };
            DataFlowConnector<double> dfc4 = new DataFlowConnector<double>() { InstanceName = "dfc4" };
            DataFlowConnector<string> id_5a8fd3747a7f49ad9b89740300b25273 = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6692306d5a004363a0f9b3f32d9a684f = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_6e3f7a805e2c4e3b901024f90be0cbbb = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_df02c9bd22e54c0ca23cf5ab01893bea = new DataFlowConnector<string>() { InstanceName = "Default" };
            DataFlowConnector<string> id_f18e4fe0e68b4fc6ba9e060799048fe6 = new DataFlowConnector<string>() { InstanceName = "Default" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1" };
            Formula Formula2 = new Formula() { InstanceName = "Formula2" };
            Formula Formula3 = new Formula() { InstanceName = "Formula3" };
            Formula Formula4 = new Formula() { InstanceName = "Formula4" };
            Horizontal id_24914ab245484fe1b70af8020ca2e831 = new Horizontal() { InstanceName = "Default" };
            Horizontal id_3cdf1b1c29524751b3b4e9e0ab35e49f = new Horizontal() { InstanceName = "Default" };
            Horizontal id_62cb709a6e8f4af8812307ef103fb600 = new Horizontal() { InstanceName = "Default" };
            Horizontal id_86ce618fc8f44a2ca2484f6136f215dd = new Horizontal() { InstanceName = "Default" };
            Horizontal id_ab03c41f8dca400bb0e82d4a28c34f0b = new Horizontal() { InstanceName = "Default" };
            NumberToString id_28e75bb388914ef192e8c6046e3e6ab0 = new NumberToString() { InstanceName = "Default" };
            NumberToString id_2c52c6f6a829412e9ff552742beec11b = new NumberToString() { InstanceName = "Default" };
            NumberToString id_52f1dffb17db4abf96224faaa5baf4d7 = new NumberToString() { InstanceName = "Default" };
            NumberToString id_a1c68c7d54d74033b59a294accc0320b = new NumberToString() { InstanceName = "Default" };
            StringConcat id_d00e2f96bebf45d3a23bc3b1b0776f22 = new StringConcat() { InstanceName = "Default", Separator="," };
            StringFormat<string> sf1 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf1" };
            StringFormat<string> sf2 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf2" };
            StringFormat<string> sf3 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf3" };
            StringFormat<string> sf4 = new StringFormat<string>("({1})=>{0}" ) { InstanceName = "sf4" };
            Text id_39a7a11c94da4b338a92b2235b8e96d1 = new Text("Units" ) { InstanceName = "Default", FontSize=50 };
            Text id_6be1dbef5dd042ba88554b4482b16079 = new Text("Formula" ) { InstanceName = "Default", FontSize=50 };
            Text id_96b879e17b4346e4b98484224e65d582 = new Text("Label" ) { InstanceName = "Default", FontSize=50 };
            Text id_ccc54bcd38e14c10a5ba59d851191cc4 = new Text("Result" ) { InstanceName = "Default", FontSize=50 };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize=50 };
            Text Result2 = new Text() { InstanceName = "Result2", FontSize=50 };
            Text Result3 = new Text() { InstanceName = "Result3", FontSize=50 };
            Text Result4 = new Text() { InstanceName = "Result4", FontSize=50 };
            TextBox FormulaText1 = new TextBox() { InstanceName = "FormulaText1", FontSize=50 };
            TextBox FormulaText2 = new TextBox() { InstanceName = "FormulaText2", FontSize=50 };
            TextBox FormulaText3 = new TextBox() { InstanceName = "FormulaText3", FontSize=50 };
            TextBox FormulaText4 = new TextBox() { InstanceName = "FormulaText4", FontSize=50 };
            TextBox Label1 = new TextBox() { InstanceName = "Label1", FontSize=50 };
            TextBox Label2 = new TextBox() { InstanceName = "Label2", FontSize=50 };
            TextBox Label3 = new TextBox() { InstanceName = "Label3", FontSize=50 };
            TextBox Label4 = new TextBox() { InstanceName = "Label4", FontSize=50 };
            TextBox Units1 = new TextBox() { InstanceName = "Units1", FontSize=50 };
            TextBox Units2 = new TextBox() { InstanceName = "Units2", FontSize=50 };
            TextBox Units3 = new TextBox() { InstanceName = "Units3", FontSize=50 };
            TextBox Units4 = new TextBox() { InstanceName = "Units4", FontSize=50 };
            Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator.xmind
            mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_24914ab245484fe1b70af8020ca2e831, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_24914ab245484fe1b70af8020ca2e831).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_3cdf1b1c29524751b3b4e9e0ab35e49f, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_3cdf1b1c29524751b3b4e9e0ab35e49f).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_62cb709a6e8f4af8812307ef103fb600, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_62cb709a6e8f4af8812307ef103fb600).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_86ce618fc8f44a2ca2484f6136f215dd, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_ab03c41f8dca400bb0e82d4a28c34f0b, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).child)
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
            Formula3.WireTo(dfc1, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
            Formula4.WireTo(dfc1, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc1).outputsB)
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
            Formula3.WireTo(dfc2, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            Formula4.WireTo(dfc2, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc2).outputsB)
            dfc2.WireTo(id_2c52c6f6a829412e9ff552742beec11b, "outputs"); // (DataFlowConnector<double> (dfc2).outputs) -- [IDataFlow<T>] --> (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).input)
            id_2c52c6f6a829412e9ff552742beec11b.WireTo(Result2, "output"); // (NumberToString (id_2c52c6f6a829412e9ff552742beec11b).output) -- [IDataFlow<string>] --> (Text (Result2).textInput)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Label3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (Label3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(FormulaText3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (FormulaText3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Result3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (Text (Result3).child)
            id_86ce618fc8f44a2ca2484f6136f215dd.WireTo(Units3, "children"); // (Horizontal (id_86ce618fc8f44a2ca2484f6136f215dd).children) -- [List<IUI>] --> (TextBox (Units3).child)
            Label3.WireTo(id_df02c9bd22e54c0ca23cf5ab01893bea, "textOutput"); // (TextBox (Label3).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_df02c9bd22e54c0ca23cf5ab01893bea).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_df02c9bd22e54c0ca23cf5ab01893bea, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_df02c9bd22e54c0ca23cf5ab01893bea).outputsB)
            FormulaText3.WireTo(sf3, "textOutput"); // (TextBox (FormulaText3).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf3).input0)
            sf3.WireTo(Formula3, "output"); // (StringFormat<string> (sf3).output) -- [IDataFlow<string>] --> (Formula (Formula3).formula)
            Formula3.WireTo(dfc3, "result"); // (Formula (Formula3).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc3).input)
            Formula1.WireTo(dfc3, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula2.WireTo(dfc3, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula3.WireTo(dfc3, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            Formula4.WireTo(dfc3, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc3).outputsB)
            dfc3.WireTo(id_28e75bb388914ef192e8c6046e3e6ab0, "outputs"); // (DataFlowConnector<double> (dfc3).outputs) -- [IDataFlow<T>] --> (NumberToString (id_28e75bb388914ef192e8c6046e3e6ab0).input)
            id_28e75bb388914ef192e8c6046e3e6ab0.WireTo(Result3, "output"); // (NumberToString (id_28e75bb388914ef192e8c6046e3e6ab0).output) -- [IDataFlow<string>] --> (Text (Result3).textInput)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Label4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (Label4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(FormulaText4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (FormulaText4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Result4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (Text (Result4).child)
            id_ab03c41f8dca400bb0e82d4a28c34f0b.WireTo(Units4, "children"); // (Horizontal (id_ab03c41f8dca400bb0e82d4a28c34f0b).children) -- [List<IUI>] --> (TextBox (Units4).child)
            Label4.WireTo(id_f18e4fe0e68b4fc6ba9e060799048fe6, "textOutput"); // (TextBox (Label4).textOutput) -- [IDataFlow<string>] --> (DataFlowConnector<string> (id_f18e4fe0e68b4fc6ba9e060799048fe6).input)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_f18e4fe0e68b4fc6ba9e060799048fe6, "inputs"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_f18e4fe0e68b4fc6ba9e060799048fe6).outputsB)
            FormulaText4.WireTo(sf4, "textOutput"); // (TextBox (FormulaText4).textOutput) -- [IDataFlow<string>] --> (StringFormat<string> (sf4).input0)
            sf4.WireTo(Formula4, "output"); // (StringFormat<string> (sf4).output) -- [IDataFlow<string>] --> (Formula (Formula4).formula)
            Formula4.WireTo(dfc4, "result"); // (Formula (Formula4).result) -- [IDataFlow<double>] --> (DataFlowConnector<double> (dfc4).input)
            Formula1.WireTo(dfc4, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula2.WireTo(dfc4, "operands"); // (Formula (Formula2).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula3.WireTo(dfc4, "operands"); // (Formula (Formula3).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            Formula4.WireTo(dfc4, "operands"); // (Formula (Formula4).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (dfc4).outputsB)
            dfc4.WireTo(id_52f1dffb17db4abf96224faaa5baf4d7, "outputs"); // (DataFlowConnector<double> (dfc4).outputs) -- [IDataFlow<T>] --> (NumberToString (id_52f1dffb17db4abf96224faaa5baf4d7).input)
            id_52f1dffb17db4abf96224faaa5baf4d7.WireTo(Result4, "output"); // (NumberToString (id_52f1dffb17db4abf96224faaa5baf4d7).output) -- [IDataFlow<string>] --> (Text (Result4).textInput)
            id_d00e2f96bebf45d3a23bc3b1b0776f22.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "output"); // (StringConcat (id_d00e2f96bebf45d3a23bc3b1b0776f22).output) -- [iDataFlow<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).input)
            sf1.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf1).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf2.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf2).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf3.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf3).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            sf4.WireTo(id_5a8fd3747a7f49ad9b89740300b25273, "inputs"); // (StringFormat<string> (sf4).inputs) -- [IDataFlowB<string>] --> (DataFlowConnector<string> (id_5a8fd3747a7f49ad9b89740300b25273).outputsB)
            // END AUTO-GENERATED WIRING FOR Calculator.xmind

            // BEGIN manual instantiations
            // END manual instantiations

            // BEGIN manual wiring
            // mainWindow.WireTo(new Text("Hello world.") { FontSize = 200 });
            // END manual wiring
        }
    }
}
