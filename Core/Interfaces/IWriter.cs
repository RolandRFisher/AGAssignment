using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IWriter
    {
        void Print(IEnumerable<TwitterModel> twitterReport);
    }
}