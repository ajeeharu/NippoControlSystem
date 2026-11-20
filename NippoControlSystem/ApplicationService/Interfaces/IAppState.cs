namespace NippoControlSystem.ApplicationService.Interfaces
{
    using MainMenu = Confiigurations.MainMenu;

    public interface IAppState
    {
        List<MainMenu> MainMenus { get; set; }
        string? CurrentFolder { get; set; }
        string? SelectedMainTitle { get; set; }
        string? SelectedSubTitle { get; set; }
        int? SelectedSubId { get; set; }
        int SelectedMainIndex { get; set; }
        int SelectedSubIndex { get; set; }

        /// <summary>
        /// 現在選択中のSubIdからフォルダーパスの検証・取得を行います。
        /// </summary>
        bool TryGetSelectedFolder(out string? folderPath);
        /// <summary>
        /// 選択中のSubIdから検査項目用フォルダパスを取得します。
        /// </summary>
        bool TryGetSelectedInspectionItemFolder(out string? inspectionItemFolderPath);
    }
}