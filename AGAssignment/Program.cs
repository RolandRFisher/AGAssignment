using System;
using System.Diagnostics;
using Autofac;
using Core.Interfaces;
using Repository.Repositories;
using Service.Twitter;

namespace AGAssignment
{
    public class Program
    {
        //TODO: Error Handling
        //TODO: ReportingService - Method for Generating and writing to Console/Database/File etc...
        //TODO: Create abstract DAL  
        //TODO: Create unit tests

        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TwitterService>().As<ITwitterService>();
            builder.RegisterType<TwitterRepository>().As<ITwitterRepository>();
            Container = builder.Build();
            
            using (var scope = Container.BeginLifetimeScope())
            {
                var twitterService = scope.Resolve<ITwitterService>();
                //TODO: change method call to Write(Enum target)
                twitterService.PrintReport();
            }
        }
    }
}
