﻿using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services
{
    public class ChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(string senderId, string receiverId, string messageText)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = messageText,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(string currentUserId, string receiverId)
        {
            var messages =  await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            foreach (var message in messages)
            {
                // Chuyển đổi thời gian từ UTC sang múi giờ hiện tại
                message.Timestamp = TimeZoneInfo.ConvertTimeFromUtc((DateTime)message.Timestamp, TimeZoneInfo.Local);
            }

            return messages;
        }
    }
}
