using ChatApp.Core.Infrastructure.Attributes;
using PetaPoco;
using System;

namespace ChatApp.Core.Models.Entity
{
    [TableName("Users")]
    [PrimaryKey("UserId")]
    [Sort("UserId", "ASC")]
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ResultColumn]
        public bool IsOnline { get; set; }
    }
}
