using System;
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
            // BEGIN AUTO-GENERATED INSTANTIATIONS FOR Calculator1.xmind
            // END AUTO-GENERATED INSTANTIATIONS FOR Calculator1.xmind

            // BEGIN AUTO-GENERATED WIRING FOR Calculator1.xmind
            // END AUTO-GENERATED WIRING FOR Calculator1.xmind

            // BEGIN manual instantiations
            // END manual instantiations

            // BEGIN manual wiring
            mainWindow.WireTo(new Text("Hello world.") { FontSize = 200 });
            // END manual wiring
        }
    }
}
