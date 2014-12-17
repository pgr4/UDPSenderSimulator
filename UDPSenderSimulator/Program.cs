﻿using System;
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
                sourceIP[3] = 2;

                //Destination IP
                byte[] destinationIP = new byte[4];
                destinationIP[0] = 192;
                destinationIP[1] = 168;
                destinationIP[2] = 1;
                destinationIP[3] = 5;

                //Command
                //0=none
                //1=new album
                //2=current album
                byte command = 0x01;

                //Signal end of Header Info
                byte[] cutoffSequence = new byte[6];
                cutoffSequence[0] = 111;
                cutoffSequence[1] = 111;
                cutoffSequence[2] = 111;
                cutoffSequence[3] = 111;
                cutoffSequence[4] = 111;
                cutoffSequence[5] = 111;

                //Message ID for now is the unique ID
                byte[] message = new byte[3];
                message[0] = 0x57;  //id
                message[1] = 0x51;  //id
                //Amount of breaks
                message[2] = 1;     //songs

                byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + message.Length + 1];
                sourceIP.CopyTo(SendArray, 0);
                destinationIP.CopyTo(SendArray, 4);
                SendArray[8] = command;
                cutoffSequence.CopyTo(SendArray, 9);
                message.CopyTo(SendArray, 15);

                s.SendTo(SendArray, ep);

                Console.WriteLine("Message sent to the broadcast address");
            }
            catch(Exception e)
            {
                Console.Write(e.Message + "\n");
                Console.Write(e.InnerException);
                Console.Read();
            }
        }
    }
}
