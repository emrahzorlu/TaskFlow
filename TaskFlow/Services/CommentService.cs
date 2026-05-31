using AutoMapper;
using TaskFlow.DTOs.Comment;
using TaskFlow.Exceptions;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.Services.Interfaces;

namespace TaskFlow.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentResponseDto>> GetByTaskIdAsync(int taskId)
    {
        var comments = await _unitOfWork.Comments.GetByTaskIdWithDetailsAsync(taskId);
        
        return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
    }

    public async Task<CommentResponseDto> CreateAsync(CreateCommentDto dto, int userId)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(dto.TaskId);
        if (task == null)
            throw new NotFoundException($"Task with id {dto.TaskId} not found");

        var comment = _mapper.Map<Comment>(dto);
        comment.UserId = userId;
        comment.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.Comments.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CommentResponseDto>(comment);
    }

    public async Task<CommentResponseDto?> UpdateAsync(int id, UpdateCommentDto dto, int userId)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(id);
        if (comment == null)
            throw new NotFoundException($"Comment with id {id} not found");

        if (comment.UserId != userId)
            throw new ForbiddenException("You can only update your own comments");

        comment.Content = dto.Content;
        comment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Comments.UpdateAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CommentResponseDto>(comment);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(id);
        if (comment == null)
            throw new NotFoundException($"Comment with id {id} not found");

        if (comment.UserId != userId)
            throw new ForbiddenException("You can only delete your own comments");

        await _unitOfWork.Comments.DeleteAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}