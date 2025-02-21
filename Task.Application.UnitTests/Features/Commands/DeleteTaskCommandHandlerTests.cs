using Default = System.Threading.Tasks;
using AutoMapper;
using Moq;
using Task.Application.MappingProfiles;
using Task.Application.UnitTests.Mocks;
using Task.Domain.Repositories;
using Task.Application.Features.Task.Commands.DeleteTask;
using Shouldly;

namespace Task.Application.UnitTests.Features.Commands
{
    public class DeleteTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;

        public DeleteTaskCommandHandlerTests()
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
        public async Default.Task Handle_ReturnsTrue_WhenDeletionIsSuccessful()
        {
            // Arrange
            var handler = new DeleteTaskCommandHandler(_mapper, _mockTaskRepository.Object);
            var command = new DeleteTaskCommand(Guid.Parse("054e67b7-bb8f-4cd6-b9af-79047ea808ac"));


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<bool>();
            result.ShouldBeEquivalentTo(true);
        }
    }
}
