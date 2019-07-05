using System.Collections.Generic;
using Fg.Blog.Model.Base;

namespace Fg.Blog.Model
{
    public class User : IEntityBase
    {
        public User()
        {
            Blogs = new List<Blog>();
            Comments = new List<Comment>();
        }
        public long Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Blog> Blogs { get; set; }
        public List<Comment> Comments { get; set; }

    }
}