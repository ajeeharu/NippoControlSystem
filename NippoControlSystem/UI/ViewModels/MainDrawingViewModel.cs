using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class DrawingViewModel
    {
        // プロパティ（Viewがバインド、または参照するデータ）
        public float RawVal { get; set; }
        public string DisplayText { get; set; }

        // 描画に必要な計算結果を保持するプロパティ
        public float Angle { get; private set; }
        public string FormattedValue { get; private set; }

        // 値が更新されたときに計算を行うメソッド
        public void UpdateValue(string text, float val)
        {
            DisplayText = text;
            RawVal = val;

            // 1. 値のクランプ処理
            float valx = val * 2.0f;
            if (val < -2f) valx = -2f;
            if (val > 32f) valx = 32f;

            // 2. 角度の計算 (Model/ViewModelの責務)
            Angle = (float)(Math.PI / 180f * (25f - (valx * 1.66d)));

            // 3. 表示用文字列の整形
            FormattedValue = string.Format("{0:F1}", val);
        }
    }
}