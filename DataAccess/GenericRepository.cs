using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FstoreDbContext _context = null;
        protected DbSet<T> dbSet = null;

        public GenericRepository()
        {
            _context = new FstoreDbContext();
            dbSet = _context.Set<T>();
        }

        public GenericRepository(FstoreDbContext dbContext)
        {
            _context = dbContext;
            dbSet = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
