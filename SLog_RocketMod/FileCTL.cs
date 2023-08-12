using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOpenModPlugin
{
    public class FileCTL
    {
        static string _dirPath = Path.Combine(Environment.CurrentDirectory, "SLog");
        static string _fileName = _dirPath + "/SLog.txt";
        
        public static void AppendAllText(string text)
        {
            File.AppendAllText(_fileName, text + "\n");
        }
    }
}
