using NippoControlSystem.Infrastructure.Configuration;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Controls
{
    class screenShot
    {
        //フォームのイメージを保存する変数
        private Bitmap memoryImage;
        Settings Default = Settings.GetInstance();

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static screenShot m_instance = null;
        public static screenShot GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new screenShot();
            }
            return m_instance;
        }

        /// <summary>
        /// フォームのイメージを印刷する
        /// </summary>
        /// <param name="frm">イメージを印刷するフォーム</param>
        /// 
        public void PrintForm(Form frm)
        {
            PrintForm(frm, false);
        }
        public void PrintFormWithPrintDialog(Form frm)
        {
            PrintForm(frm, true);
        }

        public void PrintForm(Form frm, bool bPrintDialog)
        {
            // http://dobon.net/vb/dotnet/vb6/printform.html
            //フォームのイメージを取得する
            memoryImage = CaptureControl(frm);
            //フォームのイメージを印刷する
            System.Drawing.Printing.PrintDocument PrintDocument1 =
                 new System.Drawing.Printing.PrintDocument();
            PrintDocument1.DefaultPageSettings.Landscape = Default.DefaultPageSettings_Landscape;

            PrintDocument1.PrintPage +=
                 new System.Drawing.Printing.PrintPageEventHandler(
                 PrintDocument1_PrintPage);

            //マージンを指定する（上下左右1インチに設定する）
            //PrintDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            PrintDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(Default.DefaultPageSettings_Margins_Left, Default.DefaultPageSettings_Margins_Right, Default.DefaultPageSettings_Margins_Top, Default.DefaultPageSettings_Margins_Bottom);

            if (bPrintDialog)
            {
                //PrintDialogクラスの作成
                PrintDialog pdlg = new PrintDialog();
                //PrintDocumentを指定
                pdlg.Document = PrintDocument1;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.OK)
                {
                    //OKがクリックされた時は印刷する
                    PrintDocument1.Print();
                }
            }
            else
            {
                PrintDocument1.Print();
            }

            memoryImage.Dispose();
        }

        public void PrintFormMargins(Form frm)
        {
            //フォームのイメージを取得する
            memoryImage = CaptureControl(frm);
            //フォームのイメージを印刷する
            System.Drawing.Printing.PrintDocument PrintDocument1 =
                 new System.Drawing.Printing.PrintDocument();
            PrintDocument1.PrintPage +=
                 new System.Drawing.Printing.PrintPageEventHandler(
                 PrintDocument1_PrintPage);

            //マージンを指定する（上下左右1インチに設定する）
            PrintDocument1.DefaultPageSettings.Margins =
                new System.Drawing.Printing.Margins(100, 100, 100, 100);

            //PageSetupDialogクラスの作成
            PageSetupDialog psd = new PageSetupDialog();
            psd.EnableMetric = true;    //インチとミリメートルの変換が正常に行われるようにする
            //PrintDocumentを指定
            psd.Document = PrintDocument1;
            //ページ設定ダイアログを表示する
            if (psd.ShowDialog() == DialogResult.OK)
            {
                //OKがクリックされた時は印刷する
                PrintDocument1.Print();
            }
            memoryImage.Dispose();
        }

        public void PrintFormPreview(Form frm)
        {
            //フォームのイメージを取得する
            memoryImage = CaptureControl(frm);
            //フォームのイメージを印刷する
            System.Drawing.Printing.PrintDocument PrintDocument1 =
                 new System.Drawing.Printing.PrintDocument();
            PrintDocument1.PrintPage +=
                 new System.Drawing.Printing.PrintPageEventHandler(
                 PrintDocument1_PrintPage);

            //マージンを指定する（上下左右1インチに設定する）
            PrintDocument1.DefaultPageSettings.Margins =
                new System.Drawing.Printing.Margins(100, 100, 100, 100);

            //PrintPreviewDialogオブジェクトの作成
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            SelectPrintPreviewDialog ppd = new SelectPrintPreviewDialog();  //ここで、印刷前にプリンタ選択ができるPrintPreviewDialogを呼ぶ
            //プレビューするPrintDocumentを設定
            ppd.Document = PrintDocument1;
            ppd.AutoClose = true;
            //印刷プレビューダイアログを表示する
            ppd.ShowDialog();

            memoryImage.Dispose();
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest,
             int nXDest, int nYDest, int nWidth, int nHeight,
             IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        private const int SRCCOPY = 0xCC0020;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private extern static bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        /// <summary>
        /// コントロールのイメージを取得する
        /// </summary>
        /// <param name="ctrl">キャプチャするコントロール</param>
        /// <returns>取得できたイメージ</returns>
        public Bitmap CaptureControl(Control ctrl)
        {
            Bitmap img = new Bitmap(ctrl.Width, ctrl.Height);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc = memg.GetHdc();
            PrintWindow(ctrl.Handle, dc, 0);
            memg.ReleaseHdc(dc);
            memg.Dispose();
            return img;
        }

        /// <summary>
        /// コントロールのイメージを取得する
        /// </summary>
        /// <param name="ctrl">キャプチャするコントロール</param>
        /// <returns>取得できたイメージ</returns>
        public Bitmap CaptureControlXX(Control ctrl)
        {
            Graphics g = ctrl.CreateGraphics();
            Bitmap img = new Bitmap(ctrl.ClientRectangle.Width,
                ctrl.ClientRectangle.Height, g);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc1 = g.GetHdc();
            IntPtr dc2 = memg.GetHdc();
            BitBlt(dc2, 0, 0, img.Width, img.Height, dc1, 0, 0, SRCCOPY);
            g.ReleaseHdc(dc1);
            memg.ReleaseHdc(dc2);
            memg.Dispose();
            g.Dispose();
            return img;
        }

        //public Bitmap CaptureControlXXX(Control ctrl)
        //{
        //    //画面が隠れない内に、自身のBitmapを得る
        //    //コントロールの外観を描画するBitmapの作成
        //    Bitmap bmp = new Bitmap(this.Width, this.Height);
        //    //キャプチャする
        //    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
        //    //ファイルに保存する
        //    bmp.Save(mc.Folder + "1.png");
        //    //後始末
        //    bmp.Dispose();
        //}


        //PrintDocument1のPrintPageイベントハンドラ
        private void PrintDocument1_PrintPage(object sender,
             System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(memoryImage, 0, 0);
            //e.Graphics.DrawImage(memoryImage, e.MarginBounds);    //これだと、マージンはされるが、イメージがいっぱいに拡大される
            if (e.PageSettings.Landscape)
            {
                //横置き
                if (memoryImage.Width > e.MarginBounds.Width)
                {
                    int newHeight = memoryImage.Height * e.MarginBounds.Width / memoryImage.Width;
                    if (newHeight > e.MarginBounds.Height)
                    {
                        int newWidth = memoryImage.Width * e.MarginBounds.Height / memoryImage.Height;
                        e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, newWidth, e.MarginBounds.Height);
                    }
                    else
                    {
                        e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, newHeight);
                    }
                }
                else
                {
                    e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top);
                }
            }
            else
            {
                //縦置き(縦と同じです)
                if (memoryImage.Width > e.MarginBounds.Width)
                {
                    int newHeight = memoryImage.Height * e.MarginBounds.Width / memoryImage.Width;
                    if (newHeight > e.MarginBounds.Height)
                    {
                        int newWidth = memoryImage.Width * e.MarginBounds.Height / memoryImage.Height;
                        e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, newWidth, e.MarginBounds.Height);
                    }
                    else
                    {
                        e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, newHeight);
                    }
                }
                else
                {
                    e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top);
                }
            }
        }

        ////Button1のClickイベントハンドラ
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    PrintForm(this);
        //}
    }
    /// <summary>
    /// [印刷]ボタンでプリンタ選択できるPrintPreviewDialog
    /// http://homepage1.nifty.com/yamato/texts/memorandum/memo5_1.html
    /// </summary>
    class SelectPrintPreviewDialog : PrintPreviewDialog
    {
        /// <summary>初回読み込み時のみの処理実行フラグ</summary>
        private bool _NewLoad = true;
        /// <summary>印刷終了後のダイアログ自動終了フラグ</summary>
        private bool _CloseFlg = false;

        /// <summary>自動終了実行フラグ</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Browsable(true)]
        public bool AutoClose
        {
            get { return _CloseFlg; }
            set { _CloseFlg = value; }
        }

        /// <summary>
        /// 継承されたコンストラクタ
        /// </summary>
        public SelectPrintPreviewDialog()
        {
        }

        /// <summary>
        /// 継承されたOnLoadイベント
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            if (_NewLoad)
            {
                ToolStrip tool = this.Controls[1] as ToolStrip;
                ToolStripButton tbtn = new ToolStripButton();
                ToolStripSeparator tsep = new ToolStripSeparator();

                tbtn.Image = tool.Items[0].Image.Clone() as Image;
                tbtn.ToolTipText = tool.Items[0].ToolTipText;
                tbtn.Click += new EventHandler(printerselectAndPrint);

                // [印刷]ボタンを差し替える(一緒に区切り線も挿入)
                tool.Items.RemoveAt(0);
                tool.Items.Insert(0, tbtn);
                tool.Items.Insert(1, tsep);

                _NewLoad = false;
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// 印刷実行前にプリンタ選択ダイアログを表示するイベント
        /// </summary>
        private void printerselectAndPrint(object sender, EventArgs e)
        {
            if (this.Document == null)
            {
                return;
            }

            using (PrintDialog prtDlg = new PrintDialog())
            {
                // XP Styleを有効に(Windows 2000以前だと無視される)
                prtDlg.UseEXDialog = true;
                // プレビューの印刷設定をダイアログに渡す
                prtDlg.Document = this.Document;

                if (prtDlg.ShowDialog() == DialogResult.OK)
                {
                    // ダイアログで変更された設定を書き戻す
                    this.Document = prtDlg.Document;

                    // 印刷実行
                    this.Document.Print();

                    // 自動終了が有効ならダイアログを終了する
                    if (_CloseFlg)
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}


