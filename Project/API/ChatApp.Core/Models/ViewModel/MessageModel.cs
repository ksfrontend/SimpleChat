using ChatApp.Core.Models.Entity;

namespace ChatApp.Core.Models.ViewModel
{
    public class MessageRequestModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }

    public class MessageViewModel : Message
    {
    }

    public class MessageListModel : Message
    {
        public int Count { get; set; }
    }
}
