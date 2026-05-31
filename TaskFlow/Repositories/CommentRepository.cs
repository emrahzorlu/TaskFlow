using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace TaskFlow.Repositories;

public class CommentRepository : GenericRepository<Comment> , ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {}
    
    public async Task<IEnumerable<Comment>> GetByTaskIdWithDetailsAsync(int taskId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.TaskId == taskId)
            .ToListAsync();
    }
}