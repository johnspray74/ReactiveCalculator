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
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR WiringDiagram.xmind
            /*
            FileReaderWriter id_ff00492ce4024f618f7632899e5f2568 = new FileReaderWriter("d:\\temp\\test.txt" ) { InstanceName = "Default", WatchFile = true };
            TextBox id_1a43d927cb44461e9c9419008b26dcbe = new TextBox() { InstanceName = "Default", Height = 300 };
            TextBox id_2aeaeca5cbd147dc80410e3b98f6c9a3 = new TextBox() { InstanceName = "Default", Height = 300 };
            Vertical id_60081e709ce64a2da8baae542eff69e0 = new Vertical() { InstanceName = "Default" };
            */
            // END AUTO-GENERATED INSTANTIATIONS FOR WiringDiagram.xmind

            // BEGIN AUTO-GENERATED WIRING FOR WiringDiagram.xmind
            /*
            mainWindow.WireTo(id_60081e709ce64a2da8baae542eff69e0, "iuiStructure"); // (@MainWindow (mainWindow).iuiStructure) -- [IUI] --> (Vertical (id_60081e709ce64a2da8baae542eff69e0).NEEDNAME)
            id_60081e709ce64a2da8baae542eff69e0.WireTo(id_1a43d927cb44461e9c9419008b26dcbe, "children"); // (Vertical (id_60081e709ce64a2da8baae542eff69e0).children) -- [List<IUI>] --> (TextBox (id_1a43d927cb44461e9c9419008b26dcbe).NEEDNAME)
            id_60081e709ce64a2da8baae542eff69e0.WireTo(id_2aeaeca5cbd147dc80410e3b98f6c9a3, "children"); // (Vertical (id_60081e709ce64a2da8baae542eff69e0).children) -- [List<IUI>] --> (TextBox (id_2aeaeca5cbd147dc80410e3b98f6c9a3).NEEDNAME)
            id_1a43d927cb44461e9c9419008b26dcbe.WireTo(id_ff00492ce4024f618f7632899e5f2568, "textOutput"); // (TextBox (id_1a43d927cb44461e9c9419008b26dcbe).textOutput) -- [IDataFlow<string>] --> (FileReaderWriter (id_ff00492ce4024f618f7632899e5f2568).contents)
            id_ff00492ce4024f618f7632899e5f2568.WireTo(id_2aeaeca5cbd147dc80410e3b98f6c9a3, "textContentOutput"); // (FileReaderWriter (id_ff00492ce4024f618f7632899e5f2568).textContentOutput) -- [IDataFlow<string>] --> (TextBox (id_2aeaeca5cbd147dc80410e3b98f6c9a3).NEEDNAME)
            */
            // END AUTO-GENERATED WIRING FOR WiringDiagram.xmind

            // BEGIN manual instantiations
            // END manual instantiations

            // BEGIN manual wiring
            mainWindow.WireTo(new Text("Hello world.") { FontSize = 200 });
            // END manual wiring
        }
    }
}
