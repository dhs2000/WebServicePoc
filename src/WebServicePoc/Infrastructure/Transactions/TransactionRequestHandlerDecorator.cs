using System;
using System.Data;
using System.Threading.Tasks;

using MediatR;

using NHibernate;

using NLog;

namespace WebServicePoc.Infrastructure.Transactions
{
    public class TransactionRequestHandlerDecorator<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAsyncRequestHandler<TRequest, TResponse> internaHandler;

        private readonly ISession session;

        public TransactionRequestHandlerDecorator(IAsyncRequestHandler<TRequest, TResponse> internaHandler, ISession session)
        {
            if (internaHandler == null)
            {
                throw new ArgumentNullException(nameof(internaHandler));
            }

            this.internaHandler = internaHandler;
            this.session = session;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            this.session.BeginTransaction(IsolationLevel.ReadCommitted);
            Logger.Debug("Begin transaction. Session '{0}'", this.session.GetHashCode());
            try
            {
                TResponse response = await this.internaHandler.Handle(message);
                this.session.Transaction.Commit();
                Logger.Debug("Commit transaction. Session '{0}'", this.session.GetHashCode());
                return response;
            }
            catch (Exception)
            {
                if (this.session.Transaction.IsActive)
                {
                    this.session.Transaction.Rollback();
                    Logger.Debug("Rollback transaction. Session '{0}'", this.session.GetHashCode());
                }

                throw;
            }
        }
    }
}