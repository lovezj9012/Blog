using SqlSugar;
using System.Linq.Expressions;

namespace Blog.IRepository
{
    public interface IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <param name="enity">实体</param>
        /// <returns>bool</returns>
        Task<bool> AddAsync(T enity);

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 异步编辑数据
        /// </summary>
        /// <param name="enity">实体</param>
        /// <returns>bool</returns>
        Task<bool> EditAsync(T enity);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>T</returns>
        Task<T> FindByIdAsync(int id);

        /// <summary>
        /// 根据条件异步查询数据
        /// </summary>
        /// <param name="func">条件表达式</param>
        /// <returns>T</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> func);

        /// <summary>
        /// 异步查询全部数据
        /// </summary>
        /// <returns>List<T></returns>
        Task<List<T>> QueryAsync();

        /// <summary>
        /// 异步根据条件查询全部数据
        /// </summary>
        /// <returns>List<T></returns>
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> func);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">页总数</param>
        /// <param name="total">总记录数</param>
        /// <returns>List<T></returns>
        Task<List<T>> PageAsync(int page, int size, RefAsync<int> total);

        /// <summary>
        /// 自定义条件分页
        /// </summary>
        /// <param name="func">条件</param>
        /// <param name="page">页码</param>
        /// <param name="size">页总数</param>
        /// <param name="total">总记录数</param>
        /// <returns>List<T></returns>
        Task<List<T>> PageAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total);
    }
}
