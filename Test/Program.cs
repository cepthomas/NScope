using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NBagOfTricks;
using NBagOfTricks.PNUT;


namespace NebScope.Test
{
    // Example of a scope client/sender.
    class Program
    {
        static UdpClient _udp;

        static void Main()
        {
            // Set up UDP sender.
            _udp = new UdpClient(0);
            _udp.Connect("127.0.0.1", 9888);

            Test1();

            // Shut down.
            _udp?.Close();
            _udp?.Dispose();
        }

        static int SendMsg(int channel, int cmd, float[] vals)
        {
            int dataSize = 4; // I know this.
            byte[] buff = new byte[(2 + vals.Length) * dataSize];

            byte[] bytes = BitConverter.GetBytes(channel);
            Array.Copy(bytes, 0, buff, 0 * dataSize, dataSize);

            bytes = BitConverter.GetBytes(cmd);
            Array.Copy(bytes, 0, buff, 1 * dataSize, dataSize);

            for (int i = 0; i < vals.Length; i++)
            {
                bytes = BitConverter.GetBytes(vals[i]);
                Array.Copy(bytes, 0, buff, (i + 2) * dataSize, dataSize);
            }

            int num = _udp.Send(buff, buff.Length);

            return num;
        }

        static void Test1()
        {
            // Make some data. Max of 65k bytes. 5000 floats == 20000 bytes.
            int buffSize = 9000;
            float[] ch1 = new float[buffSize];
            float[] ch2 = new float[buffSize];

            for (int i = 0; i < buffSize; i++)
            {
                ch1[i] = (float)Math.Sin(i / 500.0);
                ch2[i] = i / 1500.0f % 1.0f;
            }

            int num = SendMsg(0, 1, ch1);
            Log($"ch1 buff:{ch1.Length} sent:{num}");

            num = SendMsg(1, 1, ch2);
            Log($"ch2 buff:{ch2.Length} sent:{num}");
        }

        static void Log(string msg)
        {
            Console.WriteLine(msg + Environment.NewLine);
        }
    }
}
