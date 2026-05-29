using TaskFlow.DTOs.Project;

namespace TaskFlow.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int createdById);
    Task<ProjectResponseDto> UpdateAsync(int id, UpdateProjectDto dto);
    Task<bool> DeleteAsync(int id);
}