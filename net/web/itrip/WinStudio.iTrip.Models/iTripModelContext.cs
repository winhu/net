using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;

namespace WinStudio.iTrip.Models
{
    public class iTripModelContext : MongoDBContext
    {
        public iTripModelContext(string connectionStringName = "MongoDB") : base(connectionStringName) { }
        public override void OnRegisterModel(ITypeRegistration registration)
        {
            registration.RegisterType<Profile>().RegisterType<Passport>().RegisterType<Customer>();
            registration.RegisterType<ShortMessage>();
            registration.RegisterType<Location>();
        }
    }
}
