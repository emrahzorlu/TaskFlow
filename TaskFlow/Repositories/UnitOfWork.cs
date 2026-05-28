using TaskFlow.Data;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IProjectRepository Projects { get; }
    public IWorkTaskRepository Tasks { get; }
    public ICommentRepository Comments { get; }

    public UnitOfWork(AppDbContext context,
        IUserRepository users,
        IProjectRepository projects,
        IWorkTaskRepository tasks,
        ICommentRepository comments)
    {
        _context = context;
        Users = users;
        Projects = projects;
        Tasks = tasks;
        Comments = comments;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}