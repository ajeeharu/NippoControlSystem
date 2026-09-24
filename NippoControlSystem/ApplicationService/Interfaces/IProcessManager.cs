using System.Diagnostics;

namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface IProcessManager
    {
        /// <summary>
        /// 実行中の同じアプリケーションのプロセスを取得します。
        /// </summary>
        Process? GetPreviousProcess();

        /// <summary>
        /// 指定されたプロセスのウィンドウを検索し、最前面に表示します。
        /// </summary>
        /// <param name="target">対象プロセス</param>
        /// <param name="caption">対象ウィンドウのタイトル</param>
        void WakeupWindow(Process target, string caption);
    }
}