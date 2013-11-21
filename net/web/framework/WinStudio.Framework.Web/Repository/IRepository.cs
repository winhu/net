using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Framework.Web.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Save(T entity);
        bool Delete(int id);
        bool Delete(T item);
        bool Delete(Expression<Func<T, bool>> where);
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
