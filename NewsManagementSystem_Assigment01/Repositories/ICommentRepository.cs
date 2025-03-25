namespace NewsManagementSystem_Assigment01.Repositories
{
    using NewsManagementSystem_Assigment01.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(string postId);
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }

}
