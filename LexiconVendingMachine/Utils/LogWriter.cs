using System;
using System.IO;
using System.Reflection;

namespace LexiconVendingMachine.Utils
{
    // Class to log exceptions, log.txt is located in \bin\Debug\net5.0 or LexiconVendingMachine.Tests\bin\Debug\netcoreapp3.1
    public class LogWriter
    {
        public static void LogWrite(string logMessage)
        {
            string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using StreamWriter w = File.AppendText(filepath + "\\" + "log.txt"); // alternative way: File.AppendAllText(filePath + "log.txt", log);
                Log(logMessage, w);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e} when writing to logfile");
            }
        }

        private static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }
    }
}