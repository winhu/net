using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Framework.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        void Add(T entity);
        void Update(T entity);
        void Save(T entity);
        void Delete(int id);
        void Delete(T item);
        void Delete(Expression<Func<T, bool>> where);
        int Commit();
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        bool Exist(int id);
        bool Exist(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllEnt();
        IQueryable<T> GetAllEnt(bool deleted);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(bool deleted);
        IQueryable<T> GetAll(Expression<Func<T, bool>> where);
    }
}
