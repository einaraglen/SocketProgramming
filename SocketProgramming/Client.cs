using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketProgramming {
    class Client {

        private NetworkLogger logger = new NetworkLogger(true);
        private static string ADDRESS = "localhost";
        private static int PORT = 1234;

        public void Run() {
            logger.Log("Client started...");

            byte[] bytes = new byte[1024];

            try {
                IPHostEntry host = Dns.GetHostEntry(ADDRESS);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

                Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remoteEP);
                logger.Log("Socket connected to : " +
                    sender.RemoteEndPoint.ToString());

                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.    
                int bytesRec = sender.Receive(bytes);
                logger.Log("Echoed test :" +
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.    
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (SocketException e) {
                logger.Exception("Connection to server failed : " + e.Message);
            }
        }

    }
}
