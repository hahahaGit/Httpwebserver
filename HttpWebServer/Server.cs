using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class Server
    {
        private TcpClient connectionSocket;
       // Setting The Location For File Source
        private static readonly string RootCatalog = @"c:/temp";

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
                string answer;
                //string answer="Hey Static Message";
                //while (message != null && message != "")
                {
                    //Console.WriteLine("Client: " + message);
// Splitting The Readline by space
                    string[] words = message.Split(' ');
                    
                    foreach (string word  in words)
                    {
                        Console.WriteLine(word);
                    }
                   
                    //sw.WriteLine(words[1]);
                    //message = sr.ReadLine();
                 
                    string PathCheck = RootCatalog + words[1];
                    // checking the file location
                    if (!File.Exists(PathCheck))
                    {
                        sw.WriteLine("Http/1.0 404 Not Found\r\n\r\n");
                        message = sr.ReadLine();
                    }
                    else
                    {
                        answer = "Http/1.0 200 OK\r\n\r\n";
                        sw.WriteLine(answer);
                        message = sr.ReadLine();
                        using (FileStream source = File.Open(PathCheck, FileMode.Open, FileAccess.Read))
                        {
                            source.CopyTo(sw.BaseStream);
                            source.Flush();
                            source.Close();

                        }
                        Console.WriteLine(message);
                    }


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
