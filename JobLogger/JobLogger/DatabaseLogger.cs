using System;
using System.Configuration;

namespace JobLogger
{
    internal class DatabaseLogger : ILogger
    {
        private readonly Model model;

        internal DatabaseLogger(Model model)
        {
            this.model = model;
        }

        public bool Log(LogItem logItem)
        {
            bool ret = false;
            if (model != null && logItem != null)
            {
                ret = model.Insert(logItem);
            }
            return ret;
        }
    }
}
