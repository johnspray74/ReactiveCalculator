
using ProgrammingParadigms;

namespace DomainAbstractions
{
    /// <summary>
    /// ALA Domain Abstraction: Just takes up space. Useful n the Vertical and Horizontal to make gaps.
    /// Becasue this entire set of DAs are designed to size from the outer window in as proportions, it is easier to do spacial contro with this abstraction rather than WPF like margins or padding.
    /// Will size itself to fill the available space.
    /// </summary>
    class Space : IUI
    {
        public string InstanceName { get; set; }






        System.Windows.UIElement IUI.GetWPFElement()
        {
            // Debug.WriteLine($"Space[{instanceName}] Build");
            var WPFborder = new System.Windows.Controls.Border();
            // if (!Debug.GraphicsMode) WPFborder.Visibility = System.Windows.Visibility.Hidden;
            WPFborder.Width = 10;
            WPFborder.Height = 10;
            WPFborder.BorderThickness = new System.Windows.Thickness(1);
            WPFborder.BorderBrush = System.Windows.Media.Brushes.Red;
            WPFborder.CornerRadius = new System.Windows.CornerRadius(4);
            return WPFborder;
        }

    }
}

