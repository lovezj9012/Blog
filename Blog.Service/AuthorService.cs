using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository _authorRepository)
        {
            base.baseRepository = _authorRepository;
            this.authorRepository = _authorRepository;
        }
    }
}
