using TaskFlow.DTOs.Comment;

namespace TaskFlow.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetByTaskIdAsync(int taskId);
    Task<CommentResponseDto> CreateAsync(CreateCommentDto dto, int userId);
    Task<CommentResponseDto> UpdateAsync(int id, UpdateCommentDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}