namespace NippoControlSystem.Domain.Models
{
    /// <summary>
    /// トップメニュー設定全体を保持するコンテナ
    /// </summary>
    public class TopMenuConfigContainer
    {
        public List<MenuMainItem> MainMenus { get; set; } = new();
    }
}