using Blog.IRepository;
using Blog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        protected IBaseRepository<T> baseRepository;
        public async Task<bool> AddAsync(T enity)
        {
            return await baseRepository.AddAsync(enity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await baseRepository.DeleteAsync(id);
        }

        public async Task<bool> EditAsync(T enity)
        {
            return await baseRepository.EditAsync(enity);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> func)
        {
            return await baseRepository.FindAsync(func);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await baseRepository.FindByIdAsync(id);
        }

        public async Task<List<T>> PageAsync(int page, int size, RefAsync<int> total)
        {
            return await baseRepository.PageAsync(page, size, total);
        }

        public async Task<List<T>> PageAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await baseRepository.PageAsync(func, page, size, total);
        }

        public async Task<List<T>> QueryAsync()
        {
            return await baseRepository.QueryAsync();
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func)
        {
            return await baseRepository.QueryAsync(func);
        }
    }


}
