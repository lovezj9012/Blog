using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository blogNewsRepository;
        public BlogNewsService(IBlogNewsRepository _blogNewsRepository)
        {
            base.baseRepository = _blogNewsRepository;
            this.blogNewsRepository = _blogNewsRepository;
        }
    }
}
