using System;
using System.Threading;

namespace Scilab
{
    /* Scilab Types */
    public enum ScilabType
    {
        SciMatrix = 1,
        SciPoly = 2,
        SciBoolean = 4,
        SciSparse = 5,
        SciBooleanSparse = 6,
        SciMatlabSparse = 7,
        SciInts = 8,
        SciHandles = 9,
        SciStrings = 10,
        SciUFunction = 11,
        SciCFunction = 13,
        SciLib = 14,
        SciList = 15,
        SciTlist = 16,
        SciMlist = 17,
        SciPointer = 128,
        SciImplicitPoly = 129,
        SciIntrinsicFunction = 130
    };

    public sealed class Scilab
    {
        static Scilab _instance;
        static readonly object Padlock = new object();
        private readonly Boolean _withGraphics;

        /// <summary>
        /// Constructor, initialize scilab engine.
        /// </summary>
        public Scilab()
        {
            // Disable TCL/TK and Java graphic interfaces
            ScilabWrapper.DisableInteractiveMode();
            _withGraphics = false;

            // start Scilab engine configurated without java
            ScilabWrapper.StartScilab(null, null, null);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Scilab"/> class.
        /// </summary>
        /// <param name="bWithGraphics">if set to <c>true</c> [b with graphics].</param>
        public Scilab(Boolean bWithGraphics)
        {
            // Disable TCL/TK and Java graphic interfaces
            if (bWithGraphics == false)
            {
                ScilabWrapper.DisableInteractiveMode();
                _withGraphics = false;
            }
            else
            {
                _withGraphics = true;
            }

            // start Scilab engine
            ScilabWrapper.StartScilab(null, null, null);
        }
        /// <summary>
        /// Singleton: Only one instance of Scilab can be launch thread safe
        /// </summary>
        public static Scilab Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Scilab();
                    }
                    return _instance;
                }
            }
        }
        
        /// <summary>
        /// Destructor
        /// </summary>
        ~Scilab()
        {
            // freed by O.S
            //ScilabWrapper.TerminateScilab(null);
        }
        /// <summary>
        /// Send a job to scilab
        /// </summary>
        /// <param name="command">command to send to scilab</param>
        /// <returns>error code operation, 0 : OK</returns>
        public int SendScilabJob(string command)
        {
            return ScilabWrapper.SendScilabJob(command);
        }
        /// <summary>
        ///  Get the last error code
        /// </summary>
        public int GetLastErrorCode()
        {
            return ScilabWrapper.GetLastErrorCode();
        }
        /// <summary>
        /// Write a named matrix of double in scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <param name="iRows"> Number of row</param>
        /// <param name="iCols"> Number of column</param>
        /// <param name="matrixDouble"> pointer on data</param>
        /// <returns> if the operation successes (0) or not ( !0 )</returns>
        public int CreateNamedMatrixOfDouble(string matrixName, int iRows, int iCols, double[] matrixDouble)
        {
            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.createNamedMatrixOfDouble(ptrEmpty, matrixName, iRows, iCols, matrixDouble);
            return SciErr.iErr;
        }
        /// <summary>
        /// Write a named matrix of string in scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <param name="iRows"> Number of row</param>
        /// <param name="iCols"> Number of column</param>
        /// <param name="matrixDouble"> pointer on data</param>
        /// <returns> if the operation successes (0) or not ( !0 )</returns>
        public int CreateNamedMatrixOfString(string matrixName, int iRows, int iCols, string[] matrixString)
        {
            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.createNamedMatrixOfString(ptrEmpty, matrixName, iRows, iCols, matrixString);
            return SciErr.iErr;
        }
        /// <summary>
        /// Write a named matrix of boolean in Scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <param name="iRows"> Number of row</param>
        /// <param name="iCols"> Number of column</param>
        /// <param name="matrixBoolean"> pointer on data</param>
        /// <returns> if the operation successes (0) or not ( !0 )</returns>
        public int CreateNamedMatrixOfBoolean(string matrixName, int iRows, int iCols, Boolean[] matrixBoolean)
        {
            int[] matrixInt = new int[matrixBoolean.Length];
            for (int i = 0; i < matrixBoolean.Length; i++)
            {
                if (matrixBoolean[i] == true)
                {
                    matrixInt[i] = 1;
                }
                else
                {
                    matrixInt[i] = 0;
                }
            }
            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.createNamedMatrixOfBoolean(ptrEmpty, matrixName, iRows, iCols, matrixInt);
            return SciErr.iErr;
        }
        /// <summary>
        /// Write a named matrix of int(32) in Scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <param name="iRows"> Number of row</param>
        /// <param name="iCols"> Number of column</param>
        /// <param name="matrixInt"> pointer on data</param>
        public int CreateNamedMatrixOfInt32(string matrixName, int iRows, int iCols, int[] matrixInt)
        {
            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.createNamedMatrixOfInteger32(ptrEmpty, matrixName, iRows, iCols, matrixInt);
            return SciErr.iErr;
        }

        /// <summary>
        /// Write a named matrix of complex double in scilab.
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <param name="iRows">Number of row</param>
        /// <param name="iCols">Number of column</param>
        /// <param name="matrixRealPart">real part</param>
        /// <param name="matrixImagPart">imag part</param>
        /// <returns></returns>
        public int CreateNamedComplexMatrixOfDouble(string matrixName,
                                                int iRows, int iCols,
                                                double[] matrixRealPart,
                                                double[] matrixImagPart)
        {
            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.createNamedComplexMatrixOfDouble(ptrEmpty, matrixName,
                                                    iRows, iCols,
                                                    matrixRealPart,
                                                    matrixImagPart);
            return SciErr.iErr;
        }
        /// <summary>
        /// Read a named matrix of double from scilab.
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>a matrix of double from scilab. If Variable name does not exist returns null</returns>
        public unsafe double[] readNamedMatrixOfDouble(string matrixName)
        {
            int iRows = 0;
            int iCols = 0;

            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedMatrixOfDouble(ptrEmpty, matrixName, &iRows, &iCols, null);

            if (iRows * iCols > 0)
            {
                double[] matrixDouble = new double[iRows * iCols];

                // get values in matrixDouble
                SciErr = ScilabWrapper.readNamedMatrixOfDouble(ptrEmpty, matrixName, &iRows, &iCols, matrixDouble);
                if (SciErr.iErr != 0) return null;
                return matrixDouble;
            }
            return null;
        }
        /// <summary>
        /// Get dimensions of a named matrix in scilab.
        /// </summary>
        /// <returns>a int array. 
        /// if variable name does not exist dimensions are null </returns>
        public unsafe int[] getNamedVarDimension(string matrixName)
        {
            int[] iDim = null;
            int iRows = 0;
            int iCols = 0;

            System.IntPtr ptrEmpty = new System.IntPtr();
            ScilabWrapper.ApiErr SciErr = ScilabWrapper.getNamedVarDimension(ptrEmpty, matrixName, &iRows, &iCols);
            if (SciErr.iErr == 0)
            {
                iDim = new int[2];
                iDim[0] = iRows;
                iDim[1] = iCols;
            }
            return iDim;
        }
        /// <summary>
        /// Read a named matrix of string from scilab.
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>a matrix of string from scilab. If Variable name does not exist returns null</returns>
        public unsafe string[] readNamedMatrixOfString(string matrixName)
        {
            string[] matrixString = null;

            int[] iDim = getNamedVarDimension(matrixName);

            if (iDim != null)
            {
                int iRows = iDim[0];
                int iCols = iDim[1];

                // we allocate lengthmatrixString
                int[] lengthmatrixString = new int[iRows * iCols];

                System.IntPtr ptrEmpty = new System.IntPtr();

                // we get length of strings
                ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedMatrixOfString(ptrEmpty, matrixName, 
                                        &iRows, &iCols, 
                                        lengthmatrixString, null);

                // we allocate each string
                matrixString = new string[iRows * iCols];
                for (int i = 0; i < iRows * iCols; i++)
                {
                    matrixString[i] = new string(' ', lengthmatrixString[i]);
                }

                // we get strings from scilab
                SciErr = ScilabWrapper.readNamedMatrixOfString(ptrEmpty, matrixName, 
                                                &iRows, &iCols,
                                                lengthmatrixString, 
                                                matrixString);
            }
            return matrixString;
        }
        /// <summary>
        /// Read a named matrix of boolean from Scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>a matrix of boolean from scilab. If Variable name does not exist returns null</returns>
        public unsafe Boolean[] GetNamedMatrixOfBoolean(string matrixName)
        {
            Boolean[] matrixBoolean = null;
            int[] iDim = getNamedVarDimension(matrixName);

            if (iDim != null)
            {
                int iRows = iDim[0];
                int iCols = iDim[1];
                int[] matrixInt = new int[iRows * iCols];

                System.IntPtr ptrEmpty = new System.IntPtr();

                // get values in matrixDouble
                ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedMatrixOfBoolean(ptrEmpty, matrixName, 
                                                            &iRows, &iCols, 
                                                            matrixInt);

                if (matrixInt != null)
                {
                    matrixBoolean = new Boolean[iRows * iCols];
                    for (int i = 0; i < iRows * iCols; i++)
                    {
                        if (matrixInt[i] == 1)
                        {
                            matrixBoolean[i] = true;
                        }
                        else
                        {
                            matrixBoolean[i] = false;
                        }
                    }
                }
            }
            return matrixBoolean;
        }
        /// <summary>
        /// Read a named matrix of complex double in Scilab (Real part)
        /// </summary>
        /// <param name="matrixName">variable name</param>
        /// <returns> real part. If Variable name does not exist returns null</returns>
        public unsafe double[] ReadNamedComplexMatrixOfDoubleRealPart(string matrixName)
        {
            double[] dRealPart = null;
            int[] iDim = getNamedVarDimension(matrixName);
            if (iDim != null)
            {
                int iRows = iDim[0];
                int iCols = iDim[1];

                double[] dImagPart = new double[iRows * iCols];
                dRealPart = new double[iRows * iCols];

                System.IntPtr ptrEmpty = new System.IntPtr();

                ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedComplexMatrixOfDouble(ptrEmpty, matrixName,
                                           &iRows, &iCols,
                                           dRealPart,
                                           dImagPart);
            }
            return dRealPart;
        }
        /// <summary>
        /// Read a named matrix of complex double in Scilab (Imag part)
        /// </summary>
        /// <param name="matrixName">variable name</param>
        /// <returns> img part. If Variable name does not exist returns null</returns>
        public unsafe double[] ReadNamedComplexMatrixOfDoubleImgPart(string matrixName)
        {
            double[] dImagPart = null;
            int[] iDim = getNamedVarDimension(matrixName);
            if (iDim != null)
            {
                int iRows = iDim[0];
                int iCols = iDim[1];

                double[] dRealPart = new double[iRows * iCols];
                dImagPart = new double[iRows * iCols];

                System.IntPtr ptrEmpty = new System.IntPtr();

                ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedComplexMatrixOfDouble(ptrEmpty, matrixName,
                                           &iRows, &iCols,
                                           dRealPart,
                                           dImagPart);
            }
            return dImagPart;
        }
        /// <summary>
        /// Read a named matrix of int(32) in Scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>a matrix of int(32) from scilab. If Variable name does not exist returns null</returns>
        public unsafe int[] ReadNamedMatrixOfInt32(string matrixName)
        {
            int[] matrixInt = null;
            int[] iDim = getNamedVarDimension(matrixName);
            if (iDim != null)
            {
                int iRows = iDim[0];
                int iCols = iDim[1];

                // we allocate matrixInt array
                matrixInt = new int[iRows * iCols];

                System.IntPtr ptrEmpty = new System.IntPtr();

                // get values in matrixInt
                ScilabWrapper.ApiErr SciErr = ScilabWrapper.readNamedMatrixOfInteger32(ptrEmpty, matrixName, &iRows, &iCols, matrixInt);
            }
            return matrixInt;
        }
        //=============================================================================
        /// <summary>
        /// get scilab type of named matrix
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>scilab type (see enum ScilabType)</returns>
        public unsafe int GetNamedVarType(string matrixName)
        {
            int iType = 0;
            System.IntPtr ptrEmpty = new System.IntPtr();

            ScilabWrapper.ApiErr SciErr = ScilabWrapper.getNamedVarType(ptrEmpty, matrixName, &iType);
            if (SciErr.iErr == 0) return iType;
            return 0;
        }
        //=============================================================================
        /// <summary>
        /// Detect if a variable name exists in Scilab
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns> true if exists</returns>
        public unsafe Boolean ExistNamedVariable(string matrixName)
        {
            int* piAdress = null;
            System.IntPtr ptrEmpty = new System.IntPtr();

            ScilabWrapper.ApiErr SciErr = ScilabWrapper.getVarAddressFromName(ptrEmpty, matrixName, &piAdress);
            if (SciErr.iErr == 0) return true;
            return false;
        }
        //=============================================================================
        /// <summary>
        /// Execute a scilab script .sce
        /// </summary>
        /// <param name="scriptFilename">the path to the .sce file</param>
        /// <returns>error code operation, 0 : OK</returns>
        public int ExecScilabScript(String scriptFilename)
        {
            return ScilabWrapper.SendScilabJob("exec('" + scriptFilename + "');");
        }
        //=============================================================================
        /// <summary>
        /// Detect if a Scilab graphic window is opened
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean HaveAGraph()
        {
            if (_withGraphics)
            {
                int ierr = ScilabWrapper.sciHasFigures();
                if (ierr == 1) return true;
            }
            return false;
        }
        //=============================================================================
        /// <summary>
        /// do a scilab event
        /// parser need to run to do a event
        /// </summary>
        /// <returns>error code operation, 0 : OK</returns>
        public int doEvent()
        {
            // do a pause (we do not want 100% CPU used)
            // ugly but it works ...
            Thread.Sleep(1);
            // do a loop of parser
            return SendScilabJob("");
        }
        //=============================================================================
        /// <summary>
        /// get scilab type of named matrix
        /// </summary>
        /// <param name="matrixName"> variable name</param>
        /// <returns>scilab type (see enum ScilabType)</returns>
        public int isNamedVarComplex(string matrixName)
        {
            System.IntPtr ptrEmpty = new System.IntPtr();
            return ScilabWrapper.isNamedVarComplex(ptrEmpty, matrixName);
        }
        //=============================================================================
    }
}
//=============================================================================
