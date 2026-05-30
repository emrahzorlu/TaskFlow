using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.DTOs.Comment;
using TaskFlow.Services.Interfaces;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetByTask(int taskId)
    {
        var comments = await _commentService.GetByTaskIdAsync(taskId);
        
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var comment = await _commentService.CreateAsync(dto, userId);
        
        return Ok(comment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var comment = await _commentService.UpdateAsync(id, dto, userId);
        
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _commentService.DeleteAsync(id, userId);
        
        return NoContent();
    }
}