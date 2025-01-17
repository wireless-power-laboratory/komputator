using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CircuitCalculator.Forms
{
    public partial class CouplingCoefficientCalculator : Form
    {
        private Form owner;

        public CouplingCoefficientCalculator(Form mOwner)
        {
            InitializeComponent();
            owner = mOwner;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ControlBox = false;
            Instance = true;
        }

        public CouplingCoefficientCalculator()
        {
            InitializeComponent();
            Instance = false;
            mutualInductanceFactor.Text = "uH";
            aInductanceFactor.Text = "uH";
            bInductanceFactor.Text = "uH";

        }

        /// <summary>
        /// Checks the instance of the form.
        /// </summary>
        public static bool Instance { get; set; }

        private void CouplingCoefficientCalculator_Load(object sender, EventArgs e)
        {
            var toolTip = new ToolTip {AutoPopDelay = 5000, InitialDelay = 750, ReshowDelay = 250, ShowAlways = true};
            toolTip.SetToolTip(mutualInductanceValueBox, "The Calculated value of Mutual Inductance in the Circuit");
            toolTip.SetToolTip(mutualInductanceFactor, "The Dimension of the Mutual Inductance");
            toolTip.SetToolTip(aInductanceValueBox, "The Self-Inductance of the first Coil in the Circuit");
            toolTip.SetToolTip(aInductanceFactor, "The Dimension of the Inductance");
            toolTip.SetToolTip(bInductanceValueBox, "The Self-Inductance of the second Coil in the Circuit");
            toolTip.SetToolTip(bInductanceFactor, "The Dimension of the Inductance");
            toolTip.SetToolTip(angularFrequencyValueBox, "The Excited frequency inherent in the Circuit");
            toolTip.SetToolTip(angularFrequencyUnit, "The Unit of Oscillation Used");
            toolTip.SetToolTip(k1PictureBox, "The common form of the coupling coefficient");
            //toolTip.SetToolTip(this.k1CheckBox, "Check this box if you want to calculate this equation");
            toolTip.SetToolTip(saveK1SelectionBox, "Select if you want to calculate for efficiency later.");
            //toolTip.SetToolTip(this.k2PictureBox, "A modified form of the coupling coefficient");
            //toolTip.SetToolTip(this.k2CheckBox, "Check this box if you want to calculate this equation");
            //toolTip.SetToolTip(this.saveK2SelectionBox, "Select if you want to calculate for efficiency later.");
        }
        /// <summary>
        /// Calculates the first instance of the coupling coefficient.
        /// </summary>
        /// <param name="m">The mutual inductance.</param>
        /// <param name="la">The first inductance.</param>
        /// <param name="lb">The second inductance.</param>
        public double CalculateFirstK(double m, double la, double lb)
        {
            double mutualInductance = m;
            double firstInductance = la;
            double secondInductance = lb;

            //var tempMutualInductance = Convert.ToDouble(mutualInductanceValueBox.Text);
            //var tempFirstInductance = Convert.ToDouble(aInductanceValueBox.Text);
            //var tempSecondInductance = Convert.ToDouble(bInductanceValueBox.Text);
            var tempMutualInductance = mutualInductance;
            var tempFirstInductance = firstInductance;
            var tempSecondInductance = secondInductance;

            #region Mutual Inductance Factor
            if (mutualInductanceFactor.Text == "mH")
            {
                mutualInductance = tempMutualInductance * 1E-3;
            }
            if (mutualInductanceFactor.Text == "uH")
            {
                mutualInductance = tempMutualInductance * 1E-6;
            }
            if (mutualInductanceFactor.Text == "nH")
            {
                mutualInductance = tempMutualInductance * 1E-9;
            }
            if (mutualInductanceFactor.Text == "pH")
            {
                mutualInductance = tempMutualInductance * 1E-12;
            }
            #endregion
            #region First Inductance Factor
            if (aInductanceFactor.Text == "mH")
            {
                firstInductance = tempFirstInductance * 1E-3;
            }
            if (aInductanceFactor.Text == "uH")
            {
                firstInductance = tempFirstInductance * 1E-6;
            }
            if (aInductanceFactor.Text == "nH")
            {
                firstInductance = tempFirstInductance * 1E-9;
            }
            if (aInductanceFactor.Text == "pH")
            {
                firstInductance = tempFirstInductance * 1E-12;
            }
            #endregion
            #region Second Inductance Factor
            if (bInductanceFactor.Text == "mH")
            {
                secondInductance = tempSecondInductance * 1E-3;
            }
            if (bInductanceFactor.Text == "uH")
            {
                secondInductance = tempSecondInductance * 1E-6;
            }
            if (bInductanceFactor.Text == "nH")
            {
                secondInductance = tempSecondInductance * 1E-9;
            }
            if (bInductanceFactor.Text == "pH")
            {
                secondInductance = tempSecondInductance * 1E-12;
            }
            #endregion
            
            // Perform the calculation.
            var sqrt = Math.Sqrt(firstInductance * secondInductance);
            var result = mutualInductance / sqrt;
            resultBox.Text = result.ToString(CultureInfo.InvariantCulture);

            if (saveK1SelectionBox.Checked)
            {
                SaveParameterResult(result, "CouplingCoefficient1");
            }

            return result;
        }
        /// <summary>
        /// Calculates the second instance of the coupling coefficient.
        /// </summary>
        /// <param name="m">The mutual inductance.</param>
        /// <param name="la">The first inductance.</param>
        /// <param name="lb">The second inductance.</param>
        /// <param name="omega">The oscillation frequency.</param>
        public double CalculateSecondK(string m, string la, string lb, string omega)
        {
            double result = 0;
            double mutualInductance = 0;
            double firstInductance = 0;
            double secondInductance = 0;
            double angularFrequency = 0;

            var tempMutualInductance = Convert.ToDouble(mutualInductanceValueBox.Text);
            var tempFirstInductance = Convert.ToDouble(aInductanceValueBox.Text);
            var tempSecondInductance = Convert.ToDouble(bInductanceValueBox.Text);
            var tempAngularFrequency = Convert.ToDouble(angularFrequencyValueBox.Text);

            #region Mutual Inductance Factor
            if (mutualInductanceFactor.Text == "mH")
            {
                mutualInductance = tempMutualInductance * 1E-3;
            }
            if (mutualInductanceFactor.Text == "uH")
            {
                mutualInductance = tempMutualInductance * 1E-6;
            }
            if (mutualInductanceFactor.Text == "nH")
            {
                mutualInductance = tempMutualInductance * 1E-9;
            }
            if (mutualInductanceFactor.Text == "pH")
            {
                mutualInductance = tempMutualInductance * 1E-12;
            }
            #endregion
            #region First Inductance Factor
            if (aInductanceFactor.Text == "mH")
            {
                firstInductance = tempFirstInductance * 1E-3;
            }
            if (aInductanceFactor.Text == "uH")
            {
                firstInductance = tempFirstInductance * 1E-6;
            }
            if (aInductanceFactor.Text == "nH")
            {
                firstInductance = tempFirstInductance * 1E-9;
            }
            if (aInductanceFactor.Text == "pH")
            {
                firstInductance = tempFirstInductance * 1E-12;
            }
            #endregion
            #region Second Inductance Factor
            if (bInductanceFactor.Text == "mH")
            {
                secondInductance = tempSecondInductance * 1E-3;
            }
            if (bInductanceFactor.Text == "uH")
            {
                secondInductance = tempSecondInductance * 1E-6;
            }
            if (bInductanceFactor.Text == "nH")
            {
                secondInductance = tempSecondInductance * 1E-9;
            }
            if (bInductanceFactor.Text == "pH")
            {
                secondInductance = tempSecondInductance * 1E-12;
            }
            #endregion
            #region Angular Frequency Factor
            if (angularFrequencyUnit.Text == "rad/s")
            {
                angularFrequency = tempAngularFrequency;
            }
            if (angularFrequencyUnit.Text == "Hz")
            {
                angularFrequency = tempAngularFrequency / (2 * Math.PI);
            }
            #endregion

            // Perform the calculation.
            var sqrt = 2 * (Math.Sqrt(firstInductance * secondInductance));
            result = (angularFrequency * mutualInductance) / sqrt;
            //k2ResultBox.Text = result.ToString();

            //if (saveK2SelectionBox.Checked == true)
            //{
            //    SaveParameterResult(result, "CouplingCoefficient2");
            //}

            return result;
        }

        #region Save the parameter into the SQLite database.
        /// <summary>
        /// Saves the parameter result into the SQLite database.
        /// </summary>
        /// <param name="result">The returned result.</param>
        /// <param name="table">The table where the value is stored.</param>
        protected void SaveParameterResult(double result, string column)
        {
            var directory = MapPath(@"\db\parameters.db");
            var path = @"c:\Users\ctucker\aeonProjectDirectory\Monkey-Man\thesis\circuits\CircuitCalculator";

            var conn = new SQLiteConnection(@"Data Source=" + path + directory);
            var cmd = new SQLiteCommand();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Parameter (" + column + ") VALUES ('" + result + "')";
            conn.Open();
            var trans = conn.BeginTransaction();
            cmd.ExecuteNonQuery();
            trans.Commit();
            conn.Close();
            trans.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }
        /// <summary>
        /// Maps the path of the executing file.
        /// </summary>
        /// <param name="path">The path to map.</param>
        public virtual string MapPath(string path)
        {
            var location = Assembly.GetEntryAssembly().Location;
            var zebra = AppDomain.CurrentDomain.BaseDirectory.ToString();
            var local = Environment.CurrentDirectory;
            return Path.Combine(zebra, path);
        }
        #endregion

        /// <summary>
        /// Sets to calculate the solution.
        /// </summary>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            #region Commented
            //if (k1CheckBox.Checked)
            //{
            //    CalculateFirstK(mutualInductanceValueBox.Text, aInductanceValueBox.Text, bInductanceValueBox.Text);
            //}
            //if (k2CheckBox.Checked)
            //{
            //    CalculateSecondK(mutualInductanceValueBox.Text, aInductanceValueBox.Text, bInductanceValueBox.Text, angularFrequencyValueBox.Text);
            //}
            //else if (k1CheckBox.Checked != true && k2CheckBox.Checked != true)
            //{
            //    MessageBox.Show("Select a 'k' operation to perform.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            #endregion

            if (mutualInductanceValueBox.Text == "" | aInductanceValueBox.Text == "" | bInductanceValueBox.Text == "")
            {
                MessageBox.Show("Value boxes are empty. Unless you have checked to load from the database and there are values stored, zeros will be shown.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                mutualInductanceValueBox.Text = "0";
                aInductanceValueBox.Text = "0";
                bInductanceValueBox.Text = "0";
            }
            else
            {
                //CalculateTwoCoilCoupling(mutualInductanceValueBox.Text, aInductanceValueBox.Text, bInductanceValueBox.Text);
                CalculateCoupling();
            }
        }
        public double CalculateCoupling()
        {
            double result = 0;
            var mutualInductance = Convert.ToDouble(mutualInductanceValueBox.Text);
            var firstInductance = Convert.ToDouble(aInductanceValueBox.Text);
            var secondInductance = Convert.ToDouble(bInductanceValueBox.Text);
            var denominator = Math.Sqrt((firstInductance * secondInductance));
            result = mutualInductance / denominator;
            resultBox.Text = result.ToString();

            return result;
        }
        /// <summary>
        /// Calculates two-coil coupling.
        /// </summary>
        /// <param name="mutual">The mutual inductance.</param>
        /// <param name="firstInd">The first inductance.</param>
        /// <param name="secondInd">The second inductance.</param>
        /// <returns>The couping coefficient.</returns>
        public double CalculateTwoCoilCoupling(string mutual, string firstInd, string secondInd)
        {
            double result = 0;
            var Mab = Convert.ToDouble(mutual);
            var La = Convert.ToDouble(firstInd);
            var Lb = Convert.ToDouble(secondInd);
            var sqrt = Math.Sqrt(La * Lb);
            result = Mab / sqrt;
            resultBox.Text = result.ToString();

            return result;

        }
        /// <summary>
        /// Opens an instance of the Efficiency Calculator.
        /// </summary>
        private void efficiencyCalculatorButton_Click(object sender, EventArgs e)
        {
            if (EfficiencyCalculator.Instance == false)
            {
                var form = new EfficiencyCalculator(this);
                form.Show(this);
                EfficiencyCalculator.Instance = true;
            }
            else if (EfficiencyCalculator.Instance == true)
            {
                // Do nothing.
            }
        }
        /// <summary>
        /// Close the form.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            Instance = false;
            Close();
        }
    }
}
