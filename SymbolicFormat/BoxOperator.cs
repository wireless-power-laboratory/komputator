using System.Windows.Forms;

namespace SymbolicFormat
{
    public partial class BoxOperator : UserControl
    {
        public BoxOperator()
        {
            InitializeComponent();
            BoxOperatorValue = operatorBox.Text;
        }

        public string BoxOperatorValue { get { return operatorBox.Text; } set { operatorBox.Text = value; } }
    }
}
