using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Common.Framework.Repository;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Common.Framework.Service
{
    public abstract class BaseBusiService<T> : BaseRepository<T> where T : BaseModel
    {
        public BaseBusiService() : base(new PermissionDbContext()) { }
        public BaseBusiService(PermissionDbContext context) : base(context) { }

        #region 返回结果
        /// <summary>
        /// 返回结果（默认为正确结果）
        /// </summary>
        /// <param name="ret">结果标识</param>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public Result Result(bool ret = true, string msg = "")
        {
            return new Result(ret, msg);
        }
        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns></returns>
        public Result Result(Exception e)
        {
            return new Result(e);
        }
        /// <summary>
        /// 返回正确结果（注：id>0时为正确结果，否则为错误结果）
        /// </summary>
        /// <param name="id">id>0时为正确结果，否则为错误结果</param>
        /// <returns></returns>
        public Result Result(int id)
        {
            return new Result(id);
        }
        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="err">错误信息</param>
        /// <returns></returns>
        public Result Result(string err)
        {
            return new Result(err);
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="ret">结果标识</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public Result Result(bool ret, object obj)
        {
            return new Result(ret, string.Empty, 0, obj);
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="ret">结果标识</param>
        /// <param name="msg">信息</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public Result Result(bool ret, string msg, object obj)
        {
            return new Result(ret, msg, 0, obj);
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="ret">结果标识</param>
        /// <param name="msg">信息</param>
        /// <param name="id">id</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public Result Result(bool ret, string msg, int id, object obj)
        {
            return new Result(ret, msg, id, obj);
        }
        #endregion
    }
}
