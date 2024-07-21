using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class Message
{
    public int Id { get; set; }

    public string? SenderId { get; set; }

    public string? ReceiverId { get; set; }

    public string? MessageText { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual AspNetUser? Receiver { get; set; }

    public virtual AspNetUser? Sender { get; set; }
}
