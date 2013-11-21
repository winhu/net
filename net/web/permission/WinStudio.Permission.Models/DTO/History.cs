using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(HistoryMetadata))]
    public partial class History : BaseModel
    {
        public OperationType Operation { get; set; }

        public string ModelType { get; set; }

        public int ModelID { get; set; }

        public int OperatorID { get; set; }
        public virtual Administrator Operator { get; set; }

        public string Detail { get; set; }
    }
}
