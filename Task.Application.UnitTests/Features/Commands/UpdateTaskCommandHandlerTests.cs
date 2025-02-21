using Default = System.Threading.Tasks;
using AutoMapper;
using Moq;
using Shouldly;
using Task.Application.Features.Task.Commands.UpdateTask;
using Task.Application.MappingProfiles;
using Task.Application.UnitTests.Mocks;
using Task.Domain.Enums;
using Task.Domain.Repositories;

namespace Task.Application.UnitTests.Features.Commands
{
    public class UpdateTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public UpdateTaskCommandHandlerTests()
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
        public async Default.Task Handle_ReturnsTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var handler = new UpdateTaskCommandHandler(_mapper, _mockTaskRepository.Object);
            var command = new UpdateTaskCommand(Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"), "Task 1", "Description 1", ETaskStatus.InProgress);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<bool>();
            result.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public async Default.Task Handler_ThrowsException_WhenTaskDoesNotExist()
        {
            // Arrange
            var handler = new UpdateTaskCommandHandler(_mapper, _mockTaskRepository.Object);
            var command = new UpdateTaskCommand(Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b1"), "Task 1", "Description 1", ETaskStatus.InProgress);

            // Act & Assert
            await Should.ThrowAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
