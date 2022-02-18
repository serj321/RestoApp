using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjectServer
{
    class ProductHelper
    {
        public static List<Product> products;

        public static void SetupProducts()
        {
            Random rand = new Random();
            products = new List<Product>()
            {
                new Product() {Name = "Shawarma", Quantity = rand.Next(1,4)},
                new Product() {Name = "Baklava", Quantity = rand.Next(1,4)},
                new Product() {Name = "Kebob", Quantity = rand.Next(1,4)},
                new Product() {Name = "Hummus", Quantity = rand.Next(1,4)},
                new Product() {Name = "Bebsi", Quantity = rand.Next(1,4)}
            };
        }

        public static int Purchase(string productName)
        {
            if(products.Exists(prod => prod.Name == productName))
            {
                Product product = (products.SingleOrDefault(prod => prod.Name == productName));
                if (product.Quantity > 0)
                {
                    product.Quantity -= 1;
                    return product.Quantity;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public static string getProductsAsString()
        {
            string result = "PRODUCTS:";

            Product lastProduct = products.Last();
            foreach (Product product in products)
            {
                string quantityString = product.Quantity.ToString();
                if (lastProduct.Equals(product))
                {
                    result += $"{product.Name},{quantityString}";
                }
                else
                {
                    result += $"{product.Name},{quantityString}|";
                }
            }
            return result;
        }
    }
}
