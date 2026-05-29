using AutoMapper;
using TaskFlow.DTOs.Project;
using TaskFlow.Exceptions;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.Services.Interfaces;

namespace TaskFlow.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        var projects = await _unitOfWork.Projects.GetAllAsync();
        
        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null)
            throw new NotFoundException($"Project with id {id} not found");
        
        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int createdById)
    {
        var project = _mapper.Map<Project>(dto);
        project.CreatedById = createdById;
        project.CreatedAt = DateTime.UtcNow;
        project.IsDeleted = false;

        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto dto)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null)
            throw new NotFoundException($"Project with id {id} not found");

        project.Title = dto.Title;
        project.Description = dto.Description;

        await _unitOfWork.Projects.UpdateAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null)
            throw new NotFoundException($"Project with id {id} not found");

        project.IsDeleted = true;
        project.DeletedAt = DateTime.UtcNow;

        await _unitOfWork.Projects.UpdateAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}