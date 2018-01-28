using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Service.Twitter
{
    public class Report:IReport
    {
        private readonly ITwitterRepository _twitterRepository;

        public Report(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        #region public methods

        public IEnumerable<TwitterModel> GenerateReport()
        {
            var userList = _twitterRepository.GetUsers();
            var tweets = _twitterRepository.GetTweets();


            return GenerateReport(userList.OrderBy(users => users.UserId).ToList(), tweets);
        } 

        #endregion

        #region private methods

        private static IEnumerable<TwitterModel> GenerateReport(IEnumerable<Users> userList, IEnumerable<Tweet> tweets)
        {
            var twitterReport = new List<TwitterModel>();
            if (tweets == null) return twitterReport;


            foreach (var user in userList)
            {
                var followsList = user.Follows;
                var report = new TwitterModel { UserId = user.UserId };

                var twts = new List<Tweet>();
                foreach (var userTweet in tweets)
                {
                    if (userTweet.UserId == user.UserId)
                    {
                        twts.Add(new Tweet() { UserId = userTweet.UserId, UserTweet = userTweet.UserTweet });
                    }

                    foreach (var followedUser in followsList)
                    {
                        if (followedUser == userTweet.UserId)
                        {
                            twts.Add(new Tweet() { UserId = userTweet.UserId, UserTweet = userTweet.UserTweet });
                        }
                    }
                }

                report.Tweets = twts;
                twitterReport.Add(report);
            }

            return twitterReport;
        } 

        #endregion
    }
}
