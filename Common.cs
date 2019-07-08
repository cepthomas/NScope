using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    #endregion
    
    public class Common
    {
        #region Constants
        /// <summary>Visual X.</summary>
        public const int NUM_X_DIVISIONS = 12;

        /// <summary>Visual Y.</summary>
        public const int NUM_Y_DIVISIONS = 8;

        /// <summary>Volts per div options.</summary>
        public static readonly Dictionary<string, double> VoltOptions = new Dictionary<string, double>()
        {
            {"0.01", 0.01 },
            {"0.02", 0.02 },
            {"0.05", 0.05 },
            {"0.1",   0.1 },
            {"0.2",   0.2 },
            {"0.5",   0.5 },
            {"1",       1 },
            {"2",       2 },
            {"5",       5 },
        };

        /// <summary>Time per div options.</summary>
        public static readonly Dictionary<string, double> TimeOptions = new Dictionary<string, double>()
        {
            {"0.001", 0.001 },
            {"0.002", 0.002 },
            {"0.005", 0.005 },
            {"0.01",   0.01 },
            {"0.02",   0.02 },
            {"0.05",   0.05 },
            {"0.1",     0.1 },
            {"0.2",     0.2 },
            {"0.5",     0.5 },
            {"1",         1 },
            {"2",         2 },
            {"5",         5 },
        };
        #endregion
    }
}
