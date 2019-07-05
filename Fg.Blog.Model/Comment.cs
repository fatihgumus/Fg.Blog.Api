
using Fg.Blog.Model.Base;

namespace Fg.Blog.Model
{
    public class Comment :IEntityBase
    {
        public long Id { get; set; }
        public long BlogId { get; set; }
        public Blog Blog { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long CreationTime { get; set; }
        public string Content { get; set; }
      
    }
}