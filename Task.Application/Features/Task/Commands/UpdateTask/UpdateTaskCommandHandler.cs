using AutoMapper;
using MediatR;
using Task.Domain.Enums;
using Task.Domain.Repositories;
using Entity = Task.Domain.Entities;

namespace Task.Application.Features.Task.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTaskCommandValidator(_taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
            {
                throw new Exception();
            }

            var task = _mapper.Map<Entity.Task>(request);

            if (task.Status == ETaskStatus.Completed)
            {
                task.DateDelivery = DateTime.Now;
            }

            return await _taskRepository.UpdateAsync(task);
        }
    }
}
