using System.Collections.Generic;

namespace Core.Models
{
    public class TwitterModel
    {
        public string UserId { get; set; }
        public List<Tweet> Tweets { get; set; }
    }
}