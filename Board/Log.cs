using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyBoard
{
    public class Log
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        private string Message { get; set; }

        public Log(object className, string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            this.ClassName = GetClassName(className);
            this.MethodName = memberName;
            this.Message = message;

        }

        public string GetClassName(object o)
        {
            Type t = o.GetType();
            return ClassName = t.Name;
        }

        public void WriteToFile()
        {
            string str = ClassName + " " + MethodName + " " + DateTime.Now + " " + Message;
            string path = @"D:\Example";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            using FileStream filestream = new FileStream($"{path}\\examlpe.txt", FileMode.Append);
            var array = System.Text.Encoding.Default.GetBytes(str);
            filestream.Write(array, 0, array.Length);
        }
    }
}