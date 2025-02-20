using AutoMapper;
using MediatR;
using Task.Application.Dtos;
using Task.Domain.Repositories;

namespace Task.Application.Features.Task.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IReadOnlyList<TaskListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<IReadOnlyList<TaskListDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();
            var result = _mapper.Map<IReadOnlyList<TaskListDto>>(tasks);

            return result;
        }
    }
}