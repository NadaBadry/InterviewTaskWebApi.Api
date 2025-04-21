using InterviewTaskWebApi.Domain.DataDbContext;
using InterviewTaskWebApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace InterviewTaskWebApi.Infrustructure.Repository.ImplementRepository
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }
        public  virtual IEnumerable<T> GetAll()
        {
            return  _dbSet.AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return  _dbSet.Find(id);
        }

        public  void Insert(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }

        public  void Delete(Guid id)
        {
          var entity=  _dbSet.Find(id);
            if(entity!=null)
            {
                 _dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
