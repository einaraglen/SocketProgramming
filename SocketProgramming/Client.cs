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

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try {
                socket.Connect(ADDRESS, PORT);
                logger.Log("Successfully connected");
                
                //Sending data to server
                byte[] data = System.Text.Encoding.ASCII.GetBytes("GET / HTTP/1.0\r\n\r\n");
                socket.Send(data);

                //Get HTTP respind from server
                byte[] buffer = new byte[1024];
                int byteNumber = socket.Receive(buffer);
                logger.Log(byteNumber.ToString() + " bytes recieved");
                char[] chars = new char[byteNumber];

                Decoder decoder = Encoding.UTF8.GetDecoder();
                int charLenght = decoder.GetChars(buffer, 0, byteNumber, chars, 0);
                string recived = new string(chars);

                logger.Log(recived);

                socket.Disconnect(false);
                socket.Close();
            }
            catch(SocketException e) {
                logger.Exception("Connection to server failed : " + e.Message);
            }
        }

    }
}
