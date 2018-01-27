using System;
using System.Diagnostics;
using Autofac;
using Core.Interfaces;
using Service.Twitter;

namespace AGAssignment
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<TwitterService>().As<ITwitterService>();
            Container = builder.Build();
            

            var sw = new Stopwatch();
            sw.Start();


            using (var scope = Container.BeginLifetimeScope())
            {
                var execute = scope.Resolve<ITwitterService>();
                execute.PrintReport();
            }

            


            var t = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
            var format = $"{t.TotalHours:00}:{t.TotalMinutes:00}:{t.TotalSeconds:00}.{t.TotalMilliseconds:00}";
            Console.WriteLine(format);
            sw.Stop();
        }
        


    }
}
