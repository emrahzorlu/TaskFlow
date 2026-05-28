using AutoMapper;
using TaskFlow.DTOs.Auth;
using TaskFlow.DTOs.Comment;
using TaskFlow.DTOs.Project;
using TaskFlow.DTOs.Task;
using TaskFlow.DTOs.User;
using TaskFlow.Models;

namespace TaskFlow.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        
        CreateMap<Project, ProjectResponseDto>()
            .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName));

        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();

        CreateMap<WorkTask, TaskResponseDto>()
            .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

        CreateMap<CreateTaskDto, WorkTask>();
        CreateMap<UpdateTaskDto, WorkTask>();

        CreateMap<Comment, CommentResponseDto>()
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));

        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}