using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByTaskIdWithDetailsAsync(int taskId);
}