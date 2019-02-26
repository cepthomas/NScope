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
        /// <summary>Fixed for now.</summary>
        const int NUM_CHANNELS = 2;

        /// <summary>Whitespace around edges.</summary>
        const int BORDER_PAD = 20;

        /// <summary>Reserved for axes.</summary>
        const int AXIS_SPACE = 40;

        const int NUM_X_DIVISIONS = 12;
        const int NUM_Y_DIVISIONS = 8;
        #endregion

        #region Properties

        /// <summary></summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Continuous;

        /// <summary></summary>
        public int TriggerChannel { get; set; } = 0;

        /// <summary></summary>
        public TriggerMode TriggerMode { get; set; } = TriggerMode.Rising;

        // Value to start displaying.
        /// <summary></summary>
        public double TriggerLevel { get; set; } = 0.0;
        

        /// <summary>Shifts along X axis.</summary>
        public double Position { get; set; } = 0.0;

        /// <summary>Seconds per horizontal division.</summary>
        public double TimeRangePerDivision { get; set; } = 100.0;
        // public double Timebase { get; set; } = 1.0;


        ///<summary>Trace thickness.</summary>
        public double StrokeSize { get; set; } = 1;


        // ///<summary></summary>
        // public string XUnits { get; set; } = "X units";

        // ///<summary></summary>
        // public string YUnits { get; set; } = "Y units";
        #endregion


////////////// Put these somewhere
        public void SetData(int channelNum, double[] data)
        {
            if(channelNum >= NUM_CHANNELS || channelNum < 0)
            {
                throw new Exception("Invalid channel number");
            }

            _channels[channelNum].Values = data;
        }

        public Channel GetChannel(int channelNum)
        {
            if(channelNum >= NUM_CHANNELS || channelNum < 0)
            {
                throw new Exception("Invalid channel number");
            }

            return _channels[channelNum];
        }

        public void RefreshXXX()
        {
            // Update the display. TODON2 use INotifyPropertyChanged?

            Invalidate();
        }
