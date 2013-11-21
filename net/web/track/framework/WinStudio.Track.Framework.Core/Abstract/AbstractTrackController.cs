using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.ComResult;
using WinStudio.Framework.Authentication;

namespace System.Web.Mvc
{
    public abstract class AbstractTrackController : AbstractMvcController
    {

        protected bool IsSigned
        {
            get
            {
                return null != HttpContext.GetWinSession();
            }
        }
        private string myAccount, myNickName;
        protected string MyAccount
        {
            get
            {
                if (myAccount.HasValue()) return myAccount;
                if (IsSigned)
                    myAccount = HttpContext.GetWinSession().Account;
                return myAccount;
            }
        }
        protected string MyNickName
        {
            get
            {
                if (myNickName.HasValue()) return myAccount;
                if (IsSigned)
                    myNickName = HttpContext.GetWinSession().NickName;
                return myNickName;
            }
        }

     }

    public abstract class AbstractAsyncTrackController : AbstractMvcController
    {
 }
}
