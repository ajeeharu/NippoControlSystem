using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class frmMain : Form
    {

        private void DrawMeter(PictureBox pictureBox1,string text, float val) // メーターを描く関数
        {
            int i;  // ループ変数
            float a, x, y;  // 座標変換用変数
            Font fontMSG = new Font("ＭＳ ゴシック", 9, FontStyle.Regular);
            Font fontMSGscale = new Font("ＭＳ ゴシック", 8, FontStyle.Regular);
            SolidBrush brushBlack = new SolidBrush(Color.Black);
            Pen penRed = new Pen(Color.Red);

            //Graphics g = pictureBox1.CreateGraphics();//ピクチャーボックスの操作宣言
            Graphics g = Graphics.FromImage(pictureBox1.Image);//ピクチャーボックスの操作宣言

            //元のサイズ：540, 236
            //Point[] points = new Point[7];  //赤い針の頂点データ作成
            //points[0] = new Point(270, 60); //頂点データ0セット
            //points[1] = new Point(258, 110); //頂点データ1セット
            //points[2] = new Point(265, 110); //頂点データ2セット
            //points[3] = new Point(265, 300); //頂点データ3セット
            //points[4] = new Point(275, 300); //頂点データ4セット
            //points[5] = new Point(275, 110); //頂点データ5セット
            //points[6] = new Point(282, 110); //頂点データ6セット
            //元のサイズ：540, 236
            Point[] points = new Point[7];  //赤い針の頂点データ作成
            points[0] = new Point(270, 60); //頂点データ0セット
            points[1] = new Point(271, 60); //頂点データ1セット
            points[2] = new Point(265, 110); //頂点データ2セット
            points[3] = new Point(265, 300); //頂点データ3セット
            points[4] = new Point(275, 300); //頂点データ4セット
            points[5] = new Point(270, 300); //頂点データ5セット
            points[6] = new Point(271, 300); //頂点データ6セット

            float valx = val * 2.0f;
            if (val < -2f) valx = -2f;
            if (val > 32f) valx = 32f;
            //Image img1 = Properties.Resources.panel30;    // イメージリソース読み込み 20160801 30V→15Vに変更
            //Image img1 = Properties.Resources.panel15;    // イメージリソース読み込み 20160801 30V→15Vに変更
            Image img1 = UI.Properties.Resources.panel00;    // イメージリソース読み込み 20160801 30V→15Vに変更
            //補間方法として最近傍補間を指定する
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(img1, 0, 0, pictureBox1.Width, pictureBox1.Height); // メーターパネルを描画

            //目盛り値を印字
            g.DrawString(
                "0",  // 描画文字列
                fontMSGscale,   // Font
                brushBlack,  // SolidBrush
                6,     // X位置（左上の）
                15     // Y位置（左上の）
                );
            g.DrawString(
                "5",  // 描画文字列
                fontMSGscale,   // Font
                brushBlack,  // SolidBrush
                45,     // X位置（左上の）
                3     // Y位置（左上の）
                );
            g.DrawString(
                "10",  // 描画文字列
                fontMSGscale,   // Font
                brushBlack,  // SolidBrush
                84,     // X位置（左上の）
                3     // Y位置（左上の）
                );
            g.DrawString(
                "15",  // 描画文字列
                fontMSGscale,   // Font
                brushBlack,  // SolidBrush
                124,     // X位置（左上の）
                15     // Y位置（左上の）
                );

            //データ値を印字
            g.DrawString(
                text,  // 描画文字列
                fontMSG,   // Font
                brushBlack,  // SolidBrush
                20,     // X位置（左上の）
                35     // Y位置（左上の）
                );
            //データ値を印字
            g.DrawString(
                string.Format("{0:F1}",val),  // 描画文字列
                fontMSG,   // Font
                brushBlack,  // SolidBrush
                90,     // X位置（左上の）
                35     // Y位置（左上の）
                );


            //a = (float)(Math.PI / 180 * (30 - (66 * val / 4096)));//UDPデータから針の角度へ変換
            a = (float)(Math.PI / 180f * (25f - (valx * 1.66d)));//UDPデータから針の角度へ変換,入力値はMAX時に60度になるよう調整 Val=0～30
            for (i = 0; i < 7; i++)// 矢印の7つの頂点すべてを回転させる
            {
                //x = points[i].X - 270;  // 原点をメーター中心へシフト
                //y = 494 - points[i].Y;  // 原点を下方へシフト
                //points[i].X = (int)(x * Math.Cos(a) - y * Math.Sin(a)) + 270;// Xを回転する
                //points[i].Y = 494 - (int)(x * Math.Sin(a) + y * Math.Cos(a));// Yを回転する

                x = points[i].X - 270;  // 原点をメーター中心へシフト
                y = 780 - points[i].Y;  // 原点を下方へシフト
                //サイズを拡大・縮小する
                x = x * pictureBox1.Width / 540;    //540
                y = y * pictureBox1.Height / 300;   //247
                points[i].X = (int)(x * Math.Cos(a) - y * Math.Sin(a)) + pictureBox1.Width / 2;// Xを回転する
                points[i].Y = pictureBox1.Height * 2 + 38 - (int)(x * Math.Sin(a) + y * Math.Cos(a));// Yを回転する

                //points[i].X = (int)(x * Math.Cos(a) - y * Math.Sin(a)) + pictureBox1.Width / 2;// Xを回転する
                //points[i].Y = pictureBox1.Height - (int)(x * Math.Sin(a) + y * Math.Cos(a));// Yを回転する

            }
            //g.FillPolygon(Brushes.Red, points); // 赤い針を描画
            g.DrawLine(penRed, points[0], points[5]); // 赤い針を描画,形がぎざぎざになるので直線に変更
            g.DrawLine(penRed, points[1], points[6]); // 赤い針を描画,形がぎざぎざになるので直線に変更
            //g.FillRectangle(Brushes.Black, 5, 226, 530, 11);    // メーター下部の枠を再描画
            g.FillRectangle(Brushes.Black, 1, pictureBox1.Height - (10 * pictureBox1.Height / 300) - 1, pictureBox1.Width - (10 * pictureBox1.Width / 540), 11);    // メーター下部の枠を再描画
            pictureBox1.Refresh(); // PictureBoxを更新（再描画させる）
        }
    }
}
