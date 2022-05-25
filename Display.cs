using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;


namespace NebScope
{
    public partial class Display : UserControl
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
        /// <summary>Current pen to draw with. Specific user can adjust to taste.</summary>
        readonly SKPaint _pen = new()
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Stroke,
            IsStroke = true,
            StrokeWidth = 2,
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true
        };

        /// <summary>Current font to draw with.</summary>
        readonly SKPaint _font = new()
        {
            TextSize = 18,
            Color = SKColors.White,
            Typeface = SKTypeface.FromFamilyName("Arial"),
            TextAlign = SKTextAlign.Left,
            IsAntialias = true,
        };

        /// <summary>Rendered bitmap for display when painting.</summary>
        Bitmap? _bitmap = null;

        /// <summary>Just the data area.</summary>
        RectangleF _dataRegion = new();
        #endregion

        #region Lifecycle
        /// <summary>
        /// 
        /// </summary>
        public Display()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            DoubleBuffered = true;
            BackColor = Color.Black;
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            UpdateBitmap();
            base.OnResize(e);
        }

        /// <summary>
        /// Renders the stored bitmap to the UI.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.WriteLine("OnPaint");

            if (_bitmap is not null)
            {
                Debug.WriteLine("DrawImage");
                e.Graphics.DrawImage(_bitmap, new Point(0, 0));
            }
        }
        #endregion

        #region Render functions
        /// <summary>
        /// Generate the bitmap if it's time and enabled.
        /// </summary>
        public void UpdateBitmap()
        {
            // Check for resize or init.
            if (_bitmap is null || _bitmap.Width != Width || _bitmap.Height != Height)
            {
                _bitmap?.Dispose();
                _bitmap = new(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            }
            else
            {
                Graphics.FromImage(_bitmap).Clear(Color.Black);
            }

            // Render the new bitmap.
            var data = _bitmap.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, _bitmap.PixelFormat);

            using (SKSurface surface = SKSurface.Create(new SKImageInfo(Width, Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul), data.Scan0, Width * 4))
            {
                // Calc the drawing region.
                _dataRegion = new Rectangle(
                    BORDER_PAD + Y_AXIS_SPACE,
                    BORDER_PAD,
                    Width - BORDER_PAD - BORDER_PAD - Y_AXIS_SPACE,
                    Height - BORDER_PAD - BORDER_PAD - X_AXIS_SPACE);

                // Draw axes first before clipping.
                DrawAxes(surface.Canvas);
                DrawLabels(surface.Canvas);

                // Now clip to drawing region.
                surface.Canvas.ClipRect(ToSKRect(_dataRegion));

                DrawData(surface.Canvas, Common.Settings.Channel1);
                DrawData(surface.Canvas, Common.Settings.Channel2);
            }

            _bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Draw lines.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="ch"></param>
        void DrawData(SKCanvas canvas, Channel ch)
        {
            if (ch.DataPoints is not null && ch.DataPoints.Count >= 2)
            {
                _pen.Color = ToSKColor(ch.Color);
                _pen.StrokeWidth = (float)Common.Settings.StrokeSize;

                SKPath path = new();
                SKPoint[] points = new SKPoint[ch.DataPoints.Count];

                var mapped = ch.MapData(_dataRegion,
                    Common.Settings.XPosition,
                    Common.Settings.SampleRate * Common.TimeOptions[Common.Settings.TimePerDivision]);

                for (int i = 0; i < mapped.Count; i++)
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
            double xTotal = Common.TimeOptions[Common.Settings.TimePerDivision] * Common.NUM_X_DIVISIONS;
            double xOffset = Common.Settings.XPosition * xTotal;
            double xMin = 0 + xOffset;
            double xMax = xTotal + xOffset;

            canvas.DrawText($"{xMin:0.00}", _dataRegion.Left - 10, bottom, _font);
            canvas.DrawText($"{xMax:0.00}", _dataRegion.Right - 10, bottom, _font);

            ///// Y axis ch1 /////
            double y1Total = Common.VoltOptions[Common.Settings.Channel1.VoltsPerDivision] * Common.NUM_Y_DIVISIONS;
            double y1Offset = Common.Settings.Channel1.Position * y1Total;
            double y1Min = -y1Total / 2 + y1Offset;
            double y1Max = y1Total / 2 + y1Offset;
            double y1Mid = y1Max - y1Total / 2;

            _font.Color = ToSKColor(Common.Settings.Channel1.Color);
            canvas.DrawText($"{y1Min:0.00}", left1, _dataRegion.Bottom - _font.FontMetrics.XHeight / 2, _font);
            canvas.DrawText($"{y1Max:0.00}", left1, _dataRegion.Top + _font.FontMetrics.XHeight / 2, _font);
            canvas.DrawText($"{y1Mid:0.00}", left1, _dataRegion.Top + _dataRegion.Height / 2, _font);

            ///// Y axis ch2 /////
            double y2Total = Common.VoltOptions[Common.Settings.Channel2.VoltsPerDivision] * Common.NUM_Y_DIVISIONS;
            double y2Offset = Common.Settings.Channel2.Position * y2Total;
            double y2Min = -y2Total / 2 + y2Offset;
            double y2Max = y2Total / 2 + y2Offset;
            double y2Mid = y2Max - y2Total / 2;

            _font.Color = ToSKColor(Common.Settings.Channel2.Color);
            canvas.DrawText($"{y2Min:0.00}", left2, _dataRegion.Bottom - _font.FontMetrics.XHeight / 2, _font);
            canvas.DrawText($"{y2Max:0.00}", left2, _dataRegion.Top + _font.FontMetrics.XHeight / 2, _font);
            canvas.DrawText($"{y2Mid:0.00}", left2, _dataRegion.Top + _dataRegion.Height / 2, _font);
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

            // X
            for (int i = 0; i <= Common.NUM_X_DIVISIONS; i++)
            {
                _pen.StrokeWidth = i == 0 ? 2.0f : 0.6f;
                canvas.DrawLine(_dataRegion.Left + i * xinc, _dataRegion.Top, _dataRegion.Left + i * xinc, _dataRegion.Bottom, _pen);
            }

            // Y
            for (int i = 0; i <= Common.NUM_Y_DIVISIONS; i++)
            {
                _pen.StrokeWidth = i == Common.NUM_Y_DIVISIONS / 2 ? 2.0f : 0.6f;
                canvas.DrawLine(_dataRegion.Left, _dataRegion.Top + i * yinc, _dataRegion.Right, _dataRegion.Top + i * yinc, _pen);
            }
        }
        #endregion

        #region Private utilities
        /// <summary>
        /// Converter.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        SKRect ToSKRect(RectangleF rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Converter.
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        SKColor ToSKColor(Color col)
        {
            return new SKColor(col.R, col.G, col.B, col.A);
        }
        #endregion
    }
}
