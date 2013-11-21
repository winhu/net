using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.ComResult
{
    public class ComRet
    {
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="ret"></param>
        public ComRet(bool ret = true) : this(ret, string.Empty, 0, null) { }
        /// <summary>
        /// failed result
        /// </summary>
        /// <param name="err">error msg</param>
        public ComRet(string err) : this(false, err, 0, null) { }
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="obj">object</param>
        public ComRet(object obj) : this(obj != null, string.Empty, 0, obj) { }
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="num">num</param>
        public ComRet(int num) : this(true, string.Empty, num, null) { }
        /// <summary>
        /// customed result
        /// </summary>
        /// <param name="ret">ret</param>
        /// <param name="msg">msg</param>
        public ComRet(bool ret, string msg) : this(ret, msg, 0, null) { }
        /// <summary>
        /// customed result
        /// </summary>
        /// <param name="ret">ret</param>
        /// <param name="msg">msg</param>
        /// <param name="num">num</param>
        /// <param name="obj">obj</param>
        public ComRet(bool ret, string msg, int num, object obj)
        {
            Ret = ret;
            AddMsg(msg);
            Num = num;
            AddInstance(obj);
        }
        public virtual bool Ret { get; set; }

        private List<string> msgs = new List<string>();

        private IDictionary<Type, object> instances = new Dictionary<Type, object>();

        public ComRet AddInstance<T>(T obj)
        {
            if (obj == null) return this;// throw new NullReferenceException("canot add null instance from ComRet");
            Type type = obj.GetType();
            if (type.Namespace == "System.Data.Entity.DynamicProxies")
                type = type.BaseType;
            instances[type] = obj;
            return this;
        }
        public ComRet AddInstance(object obj)
        {
            if (obj == null) return this;// throw new NullReferenceException("canot add null instance from ComRet");
            Type type = obj.GetType();
            if (type.Namespace == "System.Data.Entity.DynamicProxies")
                type = type.BaseType;
            instances[type] = obj;
            return this;
        }
        public T Instance<T>()
        {
            Type t = typeof(T);
            if (t.Namespace == "System.Data.Entity.DynamicProxies")
                t = t.BaseType;
            object ret;
            if (instances.TryGetValue(t, out ret))
                return (T)ret;
            return default(T);

            //if (!instances.Keys.Contains(t)) return default(T);
            //return (T)instances[t];
        }
        public object Instance(int index = 0)
        {
            if (index < 0 || index >= instances.Values.Count) return null;
            return instances.Values.ToArray()[index];
        }
        public object[] Instances
        {
            get { return instances.Values.ToArray(); }
        }

        public virtual int Num { get; set; }

        /// <summary>
        /// add new msg
        /// </summary>
        /// <param name="msg"></param>
        public virtual ComRet AddMsg(string msg, bool successful = false)
        {
            msgs.Add(msg);
            if (successful) Ret = true;
            return this;
        }
        /// <summary>
        /// add error msg and Ret is false
        /// </summary>
        /// <param name="err">error msg</param>
        public virtual ComRet AddError(string err)
        {
            Ret = false;
            msgs.Add(err);
            return this;
        }
        /// <summary>
        /// get last msg
        /// </summary>
        public virtual string LastMsg
        {
            get
            {
                return msgs.FindLast(m => !string.IsNullOrEmpty(m));
            }
        }
        /// <summary>
        /// get msg by index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns></returns>
        public virtual string GetMsg(int index)
        {
            if (msgs.Count > index)
                return msgs[index];
            return string.Empty;
        }
        /// <summary>
        /// assert successed
        /// </summary>
        public virtual ComRet Successed() { Ret = true; return this; }
        /// <summary>
        /// assert failed
        /// </summary>
        public virtual ComRet Failed() { Ret = false; return this; }
        /// <summary>
        /// all msgs
        /// </summary>
        public virtual string[] AllMsg { get { return msgs.ToArray(); } }
    }

    public class ComRet<T> : ComRet where T : class
    {
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="ret"></param>
        public ComRet(bool ret = true) : this(ret, string.Empty, 0, null) { }
        /// <summary>
        /// failed result
        /// </summary>
        /// <param name="err">error msg</param>
        public ComRet(string err) : this(false, err, 0, null) { }
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="obj">object</param>
        public ComRet(T obj) : this(true, string.Empty, 0, obj) { }
        /// <summary>
        /// success result
        /// </summary>
        /// <param name="num">num</param>
        public ComRet(int num) : this(true, string.Empty, num, null) { }
        /// <summary>
        /// customed result
        /// </summary>
        /// <param name="ret">ret</param>
        /// <param name="msg">msg</param>
        public ComRet(bool ret, string msg) : this(ret, msg, 0, null) { }
        /// <summary>
        /// customed result
        /// </summary>
        /// <param name="ret">ret</param>
        /// <param name="msg">msg</param>
        /// <param name="num">num</param>
        /// <param name="obj">obj</param>
        public ComRet(bool ret, string msg, int num, T obj) : base(ret, msg, num, obj) { }

        public T Instance { get { return Instance<T>(); } set { base.AddInstance<T>(value); } }
    }
}
