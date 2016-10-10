using Contracts;

using FluentValidation;

namespace ApplicationServices.Projects
{
    public class GetProjectsRequestValidator : AbstractValidator<GetProjectsRequest>
    {
        public GetProjectsRequestValidator()
        {
            this.RuleFor(i => i.Id).IsGuid().When(i => i.Id != null);
        }
    }
}