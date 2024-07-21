using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Pages.Chats
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IdentityDbContext _identityContext;
        private readonly ChatService _chatService;

        public IndexModel(IdentityDbContext identityContext, ChatService chatService)
        {
            _identityContext = identityContext;
            _chatService = chatService;
        }

        public List<IdentityUser> Users { get; set; }
        public List<Message> Messages { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _identityContext.Users.ToListAsync();
        }

        public async Task<IActionResult> OnGetMessagesAsync(string receiverId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Messages = await _chatService.GetMessagesAsync(currentUserId, receiverId);
            return new JsonResult(Messages);
        }
    }
}
