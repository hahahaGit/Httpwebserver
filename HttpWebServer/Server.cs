using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class Server
    {
        private TcpClient connectionSocket;

        public Server(TcpClient connectionSocket)
        {

            this.connectionSocket = connectionSocket;
        }

        internal void SimpleReply()
        {


            try
            {
                Stream ns = connectionSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true; // enable automatic flushing

                string message = sr.ReadLine();
                string answer="Hey Static Message";
                //while (message != null && message != "")
                {
                    //Console.WriteLine("Client: " + message);


                   
                    sw.WriteLine(answer);
                    message = sr.ReadLine();
                    


                }
                ns.Close();
                connectionSocket.Close();
            }
            catch (SocketException se)
            {

                Console.WriteLine(se.Message);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
        }
    }
}
