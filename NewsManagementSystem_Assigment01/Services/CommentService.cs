using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;

namespace NewsManagementSystem_Assigment01.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetCommentsAsync(string postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            return await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<Comment> EditCommentAsync(int id, string content)
        {
            var comment = await _commentRepository.UpdateCommentAsync(new Comment { CommentId = id, Content = content });
            return comment;
        }

        public async Task<bool> RemoveCommentAsync(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }
    }
}
