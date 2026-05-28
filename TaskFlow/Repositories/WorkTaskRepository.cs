using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories;

public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(AppDbContext context) : base(context) 
    {}
}