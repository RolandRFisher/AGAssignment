using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IReport
    {
        IEnumerable<TwitterModel> GetReport();
        IEnumerable<TwitterModel> GenerateReport(IEnumerable<Users> userList, IEnumerable<Tweet> tweets, int tweetLengthLimit);
    }
}