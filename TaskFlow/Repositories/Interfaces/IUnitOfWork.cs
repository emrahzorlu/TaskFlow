namespace TaskFlow.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IProjectRepository Projects { get; }
    IWorkTaskRepository Tasks { get; }
    ICommentRepository Comments { get; }

    Task<int> SaveChangesAsync();
}