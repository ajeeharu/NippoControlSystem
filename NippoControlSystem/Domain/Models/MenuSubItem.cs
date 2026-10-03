namespace NippoControlSystem.Domain.Models
{
    /// <summary>
    /// サブメニュー設定モデル
    /// </summary>
    public class MenuSubItem
    {
        public int SubID { get; set; }
        public string SubTitle { get; set; } = string.Empty;
        public string Folder { get; set; } = string.Empty;
    }
}