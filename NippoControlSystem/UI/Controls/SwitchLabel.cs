using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace Cyc.Forms
{
    public partial class SwitchLabel : System.Windows.Forms.Label
    {
        public SwitchLabel()
        {
            //InitializeComponent();
            //BackOnColor = Color.Red;
            //BackOffColor = Color.Lime;
            this._LampValue = -9999999;
        }

        [System.ComponentModel.Description("BackColorオン時のColor")]
        private Color _BackOnColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Empty")]
        public Color BackOnColor
        {
            get { return _BackOnColor; }
            set
            {
                if (_BackOnColor != value)
                {
                    _BackOnColor = value;
                    this_Refresh();
                }
            }
        }

        [System.ComponentModel.Description("BackColorオフ時のColor")]
        private Color _BackOffColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Empty")]
        public Color BackOffColor
        {
            get { return _BackOffColor; }
            set
            {
                if (_BackOffColor != value)
                {
                    _BackOffColor = value;
                    this_Refresh();
                }
            }
        }

        [System.ComponentModel.Description("ForeColorオン時のColor")]
        private Color _ForeOnColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Empty")]
        public Color ForeOnColor
        {
            get { return _ForeOnColor; }
            set
            {
                if (_ForeOnColor != value)
                {
                    _ForeOnColor = value;
                    this_Refresh();
                }
            }
        }

        [System.ComponentModel.Description("ForeColorオフ時のColor")]
        private Color _ForeOffColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Empty")]
        public Color ForeOffColor
        {
            get { return _ForeOffColor; }
            set
            {
                if (_ForeOffColor != value)
                {
                    _ForeOffColor = value;
                    this_Refresh();
                }
            }
        }

        [System.ComponentModel.Description("LampBitDeviceの名称")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LampBitDevice
        {
            get;
            set;
        }

        private int _LampValue;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LampValue
        {
            get
            {
                return _LampValue;
            }

            set
            {
                if (this._LampValue != value)
                {
                    this._LampValue = value;
                    //this.Refresh();
                    this_Refresh();
                }
            }
        }
        public void this_Refresh()
        {
            if (this._LampValue == 0)
            {
                if (this.ForeOffColor != System.Drawing.Color.Empty && this.ForeColor != this.ForeOffColor) this.ForeColor = this.ForeOffColor;
                if (this.BackOffColor != System.Drawing.Color.Empty && this.BackColor != this.BackOffColor) this.BackColor = this.BackOffColor;
            }
            else
            {
                if (this.ForeOnColor != System.Drawing.Color.Empty && this.ForeColor != this.ForeOnColor) this.ForeColor = this.ForeOnColor;
                if (this.BackOnColor != System.Drawing.Color.Empty && this.BackColor != this.BackOnColor) this.BackColor = this.BackOnColor;
            }

        }
    }
}
