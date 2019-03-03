using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NebScope
{
    /// <summary>
    /// Control potentiometer.
    /// </summary>
    public partial class RotarySel : UserControl
    {
        #region Fields
        string _value = "";
        int _beginDragY = 0;
        double _beginDragValue = 0.0;
        bool _dragging = false;
        #endregion

        #region Properties
        /// <summary>
        /// For styling.
        /// </summary>
        public Color ControlColor { get; set; } = Color.Black;

        /// <summary>
        /// Name etc.
        /// </summary>
        public string Label { get; set; } = "???";

        /// <summary>
        /// The current value of the pot.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { SetValue(value, false); }
        }

        /// <summary>
        /// The possible click values.
        /// </summary>
        public List<string> DiscreteOptions { get; set; } = new List<string>();
        #endregion

        #region Events
        /// <summary>
        /// Value changed event.
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Functions
        /// <summary>
        /// Creates a new control.
        /// </summary>
        public RotarySel()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            InitializeComponent();
        }

        /// <summary>
        /// Set the new value.
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="raiseEvents"></param>
        void SetValue(string newValue, bool raiseEvents)
        {
            _value = Common.Constrain(newValue, _minimum, _maximum);

            if (raiseEvents)
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
            Invalidate();
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            int diameter = Math.Min(Width - 4, Height - 4);

            Pen potPen = new Pen(ControlColor, 3.0f)
            {
                LineJoin = System.Drawing.Drawing2D.LineJoin.Round
            };

            System.Drawing.Drawing2D.GraphicsState state = e.Graphics.Save();

            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawArc(potPen, new Rectangle(diameter / -2, diameter / -2, diameter, diameter), 135, 270);

            double percent = (_value - _minimum) / (_maximum - _minimum);
            double degrees = 135 + (percent * 270);
            double x = (diameter / 2.0) * Math.Cos(Math.PI * degrees / 180);
            double y = (diameter / 2.0) * Math.Sin(Math.PI * degrees / 180);
            e.Graphics.DrawLine(potPen, 0, 0, (float)x, (float)y);

            StringFormat format = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            Rectangle srect = new Rectangle(0, 7, 0, 0);
            string sValue = _value.ToString("#." + new string('0', DecPlaces));
            e.Graphics.DrawString(sValue, Font, Brushes.Black, srect, format);

            srect = new Rectangle(0, 20, 0, 0);
            e.Graphics.DrawString(Label, Font, Brushes.Black, srect, format);

            e.Graphics.Restore(state);
            base.OnPaint(e);
        }

        /// <summary>
        /// Handles the mouse down event to allow changing value by dragging.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            _beginDragY = e.Y;
            _beginDragValue = _value;
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handles the mouse up event to allow changing value by dragging.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Handles the mouse down event to allow changing value by dragging.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                int yDifference = _beginDragY - e.Y;
                double delta = (_maximum - _minimum) * (yDifference / 100.0);
                double newValue = Common.Constrain(_beginDragValue + delta, _minimum, _maximum);
                SetValue(newValue, true);
            }
            base.OnMouseMove(e);
        }
        #endregion
    }
}
