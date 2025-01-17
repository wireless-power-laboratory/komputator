using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CircuitCalculator.Forms
{
    public partial class EfficiencyCalculator : Form
    {
        private Form _owner;

        public EfficiencyCalculator(Form mOwner)
        {
            InitializeComponent();
            _owner = mOwner;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ControlBox = false;
            Instance = true;
        }

        public EfficiencyCalculator()
        {
            InitializeComponent();
            Instance = false;
        }

        private void EfficiencyCalculator_Load(object sender, EventArgs e)
        {
            var toolTip = new ToolTip {AutoPopDelay = 5000, InitialDelay = 750, ReshowDelay = 250, ShowAlways = true};
            toolTip.SetToolTip(firstQualityValueBox, "The value for Qi of the first loop.");
            toolTip.SetToolTip(secondQualityValueBox, "The value for Qj of the second loop.");
            toolTip.SetToolTip(saveEfficiencySelectionBox, "Save the efficiency to the database.");
            toolTip.SetToolTip(loadValuesSelectionBox, "Load the values from the database if they have already been calculated previously.");
            toolTip.SetToolTip(resultBox, "The efficiency of the scheme.");

        }

        /// <summary>
        /// Checks the instance of the form.
        /// </summary>
        public static bool Instance { get; set; }

        /// <summary>
        /// Calculates the two-coil efficiency.
        /// </summary>
        /// <param name="k">The k.</param>
        /// <param name="firstQuality">The first quality.</param>
        /// <param name="secondQuality">The second quality.</param>
        /// <returns>Calculated result.</returns>
        public double CalculateTwoCoilEfficiency(double k, double firstQuality, double secondQuality)
        {
            var kab = k;
            var qa = firstQuality;
            var qb = secondQuality;

            #region Commented

            //if (k1SelectionBox.Checked == true && k2SelectionBox.Checked == true | k1SelectionBox.Checked == false && k2SelectionBox.Checked == false)
            //{
            //    MessageBox.Show("One value for k is necessary for the calculation.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            //if (k1SelectionBox.Checked == true)
            //{
            //    //Get the data from the table.
            //}
            //else if (k2SelectionBox.Checked == true)
            //{
            //    //Get the data from the table.
            //}

            // Calculate the result


            //SaveParameterResult(result, "Efficiency");
            #endregion

            var numerator = ((kab * kab) * qa * qb);
            var denominator = (1 + ((kab * kab) * qa * qb));
            var result = numerator / denominator;
            var percentage = result * 100;
            resultBox.Text = result.ToString(CultureInfo.InvariantCulture);
            firstPercentageBox.Text = percentage.ToString(CultureInfo.InvariantCulture);

            return result;
        }
        #region Save the parameter into the SQLite database.
        /// <summary>
        /// Saves the parameter result.
        /// </summary>
        /// <param name="result">The returned result.</param>
        /// <param name="column">The column where the value is stored.</param>
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
        /// Calculates the efficiency for two coils.
        /// </summary>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            if (couplingCoefficientValueBox.Text == "" | firstQualityValueBox.Text == "" | secondQualityValueBox.Text == "")
            {
                MessageBox.Show("Value boxes are empty. Unless you have checked to load from the database and there are values stored, zeros will be shown.", "Input Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //Check into how to use the OK-Cancel buttons here.
                couplingCoefficientValueBox.Text = "0";
                firstQualityValueBox.Text = "0";
                secondQualityValueBox.Text = "0";

                CalculateTwoCoilEfficiency(Convert.ToDouble(couplingCoefficientValueBox.Text), Convert.ToDouble(firstQualityValueBox.Text), Convert.ToDouble(secondQualityValueBox.Text));
            }
            else
            {
                CalculateTwoCoilEfficiency(Convert.ToDouble(couplingCoefficientValueBox.Text), Convert.ToDouble(firstQualityValueBox.Text), Convert.ToDouble(secondQualityValueBox.Text));
            }
        }
        /// <summary>
        ///  Calculates the efficiency for four coils.
        /// </summary>
        private void calculateFourCoilButton_Click(object sender, EventArgs e)
        {
            if (kabValueBox.Text == "" | kbcValueBox.Text == "" | kcdValueBox.Text == "" | qaValueBox.Text == "" | qbValueBox.Text == "" | qcValueBox.Text == "" | qdValueBox.Text == "")
            {
                MessageBox.Show("Value boxes are empty. Unless you have checked to load from the database and there are values stored, zeros will be shown.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                kabValueBox.Text = "0";
                kbcValueBox.Text = "0";
                kcdValueBox.Text = "0";
                qaValueBox.Text = "0";
                qbValueBox.Text = "0";
                qcValueBox.Text = "0";
                qdValueBox.Text = "0";

                CalculateFourCoilEfficiency(kabValueBox.Text, kbcValueBox.Text, kcdValueBox.Text, qaValueBox.Text, qbValueBox.Text, qcValueBox.Text, qdValueBox.Text);
            }
            else
            {
                CalculateFourCoilEfficiency(kabValueBox.Text, kbcValueBox.Text, kcdValueBox.Text, qaValueBox.Text, qbValueBox.Text, qcValueBox.Text, qdValueBox.Text);
            }
        }
        /// <summary>
        /// Calculates the four-coil efficiency.
        /// </summary>
        /// <param name="kab">The coupling coefficient of the first significant pair of coils.</param>
        /// <param name="kbc">The coupling coefficient of the second significant pair of coils.</param>
        /// <param name="kcd">The coupling coefficient of the third significant pair of coils.</param>
        /// <param name="qa">The quality factor of the first coil.</param>
        /// <param name="qb">The quality factor of the second coil.</param>
        /// <param name="qc">The quality factor of the third coil.</param>
        /// <param name="qd">The quality factor of the fourth coil.</param>
        /// <returns></returns>
        public double CalculateFourCoilEfficiency(string kabCoupling, string kbcCoupling, string kcdCoupling, string qaFactor, string qbFactor, string qcFactor, string qdFactor)
        {
            double result = 0;
            double percentage = 0;
            var kab = Convert.ToDouble(kabCoupling);
            var kbc = Convert.ToDouble(kbcCoupling);
            var kcd = Convert.ToDouble(kcdCoupling);
            var qa = Convert.ToDouble(qaFactor);
            var qb = Convert.ToDouble(qbFactor);
            var qc = Convert.ToDouble(qcFactor);
            var qd = Convert.ToDouble(qdFactor);

            var numerator = ((kab *kab) * qa * qb) * ((kbc * kbc) * qb * qc) * ((kcd *kcd) * qc * qd);
            var denominator = ((1 + (kab * kab) * qa * qb) * (1 + (kcd * kcd) * qc * qd) + (kbc * kbc) * qb * qc) * (1 + ((kbc * kbc) * qb * qc) + ((kcd * kcd) * qc * qd));
            result = numerator / denominator;
            percentage = result * 100;
            fourCoilResultBox.Text = result.ToString();
            percentageBox.Text = percentage.ToString();

            return result;
        }
        /// <summary>
        /// Calculates the additive Q equation.
        /// </summary>
        private void calculateAdditiveQualityButton_Click(object sender, EventArgs e)
        {
            if (kijValueBox.Text == "" | qiValueBox.Text == "" | qjValueBox.Text == "" | qkValueBox.Text == "" | riValueBox.Text == "" | rjValueBox.Text == "" | rkValueBox.Text == "")
            {
                MessageBox.Show("Value boxes are empty. Unless you have checked to load from the database and there are values stored, zeros will be shown.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                kijValueBox.Text = "0";
                qiValueBox.Text = "0";
                qjValueBox.Text = "0";
                qkValueBox.Text = "0";
                riValueBox.Text = "0";
                rjValueBox.Text = "0";
                rkValueBox.Text = "0";

                CalculateAdditiveQuality(kijValueBox.Text, qiValueBox.Text, qjValueBox.Text, qkValueBox.Text, riValueBox.Text, rjValueBox.Text, rkValueBox.Text);
            }
            else
            {
                CalculateAdditiveQuality(kijValueBox.Text, qiValueBox.Text, qjValueBox.Text, qkValueBox.Text, riValueBox.Text, rjValueBox.Text, rkValueBox.Text);
            }
        }
        /// <summary>
        /// Calculates the additive quality.
        /// </summary>
        /// <param name="kijCoupling">The kij coupling.</param>
        /// <param name="qiFactor">The qi factor.</param>
        /// <param name="qjFactor">The qj factor.</param>
        /// <param name="qkFactor">The qk factor.</param>
        /// <param name="riValue">The ri value.</param>
        /// <param name="rjValue">The rj value.</param>
        /// <param name="rkValue">The rk value.</param>
        public double CalculateAdditiveQuality(string kijCoupling, string qiFactor, string qjFactor, string qkFactor, string riValue, string rjValue, string rkValue)
        {
            double result = 0;
            double percentage = 0;
            var kij = Convert.ToDouble(kijCoupling);
            var qi = Convert.ToDouble(qiFactor);
            var qj = Convert.ToDouble(qjFactor);
            var qk = Convert.ToDouble(qkFactor);
            var ri = Convert.ToDouble(riValue);
            var rj = Convert.ToDouble(rjValue);
            var rk = Convert.ToDouble(rkValue);

            double numerator = 1;
            var denominator = (1 + (1/(Math.Pow(kij, 2))) * ((1 / qi) + (1 / ri)) * ((1 / qj) + (1 / rj)) * ((1 / qk) + (1 / rk)) * ((1 + (rj / qj)) * (1 + (rk / qk))));
            result = numerator / denominator;
            percentage = result * 100;
            additiveQualityResultBox.Text = result.ToString();
            thirdPercentageBox.Text = percentage.ToString();

            return result;
        }
        /// <summary>
        /// Closes the instance of the form.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            Instance = false;
            Close();
        }
        /// <summary>
        /// Calls the instance of the Coupling Coefficient Calculator form.
        /// </summary>
        private void couplingCoefficentCacluatorButton_Click(object sender, EventArgs e)
        {
            if (CouplingCoefficientCalculator.Instance == false)
            {
                var form = new CouplingCoefficientCalculator(this);
                form.Show(this);
                CouplingCoefficientCalculator.Instance = true;
            }
            else if (CouplingCoefficientCalculator.Instance == true)
            {
                // Do nothing.
            }
        }
    }
}