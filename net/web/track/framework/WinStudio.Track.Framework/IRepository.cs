using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace Synjones.Dreams.Core.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        void Add(T entity);
        void Update(T entity);
        void Save(Guid? id, T entity);
        void Delete(Guid id);
        void Delete(T item);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(Guid id);
        T Get(Expression<Func<T, bool>> where);
        bool Exist(Guid id);
        bool Exist(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllEnt();
        IQueryable<T> GetAllEnt(bool deleted);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(bool deleted);
        IQueryable<T> GetAll(Expression<Func<T, bool>> where);
    }
}
