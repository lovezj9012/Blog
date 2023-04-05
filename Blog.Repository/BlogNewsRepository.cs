using Blog.IRepository;
using Blog.Model;
using SqlSugar;
using System.Linq.Expressions;

namespace Blog.Repository
{
    public class BlogNewsRepository : BaseRepository<BlogNews>, IBlogNewsRepository
    {
        public override async Task<List<BlogNews>> QueryAsync()
        {

            return await base.Context.Queryable<BlogNews>().Mapper(b => b.BlogType, b => b.TypeId, b => b.BlogType.Id).Mapper(b => b.Author, b => b.AuthorId, b => b.Author.Id).ToListAsync();
        }

        public override async Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(b => b.Author, b => b.AuthorId, b => b.Author.Id).Mapper(b => b.BlogType, b => b.TypeId, b => b.BlogType.Id).ToListAsync();
        }

        public override async Task<List<BlogNews>> PageAsync(Expression<Func<BlogNews, bool>> func, PageModel model)
        {
            RefAsync<int> count = 0;
            List<BlogNews> result = await base.Context.Queryable<BlogNews>().Where(func)
                .Mapper(b => b.Author, b => b.AuthorId, b => b.Author.Id).Mapper(b => b.BlogType, b => b.TypeId, b => b.BlogType.Id).ToPageListAsync(model.PageIndex, model.PageSize, count);
            model.TotalCount = count;
            return result;
        }

        public override async Task<List<BlogNews>> PageAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(b => b.Author, b => b.AuthorId, b => b.Author.Id).Mapper(b => b.BlogType, b => b.TypeId, b => b.BlogType.Id).ToListAsync(); ;
        }
    }
}
