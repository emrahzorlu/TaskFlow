using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace TaskFlow.Repositories;

public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(AppDbContext context) : base(context) 
    {}
    
    public async Task<IEnumerable<WorkTask>> GetAllWithDetailsAsync()
    {
        return await _context.Tasks
            .Include(t => t.CreatedBy)
            .Include(t => t.Project)
            .ToListAsync();
    }

    public async Task<WorkTask?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Tasks
            .Include(t => t.CreatedBy)
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}