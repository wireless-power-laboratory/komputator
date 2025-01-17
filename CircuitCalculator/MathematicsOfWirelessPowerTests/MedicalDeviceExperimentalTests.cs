using System.Globalization;
using AnalyticLibrary.Fields;
using AnalyticLibrary.Numbers;
using AnalyticLibrary.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitCalculatorTests
{
    /// <summary>
    /// This class is for numeric solutions to the physical model of the circuit in human proximity appearing in "Magnetic resonant energy transfer to unipolar neurons for dendrite growth".
    /// </summary>
    [TestClass]
    public class MedicalDeviceExperimentalTests
    {
        public Antenna CreateTranmissionAntenna()
        {
            const Antenna.AntennaType type = Antenna.AntennaType.Loop;
            const Antenna.AntennaWinding winding = Antenna.AntennaWinding.Litz;
            const Antenna.DimensionalUnits dimension = Antenna.DimensionalUnits.mm;
            const Antenna.AntennaPresence presenceType = Antenna.AntennaPresence.Transmitter;
            const Antenna.InductanceMethod inductanceMethod = Antenna.InductanceMethod.Grover;
            const Antenna.AntennaMaterial material = Antenna.AntennaMaterial.Copper;
            Circuit.CircuitCapacitance = 90;
            Circuit.CircuitCapacitanceUnit = "nF";
            // Measured physical values of the medical unit.
            var legs = new double[] { 50, 100, 144 };
            // Initialize with an application of circuital properties
            Circuit.Voltage = 4.5;
            Circuit.Current = new Complex(1.105, 0);
            Antenna.ResonanceParameters.DipoleDistance = 0.020;
            Antenna.ResonanceParameters.ObservationDistance = 0.250;
            Antenna.ResonanceParameters.InferredFieldHeight = 0.060;
            // Initialize a new antenna object with the specified parameters.
            var antenna = new Antenna(29, 0.5, 2.65, legs, dimension, material, type, winding, presenceType, inductanceMethod);
            
            // Calculate inductance.
            var inductanceValue = antenna.FormattedInductance;
            // Calculate resonance f, and wavelength.
            var resonanceValue = antenna.Resonance.ResonanceFrequency;
            var wavelength = antenna.Wavelength;
            // Calculate the regions distant from the antenna.
            var reactive = antenna.ReactiveRegion; // Outer boundary.
            var evanescent = antenna.EvanescentRegion; // Outer boundary.
            var radiating = antenna.RadiatingRegion; // Inner boundary.
            // Shape the local magnetic field from the antenna.
            antenna.ElectricFields = new ElectricField(antenna.AntennaRadius, antenna.Wavelength,
                                                       Circuit.Current,
                                                       Antenna.ResonanceParameters.ObservationDistance);
            var localElectricFieldStrength = antenna.ElectricFields.E;
            antenna.MagneticFields = new MagneticField(Circuit.Current, antenna.Wavelength, antenna.AntennaRadius, 0, Antenna.ResonanceParameters.InferredFieldHeight);

            return antenna;
        }

        [TestMethod]
        public void DosimetryTest()
        {
            // Write a test of what the electric field strength and magnetic field strength on a presence at 1 cm, 2 cm ... to the limit in three-dimensions.
            var antenna = CreateTranmissionAntenna();
            var localFluxDensity = antenna.MagneticFields.B.ToString(CultureInfo.InvariantCulture);
            var localFieldStrength = antenna.MagneticFields.H.ToString(CultureInfo.InvariantCulture);
            // Calculate electric field strength.
            var efs = antenna.ElectricFields.E;
            var nEfs = efs.ToString(CultureInfo.InvariantCulture);
            // Calculate magnetic field strength.
            var mfs = antenna.MagneticFields.H;
            var nMfs = mfs.ToString(CultureInfo.InvariantCulture);
            // Calculate magnetic flux density.
            var mfd = antenna.MagneticFields.B;
            var nMfd = mfd.ToString(CultureInfo.InvariantCulture);
            // Calculate SAR - http://www.tele.soumu.go.jp/e/sys/ele/body/

            // NOTES:
            // E0 = 0.000431115802422254 (A/m), H0 = 1.14357018334218E-06 (Gauss), P(rad) = 7.44137173633921E-12 W/m, Sr = 8.249E-16.
            // Not sure about these numbers as they have a const Poynting number and a reference to Kraus for small loops. A good idea is to check pp.197-8.
            // We also need induced current density, as a measure of Am-2.
            var hold = "";
            // These calcs need to be integrated and somehow confirmed in an experiment.

            // Test against the specification for limit of public exposure.
            const double limitPublicElectricFieldStrength = 87; // In V/m^-1.
            Assert.IsTrue(efs < limitPublicElectricFieldStrength, "The emitted electric field is greater than the specification by " + System.Math.Abs(efs - limitPublicElectricFieldStrength).ToString(CultureInfo.InvariantCulture) + ".");
            var limitPublicMagneticFieldStrength = 0.73 / antenna.Resonance.ResonanceFrequency; // In A/m^-1.
            //Assert.IsTrue(mfs < limitPublicMagneticFieldStrength, "The emitted magnetic field strength is greater than the specification by " + System.Math.Abs(mfs - limitPublicMagneticFieldStrength).ToString(CultureInfo.InvariantCulture) + ".");
            var limitPublicMagneticFluxDensity = 0.92 / antenna.Resonance.ResonanceFrequency; // In uT.
            //Assert.IsTrue(mfd < limitPublicMagneticFluxDensity, "The emitted magnetic flux density is greater than the specification by " + System.Math.Abs(mfd - limitPublicMagneticFluxDensity).ToString(CultureInfo.InvariantCulture) + ".");

            // FINIS.

            // Appendix:
            //
            // Help in computation:
            // 1. http://hllye.com/download/Antenna-Field-regions-calculator.html
            // 2. http://www.evaluationengineering.com/articles/200510/the-world-of-the-near-field.php
            // 
        }
    }
}
