using Microsoft.EntityFrameworkCore;
using NewsManagementSystem_Assigment01.Models;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FunewsManagementContext _context;

        public CommentRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            return await _context.Comments.Where(c => c.NewsArticleId.Equals(postId)).ToListAsync();
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
