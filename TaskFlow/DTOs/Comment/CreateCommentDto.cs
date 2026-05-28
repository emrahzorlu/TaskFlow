namespace TaskFlow.DTOs.Comment;

public class CreateCommentDto
{
    public string Content { get; set; }
    public int TaskId { get; set; }
}