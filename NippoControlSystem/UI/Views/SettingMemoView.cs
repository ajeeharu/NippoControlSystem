using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; // setforegraoundwindow
using System.Diagnostics;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class SettingView : Form
    {
        public const uint WM_LBUTTONDOWN = 0x201;
        public const uint WM_LBUTTONUP = 0x202;
        public const uint MK_LBUTTON = 0x0001;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        // イベントを送信するためのWin32 API
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        // 送信するメッセージ
        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x0101;


        /// <summary>
        /// エントリポイント
        /// </summary>
        public bool CloseNotepad(string FilePath)
        {
            string FileName = System.IO.Path.GetFileName(FilePath);    //変更履歴.txt
            string title = string.Format("{0} - メモ帳", FileName);    //変更履歴.txt - メモ帳"
            //タイトルが"無題 - メモ帳"のウィンドウを探す
            //IntPtr hWnd = FindWindow(null, "変更履歴.txt - メモ帳");
            IntPtr hWnd = FindWindow(null, title);      //"変更履歴.txt - メモ帳"
            if (hWnd != IntPtr.Zero)
            {
                //ウィンドウを作成したプロセスのIDを取得する
                int processId;
                GetWindowThreadProcessId(hWnd, out processId);
                //Processオブジェクトを作成する
                Process p = Process.GetProcessById(processId);

                Console.WriteLine("プロセス名:" + p.ProcessName);
                //メモ帳を終了させる
                p.CloseMainWindow();
                p.Close();

                System.Diagnostics.Stopwatch sw = new Stopwatch();
                sw.Start(); //ストップウォッチ開始

                //メモ帳の終了を待つ
                while (true)
                {
                    //タイトルが"無題 - メモ帳"のウィンドウを探す
                    hWnd = FindWindow(null, title);      //"変更履歴.txt - メモ帳"
                    if (hWnd == IntPtr.Zero)
                    {
                        //見つけられなければ、終了
                        break;
                    }
                    //保存しますかのウィンドウを探す
                    hWnd = FindWindow("#32770", "メモ帳");
                    if (hWnd != IntPtr.Zero)
                    {
                        // プロセス名がnotepad(メモ帳)の画面情報を取得します。
                        WindowsHandles.Clear();
                        WindowsHandles.Initialize("notepad");
                        // 画面情報に「保存する」ボタンがあるかチェックします。
                        IntPtr hWndSave = WindowsHandles.GethWndExistsTitle("保存する(&S)");
                        if (hWndSave != IntPtr.Zero)
                        {
                            //保存するのボタンを押す
                            //List<List<WindowsHandles.Window>> WindowsList = WindowsHandles.WindowsList;
                            //PostMessage(WindowsList[0][3].hWnd, WM_KEYDOWN, (IntPtr)ConsoleKey.Enter, IntPtr.Zero);
                            PostMessage(hWndSave, WM_KEYDOWN, (IntPtr)ConsoleKey.Enter, IntPtr.Zero);
                        }
                    }
                    if (sw.ElapsedMilliseconds > 3000)
                    {
                        //時間が経っても終了しないとき
                        return false;
                    }
                    System.Threading.Thread.Sleep(50);
                }
            }
            else
            {
                Console.WriteLine("見つかりませんでした。");
            }
            return true;
        }

                /// <summary>
        /// エントリポイント
        /// </summary>
        public void OpenNotepad(string FilePath)
        {
            //コマンドライン引数に「"C:\test\1.txt"」を指定してメモ帳を起動する
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("notepad.exe", FilePath);
            p.WaitForInputIdle(); // 起動完了待ち
        }

        /// <summary>
        /// エントリポイント
        /// </summary>
        public void StartNotepad(string FilePath)
        {

            IntPtr hWnd = (IntPtr)0;

            if (System.IO.Directory.Exists(this.Folder))
            {
                try
                {
                    //string FilePath = this.Folder + System.IO.Path.DirectorySeparatorChar + @"変更履歴.txt";
                    if (!System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.CreateText(FilePath);
                    }
                    string testAll = string.Format("〇{0}\r\n\r\n", System.DateTime.Now.ToString());
                    testAll += System.IO.File.ReadAllText(FilePath);
                    System.IO.File.WriteAllText(FilePath, testAll);
                    //コマンドライン引数に「"C:\test\1.txt"」を指定してメモ帳を起動する
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start("notepad.exe", FilePath);
                    p.WaitForInputIdle(); // 起動完了待ち

                    //メモ帳が入力を待ち状態になるまで待機
                    // 対象ウィンドウにキーダウンメッセージを送信する
                    // プロセス名がnotepad(メモ帳)の画面情報を取得します。
                    WindowsHandles.Clear();
                    WindowsHandles.Initialize("notepad");
                    while (WindowsHandles.WindowsList.Count != 3)
                    {
                        System.Threading.Thread.Sleep(10);
                        WindowsHandles.Clear();
                        WindowsHandles.Initialize("notepad");
                    }

                    //p.WaitForInputIdle(); // 起動完了待ち
                    ////デスクトップウィンドウのハンドルを取得
                    //HWND hwnd = GetDesktopWindow();

                    ////メモ帳のウィンドウハンドルを取得
                    //hwnd = FindWindowEx(hwnd, NULL, L"Notepad", NULL);

                    ////メモ帳のEditのハンドルを取得
                    //hwnd = FindWindowEx(hwnd, NULL, L"Edit", NULL);

                    // WindowsHandles.WindowsListプロパティはPublicなのでご自由に。
                    //デバッグ用にすべての要素を印字する
                    List<List<WindowsHandles.Window>> WindowsList = WindowsHandles.WindowsList;
                    Console.WriteLine(string.Format("WindowsHandles.WindowsList.Count={0}", WindowsList.Count));
                    foreach (List<WindowsHandles.Window> list in WindowsList)
                    {
                        Console.WriteLine(string.Format("list.Count={0}", list.Count));
                        foreach (WindowsHandles.Window w in list)
                        {
                            Console.WriteLine(string.Format("hWnd={0},Title={1},ClassName={2}", w.hWnd.ToString("X8"), w.Title, w.ClassName));
                        }
                    }
                    //タイトルが"無題 - メモ帳"のウィンドウを探す
                    //hWnd = FindWindow(null, "変更履歴.txt - メモ帳");

                    //hWnd=000C0890,Title=変更履歴.txt - メモ帳,ClassName=Notepad
                    hWnd = WindowsHandles.GethWndExistsClassName("Edit");
                    if (hWnd != IntPtr.Zero)
                    {
                        //List<List<WindowsHandles.Window>> WindowsList = WindowsHandles.WindowsList;
                        //PostMessage(WindowsList[0][1].hWnd, WM_KEYDOWN, (IntPtr)ConsoleKey.Enter, IntPtr.Zero);
                        PostMessage(hWnd, WM_KEYDOWN, (IntPtr)ConsoleKey.DownArrow, IntPtr.Zero);
                        //PostMessage(hWnd, WM_KEYUP, (IntPtr)ConsoleKey.DownArrow, IntPtr.Zero);
                    }
                    //デスクトップウィンドウのハンドルを取得
                    //HWND hwnd = GetDesktopWindow();

                    ////メモ帳のウィンドウハンドルを取得
                    //hwnd = FindWindowEx(hwnd, NULL, L"Notepad", NULL);

                    ////メモ帳のEditのハンドルを取得
                    //hwnd = FindWindowEx(hwnd, NULL, L"Edit", NULL);

                    ////キー操作を送信する
                    //PostMessage(hwnd, WM_KEYDOWN, VK_DOWN, 0);  //↓キー
                    //PostMessage(hwnd, WM_KEYDOWN, VK_DOWN, 0);  //↓キー
                }
                catch(Exception ex)
                {
                    //Exception 20180931
                    System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。[{0}]",ex.Message), Default.ApplicationName);
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(
            string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(
            IntPtr hWnd, out int lpdwProcessId);
    }
}
