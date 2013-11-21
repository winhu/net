using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Demo.WebMvc.ContactBusiness
{
    //[UseAutofac]
    public interface IContactorProxy
    {
        string GetContactorName(int id);
        bool ExistContactor(string name);
    }
}
