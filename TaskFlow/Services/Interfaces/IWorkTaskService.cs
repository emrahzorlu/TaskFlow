using TaskFlow.DTOs.Task;

namespace TaskFlow.Services.Interfaces;

public interface IWorkTaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync();
    Task<TaskResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<TaskResponseDto>> GetByProjectIdAsync(int projectId);
    Task<IEnumerable<TaskResponseDto>> GetMyTasksAsync(int userId);
    Task<TaskResponseDto> CreateAsync(int projectId, CreateTaskDto dto, int createdById);
    Task<TaskResponseDto?> UpdateAsync(int id, UpdateTaskDto dto);
    Task<TaskResponseDto?> UpdateStatusAsync(int id, UpdateTaskStatusDto dto);
    Task<bool> AssignUserAsync(int taskId, int userId);
    Task<bool> UnassignUserAsync(int taskId, int userId);
    Task<bool> DeleteAsync(int id);
}