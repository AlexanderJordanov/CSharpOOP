using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Product
    {
        private string name;
        private decimal price;
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public virtual string Name { get { return this.name; } set { this.name = value; } }
        public virtual decimal Price { get { return this.price; } set { this.price = value; } }
    }
}
