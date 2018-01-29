using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IDalTweets
    {
        IEnumerable<Tweet> GetTweets();
    }
}