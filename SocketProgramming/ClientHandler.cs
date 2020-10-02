using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SocketProgramming {
    class ClientHandler {

        private NetworkLogger logger = new NetworkLogger(false);
        private Thread thread;
        private Socket current;

        public ClientHandler() {
            this.thread = new Thread(new ThreadStart(this.Start));
        }

        public void NewClient(Socket current) {
            this.current = current;
        }

        public void Start() {
            Socket handler = current;

            // Incoming data from the client.    
            string data = null;
            byte[] bytes = null;

            try {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                string[] words = data.Split(' ');

                string rebuild = "";

                for (int i = 1; i < words.Length; i++) {
                    rebuild += words[i] + " ";
                }

                logger.Log("Client " + words[0] + " connected");
                logger.Log("Data recieved : " + rebuild);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Close();
            }
            catch (Exception e) {
                logger.Log("Failed to recieve " + e.Message);
            }

            //identifyer
            if (data.IndexOf(".") > -1) {
                //something something
            }
        }
    }
}
