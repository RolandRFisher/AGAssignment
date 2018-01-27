using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITwitterService
    {
        IEnumerable<TwitterModel> GenerateReport();
    }
}