namespace TaskFlow.DTOs.Comment;

public class CommentResponseDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public DateTime CreatedAt { get; set; }
}