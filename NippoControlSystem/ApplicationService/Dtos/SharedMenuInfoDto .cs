/// <summary> View間の共通情報を保持するためのDTO </summary>
namespace NippoControlSystem.ApplicationService.Dtos
{
    /// <summary>
    /// View間で共有するメニュー情報全体のルート
    /// </summary>
    public class MainMenuList
    {
        public const int Unselected = -1;
        public List<MainMenuItem> MainMenus { get; set; } = new();
    }

    /// <summary>
    /// メインメニュー（旧 menuMain テーブルに対応）
    /// </summary>
    public class MainMenuItem
    {
        public int MainID { get; set; } = MainMenuList.Unselected;
        public string Title { get; set; } = string.Empty;

        // 親子関係を表現する子メニューのリスト
        public List<SubMenuItem> SubMenus { get; set; } = new();
    }

    /// <summary>
    /// サブメニュー（旧 menuSub テーブルに対応）
    /// </summary>
    public class SubMenuItem
    {
        public int MainID { get; set; } = MainMenuList.Unselected;
        public int SubID { get; set; } = MainMenuList.Unselected;
        public string SubTitle { get; set; } = string.Empty;
        public string? Folder { get; set; }
    }
}