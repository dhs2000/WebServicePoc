using System;

using NHibernate;

using NLog;

namespace DataAccess.NHibernate
{
    public class NLogLogger : IInternalLogger
    {
        private readonly Logger logger;

        public NLogLogger(string logger)
        {
            this.logger = LogManager.GetLogger(logger);
        }

        public bool IsErrorEnabled
        {
            get
            {
                return this.logger.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return this.logger.IsFatalEnabled;
            }
        }

        public bool IsDebugEnabled
        {
            get
            {
                return this.logger.IsDebugEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return this.logger.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return this.logger.IsWarnEnabled;
            }
        }

        public void Error(object message)
        {
            this.logger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            this.logger.Error(exception, message?.ToString());
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.logger.Error(format, args);
        }

        public void Fatal(object message)
        {
            this.logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            this.logger.Error(exception, message?.ToString());
        }

        public void Debug(object message)
        {
            this.logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            this.logger.Debug(exception, message?.ToString());
        }

        public void DebugFormat(string format, params object[] args)
        {
            this.logger.Debug(format, args);
        }

        public void Info(object message)
        {
            this.logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            this.logger.Info(exception, message?.ToString());
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.logger.Info(format, args);
        }

        public void Warn(object message)
        {
            this.logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            this.logger.Warn(exception, message?.ToString());
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.logger.Warn(format, args);
        }
    }
}