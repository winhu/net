using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Track.Models
{
    public abstract class BaseOne
    {
        private DateTime createTime = DateTime.Now;
        [Required]
        public DateTime CreateTime { get { return createTime; } set { createTime = value; } }

        public string ModelType
        {
            get
            {
                if (this.GetType().Namespace == "System.Data.Entity.DynamicProxies")
                    return this.GetType().BaseType.FullName;
                else return this.GetType().FullName;
            }
        }
    }
    public abstract class BaseModel : BaseOne
    {
        [Key]
        public int ID { get; set; }
        private DateTime createTime = DateTime.Now;
        [Required]
        public DateTime CreateTime { get { return createTime; } set { createTime = value; } }
        [NotMapped]
        public string ValidMsg { get; set; }

        public virtual bool IsValid() { return string.IsNullOrEmpty(ValidMsg); }
    }
}
