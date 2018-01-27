using System.Collections.Generic;

namespace Core.Models
{
        public class Users
        {
            public string UserId { get; set; }
            public List<string> Follows { get; set; }
        }
}
