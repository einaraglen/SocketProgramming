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

        public void Run() {
            IPHostEntry host = Dns.GetHostEntry(ADDRESS);
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);
            try {
                Socket listener = new Socket(ipAddress.AddressFamily, 
                    SocketType.Stream, ProtocolType.Tcp);
                logger.Log("Server started...");

                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                logger.Log("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;

                while (true) {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1) {
                        break;
                    }
                }

                logger.Log("Text received : " +  data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch(Exception e) {
                logger.Exception("TCP Listener not working : " + e.Message);
            }
        }

    }
}
