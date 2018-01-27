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
    public class TwitterService:ITwitterService, IReportService
    {
        public TwitterService()
        {
            
        }

        public IEnumerable<TwitterModel> GenerateReport()
        {
            var userSource = GetUsersFromSource();
            var tempList = GetAllUsers(userSource);

            var userList = GetUserList(tempList);

            //TODO: Get a distint list of people following and a list of people being followed
            var followers = tempList.Select(users => users.UserId).Distinct();
            var followed = userList.Select(users => users.Follows).Distinct().SelectMany(fu => fu);

            var doNotFollowAnyone = followed.Except(followers).OrderBy(s => s).ToList();

            foreach (var user in doNotFollowAnyone)
            {
                userList.Add(new Users() { UserId = user, Follows = new List<string>() });
            }


            var tweets = GetTweetsFromSource();
            var twitterReport = GetTwitterReport(userList.OrderBy(users => users.UserId).ToList(), tweets);
            return twitterReport;
        }

        private static ICollection<string> GetUsersFromSource()
        {
            //TODO: Get Data from Generic DataSource
            ICollection<string> users = new Collection<string>();
            users.Add("Ward follows Alan");
            users.Add("Alan follows Martin");
            users.Add("Ward follows Martin, Alan");

            return users;
        }

        private static IList<Users> GetAllUsers(IEnumerable<string> users)
        {
            var result = new List<Users>();

            var userFollowerDelimiter = " follows ";
            var followerDelimiter = ", ";
            var orderBy = users.OrderBy(s => s).ToList();
            foreach (var t in orderBy)
            {
                var u = new Users();
                var strings = t.Split(new[] { userFollowerDelimiter }, StringSplitOptions.None);
                u.UserId = strings[0].Trim();
                u.Follows = strings[1].Split(new[] { followerDelimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                result.Add(u);
            }

            return result;
        }

        private static List<Users> GetUserList(IEnumerable<Users> users)
        {
            return (List<Users>)UsersWithFollowers(users);
        }

        private static IEnumerable<Users> UsersWithFollowers(IEnumerable<Users> userList)
        {
            var result = new List<Users>();
            if (userList == null) return result;

            foreach (var userId in userList.Select(users1 => users1.UserId).Distinct())
            {
                var follows = userList.Where(users1 => users1.UserId == userId)
                    .SelectMany(users1 => users1.Follows)
                    .Distinct();
                result.Add(new Users() { UserId = userId, Follows = follows.ToList() });
            }

            return result;
        }

        private static IEnumerable<Tweet> GetTweetsFromSource()
        {
            //TODO: Get Data from Generic DataSource
            ICollection<string> lines = new Collection<string>
            {
                "Alan> If you have a procedure with 10 parameters, you probably missed some.",
                "Ward> There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors.",
                "Alan> Random numbers should not be generated with a method chosen at random."
            };

            return GetTweets(lines);
        }

        private static IEnumerable<Tweet> GetTweets(ICollection<string> line)
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

        private static List<TwitterModel> GetTwitterReport(IList<Users> userList, IEnumerable<Tweet> tweets)
        {
            var twitterReport = new List<TwitterModel>();
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


        public void PrintReport()
        {
            var twitterReport = GenerateReport();

            foreach (var report in twitterReport)
            {
                var userId = report.UserId;
                Console.WriteLine($"{userId.Add(Util.EoL, 1)}");
                foreach (var reportTweet in report.Tweets)
                {
                    Console.WriteLine($"{"".Add(Util.Tb, 2)}@{reportTweet.UserId}: {reportTweet.UserTweet}");
                }
            }

        }

    }
}
