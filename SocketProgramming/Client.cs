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

        private int id;

        public Client() {
            Random random = new Random();
            id = random.Next(1, 10);
        }

        public void Send(string message) {
            byte[] bytes = new byte[1024];

            try {
                IPHostEntry host = Dns.GetHostEntry(ADDRESS);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remoteEP);
                logger.Log("Socket connected to : " + sender.RemoteEndPoint.ToString());

                byte[] msg = Encoding.ASCII.GetBytes(id.ToString() + " " + message);

                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);    
                sender.Close();
            }
            catch (SocketException e) {
                logger.Exception("Connection to server failed : " + e.Message);
            }
        }

        public void Run() {
            //logger.Log("Client started...");

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

                byte[] msg = Encoding.ASCII.GetBytes(id.ToString() + " This is a test");

                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);

                // Receive the response from t+k,lohe remote device.    
                //int bytesRec = sender.Receive(bytes);
                //logger.Log("Echoed test :" +Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.    
                sender.Close();
            }
            catch (SocketException e) {
                logger.Exception("Connection to server failed : " + e.Message);
            }
        }

    }
}
