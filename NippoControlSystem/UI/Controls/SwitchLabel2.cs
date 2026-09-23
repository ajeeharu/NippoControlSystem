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

namespace NippoControlSystem.UI.Controls
{
    public partial class SwitchLabelCeracon : Label
    {
        public SwitchLabelCeracon()
        {
            //InitializeComponent();
            this.BackColor0 = Color.White;
            this.BackColor1 = Color.Lime;
            this.BackColor2 = Color.Red;
            this.BackColor3 = Color.Red;
            this.BackColor4 = Color.Magenta;
            this.BackColor5 = Color.Firebrick;
            this.BackColor6 = Color.Orange;
            this.BackColor7 = Color.Goldenrod;
             this._LampValue = -9999999;
        }

        [System.ComponentModel.Description("BackColor　０のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor0
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor １のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor1
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ２のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor2
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ３のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor3
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ４のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor4
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ５のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor5
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ６のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor6
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColor ７のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor7
        {
            get;
            set;
        }

        [System.ComponentModel.Description("BackColorオフ時のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackOffColor
        {
            get;
            set;
        }

        [System.ComponentModel.Description("ForeColorオン時のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ForeOnColor
        {
            get;
            set;
        }

        [System.ComponentModel.Description("ForeColorオフ時のColor")]
        [TypeConverter(typeof(System.Drawing.ColorConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ForeOffColor
        {
            get;
            set;
        }
        [System.ComponentModel.Description("LampBitDeviceの名称")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LampBitDevice
        {
            get;
            set;
        }

        [System.ComponentModel.Description("LampBitDeviceの名称")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NoDeviceDisp
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
                    if (this._LampValue == 0)  //ワークなし Value == 0 
                    {
                        if (this.BackColor0 != System.Drawing.Color.Empty && this.BackColor != this.BackColor0) this.BackColor = this.BackColor0;
                    }
                    else if (this._LampValue == 1)  //ワークなし Value == 1 
                    {
                        if (this.BackColor1 != System.Drawing.Color.Empty && this.BackColor != this.BackColor1) this.BackColor = this.BackColor1;
                    }
                    else if (this._LampValue == 2)  //ワークなし Value == 2 
                    {
                        if (this.BackColor2 != System.Drawing.Color.Empty && this.BackColor != this.BackColor2) this.BackColor = this.BackColor2;
                    }
                    else if (this._LampValue == 3)  //ワークなし Value == 3 
                    {
                        if (this.BackColor3 != System.Drawing.Color.Empty && this.BackColor != this.BackColor3) this.BackColor = this.BackColor3;
                    }
                    else if (this._LampValue == 4)  //ワークなし Value == 4 
                    {
                        if (this.BackColor4 != System.Drawing.Color.Empty && this.BackColor != this.BackColor4) this.BackColor = this.BackColor4;
                    }
                    else if (this._LampValue == 5)  //ワークなし Value == 5 
                    {
                        if (this.BackColor5 != System.Drawing.Color.Empty && this.BackColor != this.BackColor5) this.BackColor = this.BackColor5;
                    }
                    else if (this._LampValue == 6)  //ワークなし Value == 6 
                    {
                        if (this.BackColor6 != System.Drawing.Color.Empty && this.BackColor != this.BackColor6) this.BackColor = this.BackColor6;
                    }
                    else if (this._LampValue == 7)  //ワークなし Value == 7 
                    {
                        if (this.BackColor7 != System.Drawing.Color.Empty && this.BackColor != this.BackColor7) this.BackColor = this.BackColor7;
                    }
                    else // その他のとき
                    {
                        if (this.BackOffColor != System.Drawing.Color.Empty && this.BackColor != this.BackOffColor) this.BackColor = this.BackOffColor;
                    }

                    //ForeColor
                    if (this._LampValue == 0)
                    {
                        if (this.ForeOffColor != System.Drawing.Color.Empty && this.ForeColor != this.ForeOffColor) this.ForeColor = this.ForeOffColor;
                    }
                    else
                    {
                        if (this.ForeOnColor != System.Drawing.Color.Empty && this.ForeColor != this.ForeOnColor) this.ForeColor = this.ForeOnColor;
                    }
                    this.Refresh();
                }
            }
        }
    }
}
