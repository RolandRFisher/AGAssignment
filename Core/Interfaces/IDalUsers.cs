using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IDalUsers
    {
        ICollection<Users> GetUsers();
        IEnumerable<Users> BindUserModel(IEnumerable<string> usrs);
        List<Users> GetUsersWithFollowers(IEnumerable<Users> users);
        IEnumerable<string> GetUsersThatDoNotFollowAnyone(IEnumerable<Users> allUsers, IEnumerable<Users> result);
    }
}