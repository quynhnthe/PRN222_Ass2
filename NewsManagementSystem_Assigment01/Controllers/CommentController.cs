using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewsManagementSystem_Assigment01.Hubs;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Services;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;
        private readonly IHubContext<CommentHub> _hubContext;
        public CommentController(CommentService commentService, IHubContext<CommentHub> hubContext)
        {
            _commentService = commentService;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    NewsArticleId = commentViewModel.NewsArticleId,
                    UserId = commentViewModel.UserId,
                    Content = commentViewModel.Content,
                    CreatedAt = DateTime.UtcNow
                };
                var newComment = await _commentService.CreateCommentAsync(comment);
                //await _hubContext.Clients.Group($"post-{comment.NewsArticleId}")
                //    .SendAsync("ReceiveComment", comment.UserId, comment.Content);
                return RedirectToAction("Details", "News", new { id = commentViewModel.NewsArticleId });
            }
        return RedirectToAction("Details", "News", new { id = commentViewModel.NewsArticleId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                TempData["ErrorMessage"] = "Comment content cannot be empty.";
                return RedirectToAction("Details", "News", new { id });
            }

            try
            {
                var updatedComment = await _commentService.EditCommentAsync(id, content);
                if (updatedComment == null)
                {
                    TempData["ErrorMessage"] = "Comment not found or you don't have permission to edit it.";
                    return RedirectToAction("Details", "News", new { id });
                }

                TempData["SuccessMessage"] = "Comment updated successfully.";
                return RedirectToAction("Details", "News", new { id = updatedComment.NewsArticleId });
            } catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the comment.";
                Console.WriteLine($"Error in Edit action: {ex.Message}");
                return RedirectToAction("Details", "News", new { id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.RemoveCommentAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
