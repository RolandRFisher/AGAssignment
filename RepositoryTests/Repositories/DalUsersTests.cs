using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DAL;
using Repository.Repositories;

namespace Repository.Repositories.Tests
{

    namespace RepositoryTests.Repositories
    {
        [TestClass()]
        public class DalUsersTests
        {
            [TestMethod()]
            public void BindUserModelTest()
            {
                //Arrange
                IEnumerable<string> input = new List<string>()
                {
                    "Ward follows Alan",
                    "Alan follows Martin",
                    "Ward follows Martin, Alan"
                };

                var obj = new DalUsers(new TextFile());

                //Act
                var sut = obj.BindUserModel(input).ToList();

                //Assert
                Assert.IsTrue(sut.Any());
                Assert.IsTrue(sut[0].UserId == "Alan");
                Assert.IsTrue(sut[1].UserId == "Ward");
                Assert.IsTrue(sut[2].UserId == "Ward");
                Assert.IsTrue(sut[0].Follows.Count == 1);
                Assert.IsTrue(sut[1].Follows.Count == 1);
                Assert.IsTrue(sut[2].Follows.Count == 2);
            }

            [TestMethod()]
            public void GetUsersWithFollowersTest()
            {
                //Arrange
                var input = GetNotFilteredUsers();

                var obj = new DalUsers(new TextFile());

                //Act
                var sut = obj.GetUsersWithFollowers(input).ToList();

                //Assert
                Assert.IsTrue(sut.Any());
                Assert.IsTrue(sut.Count == 2);
                Assert.IsTrue(sut[0].UserId == "Alan");
                Assert.IsTrue(sut[1].UserId == "Ward");
                Assert.IsTrue(sut[0].Follows.Count == 1);
                Assert.IsTrue(sut[1].Follows.Count == 2);
            }

            private static IEnumerable<Users> GetNotFilteredUsers()
            {
                IEnumerable<Users> input = new List<Users>()
                {
                    new Users()
                    {
                        UserId = "Alan",
                        Follows = new List<string>()
                        {
                            "Martin"
                        }
                    },
                    new Users()
                    {
                        UserId = "Ward",
                        Follows = new List<string>()
                        {
                            "Alan"
                        }
                    },
                    new Users()
                    {
                        UserId = "Ward",
                        Follows = new List<string>()
                        {
                            "Martin",
                            "Alan"
                        }
                    }
                };
                return input;
            }

            private static IEnumerable<Users> GetUsersWithFolowers()
            {
                IEnumerable<Users> input = new List<Users>()
                {
                    new Users()
                    {
                        UserId = "Alan",
                        Follows = new List<string>()
                        {
                            "Martin"
                        }
                    },
                    new Users()
                    {
                        UserId = "Ward",
                        Follows = new List<string>()
                        {
                            "Alan",
                            "Martin"
                        }
                    }
                };
                return input;
            }

            [TestMethod()]
            public void GetUsersThatDoNotFollowAnyoneTest()
            {
                //Arrange
                var notFilteredUsers = GetNotFilteredUsers();
                var userWithFollowers = GetUsersWithFolowers();
                var expected = "Martin";

                var obj = new DalUsers(new TextFile());

                //Act
                var sut = obj.GetUsersThatDoNotFollowAnyone(notFilteredUsers, userWithFollowers).ToList();

                //Assert
                Assert.IsTrue(sut.Any());
                Assert.IsTrue(sut.Count == 1);
                Assert.IsTrue(string.Equals(sut[0],expected,StringComparison.CurrentCulture));
            }
        }
    }
}