using System;
using System.Collections.Generic;
using AGAssignment;
using Core.Interfaces;
using Core.Models;

namespace Repository.Repositories
{
    public class TweetProcess : ITweetProcess
    {
        public IEnumerable<Tweet> GetTweets(IEnumerable<string> line)
        {
            var result = new List<Tweet>();

            var gtIdentifier = Util.Gt;
            const int offset = 2;

            foreach (var l in line)
            {
                var lLength = l.Length;
                var gtIndex = l.IndexOf(gtIdentifier, StringComparison.InvariantCultureIgnoreCase);
                var i = lLength - (gtIndex + offset);
                var userTweet = l.Substring(gtIndex + offset, i);

                var userId = l.Substring(0, gtIndex);

                result.Add(new Tweet() { UserId = userId, UserTweet = userTweet });
            }
            return result;
        }
    }
}