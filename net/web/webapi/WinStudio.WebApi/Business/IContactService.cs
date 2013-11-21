using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinStudio.WebApi.Wcf;

namespace WinStudio.WebApi.Business
{
    public interface IContactService
    {
        string GetName(int id);
        bool Exist(string name);
    }

    public class ContactService : IContactService
    {
        static List<ContactApi> contacts = new List<ContactApi>()
        {
            new ContactApi {ContactId = 1, Name = "Phil Haack"},
            new ContactApi {ContactId = 2, Name = "HongMei Ge"},
            new ContactApi {ContactId = 3, Name = "Glenn Block"},
            new ContactApi {ContactId = 4, Name = "Howard Dierking"},
            new ContactApi {ContactId = 5, Name = "Jeff Handley"},
            new ContactApi {ContactId = 6, Name = "Yavor Georgiev"}
        };
        public string GetName(int id)
        {
            var contact = contacts.SingleOrDefault(c => c.ContactId == id);
            if (contact == null) return null;
            return contact.Name;
        }

        public bool Exist(string name)
        {
            return contacts.Exists(c => c.Name == name);
        }
    }

}