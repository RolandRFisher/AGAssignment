using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Service.Twitter;

namespace AGAssignment
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();



            var tSvc = new TwitterService();
            tSvc.PrintReport();

            


            var t = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
            var format = $"{t.TotalHours:00}:{t.TotalMinutes:00}:{t.TotalSeconds:00}.{t.TotalMilliseconds:00}";
            Console.WriteLine(format);
            sw.Stop();
        }
    }
}
