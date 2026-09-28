using NippoControlSystem.ApplicationService.Interfaces.enums;
using NippoControlSystem.ApplicationService.Interfaces.Models;

namespace NippoControlSystem.ApplicationService.Interfaces
{
    /// <summary>
    /// ウィンドウ操作・探索を提供するサービスインターフェース
    /// </summary>
    public interface IWindowService
    {
        bool SetForegroundWindow(IntPtr hWnd);
        bool ShowWindowAsync(IntPtr hWnd, ShowWindowCommand nCmdShow);
        bool IsIconic(IntPtr hWnd);
        bool IsWindowVisible(IntPtr hWnd);
        uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
        string GetActiveProcessName();
        void SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// 指定プロセスのウィンドウ情報を取得します
        /// </summary>
        IReadOnlyList<WindowInfo> GetProcessWindows(string processName, bool includeInvisible = false);

        /// <summary>
        /// 指定タイトルのウィンドウハンドルを取得します
        /// </summary>
        IntPtr FindWindowByTitle(string title, string? processName = null);

        /// <summary>
        /// 指定クラス名のウィンドウハンドルを取得します
        /// </summary>
        IntPtr FindWindowByClassName(string className, string? processName = null);
    }
}