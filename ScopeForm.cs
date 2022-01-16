﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using SkiaSharp;
using NBagOfTricks;
using NBagOfUis;


namespace NebScope
{
    public partial class ScopeForm : Form
    {
        #region Fields
        /// <summary>Input device.</summary>
        UdpClient? _udp = null;

        /// <summary>Activity indicator.</summary>
        int _captureIndDelay = 0;

        /// <summary>For testing.</summary>
        readonly TestClient _client = new();
        #endregion

        #region Lifecycle
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScopeForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            DoubleBuffered = true;
            _client.Hide();
        }

        /// <summary>
        /// Initialize UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScopeForm_Load(object sender, EventArgs e)
        {
            try
            {
                ///// Settings /////
                string appDir = MiscUtils.GetAppDataDir("NebScope", "Ephemera");
                DirectoryInfo di = new(appDir);
                di.Create();
                Common.Settings = UserSettings.Load(appDir);

                ///// Init the form /////
                Location = new Point(Common.Settings.FormX, Common.Settings.FormY);
                Size = new Size(Common.Settings.FormWidth, Common.Settings.FormHeight);
                WindowState = FormWindowState.Normal;
                BackColor = Common.Settings.BackColor;

                ///// X pos pot /////
                potXPosition.Value = Common.Settings.XPosition;
                potXPosition.DrawColor = Common.Settings.ControlColor;
                potXPosition.BackColor = Common.Settings.BackColor;

                ///// Ch 1 pot /////
                potCh1Position.Value = Common.Settings.Channel1.Position;
                potCh1Position.DrawColor = Common.Settings.Channel1.Color;
                potCh1Position.BackColor = Common.Settings.BackColor;

                ///// Ch 2 pot /////
                potCh2Position.Value = Common.Settings.Channel2.Position;
                potCh2Position.DrawColor = Common.Settings.Channel2.Color;
                potCh2Position.BackColor = Common.Settings.BackColor;

                ///// Ch 1 volts /////
                //selCh1VoltsPerDiv.ForeColor = Common.Settings.ControlColor;
                selCh1VoltsPerDiv.Items.AddRange(Common.VoltOptions.Keys.ToArray());
                selCh1VoltsPerDiv.SelectedItem = Common.Settings.Channel1.VoltsPerDivision;

                ///// Ch 2 volts /////
                //selCh2VoltsPerDiv.ForeColor = Common.Settings.ControlColor;
                selCh2VoltsPerDiv.Items.AddRange(Common.VoltOptions.Keys.ToArray());
                selCh2VoltsPerDiv.SelectedItem = Common.Settings.Channel2.VoltsPerDivision;

                ///// Timebase /////
                //selTimebase.ForeColor = Common.Settings.ControlColor;
                selTimebase.Items.AddRange(Common.TimeOptions.Keys.ToArray());
                selTimebase.SelectedItem = Common.Settings.TimePerDivision;

                ///// Buttons /////
                btnHelp.BackColor = Common.Settings.BackColor;
                btnSettings.BackColor = Common.Settings.BackColor;

                ///// Checkboxes /////
                chkCapture.Checked = true;
                chkCapture.BackColor = Common.Settings.BackColor;
                //chkCapture.ForeColor = Common.Settings.ControlColor;
                chkCapture.FlatAppearance.CheckedBackColor = Common.Settings.ControlColor;

                ///// Start UDP server /////
                _udp = new UdpClient(Common.Settings.Port);
                _udp.BeginReceive(new AsyncCallback(UdpReceive), this);

                timerHousekeeping.Start();

                AddText("NebScope started");
            }
            catch (Exception ex)
            {
                AddText($"ERROR {ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScopeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common.Settings.FormX = Location.X;
            Common.Settings.FormY = Location.Y;
            Common.Settings.FormWidth = Size.Width;
            Common.Settings.FormHeight = Size.Height;

            Common.Settings.Save();

            _client.Close();
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

            _udp?.Close();
            _udp?.Dispose();
            _udp = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Directly update the data for the channel.
        /// </summary>
        /// <param name="channel">Chan 1 or 2 (0 or 1)</param>
        /// <param name="cmd">0 = append, 1 = overwrite.</param>
        /// <param name="data">The data to display.</param>
        void UpdateData(int channel, int cmd, double[]? data)
        {
            // Check validity and size of data.
            if(channel < 0 || channel > 1 || cmd < 0 || cmd > 1 || data is null)
            {
                // fail
            }
            else
            {
                Channel ch = channel == 0 ? Common.Settings.Channel1 : Common.Settings.Channel2;
                ch.UpdateData(cmd, data);
            }
        }
        #endregion

        #region Window Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Sel_SelectedValueChanged(object? sender, EventArgs e)
        {
            bool redraw = false;

            switch (sender)
            {
                case ComboBox cb when cb == selCh1VoltsPerDiv:
                    Common.Settings.Channel1.VoltsPerDivision = cb.SelectedItem.ToString()!;
                    redraw = true;
                    break;

                case ComboBox cb when cb == selCh2VoltsPerDiv:
                    Common.Settings.Channel2.VoltsPerDivision = cb.SelectedItem.ToString()!;
                    redraw = true;
                    break;

                case ComboBox cb when cb == selTimebase:
                    Common.Settings.TimePerDivision = cb.SelectedItem.ToString()!;
                    redraw = true;
                    break;
            }

            if (redraw)
            {
                // Ask for a redraw.
                display.UpdateData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pot_ValueChanged(object? sender, EventArgs e)
        {
            bool redraw = false;

            switch (sender)
            {
                case Pot pot when pot == potXPosition:
                    Common.Settings.XPosition = pot.Value;
                    redraw = true;
                    break;

                case Pot pot when pot == potCh1Position:
                    Common.Settings.Channel1.Position = -pot.Value;
                    redraw = true;
                    break;

                case Pot pot when pot == potCh2Position:
                    Common.Settings.Channel2.Position = -pot.Value;
                    redraw = true;
                    break;
            }

            if (redraw)
            {
                // Ask for a redraw.
                display.UpdateData();
            }
        }

        /// <summary>
        /// Reset values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pot_DoubleClick(object? sender, EventArgs e)
        {
            if(sender is Pot pot)
            {
                pot.Value = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChkCapture_CheckedChanged(object? sender, EventArgs e)
        {
            if(!chkCapture.Checked)
            {
                _captureIndDelay = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerHousekeeping_Tick(object? sender, EventArgs e)
        {
            if (_captureIndDelay > 0)
            {
                _captureIndDelay--;
                if (_captureIndDelay <= 0)
                {
                    _captureIndDelay = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnHelp_Click(object sender, EventArgs e)
        {
            // Make some markdown.
            Tools.MarkdownToHtml(File.ReadAllLines(@".\README.md").ToList(), Common.Settings.BackColor.Name, "helvetica", true);
        }

        /// <summary>
        /// Edit the common options in a property grid.
        /// </summary>
        void UserSettings_Click(object? sender, EventArgs e)
        {
            using Form f = new()
            {
                Text = "User Settings",
                Size = new Size(350, 400),
                StartPosition = FormStartPosition.Manual,
                Location = new Point(200, 200),
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            PropertyGrid pg = new()
            {
                Dock = DockStyle.Fill,
                PropertySort = PropertySort.NoSort,
                SelectedObject = Common.Settings
            };

            f.Controls.Add(pg);
            f.ShowDialog();

            Common.Settings.Save();
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Handle a received packet.
        /// </summary>
        /// <param name="ares"></param>
        void UdpReceive(IAsyncResult ares)
        {
            if(chkCapture.Checked)
            {
                // Process input.
                if(_udp is not null)
                {
                    IPEndPoint? senderIp = new(0, 0);
                    byte[] bytes = _udp.EndReceive(ares, ref senderIp);

                    var (channel, cmd, data) = UnpackMsg(bytes);

                    UpdateData(channel, cmd, data);

                    // Ask for a redraw.
                    display.UpdateData();

                    // Lights.
                    _captureIndDelay = 5;
                    chkCapture.BackColor = Common.Settings.ControlColor;
                }
            }

            // Listen again.
            _udp?.BeginReceive(new AsyncCallback(UdpReceive), this);
        }

        /// <summary>
        /// Unpack a standard message from UDP bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>tuple of channel, cmd, data. Will be -1/null if invalid.</returns>
        (int channel, int cmd, double[]? data) UnpackMsg(byte[] bytes)
        {
            int channel = -1;
            int cmd = -1;
            double[]? data = null;
            int dataSize = 4; // each element is this

            // Check validity and size of data. First two values are required params.
            if (bytes != null && bytes.Length >= 8 && bytes.Length % dataSize == 0)
            {
                // Unpack data.

                // Strip out command info.
                channel = BitConverter.ToInt32(bytes, 0 * dataSize);
                cmd = BitConverter.ToInt32(bytes, 1 * dataSize);

                int numVals = bytes.Length / dataSize - 2;
                data = new double[numVals];
                for (int i = 0; i < numVals; i++)
                {
                    int ind = (i + 2) * dataSize;
                    data[i] = BitConverter.ToSingle(bytes, ind);
                }
            }
            else
            {
                AddText("Bad message rcvd.");
            }

            return (channel, cmd, data);
        }

        /// <summary>
        /// A message to display to the user.
        /// </summary>
        /// <param name="text">The message.</param>
        void AddText(string text)
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                if (txtMsgs != null && !txtMsgs.IsDisposed)
                {
                    if (txtMsgs.TextLength > 1000)
                    {
                        txtMsgs.Select(0, 500);
                        txtMsgs.SelectedText = "";
                    }

                    txtMsgs.AppendText($"-> {text}{Environment.NewLine}");
                    txtMsgs.ScrollToCaret();
                }
            });
        }

        /// <summary>
        /// Test code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Msgs_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            _client.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        void DummyData()
        {
            // Make some data.
            int buffSize = 9000;
            double[] ch1 = new double[buffSize];
            double[] ch2 = new double[buffSize];

            for (int i = 0; i < buffSize; i++)
            {
                ch1[i] = (float)Math.Sin(i / 500.0);
                ch2[i] = i / 1500.0f % 1.0f;
            }

            UpdateData(0, 1, ch1);
            UpdateData(1, 1, ch2);

            // Ask for a redraw.
            display.UpdateData();
        }
        #endregion
    }
}
