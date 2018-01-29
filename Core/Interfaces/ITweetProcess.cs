using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITweetProcess
    {
        IEnumerable<Tweet> GetTweets(IEnumerable<string> line);
    }
}