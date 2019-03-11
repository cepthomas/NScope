using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using Newtonsoft.Json;


namespace NebScope
{
    [Serializable]
    public class UserSettings
    {
        // const string FILENAME = "settings.json";

        #region Persisted editable properties
        [DisplayName("Control Color"), Description("The color used for styling control surfaces."), Browsable(true)]
        public Color ControlColor { get; set; } = Color.Blue;

        [DisplayName("Background Color"), Description("The color used for overall background."), Browsable(true)]
        public Color BackColor { get; set; } = Color.AliceBlue;

        [DisplayName("Channels"), Description("The channels."), Browsable(true)]
        public List<Channel> Channels { get; set; } = new List<Channel>();

        [DisplayName("Stroke Size"), Description("Trace thickness."), Browsable(true)]
        public double StrokeSize { get; set; } = 2;
        #endregion

        #region Properties - X axis
        /// <summary>Shift along X axis aka time offset. +-1.0 is equivalent to the total X grid.</summary>
        public double XPosition { get; set; } = 0.0;

        /// <summary>Seconds per horizontal division.</summary>
        public double TimePerDivision { get; set; } = 0.1;

        ///<summary>Sample rate for data.</summary>
        public double SampleRate { get; set; } = 48000;
        // The Audio Engineering Society recommends 48 kHz sampling rate for most applications but gives recognition
        // to 44.1 kHz for Compact Disc (CD) and other consumer uses, 32 kHz for transmission-related applications,
        // and 96 kHz for higher bandwidth or relaxed anti-aliasing filtering.[9] Both Lavry Engineering and
        // J. Robert Stuart state that the ideal sampling rate would be about 60 kHz, but since this is not
        // a standard frequency, recommend 88.2 or 96 kHz for recording purposes.
        // interleaved buffer size of 1024. At 44100 sample/sec rate, 1 buff = 23 msec.
        // 44100 sample rate == 22.675 usec/sample.
        // A 1000 Hz waveform == 1000 usec/cycle == 44.1 samples.
        #endregion

        #region Persisted non-editable properties
        [Browsable(false)]
        public int FormX { get; set; } = 50;

        [Browsable(false)]
        public int FormY { get; set; } = 50;

        [Browsable(false)]
        public int FormWidth { get; set; } = 1000;

        [Browsable(false)]
        public int FormHeight { get; set; } = 700;
        #endregion

        #region Fields
        /// <summary>The file name.</summary>
        string _fn = "???";
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
            File.WriteAllText(_fn, json);
        }

        /// <summary>Create object from file.</summary>
        public static UserSettings Load(string appDir)
        {
            string fn = Path.Combine(appDir, "settings.json");
            UserSettings settings = null;

            if(File.Exists(fn))
            {
                string json = File.ReadAllText(fn);
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
                        Position = 0
                    });
                }
            }

            settings._fn = fn;

            return settings;
        }
        #endregion
    }
}
