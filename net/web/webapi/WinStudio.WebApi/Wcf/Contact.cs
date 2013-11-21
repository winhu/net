using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WinStudio.WebApi.Wcf
{
    [ServiceContract]
    public class ContactApi
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
    }
}