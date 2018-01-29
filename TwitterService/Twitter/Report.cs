using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Service.Twitter
{
    public class Report:IReport
    {
        private readonly ITwitterRepository _twitterRepository;
        private int _tweetLengthLimit;

        public Report(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
            _tweetLengthLimit = Convert.ToInt32(ConfigurationManager.AppSettings["LimitTweetTo"]);
        }

        #region public methods

        public IEnumerable<TwitterModel> GetReport()
        {
            var userList = _twitterRepository.GetUsers();
            var tweets = _twitterRepository.GetTweets();


            return GenerateReport(userList.OrderBy(users => users.UserId).ToList(), tweets, _tweetLengthLimit);
        }

        public IEnumerable<TwitterModel> GenerateReport(IEnumerable<Users> userList, IEnumerable<Tweet> tweets, int tweetLengthLimit)
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
                    var uTweet = userTweet.UserTweet.Substring(0, Math.Min(tweetLengthLimit, userTweet.UserTweet.Length));

                    if (userTweet.UserId == user.UserId)
                    {
                        twts.Add(new Tweet() { UserId = userTweet.UserId, UserTweet = uTweet });
                    }

                    foreach (var followedUser in followsList)
                    {
                        if (followedUser == userTweet.UserId)
                        {
                            twts.Add(new Tweet() { UserId = userTweet.UserId, UserTweet = uTweet });
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
