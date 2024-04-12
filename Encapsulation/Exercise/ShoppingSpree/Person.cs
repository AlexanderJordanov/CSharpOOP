using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private int money;
        private List<Product> bag;

        public Person(string name, int money)
        {
            Name = name;
            Money = money;
            bag = new List<Product>();
        }

        public List<Product> Bag { get { return bag; } }
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }
        public int Money
        {
            get { return money; }
            set
            {
                if (value < 0) // check condition
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }
        public void AddProduct(Product product)
        {
            if (product.Cost > this.Money)
            {
                throw new ArgumentException($"{this.Name} can't afford {product.Name}");
            }

            this.Money -= product.Cost;
            bag.Add(product);
        }
    }
}
