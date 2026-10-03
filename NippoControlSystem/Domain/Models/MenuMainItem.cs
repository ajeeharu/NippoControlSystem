namespace NippoControlSystem.Domain.Models
{
    /// <summary>
    /// メインメニュー設定モデル
    /// </summary>
    public class MenuMainItem
    {
        public int MainID { get; set; }
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// メインメニューに紐づくサブメニュー一覧
        /// </summary>
        public List<MenuSubItem> SubMenus { get; set; } = new();
    }
}