using System.Windows.Forms;

namespace SymbolicFormat
{
    public partial class BoxDivisor : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDivisor"/> class.
        /// </summary>
        public BoxDivisor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets the numerator box value.
        /// </summary>
        public string NumeratorBoxValue
        {
            get { return boxOperatorNumerator.BoxOperatorValue; }
        }
        /// <summary>
        /// Gets the denominator box value.
        /// </summary>
        public string DenominatorBoxValue
        {
            get { return boxOperatorDenominator.BoxOperatorValue; }
        }
    }
}
