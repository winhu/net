using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Common.Framework.Repository
{
    /// <summary>
    /// 实现数据仓储基本操作
    /// </summary>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected DbContext _context;
        private readonly IDbSet<T> dbset;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            dbset = context.Set<T>();
        }
        protected virtual DbContext Get()
        {
            return _context;
        }
        protected virtual TContext Get<TContext>() where TContext : DbContext
        {
            return _context as TContext;
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
            _context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }

            _context.SaveChanges();
        }
        public virtual bool Delete(T entity)
        {
            if (entity == null) return false;
            dbset.Remove(entity);
            return 1 == _context.SaveChanges();
        }
        public virtual bool DeleteByID(long id)
        {
            return Delete(t => t.ID == id);
        }
        public virtual bool Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
            return objects.Count() == _context.SaveChanges();
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }


        public virtual bool Exists(long Id)
        {
            return Exists(t => t.ID == Id);
        }

        public virtual bool Exists(Expression<Func<T, bool>> where)
        {
            return dbset.Any(where);
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).Count();
        }

        public void Save(T entity)
        {
            if (entity.ID == 0)
                Add(entity);
            else if (entity.ID < 0) return;
            else Update(entity);
        }

        public int ExecuteSqlCommand(string sql, params object[] param)
        {
            return _context.Database.ExecuteSqlCommand(sql, param);
        }
    }
}
