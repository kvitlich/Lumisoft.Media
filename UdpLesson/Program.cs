using System;
using System.Net;
using System.Net.Sockets;
using LumiSoft.Net;
using LumiSoft.Media;
using LumiSoft.Media.Wave;
using System.Collections.Generic;
using System.Threading;

namespace UdpLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
           
            while(true)
            {
                Thread.Sleep(10000);
            }
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
        private WaveIn m_pSoundReceiver = null;
        private List<byte> mediaData = new List<byte>();
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
            mediaData.AddRange(buffer);
            Console.WriteLine("Message got");
            
            // Just store audio data or stream it over the network ... 
        }
    }
}
