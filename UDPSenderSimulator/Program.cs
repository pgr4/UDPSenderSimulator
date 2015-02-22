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
        private const int listenPort = 30003;

        static void Main(string[] args)
        {
            //ipToInt(192,168,1,247);
            //SendArduinoMessage();
            //SendAppMessage();
            Receive();
        }

        private static void SendArduinoMessage()
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.1.23");

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
                //10=gotoTrack
                //11=play
                //12=lift
                //13=stop
                byte command = 0x10;

                //Signal end of Header Info
                byte[] cutoffSequence = new byte[6];
                cutoffSequence[0] = 111;
                cutoffSequence[1] = 111;
                cutoffSequence[2] = 111;
                cutoffSequence[3] = 111;
                cutoffSequence[4] = 111;
                cutoffSequence[5] = 111;

                byte message = 0x51;

                byte[] SendArray = new byte[sourceIP.Length + destinationIP.Length + cutoffSequence.Length + 1 + 1];
                sourceIP.CopyTo(SendArray, 0);
                destinationIP.CopyTo(SendArray, 4);
                SendArray[8] = command;
                cutoffSequence.CopyTo(SendArray, 9);
                SendArray[15] = command;

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

        private static void SendAppMessage()
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.1.23");

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

    }
}
