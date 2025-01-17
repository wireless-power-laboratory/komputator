using System;
using System.Windows.Forms;

namespace SymbolicFormat
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void FormatButtonClick(object sender, EventArgs e)
        {
            var num1 = boxDivisor1.NumeratorBoxValue;
            var den1 = boxDivisor1.DenominatorBoxValue;
            var operation = boxOperator.BoxOperatorValue;
            var num2 = boxDivisor2.NumeratorBoxValue;
            var den2 = boxDivisor2.DenominatorBoxValue;

            //outputTextBox.Text = ((num1) / (den1)) * ((2 * Math.PI * Math.Pow(loopRadius, 2) * current) / (Math.Pow(Math.Pow(distance, 2) + Math.Pow(loopRadius, 2), 1.5)));
            // This could be bearish.
        }
        /// <summary>
        /// Builds the programmed equation from the inputs.
        /// </summary>
        /// <remarks>Use ^ for powers instead of more boxes.</remarks>
        protected string BuildProgrammedEquation()
        {
            var result = "";
            
            return result;
        }

    }
}
