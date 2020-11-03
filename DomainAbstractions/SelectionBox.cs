using ProgrammingParadigms;
using System;
using System.Linq;
using System.Windows;

namespace DomainAbstractions
{
    /// <summary>
    /// <para>Contains a WPF ComboBox and both implements and provides ports for setting/getting the text inside.</para>
    /// <para>Ports:</para>
    /// <para>1. IUI wpfElement: returns the contained comboBox</para>
    /// <para>5. IDataFlow&lt;string&gt; textOutput: outputs the enum of type T when the user selects a value</para>
    /// </summary>
    public class SelectionBox<T> : IUI where T : Enum
    {
        // properties
        public string InstanceName { get; set; } = "Default";
        public HorizontalAlignment horizontalAlignment { set => comboBox.HorizontalAlignment = value; }
        public double Margin { set => comboBox.Margin = new Thickness(value, value, value, value); }
        public Thickness Margins { set => comboBox.Margin = value; }

        // public double Height { set => comboBox.Height = value; }
        // public double MinWidth { set => comboBox.MinWidth = value; }
        public double FontSize { set => comboBox.FontSize = value; }

        public T Value { get => (T)System.Enum.ToObject(typeof(T), comboBox.SelectedIndex); set => comboBox.SelectedIndex = (int)(object)value; }


        // ports
        private IDataFlow<T> output;
        private void outputPostWiringInitialize()
        {
            // output the default as soon as we are wired
            comboBox_TextChanged();
        }




        // Fields
        // comboBox overlaps with Systems.Windows.Controls.comboBox if we have "using System.Windows.Controls;"
        private System.Windows.Controls.ComboBox comboBox = new System.Windows.Controls.ComboBox();




        /// <summary>
        /// <para>Contains a WPF comboBox and both implements and provides ports for setting/getting the text inside.</para>
        /// </summary>
        public SelectionBox(bool readOnly = false)
        {
            comboBox.SelectionChanged += (object sender, System.Windows.Controls.SelectionChangedEventArgs args) => comboBox_TextChanged();
            comboBox.IsReadOnly = readOnly;
            Margin = 5;
            comboBox.ItemsSource = System.Enum.GetNames(typeof(T));
            comboBox.SelectedIndex = 0;
        }




        private void comboBox_TextChanged()
        {
            if (output != null) output.Data = (T)System.Enum.ToObject(typeof(T), comboBox.SelectedIndex);
        }


        /*
        private T ConvertValue<T, U>(U value) where U : System.IConvertible
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }
        */

        // IUI implementation
        System.Windows.UIElement IUI.GetWPFElement()
        {
            return comboBox;
        }

    }
}
