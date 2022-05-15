using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PS.Data.Infrastructures
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly PSContext context;

        DbSet<T> dbSet;
        public Repository(IDataBaseFactory dbFactory)
        {
            context = dbFactory.DataContext;
            dbSet = context.Set<T>();
        }
        public void Add(T obj)
        {
            dbSet.Add(obj);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
        }

        public void Delete(Expression<Func<T, bool>> condition)
        {
            dbSet.RemoveRange(dbSet.Where(condition));
        }

        public T Get(Expression<Func<T, bool>> condition)
        {
            return dbSet.Where(condition).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null)
        {
            return condition != null ? dbSet.Where(condition) : dbSet;
        }

        public void Update(T obj)
        {
            //dbSet.Attach(obj);
            //context.Entry(obj).State = EntityState.Modified;
            dbSet.Update(obj);

        }
    }
}
