using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WinStudio.Web.Controllers
{
    public class Product
    {
        public Product() { factory = new List<factory>(); }
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public virtual List<factory> factory { get; set; }
    }
    public class factory
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class ProductApiController : ApiController
    {
        private static List<Product> products = new List<Product>() { 
            new Product(){ id=0,name="prod1",date= DateTime.Now,factory=new List<factory>{
                new factory{id=0,name="beijing",address="beijinghaidian"},
                new factory{id=2,name="beijing2",address="beijingchaoyang"}
            }},
            new Product(){ id=2,name="prod2",date= DateTime.Now},
            new Product(){ id=3,name="prod3",date= DateTime.Now},
            new Product(){ id=4,name="prod4",date= DateTime.Now},
            new Product(){ id=5,name="prod5",date= DateTime.Now},
            new Product(){ id=6,name="prod6",date= DateTime.Now}
        };

        public IEnumerable<Product> Get()
        {
            return products.AsEnumerable();
        }
        public Product Get(int id)
        {
            products.RemoveAll(p => p.id == id);
            return products.SingleOrDefault(p => p.id == id);
        }
        public string Post([FromBody]Product prod)
        {
            if (products.Exists(p => p.id == prod.id)) return "exist";
            prod.id = products.Count + 1;
            products.Add(prod);
            return "ok";
        }
        public void Put(int id, [FromBody]Product prod)
        {
            Product p = Get(id);
            p.name = prod.name;
            p.date = prod.date;

        }
        public void Delete(int id)
        {
            products.RemoveAll(p => p.id == id);
        }
    }
}