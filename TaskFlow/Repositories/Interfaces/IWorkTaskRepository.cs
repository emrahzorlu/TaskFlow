using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories.Interfaces;

public interface IWorkTaskRepository : IGenericRepository<WorkTask>
{
    Task<IEnumerable<WorkTask>> GetAllWithDetailsAsync();
    Task<WorkTask?> GetByIdWithDetailsAsync(int id);
}