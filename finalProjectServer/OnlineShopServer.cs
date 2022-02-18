using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace finalProjectServer
{
    public class OnlineShopServer
    {
        public IPAddress ServerIp { get; set; } = IPAddress.Any;
        public int ServerPort { get; set; } = 55555;
        public void Start(object threadInfo)
        {
            CancellationToken cancellationToken = (CancellationToken)threadInfo;
            try
            {
                TcpListener listener = new TcpListener(ServerIp, ServerPort);
                listener.Start();
                cancellationToken.Register(listener.Stop);

                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    OnlineShopClientHandler handler = new OnlineShopClientHandler(tcpClient, cancellationToken);
                    Thread thClientHandler = new Thread(handler.Start);
                    thClientHandler.Start();
                }
            }
            catch (SocketException)
            {

            }

        }

    }
}
