using System;
using System.Windows.Forms;
using AnalyticLibrary.Numbers;
using AnalyticLibrary.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitCalculatorTests
{
    [TestClass]
    public class ThickAntennaTests
    {
        /// <summary>
        /// A transmission (Tx) antenna.
        /// </summary>
        public Antenna TxAntenna { get; set; }
        /// <summary>
        /// A receiving (Rx) antenna.
        /// </summary>
        public Antenna RxAntenna { get; set; }
        public double NumberOfTurns { get; set; }
        /// <summary>
        /// The lengths of the legs consisting the antenna, in mm.
        /// </summary>
        public double[] Legs { get; set; }
        /// <summary>
        /// The radius of the antenna, in mm.
        /// </summary>
        public double Radius { get; set; }
        /// <summary>
        /// The radius of the wire, in mm.
        /// </summary>
        public double WireRadius { get; set; }

        /// <summary>
        /// Not mentioned is that the antenna is hollow copper. Since the skin depth is small compared to the inner surfaces, this shouldn't impact.
        /// </summary>
        [TestMethod]
        public void DoesThickerAntennaPropagateFurther()
        {
            // Build a thick copper antenna, like the one I made in winter 2011.
            Radius = 50;
            WireRadius = 5;
            NumberOfTurns = 0.8;
            Legs = new[] { 0.1, 0.2 };
            TxAntenna = Core.CreateAntenna(new Circuit(4.5, new Complex(1.105, 0), 90, "nF"), Antenna.AntennaPresence.Transmitter, Legs, Radius, WireRadius, NumberOfTurns);
            RxAntenna = Core.CreateAntenna(new Circuit(4.5, new Complex(0.05, 0), 90, "nF"), Antenna.AntennaPresence.Receiver, Legs);
        }
    }
}
