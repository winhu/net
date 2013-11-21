using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Framework.Web.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseModel
    {
        private readonly IDbSet<T> _dbset;
        private WinDbContext _dataContext;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }
        protected RepositoryBase(WinDbContext context)
        {
            _dataContext = context;
            _dbset = DataContext.Set<T>();

            DatabaseFactory = new DatabaseFactory();
        }

        protected IDatabaseFactory DatabaseFactory { get; set; }

        private WinDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
        protected virtual WinDbContext Get()
        {
            return _dataContext;
        }
        protected virtual TContext Get<TContext>() where TContext : WinDbContext
        {
            return _dataContext as TContext;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Add(T entity)
        {
            _dbset.Add(entity);
            return Commit() > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentException("Cannot update a null entity.");
                var entry = _dataContext.Entry<T>(entity);
                if (entry.State == EntityState.Detached)
                {
                    var set = _dataContext.Set<T>();
                    T attachedEntity = set.Find(entity.ID);
                    if (attachedEntity != null)
                    {
                        var attachedEntry = _dataContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else entry.State = EntityState.Modified;
                }
                return Commit() > 0;
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public virtual bool Save(T entity)
        {
            var ientity = entity as IBaseModel;
            if (ientity != null)
            {
                //ientity.EnterpriseId = _userInfo.EnterpriseId;
                //ientity.UserId = _userInfo.UserId;

                if (ientity.ID > 0)
                {
                    return Update(ientity as T);
                }
                else
                {
                    return Add(ientity as T);
                }
            }
            return false;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="id"></param>
        public virtual bool Delete(int id)
        {
            var item = GetById(id);
            return Delete(item);
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="item"></param>
        public virtual bool Delete(T item)
        {
            _dbset.Remove(item);
            return Commit() > 0;
            //var entity = item as IBaseModel;
            //if (entity != null)// && entity.EnterpriseId.Equals(_userInfo.EnterpriseId))
            //    entity.Deleted = true;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="where"></param>
        public virtual bool Delete(Expression<Func<T, bool>> where)
        {
            foreach (var item in GetAll(where))
            {
                Delete(item);
            }
            return Commit() > 0;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="item"></param>
        public virtual bool Remove(T item)
        {
            _dbset.Remove(item);
            return Commit() > 0;
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// 获取全部企业数据
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllEnt()
        {
            return GetAllEnt(false);
        }

        /// <summary>
        /// 获取全部企业数据
        /// </summary>
        /// <param name="deleted"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllEnt(bool deleted)
        {
            return _dbset.Where(t => t.Deleted == deleted).OrderByDescending(t => t.CreatedTime);//.OrderBy("CreatedTime Desc");

            //return _dbset.Where(t => t.Deleted == deleted).OrderByDescending(t => t.CreatedTime);//.OrderBy("CreatedTime Desc");
        }

        /// <summary>
        /// 获取用户所在企业数据
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return GetAll(false);
        }

        /// <summary>
        /// 获取用户所在企业数据
        /// </summary>
        /// <param name="deleted"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(bool deleted)
        {
            return GetAllEnt(deleted);//.Where("EnterpriseId=@0", _userInfo.EnterpriseId);
        }

        /// <summary>
        /// 获取符合条件的用户所在企业数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return GetAll().Where(where);
        }



        public bool Exist(int id)
        {
            return _dbset.Count(t => t.ID == id) > 0;
        }

        public bool Exist(Expression<Func<T, bool>> where)
        {
            return _dbset.Count(where) > 0;
        }


        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.SingleOrDefault(where);
        }


        public int Commit()
        {
            int couter = DataContext.SaveChanges();
            return couter;
        }
    }
}
