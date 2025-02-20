using AutoMapper;
using Task.Application.Dtos;
using Task.Application.Features.Task.Commands.CreateTask;
using Task.Application.Features.Task.Commands.DeleteTask;
using Task.Application.Features.Task.Commands.UpdateTask;
using Entity = Task.Domain.Entities;

namespace Task.Application.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entity.Task, TaskListDto>().ReverseMap();
            CreateMap<Entity.Task, TaskDto>().ReverseMap();
            CreateMap<CreateTaskCommand, Entity.Task>().ReverseMap();
            CreateMap<UpdateTaskCommand, Entity.Task>().ReverseMap();
            CreateMap<DeleteTaskCommand, Entity.Task>().ReverseMap();
        }
    }
}
