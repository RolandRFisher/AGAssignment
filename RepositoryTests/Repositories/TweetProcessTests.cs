using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Repositories;

namespace RepositoryTests.Repositories
{
    [TestClass()]
    public class TweetProcessTests
    {
        [TestMethod()]
        public void GetTweetsTest()
        {
            //Arrange
            var tp = new TweetProcess();
            var firstTweet = @"If you have a procedure with 10 parameters, you probably missed some.";
            var secondTweet = @"There are only two hard things in Computer Science: cache invalidation, naming things and off - by - 1 errors.";
            var thirdTweet = @"Random numbers should not be generated with a method chosen at random.";
            IEnumerable<string> lines = new List<string>()
            {
                $"Alan> {firstTweet}",
                $"Ward> {secondTweet}",
                $"Alan> {thirdTweet}"
            };

            //Act
            var sut = tp.GetTweets(lines).ToList();

            //Assert
            Assert.IsTrue(sut.Any());
            Assert.IsTrue(sut[0].UserId == "Alan");
            Assert.IsTrue(sut[1].UserId == "Ward");
            Assert.IsTrue(sut[2].UserId == "Alan");
            Assert.IsTrue(string.Equals(sut[0].UserTweet,firstTweet,StringComparison.Ordinal));
            Assert.IsTrue(string.Equals(sut[1].UserTweet,secondTweet,StringComparison.Ordinal));
            Assert.IsTrue(string.Equals(sut[2].UserTweet,thirdTweet,StringComparison.Ordinal));
        }
    }
}