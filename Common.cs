using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ephemera.NScope
{
    #region Types
    /// <summary></summary>
    public enum TriggerMode
    {
        /// <summary>Automatic trigger can be implemented to finish acquisition at the mode when no valid trigger exists.</summary>
        Auto,
        /// <summary>Only valid triggered waveform is checked at the mode. The waveform is acquired only when satisfying the trigger condition.</summary>
        Normal,
        /// <summary>Acquire a waveform when detecting a single trigger, and then stop.</summary>
        Single
    };

    /// <summary></summary>
    public enum TriggerSlope
    {
        Rising,
        Falling,
        Both
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
        public static readonly Dictionary<string, double> VoltOptions = new()
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
        public static readonly Dictionary<string, double> TimeOptions = new()
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

        /// <summary>Current user settings.</summary>
        public static UserSettings Settings { get; set; } = new();

    }
}
