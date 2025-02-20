using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Dtos;
using Task.Application.Features.Task.Commands.CreateTask;
using Task.Application.Features.Task.Commands.DeleteTask;
using Task.Application.Features.Task.Commands.UpdateTask;
using Task.Application.Features.Task.Queries.GetTaskDetails;
using Task.Application.Features.Task.Queries.GetTasks;

namespace Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<TaskListDto>>> Get()
        {
            var tasks = await _mediator.Send(new GetTasksQuery());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get(Guid id)
        {
            var task = await _mediator.Send(new GetTaskDetailsQuery(id));
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateTaskCommand request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateTaskCommand request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));
            return CreatedAtAction(nameof(Get), result);
        }
    }
}
