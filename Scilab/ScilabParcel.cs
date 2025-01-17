using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using LabSharp;

namespace Scilab
{
    public class ScilabParcel
    {
        //private void SaveToScilabMatrix2(string fileName)
        //{
        //    if (GetAllActiveTrends().Count == 0) return;

        //    FileStream BinaryFile = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
        //    using (BinaryWriter writer = new BinaryWriter(BinaryFile))
        //    {
        //        List trends = GetAllActiveTrends();
        //        foreach (TrendData td in trends)
        //        {
        //            //name
        //            writer.Write(Str2Code(td.Name, true));
        //            // type
        //            writer.Write((int)1);
        //            // rows
        //            writer.Write((int)td.DataBuffer.Count);
        //            // cols
        //            writer.Write((int)1);
        //            // real numbers are used
        //            writer.Write((int)0);

        //            foreach (double d in td.DataBuffer.Values)
        //            {
        //                writer.Write((double)d);
        //            }
        //        }
        //    }

        //    BinaryFile.Close();
        //}

        private void AccessScilab(string script)
        {
            TextWriter tw = File.CreateText("temp.sci");
            //SaveToScilabMatrix2("data_in.dat");
            tw.WriteLine("load('{0}\\data_in.dat');", System.IO.Directory.GetCurrentDirectory());
            tw.WriteLine(script);
            //tw.WriteLine("save('{0}\\data_out.dat', {1});", System.IO.Directory.GetCurrentDirectory(), tbVariables2Save.Text);
            tw.Close();
            if (ScilabConnection.SendScilabJob(string.Format(@"exec('{0}\temp.sci');", System.IO.Directory.GetCurrentDirectory())) != 0)
            {
                //MessageBox.Show("Error in the script");
            }
        }
        private void TestInterface()
        {
            double[] sin = new double[100];

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

    }
}
