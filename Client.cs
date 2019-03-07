using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle("NebScopeClient")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Tyrell Corp")]
[assembly: AssemblyProduct("NebScopeClient")]
[assembly: AssemblyCopyright("MIT License")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("dd14fce7-1cd5-4412-8912-9b75df0e3c7b")]
[assembly: AssemblyVersion("0.9.*")]
//[assembly: AssemblyFileVersion("1.0.0.0")]


namespace Client
{
    class Program
    {
        static UdpClient _udp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
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

        /// <summary>
        /// 
        /// </summary>
        static void Go1()
        {
            // Make some data. Max of 65k bytes. 5000 floats == 20000 bytes.
            int buffSize = 5000;
            float[] ch1 = new float[buffSize];
            float[] ch2 = new float[buffSize];

            float[] cmds1 = { 0, 1, 0, 0 }; // channelnum, reset, ...
            float[] cmds2 = { 1, 1, 0, 0 }; // channelnum, reset, ...

            for (int i = 0; i < buffSize; i++)
            {
                ch1[i] = (float)Math.Sin(i / 50.0);
                ch2[i] = i / 50.0f % 1.0f;
            }

            byte[] buff = Pack(cmds1, ch1);
            int num = _udp.Send(buff, buff.Count());
            Log($"ch1 buff:{buff.Length} sent:{num}");

            buff = Pack(cmds2, ch2);
            num = _udp.Send(buff, buff.Count());
            Log($"ch2 buff:{buff.Length} sent:{num}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        static void Log(string msg)
        {
            Console.WriteLine(msg + Environment.NewLine);
        }
    }
}
