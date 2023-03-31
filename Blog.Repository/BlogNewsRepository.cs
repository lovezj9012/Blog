using Blog.IRepository;
using Blog.Model;

namespace Blog.Repository
{
    public class BlogNewsRepository : BaseRepository<BlogNews>, IBlogNewsRepository
    {
    }
}
