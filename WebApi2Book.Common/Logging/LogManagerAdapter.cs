using System;
using log4net;

namespace WebApi2Book.Common.Logging
{
    public class LogManagerAdapter : ILogManager
    {
        public ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            return LogManager.GetLogger(typeAssociatedWithRequestedLog);
        }
    }
}