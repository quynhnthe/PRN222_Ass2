using Microsoft.AspNetCore.SignalR;

namespace NewsManagementSystem_Assigment01.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(int postId, string user, string message)
        {
            await Clients.Group($"post-{postId}").SendAsync("ReceiveComment", user, message);
        }

        public async Task JoinPostGroup(int postId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"post-{postId}");
        }
    }
}
