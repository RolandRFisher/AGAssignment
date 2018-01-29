using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using AGAssignment;
using Core.Interfaces;
using Core.Models;
using Repository.DAL;

namespace Repository.Repositories
{
    public class DalTweets: IDalTweets
    {
        private readonly ITextFIle _textfile;
        private readonly string _connectionstring;
        private readonly ITweetProcess _tweetProcess;

        public DalTweets(ITextFIle textFile, ITweetProcess tweetProcess)
        {
            _textfile = textFile;
            _connectionstring = ConfigurationManager.AppSettings["TweetFile"];
            _tweetProcess = tweetProcess;
        }

        #region public methods

        public IEnumerable<Tweet> GetTweets()
        {
            var tweets = _textfile.GetData(_connectionstring);
            return _tweetProcess.GetTweets(tweets);
        }

        #endregion

        #region private methods

        #endregion
    }
}