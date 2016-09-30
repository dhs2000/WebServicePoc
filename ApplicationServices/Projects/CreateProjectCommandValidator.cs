using FluentValidation;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            this.RuleFor(i => i.Id).NotEmpty();
            this.RuleFor(i => i.Name).NotEmpty();
        }
    }
}