using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models
{
    public class MultiThreadFileWriter
    {
        private readonly string _filePath;

        private static object lockToWritingFile = new object();

        public MultiThreadFileWriter()
        {
            _filePath = System.Configuration.ConfigurationManager.AppSettings["FileLoggingPath"];
            _filePath = _filePath.Replace("{timestamp}", DateTime.Now.ToString("yyyyMMdd"));
        }

        public MultiThreadFileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public async void WriteToFile(string textToWrite)
        {
            Monitor.Enter(lockToWritingFile);
            try
            {
                using (StreamWriter w = File.AppendText(_filePath))
                {
                    await w.WriteLineAsync(textToWrite + Environment.NewLine + Environment.NewLine);
                    w.Flush();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                Monitor.Exit(lockToWritingFile);
            }
        }
    }
}
