using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Repository.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {

        public TwitterRepository()
        {
            
        }

        private static IEnumerable<string> GetUsersFromSource()
        {
            //TODO: Get Data from Generic DataSource
            ICollection<string> users = new Collection<string>();
            users.Add("Ward follows Alan");
            users.Add("Alan follows Martin");
            users.Add("Ward follows Martin, Alan");

            return users;
        }


        public ICollection<Users> GetUsers()
        {
            var result = new List<Users>();
            var allUsers = GetAllUsers();
            if (allUsers == null) return result;


            result = GetUsersWithFollowers(allUsers);


            var followers = allUsers.Select(users => users.UserId).Distinct();
            var followed = result.Select(users => users.Follows).Distinct().SelectMany(fu => fu);
            var doNotFollowAnyone = followed.Except(followers).OrderBy(s => s).ToList();
            foreach (var userId in doNotFollowAnyone)
            {
                result.Add(new Users() { UserId = userId, Follows = new List<string>() });
            }
            return result;
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

        private static List<Users> GetUsersWithFollowers(IEnumerable<Users> users)
        {
            return (List<Users>)UsersWithFollowers(users);
        }

        private static IEnumerable<Users> GetAllUsers()
        {
            var result = new List<Users>();

            var users = GetUsersFromSource();
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

        public IEnumerable<Tweet> GetTweets()
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
        
    }
}
