using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<IEnumerable<Project>> GetAllWithDetailsAsync();
    Task<Project?> GetByIdWithDetailsAsync(int id);
}