using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string receiverId, string messageText)
        {
            var senderId = Context.UserIdentifier;
            await _chatService.SaveMessageAsync(senderId, receiverId, messageText);

            var timestamp = DateTime.UtcNow.ToString("o");
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, messageText, timestamp);
        }
    }
}
