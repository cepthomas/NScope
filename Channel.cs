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
using System.Text.Json;
using System.Text.Json.Serialization;
using Ephemera.NBagOfTricks;


namespace NScope
{
    ///<summary></summary>
    [Serializable]
    public class Channel
    {
        [DisplayName("Name")]
        [Description("The name for this channel.")]
        [Browsable(true)]
        public string Name { get; set; } = "????";

        [DisplayName("Color")]
        [Description("The color to display this channel.")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonColorConverter))]
        public Color Color { get; set; } = Color.White;

        [DisplayName("Position")]
        [Description("Shift along Y axis aka DC offset.")]
        [Browsable(true)]
        public double Position { get; set; } = 0.0;

        [DisplayName("Volts Per Div")]
        [Description("Extent of y axis. Volts is a nod to tradition.")]
        [Browsable(true)]
        public string VoltsPerDivision { get; set; } = "0.5";

        ///<summary>Data points y values in "units" "volts".</summary>
        [Browsable(false)]
        [JsonIgnore]
        public List<double> DataPoints { get; set; } = new List<double>();

        /// <summary>
        /// Redraw using new data.
        /// </summary>
        /// <param name="cmd">0 = append, 1 = overwrite.</param>
        /// <param name="data">The data to display.</param>
        public void UpdateData(int cmd, double[] data)
        {
            if (cmd == 1) // reset
            {
                DataPoints = data.ToList(); // probably needs opimization?
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
            List<SKPoint> mapped = new();

            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/matrix
            //canvas.Translate(tx, ty);
            //canvas.Scale(sx, sy);
            //The Scale transform is multiplied by the Translate transform for the composite transform matrix:
            //| sx   0   0 |   | 1   0   0 |   | sx   0   0 |
            //| 0   sy   0 | × | 0   1   0 | = | 0   sy   0 |
            //| 0    0   1 |   | tx  ty  1 |   | tx  ty   1 |

            double xTotalSamples = xSamplesPerDivision * Common.NUM_X_DIVISIONS;
            double xScale = drawRegion.Width / xTotalSamples;
            double yTotalVolts = Common.VoltOptions[VoltsPerDivision] * Common.NUM_Y_DIVISIONS;
            double yScale = drawRegion.Height / yTotalVolts;
            double xOffset = xPosition * drawRegion.Width;
            double yOffset = Position * drawRegion.Height;

            SKMatrix matrix = SKMatrix.CreateIdentity();
            matrix.ScaleX = (float)xScale;
            matrix.ScaleY = -(float)yScale;
            matrix.TransX = drawRegion.Left + (float)xOffset;
            matrix.TransY = drawRegion.Top +  drawRegion.Height/2 + (float)yOffset;
            matrix.Persp2 = 1;

            // Map the data to UI space.
            for (int i = 0; i < DataPoints.Count; i++)
            {
                mapped.Add(matrix.MapPoint(new SKPoint(i, (float)DataPoints[i])));
            }

            return mapped;
        }
    }
}
