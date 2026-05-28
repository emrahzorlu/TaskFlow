using TaskFlow.Models.Enums;

namespace TaskFlow.Models;

public class WorkTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Enums.TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    public ICollection<UserTask> UserTasks { get; set; }
}