using System;

using Autofac;

using FluentValidation;

namespace WebServicePoc.Infrastructure.Validation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            this.context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;

            if (this.context.TryResolve(validatorType, out instance))
            {
                return (IValidator)instance;
            }

            return null;
        }
    }
}