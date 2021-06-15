using System;
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
using NBagOfTricks;
using NBagOfTricks.UI;


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

        /// <summary>Activity indicator.</summary>
        int _captureIndDelay = 0;
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
                ///// Settings /////
                string appDir = MiscUtils.GetAppDataDir("NebScope");
                DirectoryInfo di = new DirectoryInfo(appDir);
                di.Create();
                _settings = UserSettings.Load(appDir);

                potXPosition.Value = _settings.XPosition;
                potCh1Position.Value = _settings.Channel1.Position;
                potCh2Position.Value = _settings.Channel2.Position;

                ///// Init the form /////
                Location = new Point(_settings.FormX, _settings.FormY);
                Size = new Size(_settings.FormWidth, _settings.FormHeight);
                WindowState = FormWindowState.Normal;
                BackColor = _settings.BackColor;

                ///// Control visuals /////
                skControl.BackColor = Color.Black;
                potXPosition.DrawColor = _settings.ControlColor;
                potXPosition.BackColor = _settings.BackColor;
                potCh1Position.DrawColor = _settings.ControlColor;
                potCh1Position.BackColor = _settings.BackColor;
                potCh2Position.DrawColor = _settings.ControlColor;
                potCh2Position.BackColor = _settings.BackColor;
                selCh1VoltsPerDiv.ForeColor = _settings.ControlColor;
                selCh2VoltsPerDiv.ForeColor = _settings.ControlColor;
                selTimebase.ForeColor = _settings.ControlColor;
                chkCapture.Checked = true;

                ///// Control handlers /////
                skControl.Resize += SkControl_Resize;
                skControl.PaintSurface += SkControl_PaintSurface;

                ///// Selectors /////
                selCh1VoltsPerDiv.Items.AddRange(Common.VoltOptions.Keys.ToArray());
                selCh2VoltsPerDiv.Items.AddRange(Common.VoltOptions.Keys.ToArray());
                selTimebase.Items.AddRange(Common.TimeOptions.Keys.ToArray());
                selCh1VoltsPerDiv.SelectedItem = _settings.Channel1.VoltsPerDivision;
                selCh2VoltsPerDiv.SelectedItem = _settings.Channel2.VoltsPerDivision;
                selTimebase.SelectedItem = _settings.TimePerDivision;

                CalcDrawRegion();

                ///// Start UDP server /////
                _udp = new UdpClient(_settings.Port);
                _udp.BeginReceive(new AsyncCallback(UdpReceive), this);

                timerHousekeeping.Start();

                AddText("NebScope started");
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
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
        /// <param name="channel">Chan 1 or 2 (0 or 1)</param>
        /// <param name="cmd">0 = append, 1 = overwrite.</param>
        /// <param name="data">The data to display.</param>
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

                Channel ch = channel == 0 ? _settings.Channel1 : _settings.Channel2;
                ch.UpdateData(cmd, data);

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
            return channelNum == 0 ? _settings.Channel1 : _settings.Channel2;
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
                    _settings.Channel1.VoltsPerDivision = cb.SelectedItem.ToString();
                    redraw = true;
                    break;

                case ComboBox cb when cb == selCh2VoltsPerDiv:
                    _settings.Channel2.VoltsPerDivision = cb.SelectedItem.ToString();
                    redraw = true;
                    break;

                case ComboBox cb when cb == selTimebase:
                    _settings.TimePerDivision = cb.SelectedItem.ToString();
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
                    _settings.Channel1.Position = -pot.Value;
                    redraw = true;
                    break;

                case Pot pot when pot == potCh2Position:
                    _settings.Channel2.Position = -pot.Value;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkCapture_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkCapture.Checked)
            {
                _captureIndDelay = 0;
                chkCapture.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerHousekeeping_Tick(object sender, EventArgs e)
        {
            if (_captureIndDelay > 0)
            {
                _captureIndDelay--;
                if (_captureIndDelay <= 0)
                {
                    chkCapture.BackColor = SystemColors.Control;
                    _captureIndDelay = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Make some markdown.
            List<string> mdText = new List<string>();

            // Main help file.
            mdText.Add(File.ReadAllText(@"README.md"));

            // Put it together.
            List<string> htmlText = new List<string>();

            // Boilerplate
            htmlText.Add($"<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            // CSS
            htmlText.Add($"<style>body {{ background-color: {_settings.BackColor.Name}; font-family: \"Arial\", Helvetica, sans-serif; }}");
            htmlText.Add($"</style></head><body>");

            // Meat.
            string mdHtml = string.Join(Environment.NewLine, mdText);
            htmlText.Add(mdHtml);

            // Bottom.
            string ss = "<!-- Markdeep: --><style class=\"fallback\">body{visibility:hidden;white-space:pre;font-family:monospace}</style><script src=\"markdeep.min.js\" charset=\"utf-8\"></script><script src=\"https://casual-effects.com/markdeep/latest/markdeep.min.js\" charset=\"utf-8\"></script><script>window.alreadyProcessedMarkdeep||(document.body.style.visibility=\"visible\")</script>";
            htmlText.Add(ss);
            htmlText.Add($"</body></html>");

            string fn = Path.Combine(Path.GetTempPath(), "nebulator.html");
            File.WriteAllText(fn, string.Join(Environment.NewLine, htmlText));
            Process.Start(fn);
        }

        /// <summary>
        /// Edit the common options in a property grid.
        /// </summary>
        void UserSettings_Click(object sender, EventArgs e)
        {
            using (Form f = new Form()
            {
                Text = "User Settings",
                Size = new Size(350, 400),
                StartPosition = FormStartPosition.Manual,
                Location = new Point(200, 200),
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                ShowIcon = false,
                ShowInTaskbar = false
            })
            {
                PropertyGrid pg = new PropertyGrid()
                {
                    Dock = DockStyle.Fill,
                    PropertySort = PropertySort.NoSort,
                    SelectedObject = _settings
                };

                f.Controls.Add(pg);
                f.ShowDialog();

                _settings.Save();
            }
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

            DrawData(canvas, _settings.Channel1);
            DrawData(canvas, _settings.Channel2);
        }

        /// <summary>
        /// Draw lines.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="ch"></param>
        void DrawData(SKCanvas canvas, Channel ch)
        {
            if (ch.DataPoints != null && ch.DataPoints.Count() >= 2)
            {
                _pen.Color = ch.Color.ToSKColor();
                _pen.StrokeWidth = (float)_settings.StrokeSize;

                SKPath path = new SKPath();
                SKPoint[] points = new SKPoint[ch.DataPoints.Count()];

                var mapped = ch.MapData(_dataRegion,
                    _settings.XPosition,
                    _settings.SampleRate * Common.TimeOptions[_settings.TimePerDivision]);

                for (int i = 0; i < mapped.Count(); i++)
                {
                    points[i] = mapped[i];
                }

                path.AddPoly(points, false);
                canvas.DrawPath(path, _pen);
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
            double xTotal = Common.TimeOptions[_settings.TimePerDivision] * Common.NUM_X_DIVISIONS;
            double xOffset = _settings.XPosition * xTotal;
            double xMin = 0 + xOffset;
            double xMax = xTotal + xOffset;

            canvas.DrawText($"{xMin:0.00}", _dataRegion.Left - 10, bottom, _text);
            canvas.DrawText($"{xMax:0.00}", _dataRegion.Right - 10, bottom, _text);

            ///// Y axis ch1 /////
            double y1Total = Common.VoltOptions[_settings.Channel1.VoltsPerDivision] * Common.NUM_Y_DIVISIONS;
            double y1Offset = _settings.Channel2.Position * y1Total;
            double y1Min = -y1Total / 2 + y1Offset;
            double y1Max = y1Total / 2 + y1Offset;
            double y1Mid = y1Max - y1Total / 2;

            _text.Color = _settings.Channel1.Color.ToSKColor();
            canvas.DrawText($"{y1Min:0.00}", left1, _dataRegion.Bottom - _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y1Max:0.00}", left1, _dataRegion.Top + _text.FontMetrics.XHeight / 2, _text);
            canvas.DrawText($"{y1Mid:0.00}", left1, _dataRegion.Top + _dataRegion.Height / 2, _text);

            ///// Y axis ch2 /////
            double y2Total = Common.VoltOptions[_settings.Channel2.VoltsPerDivision] * Common.NUM_Y_DIVISIONS;
            double y2Offset = _settings.Channel2.Position * y2Total;
            double y2Min = -y2Total / 2 + y2Offset;
            double y2Max = y2Total / 2 + y2Offset;
            double y2Mid = y2Max - y2Total / 2;

            _text.Color = _settings.Channel2.Color.ToSKColor();
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
            if(chkCapture.Checked)
            {
                byte[] bytes = null;

                // Process input.
                IPEndPoint senderIp = new IPEndPoint(0, 0);
                bytes = _udp?.EndReceive(ares, ref senderIp);

                var (channel, cmd, data) = UnpackMsg(bytes);

                UpdateData(channel, cmd, data);

                // Lights.
                _captureIndDelay = 5;
                chkCapture.BackColor = _settings.ControlColor;
            }

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
                AddText("Bad message rcvd.");
            }

            return (channel:channel, cmd:cmd, data:data);
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
        void txtMsgs_MouseDoubleClick(object sender, MouseEventArgs e)
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
        #endregion
    }
}
