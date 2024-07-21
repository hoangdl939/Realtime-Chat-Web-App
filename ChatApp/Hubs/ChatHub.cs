using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
       // private static readonly ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

       /* public override async Task OnConnectedAsync()
        {
            if (Context.UserIdentifier == null) return;
            string userId = Context.UserIdentifier;
            UserConnections[Context.ConnectionId] = userId;

            
            await Clients.All.SendAsync("UserConnected", userId);

            await base.OnConnectedAsync();
        }*/

        /*public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (UserConnections.TryRemove(Context.ConnectionId, out string userId))
            {
                
                await Clients.All.SendAsync("UserDisconnected", userId);
            }

            await base.OnDisconnectedAsync(exception);
        }*/

        /*public static IEnumerable<string> GetOnlineUsers()
        {
            return UserConnections.Values.Distinct();
        }*/

        public async Task SendMessage(string receiverId, string messageText)
        {
            var senderId = Context.UserIdentifier;
            await _chatService.SaveMessageAsync(senderId, receiverId, messageText);

            var timestamp = DateTime.UtcNow.ToString("o");
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, messageText, timestamp);
        }
    }
}
