using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Libraries;
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// This is the main window of an application.
    /// The output IUI, child, wires to any UI element that is to be contained inside the window (usually would be Vertical or Horizontal). 
    /// The output IEvent fires once once the app starts running - it may be used optionally to start any non-UI driven activities.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IEvent shutdown input: closes the window (exits the application)
    /// 2. IDataFlow<bool> visible input: to enable(true) or disable(false, grey out) the UI
    /// 3. IUI children output: multiple UI elements that implement IUI to be contained within the MainWindow
    /// 4. IEvent appStart output: IEvent that fires once window has been loaded and the application UI is already running
    /// <summary>

    public class MainWindow : IEvent, IDataFlow<bool>
    {
        // Properties -----------------------------------------------------------------
        public string InstanceName { get; set; } = "Default";




        // Ports -----------------------------------------------------------------
        private IUI child;
        private IEvent appStart;




        // Private fields -----------------------------------------------------------------
        private Window window;




        /// <summary>
        /// Generates the main UI window of the application and emits a signal that the Application starts running.
        /// </summary>
        /// <param name="title">title of the window</param>
        public MainWindow(string title = null)
        {
            window = new Window()
            {
                Title = title,
                // Height = SystemParameters.PrimaryScreenHeight * 0.65,
                Height = SystemParameters.PrimaryScreenHeight * 0.5,
                Width = SystemParameters.PrimaryScreenWidth * 0.5,
                // MinHeight = 500,
                // MinWidth = 750,
                MinHeight = 300,
                MinWidth = 400,
                Background = Brushes.White,
                
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.Loaded += (object sender, RoutedEventArgs e) =>
            {
                appStart?.Execute();
            };

            window.Closed += (object sender, EventArgs e) => ((IEvent)this).Execute();

            window.SizeToContent = SizeToContent.Height; // Resizes the popup window to match the height of its contained contents
        }




        public void Run()
        {
            window.Content = child?.GetWPFElement();
            System.Windows.Application app = new System.Windows.Application();
            app.Run(window);
        }




        // IEvent implementation -------------------------------------------------------
        void IEvent.Execute() => System.Windows.Application.Current.Shutdown();




        // IDataFlow<bool> implementation ----------------------------------------------
        bool IDataFlow<bool>.Data
        {
            get => window.IsEnabled;
            set => window.IsEnabled = value;
        }
    }
}
