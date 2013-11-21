using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using WinStudio.Track.Models.Core;

namespace WinStudio.Track.Framework.Repository
{
    public class Repository : IRepository<ITrackModel>
    {
        public Repository()
            : this(typeof(ITrackModel).Name)
        { }
        public Repository(string connectionName)
        {
            factory = new MongodbFactory(connectionName);
        }

        private IMongodbFactory factory;
        public IMongodbFactory Factory
        {
            get { return factory ?? new MongodbFactory(typeof(ITrackModel).Name); }
        }

        public void Add(ITrackModel entity)
        {
            Factory.GetCollection<ITrackModel>().Save<ITrackModel>(entity);
        }

        public void Update(ITrackModel entity)
        {
        }

        public void Save(ITrackModel entity)
        {
            Factory.GetCollection<ITrackModel>().Save<ITrackModel>(entity);
        }

        public void Delete(IMongoQuery query)
        {
        }

        public void Delete(ITrackModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<ITrackModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public ITrackModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ITrackModel Get(System.Linq.Expressions.Expression<Func<ITrackModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(System.Linq.Expressions.Expression<Func<ITrackModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITrackModel> GetAllEnt()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITrackModel> GetAllEnt(bool deleted)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITrackModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITrackModel> GetAll(bool deleted)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITrackModel> GetAll(System.Linq.Expressions.Expression<Func<ITrackModel, bool>> where)
        {
            throw new NotImplementedException();
        }

    }
}
