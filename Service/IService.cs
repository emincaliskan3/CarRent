using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T>
    {
        #region Senkron Metotlar
      
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression); // x=x. ... şeklinde sorgu yazabilmemizi sağlayan metot
        T Get(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        #endregion

        #region Asenkron Metotlar
       

        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetInclueAsync(Expression<Func<T, bool>> expression, string table);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        #endregion

    }
}
