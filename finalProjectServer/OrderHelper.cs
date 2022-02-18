using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjectServer
{
    public class OrderHelper
    {
        private static List<Order> orders = new List<Order>();

        public static string GetOrdersAsString()
        {

            string result = "ORDERS:";

            if(orders.Count > 0)
            {
                Order lastOrder = orders.Last();
                foreach (Order order in orders)
                {
                    string quantityString = order.Quantity.ToString();
                    if (lastOrder.Equals(order))
                    {
                        result += $"{order.ProductName},{order.Quantity},{order.User}";

                    }
                    else
                    {
                        result += $"{order.ProductName},{order.Quantity},{order.User}|";

                    }
                }
                return result;
            }
            else
            {
                return result;
            }
        }

        public static void AddOrder(string productName, int quantity, string user)
        {
            orders.Add(new Order() { ProductName = productName, Quantity = 1, User = user });
        }
    }
}
