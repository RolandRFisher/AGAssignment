using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITwitterRepository
    {
        ICollection<Users> GetUsers();
        IEnumerable<Tweet> GetTweets();
    }
}
