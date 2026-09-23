namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface IWindowService
    {
        /// <summary>
        /// 指定されたタイトルを持つウィンドウが存在するか確認します。
        /// </summary>
        bool IsWindowOpen(string? windowName);

        /// <summary>
        /// 指定されたタイトルを持つウィンドウのハンドルを取得します。
        /// </summary>
        IntPtr GetWindowHandle(string? windowName);

        /// <summary>
        /// 指定されたタイトルを持つウィンドウのハンドルを取得します。
        /// </summary>
        IntPtr FindWindow(string? lpClassName, string? lpWindowName);
    }
}