using System;
using System.Collections.Generic;
using AnalyticLibrary.Objects;

namespace CircuitCalculatorTests
{
    /// <summary>
    /// The class from which to create objects for different kinds, flavours, and function-parameters of tests.
    /// </summary>
    public class Core
    {
        /// <summary>
        /// Creates a standard Model 'G' antenna object.
        /// </summary>
        /// <param name="circuit">The circuit.</param>
        /// <param name="presence">The presence (Tx or Rx).</param>
        /// <param name="legs">The number and leg lengths used by the presence.</param>
        /// <param name="antennaRadius">The radius of the antenna in the model, in mm.</param>
        /// <param name="wireRadius">The wire radius of the wire consisting the antenna, in mm.</param>
        /// <param name="numberOfTurns">The number of turns in the antenna winding.</param>
        /// <returns></returns>
        public static Antenna CreateAntenna(Circuit circuit, Antenna.AntennaPresence presence, double[] legs, double antennaRadius = 30, double wireRadius = 0.5, double numberOfTurns = 3)
        {
            // Create a transmission antenna and a associated local circuit.
            const Antenna.AntennaType type = Antenna.AntennaType.Loop;
            const Antenna.AntennaWinding winding = Antenna.AntennaWinding.Litz;
            const Antenna.DimensionalUnits dimension = Antenna.DimensionalUnits.mm;
            const Antenna.InductanceMethod inductanceMethod = Antenna.InductanceMethod.Grover;
            const Antenna.QualityFactorMethod qualityMethod = Antenna.QualityFactorMethod.Zeirhofer;
            const Antenna.AntennaMaterial material = Antenna.AntennaMaterial.Copper;
            // Measured physical values of the transmission antenna.
            //var legsTx = new double[] { 20, 20, 50 };
            //var legsRx = new double[] { 20, 20 };
            // Initialize with an application of circuital properties
            Circuit.Voltage = circuit.CircuitVoltage();
            Circuit.Current = circuit.CircuitCurrent();
            Antenna.ResonanceParameters.DipoleDistance = 0.020;
            Antenna.ResonanceParameters.ObservationDistance = 0.250;
            Antenna.ResonanceParameters.InferredFieldHeight = 0.060;
            if (presence == Antenna.AntennaPresence.Transmitter)
            {
                return new Antenna(antennaRadius, wireRadius, numberOfTurns, legs, dimension, material, type, winding, presence, circuit, inductanceMethod, qualityMethod);
            }
            if (presence == Antenna.AntennaPresence.Receiver)
            {
                return new Antenna(antennaRadius, wireRadius, numberOfTurns, legs, dimension, material, type, winding, presence, circuit, inductanceMethod, qualityMethod);
            } 
            // Won't reach here, but initialize a blank antenna.
            return new Antenna();
        }

        public class TupleTwo<T1, T2> : List<Tuple<T1, T2>>
        {
            public void Add(T1 item, T2 item2)
            {
                Add(new Tuple<T1, T2>(item, item2));
            }
        }
        public class TupleThree<T1, T2, T3> : List<Tuple<T1, T2, T3>>
        {
            public void Add(T1 item, T2 item2, T3 item3)
            {
                Add(new Tuple<T1, T2, T3>(item, item2, item3));
            }
        }
        public class TupleFour<T1, T2, T3, T4> : List<Tuple<T1, T2, T3, T4>>
        {
            public void Add(T1 item, T2 item2, T3 item3, T4 item4)
            {
                Add(new Tuple<T1, T2, T3, T4>(item, item2, item3, item4));
            }
        }
    }
}
