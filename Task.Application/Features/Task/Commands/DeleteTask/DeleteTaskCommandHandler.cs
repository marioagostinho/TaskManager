using AutoMapper;
using MediatR;
using Task.Domain.Repositories;
using Entity = Task.Domain.Entities;

namespace Task.Application.Features.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            DeleteTaskCommandValidator validator = new DeleteTaskCommandValidator();
            var validatorResult =  await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
            {
                throw new Exception();
            }

            var task = _mapper.Map<Entity.Task>(request);
            return await _taskRepository.DeleteAsync(task);
        }
    }
}
