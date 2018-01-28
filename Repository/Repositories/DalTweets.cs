using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

        public IEnumerable<Tweet> GetTweets()
        {
            ////TODO: Get Data from Generic DataSource
            //ICollection<string> lines = new Collection<string>
            //{
            //    "Alan> If you have a procedure with 10 parameters, you probably missed some.",
            //    "Ward> There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors.",
            //    "Alan> Random numbers should not be generated with a method chosen at random."
            //};

            var tweets = _textfile.GetData(_connectionstring);
            return GetTweets(tweets);
        }

        private static IEnumerable<Tweet> GetTweets(IEnumerable<string> line)
        {
            var result = new List<Tweet>();

            const string gtIdentifier = "> ";
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