using TaskFlow.Models.Enums;

namespace TaskFlow.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } 
    public string FullName { get; set; }
    public UserRole Role { get; set; } 
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Project> Projects { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<UserTask> UserTasks { get; set; }
}
