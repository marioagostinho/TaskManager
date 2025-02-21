using Default = System.Threading.Tasks;
using AutoMapper;
using Moq;
using Task.Application.MappingProfiles;
using Task.Application.UnitTests.Mocks;
using Task.Domain.Repositories;
using Task.Application.Features.Task.Queries.GetTasks;
using Shouldly;
using Task.Application.Dtos;

namespace Task.Application.UnitTests.Features.Queries
{
    public class GetTasksQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public GetTasksQueryHandlerTests()
        {
            // Setting up the AutoMapper 
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TaskProfile());
            });

            _mapper = configuration.CreateMapper();

            // Setting up the Mock
            _mockTaskRepository = MockTaskRepository.GetMockTaskRepository();
        }

        [Fact]
        public async Default.Task Handle_ReturnsAllAppointmentsSuccessfully()
        {
            // Arrange
            var handler = new GetTasksQueryHandler(_mapper, _mockTaskRepository.Object);
            var query = new GetTasksQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<TaskListDto>>();
            result.Count.ShouldBe(4);
        }
    }
}
