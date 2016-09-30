using System.Linq;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace WebServicePoc.Infrastructure
{
    public class AsyncValidationRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {
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

            return await this.innerHander.Handle(message);
        }
    }
}