using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using System.Configuration;
using Repository.DAL;

namespace Repository.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly IDalUsers _dalUsers;
        private readonly IDalTweets _dalTweets;

        public TwitterRepository(IDalUsers dalUsers, IDalTweets dalTweets)
        {
            _dalUsers = dalUsers;
            _dalTweets = dalTweets;
        }

        #region public methods

        public ICollection<Users> GetUsers()
        {
            return _dalUsers.GetUsers();
        }


        public IEnumerable<Tweet> GetTweets()
        {
            return _dalTweets.GetTweets();
        } 

        #endregion
    }
}
