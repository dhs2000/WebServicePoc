using System.Linq;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using NLog;

namespace WebServicePoc.Infrastructure
{
    public class AsyncValidationRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAsyncRequestHandler<TRequest, TResponse> innerHander;

        private readonly IValidator<TRequest>[] validators;

        public AsyncValidationRequestHandler(
            IAsyncRequestHandler<TRequest, TResponse> innerHandler,
            IValidator<TRequest>[] validators)
        {
            this.validators = validators;
            this.innerHander = innerHandler;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Validating request ('{0}' / {1})", message.GetType().FullName, message.GetHashCode());
            }

            var context = new ValidationContext(message);

            ValidationFailure[] failures =
                this.validators.Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToArray();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Validated ('{0}' / {1})", message.GetType().FullName, message.GetHashCode());
            }

            return await this.innerHander.Handle(message);
        }
    }
}