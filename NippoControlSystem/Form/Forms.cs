using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    class Forms
    {
        // 現在のフォームを引数として渡して新しいフォームを開く
        frmMain m_frmMain = null;
        frmDataInput m_frmDataInput = null;
        frmDebug m_frmDebug = null;
        frmDebugNo m_frmDebugNo = null;
        frmOpenning m_frmOpenning = null;
        frmPinAi m_frmPinAi = null;
        frmPinAo m_frmPinAo = null;
        frmPinIO m_frmPinIO = null;
        frmPinName m_frmPinName = null;
        frmPort m_frmPort = null;
        frmResultView m_frmResultView = null;
        frmSerial m_frmSerial = null;
        frmSetting m_frmSetting = null;
        frmTopEdit m_frmTopEdit = null;
        frmTopEditCopy m_frmTopEditCopy = null;
        frmTopEditInput m_frmTopEditInput = null;
        frmVersion m_frmVersion = null;
        //frmAIOMonitor m_frmAIOMonitor = null;
        //frmPinAoMonitor m_frmPinAoMonitor = null;
        frmHistView m_frmHistView = null;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static Forms m_instance = null;
        public static Forms GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new Forms();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public Forms()
        {
            //frmParent = new List <Form>();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~Forms()
        {
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmMain
        public frmMain fmMain
        {
            get { return m_frmMain; }
            set { m_frmMain = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmDataInput
        public frmDataInput fmDataInput
        {
            get { return m_frmDataInput; }
            set { m_frmDataInput = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmDebug
        public frmDebug fmDebug
        {
            get { return m_frmDebug; }
            set { m_frmDebug = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmDebugNo
        public frmDebugNo fmDebugNo
        {
            get { return m_frmDebugNo; }
            set { m_frmDebugNo = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmOpenning
        public frmOpenning fmOpenning
        {
            get { return m_frmOpenning; }
            set { m_frmOpenning = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmPinAit
        public frmPinAi fmPinAi
        {
            get { return m_frmPinAi; }
            set { m_frmPinAi = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmPinAo
        public frmPinAo fmPinAo
        {
            get { return m_frmPinAo; }
            set { m_frmPinAo = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmPinIO
        public frmPinIO fmPinIO
        {
            get { return m_frmPinIO; }
            set { m_frmPinIO = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmPinName
        public frmPinName fmPinName
        {
            get { return m_frmPinName; }
            set { m_frmPinName = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmPort
        public frmPort fmPort
        {
            get { return m_frmPort; }
            set { m_frmPort = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmResultView
        public frmResultView fmResultView
        {
            get { return m_frmResultView; }
            set { m_frmResultView = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmSerial
        public frmSerial fmSerial
        {
            get { return m_frmSerial; }
            set { m_frmSerial = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmSetting
        public frmSetting fmSetting
        {
            get { return m_frmSetting; }
            set { m_frmSetting = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmTopEdit
        public frmTopEdit fmTopEdit
        {
            get { return m_frmTopEdit; }
            set { m_frmTopEdit = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_frmTopEditCopy
        public frmTopEditCopy fmTopEditCopy
        {
            get { return m_frmTopEditCopy; }
            set { m_frmTopEditCopy = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmTopEditInput
        public frmTopEditInput fmTopEditInput
        {
            get { return m_frmTopEditInput; }
            set { m_frmTopEditInput = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ fmVersion
        public frmVersion fmVersion
        {
            get { return m_frmVersion; }
            set { m_frmVersion = value; }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ fmAIOMonitor
        //public frmAIOMonitor fmAIOMonitor
        //{
        //    get { return m_frmAIOMonitor; }
        //    set { m_frmAIOMonitor = value; }
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ fmPinAoMonitor
        //public frmPinAoMonitor fmPinAoMonitor
        //{
        //    get { return m_frmPinAoMonitor; }
        //    set { m_frmPinAoMonitor = value; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ frmHist
        public frmHistView fmHistView
        {
            get { return m_frmHistView; }
            set { m_frmHistView = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //RemoveCloseButton
        public void allFormsCreate()
        {
            if (m_frmMain == null) m_frmMain = new frmMain();
            if (m_frmDataInput == null) m_frmDataInput = new frmDataInput();
            if (m_frmDebug == null) m_frmDebug = new frmDebug();
            if (m_frmDebugNo == null) m_frmDebugNo = new frmDebugNo();
            if (m_frmOpenning == null) m_frmOpenning = new frmOpenning();
            if (m_frmPinAi == null) m_frmPinAi = new frmPinAi();
            if (m_frmPinAo == null) m_frmPinAo = new frmPinAo();
            if (m_frmPinAo == null) m_frmPinAo = new frmPinAo();
            if (m_frmPinIO == null) m_frmPinIO = new frmPinIO();
            if (m_frmOpenning == null) m_frmOpenning = new frmOpenning();
            if (m_frmPinIO == null) m_frmPinIO = new frmPinIO();
            if (m_frmPinName == null) m_frmPinName = new frmPinName();
            if (m_frmPort == null) m_frmPort = new frmPort();
            if (m_frmResultView == null) m_frmResultView = new frmResultView();
            if (m_frmSerial == null) m_frmSerial = new frmSerial();
            if (m_frmSetting == null) m_frmSetting = new frmSetting();
            if (m_frmTopEdit == null) m_frmTopEdit = new frmTopEdit();
            if (m_frmTopEditCopy == null) m_frmTopEditCopy = new frmTopEditCopy();
            if (m_frmTopEditInput == null) m_frmTopEditInput = new frmTopEditInput();
            if (m_frmVersion == null) m_frmVersion = new frmVersion();
            //if (m_frmAIOMonitor == null) m_frmAIOMonitor = new frmAIOMonitor();
            //if (m_frmPinAoMonitor == null) m_frmPinAoMonitor = new frmPinAoMonitor();
            if (m_frmHistView == null) m_frmHistView = new frmHistView();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //RemoveCloseButton
        public void allFormsClose(Form withoutForm)
        {
            if (!m_frmMain.Equals(withoutForm) && m_frmMain != null) m_frmMain.Close();
            if (!m_frmDataInput.Equals(withoutForm) && m_frmDataInput != null) m_frmDataInput.Close();
            if (!m_frmDebug.Equals(withoutForm) && m_frmDebug != null) m_frmDebug.Close();
            if (!m_frmDebugNo.Equals(withoutForm) && m_frmDebugNo != null) m_frmDebugNo.Close();
            if (!m_frmOpenning.Equals(withoutForm) && m_frmOpenning != null) m_frmOpenning.Close();
            if (!m_frmPinAi.Equals(withoutForm) && m_frmPinAi != null) m_frmPinAi.Close();
            if (!m_frmPinAo.Equals(withoutForm) && m_frmPinAo != null) m_frmPinAo.Close();
            if (!m_frmPinIO.Equals(withoutForm) && m_frmPinIO != null) m_frmPinIO.Close();
            if (!m_frmPinName.Equals(withoutForm) && m_frmPinName != null) m_frmPinName.Close();

            if (!m_frmPort.Equals(withoutForm) && m_frmPort != null) m_frmPort.Close();
            if (!m_frmResultView.Equals(withoutForm) && m_frmResultView != null) m_frmResultView.Close();
            if (!m_frmSerial.Equals(withoutForm) && m_frmSerial != null) m_frmSerial.Close();
            if (!m_frmSetting.Equals(withoutForm) && m_frmSetting != null) m_frmSetting.Close();
            if (!m_frmTopEdit.Equals(withoutForm) && m_frmTopEdit != null) m_frmTopEdit.Close();
            if (!m_frmTopEditCopy.Equals(withoutForm) && m_frmTopEditCopy != null) m_frmTopEditCopy.Close();

            if (!m_frmTopEditInput.Equals(withoutForm) && m_frmTopEditInput != null) m_frmTopEditInput.Close();
            if (!m_frmVersion.Equals(withoutForm) && m_frmVersion != null) m_frmVersion.Close();
            //if (!m_frmAIOMonitor.Equals(withoutForm) && m_frmAIOMonitor != null) m_frmAIOMonitor.Close();
            //if (!m_frmPinAoMonitor.Equals(withoutForm) && m_frmPinAoMonitor != null) m_frmPinAoMonitor.Close();
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////DesktopLocation
        static public void DesktopLocation(Form f, string loc)   //loc as 100,200
        {
            if (string.IsNullOrEmpty(loc))
                return;
            string[] sPoint = loc.Split(new char [] {','});
            if (sPoint.Length != 2)
                return;
            int x, y;
            if (!int.TryParse(sPoint[0], out x))
                return;
            if (!int.TryParse(sPoint[1], out y))
                return;
            if (x < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width && y < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)
                f.DesktopLocation = new Point(x, y);
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////RemoveCloseButton
        //public bool fBInitShowWithWait()
        //{
        //    Cyc.IO.ComPlus plcif = Cyc.IO.ComPlus.GetInstance();
        //    //plcif.Init();

        //    //[制御機器チェック]ダイアログボックスの処理(&表示)
        //    m_fmBInit.cmdExecute.DoClick();
        //    //m_fmBInit.ShowDialog();
        //    while (m_fmBInit.Completed == false)
        //    {
        //        Application.DoEvents();
        //    }
        //    return m_fmBInit.Ok;
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //RemoveCloseButton
        public static void RemoveCloseButton(System.Windows.Forms.Form f)
        {
            // コントロールボックスの［閉じる］ボタンの無効化
            IntPtr hMenu = GetSystemMenu(f.Handle, 0);
            RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
            //RemoveMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            //RemoveMenu(hMenu, SC_MINIMIZE, MF_BYCOMMAND);
            //RemoveMenu(hMenu, SC_RESTORE, MF_BYCOMMAND);
            // 最小化ボタンを無効にする
            f.MinimizeBox = false;
            f.MaximizeBox = false;

        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // Win32 APIのインポート
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, UInt32 bRevert);
        [DllImport("USER32.DLL")]
        private static extern UInt32 RemoveMenu(IntPtr hMenu, UInt32 nPosition, UInt32 wFlags);

        // ［閉じる］ボタンを無効化するための値
        private const UInt32 SC_CLOSE = 0x0000F060;
        private const UInt32 SC_MAXIMIZE = 0x0000F030;
        private const UInt32 SC_MINIMIZE = 0x0000F020;
        private const UInt32 SC_RESTORE = 0x00000120;
        private const UInt32 MF_BYCOMMAND = 0x00000000;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //FullScreenModeの設定
        //string viewMode = Cyc.IO.IniFileHandler.GetPrivateProfileString("Window", "WindowStyle", "2");
        string viewMode = Cyc.IO.Settings.GetInstance().WindowStyle;
        // フルスクリーン・モードかどうかのフラグ
        private bool _bScreenMode;
        // フルスクリーン表示前のウィンドウの状態を保存する
        private FormWindowState prevFormState;
        // 通常表示時のフォームの境界線スタイルを保存する
        private FormBorderStyle prevFormStyle;
        // 通常表示時のウィンドウのサイズを保存する
        private Size prevFormSize;

        public bool FullScreenMode
        {
            get { return (_bScreenMode); }
        }

        public void FullScreen(Form frm)
        {
            switch (viewMode)
            {
                case "1":
                    // ＜フルスクリーン表示への切り替え処理＞

                    // 通常表示時のウィンドウのサイズを保存する
                    prevFormSize = frm.Size;

                    // ウィンドウの状態を保存する
                    prevFormState = frm.WindowState;
                    // 境界線スタイルを保存する
                    prevFormStyle = frm.FormBorderStyle;

                    // 0. 「最大化表示」→「フルスクリーン表示」では
                    // タスク・バーが消えないので、いったん「通常表示」を行う
                    //if (frm.WindowState == FormWindowState.Maximized)
                    //{
                    //    frm.WindowState = FormWindowState.Normal;
                    //}

                    // フォームのサイズを保存する
                    //prevFormSize = this.ClientSize;

                    // 1. フォームの境界線スタイルを「None」にする
                    frm.FormBorderStyle = FormBorderStyle.None;
                    // 2. フォームのウィンドウ状態を「最大化」する
                    frm.WindowState = FormWindowState.Maximized;

                    // フルスクリーン表示をONにする
                    _bScreenMode = true;
                    break;

                case "2":
                    // ウィンドウ状態を「最大化」する
                    frm.WindowState = FormWindowState.Maximized;
                    foreach (Control ctl in frm.Controls)
                    {
                        if (ctl.GetType() == typeof(System.Windows.Forms.Button))
                        {
                            if (ctl.Name == "button1")
                            {
                                ctl.Visible = false;
                            }
                        }
                    }
                    break;


                default:
                    // 何もしない
                    break;

            }
        }

        public void NormalScreen(Form frm)
        {
            // ＜通常表示／最大化表示への切り替え処理＞

            // フォームのウィンドウのサイズを元に戻す
            frm.ClientSize = prevFormSize;

            // 0. 最大化表示に戻す場合にはいったん通常表示を行う
            // （フルスクリーン表示の処理とのバランスと取るため）
            if (prevFormState == FormWindowState.Maximized)
            {
                frm.WindowState = FormWindowState.Normal;
            }

            // 1. フォームの境界線スタイルを元に戻す
            frm.FormBorderStyle = prevFormStyle;

            // 2. フォームのウィンドウ状態を元に戻す
            frm.WindowState = prevFormState;

            // フルスクリーン表示をOFFにする
            _bScreenMode = false;
        }

        [DllImport("user32.dll")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        const int HWND_TOP = -2;
        private const uint SWP_HIDEWINDOW = 0x80;
        private const uint SWP_SHOWWINDOW = 0x40;

        //m_HideTaskbar
        //string m_HideTaskbar = Cyc.IO.IniFileHandler.GetPrivateProfileString("Window", "HideTaskbar", @"0");
        string m_HideTaskbar = Cyc.IO.Settings.GetInstance().HideTaskbar;   // @"0"

        public void HideTaskbar(bool mode)
        {
            //クラス名を与えてタスクバーのハンドルを取得
            IntPtr lnghwnd = FindWindow("Shell_traywnd", null);
            //タスクバーを非表示
            bool retValue = SetWindowPos(lnghwnd, HWND_TOP, 0, 0, 0, 0, (mode == true ? SWP_HIDEWINDOW : SWP_SHOWWINDOW));
        }

        public void HideTaskbar()
        {
            HideTaskbar(m_HideTaskbar == @"1");
        }

        //List<Form> frmParent = null;
        //public void ShowDialog(Form parent, Form child)
        //{
        //    if (child != null)
        //    {
        //        //frmParent.Add((Form)parent);
        //        //child.Show();
        //        //parent.Hide();
        //        ////while (child.Visible == true)
        //        ////{
        //        ////    System.Threading.Thread.Sleep(100);
        //        ////}
        //        ////ev.WaitOne();
        //        child.ShowDialog();
        //    }
        //}
        //public void Hide(Form child, FormClosingEventArgs e)
        //{
        //    this.Hide(child);
        //    //e.Cancel = true;
        //}
        //public void Hide(Form child)
        //{
        //    //Form parent = frmParent.Last();
        //    //if (parent != null)
        //    //{
        //    //    parent.Show();
        //    //    child.Hide();
        //    //    frmParent.Remove(parent);
        //    //}
        //    child.DialogResult = DialogResult.OK;
        //    child.Hide();
        //}
    }
}
