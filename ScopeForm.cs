using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;


namespace NebScope
{
     #region Enums
    /// <summary></summary>
    public enum DisplayMode { Continuous, OneShot };

    /// <summary>free for slow signals?</summary>
    public enum TriggerMode { FreeRun, Rising, Falling };
    #endregion

    public partial class ScopeForm : Form
    {
        #region Constants
        /// <summary>Whitespace around edges.</summary>
        const int BORDER_PAD = 20;

        /// <summary>Reserved for axes.</summary>
        const int AXIS_SPACE = 40;
        #endregion

        #region Properties - cosmetics
        ///<summary>Trace thickness.</summary>
        public double StrokeSize { get; set; } = 1;

        #endregion

        #region Properties - triggering
        /// <summary></summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Continuous;

        /// <summary></summary>
        public int TriggerChannel { get; set; } = 0;

        /// <summary></summary>
        public TriggerMode TriggerMode { get; set; } = TriggerMode.Rising;

        /// <summary>Value to start displaying.</summary>
        public double TriggerLevel { get; set; } = 0.0;
        #endregion

        #region Properties - X axis
        /// <summary>Shift along X axis aka time offset. +-1.0 is equivalent to the total X grid.</summary>
        public double XPosition { get; set; } = 0.0;

        /// <summary>Seconds per horizontal division.</summary>
        public double XTimePerDivision { get; set; } = 1.0;

        ///<summary>Sample rate for data.</summary>
        public double SampleRate { get; set; } = 44100;
        #endregion

        #region Fields
        ///<summary>Data to chart.</summary>
        Channel[] _channels = new Channel[Common.NUM_CHANNELS];

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
            Color = SKColors.Black,
            Typeface = SKTypeface.FromFamilyName("Arial"),
            TextAlign = SKTextAlign.Left,
            IsAntialias = true
        };
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

            for (int i = 0; i < Common.NUM_CHANNELS; i++)
            {
                _channels[i] = new Channel();
            }

            // Improve performance and eliminate flicker.
            DoubleBuffered = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScopeForm_Load(object sender, EventArgs e)
        {
            skControl.BackColor = Color.Black;

            // Hook up handlers.
            skControl.Resize += SkControl_Resize;
            skControl.PaintSurface += SkControl_PaintSurface;

            CalcDrawRegion();
        }
        #endregion



        private void btnTest_Click(object sender, EventArgs e)
        {
            Test tt = new Test(); // TODON need a continuous generator in a thread.
            tt.Go1(this);
        }


        #region Public functions
        /// <summary>
        /// Update the data for a channel.
        /// </summary>
        /// <param name="channelNum"></param>
        /// <param name="data"></param>
        public void SetData(int channelNum, double[] data)
        {
            if (channelNum >= Common.NUM_CHANNELS || channelNum < 0)
            {
                throw new Exception("Invalid channel number");
            }

            CalcDrawRegion();

            _channels[channelNum].SetData(data);

            double xIncSize = 1 / SampleRate;

            _channels[channelNum].MapData(_dataRegion, XPosition, SampleRate * XTimePerDivision);

            // Ask for a redraw.
            skControl.Invalidate();
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

            return _channels[channelNum];
        }
        #endregion

        #region Window Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SkControl_Resize(object sender, EventArgs e)
        {
            CalcDrawRegion();

            // Remap the data.
            foreach (Channel ch in _channels)
            {
                ch.MapData(_dataRegion, XPosition, SampleRate * XTimePerDivision);
            }

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
            foreach (Channel ser in _channels)
            {
                if(ser.DataPoints != null && ser.DataPoints.Count() >= 2)
                {
                    _pen.Color = ser.Color.ToSKColor();
                    _pen.StrokeWidth = (float)StrokeSize;

                    SKPath path = new SKPath();
                    SKPoint[] points = new SKPoint[ser.DataPoints.Count()];

                    for (int i = 0; i < ser.DataPoints.Count(); i++)
                    {
                        points[i] = ser.DataPoints[i].ClientPoint;
                    }

                    path.AddPoly(points, false);
                    canvas.DrawPath(path, _pen);
                }
            }
        }

        /// <summary>
        /// Draw axes.
        /// </summary>
        /// <param name="canvas"></param>
        void DrawAxes(SKCanvas canvas)
        {
            _pen.Color = SKColors.LightBlue;
            _pen.StrokeWidth = 0.6f;
            _text.Color = SKColors.Black;

            float xinc = _dataRegion.Width / Common.NUM_X_DIVISIONS;
            float yinc = _dataRegion.Height / Common.NUM_Y_DIVISIONS;

            for (int i = 0; i <= Common.NUM_X_DIVISIONS; i++)
            {
                canvas.DrawLine(_dataRegion.Left + i * xinc, _dataRegion.Top, _dataRegion.Left + i * xinc, _dataRegion.Bottom, _pen);
            }

            for (int i = 0; i <= Common.NUM_Y_DIVISIONS; i++)
            {
                canvas.DrawLine(_dataRegion.Left, _dataRegion.Top + i * yinc, _dataRegion.Right, _dataRegion.Top + i * yinc, _pen);
            }

            //TODON draw axis labels.

            //// Y axis text
            //_text.TextAlign = SKTextAlign.Left;
            //float left = 2;
            //canvas.DrawText($"{YUnits}", left, _dataRegion.Top + _dataRegion.Height / 2 + _text.FontMetrics.XHeight / 2, _text);
            //canvas.DrawText($"{_yMin:0.00}", left, _dataRegion.Bottom + _text.FontMetrics.XHeight / 2, _text);
            //canvas.DrawText($"{_yMax:0.00}", left, _dataRegion.Top + +_text.FontMetrics.XHeight / 2, _text);

            //// X axis text
            //_text.TextAlign = SKTextAlign.Center;
            //canvas.DrawText($"{XUnits}", _dataRegion.Left + _dataRegion.Width / 2, _dataRegion.Bottom + AXIS_SPACE, _text);
            //canvas.DrawText($"{_xMin:0.00}", _dataRegion.Left, _dataRegion.Bottom + AXIS_SPACE, _text);
            //canvas.DrawText($"{_xMax:0.00}", _dataRegion.Right, _dataRegion.Bottom + AXIS_SPACE, _text);
        }
        #endregion

        #region Private functions
        /// <summary>
        /// 
        /// </summary>
        void CalcDrawRegion()
        {
            // Calc the drawing region.
            _dataRegion = new Rectangle(
                skControl.Left + BORDER_PAD + AXIS_SPACE,
                skControl.Top + BORDER_PAD,
                skControl.Width - BORDER_PAD - BORDER_PAD - AXIS_SPACE,
                skControl.Height - BORDER_PAD - BORDER_PAD - AXIS_SPACE);
        }
        #endregion
    }
}
