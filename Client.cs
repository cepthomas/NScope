using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle("NebScope.Client")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Ephemera")]
[assembly: AssemblyProduct("NebScope")]
[assembly: AssemblyCopyright("MIT License")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("dd14fce7-1cd5-4412-8912-9b75df0e3c7b")]
[assembly: AssemblyVersion("0.9.*")]
//[assembly: AssemblyFileVersion("1.0.0.0")]


namespace Client
{
    // Example of a scope client/sender.
    class Program
    {
        static UdpClient _udp;

        static void Main(string[] args)
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
            byte[] buff = new byte[(2 + vals.Count()) * dataSize];

            byte[] bytes = BitConverter.GetBytes(channel);
            Array.Copy(bytes, 0, buff, 0 * dataSize, dataSize);

            bytes = BitConverter.GetBytes(cmd);
            Array.Copy(bytes, 0, buff, 1 * dataSize, dataSize);

            for (int i = 0; i < vals.Count(); i++)
            {
                bytes = BitConverter.GetBytes(vals[i]);
                Array.Copy(bytes, 0, buff, (i + 2) * dataSize, dataSize);
            }

            int num = _udp.Send(buff, buff.Count());

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
