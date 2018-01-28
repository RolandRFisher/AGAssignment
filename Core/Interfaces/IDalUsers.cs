using System.Collections.Generic;
using Core.Models;

namespace Repository.Repositories
{
    public interface IDalUsers
    {
        ICollection<Users> GetUsers();
    }
}