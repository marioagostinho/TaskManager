﻿using FluentValidation;
using Task.Domain.Repositories;

namespace Task.Application.Features.Task.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandValidator(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .MustAsync(TaskMustExist).WithMessage("Task must exist");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long.");

            RuleFor(p => p.Status)
                .NotNull().WithMessage("{PropertyName} can't be null");
        }

        private async Task<bool> TaskMustExist(Guid id, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetByIdAsync(id) != null;
        }
    }
}
