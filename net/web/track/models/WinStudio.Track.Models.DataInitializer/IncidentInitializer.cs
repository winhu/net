using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.TestDataFramework.EF;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Models.DataInitializer
{

    public class CustomerInitializer : ITestDataInitializer<TrackDbContext>
    {
        public string Code
        {
            get { return "Customer"; }
        }

        public void Initialize(TrackDbContext context)
        {

            new List<Customer>() { 
                new Customer(){ Account="admin"},
                new Customer(){  Account="hyfhyf"}
            }.ForEach(d => context.Customers.Add(d));
        }

        public int Order
        {
            get { return 0; }
        }

        public int Save(TrackDbContext context)
        {
            return context.SaveChanges();
        }
    }
    public class IncidentInitializer : ITestDataInitializer<TrackDbContext>
    {

        public string Code
        {
            get { return "Incident"; }
        }

        public void Initialize(TrackDbContext context)
        {
            new List<Incident>() { 
                new Incident(){ Title="测试1", TrackerID=1},
                new Incident(){ Title="测试2", TrackerID=2}
            }.ForEach(i => context.Incidents.Add(i));

            new List<Detail>() { 
                new Detail(){  Content="这个是测试1的信息", IncidentID=1},
                new Detail(){  Content="这个是测试2的信息", IncidentID=2}
            }.ForEach(d => context.Details.Add(d));
        }

        public int Save(TrackDbContext context)
        {
            return context.SaveChanges();
        }


        public int Order
        {
            get { return 10; }
        }
    }
}
