using NippoControlSystem.UI.ViewModels;
using System.Runtime.Versioning;


namespace NippoControlSystem.UI.Views
{
    [SupportedOSPlatform("windows")]

    public partial class InspectionMainView : Form
    {

        private DrawingViewModel _viewModel = new DrawingViewModel();

        // 外部からデータが更新されたら呼ばれる、またはデータバインディング経由で実行される
        public void RefreshMeter(string text, float val)
        {
            // 1. 計算はViewModelにお任せする
            _viewModel.UpdateValue(text, val);

            // 2. 描画処理のみをViewで行う
            DrawMeter(pictureBox1, _viewModel);
        }

        private void DrawMeter(PictureBox pictureBox, DrawingViewModel vm)
        {
            Font fontMSG = new Font("ＭＳ ゴシック", 9, FontStyle.Regular);
            Font fontMSGscale = new Font("ＭＳ ゴシック", 8, FontStyle.Regular);
            SolidBrush brushBlack = new SolidBrush(Color.Black);
            Pen penRed = new Pen(Color.Red);

            Graphics g = Graphics.FromImage(pictureBox.Image);

            Point[] points = new Point[7];
            points[0] = new Point(270, 60);
            points[1] = new Point(271, 60);
            points[2] = new Point(265, 110);
            points[3] = new Point(265, 300);
            points[4] = new Point(275, 300);
            points[5] = new Point(270, 300);
            points[6] = new Point(271, 300);

            Image img1 = UI.Properties.Resources.panel00;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(img1, 0, 0, pictureBox.Width, pictureBox.Height);

            // 目盛り値の描画
            g.DrawString("0", fontMSGscale, brushBlack, 6, 15);
            g.DrawString("5", fontMSGscale, brushBlack, 45, 3);
            g.DrawString("10", fontMSGscale, brushBlack, 84, 3);
            g.DrawString("15", fontMSGscale, brushBlack, 124, 15);

            // データ値の描画（ViewModelから取得）
            g.DrawString(vm.DisplayText, fontMSG, brushBlack, 20, 35);
            g.DrawString(vm.FormattedValue, fontMSG, brushBlack, 90, 35);

            // 針の回転・座標変換（ViewModelから計算済みの Angle をもらう）
            float a = vm.Angle;
            for (int i = 0; i < 7; i++)
            {
                float x = points[i].X - 270;
                float y = 780 - points[i].Y;

                x = x * pictureBox.Width / 540;
                y = y * pictureBox.Height / 300;
                points[i].X = (int)(x * Math.Cos(a) - y * Math.Sin(a)) + pictureBox.Width / 2;
                points[i].Y = pictureBox.Height * 2 + 38 - (int)(x * Math.Sin(a) + y * Math.Cos(a));
            }

            g.DrawLine(penRed, points[0], points[5]);
            g.DrawLine(penRed, points[1], points[6]);
            g.FillRectangle(Brushes.Black, 1, pictureBox.Height - (10 * pictureBox.Height / 300) - 1, pictureBox.Width - (10 * pictureBox.Width / 540), 11);

            pictureBox.Refresh();
        }
    }
}
