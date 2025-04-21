using FluentValidation;
using InterviewTaskWebApi.Application.Dto.Tasks;

namespace InterviewTaskWebApi.Application.Validation
{
    public class TaskValidation : AbstractValidator<CreateTask>
    {
        public TaskValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title Must be required.");

            RuleFor(x => x.DueDte)
                .NotNull().WithMessage("DueDate Must be required.");

            RuleFor(x => x.ProjectId)
                .NotEqual(Guid.Empty).WithMessage("ProjectId Must be required.");


        }
    }
}

