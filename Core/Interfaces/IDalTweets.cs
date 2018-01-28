using System.Collections.Generic;
using Core.Models;

namespace Repository.Repositories
{
    public interface IDalTweets
    {
        IEnumerable<Tweet> GetTweets();
    }
}