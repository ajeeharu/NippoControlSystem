using System.ComponentModel;

namespace NippoControlSystem.ApplicationService.Dtos
{
    /// <summary> View-ViewModel間のバインディング用メニュー情報を保持するDTO </summary>
    public class MenuBindDto
    {
        public BindingList<BindMainMenuDto> MainMenus { get; set; } = new();
    }

    public class BindMainMenuDto
    {
        /// <summary> 未選択状態を表す定数 </summary>
        public const int Unselected = -1;

        public int SelectedIndex { get; set; } = Unselected;
        public int MainId { get; set; }
        public string MainTitle { get; set; } = string.Empty;
        public List<BindSubMenuDto> SubMenus { get; set; } = new();
    }

    public class BindSubMenuDto
    {
        public const int Unselected = BindMainMenuDto.Unselected;
        public int SelectedIndex { get; set; } = Unselected;
        public int MainId { get; set; }
        public int SubId { get; set; }
        public string SubTitle { get; set; } = string.Empty;

        /// <summary> 実行用・結果出力用のフォルダパス </summary>
        public string? Folder { get; set; }

        /// <summary> 検査項目用（定義ファイル等）のフォルダパス </summary>
        public string? InspectionItemFolder { get; set; }
    }
}