using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Client
{
    class Program
    {
        static UdpClient _udp;
        const int NUM_CHANNELS = 2;

        static void Main(string[] args)
        {
            // Set up UDP sender.
            _udp = new UdpClient(0);
            _udp.Connect("127.0.0.1", 9888);

            Go1();

            // Shut down.
            _udp?.Close();
            _udp?.Dispose();
        }

        static void Go1()
        {
            // Make some data. Max of 65k bytes. 5000 floats == 40000 bytes.
            int buffSize = 5000;
            float[] interleaved = new float[buffSize * NUM_CHANNELS];

            for (int i = 0; i < buffSize; i++)
            {
                interleaved[i * NUM_CHANNELS + 0] = (float)Math.Sin(i / 50.0);
                interleaved[i * NUM_CHANNELS + 1] = i / 50.0f % 1.0f;
            }

            // Package it up and send it.
            int dataSize = sizeof(float);
            byte[] buff = new byte[interleaved.Count() * dataSize];
            Log($"buff:{buff.Length}");

            for (int i = 0; i < interleaved.Count(); i++)
            {
                byte[] bytes = BitConverter.GetBytes(interleaved[i]);
                Array.Copy(bytes, 0, buff, i * dataSize, dataSize);
            }

            int num = _udp.Send(buff, buff.Count());
            Log($"_udpClient.Send:{num}");
        }

        static void Log(string msg)
        {
            Console.WriteLine(msg + Environment.NewLine);
        }
    }
}
