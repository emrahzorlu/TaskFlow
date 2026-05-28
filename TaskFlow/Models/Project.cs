namespace TaskFlow.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public ICollection<WorkTask> Tasks { get; set; }
}