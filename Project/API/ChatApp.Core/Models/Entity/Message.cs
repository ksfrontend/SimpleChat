using ChatApp.Core.Infrastructure.Attributes;
using PetaPoco;
using System;

namespace ChatApp.Core.Models.Entity
{
    [TableName("Messages")]
    [PrimaryKey("MessageId")]
    [Sort("MessageId", "ASC")]
    public class Message
    {
        public long MessageId { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string TextMessage { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
