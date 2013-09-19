using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace JobLogger
{
    internal class DatabaseLogger : ILogger
    {
        private static readonly string CONN_STRING;

        private static readonly Model model;

        static DatabaseLogger()
        {
            try
            {
                CONN_STRING = ConfigurationManager.AppSettings["ConnectionString"];
                model = new Model(CONN_STRING);
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Error {0} while creating the db model. You will not be able to log to database!"
                    , e.Message));
            }
        }

        public void Log(LogItem logItem)
        {
            if (model != null)
            {
                model.insert(logItem);
            }
        }
    }
}
