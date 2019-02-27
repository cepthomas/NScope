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
    public class Test
    {
        public void Go1(ScopeForm sf)
        {
            //Color.Firebrick, Color.CornflowerBlue, Color.MediumSeaGreen, Color.MediumOrchid,
            //Color.DarkOrange, Color.DarkGoldenrod, Color.DarkSlateGray, Color.Khaki, Color.PaleVioletRed

            // Setup some channels.
            Channel ch1 = sf.GetChannel(0);
            ch1.Name = "Channel 1 - Sin";
            ch1.Color = Color.Firebrick;
            ch1.VoltsPerDivision = 0.5;
            ch1.YPosition = 0;

            Channel ch2 = sf.GetChannel(0);
            ch2.Name = "Channel 2 - Tri";
            ch2.Color = Color.DarkGoldenrod;
            ch2.VoltsPerDivision = 0.5;
            ch2.YPosition = 0;

            // Make some data.
            int num = 1000;
            double[] ch1Data = new double[num];
            double[] ch2Data = new double[num];

            for (int i = 0; i < num; i++)
            {
                ch1Data[i] = Math.Sin(i / 100.0);
                ch2Data[i] = i / 50.0 % 1.0;
            }

            sf.XTimePerDivision = 0.01;

            sf.SetData(0, ch1Data);
            sf.SetData(1, ch2Data);

            sf.Refresh();
        }
    }
}
