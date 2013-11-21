using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WinStudio.Track.Framework.Repository
{
    public interface IMongodbFactory
    {
        MongoCollection<TDocument> GetCollection<TDocument>();
    }

    public class MongodbFactory : IMongodbFactory
    {
        public MongodbFactory(string connectionName)
        {
            this.name = connectionName;
            Initialize(this.name);
        }
        private string name;
        public string Name { get { return name; } }

        private static IDictionary<string, MongoDatabase> factory = new Dictionary<string, MongoDatabase>();
        private static IDictionary<string, MongodbSetting> settings = new Dictionary<string, MongodbSetting>();
        public MongoDatabase GetDatabase(string name)
        {
            MongodbSetting setting = settings[name];
            if (setting == null)
                throw new ArgumentNullException(name, "Don't exists mongodb server configuration.");
            MongoServerSettings serversettings = new MongoServerSettings();
            serversettings.Server = new MongoServerAddress(setting.Server, setting.Port);
            MongoServer server = new MongoServer(serversettings);
            var database = server.GetDatabase(setting.Source);
            return database;
        }

        private const string ProviderName = "Mongodb.CSharpDriver1.8.2";

        public void Initialize(string connectionname)
        {
            if (settings.Any(s => s.Key == connectionname && s.Value.ProviderName == ProviderName))
                return;
            MongodbSetting setting = new MongodbSetting(connectionname);
            if (setting.IsValid())
                settings.Add(connectionname, setting);
        }

        MongoCollection<TDocument> IMongodbFactory.GetCollection<TDocument>()
        {
            return GetDatabase(Name).GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }

    public class MongodbSetting
    {
        public MongodbSetting() { }
        public MongodbSetting(string connectionName)
        {
            if (string.IsNullOrEmpty(connectionName)) return;
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[connectionName];
            if (setting == null) return;
            this.Name = connectionName;
            this.ProviderName = setting.ProviderName;
            connectionstring = setting.ConnectionString;

            string[] arr = connectionstring.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in arr)
            {
                if (a.StartsWith("Server="))
                {
                    Server = a.Split('=')[1];
                }
                if (a.StartsWith("Source="))
                {
                    Source = a.Split('=')[1];
                }
                if (a.StartsWith("Port="))
                {
                    Port = Int32.Parse(a.Split('=')[1]);
                }
                if (a.StartsWith("UserId="))
                {
                    UserId = a.Split('=')[1];
                }
                if (a.StartsWith("Password="))
                {
                    Password = a.Split('=')[1];
                }
                if (a.StartsWith("Version="))
                {
                    Version = a.Split('=')[1];
                }
                if (a.StartsWith("Driver="))
                {
                    Driver = a.Split('=')[1];
                }
            }

        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ProviderName)
                 && string.IsNullOrEmpty(Driver) && string.IsNullOrEmpty(Version)
                  && string.IsNullOrEmpty(Server) && string.IsNullOrEmpty(Source)
                  && Port <= 0 && string.IsNullOrEmpty(UserId)
                  && string.IsNullOrEmpty(Password);
        }

        public string Name { get; set; }
        public string ProviderName { get; set; }
        public string Driver { get; set; }
        public string Version { get; set; }

        public string Server { get; set; }
        public string Source { get; set; }
        public int Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        private string connectionstring = "";
        public string ConnectionString { get { return connectionstring; } }
    }
}
