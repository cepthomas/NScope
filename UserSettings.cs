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
        [DisplayName("Control Font"), Description("The font to use for controls."), Browsable(true)]
        public Font ControlFont { get; set; } = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

        [DisplayName("Control Color"), Description("The color used for styling control surfaces."), Browsable(true)]
        public Color ControlColor { get; set; } = Color.Yellow;

        [DisplayName("Background Color"), Description("The color used for overall background."), Browsable(true)]
        public Color BackColor { get; set; } = Color.AliceBlue;
        #endregion

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
            }

            return settings;
        }
        #endregion
    }
}
