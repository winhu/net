using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;

namespace WinStudio.Passport.Models
{
    public class PassportMongoDBContext : MongoDBContext
    {
        public PassportMongoDBContext() : base("PassportDB") { }

        public override void OnRegisterModel(ITypeRegistration registration)
        {
            registration.RegisterType<SignerKey>().RegisterType<Profile>().RegisterType<OpenID>();
            registration.RegisterType<Contact>().RegisterType<Module>();
            registration.RegisterType<UserSignHistory>().RegisterType<ModuleSignHistory>();
        }
    }
}
