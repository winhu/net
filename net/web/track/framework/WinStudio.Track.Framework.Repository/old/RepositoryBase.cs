using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Framework.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseModel
    {
        private readonly IDbSet<T> _dbset;
        private DbContext _dataContext;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }
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
            Commit();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            Commit();
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public virtual void Save(T entity)
        {
            var ientity = entity as IBaseModel;
            if (ientity != null)
            {
                //ientity.EnterpriseId = _userInfo.EnterpriseId;
                //ientity.UserId = _userInfo.UserId;

                if (ientity.ID > 0)
                {
                    Update(ientity as T);
                }
                else
                {
                    Add(ientity as T);
                }
                Commit();
            }
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var item = GetById(id);
            Delete(item);
            Commit();
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="item"></param>
        public virtual void Delete(T item)
        {
            //var entity = item as IBaseModel;
            //if (entity != null)// && entity.EnterpriseId.Equals(_userInfo.EnterpriseId))
            //    entity.Deleted = true;
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
            Commit();
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            _dbset.Remove(item);
            Commit();
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
            return DataContext.SaveChanges();
        }
    }
}
