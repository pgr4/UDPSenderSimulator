using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPSenderSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");

            IPEndPoint ep = new IPEndPoint(broadcast, 30003);

            //Sender IP
            byte[] sourceIP = new byte[4];
            sourceIP[0] = 192;
            sourceIP[1] = 168;
            sourceIP[2] = 1;
            sourceIP[3] = 2;

            //Destination IP
            byte[] destinationIP = new byte[4];
            destinationIP[0] = 192;
            destinationIP[1] = 168;
            destinationIP[2] = 1;
            destinationIP[3] = 5;

            //Command
            byte command = 0x01;

            //Signal end of Header Info
            byte[] cutoffSequence = new byte[6];
            cutoffSequence[0] = 111;
            cutoffSequence[1] = 111;
            cutoffSequence[2] = 111;
            cutoffSequence[3] = 111;
            cutoffSequence[4] = 111;
            cutoffSequence[5] = 111;

            //Message id (I can see this not being needed) It does however give us a way to show which songs came in together but this could be done elsewhere
            //Amount of breaks
            byte[] message = new byte[3];
            message[0] = 0x88;  //id
            message[1] = 0x89;  //id
            message[2] = 5;     //breaks

            byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + message.Length + 1];
            sourceIP.CopyTo(SendArray, 0);
            destinationIP.CopyTo(SendArray, 4);
            SendArray[8] = command;
            cutoffSequence.CopyTo(SendArray, 9);
            message.CopyTo(SendArray, 15);

            s.SendTo(SendArray, ep);

            Console.WriteLine("Message sent to the broadcast address");
        }
    }
}
