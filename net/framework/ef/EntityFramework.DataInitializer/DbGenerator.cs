using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity
{
    internal interface IDbGenerator<TDbContext> where TDbContext : DbContext
    {
        void RegisterDBContext(TDbContext context);
        void Run(IDataInitializer<TDbContext> initializer);
    }

    internal class DbGenerator<TDbContext> : IDbGenerator<TDbContext> where TDbContext : DbContext
    {
        DataInitializerFactory<TDbContext> _factory;
        TDbContext _context;
        public void RegisterDBContext(TDbContext context)
        {
            _factory = new DataInitializerFactory<TDbContext>();
            _context = context;
        }

        public void Run(IDataInitializer<TDbContext> initializer)
        {
            _factory.AddDataInitializer(initializer);
            _factory.InitializeDatabase(_context);
            _context.SaveChanges();
        }
    }

    public static class GeneratorExtensions
    {
        /// <summary>
        /// 使用Entity Framework框架构建数据库并初始化数据
        /// </summary>
        /// <typeparam name="TDbContext">上下文类型</typeparam>
        /// <param name="initializers">数据构造器</param>
        /// <param name="context">上下文</param>
        public static void Setup<TDbContext>(this List<IDataInitializer<TDbContext>> initializers, TDbContext context) where TDbContext : DbContext
        {
            DataInitializerFactory<TDbContext> _factory = new DataInitializerFactory<TDbContext>();
            initializers.ForEach(initializer => _factory.AddDataInitializer(initializer));
            _factory.InitializeDatabase(context);
            context.SaveChanges();
        }
        /// <summary>
        /// 使用Entity Framework框架构建数据库并初始化数据
        /// </summary>
        /// <typeparam name="TDbContext">上下文类型</typeparam>
        /// <param name="initializer">数据构造器</param>
        /// <param name="context">上下文</param>
        public static void Setup<TDbContext>(this IDataInitializer<TDbContext> initializer, TDbContext context) where TDbContext : DbContext
        {
            DataInitializerFactory<TDbContext> _factory = new DataInitializerFactory<TDbContext>();
            _factory.AddDataInitializer(initializer);
            _factory.InitializeDatabase(context);
            context.SaveChanges();
        }
    }
}
