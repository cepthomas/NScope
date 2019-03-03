using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using Newtonsoft.Json; // version="9.0.1"


namespace NebScope
{
    [Serializable]
    public class UserSettings
    {
        const string FILENAME = "settings.json";

        #region Persisted editable properties
        [DisplayName("Control Color"), Description("The color used for styling control surfaces."), Browsable(true)]
        public Color ControlColor { get; set; } = Color.Yellow;

        [DisplayName("Background Color"), Description("The color used for overall background."), Browsable(true)]
        public Color BackColor { get; set; } = Color.AliceBlue;
        #endregion

        #region Properties - cosmetics
        ///<summary>Trace thickness.</summary>
        public double StrokeSize { get; set; } = 2;
        #endregion

        #region Properties - X axis
        /// <summary>Shift along X axis aka time offset. +-1.0 is equivalent to the total X grid.</summary>
        public double XPosition { get; set; } = 0.0;

        /// <summary>Seconds per horizontal division.</summary>
        public double XTimePerDivision { get; set; } = 0.001;

        ///<summary>Sample rate for data.</summary>
        public double SampleRate { get; set; } = 48000;
        #endregion

        #region Properties - triggering
        /// <summary></summary>
        public int TriggerChannel { get; set; } = 0;

        /// <summary></summary>
        public TriggerMode TriggerMode { get; set; } = TriggerMode.Normal;

        /// <summary></summary>
        public TriggerSlope TriggerSlope { get; set; } = TriggerSlope.Both;

        /// <summary>Value to start displaying.</summary>
        public double TriggerLevel { get; set; } = 0.0;
        #endregion


        public List<Channel> Channels { get; set; } = new List<Channel>();

        #region Persisted non-editable properties
        [Browsable(false)]
        public int X { get; set; } = 50;

        [Browsable(false)]
        public int Y { get; set; } = 50;

        [Browsable(false)]
        public int Width { get; set; } = 1000;

        [Browsable(false)]
        public int Height { get; set; } = 700;


        #endregion

        /// <summary>Default constructor.</summary>
        public UserSettings()
        {
        }

        #region Persistence
        /// <summary>Save object to file.</summary>
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FILENAME, json);
        }

        /// <summary>Create object from file.</summary>
        public static UserSettings Load()
        {
            UserSettings settings = null;

            if(File.Exists(FILENAME))
            {
                string json = File.ReadAllText(FILENAME);
                settings = JsonConvert.DeserializeObject<UserSettings>(json);
            }
            else
            {
                // Doesn't exist, create a new one.
                settings = new UserSettings();

                // Setup some default channels.
                int icolor = DateTime.Now.Second;
                for (int i = 0; i < Common.NUM_CHANNELS; i++)
                {
                    settings.Channels.Add(new Channel()
                    {
                        Name = $"Channel {i + 1}",
                        Color = Common.COLORS[icolor++ % Common.COLORS.Length],
                        VoltsPerDivision = 0.5,
                        YPosition = 0
                    });
                }
            }

            return settings;
        }
        #endregion
    }
}
