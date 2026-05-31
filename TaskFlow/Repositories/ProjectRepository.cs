using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace TaskFlow.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context) 
    {}
    
    public async Task<IEnumerable<Project>> GetAllWithDetailsAsync()
    {
        return await _context.Projects
            .Include(p => p.CreatedBy)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Projects
            .Include(p => p.CreatedBy)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}