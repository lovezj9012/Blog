using Blog.IRepository;
using Blog.Model;

namespace Blog.Repository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
    }
}
