using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebScope
{
    public class Common
    {
        #region Constants
        /// <summary>Fixed for now.</summary>
        public const int NUM_CHANNELS = 2;

        /// <summary>Visual X.</summary>
        public const int NUM_X_DIVISIONS = 12;

        /// <summary>Visual Y.</summary>
        public const int NUM_Y_DIVISIONS = 8;

        /// <summary>Volts per div options.</summary>
        public static string[] VOLT_OPTIONS = { "0.01", "0.02", "0.05", "0.1", "0.2", "0.5", "1", "2", "5" };

        /// <summary>Time per div options.</summary>
        public static string[] TIMEBASE_OPTIONS = { "0.001", "0.002", "0.005", "0.01", "0.02", "0.05", "0.1", "0.2", "0.5", "1", "2", "5" };
        #endregion
    }
}
