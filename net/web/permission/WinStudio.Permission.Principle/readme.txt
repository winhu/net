权限生成规则举例：如下的控制器dll结构

namespace TestController : Controller
{
    [NeedPermission(Name = "卡应用", Display = true)]
    public class MainController : IDreamController
    {
        public ActionResult Index()
        { return View(); }

        public ActionResult SubIndex()
        { return View(); }

        [NeedPermission(Name = "可挂失", Display = true, Parent = "Manager")]
        public bool CanHangup()
        { return false; }

        [NeedPermission(Name = "通知", Display = true)]
        public void Notification()
        { }
    }

    [NeedPermission(Name = "卡管理", Display = true)]
    public class ManagerController : IDreamController
    {
        [NeedPermission(Name = "首页", Display = true)]
        public ActionResult Index()
        { return View(); }

        [NeedPermission(Name = "删除", Display = true, Parent = "Manager.Update")]
        public bool Delete(int cid)
        { return false; }

        [NeedPermission(Name = "更新", Parent = "Manager.SubIndex")]
        public bool Update(int cid)
        { return false; }

        [NeedPermission(Name = "子卡", Display = true)]
        public ActionResult SubIndex()
        { return View(); }

        [NeedPermission(Name = "是否可用", Display = true, Parent = "Main")]
        public bool IsCardCanbeUsed()
        { return false; }
		
        [NeedPermission(Name = "管理通知", Display = false, Parent = "Main.Notification")]
        public void Notification()
        { }

    }
}

如注册的域为Card，生成树结构如下：
--------------------------------------------------
卡应用,Card,Main,,True
---通知,Card,Main,Notification,True
------管理通知,Card,Manager,Notification,False
---是否可用,Card,Manager,IsCardCanbeUsed,True
卡管理,Card,Manager,,True
---首页,Card,Manager,Index,True
---子卡,Card,Manager,SubIndex,True
------更新,Card,Manager,Update,False
---------删除,Card,Manager,Delete,False
---可挂失,Card,Main,CanHangup,True
---------------------------------------------------