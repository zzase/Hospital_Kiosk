using System;
using System.IO;

namespace HKiosk.Util
{
    public static class Log
    {
        private static readonly string logPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Log";

        public static void Write(string str)
        {
#if DEBUG
            Console.WriteLine(str);
#else
            LogWrite(str);
#endif
        }

        private static void LogWrite(string str)
        {
            string DirPath = logPath;

            string FilePath = $@"{DirPath}\Log_{DateTime.Today.ToString("yyyyMMdd")}.log";
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);
            StreamWriter sw;

            try
            {
                if (!di.Exists) Directory.CreateDirectory(DirPath);

                sw = (!fi.Exists) ? new StreamWriter(FilePath) : File.AppendText(FilePath);
                temp = str != string.Empty ? $"[{DateTime.Now}] {str}" : string.Empty;

                sw.WriteLine(temp);
                sw.Close();
                sw.Dispose();
            }
            catch (Exception e)
            {
                Write($"[LogManager - LogWrite()] 예외 {e.ToString()}");
            }
        }
    }
}
