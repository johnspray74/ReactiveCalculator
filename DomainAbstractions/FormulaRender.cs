using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using ProgrammingParadigms;
using WpfMath.Controls;

namespace DomainAbstractions
{
    /// <summary>
    /// A UI element, just like a label with a text string on it. Two inputs:
    /// 1. IUI for getting the WPF element.
    /// 2. IDataFlow<string> for inputting the text.
    /// </summary>
    public class FormulaRender : IUI, IDataFlow<string>
    {
        // properties
        public string InstanceName { get; set; } = "Default";

        public double FontSize { set => formulaControl.FontSize = value; }


        public double margin { set => formulaControl.Margin = new Thickness(value, value, value, value); }



        // private fields
        private UIElement wpfElement;
        private WpfMath.Controls.FormulaControl formulaControl = new WpfMath.Controls.FormulaControl();

        /// <summary>
        /// An IUI abstraction which disaplys a giving text.
        /// </summary>
        /// <param name="text">text displayed</param>
        /// <param name="visible">control the visibility of the text</param>
        public FormulaRender()
        {
        }





        // IUI implementation ---------------------------------------------------------------
        UIElement IUI.GetWPFElement()
        {

            wpfElement = formulaControl;
            formulaControl.Scale = 30;
            formulaControl.SelectionBrush = Brushes.LightBlue;
            formulaControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            formulaControl.VerticalAlignment = VerticalAlignment.Stretch;
            formulaControl.SnapsToDevicePixels = true;
            margin = 5;
            return wpfElement;
        }




        // IDataFlow<string> implementation ---------------------------------------------------------------
        string IDataFlow<string>.Data
        {
            get => formulaControl.Formula;
            set 
            {
                Analytics.Translator translator = new Analytics.Translator();
                Exversion.Analytics.AnalyticsConverter converter = new Exversion.Analytics.AnalyticsTeXConverter();
                try
                {
                    if (translator.CheckSyntax(value))
                    {
                        // object result = translator.Calculate(value);  // returns the calculated value TBD output this on a port and display it as well
                        string latexString;
                        if (value != "") 
                        {
                            latexString = converter.Convert(value); // it doesn't seem to like a null string being passed to it
                        }
                        else
                        {
                            latexString = "";
                        }
                        formulaControl.Formula = latexString;
                        // formulaControl.Formula = @"{{{A}\cdot{{sin}\left({{n}\cdot{x}}\right)}}+{{\frac{B}{2}}\cdot{{e}^{{m}\cdot{y}}}}}";
                        // formulaControl.Formula = latexString + " = {" + result + "}";
                    }
                }
                catch (Exception ex)
                {
                    formulaControl.Formula = ex.Message;
                }
            }
        }

    }
}
