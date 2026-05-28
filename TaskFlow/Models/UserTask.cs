namespace TaskFlow.Models;

public class UserTask
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int TaskId { get; set; }
    public WorkTask Task { get; set; }
    public DateTime AssignedAt { get; set; }
}