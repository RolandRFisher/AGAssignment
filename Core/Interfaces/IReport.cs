using System.Collections.Generic;
using Core.Models;

namespace Service.Twitter
{
    public interface IReport
    {
        IEnumerable<TwitterModel> GenerateReport();
    }
}