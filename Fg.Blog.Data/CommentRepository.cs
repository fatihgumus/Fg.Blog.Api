

using Fg.Blog.Data.Repository;
using Fg.Blog.Model;

namespace Fg.Blog.Data.Repositories {
    public class CommentRepository : EntityBaseRepository<Comment>, ICommentRepository {
        public CommentRepository (BlogContext context) : base (context) { }

         
    }
}