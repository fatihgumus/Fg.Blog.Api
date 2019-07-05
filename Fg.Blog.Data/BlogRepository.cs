

using Fg.Blog.Data.Repository;

namespace Fg.Blog.Data.Repositories
{
    public class BlogRepository : EntityBaseRepository<Model.Blog>, IBlogRepository 
    {
        public BlogRepository(BlogContext context) : base (context) { }

       

        public bool IsOwner(long blogId, long userId)
        {
            var blog = this.GetSingle(s => s.Id == blogId);
            return blog.OwnerId == userId;
        }
  }
}