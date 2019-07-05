 

namespace Fg.Blog.Data.Repository
{
    public interface IBlogRepository: IEntityBaseRepository<Model.Blog>
    {
        bool IsOwner(long blogId, long userId); 
    }
}