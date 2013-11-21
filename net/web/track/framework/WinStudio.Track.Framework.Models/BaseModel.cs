using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WinStudio.Track.Framework.Models
{

    public interface IBaseModel
    {
        int ID { get; set; }
        DateTime CreatedTime { get; set; }
        string EntityType { get; }
        [NotMapped]
        string ValidMsg { get; set; }
        bool IsValid();
        bool Deleted { get; set; }
        //int EnterpriseId { get; set; }
        //int UserId { get; set; }
    }
    public abstract class BaseModel : IBaseModel
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        private DateTime createdTime = DateTime.Now;
        [Required]
        public virtual DateTime CreatedTime { get { return createdTime; } set { createdTime = value; } }

        [NotMapped]
        [JsonIgnore]
        public string ValidMsg { get; set; }

        [JsonIgnore]
        public string EntityType
        {
            get
            {
                if (this.GetType().Namespace == "System.Data.Entity.DynamicProxies")
                    return this.GetType().BaseType.FullName;
                else return this.GetType().FullName;
            }
        }
        public virtual bool IsValid() { return string.IsNullOrEmpty(ValidMsg); }



        //public virtual int EnterpriseId { get; set; }

        //public virtual int UserId { get; set; }

        public virtual bool Deleted { get; set; }
    }
}
