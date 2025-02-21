using Default = System.Threading.Tasks;
using AutoMapper;
using Moq;
using Task.Application.MappingProfiles;
using Task.Application.UnitTests.Mocks;
using Task.Domain.Repositories;
using Task.Application.Features.Task.Commands.CreateTask;
using Task.Domain.Enums;
using Shouldly;

namespace Task.Application.UnitTests.Features.Commands
{
    public class CreateTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public CreateTaskCommandHandlerTests()
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
        public async Default.Task Handle_ReturnsGuid_WhenCreateTaskIsSuccessfull()
        {
            // Arrange
            var handler = new CreateTaskCommandHandler(_mapper, _mockTaskRepository.Object);
            var command = new CreateTaskCommand("Test Alpha", "Description Alpha", ETaskStatus.New);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<Guid>();
            result.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Default.Task Handle_ThrowsException_WhenValidationIsUnsuccessfull()
        {
            // Arrange
            var handler = new CreateTaskCommandHandler(_mapper, _mockTaskRepository.Object);
            var command = new CreateTaskCommand("", "", ETaskStatus.New);

            // Act & Assert
            await Should.ThrowAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
