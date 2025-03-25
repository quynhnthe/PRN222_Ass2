using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewsManagementSystem_Assigment01.Hubs;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Services;

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
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var newComment = await _commentService.CreateCommentAsync(comment);
                await _hubContext.Clients.Group($"post-{comment.NewsArticleId}")
                    .SendAsync("ReceiveComment", comment.UserId, comment.Content);
                return RedirectToAction("Details", "Post", new { id = comment.NewsArticleId });
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string content)
        {
            var updatedComment = await _commentService.EditCommentAsync(id, content);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Post", new { id = updatedComment.NewsArticleId });
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
