using Blog.IRepository;
using Blog.Model;
using SqlSugar;
using SqlSugar.IOC;
using System.Linq.Expressions;

namespace Blog.Repository
{
    public class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            base.Context = DbScoped.Sugar;
            //第一次运行创建数据库和表之后需注释下面的代码
            /*base.Context.DbMaintenance.CreateDatabase();
            base.Context.CodeFirst.InitTables(typeof(BlogNews), typeof(BlogType), typeof(Author));*/
        }

        public async Task<bool> AddAsync(T enity)
        {
            return await base.InsertAsync(enity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(T enity)
        {
            return await base.UpdateAsync(enity);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> func)
        {
            return await base.GetFirstAsync(func);
        }

        /// <summary>
        /// 该方法加virtual修饰，是为了子类可以重写方法返回自定义属性字段的字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>T</returns>
        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<List<T>> PageAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<T>().ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<T>> PageAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total)
        {
            PageModel model = new PageModel();
            model.PageIndex = page;
            model.PageSize = size;
            model.TotalCount = total;
            return await base.GetPageListAsync(func, model);
        }

        public virtual async Task<List<T>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func)
        {
            return await base.GetListAsync(func);
        }
    }
}
