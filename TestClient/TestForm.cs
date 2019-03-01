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
    public partial class TestForm : Form
    {
        #region Fields
        /// <summary>OSC output device.</summary>
        UdpClient _udpClient;

        /// <summary>Access synchronizer.</summary>
        object _lock = new object();

        /// <summary>Resource clean up.</summary>
        bool _disposed = false;
        #endregion


        #region Lifecycle

        public TestForm()
        {
            InitializeComponent();
        }

        public bool Init(string name)
        {
            bool inited = false;

            try
            {
                if (_udpClient != null)
                {
                    _udpClient.Close();
                    _udpClient.Dispose();
                    _udpClient = null;
                }

                _udpClient = new UdpClient(0);
                _udpClient.Connect("127.0.0.1", 9888);
                inited = true;
            }
            catch (Exception ex)
            {
                //LogMsg(DeviceLogCategory.Error, $"Init OSC out failed: {ex.Message}");
                inited = false;
            }

            return inited;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            _udpClient?.Close();
            _udpClient?.Dispose();
            _udpClient = null;

            base.Dispose(disposing);
        }
        #endregion


        /// <summary>
        /// Handle endianness.
        /// </summary>
        /// <param name="bytes">Data in place.</param>
        public void FixEndian(List<byte> bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }
        }


        public List<byte> Pack(float value)
        {
            List<byte> bytes = new List<byte>(BitConverter.GetBytes(value));
            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }
            return bytes;
        }



        public void Go1()
        {
            // Assume asio-like buffer size of 1024. At 44100 sample rate, 1 buff = 23 msec.


            // Make some data.
            int buffSize = 1024;
            float[] ch1Data = new float[buffSize];
            float[] ch2Data = new float[buffSize];

            for (int i = 0; i < buffSize; i++)
            {
                ch1Data[i] = (float)Math.Sin(i / 100.0);
                ch2Data[i] = i / 50.0f % 1.0f;
            }

            // Package it up and send it.
            //sf.SetData(ch1Data, ch2Data);

            if (_udpClient != null)
            {
                // Interleave.
                float[] interleaved = new float[buffSize * 2];

                for (int i = 0; i < buffSize; i++)
                {
                    interleaved[2 * i] = ch1Data[2 * i];
                    interleaved[2 * i + 1] = ch2Data[2 * i];
                }


                byte[] buff = new byte[interleaved.Count() * 4];

                for (int i = 0; i < interleaved.Count(); i++)
                {
                    byte[] bytes = BitConverter.GetBytes(interleaved[i]);

                    if (BitConverter.IsLittleEndian)
                    {
                        //lvals.Reverse();
                        buff[i * 4 + 0] = bytes[3];
                        buff[i * 4 + 1] = bytes[2];
                        buff[i * 4 + 2] = bytes[1];
                        buff[i * 4 + 3] = bytes[0];
                    }
                    else
                    {
                        buff[i * 4 + 0] = bytes[0];
                        buff[i * 4 + 1] = bytes[1];
                        buff[i * 4 + 2] = bytes[2];
                        buff[i * 4 + 3] = bytes[3];
                    }
                }

                _udpClient.Send(buff, buff.Count());
            }
        }

        private void chkRun_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
}
