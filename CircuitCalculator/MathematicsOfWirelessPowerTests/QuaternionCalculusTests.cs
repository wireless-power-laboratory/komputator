using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuaternionObjects;
using System;

namespace CircuitCalculatorTests
{
    [TestClass]
    public class QuaternionCalculusTests
    {
        double C = 4 + Math.PI * Math.Pow(10, -7);

        const double w1 = 5;
        const double x1 = 45;
        const double y1 = 45;
        const double z1 = 30;

        const double w2 = 2;
        const double x2 = 60;
        const double y2 = 60;
        const double z2 = 45;

        double X1 = Math.Sin(x1) * Math.Cos(x1);
        double Y1 = Math.Sin(y1) * Math.Cos(y1);
        double Z1 = Math.Sin(z1) * Math.Cos(z1);

        double X2 = Math.Sin(x2) * Math.Cos(x2);
        double Y2 = Math.Sin(y2) * Math.Cos(y2);
        double Z2 = Math.Sin(z2) * Math.Cos(z2);

        public Quaternion Q1 { get; set; }
        public Quaternion Q2 { get; set; }

        public QuaternionCalculusTests()
        {
            Q1 = new Quaternion(w1, X1, Y1, Z1);
            Q2 = new Quaternion(w2, X2, Y2, Z2);
        }

        [TestMethod]
        public void FirstChallengeTest()
        {
            var first = 2 * C;
            var numerator = Q1 * Q2 * Q1.UnitVector();
            var denominator = Math.Pow(Q1.Tensor(), 0.5);
            var outside = Q1.Differential() * Q2.Differential() * Math.Pow(Q1.Tensor(), 0.5);
            // Do your homework and sort the problem!
            var hold = 0;
        }

        [TestMethod]
        public void CheckCommutationRule()
        {
            // Check the rule.
            var Q7 = Q1.Multiply(Q2);
            var Q8 = Q2.Multiply(Q1);
            Assert.AreEqual(Q7, Q8);// This won't work until the assert is made smarter.
        }

        [TestMethod]
        public void SanityTest()
        {
            // Check the operations.
            var Q3 = Q1.Norm();
            var Q4 = Q2.Tensor();
            var Q5 = Q2.Conjugate();
            var Q6 = Q1.Multiply(Q2);

            // What is the assert here?
        }

        [TestMethod]
        public void IsUnitVector()
        {
            var Q8 = Q1.Copy();

            if (!Q1.IsUnitVector())
            {
                Q8.Norm();
            }
            Assert.IsTrue(Q8.IsUnitVector());
        }

        [TestMethod]
        public void TaitAmpereExpressionTest()
        {

        }
    }
}
