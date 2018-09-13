using System;
using System.ComponentModel;
using System.Diagnostics;
using Autofac;
using Core.Interfaces;
using NLog;
using Repository.DAL;
using Repository.Repositories;
using Service.Twitter;
using IContainer = System.ComponentModel.IContainer;

namespace AGAssignment
{
    public class Program
    {
        //TODO: Create unit tests

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static Autofac.IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TwitterService>().As<ITwitterService>();
            builder.RegisterType<TwitterRepository>().As<ITwitterRepository>();
            builder.RegisterType<TextFile>().As<ITextFIle>();
            builder.RegisterType<DalUsers>().As<IDalUsers>();
            builder.RegisterType<DalTweets>().As<IDalTweets>();
            builder.RegisterType<Report>().As<IReport>();
            builder.RegisterType<Writer>().As<IWriter>();
            builder.RegisterType<TweetProcess>().As<ITweetProcess>();
            Container = builder.Build();
            
            using (var scope = Container.BeginLifetimeScope())
            {
                try
                {
                    var twitterService = scope.Resolve<ITwitterService>();
                    twitterService.PrintReport();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }
    }
}
