using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Scilab
{
    public class ScilabWrapper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public unsafe struct ApiCtx  
        {
            public String pstName; /**< Function name */
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public unsafe struct ApiErr
        {
            public int iErr;
            public int iMsgCount;
            public fixed int pstructMsg[5];
        }

        //==========================Scilab v5.3.3 32-bit===========================
        private const string CallScilabDll = @"C:\Program Files\scilab-5.3.3\bin\call_scilab.dll";
        private const string ApiScilabDll = @"C:\Program Files\scilab-5.3.3\bin\api_scilab.dll";
        private const string GraphicsDll = @"C:\Program Files\scilab-5.3.3\bin\graphics.dll";
        private const string OutputStreamDll = @"C:\Program Files\scilab-5.3.3\bin\scioutput_stream.dll";
        private const string MyCommonLib = @"C:\Program Files\scilab-5.3.3\bin\LibScilab.dll";
        //==========================Scilab v5.3.3===============================
        //private const string CallScilabDll = @"C:\Program Files (x86)\scilab-5.3.3\bin\call_scilab.dll";
        //private const string ApiScilabDll = @"C:\Program Files (x86)\scilab-5.3.3\bin\api_scilab.dll";
        //private const string GraphicsDll = @"C:\Program Files (x86)\scilab-5.3.3\bin\graphics.dll";
        //private const string OutputStreamDll = @"C:\Program Files (x86)\scilab-5.3.3\bin\scioutput_stream.dll";
        //private const string MyCommonLib = @"C:\Program Files (x86)\scilab-5.3.3\bin\LibScilab.dll";
        //==========================Scilab v5.4.0, new interface================================= works ok but seems same
        //private const string CallScilabDll = @"C:\Program Files (x86)\scilab-5.4.0\bin\call_scilab.dll";
        //private const string ApiScilabDll = @"C:\Program Files (x86)\scilab-5.4.0\bin\api_scilab.dll";
        //private const string GraphicsDll = @"C:\Program Files (x86)\scilab-5.4.0\bin\graphics.dll";
        //private const string OutputStreamDll = @"C:\Program Files (x86)\scilab-5.4.0\bin\scioutput_stream.dll";
        //private const string MyCommonLib = @"C:\Program Files (x86)\scilab-5.4.0\bin\LibScilab.dll";
        //=============================================================================
        /// <summary>
        /// import SendScilabJob from C (see call_scilab.h)
        /// </summary>
        [DllImport(CallScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendScilabJob([In]String job);
        /// <summary>
        /// import StartScilab from C (see call_scilab.h)
        /// </summary>
        [DllImport(CallScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartScilab([In] String scIpath, [In] String scilabStartup, [In] Int32[] stacksize);

        [DllImport(@"C:\Program Files\scilab-5.4.0\bin\LibScilab.dll", EntryPoint = "StartScilab", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartScilab54(StringBuilder scIpath, StringBuilder scilabStartup, int stacksize);

        [DllImport(MyCommonLib, EntryPoint = "StartScilab", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MyStartScilab(StringBuilder scIpath, StringBuilder scilabStartup, int stacksize);
        /// <summary>
        /// import TerminateScilab from C (see call_scilab.h)
        /// </summary>
        [DllImport(CallScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TerminateScilab([In] String scilabQuit);
        /// <summary>
        /// import DisableInteractiveMode from C (see call_scilab.h)
        /// </summary>
        [DllImport(CallScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableInteractiveMode();
        /// <summary>
        /// import createNamedMatrixOfString from C (see api_string.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern ApiErr createNamedMatrixOfString([In]IntPtr pvApiCtx, [In] String pstName,
                                                            [In] int iRows, [In] int iCols,
                                                            [In] String[] pstStrings);
        /// <summary>
        /// import createNamedMatrixOfWideString from C (see api_string.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern ApiErr createNamedMatrixOfWideString([In]IntPtr pvApiCtx,
                                                            [In] String _pstName,
                                                            [In] int _iRows, [In] int _iCols,
                                                            [In] String[] _pstStrings);
        /// <summary>
        /// import createNamedMatrixOfDouble from C (see api_double.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern ApiErr createNamedMatrixOfDouble([In]IntPtr pvApiCtx, [In] String _pstName,
                                                            [In] int _iRows, [In] int _iCols,
                                                            [In] double[] _pdblReal);
        /// <summary>
        /// import createNamedMatrixOfBoolean from C (see api_boolean.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern ApiErr createNamedMatrixOfBoolean([In]IntPtr pvApiCtx, [In] String _pstName,
                                                            [In] int _iRows, [In] int _iCols,
                                                            [In] int[] _piBool);
        /// <summary>
        /// import createNamedMatrixOfInteger32 from C (see api_int.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr createNamedMatrixOfInteger32([In]IntPtr pvApiCtx, [In] String _pstName,
                                                           [In] int _iRows, [In] int _iCols,
                                                           [In] int[] _piData);
        /// <summary>
        /// import createNamedComplexMatrixOfDouble from C (see api_double.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr createNamedComplexMatrixOfDouble([In]IntPtr pvApiCtx, [In] String _pstName,
                                                            [In] int _iRows, [In] int _iCols,
                                                            [In] double[] _pdblReal,
                                                            [In] double[] _pdblImg);
        /// <summary>
        /// import readNamedMatrixOfString from C (see api_string.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedMatrixOfString([In]IntPtr pvApiCtx, [In] String _pstName,
                                                          [Out]  Int32* _piRows, [Out]  Int32* _piCols,
                                                          [In, Out] int[] _piLength,
                                                          [In, Out] String[] _pstStrings);
        /// <summary>
        /// import readNamedMatrixOfWideString from C (see api_string.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedMatrixOfWideString([In]IntPtr pvApiCtx, [In] String _pstName,
                                                          [Out]  Int32* _piRows, [Out]  Int32* _piCols,
                                                          [In, Out] int[] _piLength,
                                                          [In, Out] String[] _pstStrings);
        /// <summary>
        /// import readNamedMatrixOfDouble from C (see api_double.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedMatrixOfDouble([In]IntPtr pvApiCtx, [In] String _pstName,
                                                          [Out] Int32* _piRows, [Out] Int32* _piCols,
                                                          [In, Out] Double[] _pdblReal);
        /// <summary>
        /// import readNamedMatrixOfBoolean from C (see api_boolean.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedMatrixOfBoolean([In]IntPtr pvApiCtx, [In] String _pstName,
                                                          [Out] Int32* _piRows, [Out] Int32* _piCols,
                                                          [In, Out] int[] _piBool);
        /// <summary>
        /// import readNamedMatrixOfInteger32 from C (see api_int.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedMatrixOfInteger32([In]IntPtr pvApiCtx, [In] String _pstName,
                                                          [Out] Int32* _piRows, [Out] Int32* _piCols,
                                                          [In, Out] int[] _piData);
        /// <summary>
        /// import readNamedComplexMatrixOfDouble from C (see api_double.h)
        /// </summary>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr readNamedComplexMatrixOfDouble([In]IntPtr pvApiCtx, [In] String _pstName,
                                                        [Out] Int32* _piRows, [Out] Int32* _piCols,
                                                        [In, Out] double[] _pdblReal,
                                                        [In, Out] double[] _pdblImg);
        //=============================================================================
        /// <summary>
        /// get Variable Adress in scilab stack from name
        /// used for getNamedMatrixType (internal)
        /// </summary>
        /// <param name="_pstName">variable name</param>
        /// <param name="_piAddress"> stack address</param>
        /// <returns>1 if ok</returns>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr getVarAddressFromName([In]IntPtr pvApiCtx, [In] String _pstName,
                                                               [Out] Int32** _piAddress);
        //=============================================================================
        /// <summary>
        /// get Variable type in scilab stack from name
        /// </summary>
        /// <param name="_pstName">variable name</param>
        /// <returns>type or -1</returns>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr getNamedVarType([In]IntPtr pvApiCtx, [In] String _pstName, [Out]Int32* _piType);
        //=============================================================================
        /// <summary>
        /// get variable type with adress in scilab stack
        /// used for getNamedMatrixType (internal)
        /// </summary>
        /// <param name="_piAddress"> stack address</param>
        /// <returns>scilab type, 0 fails</returns>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr getVarType([In]IntPtr pvApiCtx, [In] Int32* _piAddress, [Out]Int32* _piType);
        //=============================================================================
        /// <summary>
        ///  Detect if a Scilab graphic window is opened
        /// </summary>
        /// <returns>0 (FALSE) or 1 (TRUE)</returns>
        [DllImport(GraphicsDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int sciHasFigures();
        //=============================================================================
        /// <summary>
        ///  get last error code
        /// </summary>
        /// <returns>last error code</returns>
        [DllImport(OutputStreamDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int GetLastErrorCode();
        //=============================================================================
        /// <summary>
        ///  Get variable dimension
        /// </summary>
        /// import getNamedVarDimension from C (see api_common.h)
        /// <returns>int last error code</returns>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern ApiErr getNamedVarDimension([In]IntPtr pvApiCtx, [In] String _pstName,
                                   [Out] Int32* _piRows, [Out] Int32* _piCols);
        //=============================================================================
        /// <summary>
        ///  Get named complex information
        /// </summary>
        /// import isNamedVarComplex from C (see api_common.h)
        /// <returns>int if complex 1 otherwise 0</returns>
        [DllImport(ApiScilabDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int isNamedVarComplex([In]IntPtr pvApiCtx, [In] String _pstName);
        //=============================================================================

    }
}
//=============================================================================
