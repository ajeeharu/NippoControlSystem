using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Controls
{
    public class NumericTextBox : TextBox
    {
        ErrorProvider er = new ErrorProvider();

        bool m_BypasOnTextChange = false;

        private double _Value;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    //this.Text = NumberFormated(_Value);
                    //this.Refresh();
                    this.m_BypasOnTextChange = true;
                    NumberFormatedDisp(_Value);
                    this.m_BypasOnTextChange = false;
                }
            }
        }
        delegate void NumberFormatedDispDelegate(double Value);
        private void NumberFormatedDisp(double Value)
        {
            if (InvokeRequired)
            {
                // 別スレッドから呼び出された場合
                Invoke(new NumberFormatedDispDelegate(NumberFormatedDisp), new object[] { Value });
                return;
            }
            // Enable all controls on the form
            this.Text = NumberFormated(Value);
            this.Refresh();
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NumericFormat
        {
            get;
            set;
        }
        double _LimitUpper = double.MaxValue;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double LimitUpper
        {
            get { return _LimitUpper; }
            set { _LimitUpper = value; }
        }
        double _LimitLower = double.MinValue;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double LimitLower
        {
            get { return _LimitLower; }
            set { _LimitLower = value; }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ValueChanged
        {
            get;
            set;
        }
        private string NumberFormated(double value)
        {
            if (this.NumericFormat == null || this.NumericFormat == "")
            {
                return _Value.ToString();
            }
            return _Value.ToString(this.NumericFormat);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (this.TextLength == 0)
            {
                this._Value = 0d;
                return;
            }

            double result;
            bool returnOK = Double.TryParse(this.Text, out result);

            if (returnOK)
            {
                if (this.LimitUpper > this.LimitLower)
                {
                    if (result > this.LimitUpper)
                    {
                        er.SetError(this, this.LimitLower.ToString() + " ～ " + this.LimitUpper.ToString() + "の範囲で入力して下さい");
                        e.Cancel = true;

                    }
                    else if (result < this.LimitLower)
                    {
                        er.SetError(this, this.LimitLower.ToString() + " ～ " + this.LimitUpper.ToString() + "の範囲で入力して下さい");
                        e.Cancel = true;
                    }
                    else
                    {
                        er.Clear();
                    }
                }
                else
                {
                    er.Clear();
                }
            }
            else
            {
                er.SetError(this, "正しい数値を入力して下さい");
                e.Cancel = true;
            }
            base.OnValidating(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            if (!m_BypasOnTextChange)
            {
                if (this.TextLength == 0)
                {
                    this._Value = 0d;
                    return;
                }

                double NewValue;
                bool returnOK = Double.TryParse(this.Text, out NewValue);

                if (returnOK)
                {
                    if (this._Value != NewValue)
                    {
                        this._Value = NewValue;
                        ValueChanged = true;
                    }
                    //if (this.Text != NumberFormated(NewValue))
                    //{
                    //    this.Text = NumberFormated(NewValue);
                    //}
                    base.OnTextChanged(e);
                }
            }
        }

        protected override void OnLeave( System.EventArgs e)
        {
            if (this.Text != NumberFormated(this._Value))
            {
                this.Text = NumberFormated(this._Value);
            }

            base.OnLeave(e);
        }

        //protected override void OnValidated(System.EventArgs e)
        //{
        //    if (this.TextLength == 0)
        //    {
        //        this._Value = 0d;
        //        return;
        //    }

        //    double NewValue;
        //    bool returnOK = Double.TryParse(this.Text, out NewValue);

        //    if (returnOK)
        //    {
        //        if (this._Value != NewValue)
        //        {
        //            this._Value = NewValue;
        //            ValueChanged = true;
        //        }
        //        if (this.Text != NumberFormated(NewValue))
        //        {
        //            this.Text = NumberFormated(NewValue);
        //        }
        //        base.OnValidated(e);
        //    }
        //}

        //http://dobon.net/vb/dotnet/control/numerictextbox.html
        //TextBoxに数字しか入力できないようにする
        const int WM_PASTE = 0x302;

        //IMEのオン、クリップボードからの貼り付けを防ぐ
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                //文字列がクリップボードにあるか
                if (iData != null && iData.GetDataPresent(DataFormats.Text))
                {
                    string clipStr = (string)iData.GetData(DataFormats.Text);
                    //クリップボードの文字列が数字か調べる
                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                        clipStr,
                        @"^[0-9]+\.$"))
                        return;
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!IsPermitChars(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

            private static bool IsPermitChars(char chTarget)
        {
            if (char.IsControl(chTarget) || char.IsDigit(chTarget) || chTarget == '.' || chTarget == '-' || chTarget == '+' || chTarget == 'E' || chTarget == 'e')
            {
                return true;
            }
            return false;
        }

        private static string GetPermitedString(string stTarget)
        {
            string stReturn = string.Empty;

            foreach (char chTarget in stTarget)
            {
                if (IsPermitChars(chTarget))
                {
                    stReturn += chTarget;
                }
            }
            return stReturn;
        }


        ////CreateParamsをオーバーライドする方法
        //const int ES_NUMBER = 0x2000;

        //protected override CreateParams CreateParams
        //{
        //    [System.Security.Permissions.SecurityPermission(
        //        System.Security.Permissions.SecurityAction.LinkDemand,
        //        Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //    get
        //    {
        //        CreateParams parms = base.CreateParams;
        //        parms.Style |= ES_NUMBER;
        //        return parms;
        //    }
        //}
    }
}
