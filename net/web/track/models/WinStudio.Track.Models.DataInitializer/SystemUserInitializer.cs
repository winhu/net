using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.TestDataFramework.EF;
using WinStudio.Track.Framework.Models;
using WinStudio.Track.Models.Authentication;

namespace WinStudio.Track.Models.DataInitializer
{

    public class SystemUserInitializer : ITestDataInitializer<AuthenticationDbContext>
    {
        public void Initialize(AuthenticationDbContext context)
        {
            var hyfcontacts = new List<Contact>(){
                    new Contact(){ Content="baicai@gmail.com", Type= ContactType.Email },
                    new Contact(){ Content="11271058", Type= ContactType.QQ },
                    new Contact(){ Content="13612345678", Type= ContactType.Phone },
                    new Contact(){ Content="http://cnblog.com", Type= ContactType.Bolg }
                };
            var admincontacts = new List<Contact>(){
                    new Contact(){ Content="admin@gmail.com", Type= ContactType.Email },
                    new Contact(){ Content="12345678", Type= ContactType.QQ },
                    new Contact(){ Content="13912345678", Type= ContactType.Phone },
                    new Contact(){ Content="http://weibo.com", Type= ContactType.Bolg }
                };


            new List<SignerKey>{ 
                new SignerKey{ Account="admin", Password="admin".ToMD5()},
                new SignerKey{ Account="hyfhyf", Password="hyfhyf".ToMD5()}
            }.ForEach(k => context.SingerKeies.Add(k));

            new List<Profile>{ 
                new Profile{ Account="admin", Name="系统管理员", NickName="系统管理员儿" , Contacts=admincontacts},
                new Profile{ Account="hyfhyf", Name="胡云丰", NickName="白菜", Gender= Gender.Male,Contacts=hyfcontacts}
            }.ForEach(i => context.Profiles.Add(i));

        }

        public int SaveToDatabase(AuthenticationDbContext context)
        {
            return context.SaveChanges();
        }

        public string Code
        {
            get { return "SystemUser"; }
        }

        public int Save(AuthenticationDbContext context)
        {
            return context.SaveChanges();
        }
        public int Order
        {
            get { return 1; }
        }
    }
}
