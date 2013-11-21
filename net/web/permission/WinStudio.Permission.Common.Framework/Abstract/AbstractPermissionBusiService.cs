using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;
using WinStudio.Framework.Web.Repository;
using WinStudio.Framework.Web.Services;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Common.Framework.Abstract
{
    public class AbstractPermissionBusiService<T> : AbstractBusiService<T> where T : BaseModel
    {
        public AbstractPermissionBusiService() : this(new PermissionDbContext()) { }
        public AbstractPermissionBusiService(PermissionDbContext context) : base(context) { }
        public AbstractPermissionBusiService(IDatabaseFactory factory) : base(factory) { }

    }
}
