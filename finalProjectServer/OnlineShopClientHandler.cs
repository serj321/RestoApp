using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace finalProjectServer
{
    public class OnlineShopClientHandler
    {
        private readonly TcpClient m_tcpClient;
        private readonly CancellationToken m_cancellationToken;
        private StreamReader reader;
        private StreamWriter writer;

        private List<Shopper> shoppers = new List<Shopper>()
        {
            new Shopper() { AccountNo = "1111", Username = "Serj" },
            new Shopper() { AccountNo = "2222", Username = "John" },
            new Shopper() { AccountNo = "3333", Username = "Doe" }
        };
        public OnlineShopClientHandler(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            m_tcpClient = tcpClient;
            m_cancellationToken = cancellationToken;
        }

        public void Start()
        {
            using (m_tcpClient)
            {
                try
                {
                    NetworkStream stream = m_tcpClient.GetStream();
                    reader = new StreamReader(stream);
                    writer = new StreamWriter(stream);
                    m_cancellationToken.Register(() =>
                    {
                        Thread.Sleep(100);
                        stream.Close();
                    });
                    writer.AutoFlush = true;
                    while (true)
                    {
                        string line = reader.ReadLine();

                        if (line == null || line == "DISCONNECT" || m_cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                        else if (line.Contains("CONNECT"))
                        {
                            Login(line.Split(':')[1]);
                        }
                        else if (line.Contains("GET_PRODUCTS"))
                        {
                            GetProducts();
                        }
                        else if (line.Contains("GET_ORDERS"))
                        {
                            GetOrders();
                        }
                        else if (line.Contains("PURCHASE"))
                        {
                            string productAndShopper = line.Split(':')[1];
                            Purchase(productAndShopper.Split('_')[0], productAndShopper.Split('_')[1]);
                        }
                    }


                }
                catch (IOException e) // Exception takes us out of the loop, so client handler thread will end
                {
                    Console.WriteLine("***Network Error***");
                    Console.WriteLine(e);
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine("***Network Error***"); // May occur if read  or write is attempted after stream is closed
                    Console.WriteLine(e);
                }
            }
        }

        private void Login(string accNo)
        {
            Shopper currentShopper = shoppers.SingleOrDefault(shopper => shopper.AccountNo == accNo);

            if (currentShopper == null)
            {
                writer.WriteLine("CONNECT_ERROR");
                Console.WriteLine("CONNECT_ERROR");
            }
            else
            {
                string username = currentShopper.Username;
                Console.WriteLine($"CONNECTED:{username}");
                writer.WriteLine($"CONNECTED:{username}");
            }
        }

        private void GetProducts()
        {
            string line = ProductHelper.getProductsAsString();
            Console.WriteLine(line);
            writer.WriteLine(line);
        }

        private void GetOrders()
        {
            string line = OrderHelper.GetOrdersAsString();
            Console.WriteLine(line);
            writer.WriteLine(line);
        }

        private void Purchase(string productName, string purchaser)
        {
            try
            {
                int quantity = ProductHelper.Purchase(productName);
                OrderHelper.AddOrder(productName, quantity, purchaser);
                Console.WriteLine("DONE");
                writer.WriteLine("DONE");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("NOT_AVAILABLE");
                writer.WriteLine("NOT_AVAILABLE");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("NOT_VALID");
                writer.WriteLine("NOT_VALID");
            }
        }
    }
}
