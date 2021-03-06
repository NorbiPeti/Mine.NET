﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET
{
    public class Logger
    {
        protected string Prefix = "";

        private string name;
        public Logger(string name)
        {
            this.name = name;
            if (!Loggers.ContainsKey(name))
                Loggers.Add(name, this);
        }
        public void Severe(object p, Exception ex = null)
        {
            throw new NotImplementedException(); //TODO
        }

        public void Warning(string v, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public void Info(string v, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        private void Log(string level, string msg, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();
        public static Logger getLogger(string name)
        {
            if (!Loggers.ContainsKey(name))
                Loggers.Add(name, new Logger(name));
                return Loggers[name];
        }
    }
}
