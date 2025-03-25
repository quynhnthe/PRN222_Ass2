using Microsoft.AspNetCore.SignalR;

namespace NewsManagementSystem_Assigment01.Hubs
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(string action, int newsId)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", action, newsId);
        }
        public async Task SendArticleNotification(string username, string title)
        {
            await Clients.All.SendAsync("ReceiveNotification", username, title);
        }
    }
}
