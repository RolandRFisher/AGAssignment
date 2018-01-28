using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using AGAssignment;
using Core.Models;
using Repository.DAL;

namespace Repository.Repositories
{
    public class DalTweets: IDalTweets
    {
        private readonly ITextFIle _textfile;
        private readonly string _connectionstring;

        public DalTweets(ITextFIle textFile)
        {
            _textfile = textFile;
            _connectionstring = ConfigurationManager.AppSettings["TweetFile"];
        }

        #region public methods

        public IEnumerable<Tweet> GetTweets()
        {
            var tweets = _textfile.GetData(_connectionstring);
            return GetTweets(tweets);
        }

        #endregion

        #region private methods

        private static IEnumerable<Tweet> GetTweets(IEnumerable<string> line)
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

        #endregion
    }
}