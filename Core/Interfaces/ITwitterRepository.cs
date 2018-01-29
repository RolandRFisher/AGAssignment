using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITwitterRepository
    {
        ICollection<Users> GetUsers();
        IEnumerable<Tweet> GetTweets();
    }
}
