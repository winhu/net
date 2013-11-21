using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinStudio.WebApi.Business;
using WinStudio.WebApi.Wcf;

namespace WinStudio.WebApi.Controllers
{
    public class ContactController : ApiController
    {
        private IContactService contact;
        public ContactController(IContactService serv)
        {
            contact = serv;
        }
        static List<ContactApi> contacts = new List<ContactApi>()
        {
            new ContactApi {ContactId = 1, Name = "Phil Haack"},
            new ContactApi {ContactId = 2, Name = "HongMei Ge"},
            new ContactApi {ContactId = 3, Name = "Glenn Block"},
            new ContactApi {ContactId = 4, Name = "Howard Dierking"},
            new ContactApi {ContactId = 5, Name = "Jeff Handley"},
            new ContactApi {ContactId = 6, Name = "Yavor Georgiev"}
        };
        public IEnumerable<ContactApi> Get()
        {
            return contacts.AsQueryable().Where(c => c.ContactId > 2);
        }

        public IEnumerable<ContactApi> GetList()
        {
            return contacts.AsQueryable().Where(c => c.ContactId > 3);
        }
    }
}
