using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using LabSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scilab.Tests
{
    [TestClass]
    public class ScilabConnectionTests
    {
        [TestMethod]
        public void TestMatlabInterface()
        {
            var sin = new double[100];

            for (int i = 0; i < sin.Length; i++)
            {
                sin[i] = Math.Sin(i / 10.0);
            }
            using (Engine eng = Engine.Open())
            {
                eng.SetVariable("sin", sin);
                eng.Eval("plot(sin); figure(gcf)");
            }
        }

        [TestMethod]
        public void StartScilabTest()
        {
            var mOsCilab = new Scilab(true);

            CustomSamples.ExampleReadwriteMatrixOfDouble(mOsCilab);

            Samples.ExampleReadwriteMatrixOfString(mOsCilab);

            Samples.ExampleReadwriteMatrixOfBoolean(mOsCilab);

            Samples.ExampleReadwriteMatrixOfInt(mOsCilab);

            Samples.ExampleReadwriteComplexMatrixOfDouble(mOsCilab);

            Samples.ExampleDoplot3D(mOsCilab);

            var hold = "";
        }
    }

    /// <summary>
    /// Test methods container.
    /// </summary>
    public class CustomSamples
    {
        public static void SendCommand(Scilab objScilab)
        {
            // Test Matlab script:

        }
        private List<string> LoadParameters()
        {
            var tempParams = new List<string>();
            const string fileName = @"C:\aeonWorkingDirectory\bph\calculators\thesis\Scilab\commands\rwg1.txt";

            TextReader reader = new StreamReader(fileName);

            try
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    tempParams.Add(str);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (true)
                    reader.Close();
            }

            return tempParams;
        }

        public static void ExampleReadwriteMatrixOfDouble(Scilab objScilab)
        {
            // Send a command to scilab
            // Here , we want to display SCI variable
            objScilab.SendScilabJob("disp(\'SCI = \');");
            objScilab.SendScilabJob("disp(SCI);");

            var a = new double[] { 1, 2, 3, 4, 5, 6 };
            const int mA = 2;
            const int nA = 3;

            // Write a matrix of double named in scilab
            objScilab.CreateNamedMatrixOfDouble("A", mA, nA, a);

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'A =\');");
            objScilab.SendScilabJob("disp(A);");

            if (objScilab.GetNamedVarType("A") == (int)ScilabType.SciMatrix)
            {
                // Show in the output (debug) window of Visual Studio.
                Debug.Write("A is a matrix of double");
            }
            objScilab.SendScilabJob("B = A + 1;");

            // get dimensions of a named matrix of double
            var dimB = objScilab.getNamedVarDimension("B");

            // get named matrix of double
            var b = objScilab.readNamedMatrixOfDouble("B");

            // display matrix of double from C#
            Debug.Write(" ");
            Debug.Write("(C#) B = ");

            for (int i = 0; i < dimB[0]; i++)
            {
                for (var j = 0; j < dimB[1]; j++)
                {
                    Debug.Write(b[j * dimB[0] + i] + " ");
                }

                Debug.Write(" ");
            }

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'B =\');");
            objScilab.SendScilabJob("disp(B);");
        }
    }
    /// <summary>
    /// Test methods container.
    /// </summary>
    public class Samples
    {
        public static void SendCommand(Scilab objScilab)
        {
            // Test Matlab script:

        }
        private List<string> LoadParameters()
        {
            var tempParams = new List<string>();

            string fileName = @"C:\aeonWorkingDirectory\bph\calculators\thesis\Scilab\commands\rwg1.txt";

            TextReader reader = new StreamReader(fileName);

            try
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    tempParams.Add(str);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (true)
                    reader.Close();
            }

            return tempParams;
        }

        public static void ExampleReadwriteMatrixOfDouble(Scilab objScilab)
        {
            // Send a command to scilab
            // Here , we want to display SCI variable
            objScilab.SendScilabJob("disp(\'SCI = \');");
            objScilab.SendScilabJob("disp(SCI);");

            var a = new double[] { 1, 2, 3, 4, 5, 6 };
            const int mA = 2;
            const int nA = 3;

            // Write a matrix of double named in scilab
            objScilab.CreateNamedMatrixOfDouble("A", mA, nA, a);

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'A =\');");
            objScilab.SendScilabJob("disp(A);");

            if (objScilab.GetNamedVarType("A") == (int)ScilabType.SciMatrix)
            {
                Console.WriteLine("A is a matrix of double");
            }
            objScilab.SendScilabJob("B = A + 1;");

            // get dimensions of a named matrix of double
            var dimB = objScilab.getNamedVarDimension("B");

            // get named matrix of double
            var b = objScilab.readNamedMatrixOfDouble("B");

            // display matrix of double from C#
            Console.WriteLine("");
            Console.WriteLine("(C#) B =");
            for (int i = 0; i < dimB[0]; i++)
            {
                for (var j = 0; j < dimB[1]; j++)
                {
                    Console.Write(b[j * dimB[0] + i] + " ");
                }

                Console.WriteLine("");
            }

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'B =\');");
            objScilab.SendScilabJob("disp(B);");
        }
        
        public static void ExampleReadwriteMatrixOfString(Scilab objScilab)
        {
            var strA = new[] { "Scilab", "The", "open",
                                        "source", "for", "numerical",
                                        "computation" , ":", ")"};
            const int mstrA = 3;
            const int nstrA = 3;

            // Write a matrix of string named in scilab
            objScilab.CreateNamedMatrixOfString("string_A", mstrA, nstrA, strA);

            // display matrix of string by scilab
            objScilab.SendScilabJob("disp(\'string_A =\');");
            objScilab.SendScilabJob("disp(string_A);");

            if (objScilab.GetNamedVarType("string_A") == (int)ScilabType.SciStrings)
            {
                Console.WriteLine("string_A is a matrix of strings");
            }
            objScilab.SendScilabJob("string_B = convstr(string_A,\'u\');");

            // get dimensions of a named matrix of string
            var dimstrB = objScilab.getNamedVarDimension("string_B");

            // get named matrix of string
            var strB = objScilab.readNamedMatrixOfString("string_B");

            Console.WriteLine("");
            Console.WriteLine("(C#) strB =");
            for (int i = 0; i < dimstrB[0]; i++)
            {
                for (int j = 0; j < dimstrB[1]; j++)
                {
                    Console.Write(strB[j * dimstrB[0] + i] + " ");
                }

                Console.WriteLine("");
            }

            // display matrix of string by scilab
            objScilab.SendScilabJob("disp(\'string_B =\');");
            objScilab.SendScilabJob("disp(string_B);");
        }
        /// <summary>
        /// Examples the readwrite matrix of boolean.
        /// </summary>
        /// <param name="objScilab">The scilab connection object.</param>
        public static void ExampleReadwriteMatrixOfBoolean(Scilab objScilab)
        {
            //=============================================================================
            var bA = new[] { false, true, false,
                                           true, false, true};
            const int mbA = 2;
            const int nbA = 3;

            // Write a matrix of string named in scilab
            objScilab.CreateNamedMatrixOfBoolean("boolean_A", mbA, nbA, bA);

            // display matrix of string by scilab
            objScilab.SendScilabJob("disp(\'boolean_A =\');");
            objScilab.SendScilabJob("disp(boolean_A);");
            //=============================================================================
            // check if av
            if (objScilab.ExistNamedVariable("boolean_A"))
            {
                Console.WriteLine("boolean_A exists in scilab");
            }

            if (objScilab.ExistNamedVariable("boolean_B") == false)
            {
                Console.WriteLine("boolean_B does not exist in scilab");
            }
            //=============================================================================
            if (objScilab.GetNamedVarType("boolean_A") == (int)ScilabType.SciBoolean)
            {
                Console.WriteLine("boolean_A is a matrix of boolean");
            }
            //=============================================================================
            objScilab.SendScilabJob("boolean_B = ~boolean_A;");
            // get dimensions of a named matrix of boolean
            var dimbB = objScilab.getNamedVarDimension("boolean_B");

            // get named matrix of boolean
            var bB = objScilab.GetNamedMatrixOfBoolean("boolean_B");

            Console.WriteLine("");
            Console.WriteLine("(C#) bB =");
            for (int i = 0; i < dimbB[0]; i++)
            {
                for (int j = 0; j < dimbB[1]; j++)
                {
                    Console.Write(bB[j * dimbB[0] + i] + " ");
                }

                Console.WriteLine("");
            }

            // display matrix of string by scilab
            objScilab.SendScilabJob("disp(\'boolean_B =\');");
            objScilab.SendScilabJob("disp(boolean_B);");
            //=============================================================================
        }
        /// <summary>
        /// Example of doplot3D.
        /// </summary>
        /// <param name="objScilab">The scilab connection object.</param>
        public static void ExampleDoplot3D(Scilab objScilab)
        {
            objScilab.SendScilabJob("plot3d()");
            while (objScilab.HaveAGraph())
            {
                objScilab.doEvent();
            }
        }
        
        public static void ExampleReadwriteMatrixOfInt(Scilab objScilab)
        {
            
            var a = new[] { 1, 2, 3, 4, 5, 6 };
            const int mA = 2;
            const int nA = 3;

            // Write a matrix of double named in scilab
            objScilab.CreateNamedMatrixOfInt32("int32_A", mA, nA, a);

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'int32_A =\');");
            objScilab.SendScilabJob("disp(int32_A);");
            
            if (objScilab.GetNamedVarType("int32_A") == (int)ScilabType.SciInts)
            {
                Console.WriteLine("int32_A is a matrix of int(32)");
            }
            
            objScilab.SendScilabJob("int32_B = int32_A + 1;");

            // get dimensions of a named matrix of double
            var dimB = objScilab.getNamedVarDimension("int32_B");

            // get named matrix of double
            var b = objScilab.ReadNamedMatrixOfInt32("int32_B");

            // display matrix of double from C#
            Console.WriteLine("");
            Console.WriteLine("(C#) int32_B =");
            for (var i = 0; i < dimB[0]; i++)
            {
                for (int j = 0; j < dimB[1]; j++)
                {
                    Console.Write(b[j * dimB[0] + i] + " ");
                }

                Console.WriteLine("");
            }

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'int32_B =\');");
            objScilab.SendScilabJob("disp(int32_B);");
            
        }
        
        public static void ExampleReadwriteComplexMatrixOfDouble(Scilab objScilab)
        {
            var realPartA = new double[] { 1, 2, 3, 4, 5, 6 };
            var imagPartA = new double[] { 6, 5, 4, 3, 2, 1 };
            const int mA = 2;
            const int nA = 3;

            // Write a matrix of double named in scilab
            objScilab.CreateNamedComplexMatrixOfDouble("cplx_A", mA, nA, realPartA, imagPartA);

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'cplx_A =\');");
            objScilab.SendScilabJob("disp(cplx_A);");
            objScilab.SendScilabJob("cplx_B = cplx_A * 2;");

            // get dimensions of a named matrix of double
            var dimB = objScilab.getNamedVarDimension("cplx_B");

            // get named matrix of double
            var realPartB = objScilab.ReadNamedComplexMatrixOfDoubleRealPart("cplx_B");
            var imagPartB = objScilab.ReadNamedComplexMatrixOfDoubleImgPart("cplx_B");

            // display matrix of double from C#
            Console.WriteLine("");
            Console.WriteLine("(C#) cplx_B =");
            for (var i = 0; i < dimB[0]; i++)
            {
                for (var j = 0; j < dimB[1]; j++)
                {
                    Console.Write(realPartB[j * dimB[0] + i] + " + i *" + imagPartB[j * dimB[0] + i] + " ");
                }

                Console.WriteLine("");
            }

            // display matrix of double by scilab
            objScilab.SendScilabJob("disp(\'cplx_B =\');");
            objScilab.SendScilabJob("disp(cplx_B);");
        }
    }
}
