using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Libraries;
using ProgrammingParadigms;
using DomainAbstractions;
using System.Linq.Expressions;

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
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Exercise2.xmind
            /*
            DataFlowConnector<double> id_b59a92f685d04efc8d502ad61ad6060d = new DataFlowConnector<double>() { InstanceName = "Default" };
            DataFlowConnector<double> id_b920086539b24a5dbef8d26461645c17 = new DataFlowConnector<double>() { InstanceName = "Default" };
            Formula Formula1 = new Formula() { InstanceName = "Formula1", Lambda = (x,y,P2,P3,P4,P5)=>x+y };
            NumberToString id_a1c68c7d54d74033b59a294accc0320b = new NumberToString() { InstanceName = "Default" };
            StringToNumber id_1782c2ec97954179b2cfe39997b5a614 = new StringToNumber() { InstanceName = "Default" };
            StringToNumber id_8723710adf1642af8e7b00c3caea5e66 = new StringToNumber() { InstanceName = "Default" };
            Text id_fc0b8f38b3c14f799f605cd54214b503 = new Text("Debug output" ) { InstanceName = "Default", FontSize=50 };
            Text Result1 = new Text() { InstanceName = "Result1", FontSize=50 };
            TextBox Operand1 = new TextBox() { InstanceName = "Operand1", FontSize=50 };
            TextBox Operand2 = new TextBox() { InstanceName = "Operand2", FontSize=50 };
            Vertical id_b02d2caea938499b997b9bfcb80fb0e9 = new Vertical() { InstanceName = "Default" };
            */
            // END AUTO-GENERATED INSTANTIATIONS FOR Exercise2.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Exercise2.xmind
            /*
            mainWindow.WireTo(id_b02d2caea938499b997b9bfcb80fb0e9, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(Operand1, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (TextBox (Operand1).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(Operand2, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (TextBox (Operand2).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(Result1, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (Result1).child)
            id_b02d2caea938499b997b9bfcb80fb0e9.WireTo(id_fc0b8f38b3c14f799f605cd54214b503, "children"); // (Vertical (id_b02d2caea938499b997b9bfcb80fb0e9).children) -- [List<IUI>] --> (Text (id_fc0b8f38b3c14f799f605cd54214b503).child)
            Operand1.WireTo(id_1782c2ec97954179b2cfe39997b5a614, "textOutput"); // (TextBox (Operand1).textOutput) -- [IDataFlow<string>] --> (StringToNumber (id_1782c2ec97954179b2cfe39997b5a614).input)
            id_1782c2ec97954179b2cfe39997b5a614.WireTo(id_b920086539b24a5dbef8d26461645c17, "output"); // (StringToNumber (id_1782c2ec97954179b2cfe39997b5a614).output) -- [IDataFlow<double>] --> (DataFlowConnector<double> (id_b920086539b24a5dbef8d26461645c17).input)
            Formula1.WireTo(id_b920086539b24a5dbef8d26461645c17, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (id_b920086539b24a5dbef8d26461645c17).outputsB)
            Formula1.WireTo(id_a1c68c7d54d74033b59a294accc0320b, "result"); // (Formula (Formula1).result) -- [IDataFlow<double>] --> (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).input)
            id_a1c68c7d54d74033b59a294accc0320b.WireTo(Result1, "output"); // (NumberToString (id_a1c68c7d54d74033b59a294accc0320b).output) -- [IDataFlow<string>] --> (Text (Result1).textInput)
            Operand2.WireTo(id_8723710adf1642af8e7b00c3caea5e66, "textOutput"); // (TextBox (Operand2).textOutput) -- [IDataFlow<string>] --> (StringToNumber (id_8723710adf1642af8e7b00c3caea5e66).input)
            id_8723710adf1642af8e7b00c3caea5e66.WireTo(id_b59a92f685d04efc8d502ad61ad6060d, "output"); // (StringToNumber (id_8723710adf1642af8e7b00c3caea5e66).output) -- [IDataFlow<double>] --> (DataFlowConnector<double> (id_b59a92f685d04efc8d502ad61ad6060d).input)
            Formula1.WireTo(id_b59a92f685d04efc8d502ad61ad6060d, "operands"); // (Formula (Formula1).operands) -- [IDataFlowB<double>] --> (DataFlowConnector<double> (id_b59a92f685d04efc8d502ad61ad6060d).outputsB)
            */
            // END AUTO-GENERATED WIRING FOR Exercise2.xmind

            // BEGIN manual instantiations
            var Result1 = new Text() { FontSize = 50 };
            var Formula1 = new Formula() { Lambda = (x,y,P2,P3,P4,P5)=>x+y};
            // END manual instantiations

            // BEGIN manual wiring
            mainWindow.WireTo(new Vertical()
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

            // mainWindow.WireTo(new Text("Hello world.") { FontSize = 200 });
            // END manual wiring
        }
    }
}
