using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace finalProjectClient
{
    public class ShopperServerHandler
    {
        public string HostName { get; set; }
        public int HostPort { get; set; } = 55555;
        public ShopperData currentShopper;
        public ShopperServerHandler(string hostName, string accNo)
        {
            HostName = hostName;
            currentShopper = new ShopperData();
            currentShopper.AccountNumber = accNo;
        }

        private Tuple<StreamReader, StreamWriter> SetupStream(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            StreamReader reader= new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            return Tuple.Create(reader, writer);
        }

        public bool CheckConnect()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect(HostName, HostPort);
                (StreamReader reader, StreamWriter writer) = SetupStream(tcpClient);
                writer.WriteLine($"CONNECT:{currentShopper.AccountNumber}");
                writer.Flush();
                string line = reader.ReadLine();
                if (line.Contains("CONNECTED"))
                {
                    currentShopper.Username = line.Split(':')[1];
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Product> GetProducts()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(HostName, HostPort);
                    (StreamReader reader, StreamWriter writer) = SetupStream(tcpClient);
                    writer.WriteLine("GET_PRODUCTS");
                    writer.Flush();
                    string line = reader.ReadLine();
                    string results = line.Split(':')[1];
                    string[] productsAsString = results.Split('|');
                    List<Product> products = new List<Product>();
                    foreach(string product in productsAsString)
                    {
                        products.Add(new Product { Name = product.Split(',')[0], Quantity = int.Parse(product.Split(',')[1]) });
                    }
                    return products;
                }
                catch (IOException e) {
                    Console.WriteLine(e);
                    return null;
                }
                catch (SocketException e) {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        public List<Order> GetOrders()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect(HostName, HostPort);
                (StreamReader reader, StreamWriter writer) = SetupStream(tcpClient);
                writer.WriteLine("GET_ORDERS");
                writer.Flush();
                string line = reader.ReadLine();
                string results = line.Split(':')[1];
                string[] ordersAsString = results.Split('|');
                List<Order> orders = new List<Order>();
                if(ordersAsString.Count() > 1)
                {
                    foreach (string order in ordersAsString)
                    {
                        orders.Add(new Order { ProductName = order.Split(',')[0], Quantity = int.Parse(order.Split(',')[1]), User = order.Split(',')[2] });
                    }
                    return orders;
                } else if(ordersAsString.Count() == 1 && ordersAsString[0].Length > 1)
                {
                    string order = ordersAsString[0];
                    orders.Add(new Order { ProductName = order.Split(',')[0], Quantity = int.Parse(order.Split(',')[1]), User = order.Split(',')[2] });
                    return orders;
                }
                return null;
            }
        }

        public string Purchase(string product)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(HostName, HostPort);
                    (StreamReader reader, StreamWriter writer) = SetupStream(tcpClient);
                    writer.WriteLine($"PURCHASE:{product}_{currentShopper.Username}");
                    writer.Flush();
                    string line = reader.ReadLine();
                    return line;
                }
                catch(IOException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
                catch(SocketException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
    }
}
