using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class MainDrawingViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _meterText = string.Empty;
        private float _meterValue;
        private Image _meterImage;

        // デフォルトの描画領域サイズ (PictureBoxのサイズに合わせる)
        private int _meterWidth = 150;
        private int _meterHeight = 80;
        #endregion

        #region Properties
        /// <summary>
        /// メーターに表示するラベル文字列
        /// </summary>
        public string MeterText
        {
            get => _meterText;
            set
            {
                if (_meterText != value)
                {
                    _meterText = value;
                    OnPropertyChanged();
                    UpdateMeterImage();
                }
            }
        }

        /// <summary>
        /// メーターの測定値
        /// </summary>
        public float MeterValue
        {
            get => _meterValue;
            set
            {
                if (_meterValue != value)
                {
                    _meterValue = value;
                    OnPropertyChanged();
                    UpdateMeterImage();
                }
            }
        }

        /// <summary>
        /// View側 (PictureBox.Image など) にバインドする描画済みメーター画像
        /// </summary>
        public Image MeterImage
        {
            get => _meterImage;
            private set
            {
                if (_meterImage != value)
                {
                    _meterImage?.Dispose();
                    _meterImage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 描画キャンバスの幅
        /// </summary>
        public int MeterWidth
        {
            get => _meterWidth;
            set
            {
                if (_meterWidth != value && value > 0)
                {
                    _meterWidth = value;
                    OnPropertyChanged();
                    UpdateMeterImage();
                }
            }
        }

        /// <summary>
        /// 描画キャンバスの高さ
        /// </summary>
        public int MeterHeight
        {
            get => _meterHeight;
            set
            {
                if (_meterHeight != value && value > 0)
                {
                    _meterHeight = value;
                    OnPropertyChanged();
                    UpdateMeterImage();
                }
            }
        }
        #endregion

        #region Constructor
        public MainDrawingViewModel()
        {
            UpdateMeterImage();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// メーターの値と文字列を一度に更新
        /// </summary>
        public void SetMeterData(string text, float value)
        {
            _meterText = text;
            _meterValue = value;
            OnPropertyChanged(nameof(MeterText));
            OnPropertyChanged(nameof(MeterValue));
            UpdateMeterImage();
        }

        /// <summary>
        /// Viewの表示サイズ変化に合わせて描画サイズを変更
        /// </summary>
        public void ResizeMeter(int width, int height)
        {
            if (width <= 0 || height <= 0) return;

            _meterWidth = width;
            _meterHeight = height;
            OnPropertyChanged(nameof(MeterWidth));
            OnPropertyChanged(nameof(MeterHeight));
            UpdateMeterImage();
        }
        #endregion

        #region Drawing Logic
        /// <summary>
        /// メーター画像を非同期・オンデマンドで生成
        /// </summary>
        private void UpdateMeterImage()
        {
            if (MeterWidth <= 0 || MeterHeight <= 0) return;

            Bitmap bitmap = new(MeterWidth, MeterHeight);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Font font = new("ＭＳ ゴシック", 9, FontStyle.Regular);
                using Font fontMSG = font;
                using Font fontMSGscale = new("ＭＳ ゴシック", 8, FontStyle.Regular);
                using SolidBrush brushBlack = new(Color.Black);
                using Pen penRed = new(Color.Red);
                // 補間方法として最近傍補間を指定
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                // 背景パネル画像の描画
                Image img1 = UI.Properties.Resources.panel00;
                g.DrawImage(img1, 0, 0, MeterWidth, MeterHeight);

                // 目盛り値を印字
                g.DrawString("0", fontMSGscale, brushBlack, 6, 15);
                g.DrawString("5", fontMSGscale, brushBlack, 45, 3);
                g.DrawString("10", fontMSGscale, brushBlack, 84, 3);
                g.DrawString("15", fontMSGscale, brushBlack, 124, 15);

                // データテキスト値を印字
                g.DrawString(MeterText ?? string.Empty, fontMSG, brushBlack, 20, 35);

                // 数値データを印字 (F1フォーマット)
                g.DrawString(string.Format("{0:F1}", MeterValue), fontMSG, brushBlack, 90, 35);

                // 針の頂点座標定義
                Point[] points = new Point[7];
                points[0] = new Point(270, 60);
                points[1] = new Point(271, 60);
                points[2] = new Point(265, 110);
                points[3] = new Point(265, 300);
                points[4] = new Point(275, 300);
                points[5] = new Point(270, 300);
                points[6] = new Point(271, 300);

                // 入力値のリミット処理およびスケーリング
                float valx = MeterValue * 2.0f;
                if (MeterValue < -2f) valx = -2f;
                if (MeterValue > 32f) valx = 32f;

                // UDPデータ/入力値から針の回転角度(ラジアン)へ変換
                float a = (float)(Math.PI / 180f * (25f - (valx * 1.66d)));

                // 矢印の頂点座標変換および回転
                for (int i = 0; i < 7; i++)
                {
                    float x = points[i].X - 270; // 原点をメーター中心へシフト
                    float y = 780 - points[i].Y; // 原点を下方へシフト

                    // サイズを拡大・縮小
                    x = x * MeterWidth / 540f;
                    y = y * MeterHeight / 300f;

                    points[i].X = (int)(x * Math.Cos(a) - y * Math.Sin(a)) + MeterWidth / 2;
                    points[i].Y = MeterHeight * 2 + 38 - (int)(x * Math.Sin(a) + y * Math.Cos(a));
                }

                // 赤い針（直線）を描画
                g.DrawLine(penRed, points[0], points[5]);
                g.DrawLine(penRed, points[1], points[6]);

                // メーター下部の枠を再描画
                int rectY = MeterHeight - (10 * MeterHeight / 300) - 1;
                int rectWidth = MeterWidth - (10 * MeterWidth / 540);
                g.FillRectangle(Brushes.Black, 1, rectY, rectWidth, 11);
            }

            // 生成したビットマップをプロパティにセットしてPropertyChangedを発火
            MeterImage = bitmap;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }
        #endregion
    }
}