using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace WindowsAPI
{
    public class CheckAlreadyRunning
    {
        // 外部プロセスのメイン・ウィンドウを起動するためのWin32 API
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, ShowWindowEnum nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint procId);

        [DllImport("user32")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        // ShowWindowAsync関数のパラメータに渡す定義値
        public enum ShowWindowEnum : int
        {
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 11
        }

        // コールバックメソッドのデリゲート
        private delegate int EnumerateWindowsCallback(IntPtr hWnd, int lParam);

        [DllImport("user32", EntryPoint = "EnumWindows")]
        private static extern int EnumWindows(EnumerateWindowsCallback lpEnumFunc, int lParam);

        private static System.Diagnostics.Process target_proc = null;
        private static IntPtr target_hwnd = IntPtr.Zero;
        private static string target_caption = null;


        // ウィンドウを列挙するためのコールバックメソッド
        public static int EnumerateWindows(IntPtr hWnd, int lParam)
        {
            uint procId = 0;
            uint result = GetWindowThreadProcessId(hWnd, ref procId);
            if (procId == target_proc.Id)
            {
                // すべてのウィンドウから、指定のProcessId、Captionを持つウィンドウを見つける
                // とりあえず最初のウィンドウが見つかった時点で終了する
                StringBuilder caption = new StringBuilder(0x1000);

                GetWindowText(hWnd, caption, caption.Capacity);

                if (caption.ToString() == target_caption)
                {
                    target_hwnd = hWnd;
                    return 0;
                }
            }

            // 列挙を継続するには0以外を返す必要がある
            return 1;
        }

        // 外部プロセスのウィンドウを最前面に表示する
        public static void WakeupWindow(System.Diagnostics.Process target, string caption)
        {
            //コールバックルーチンで参照するために、共通変数にSetする
            target_proc = target;
            target_caption = caption;               //"PC連動コンセント";
            target_hwnd = IntPtr.Zero;
            EnumWindows(new EnumerateWindowsCallback(EnumerateWindows), 0);
            if (target_hwnd != IntPtr.Zero)
            {
                // メイン・ウィンドウが最小化か、不可視なら元に戻す
                if (IsIconic(target_hwnd) || IsWindowVisible(target_hwnd) == false)
                {
                    ShowWindowAsync(target_hwnd, ShowWindowEnum.SW_RESTORE);
                }
                // メイン・ウィンドウを最前面に表示する
                SetForegroundWindow(target_hwnd);
                // タスクトレイへの表示は、frmMain_Resizeで実施
            }
        }

        // 実行中の同じアプリケーションのプロセスを取得する
        public static System.Diagnostics.Process GetPreviousProcess()
        {
            System.Diagnostics.Process curProcess = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] allProcesses = System.Diagnostics.Process.GetProcessesByName(curProcess.ProcessName);

            foreach (System.Diagnostics.Process checkProcess in allProcesses)
            {
                // 自分自身のプロセスIDは無視する
                if (checkProcess.Id != curProcess.Id)
                {
                    // プロセスのフルパス名を比較して同じアプリケーションか検証
                    if (String.Compare(
                        checkProcess.MainModule.FileName,
                        curProcess.MainModule.FileName, true) == 0)
                    {
                        // 同じフルパス名のプロセスを取得
                        return checkProcess;
                    }
                }
            }

            // 同じアプリケーションのプロセスが見つからない！
            return null;
        }
    }
}
