using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories;

public class CommentRepository : GenericRepository<Comment> , ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {}
}