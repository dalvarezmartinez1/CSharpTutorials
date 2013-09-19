using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace JobLogger
{
    internal class FileLogger : ILogger
    {
        private static readonly string LOG_DIR = ConfigurationManager.AppSettings["LogFileDirectory"];
        // We will be able to create 1 different file every second, I assume desired behavior
        private static readonly string DATE_FORMAT = "yyyyMMdd-HHmmss";
        // We will create only 1 file per every instantiation of this class. I assume desired behavior
        private readonly string FILE_PATH;

        public FileLogger()
        {
            FILE_PATH = LOG_DIR + "\\LogFile" +  DateTime.Now.ToString(DATE_FORMAT) + ".txt";
        }
        
        public void Log(LogItem logItem)
        {
            if (File.Exists(FILE_PATH))
            {
                writeToFile(File.AppendText(FILE_PATH), logItem);
            }
            else
            {
                FileStream fs = createFile(FILE_PATH);
                if (fs != null)
                {
                    writeToFile(new StreamWriter(fs), logItem);
                }
            }
        }

        // Creates a file and returns its FileStream, in case we want to read or write to it.
        private FileStream createFile(string FILE_PATH) {
            FileStream fs = null;
            try
            {
                Directory.CreateDirectory(LOG_DIR);
                fs = File.Create(FILE_PATH);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating the file " + e.Message);
            } 
            return fs;
        }

        // Writes to a specific file, the desired message from the logItem
        private void writeToFile(StreamWriter sw, LogItem logItem) {
            if (sw != null)
            {
                try
                {
                    if (logItem != null)
                    {
                        sw.WriteLine(logItem.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while writing to file ", e.ToString());
                }
                finally
                {
                    sw.Close();
                }
            }
        }
    }
}
