using System.Text;
using System.Runtime.InteropServices;

namespace Scilab
{
    public class ScilabConnection
    {
        [DllImport(@"C:\Program Files\scilab-5.4.0\bin\LibScilab.dll", EntryPoint = "StartScilab", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartScilab2(StringBuilder SCIpath, StringBuilder ScilabStartup, int Stacksize);

        [DllImport(@"C:\Program Files\scilab-5.4.0\bin\LibScilab.dll", EntryPoint = "SendScilabJob", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendScilabJob(string job);
    }
}
