using Default = System.Threading.Tasks;
using AutoMapper;
using Moq;
using Task.Application.MappingProfiles;
using Task.Application.UnitTests.Mocks;
using Task.Domain.Repositories;
using Task.Application.Dtos;
using Task.Domain.Enums;
using Task.Application.Features.Task.Queries.GetTaskDetails;
using Shouldly;

namespace Task.Application.UnitTests.Features.Queries
{
    public class GetTaskDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public GetTaskDetailsQueryHandlerTests()
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
        public async Default.Task Handle_ReturnTaskSuccessfully_WhenIdExists()
        {
            // Arrange
            var id = Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7");
            var expectedTask = new TaskDto(id, "Task 1", "Description 1", ETaskStatus.New, default, default, default);

            var handler = new GetTaskDetailsQueryHandler(_mapper, _mockTaskRepository.Object);
            var query = new GetTaskDetailsQuery(id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<TaskDto>();
            result.ShouldBeEquivalentTo(expectedTask);
        }

        [Fact]
        public async Default.Task Handle_ReturnsNull_WhenIdDoesNotExists()
        {
            // Arrange
            var id = new Guid();

            var handler = new GetTaskDetailsQueryHandler(_mapper, _mockTaskRepository.Object);
            var query = new GetTaskDetailsQuery(id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
