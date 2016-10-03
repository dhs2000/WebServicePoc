using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

using NLog;

namespace DataAccess
{
    public class NLogDbInterceptor : IDbCommandInterceptor
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        private static void WriteLog(string command)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(command);
            }
        }
    }
}