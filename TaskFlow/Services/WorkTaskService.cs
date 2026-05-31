using AutoMapper;
using TaskFlow.DTOs.Task;
using TaskFlow.Exceptions;
using TaskFlow.Models;
using TaskFlow.Models.Enums;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.Services.Interfaces;
using TaskStatus = TaskFlow.Models.Enums.TaskStatus;
namespace TaskFlow.Services;

public class WorkTaskService : IWorkTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WorkTaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
    {
        var tasks = await _unitOfWork.Tasks.GetAllWithDetailsAsync();
        
        return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdWithDetailsAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");
        
        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _unitOfWork.Tasks.GetAllAsync();
        var projectTasks = tasks.Where(t => t.ProjectId == projectId);
        
        return _mapper.Map<IEnumerable<TaskResponseDto>>(projectTasks);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetMyTasksAsync(int userId)
    {
        var tasks = await _unitOfWork.Tasks.GetAllAsync();
        var myTasks = tasks.Where(t => t.UserTasks.Any(ut => ut.UserId == userId));
        
        return _mapper.Map<IEnumerable<TaskResponseDto>>(myTasks);
    }

    public async Task<TaskResponseDto> CreateAsync(int projectId, CreateTaskDto dto, int createdById)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project == null)
            throw new NotFoundException($"Project with id {projectId} not found");

        var task = _mapper.Map<WorkTask>(dto);
        task.ProjectId = projectId;
        task.CreatedById = createdById;
        task.CreatedAt = DateTime.UtcNow;
        task.Status = TaskStatus.Todo;
        task.IsDeleted = false;

        await _unitOfWork.Tasks.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<TaskResponseDto?> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Priority = Enum.Parse<TaskPriority>(dto.Priority);
        task.DueDate = dto.DueDate;

        await _unitOfWork.Tasks.UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<TaskResponseDto?> UpdateStatusAsync(int id, UpdateTaskStatusDto dto)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        task.Status = Enum.Parse<TaskStatus>(dto.Status);

        await _unitOfWork.Tasks.UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<bool> AssignUserAsync(int taskId, int userId)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);
        if (task == null)
            throw new NotFoundException($"Task with id {taskId} not found");

        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User with id {userId} not found");

        var userTask = new UserTask
        {
            TaskId = taskId,
            UserId = userId,
            AssignedAt = DateTime.UtcNow
        };

        await _unitOfWork.UserTasks.AddAsync(userTask);
        await _unitOfWork.SaveChangesAsync();
    
        return true;
    }

    public async Task<bool> UnassignUserAsync(int taskId, int userId)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);
        if (task == null)
            throw new NotFoundException($"Task with id {taskId} not found");

        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        task.IsDeleted = true;
        task.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.Tasks.UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}