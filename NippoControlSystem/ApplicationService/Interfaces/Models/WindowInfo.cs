namespace NippoControlSystem.ApplicationService.Interfaces.Models
{
    /// <summary>
    /// ウィンドウ情報を表すDTO / ドメインモデル
    /// </summary>
    public class WindowInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
}