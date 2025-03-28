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

        public async Task<Comment> UpdateCommentAsync(int commentId, string newContent)
        {
            var comment = await _context.Comments
                .Include(c => c.User) // Tải thông tin User để kiểm tra quyền
                .Include(c => c.NewsArticle) // Tải thông tin NewsArticle để đảm bảo NewsArticleId không bị NULL
                .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (comment == null)
            {
                return null;
            }

            // Chỉ cập nhật các thuộc tính cần thiết
            comment.Content = newContent;
            comment.UpdatedAt = DateTime.UtcNow;

            // Đánh dấu chỉ các thuộc tính đã thay đổi
            _context.Entry(comment).Property(c => c.Content).IsModified = true;
            _context.Entry(comment).Property(c => c.CreatedAt).IsModified = true;

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
