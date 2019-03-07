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
using SkiaSharp.Views.Desktop;


namespace NebScope
{
    public partial class ScopeForm : Form
    {
        #region Constants
        /// <summary>Whitespace around outside edges.</summary>
        const int BORDER_PAD = 20;

        /// <summary>Reserved for axis.</summary>
        const int X_AXIS_SPACE = 40;

        /// <summary>Reserved for axis.</summary>
        const int Y_AXIS_SPACE = 80;
        #endregion

        #region Fields
        /// <summary>Current user settings.</summary>
        UserSettings _settings = null;

        /// <summary>UI region to draw the data.</summary>
        RectangleF _dataRegion = new RectangleF();

        /// <summary>Current pen to draw with.</summary>
        SKPaint _pen = new SKPaint()
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Stroke,
            IsStroke = true,
            StrokeWidth = 2,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true
        };

        /// <summary>Current font to draw with.</summary>
        SKPaint _text = new SKPaint()
        {
            TextSize = 14,
            Color = SKColors.White,
            Typeface = SKTypeface.FromFamilyName("Arial"),
            TextAlign = SKTextAlign.Left,
            IsAntialias = true
        };

        /// <summary>Input device.</summary>
        UdpClient _udp = null;
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
                UserSettings.Load();
                _settings = UserSettings.Load();

                ///// Init the form /////
                Location = new Point(_settings.FormX, _settings.FormY);
                Size = new Size(_settings.FormWidth, _settings.FormHeight);
                WindowState = FormWindowState.Normal;
                BackColor = _settings.BackColor;

                ///// Control visuals /////
                skControl.BackColor = Color.Black;
                potXPosition.ControlColor = _settings.ControlColor;
                potCh1Position.ControlColor = _settings.ControlColor;
                potCh2Position.ControlColor = _settings.ControlColor;
                selCh1VoltsPerDiv.ForeColor = _settings.ControlColor;
                selCh2VoltsPerDiv.ForeColor = _settings.ControlColor;
                selTimebase.ForeColor = _settings.ControlColor;

                ///// Control handlers /////
                skControl.Resize += SkControl_Resize;
                skControl.PaintSurface += SkControl_PaintSurface;

                ///// Selectors /////
                selCh1VoltsPerDiv.Items.AddRange(Common.VOLT_OPTIONS);
                selCh1VoltsPerDiv.SelectedItem = "0.5";
                selCh2VoltsPerDiv.Items.AddRange(Common.VOLT_OPTIONS);
                selCh2VoltsPerDiv.SelectedItem = "0.5";
                selTimebase.Items.AddRange(Common.TIMEBASE_OPTIONS);
                selTimebase.SelectedItem = "0.1";

                CalcDrawRegion();

