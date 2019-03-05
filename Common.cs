using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebScope
{
    #region Enums
    /// <summary></summary>
    public enum TriggerMode
    {
        Auto,     //> Automatic trigger can be implemented to finish acquisition at the mode when no valid trigger exists.
        Normal,   //> Only valid triggered waveform is checked at the mode. The waveform is acquired only when satisfying the trigger condition.
        Single    //> Acquire a waveform when detecting a single trigger, and then stop.
    };

    /// <summary></summary>
    public enum TriggerSlope
    {
        Rising,   //>
        Falling,  //>
        Both      //>
    }

    public enum xxx
    {

    }
    #endregion

    public class Common
    {
        #region Constants
        /// <summary>Fixed for now.</summary>
        public const int NUM_CHANNELS = 2;

        /// <summary>Visual X.</summary>
        public const int NUM_X_DIVISIONS = 12;

        /// <summary>Visual Y.</summary>
        public const int NUM_Y_DIVISIONS = 8;

        /// <summary>Server listening for data on port.</summary>
        public const int UDP_PORT = 9888;

        /// <summary>Harmonious colors.</summary>
        public static Color[] COLORS =
        {
            Color.Firebrick, Color.CornflowerBlue, Color.MediumSeaGreen, Color.MediumOrchid,
            Color.DarkOrange, Color.DarkGoldenrod, Color.DarkSlateGray, Color.Khaki, Color.PaleVioletRed
        };
        #endregion

        /// <summary>
        /// Bounds limit a value.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Constrain(int val, int min, int max)
        {
            val = Math.Max(val, min);
            val = Math.Min(val, max);
            return val;
        }

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
