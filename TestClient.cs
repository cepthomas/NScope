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
using NBagOfTricks;


namespace NebScope
{
    public partial class TestClient : Form
    {
        static readonly UdpClient _udp = new(0);

        /// <summary>
        /// 
        /// </summary>
        public TestClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestClient_Load(object sender, EventArgs e)
        {
            // Set up UDP sender.
            _udp.Connect("127.0.0.1", 9888);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Shut down.
            _udp.Close();
            _udp.Dispose();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Log(string msg)
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
