using Fg.Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fg.Blog.Api.Migrations
{
    public static class DbSeedExtension
    {
        public static void Seed(this BlogContext ctx)
        {
            if (!ctx.Users.Any())
            {
                ctx.Users.Add(new Model.User()
                {
                    Username = "Admin",
                    Email = "Admin@Blog.com",
                    Password = "AQAAAAEAACcQAAAAEPJ/PflGvBD5oUn2D+Yi+4u4jZCLH6eXxIN8jQLrQ6gOCbG7Sp75WEja1YCmYzwVEw=="

                });
                ctx.Users.Add(new Model.User()
                {
                    Username = "Fatih",
                    Email = "Fatih@Blog.com",
                    Password = "AQAAAAEAACcQAAAAEPJ/PflGvBD5oUn2D+Yi+4u4jZCLH6eXxIN8jQLrQ6gOCbG7Sp75WEja1YCmYzwVEw=="

                });

                ctx.SaveChanges();
            }

            if (!ctx.Blogs.Any())
            {
                ctx.Blogs.Add(new Model.Blog()
                {
                    Title = "Test",
                    CreationTime = DateTime.Now.Ticks,
                    LastEditTime = DateTime.Now.Ticks,
                    OwnerId = 1,
                    Draft = false,
                    PublishTime = DateTime.Now.Ticks,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."

                });

                ctx.Blogs.Add(new Model.Blog()
                {
                    Title = "Test1",
                    CreationTime = DateTime.Now.Ticks,
                    LastEditTime = DateTime.Now.Ticks,
                    OwnerId = 1,
                    Draft = false,
                    PublishTime = DateTime.Now.Ticks,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."

                });
                ctx.Blogs.Add(new Model.Blog()
                {
                    Title = "Test2",
                    CreationTime = DateTime.Now.Ticks,
                    LastEditTime = DateTime.Now.Ticks,
                    OwnerId = 1,
                    Draft = false,
                    PublishTime = DateTime.Now.Ticks,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."

                });

                ctx.SaveChanges();
            }
        }
    }
}
