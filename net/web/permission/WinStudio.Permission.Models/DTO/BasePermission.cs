using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Permission.Models
{
    public abstract class TreeNode : BaseModel, ITreeNode
    {
        [NotMapped]
        [ScaffoldColumn(false)]
        public int? PID { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public int RPID { get; set; }

        [Display(Name = "是否显示", Order = 4)]
        public bool Display { get; set; }

        public virtual object ToTreeNode(int pid)
        {
            return new { id = ID, name = Name, type = (int)NodeType };
        }

        [Display(Name = "类型", Order = 3)]
        public abstract PermissionNodeType NodeType { get; }

        public abstract object GetChildrenNodes(PermissionNodeType type, PermissionNodeType ptype, int rpid);

        public abstract object GetVirtualNodes();

        [Display(Name = "名称", Order = 2)]
        public string Name { get; set; }
    }
    public abstract class BasePermission : TreeNode
    {
        public BasePermission()
        {
            IsUsing = true;
            Display = true;
        }

        [Display(Name = "是否启用", Order = 5)]
        public bool IsUsing { get; set; }

        [StringLength(128)]
        [Display(Name = "说明")]
        public string Memo { get; set; }

    }
}
