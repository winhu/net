using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace WinStudio.SqlCoparer
{
    public interface IPrinter
    {
        void Log(string msg);
    }
    [Export(typeof(IPrinter))]
    public class Importer : IPrinter
    {
        public void Log(string msg)
        {
            Console.WriteLine("this is IPrinter.Log");
            Console.WriteLine(msg);
        }
    }
}
