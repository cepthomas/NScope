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
using Newtonsoft.Json;


namespace NebScope
{
    ///<summary></summary>
    [Serializable]
    public class Channel
    {
        ///<summary></summary>
        public string Name { get; set; } = "????";

        ///<summary></summary>
        public Color Color { get; set; } = Color.White;

        ///<summary>Data points y values in "units" "volts".</summary>
        [Browsable(false)]
        [JsonIgnore]
        public List<double> DataPoints { get; set; } = new List<double>();

        /// <summary>Shift along Y axis aka DC offset.</summary>
        public double Position { get; set; } = 0.0;

        /// <summary>Extent of y axis. Traditional "volts" per division.</summary>
        public double VoltsPerDivision { get; set; } = 0.5;

        /// <summary>
        /// 
        /// </summary>
        public Channel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        public void UpdateData(int cmd, double[] data)
        {
            if (cmd == 1) // reset
            {
                DataPoints = data.ToList();
            }
            else // append
            {
                DataPoints.AddRange(data);
            }
        }

        /// <summary>
        /// Map from volts/time to client draw points.
        /// </summary>
        /// <param name="drawRegion">Target render area.</param>
        /// <param name="xPosition">Offset for X axis.</param>
        /// <param name="xSamplesPerDivision">Number of data points in X grid.</param>
        public List<SKPoint> MapData(RectangleF drawRegion, double xPosition, double xSamplesPerDivision)
        {
            List<SKPoint> mapped = new List<SKPoint>();

            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/matrix
            //canvas.Translate(tx, ty);
            //canvas.Scale(sx, sy);
            //The Scale transform is multiplied by the Translate transform for the composite transform matrix:
            //| sx   0   0 |   | 1   0   0 |   | sx   0   0 |
            //| 0   sy   0 | × | 0   1   0 | = | 0   sy   0 |
            //| 0    0   1 |   | tx  ty  1 |   | tx  ty   1 |

            double xTotalSamples = xSamplesPerDivision * Common.NUM_X_DIVISIONS;
            double xScale = drawRegion.Width / xTotalSamples;
            double yTotalVolts = VoltsPerDivision * Common.NUM_Y_DIVISIONS;
            double yScale = drawRegion.Height / yTotalVolts;
            double xOffset = xPosition * drawRegion.Width;
            double yOffset = Position * drawRegion.Height;

            SKMatrix matrix = SKMatrix.MakeIdentity();
            matrix.ScaleX = (float)xScale;
            matrix.ScaleY = -(float)yScale;
            matrix.TransX = drawRegion.Left + (float)xOffset;
            matrix.TransY = drawRegion.Top +  drawRegion.Height/2 + (float)yOffset;
            matrix.Persp2 = 1;

            // Map the data to UI space.
            for (int i = 0; i < DataPoints.Count(); i++)
            {
                mapped.Add(matrix.MapPoint(new SKPoint(i, (float)DataPoints[i])));
            }

            return mapped;
        }
    }
}
