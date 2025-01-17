using System;
using AnalyticLibrary.Materials;
using AnalyticLibrary.Numbers;
using AnalyticLibrary.Objects;
using CircuitCalculator.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitCalculatorTests
{
    [TestClass]
    public class CircuitCalculatorEngagementTests
    {
        /// <summary>
        /// A transmission (Tx) antenna.
        /// </summary>
        public Antenna TxAntenna { get; set; }
        /// <summary>
        /// A receiving (Rx) antenna.
        /// </summary>
        public Antenna RxAntenna { get; set; }
        /// <summary>
        /// The eddy disc in the circuit to explore medical transfer of power.
        /// </summary>
        public Disc EddyDisc { get; set; }
        /// <summary>
        /// The difference in frequency between Tx and Rx.
        /// </summary>
        public double DriftFrequency { get; set; }
        /// <summary>
        /// The deviation factor in mutual inductance calculation for environmental and explicit geometric variance.
        /// </summary>
        public double MutualDeviation { get; set; }
        public double[] AntennaLegs { get; set; }

        public CircuitCalculatorEngagementTests()
        {
            // Create a transmission antenna and a associated local circuit.
            const Antenna.AntennaType type = Antenna.AntennaType.Loop;
            const Antenna.AntennaWinding winding = Antenna.AntennaWinding.Litz;
            const Antenna.DimensionalUnits dimension = Antenna.DimensionalUnits.mm;
            const Antenna.AntennaPresence presenceType = Antenna.AntennaPresence.Transmitter;
            const Antenna.InductanceMethod inductanceMethod = Antenna.InductanceMethod.Grover;
            const Antenna.QualityFactorMethod qualityMethod = Antenna.QualityFactorMethod.Zeirhofer;
            const Antenna.AntennaMaterial material = Antenna.AntennaMaterial.Copper;
            var circuitTx = new Circuit(4.5, new Complex(1.105, 0), 90, "nF");
            // Measured physical values of the transmission antenna.
            var legs = new double[] { 20, 20, 50 };
            // Initialize with an application of circuital properties
            Circuit.Voltage = circuitTx.CircuitVoltage();
            Circuit.Current = circuitTx.CircuitCurrent();
            Antenna.ResonanceParameters.DipoleDistance = 0.020;
            Antenna.ResonanceParameters.ObservationDistance = 0.250;
            Antenna.ResonanceParameters.InferredFieldHeight = 0.060;
            // Initialize a new antenna object with the specified parameters.
            TxAntenna = new Antenna(29, 0.5, 3, legs, dimension, material, type, winding, presenceType, circuitTx, inductanceMethod, qualityMethod);
            // Create a receiving antenna a nd an associated local circuit.
            const Antenna.AntennaType typeRx = Antenna.AntennaType.Loop;
            const Antenna.AntennaWinding windingRx = Antenna.AntennaWinding.Litz;
            const Antenna.DimensionalUnits dimensionRx = Antenna.DimensionalUnits.mm;
            const Antenna.AntennaPresence presenceTypeRx = Antenna.AntennaPresence.Receiver;
            const Antenna.InductanceMethod inductanceMethodRx = Antenna.InductanceMethod.Grover;
            const Antenna.QualityFactorMethod qualityMethodRx = Antenna.QualityFactorMethod.Zeirhofer;
            const Antenna.AntennaMaterial materialRx = Antenna.AntennaMaterial.Copper;
            var circuitRx = new Circuit(4.5, new Complex(1.105, 0), 90, "nF");
            // Measured physical values of the receiving antenna.
            var legsRx = new double[] { 20, 20 };
            // Initialize a new antenna object with the specified parameters.
            RxAntenna = new Antenna(29, 0.5, 3, legsRx, dimensionRx, materialRx, typeRx, windingRx, presenceTypeRx, circuitRx, inductanceMethodRx, qualityMethodRx);
            // Extract some interesting comparative properties.
            DriftFrequency = Math.Abs(TxAntenna.Resonance.ResonanceFrequency - RxAntenna.Resonance.ResonanceFrequency);
            // What kind of deviation in mutual inductance can we expect? Can be known more intimately if computing for inductive linking effects equations.
            MutualDeviation = 0.038;

        }
        [TestMethod]
        public void CanTriggerFormComputation()
        {
            var form = new ResistiveRadiativeLossesCalculator();
            var resistance = form.CalculateOhmicResistance();
            Console.WriteLine(@"Resistance: " + resistance);
        }
        /// <summary>
        /// Mirrors the sequence when the CircuitCalculator app loads and the follow-thru clicks from the user.
        /// </summary>
        [TestMethod]
        public void CallingAllFormComputations()
        {
            // First is resonance (interesting).
            var outputString = @"Resonance: , Tx.f (Hz): " + TxAntenna.Resonance.ResonanceFrequency + @", Rx.f (Hz): " + RxAntenna.Resonance.ResonanceFrequency + @", Tx.lambda (m): " + TxAntenna.Wavelength + @", Rx.lambda (m): " + RxAntenna.Wavelength + @", Tx-Rx-Drift.f (Hz): " + DriftFrequency + @", Tx.sh256 (Hz): " + TxAntenna.Subharmonic256 + @", Rx.sh256 (Hz): " + RxAntenna.Subharmonic256;
            var print = outputString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print);
            // Next is quality.
            var qualityForm = new QualityFactorCalculator();
            var qualityTx = qualityForm.CalculateZeirhoferQuality(TxAntenna.AntennaResistance, TxAntenna.SelfInductance,
                TxAntenna.Circuit.CircuitualCapacitance());
            var qualityRx = qualityForm.CalculateZeirhoferQuality(RxAntenna.AntennaResistance, RxAntenna.SelfInductance,
                RxAntenna.Circuit.CircuitualCapacitance());
            var qualOutString = @"Quality factor: , Tx: " + qualityTx + @", Rx: " + qualityRx;
            var print5 = qualOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print5);
            // Next is inductance (interesting).
            var inOutString = @"Inductance: , Tx (H): " + TxAntenna.SelfInductance + @", Rx (H): " + RxAntenna.SelfInductance;
            var print2 = inOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print2);
            var mutualInductance = MutualDeviation * Math.Abs(TxAntenna.SelfInductance + RxAntenna.SelfInductance) / 2; // In uH, what slippage factor?
            var mutInOutString = @"Mutual inductance: , Tx-Rx (H): " + mutualInductance;
            var print6 = mutInOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print6);
            // Next is field power.
            var fieldPowerForm = new FieldsRadiativeCalculator();

            // Next is radiative resistance.
            var ohmOutString = @"Resistive loss: , Ohmic.Tx (Ω): " + TxAntenna.AntennaResistance + @", Ohmic.Rx (Ω): " + RxAntenna.AntennaResistance;
            var print3 = ohmOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print3);
            var radOutString = @"Radiative loss: , Rad.Tx (Ω): " + TxAntenna.RadiatingRegion / Math.PI + @", Rad.Rx (Ω): " + RxAntenna.RadiatingRegion / Math.PI;
            var print7 = radOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print7);
            // Next is Joule heating.
            var joule = 1231.54; // Approximated. Write computation to investigate.
            var jouOutString = @"Joule heating at capacitor: , Tx.f (J): " + joule;
            var print8 = jouOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print8);
            // Next is MOSFET switching speed.
            var period = 45; // Approximated on 10 nF capacitors. Write computation to investigate.
            var periodOutString = @"MOSFET switching period: , Tx.s (ms): " + period;
            var print9 = periodOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print9);
            // Last is efficiency of the scheme. k, M from thesis research.
            var efficiencyForm = new EfficiencyCalculator();
            var couplingCoeffForm = new CouplingCoefficientCalculator();
            var efficiency = efficiencyForm.CalculateTwoCoilEfficiency(couplingCoeffForm.CalculateFirstK(mutualInductance, TxAntenna.SelfInductance, RxAntenna.SelfInductance), qualityForm.CalculateZeirhoferQuality(TxAntenna.AntennaResistance, TxAntenna.SelfInductance, TxAntenna.Circuit.CircuitualCapacitance()), qualityForm.CalculateZeirhoferQuality(RxAntenna.AntennaResistance, RxAntenna.SelfInductance, RxAntenna.Circuit.CircuitualCapacitance()));
            var effOutString = @"Efficiency of the model: , " + efficiency;
            var print4 = effOutString.Replace(",", " " + Environment.NewLine);
            Console.WriteLine(print4);
        }

        [TestMethod]
        public void AntennaDiscTest()
        {
            AntennaLegs = new[] {20.0, 20.0, 50.0};
            // Add the model for how the machine with disk couples to your electrical tissues and transfers energy in the (cymatic) pattern.
            TxAntenna = Core.CreateAntenna(new Circuit(4.5, new Complex(1.105, 0), 90, "nF" ), Antenna.AntennaPresence.Transmitter, AntennaLegs, 50, 1.2, 40);
            AntennaLegs = new[] { 20.0, 20.0 };
            RxAntenna = Core.CreateAntenna(new Circuit(4.5, new Complex(0.05, 0), 90, "nF"), Antenna.AntennaPresence.Receiver, AntennaLegs);
            EddyDisc = new Disc(RxAntenna, RxAntenna.AntennaRadius, 0.010, Material.Aluminum);
        }

        [TestMethod]
        public void EddyCurrentTest()
        {

        }
    }
}
