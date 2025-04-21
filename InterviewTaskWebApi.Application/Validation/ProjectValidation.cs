using FluentValidation;
using InterviewTaskWebApi.Application.Dto.Projects;

namespace InterviewTaskWebApi.Application.Validation
{
    public class ProjectValidation : AbstractValidator<CreateProjectDto>
    {

        public ProjectValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.StartDate).NotEmpty()
                .NotNull().WithMessage("StatDate is required.");

            RuleFor(x => x.EndDate).NotEmpty().NotNull().WithMessage("StatDate is required.");


        }
    }
}
