namespace NippoControlSystem.ApplicationService.Dtos
{
    public class MenuInfoSettings
    {
        public List<DataMainMenu> MainMenus { get; set; } = new();

    }
    public class DataMainMenu
    {
        public int MainId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<DataSubMenu> SubMenus { get; set; } = new();
    }
    public class DataSubMenu
    {
        public int MainId { get; set; }
        public int SubId { get; set; }
        public string SubTitle { get; set; } = string.Empty;
        /// <summary>
        /// 実行用・結果出力用のフォルダパス
        /// </summary>
        public string? Folder { get; set; }

        /// <summary>
        /// ★追加：検査項目用（定義ファイル等）のフォルダパス
        /// </summary>
        public string? InspectionItemFolder { get; set; }
    }
}