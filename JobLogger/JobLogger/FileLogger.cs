using System;
using System.IO;
using System.Configuration;

namespace JobLogger
{
    internal class FileLogger : ILogger
    {
        private readonly string FILE_PATH ;

        internal FileLogger(string filePath)
        {
            FILE_PATH = filePath;
        }

        public bool Log(LogItem logItem)
        {
            bool ret = false;
            if (logItem != null && !String.IsNullOrEmpty(FILE_PATH))
            {
                if (File.Exists(FILE_PATH))
                {
                    ret = writeToFile(File.AppendText(FILE_PATH), logItem);
                }
                else
                {
                    FileStream fs = createFile();
                    if (fs != null)
                    {
                        ret = writeToFile(new StreamWriter(fs), logItem);
                    }
                }
            }
            return ret;
        }

        // Creates a file and returns its FileStream, in case we want to read or write to it.
        private FileStream createFile()
        {
            FileStream fs = null;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FILE_PATH));
                fs = File.Create(FILE_PATH);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating the file " + e.Message);
            }
            return fs;
        }

        // Writes to a specific file, the desired message from the logItem
        private bool writeToFile(StreamWriter sw, LogItem logItem)
        {
            bool ret = false;
            if (sw != null)
            {
                try
                {
                    if (logItem != null)
                    {
                        sw.WriteLine(logItem.ToString());
                        ret = true;
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
            return ret;
        }
    }
}
