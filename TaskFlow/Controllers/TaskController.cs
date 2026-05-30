using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.DTOs.Task;
using TaskFlow.Services.Interfaces;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IWorkTaskService _taskService;

    public TaskController(IWorkTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        
        return Ok(task);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTasks()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var tasks = await _taskService.GetMyTasksAsync(userId);
        
        return Ok(tasks);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var tasks = await _taskService.GetByProjectIdAsync(projectId);
        
        return Ok(tasks);
    }

    [HttpPost("project/{projectId}")]
    [Authorize(Roles = "Admin,Lead")]
    public async Task<IActionResult> Create(int projectId, CreateTaskDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var task = await _taskService.CreateAsync(projectId, dto, userId);
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Lead")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await _taskService.UpdateAsync(id, dto);
        
        return Ok(task);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto dto)
    {
        var task = await _taskService.UpdateStatusAsync(id, dto);
        
        return Ok(task);
    }

    [HttpPost("{id}/assign/{userId}")]
    [Authorize(Roles = "Admin,Lead")]
    public async Task<IActionResult> AssignUser(int id, int userId)
    {
        await _taskService.AssignUserAsync(id, userId);
        
        return Ok();
    }

    [HttpDelete("{id}/assign/{userId}")]
    [Authorize(Roles = "Admin,Lead")]
    public async Task<IActionResult> UnassignUser(int id, int userId)
    {
        await _taskService.UnassignUserAsync(id, userId);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Lead")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id);
        
        return NoContent();
    }
}