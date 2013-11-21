using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Synjones.Dreams.Core.Models;
using Synjones.Dreams.Core.SessionModels.Instance;
using Synjones.Dreams.Core.Common;
using WinStudio.Track.Framework.Models;

namespace Synjones.Dreams.Core.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseModel
    {
        private readonly IDbSet<T> _dbset;
        private DbContext _dataContext;

        //protected RepositoryBase(IDatabaseFactory databaseFactory)
        //{
        //    DatabaseFactory = databaseFactory;
        //    _dbset = DataContext.Set<T>();
        //}
        protected RepositoryBase(BaseDbContext context)
        {
            _dataContext = context;

            _dbset = DataContext.Set<T>();
        }

        private IDatabaseFactory DatabaseFactory { get; set; }

        private DbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public virtual void Save(int? id, T entity)
        {
            var ientity = entity as IBaseModel;
            if (ientity != null)
            {
                //ientity.EnterpriseId = _userInfo.EnterpriseId;
                //ientity.UserId = _userInfo.UserId;

                if (id.HasValue)
                {
                    Update(ientity as T);
                }
                else
                {
                    Add(ientity as T);
                }
            }
            else
            {
                if (id.HasValue)
                {
                    Update(entity);
                }
                else
                {
                    Add(entity);
                }
            }
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(Guid id)
        {
            var item = GetById(id);
            Delete(item);
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(T item)
        {
            var entity = item as IBaseModel;
            if (entity != null)// && entity.EnterpriseId.Equals(_userInfo.EnterpriseId))
                entity.Deleted = true;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            foreach (var item in GetAll(where))
            {
                Delete(item);
            }
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            _dbset.Remove(item);
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
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
            return _dbset.Where("Deleted=@0", deleted).OrderBy("CreatedDate Desc");
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



        public bool Exist(Guid id)
        {
            return _dbset.Count(t => t.Id == id) > 0;
        }

        public bool Exist(Expression<Func<T, bool>> where)
        {
            return _dbset.Count(where) > 0;
        }


        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.SingleOrDefault(where);
        }
    }
}
