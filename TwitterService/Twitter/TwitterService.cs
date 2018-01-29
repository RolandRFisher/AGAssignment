using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGAssignment;
using Core.Interfaces;
using Core.Models;

namespace Service.Twitter
{
    public class TwitterService: ITwitterService
    {
        private readonly IReport _report;
        private readonly IWriter _writer;

        public TwitterService(IReport report, IWriter writer)
        {
            _report = report;
            _writer = writer;
        }

        #region public methods

        public void PrintReport()
        {
            var twitterReport = _report.GetReport();

            _writer.Print(twitterReport);
        }

        #endregion
        
    }
}
