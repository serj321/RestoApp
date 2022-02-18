using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace finalProjectServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Thread thServer  = new Thread(new OnlineShopServer().Start);
            ProductHelper.SetupProducts();
            Console.WriteLine("Press Q to exit");
            thServer.Start(cancellationTokenSource.Token);
            while (thServer.IsAlive && (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Q))
                ;
            cancellationTokenSource.Cancel();
            Console.WriteLine("Server is shutting down.");
            Console.ReadKey();
        }
    }
}
