using AutoMapper;
using MediatR;
using Task.Domain.Repositories;
using Entity = Task.Domain.Entities;

namespace Task.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTaskCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
            {
                throw new Exception();
            }

            var task = _mapper.Map<Entity.Task>(request);
            return await _taskRepository.CreateAsync(task);
        }
    }
}
