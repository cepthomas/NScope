using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebScope
{
    public class Common
    {
        //Color.Firebrick, Color.CornflowerBlue, Color.MediumSeaGreen, Color.MediumOrchid,
        //Color.DarkOrange, Color.DarkGoldenrod, Color.DarkSlateGray, Color.Khaki, Color.PaleVioletRed

        #region Constants
        /// <summary>Fixed for now.</summary>
        public const int NUM_CHANNELS = 2;

        /// <summary>Visual X.</summary>
        public const int NUM_X_DIVISIONS = 12;

        /// <summary>Visual Y.</summary>
        public const int NUM_Y_DIVISIONS = 8;

        /// <summary>Listening for data.</summary>
        public const int UDP_PORT = 9888;
        #endregion

        /// <summary>
        /// Bounds limit a value.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double Constrain(double val, double min, double max)
        {
            val = Math.Max(val, min);
            val = Math.Min(val, max);
            return val;
        }
    }
}
