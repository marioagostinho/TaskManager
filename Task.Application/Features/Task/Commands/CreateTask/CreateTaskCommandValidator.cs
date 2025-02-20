using FluentValidation;

namespace Task.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotNull();

            RuleFor(p => p.Status)
                .NotNull();
        }
    }
}
