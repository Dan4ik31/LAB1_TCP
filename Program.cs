using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTcpClient
{
    class Program
    {
        static int port = 8888;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                Console.Write("Hi,send message:");
                string message = Console.ReadLine();
                string str = message.Replace(" ", "");
                byte[] data = Encoding.Unicode.GetBytes(str);
                socket.Send(data);

                TcpClient client = new TcpClient();
                client.Connect(address, port);

                byte[] buffer = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                do
                {
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    response.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (stream.DataAvailable);

                Console.WriteLine(response.ToString());

                stream.Close();
                client.Close();
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}