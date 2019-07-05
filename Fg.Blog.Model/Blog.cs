using System.Collections.Generic;
using Fg.Blog.Model.Base;

namespace Fg.Blog.Model
{
    public class Blog : IEntityBase
    {
        public Blog()
        {
            Comments = new List<Comment>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } 
        public long CreationTime { get; set; }
        public long LastEditTime { get; set; }
        public long PublishTime { get; set; }
        public bool Draft { get; set; }

        public User Owner { get; set; }
        public long OwnerId { get; set; }

        public List<Comment> Comments { get; set; }

    }
}