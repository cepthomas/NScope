using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestClient
{
    /// <summary>
    /// A test UDP sender application.
    /// </summary>
    public partial class TestForm : Form
    {
        UdpClient _udp;
        const int NUM_CHANNELS = 2;

        #region Lifecycle
        public TestForm()
        {
            InitializeComponent();
        }

        void TestForm_Load(object sender, EventArgs e)
        {
            // Set up UDP sender.
            _udp = new UdpClient(0);
            _udp.Connect("127.0.0.1", 9888);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            _udp?.Close();
            _udp?.Dispose();
            _udp = null;

            base.Dispose(disposing);
        }
        #endregion

        void Log(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
            txtLog.ScrollToCaret();
        }

        void Go1()
        {
            // interleaved buffer size of 1024. At 44100 sample rate, 1 buff = 23 msec.

            // Make some data.
            int buffSize = 1024;
            float[] interleaved = new float[buffSize * NUM_CHANNELS];

            for (int i = 0; i < buffSize; i++)
            {
                interleaved[i * NUM_CHANNELS + 0] = (float)Math.Sin(i / 100.0);
                interleaved[i * NUM_CHANNELS + 1] = i / 50.0f % 1.0f;
            }

            // Package it up and send it.
            int dataSize = sizeof(float);
            byte[] buff = new byte[interleaved.Count() * dataSize];

            for (int i = 0; i < interleaved.Count(); i++)
            {
                byte[] bytes = BitConverter.GetBytes(interleaved[i]);
                Array.Copy(bytes, 0, buff, i * dataSize, dataSize);
            }

            int num = _udp.Send(buff, buff.Count());
            Log($"_udpClient.Send:{num}");
        }

        void chkRun_CheckedChanged(object sender, EventArgs e)
        {
            if(chkRun.Checked)
            {
                Log("Sending UDP");
                Go1();
                Log("Finished UDP");

                chkRun.Checked = false;
            }
        }
    }
}