////////////////////


        #region Fields
        ///<summary>Data to chart.</summary>
        Channel[] _channels = new Channel[NUM_CHANNELS];
        //List<Channel> _channels = new List<Channel>();

        /// <summary>UI region to draw the data.</summary>
        RectangleF _dataRegion = new RectangleF();

        /// <summary>Qualitative color set from http://colorbrewer2.org.</summary>
        List<Color> _colors = new List<Color>()
        {
            //Color.FromArgb(217, 95, 2), Color.FromArgb(27, 158, 119),
            //Color.FromArgb(117, 112, 179), Color.FromArgb(231, 41, 138),
            //Color.FromArgb(102, 166, 30), Color.FromArgb(230, 171, 2),
            //Color.FromArgb(166, 118, 29), Color.FromArgb(102, 102, 102),
            //Color.FromArgb(228, 26, 28), Color.FromArgb(55, 126, 184),
            //Color.FromArgb(77, 175, 74), Color.FromArgb(152, 78, 163),
            //Color.FromArgb(255, 127, 0), Color.FromArgb(255, 255, 51),
            //Color.FromArgb(166, 86, 40), Color.FromArgb(247, 129, 191),
            Color.Firebrick, Color.CornflowerBlue, Color.MediumSeaGreen, Color.MediumOrchid,
            Color.DarkOrange, Color.DarkGoldenrod, Color.DarkSlateGray, Color.Khaki, Color.PaleVioletRed
        };
        #endregion

        #region Drawing tools
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

            for (int i = 0; i < NUM_CHANNELS; i++)
            {
                _channels[i] = new Channel();
            }


     //       int iii = _channels.Count();

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
            // Hook up handlers.
            skControl.Resize += SkControl_Resize;
            skControl.PaintSurface += SkControl_PaintSurface;

            // Assumes user has populated series.
 //           InitData();
        }
        #endregion

        #region Window Event Handlers
        private void SkControl_Resize(object sender, EventArgs e)
        {
            CalcGeometry();
            Repaint();
        }
        #endregion

        #region Render functions
        /// <summary>
        /// Draw the main display area.
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
            double xMin = 0;
            double xMax = 99;// Timebase;
            double yMin = 0; //TODON calc these from ranges/offsets
            double yMax = 1;

            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/matrix
            SKMatrix matrix = new SKMatrix();
            matrix.ScaleX = (float)((_dataRegion.Right - _dataRegion.Left) / (xMax - xMin));
            matrix.ScaleY = (float)((_dataRegion.Top - _dataRegion.Bottom) / (yMax - yMin));
            matrix.TransX = _dataRegion.Left;
            matrix.TransY = _dataRegion.Bottom;
            matrix.Persp2 = 1;


            foreach (Channel ser in _channels)
            {
                _pen.Color = ser.Color.ToSKColor();
                _pen.StrokeWidth = (float)StrokeSize;

                SKPath path = new SKPath();
                SKPoint[] points = new SKPoint[ser.Values.Count()];

                // Map the data to UI space.
                for (int i = 0; i < ser.Values.Count(); i++)
                {
                    points[i] = matrix.MapPoint(new SKPoint((float)(ser.Values[i] - xMin), (float)(ser.Values[i] - yMin)));
                }

                path.AddPoly(points, false);
                canvas.DrawPath(path, _pen);
            }
        }


        /// <summary>
        /// Draw axes.
        /// </summary>
        /// <param name="canvas"></param>
        void DrawAxes(SKCanvas canvas)
        {
            _pen.Color = SKColors.Black;
            _pen.StrokeWidth = 0.6f;
            _text.Color = SKColors.Black;

            // Draw area.
            float tick = 8;
            canvas.DrawLine(_dataRegion.Left - tick, _dataRegion.Top, _dataRegion.Right, _dataRegion.Top, _pen);
            canvas.DrawLine(_dataRegion.Left - tick, _dataRegion.Bottom, _dataRegion.Right, _dataRegion.Bottom, _pen);
            canvas.DrawLine(_dataRegion.Left, _dataRegion.Top, _dataRegion.Left, _dataRegion.Bottom + tick, _pen);
            canvas.DrawLine(_dataRegion.Right, _dataRegion.Top, _dataRegion.Right, _dataRegion.Bottom + tick, _pen);

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
        /// Calculate geometry.
        /// </summary>
        void CalcGeometry()
        {
            // Do some geometry
            _dataRegion = new Rectangle(
                skControl.Left + BORDER_PAD + AXIS_SPACE,
                skControl.Top + BORDER_PAD,
                skControl.Width - BORDER_PAD - BORDER_PAD - AXIS_SPACE,
                skControl.Height - BORDER_PAD - BORDER_PAD - AXIS_SPACE);
        }

        /// <summary>
        /// Figure out min/max etc. Do some data fixups maybe.
        /// </summary>
        void InitData()
        {
            int colorIndex = 0;

            foreach (Channel ser in _channels)
            {
                // Spec the color if not supplied.
                if (ser.Color == Color.Empty)
                {
                    ser.Color = _colors[colorIndex++ % _colors.Count];
                }

                //// Find mins and maxes.
                //foreach (double pt in ser.YValues)
                //{
                //    _xMax = Math.Max(pt.X, _xMax);
                //    _xMin = Math.Min(pt.X, _xMin);
                //    _yMax = Math.Max(pt.Y, _yMax);
                //    _yMin = Math.Min(pt.Y, _yMin);
                //}
            }

            //_xMax = Math.Ceiling(_xMax);
            //_xMin = Math.Floor(_xMin);
            //_yMax = Math.Ceiling(_yMax);
            //_yMin = Math.Floor(_yMin);

            CalcGeometry();
        }

        /// <summary>
        /// Common updater.
        /// </summary>
        void Repaint()
        {
            Invalidate();
            Refresh();
        }

        /// <summary>
        /// Bounds limits a value.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        double Constrain(double val, double min, double max)
        {
            val = Math.Max(val, min);
            val = Math.Min(val, max);
            return val;
        }
        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Setup some channels.
            Channel ch1 = GetChannel(0);
            ch1.Name = "Channel 1 - Sin";
            //others...

            Channel ch2 = GetChannel(0);
            ch2.Name = "Channel 2 - Tri";
            //others...

            // Make some data.
            int num = 1000;
            double[] ch1Data = new double[num];
            double[] ch2Data = new double[num];

            for (int i = 0; i < num; i++)
            {
                ch1Data[i] = Math.Sin(i / 100.0);
                ch2Data[i] = i / 50.0 % 1.0;
            }

            SetData(0, ch1Data);
            SetData(1, ch2Data);
        }
    }

    ///<summary></summary>
    public class Channel
    {
        ///<summary></summary>
        public string Name { get; set; } = "No Name";

        ///<summary></summary>
        public Color Color { get; set; } = Color.Empty;

        ///<summary>Data points y values in "units" "volts".</summary>
        public double[] Values { get; set; } = null;

        ///<summary>Time between x values in usec. Fixed sample rate usually.</summary>
        public double Timebase { get; set; } = 1.0;

        // DC offset. Shifts along Y axis.
        public double Position { get; set; } = 0.0;

        // Extent of y axis. Traditional "volts" per division
        public double ValueRangePerDivision { get; set; } = 100.0;

        public Channel()
        {

        }
    }
}
