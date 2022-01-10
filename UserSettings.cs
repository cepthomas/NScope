using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using NBagOfTricks;
using NBagOfUis;


namespace NebScope
{
    [Serializable]
    public class UserSettings
    {
        #region Persisted editable properties
        [DisplayName("Control Color")]
        [Description("The color used for styling control surfaces.")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonColorConverter))]
        public Color ControlColor { get; set; } = Color.Blue;

        [DisplayName("Background Color")]
        [Description("The color used for overall background.")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonColorConverter))]
        public Color BackColor { get; set; } = Color.AliceBlue;

        [DisplayName("Channel 1")]
        [Description("The channel settings.")]
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Channel Channel1 { get; set; } = new Channel();

        [DisplayName("Channel 2")]
        [Description("The channel settings.")]
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Channel Channel2 { get; set; } = new Channel();

        [DisplayName("Stroke Size")]
        [Description("Trace thickness.")]
        [Browsable(true)]
        public double StrokeSize { get; set; } = 2;

        [DisplayName("UDP Port")]
        [Description("Port to listen on.")]
        [Browsable(true)]
        public int Port { get; set; } = 9888;
        #endregion

        #region Persisted non-editable properties
        /// <summary>Shift along X axis aka time offset. +-1.0 is equivalent to the total X grid.</summary>
        [Browsable(false)]
        public double XPosition { get; set; } = 0.0;

        /// <summary>Seconds per horizontal division.</summary>
        [Browsable(false)]
        public string TimePerDivision { get; set; } = "0.1";

        ///<summary>Sample rate for data.</summary>
        /// The Audio Engineering Society recommends 48 kHz sampling rate for most applications but gives recognition
        /// to 44.1 kHz for Compact Disc (CD) and other consumer uses, 32 kHz for transmission-related applications,
        /// and 96 kHz for higher bandwidth or relaxed anti-aliasing filtering.[9] Both Lavry Engineering and
        /// J. Robert Stuart state that the ideal sampling rate would be about 60 kHz, but since this is not
        /// a standard frequency, recommend 88.2 or 96 kHz for recording purposes.
        /// interleaved buffer size of 1024. At 44100 sample/sec rate, 1 buff = 23 msec.
        /// 44100 sample rate == 22.675 usec/sample.
        /// A 1000 Hz waveform == 1000 usec/cycle == 44.1 samples.
        [Browsable(false)]
        public double SampleRate { get; set; } = 48000;

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
        string _fn = "";
        #endregion

        #region Persistence
        /// <summary>Save object to file.</summary>
        public void Save()
        {
            if(_fn != "")
            {
                JsonSerializerOptions opts = new() { WriteIndented = true };
                string json = JsonSerializer.Serialize(this, opts);
                File.WriteAllText(_fn, json);
            }
        }

        /// <summary>Create object from file.</summary>
        public static UserSettings Load(string appDir)
        {
            string fn = Path.Combine(appDir, "settings.json");
            UserSettings? settings = null;

            if (File.Exists(fn))
            {
                string json = File.ReadAllText(fn);
                settings = JsonSerializer.Deserialize<UserSettings>(json);
            }

            if (settings is null)
            {
                // Doesn't exist, create a new one.
                settings = new();

                // Setup some default channels.
                settings.Channel1 = new()
                {
                    Name = $"Channel 1",
                    Color = GraphicsUtils.GetSequenceColor(0),
                    VoltsPerDivision = "0.5",
                    Position = 0
                };

                settings.Channel2 = new()
                {
                    Name = $"Channel 2",
                    Color = GraphicsUtils.GetSequenceColor(1),
                    VoltsPerDivision = "0.5",
                    Position = 0
                };
            }

            settings._fn = fn;

            return settings;
        }
        #endregion
    }
}
