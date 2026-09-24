using NippoControlSystem.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// メモ帳連携・変更履歴編集（SettingMemoView）用 ViewModel
    /// </summary>
    public class SettingMemoViewModel : INotifyPropertyChanged
    {
        #region Native Win32 API Imports
        private static class NativeMethods
        {
            public const uint WM_KEYDOWN = 0x100;
            public const uint WM_KEYUP = 0x0101;

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
            public static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        }
        #endregion

        #region Fields
        private string _folder = string.Empty;
        private string _historyFileName = "変更履歴.txt";
        private bool _isBusy;
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// 変更履歴ファイルが格納される対象フォルダパス
        /// </summary>
        public string Folder
        {
            get => _folder;
            set
            {
                if (_folder != value)
                {
                    _folder = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HistoryFilePath));
                }
            }
        }

        /// <summary>
        /// 変更履歴ファイルのファイル名（既定: 変更履歴.txt）
        /// </summary>
        public string HistoryFileName
        {
            get => _historyFileName;
            set
            {
                if (_historyFileName != value)
                {
                    _historyFileName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HistoryFilePath));
                }
            }
        }

        /// <summary>
        /// フルパス演算プロパティ
        /// </summary>
        public string HistoryFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(Folder)) return string.Empty;
                return Path.Combine(Folder, HistoryFileName);
            }
        }

        /// <summary>
        /// 処理中（メモ帳操作中等）フラグ
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            private set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// メッセージ表示要求イベント (メッセージ内容, タイトル)
        /// </summary>
        public event Action<string, string> RequestShowMessage;
        #endregion

        #region Constructors
        public SettingMemoViewModel()
        {
        }

        public SettingMemoViewModel(string folder)
        {
            Folder = folder;
        }
        #endregion

        #region Notepad Control Logic
        /// <summary>
        /// 変更履歴ファイルへ現在日時を自動挿入し、メモ帳で起動する
        /// </summary>
        public void StartNotepad()
        {
            StartNotepad(HistoryFilePath);
        }

        /// <summary>
        /// 指定パスの変更履歴ファイルへ現在日時を自動挿入し、メモ帳で起動する
        /// </summary>
        public void StartNotepad(string filePath)
        {
            if (string.IsNullOrEmpty(Folder) || !Directory.Exists(Folder)) return;

            var defaultSettings = Settings.GetInstance();

            try
            {
                IsBusy = true;

                // ファイルが存在しない場合は新規作成
                if (!File.Exists(filePath))
                {
                    using (File.CreateText(filePath)) { }
                }

                // 日時ヘッダーを先頭に挿入
                string currentContent = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
                string newHeader = string.Format("〇{0}\r\n\r\n", DateTime.Now.ToString());
                File.WriteAllText(filePath, newHeader + currentContent);

                // メモ帳を起動
                Process p = Process.Start("notepad.exe", filePath);
                p?.WaitForInputIdle();

                // ウィンドウハンドルの取得待ち
                WindowsHandles.Clear();
                WindowsHandles.Initialize("notepad");
                int retryCount = 0;
                while (WindowsHandles.WindowsList.Count != 3 && retryCount < 50)
                {
                    System.Threading.Thread.Sleep(100);
                    WindowsHandles.Clear();
                    WindowsHandles.Initialize("notepad");
                    retryCount++;
                }

                // デバッグ用ログ出力
                List<List<WindowsHandles.Window>> windowsList = WindowsHandles.WindowsList;
                Console.WriteLine($"WindowsHandles.WindowsList.Count={windowsList.Count}");
                foreach (List<WindowsHandles.Window> list in windowsList)
                {
                    foreach (WindowsHandles.Window w in list)
                    {
                        Console.WriteLine($"hWnd={w.hWnd:X8}, Title={w.Title}, ClassName={w.ClassName}");
                    }
                }

                // メモ帳のエディタ部にカーソルフォーカスを送出 (DownArrow)
                IntPtr hWndEdit = WindowsHandles.GethWndExistsClassName("Edit");
                if (hWndEdit != IntPtr.Zero)
                {
                    NativeMethods.PostMessage(hWndEdit, NativeMethods.WM_KEYDOWN, (IntPtr)ConsoleKey.DownArrow, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                RequestShowMessage?.Invoke(
                    string.Format("書き込みができません。書込許可があるか確認して下さい。[{0}]", ex.Message),
                    defaultSettings?.ApplicationName ?? "システム"
                );
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 指定パスのメモ帳が開いている場合、保存を確定させて閉じる
        /// </summary>
        public bool CloseNotepad(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string title = string.Format("{0} - メモ帳", fileName);

            IntPtr hWnd = NativeMethods.FindWindow(null, title);
            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("対象のメモ帳ウィンドウは見つかりませんでした。");
                return true;
            }

            try
            {
                IsBusy = true;

                int v = NativeMethods.GetWindowThreadProcessId(hWnd, out int processId);
                Process p = Process.GetProcessById(processId);

                Console.WriteLine("プロセス名:" + p.ProcessName);
                p.CloseMainWindow();
                p.Close();

                Stopwatch sw = Stopwatch.StartNew();

                while (true)
                {
                    hWnd = NativeMethods.FindWindow(null, title);
                    if (hWnd == IntPtr.Zero)
                    {
                        break; // 正常終了
                    }

                    // 「保存しますか」ダイアログの検知
                    hWnd = NativeMethods.FindWindow("#32770", "メモ帳");
                    if (hWnd != IntPtr.Zero)
                    {
                        WindowsHandles.Clear();
                        WindowsHandles.Initialize("notepad");
                        IntPtr hWndSave = WindowsHandles.GethWndExistsTitle("保存する(&S)");
                        if (hWndSave != IntPtr.Zero)
                        {
                            NativeMethods.PostMessage(hWndSave, NativeMethods.WM_KEYDOWN, (IntPtr)ConsoleKey.Enter, IntPtr.Zero);
                        }
                    }

                    if (sw.ElapsedMilliseconds > 3000)
                    {
                        // タイムアウト
                        return false;
                    }
                    System.Threading.Thread.Sleep(50);
                }
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }

        /// <summary>
        /// メモ帳を開く（日時ヘッダー挿入なし）
        /// </summary>
        public static void OpenNotepad(string filePath)
        {
            if (!File.Exists(filePath)) return;

            Process p = Process.Start("notepad.exe", filePath);
            p?.WaitForInputIdle();
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}