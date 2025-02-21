using FluentValidation;

namespace Task.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long.");

            RuleFor(p => p.Status)
                .NotNull();
        }
    }
}
