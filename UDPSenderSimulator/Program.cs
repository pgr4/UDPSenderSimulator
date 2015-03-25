using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPSenderSimulator
{
    class Program
    {
        private const int listenPort = 30003;

        static void Main(string[] args)
        {
            //SendNewAlbum();
            //SendUpdatePos();
            //SendCommand(21);
            //ipToInt(192, 168, 1, 255);
            DoSend();
        }

        private static void SendNewAlbum()
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.1.255");

                IPEndPoint ep = new IPEndPoint(broadcast, 30003);

                //Sender IP
                byte[] sourceIP = new byte[4];
                sourceIP[0] = 192;
                sourceIP[1] = 168;
                sourceIP[2] = 1;
                sourceIP[3] = 23;

                //Destination IP
                byte[] destinationIP = new byte[4];
                destinationIP[0] = 192;
                destinationIP[1] = 168;
                destinationIP[2] = 1;
                destinationIP[3] = 247;

                //Command
                byte command = 1;

                //Signal end of Header Info
                byte[] cutoffSequence = new byte[6];
                cutoffSequence[0] = 111;
                cutoffSequence[1] = 111;
                cutoffSequence[2] = 111;
                cutoffSequence[3] = 111;
                cutoffSequence[4] = 111;
                cutoffSequence[5] = 111;

                byte[] key = {69,20,30,40,50,60,70,80,90,100};

                byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + key.Length + cutoffSequence.Length  + 1];
                sourceIP.CopyTo(SendArray, 0);
                destinationIP.CopyTo(SendArray, 4);
                SendArray[8] = command;
                cutoffSequence.CopyTo(SendArray, 9);
                key.CopyTo(SendArray,15);
                cutoffSequence.CopyTo(SendArray,25);

                s.SendTo(SendArray, ep);

                Console.WriteLine("Message sent to the broadcast address");
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\n");
                Console.Write(e.InnerException);
                Console.Read();
            }
        }

        private static void SendUpdatePos()
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.1.255");

                IPEndPoint ep = new IPEndPoint(broadcast, 30003);

                //Sender IP
                byte[] sourceIP = new byte[4];
                sourceIP[0] = 192;
                sourceIP[1] = 168;
                sourceIP[2] = 1;
                sourceIP[3] = 23;

                //Destination IP
                byte[] destinationIP = new byte[4];
                destinationIP[0] = 192;
                destinationIP[1] = 168;
                destinationIP[2] = 1;
                destinationIP[3] = 247;

                //Command
                byte command = 9;

                //Signal end of Header Info
                byte[] cutoffSequence = new byte[6];
                cutoffSequence[0] = 111;
                cutoffSequence[1] = 111;
                cutoffSequence[2] = 111;
                cutoffSequence[3] = 111;
                cutoffSequence[4] = 111;
                cutoffSequence[5] = 111;


                byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + 2];
                sourceIP.CopyTo(SendArray, 0);
                destinationIP.CopyTo(SendArray, 4);
                SendArray[8] = command;
                cutoffSequence.CopyTo(SendArray, 9);
                SendArray[SendArray.Length - 1] = 10;

                s.SendTo(SendArray, ep);

                Console.WriteLine("Message sent to the broadcast address");
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\n");
                Console.Write(e.InnerException);
                Console.Read();
            }
        }

        private static void SendCommand(int c)
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.1.255");

                IPEndPoint ep = new IPEndPoint(broadcast, 30003);

                //Sender IP
                byte[] sourceIP = new byte[4];
                sourceIP[0] = 192;
                sourceIP[1] = 168;
                sourceIP[2] = 1;
                sourceIP[3] = 23;

                //Destination IP
                byte[] destinationIP = new byte[4];
                destinationIP[0] = 192;
                destinationIP[1] = 168;
                destinationIP[2] = 1;
                destinationIP[3] = 247;

                //Command
                byte command = (byte)c;

                //Signal end of Header Info
                byte[] cutoffSequence = new byte[6];
                cutoffSequence[0] = 111;
                cutoffSequence[1] = 111;
                cutoffSequence[2] = 111;
                cutoffSequence[3] = 111;
                cutoffSequence[4] = 111;
                cutoffSequence[5] = 111;


                byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + 1];
                sourceIP.CopyTo(SendArray, 0);
                destinationIP.CopyTo(SendArray, 4);
                SendArray[8] = command;
                cutoffSequence.CopyTo(SendArray, 9);

                s.SendTo(SendArray, ep);

                Console.WriteLine("Message sent to the broadcast address");
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\n");
                Console.Write(e.InnerException);
                Console.Read();
            }
        }

        private static void Receive()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    int pointer = 0;

                    byte[] bytes = listener.Receive(ref groupEP);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                listener.Close();
            }

        }

        private static void ipToInt(uint f, uint s, uint t, uint l)
        {
            uint ip = 0;
            ip += f << 24;
            ip += s << 16;
            ip += t << 8;
            ip += l;

        }

        private static void DoSend()
        {
            IPAddress broadcast = IPAddress.Parse("192.168.1.255");

            IPEndPoint ep = new IPEndPoint(broadcast, 30003);
            TcpClient tcpClient = new TcpClient(ep);
            Socket socket = tcpClient.Client;
            byte[] x = {0, 1, 2, 3, 4};
            try
            { // sends the text with timeout 10s
                Send(socket, x, 0, x.Length, 10000);
            }
            catch (Exception ex) { /* ... */ }
        }

        public static void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            int startTickCount = Environment.TickCount;
            int sent = 0;  // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (sent < size);
        }

    }
}
