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
            // Assume asio-like buffer size of 1024. At 44100 sample rate, 1 buff = 23 msec.

            sf.XTimePerDivision = 0.01;
            sf.SampleRate = 10000;


            //Color.Firebrick, Color.CornflowerBlue, Color.MediumSeaGreen, Color.MediumOrchid,
            //Color.DarkOrange, Color.DarkGoldenrod, Color.DarkSlateGray, Color.Khaki, Color.PaleVioletRed

            // Setup some channels.
            Channel ch1 = sf.GetChannel(0);
            ch1.Name = "Channel 1 - Sin";
            ch1.Color = Color.Firebrick;
            ch1.VoltsPerDivision = 0.4;
            ch1.YPosition = 0;

            Channel ch2 = sf.GetChannel(1);
            ch2.Name = "Channel 2 - Tri";
            ch2.Color = Color.DarkGoldenrod;
            ch2.VoltsPerDivision = 0.6;
            ch2.YPosition = 0;

            // Make some data.
            int buffSize = 1024;
            double[] ch1Data = new double[buffSize];
            double[] ch2Data = new double[buffSize];

            for (int i = 0; i < buffSize; i++)
            {
                ch1Data[i] = Math.Sin(i / 100.0);
                ch2Data[i] = i / 50.0 % 1.0;
            }

            sf.SetData(ch1Data, ch2Data);
        }
    }
}
