namespace TaskFlow.DTOs.Task;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public int ProjectId { get; set; }
    public string CreatedByName { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}