using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGAssignment;
using Core.Interfaces;
using Core.Models;

namespace Service.Twitter
{
    public class TwitterService: ITwitterService
    {
        private readonly ITwitterRepository _twitterRepository;

        public TwitterService(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        #region public methods

        public void PrintReport()
        {
            var twitterReport = GenerateReport();

            foreach (var report in twitterReport)
            {
                var userId = report.UserId;
                Console.WriteLine($"{userId.Add(Util.EoL, 1)}");
                foreach (var reportTweet in report.Tweets)
                {
                    Console.WriteLine($"{"".Add(Util.Tb, 1)}@{reportTweet.UserId}: {reportTweet.UserTweet}");
                }
            }
        } 

        #endregion

        #region private methods

        private IEnumerable<TwitterModel> GenerateReport()
        {
            var userList = _twitterRepository.GetUsers();
            var tweets = _twitterRepository.GetTweets();


            return GenerateReport(userList.OrderBy(users => users.UserId).ToList(), tweets);
        }

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
