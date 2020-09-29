using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketProgramming {
    class Server {

        private NetworkLogger logger = new NetworkLogger(false);
        private static string ADDRESS = "localhost";
        private static int PORT = 1234;

        private List<Socket> clients;

        public Server() {
            clients = new List<Socket>();
        }

        public void Run() {
            IPHostEntry host = Dns.GetHostEntry(ADDRESS);
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

            try {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                logger.Log("Server started...");

                listener.Bind(localEndPoint);
                listener.Listen(10);

                logger.Log("Waiting for a connection...");

                while (true) {
                    Socket current = listener.Accept();
                    ClientHandler handler = new ClientHandler(current);

                    clients.Add(current);

                    handler.Start();
                }

            }
            catch(Exception e) {
                logger.Exception("TCP Listener not working : " + e.Message);
            }
        }

    }
}
