using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public static class Operation
    {
        private static object locker = new object();
        public static void WriteToFile(this string s, string filepath)
        {
            lock (locker)
            {
                FileInfo fi = new FileInfo(filepath);
                if (!Directory.Exists(fi.DirectoryName))
                    Directory.CreateDirectory(fi.DirectoryName);
                File.WriteAllText(filepath, s, Encoding.UTF8);
            }
        }
        
        public static string WriteToFile(this string s, string filename, string ext)
        {
            filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", filename + ext);
            s.WriteToFile(filename);
            return filename;
        }

        public static string ReadAllText(this string s)
        {
            return File.ReadAllText(s, Encoding.UTF8);
        }

        public static string ReadAllLine(this string s)
        {
            StringBuilder sb = new StringBuilder();
            File.ReadLines(s).ToList().ForEach(delegate(string l) { sb.Append(l); });
            return sb.ToString();
        }
    }
}
