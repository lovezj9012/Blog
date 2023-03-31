
using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class BlogTypeService : BaseService<BlogType>, IBlogTypeService
    {
        private readonly IBlogTypeRepository blogTypeRepository;

        public BlogTypeService(IBlogTypeRepository _blogTypeRepository)
        {
            base.baseRepository = _blogTypeRepository;
            this.blogTypeRepository = _blogTypeRepository;
        }
    }
}
