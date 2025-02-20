using AutoMapper;
using MediatR;
using Task.Application.Dtos;
using Task.Domain.Repositories;

namespace Task.Application.Features.Task.Queries.GetTaskDetails
{
    public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, TaskDto>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetTaskDetailsQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetByIdAsync(request.Id);
            var result = _mapper.Map<TaskDto>(tasks);

            return result;
        }
    }
}
