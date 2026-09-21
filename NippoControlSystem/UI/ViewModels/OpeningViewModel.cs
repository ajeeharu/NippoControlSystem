using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.UI.Views;

namespace NippoControlSystem.UI.ViewModels
{
    public class OpeningViewModel(INavigationService navigationService) : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService = navigationService;

        /// <summary>
        /// 親画面の参照（Hide/Show制御用）
        /// </summary>
        public Form? CurrentView { get; set; }

        public string? SelectedMainTitle { get; set; }
        public string? SelectedSubTitle { get; set; }
        public string? SelectedSubId { get; set; }
        public DataSet? DataSetTopMenu { get; set; }

        // --- 画面遷移のコマンド/メソッド ---

        public void OpenSettingView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            _navigationService.NavigateAndHideOwner<SettingView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
            });
        }

        public void OpenMainView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            _navigationService.NavigateAndHideOwner<MainView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
            });
        }

        public void OpenHistoryView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            string? historyFile = _navigationService.ShowOpenFileDialog(folder, "検査結果ファイル(A*.csv)|A*.csv|すべてのファイル(*.*)|*.*");
            if (string.IsNullOrEmpty(historyFile) || !File.Exists(historyFile)) return;

            _navigationService.NavigateAndHideOwner<TestHistoryView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
                view.historyFile = historyFile;
            });
        }

        public void OpenVersionView()
        {
            _navigationService.NavigateTo<VersionView>(isModal: true);
        }

        private bool ValidateAndGetFolder(out string? folder)
        {
            folder = null;

            if (string.IsNullOrEmpty(SelectedMainTitle) || string.IsNullOrEmpty(SelectedSubTitle))
            {
                _navigationService.ShowMessageBox("仕様書番号または追番が選択されていません。\n\n仕様書番号または追番を選択してください。");
                return false;
            }

            if (DataSetTopMenu?.Tables["menuSub"] == null) return false;

            DataRow[]? dtSubRows = DataSetTopMenu.Tables["menuSub"]?.Select($"SubID='{SelectedSubId}'");
            if (dtSubRows?.Length != 1)
            {
                _navigationService.ShowMessageBox("選択されたSubIDが見つかりません。");
                return false;
            }

            folder = dtSubRows[0]["Folder"].ToString() ?? string.Empty;
            if (!Directory.Exists(folder))
            {
                _navigationService.ShowMessageBox("検査フォルダが見つかりません。");
                return false;
            }

            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}