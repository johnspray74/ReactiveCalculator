using Newtonsoft.Json;
using ProgrammingParadigms;
using System.Windows;

namespace DomainAbstractions
{
    /// <summary>
    /// <para>Contains a WPF TextBox</para>
    /// </summary>
    public class TextBox : IUI, IEvent, IBidirectionalDataflow<string>
    {
        // properties
        public string InstanceName { get; set; } = "Default";
        public HorizontalAlignment horizontalAlignment { set => textBox.HorizontalAlignment = value; }
        public double Margin { set => textBox.Margin = new Thickness(value,value,value,value); }
        public Thickness Margins { set => textBox.Margin = value; }
        // public double Height { set => textBox.Height = value; }
        // public double MinWidth { set => textBox.MinWidth = value; }
        public double FontSize { set => textBox.FontSize = value; }
        public string Text { set { textBox.Text = value; } }

        public bool Multiline { set
            {
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.AcceptsReturn = true;
            }
        }

        // ports
        // IUI Parent : The UI element that contains this TextBox
        // IEvent Clear : Make the TextBox blank
        // IBidirectionalDataflow<string> Persistence : Push persistence data each time TextBox changes, receive persistence data at start of program running
        private IDataFlow<string> textOutput;





        // Fields
        // TextBox overlaps with Systems.Windows.Controls.TextBox if we have "using System.Windows.Controls;"
        private System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox();




        /// <summary>
        /// <para>Contains a WPF TextBox and both implements and provides ports for setting/getting the text inside.</para>
        /// </summary>
        public TextBox(bool readOnly = false)
        {
            textBox.TextChanged += (object sender, System.Windows.Controls.TextChangedEventArgs e) => TextChanged();
            //DataChanged = TextBox_TextChanged;
            textBox.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            textBox.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            textBox.IsReadOnly = readOnly;
            Margin = 5;
        }

        private void TextChanged()
        {
            if (textOutput != null) textOutput.Data = textBox.Text;
            if (!suppressLoop) BPushToA?.Invoke(this, $"\"{InstanceName}\":\"{textBox.Text}\""); // output Json for persistence
            suppressLoop = false;
        }

        // IUI implementation
        System.Windows.UIElement IUI.GetWPFElement()
        {
            return textBox;
        }


        // IEvent implementation
        void IEvent.Execute()
        {
            textBox.Clear();
        }





        // implement the IBidirectionalDataflow interface. We are the B side. We need to support push in both directions but not Pull

        private event PutData<string> BPushToA;
        event PutData<string> IBidirectionalDataflow<string>.BPushToA { add { BPushToA += value; } remove { BPushToA -= value; } }


        void IBidirectionalDataflow<string>.APushToB(string data)
        {
            if (data.Length > 0) // we will get null string here if nothing was ever enetered in the field
            {
                char[] separators = { ':', '"' };
                string[] strs = data.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (strs[0] == InstanceName)
                {
                    suppressLoop = true;
                    textBox.Text = strs[1];
                }
                else
                {
                    throw new System.Exception("Wrong Json name");
                }
            }
        }

        private bool suppressLoop = false;

    }
}
