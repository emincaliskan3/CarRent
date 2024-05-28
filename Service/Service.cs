using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Service
{
    public class Service<T> : IService<T> where T : class // Buraya gönderilecek olan T bir class olmalıdır

      , IEntity 
        , new() 
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet; 

        public Service(DatabaseContext context)
        {
            _context = context; 
            _dbSet = _context.Set<T>(); 
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
           
            await _dbSet.AddAsync(entity); 
        }

        public void Delete(T entity)
        {

            _dbSet.Remove(entity);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {

            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetIncluDeAsync(Expression<Func<T, bool>> expression, string table)
        {
            return await _dbSet.Where(expression).Include(table).FirstOrDefaultAsync();

        }

        public Task<T> GetInclueAsync(Expression<Func<T, bool>> expression, string table)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
