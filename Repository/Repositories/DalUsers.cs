using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Models;
using Repository.DAL;

namespace Repository.Repositories
{
    public class DalUsers:IDalUsers
    {
        private readonly ITextFIle _textfile;
        private readonly string _connectionstring;

        public DalUsers(ITextFIle textFile)
        {
            _textfile = textFile;
            _connectionstring = ConfigurationManager.AppSettings["UserFIle"];
        }

        public ICollection<Users> GetUsers()
        {
            var result = new List<Users>();

            var usrs = _textfile.GetData(_connectionstring);
            var allUsers = GetAllUsers(usrs);


            if (allUsers == null) return result;


            result = GetUsersWithFollowers(allUsers);


            var doNotFollowAnyone = GetUsersThatDoNotFollowAnyone(allUsers, result);
            foreach (var userId in doNotFollowAnyone)
            {
                result.Add(new Users() { UserId = userId, Follows = new List<string>() });
            }


            return result;
        }

        private static IEnumerable<Users> GetAllUsers(IEnumerable<string> usrs)
        {
            var result = new List<Users>();

            var userFollowerDelimiter = " follows ";
            var followerDelimiter = ", ";
            var orderBy = usrs.OrderBy(s => s).ToList();
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

        private static List<Users> GetUsersWithFollowers(IEnumerable<Users> users)
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

        private static IEnumerable<string> GetUsersThatDoNotFollowAnyone(IEnumerable<Users> allUsers, IEnumerable<Users> result)
        {
            var followers = allUsers.Select(users => users.UserId).Distinct();
            var followed = result.Select(users => users.Follows).Distinct().SelectMany(fu => fu);
            var doNotFollowAnyone = followed.Except(followers).OrderBy(s => s).ToList();
            return doNotFollowAnyone;
        }
    }
}