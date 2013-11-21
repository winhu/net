using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WinStudio.Framework.EFModels
{
    public interface IBaseModel
    {
        int ID { get; set; }
        DateTime CreatedTime { get; set; }
        Type EntityType { get; }
        [NotMapped]
        string ValidMsg { get; set; }
        bool IsValid();
        bool IsNewModel();
        bool Deleted { get; set; }
        //int EnterpriseId { get; set; }
        //int UserId { get; set; }
    }
    public abstract class BaseModel : IBaseModel
    {
        [Key]
        [Display(Name = "主键", Order = 1)]
        public virtual int ID { get; set; }
        [Required]
        public DateTime CreatedTime { get { return createdTime; } set { createdTime = value; } }
        private DateTime createdTime = DateTime.Now;

        [NotMapped]
        [JsonIgnore]
        public string ValidMsg { get; set; }

        [JsonIgnore]
        public Type EntityType
        {
            get
            {
                if (this.GetType().Namespace == "System.Data.Entity.DynamicProxies")
                    return this.GetType().BaseType;
                else return this.GetType();
            }
        }

        public virtual bool IsValid() { return string.IsNullOrEmpty(ValidMsg); }



        //public virtual int EnterpriseId { get; set; }

        //public virtual int UserId { get; set; }

        public virtual bool Deleted { get; set; }


        public bool IsNewModel()
        {
            return ID <= 0;
        }
    }
}
