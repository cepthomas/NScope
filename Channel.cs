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
    public class DataPoint
    {
        /// <summary>Let's call the Y value volts for sentimentality.</summary>
        public double Volts { get; set; } = 0;

        /// <summary>Where currently in the UI.</summary>
        public SKPoint ClientPoint { get; set; }
    
        //public override string ToString()
        //{
        //    return $"X:{X:0.00}  Y:{Y:0.00}{Environment.NewLine}Series:{Owner.Name}";
        //}
    }


    ///<summary></summary>
    public class Channel
    {
        ///<summary></summary>
        public string Name { get; set; } = "No Name";

        ///<summary></summary>
        public Color Color { get; set; } = Color.White;

        ///<summary>Data points y values in "units" "volts".</summary>
        public DataPoint[] DataPoints { get; set; } = null;

        /// <summary>Shift along Y axis aka DC offset. +-1.0 is equivalent to the total Y grid.</summary>
        public double YPosition { get; set; } = 0.0;

        /// <summary>Extent of y axis. Traditional "volts" per division</summary>
        public double VoltsPerDivision { get; set; } = 1.0;

        /// <summary>
        /// 
        /// </summary>
        public Channel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(double[] data)
        {
            DataPoints = new DataPoint[data.Count()];

            for (int i = 0; i < data.Count(); i++)
            {
                DataPoints[i] = new DataPoint
                {
                    Volts = data[i]
                };
            }
        }

        /// <summary>
        /// Map from volts/time to client draw points.
        /// </summary>
        /// <param name="drawRegion">Target render area.</param>
        /// <param name="xPosition">Offset for X axis.</param>
        /// <param name="xSamplesPerDivision">Number of data points in X grid.</param>
        public void MapData(RectangleF drawRegion, double xPosition, double xSamplesPerDivision)
        {
            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/matrix
            //canvas.Translate(tx, ty);
            //canvas.Scale(sx, sy);
            //The Scale transform is multiplied by the Translate transform for the composite transform matrix:
            //| sx   0   0 |   | 1   0   0 |   | sx   0   0 |
            //| 0   sy   0 | � | 0   1   0 | = | 0   sy   0 |
            //| 0    0   1 |   | tx  ty  1 |   | tx  ty   1 |


            double xTotalSamples = xSamplesPerDivision * Common.NUM_X_DIVISIONS;
            double xScale = xTotalSamples / drawRegion.Width;
            double yTotalVolts = VoltsPerDivision * Common.NUM_Y_DIVISIONS;
            double yScale = yTotalVolts / drawRegion.Height;

            double xOffset = xPosition / 100 * xTotalSamples;
            double yOffset = YPosition / 100 * yScale;

            SKMatrix matrix = new SKMatrix
            {
                ScaleX = (float)xScale,
                ScaleY = (float)yScale,
                TransX = drawRegion.Left + (float)xOffset,
                TransY = drawRegion.Bottom - (float)yOffset,
                Persp2 = 1
            };

            // Map the data to UI space.
            for (int i = 0; i < DataPoints.Count(); i++)
            {
                DataPoint dp = DataPoints[i];
                dp.ClientPoint = matrix.MapPoint(new SKPoint(i, (float)dp.Volts));
            }
        }
    }
}
