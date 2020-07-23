using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace send_working_day.Actions
{
    class WriteLog
    {
        public static void WriteLogFile(string textLog, int type)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                if (type == 0) LogProcess(textLog, w);
                else LogSuccess(textLog, w);
            }

            using (StreamReader r = File.OpenText("log.txt"))
            {
                DumpLog(r);
            }
        }

        public static void LogProcess(string logMessage, TextWriter w)
        {
            w.WriteLine("------------------------------- \r\n");
            w.Write("Log Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} \r\n");
            w.WriteLine($"{logMessage} \r\n");
        }
        public static void LogSuccess(string logMessage, TextWriter w)
        {
            w.WriteLine("------------------------------- \r\n");
            w.WriteLine($"{logMessage} \r\n");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
