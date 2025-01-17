using System;
using CircuitCalculator.Classes.Calculus;

namespace CircuitCalculator
{
    public class Program
    {
        // Sample functions to be integrated.
        public static double f1(double x)
        {
            return x * x;
        }

        public static double f2(double x)
        {
            return x * x * x;
        }

        public static double f3(double x)
        {
            return Math.Sin(x) * Math.Cos(x * x) + Math.PI;
        }

        public static double f9(double t, double y)
        {
            return -y + t + 1;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine(Integral.integral(new Integral.Function(f1), 1, 10, 200));
            //Console.WriteLine(Integral.integral(new Integral.Function(f2), 1, 10, 200));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 20000));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 2000));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 200));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 20));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 10));
            Console.WriteLine(SimpsonIntegrator.Integrate(new SimpsonIntegrator.Function(f3), 1, 10, 5));
            //Console.WriteLine(Runge_Kutta.runge(0, 1, 1, .1f, new Runge_Kutta.Function(Test.f1)));
            Console.Write("Press return to exit.");
            Console.ReadLine();
        }
    }
}
