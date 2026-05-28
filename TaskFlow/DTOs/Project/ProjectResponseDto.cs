namespace TaskFlow.DTOs.Project;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByName { get; set; }
}