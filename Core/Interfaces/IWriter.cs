using System.Collections.Generic;
using Core.Models;

namespace Service.Twitter
{
    public interface IWriter
    {
        void Print(IEnumerable<TwitterModel> twitterReport);
    }
}