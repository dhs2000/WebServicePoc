using Contracts;

using FluentValidation;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            this.RuleFor(i => i.Id).NotEmpty().IsGuid();
            this.RuleFor(i => i.Name).Length(1, 255);
        }
    }
}