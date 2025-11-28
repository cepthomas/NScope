using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Ephemera.NBagOfTricks;


namespace NScope
{
    public partial class TestClient : Form //TODO1 clean this up
    {
        /// <summary>Native client.</summary>
        readonly UdpClient _udp = new(0);

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestClient()
        {
            InitializeComponent();
            // Set up UDP sender.
            _udp.Connect("127.0.0.1", Common.Settings.Port);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Shut down.
            _udp.Close();
            _udp.Dispose();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Send_Click(object sender, EventArgs e)
        {
            Send1();
        }

        /// <summary>
        /// 
        /// </summary>
        void Send1()
        {
            // Make some data. Max of 65k bytes. 5000 floats == 20000 bytes.
            int buffSize = 12000;
            float[] ch1 = new float[buffSize];
            float[] ch2 = new float[buffSize];

            var r = new Random();
            double r1 = r!.NextDouble() + 0.5;
            double r2 = r!.NextDouble() + 0.5;
            double r3 = r!.NextDouble() * 10.0 - 5;
            double r4 = r!.NextDouble() * 10.0 - 5;

            for (int i = 0; i < buffSize; i++)
            {
                ch1[i] = (float)(Math.Sin(i * r1 / 500.0 * r2) * r3);
                ch2[i] = (float)((i * r1) / (500.0 * r2) % 1.0 * r4);
            }

            int num = SendMsg(0, 1, ch1);
            Tell($"ch1 buff:{ch1.Length} sent:{num}");

            num = SendMsg(1, 1, ch2);
            Tell($"ch2 buff:{ch2.Length} sent:{num}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Tell(string msg)
        {
            rtbLog.AppendText(msg + Environment.NewLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="cmd"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        int SendMsg(int channel, int cmd, float[] vals)
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
    }
}
