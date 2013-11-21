using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Common.Framework.Repository
{
    /// <summary>
    /// 数据仓库基类,定义基本方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        bool Delete(T entity);
        bool Exists(long Id);
        bool Exists(Expression<Func<T, bool>> where);
        bool Delete(Expression<Func<T, bool>> where);
        bool DeleteByID(long id);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> where);
        int Count(Expression<Func<T, bool>> where);
        void Save(T entity);

        int ExecuteSqlCommand(string sql, params object[] param);

    }
}
