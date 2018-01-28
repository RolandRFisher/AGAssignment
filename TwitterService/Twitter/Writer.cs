using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGAssignment;
using Core.Models;

namespace Service.Twitter
{
    public class Writer:IWriter
    {

        #region public methods

        public void Print(IEnumerable<TwitterModel> twitterReport)
        {
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

    }
}