                ///// Start UDP server /////
                _udp = new UdpClient(Common.UDP_PORT);
                _udp.BeginReceive(new AsyncCallback(UdpReceive), this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(caption:"caption", text:ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScopeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.FormX = Location.X;
            _settings.FormY = Location.Y;
            _settings.FormWidth = Size.Width;
            _settings.FormHeight = Size.Height;

            _settings.Save();
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

            //_udp?.Close();
            _udp?.Dispose();
            _udp = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Directly update the data for the channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        public void UpdateData(int channel, int cmd, double[] data)
        {
            // Check validity and size of data.
            if(channel < 0 || channel > 1 || cmd < 0 || cmd > 1 || data == null)
            {
                // fail
            }
            else
            {
                CalcDrawRegion();
                _settings.Channels[channel].UpdateData(cmd, data);

                // Ask for a redraw.
                skControl.Invalidate();
            }
        }

        /// <summary>
        /// Helper.
        /// </summary>
        /// <param name="channelNum"></param>
        /// <returns></returns>
        public Channel GetChannel(int channelNum)
        {
            if (channelNum >= Common.NUM_CHANNELS || channelNum < 0)
            {
                throw new Exception("Invalid channel number");
            }

            return _settings.Channels[channelNum];
        }
        #endregion

        #region Window Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Sel_SelectedValueChanged(object sender, EventArgs e)
        {
            bool redraw = false;

            switch (sender)
            {
                case ComboBox cb when cb == selCh1VoltsPerDiv:
                    _settings.Channels[0].VoltsPerDivision = double.Parse(cb.SelectedItem.ToString());
                    redraw = true;
                    break;

                case ComboBox cb when cb == selCh2VoltsPerDiv:
                    _settings.Channels[1].VoltsPerDivision = double.Parse(cb.SelectedItem.ToString());
                    redraw = true;
                    break;

                case ComboBox cb when cb == selTimebase:
                    _settings.TimePerDivision = double.Parse(cb.SelectedItem.ToString());
                    redraw = true;
                    break;
            }

            if (redraw)
            {
                // Ask for a redraw.
                skControl.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pot_ValueChanged(object sender, EventArgs e)
        {
            bool redraw = false;

            switch (sender)
            {
                case Pot pot when pot == potXPosition:
                    _settings.XPosition = pot.Value;
                    redraw = true;
                    break;

                case Pot pot when pot == potCh1Position:
                    _settings.Channels[0].Position = -pot.Value;
                    redraw = true;
                    break;

                case Pot pot when pot == potCh2Position:
                    _settings.Channels[1].Position = -pot.Value;
                    redraw = true;
                    break;
            }

            if (redraw)
            {
                // Ask for a redraw.
                skControl.Invalidate();
            }
        }

        /// <summary>
        /// Reset values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pot_DoubleClick(object sender, EventArgs e)
        {
            if(sender is Pot)
            {
                (sender as Pot).Value = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SkControl_Resize(object sender, EventArgs e)
        {
            CalcDrawRegion();

            // Ask for a redraw.
            skControl.Invalidate();
        }
        #endregion

        #region Render functions
        /// <summary>
        /// Draw the main area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScopeForm_Paint(object sender, PaintEventArgs e)
        {
            // Ask for a redraw.
            skControl.Invalidate();
        }

        /// <summary>
        /// Draw the display area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SkControl_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            // Draw axes first before clipping.
            DrawAxes(canvas);

            DrawLabels(canvas);

            // Now clip to drawing region.
            canvas.ClipRect(_dataRegion.ToSKRect());

            DrawData(canvas);
        }

        /// <summary>
        /// Draw lines.
        /// </summary>
        /// <param name="canvas"></param>
        void DrawData(SKCanvas canvas)
        {
            foreach (Channel ser in _settings.Channels)
            {
                if(ser.DataPoints != null && ser.DataPoints.Count() >= 2)
                {
                    _pen.Color = ser.Color.ToSKColor();
                    _pen.StrokeWidth = (float)_settings.StrokeSize;

                    SKPath path = new SKPath();
                    SKPoint[] points = new SKPoint[ser.DataPoints.Count()];

                    var mapped = ser.MapData(_dataRegion, _settings.XPosition, _settings.SampleRate * _settings.TimePerDivision);

                    for (int i = 0; i < mapped.Count(); i++)
                    {
                        points[i] = mapped[i];
                    }

                    path.AddPoly(points, false);
                    canvas.DrawPath(path, _pen);
                }
            }
        }

        /// <summary>
        /// Draw axis labels.
        /// </summary>
        /// <param name="canvas"></param>
        void DrawLabels(SKCanvas canvas)
        {
            float left1 = _dataRegion.Left - 90;
            float left2 = _dataRegion.Left - 45;
            float bottom = _dataRegion.Bottom + 30;

            ///// X axis /////
            double xTotal = _settings.TimePerDivision * Common.NUM_X_DIVISIONS;
            double xOffset = _settings.XPosition * xTotal;
            double xMin = 0 + xOffset;
            double xMax = xTotal + xOffset;

            canvas.DrawText($"{xMin:0.00}", _dataRegion.Left - 10, bottom, _text);
            canvas.DrawText($"{xMax:0.00}", _dataRegion.Right - 10, bottom, _text);

            ///// Y axis ch1 /////
            double y1Total = _settings.Channels[0].VoltsPerDivision * Common.NUM_Y_DIVISIONS;
            double y1Offset = _settings.Channels[0].Position * y1Total;
            double y1Min = -y1Total / 2 + y1Offset;
            double y1Max = y1Total / 2 + y1Offset;
            double y1Mid = y1Max - y1Total / 2;

            _text.Color = _settings.Channels[0].Color.ToSKColor();
            canvas.DrawText($"{y1Min:0.00}", left1, _dataRegion.Bottom - _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y1Max:0.00}", left1, _dataRegion.Top + _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y1Mid:0.00}", left1, _dataRegion.Top + _dataRegion.Height / 2, _text);

            ///// Y axis ch2 /////
            double y2Total = _settings.Channels[1].VoltsPerDivision * Common.NUM_Y_DIVISIONS;
            double y2Offset = _settings.Channels[1].Position * y2Total;
            double y2Min = -y2Total / 2 + y2Offset;
            double y2Max = y2Total / 2 + y2Offset;
            double y2Mid = y2Max - y2Total / 2;

            _text.Color = _settings.Channels[1].Color.ToSKColor();
            canvas.DrawText($"{y2Min:0.00}", left2, _dataRegion.Bottom - _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y2Max:0.00}", left2, _dataRegion.Top + _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y2Mid:0.00}", left2, _dataRegion.Top + _dataRegion.Height / 2, _text);
        }

        /// <summary>
        /// Draw axes.
        /// </summary>
        /// <param name="canvas"></param>
        void DrawAxes(SKCanvas canvas)
        {
            _pen.Color = SKColors.LightBlue;

            float xinc = _dataRegion.Width / Common.NUM_X_DIVISIONS;
            float yinc = _dataRegion.Height / Common.NUM_Y_DIVISIONS;

            /////
            for (int i = 0; i <= Common.NUM_X_DIVISIONS; i++)
            {
                _pen.StrokeWidth = i == 0 ? 2.0f : 0.6f;
                canvas.DrawLine(_dataRegion.Left + i * xinc, _dataRegion.Top, _dataRegion.Left + i * xinc, _dataRegion.Bottom, _pen);
            }

            /////
            for (int i = 0; i <= Common.NUM_Y_DIVISIONS; i++)
            {
                _pen.StrokeWidth = i == Common.NUM_Y_DIVISIONS / 2 ? 2.0f : 0.6f;
                canvas.DrawLine(_dataRegion.Left, _dataRegion.Top + i * yinc, _dataRegion.Right, _dataRegion.Top + i * yinc, _pen);
            }
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Handle a received packet.
        /// </summary>
        /// <param name="ares"></param>
        void UdpReceive(IAsyncResult ares)
        {
            byte[] bytes = null;

            // Process input.
            IPEndPoint senderIp = new IPEndPoint(0, 0);
            bytes = _udp?.EndReceive(ares, ref senderIp);

            var (channel, cmd, data) = UnpackMsg(bytes);

            UpdateData(channel, cmd, data);

            // Listen again.
            _udp?.BeginReceive(new AsyncCallback(UdpReceive), this);
        }

        /// <summary>
        /// Where to put the lines.
        /// </summary>
        void CalcDrawRegion()
        {
            // Calc the drawing region.
            _dataRegion = new Rectangle(
                skControl.Left + BORDER_PAD + Y_AXIS_SPACE,
                skControl.Top + BORDER_PAD,
                skControl.Width - BORDER_PAD - BORDER_PAD - Y_AXIS_SPACE,
                skControl.Height - BORDER_PAD - BORDER_PAD - X_AXIS_SPACE);
        }
        #endregion

        /// <summary>
        /// Unpack a standard message from UDP bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>tuple of channel, cmd, data. Will be -1/null if invalid.</returns>
        (int channel, int cmd, double[] data) UnpackMsg(byte[] bytes)
        {
            int channel = -1;
            int cmd = -1;
            double[] data = null;
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
                //TODON? logger? or textbox?                
            }

            return (channel:channel, cmd:cmd, data:data);
        }

        private void btnTest_Click(object sender, EventArgs e)
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
        }
    }
}