using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Demo.WebMvc.ContactBusiness
{
    public class Contactor
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ContactorRepository
    {
        private static List<Contactor> Contactors = new List<Contactor>() { 
            new Contactor(){ ID=1,Name="hyf"},
            new Contactor(){ ID=2,Name="zhc"},
            new Contactor(){ ID=3,Name="lxd"}
        };
        public string GetName(int id)
        {
            if (Contactors.Exists(c => c.ID == id)) return Contactors.SingleOrDefault(c => c.ID == id).Name;
            return string.Empty;
        }
        public bool Exist(string name)
        {
            return Contactors.Exists(c => c.Name == name);
        }
    }
    [Export(typeof(IContactorProxy))]
    public class ContactorProxy : IContactorProxy
    {
        public string GetContactorName(int id)
        {
            return new ContactorRepository().GetName(id);
        }

        public bool ExistContactor(string name)
        {
            return new ContactorRepository().Exist(name);
        }
    }
}
