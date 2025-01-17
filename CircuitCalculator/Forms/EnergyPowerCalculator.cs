using System;
using System.Windows.Forms;
using CircuitCalculator.Numbers;

namespace CircuitCalculator.Forms
{
    public partial class EnergyPowerCalculator : Form
    {
        private Form owner;

        public EnergyPowerCalculator(Form mOwner)
        {
            InitializeComponent();
            degRadSelection.SelectedItem = "deg";
            inductanceFactor.SelectedItem = "uH";
            distanceFactor.SelectedItem = "cm";
            litzWireLengthFactor.SelectedItem = "cm";
            inputCurrentFactor.SelectedItem = "A";
            inputCurrentPowerFactor.SelectedItem = "A";
            frequencyFactor.SelectedItem = "Hz";
            owner = mOwner;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ControlBox = false;
            Instance = true;
        }
        public EnergyPowerCalculator()
        {
            InitializeComponent();
            degRadSelection.SelectedItem = "deg";
            inductanceFactor.SelectedItem = "uH";
            distanceFactor.SelectedItem = "cm";
            litzWireLengthFactor.SelectedItem = "cm";
            inputCurrentFactor.SelectedItem = "A";
            inputCurrentPowerFactor.SelectedItem = "A";
            frequencyFactor.SelectedItem = "Hz";
            Instance = true;
        }

        private void energyPowerCalculator_Load(object sender, EventArgs e)
        {
            var toolTip = new ToolTip {AutoPopDelay = 10000, InitialDelay = 400, ReshowDelay = 250, ShowAlways = true};
            toolTip.SetToolTip(distancePictureBox, "The observation distance from the coil.");
            toolTip.SetToolTip(thetaPictureBox, "The observation angle from the coil.");
            //toolTip.SetToolTip(this.dipoleDistancePictureBox, "The distance from the dipole, which in the case of a circular loop is the observation distance since the dipole lies in the x-y plane.");
            //toolTip.SetToolTip(this.depthPenetrationPictureBox, "The depth of penetration of the antenna field.");
        }
        /// <summary>
        /// Checks the instance of the form.
        /// </summary>
        public static bool Instance { get; set; }

        /// <summary>
        /// Closes the form.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            Instance = false;
            Close();
        }
        /// <summary>
        /// Calculates the lambda.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        protected double CalculateLambda(double frequency)
        {
            var result = Constants.SpeedOfLight / frequency;
            return result;
        }

        /// <summary>
        /// Calculates the radians from degrees.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        protected double CalculateRadians(double degrees)
        {
            double result = 0;
            result = degrees * (Math.PI / 180);

            return result;
        }
        /// <summary>
        /// Calculates the electric current on a Litz wire.
        /// </summary>
        public double CalculateElectricLitz()
        {
            double result = 0;
            double theta = 0;
            double frequency = 0;
            double distance = 0;
            double inputCurrent = 0;
            double numberOfTurns = Convert.ToDouble(turnsTextBox.Text);
            double tempTheta = Convert.ToDouble(thetaValueBox.Text);
            double tempDistance = Convert.ToDouble(distanceValueBox.Text);
            double tempFrequency = Convert.ToDouble(frequencyValueBox.Text);
            double tempCurrent = Convert.ToDouble(inputCurrentValueBox.Text);

            if (degRadSelection.Text == "deg")
            {
                theta = CalculateRadians(tempTheta);
            }
            if (degRadSelection.Text == "rad")
            {
                theta = tempTheta;
            }
            if (distanceFactor.Text == "m")
            {
                distance = tempDistance;
            }
            if (distanceFactor.Text == "cm")
            {
                distance = tempDistance * 1E-2;
            }
            if (distanceFactor.Text == "mm")
            {
                distance = tempDistance * 1E-3;
            }
            if (frequencyFactor.Text == "MHz")
            {
                frequency = tempFrequency * 1E6;
            }
            if (frequencyFactor.Text == "kHz")
            {
                frequency = tempFrequency * 1E3;
            }
            if (frequencyFactor.Text == "Hz")
            {
                frequency = tempFrequency;
            }
            if (inputCurrentFactor.Text == "A")
            {
                inputCurrent = tempCurrent;
            }
            if (inputCurrentFactor.Text == "mA")
            {
                inputCurrent = tempCurrent * 1E-3;
            }
            if (inputCurrentFactor.Text == "uA")
            {
                inputCurrent = tempCurrent * 1E-6;
            }

            double lambda = CalculateLambda(frequency);
            double beta = (Math.PI * Math.Sin(theta)) / lambda;
            double der = (Math.PI * distance * Math.Sin(theta) / theta);
            result = inputCurrent * (Math.Sin((Math.Pow(Math.Sin(beta), 2) / Math.Pow(beta, 2)) * (Math.Pow(Math.Sin((numberOfTurns * der)), 2) / Math.Pow(Math.Sin(der), 2))));
            electricCurrentThetaSolutionBox.Text = result.ToString();

            return result;
        }

        #region Click events
        /// <summary>
        /// Calculates the electric Litz result.
        /// </summary>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            CalculateElectricLitz();
        }
        /// <summary>
        /// Calculates the stored energy in the magnetic field.
        /// </summary>
        private void calculateStoredEnergyButton_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Calculates the dissipated power.
        /// </summary>
        private void calculateDissipatedPowerButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}