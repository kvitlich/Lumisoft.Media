using System;
using System.Net;
using System.Net.Sockets;
using LumiSoft.Net;
using LumiSoft.Media;
using LumiSoft.Media.Wave;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace UdpLesson
{
    class Program
    {
        public static List<byte> mediaData = new List<byte>();
        static void Main(string[] args)
        {
            Test test = new Test();

            Thread.Sleep(10000);
            test.m_pSoundReceiver.Stop();
            Console.WriteLine("all, done");

            Console.ReadLine();
            Console.WriteLine($"Hey {WaveOut.Devices[0].Name}");
            WaveOut waveOut = new WaveOut(WaveOut.Devices[0], 8000, 16, 1);
            waveOut.Play(mediaData.ToArray(), 300, 0);
            //UdpClient udpClient = new UdpClient(3132);
            //IPEndPoint endPoint = null; 
            //var result = udpClient.Receive(ref endPoint);

            ////для мультикаст сообщений 
            ////udpClient.JoinMulticastGroup(IPAddress.Parse("47.0.0.1"));

            //udpClient.Close();

            //UdpClient senderUdpClient = new UdpClient(3231);
            //var text = "Hello!";
            //var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            //var sendedbytes = senderUdpClient.Send(bytes, bytes.Length);
            //senderUdpClient.Close();
            Console.ReadKey();
        }
    }

    public class Test
    {
        public WaveIn m_pSoundReceiver = null;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Test()
        {
            // G711 needs 8KHZ 16 bit 1 channel audio, 
            // 400kb buffer gives us 25ms audio frame.
            m_pSoundReceiver = new WaveIn(WaveIn.Devices[0], 8000, 16, 1, 400);
            m_pSoundReceiver.BufferFull += new BufferFullHandler
                                             (m_pSoundReceiver_BufferFull);
            m_pSoundReceiver.Start();
        }

        /// <summary>
        /// This method is called when recording buffer is full 
        /// and we need to process it.
        /// </summary>
        /// <param name="buffer">Recorded data.</param>
        private void m_pSoundReceiver_BufferFull(byte[] buffer)
        {
            Program.mediaData.AddRange(buffer);
            Console.WriteLine("Message got");

            // Just store audio data or stream it over the network ... 
        }
    }
}
