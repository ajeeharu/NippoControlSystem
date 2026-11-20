using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.ApplicationServices.Services;
using NippoControlSystem.Domain.Interfaces;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Versioning;

namespace NippoControlSystem.UI.ViewModels
{
    [SupportedOSPlatform("windows")]
    public partial class OpeningViewModel : ObservableObject
    {
        private readonly MainMenuItem _sharedMenuInfo;
        private readonly IOperationPanelService _operationPanel;
        private readonly MenuApplicationService _menuApplicationService;
        private GeneralSettings _generalSettings;    // View から直接参照するために public に変更
        private readonly MessageBoxDialogService _messageBoxDialogService;

        private readonly IDialogService _dialogService;
        // リソース解放フラグ
        private bool _disposed = false;
        // View へのデータバインド用

        // 画面のコントロールと同期させたいプロパティ
        [ObservableProperty]
        private MenuBindDto _menuDataBinding = new()
        {
            // 追加・削除をUIに通知できる BindingList を使う
            MainMenus = new BindingList<BindMainMenuDto>()
        };
        public GeneralSettings GeneralSettings => _generalSettings;

        public OpeningViewModel(
            MainMenuItem sharedMenuInfo,
            GeneralSettings generalSettings,
            IOperationPanelService operationPanelService,
            MenuApplicationService menuApplicationService,
            IDialogService dialogService,
            MessageBoxDialogService messageBoxDialogService)
        {
            _sharedMenuInfo = sharedMenuInfo;
            _generalSettings = generalSettings;
            _operationPanel = operationPanelService;
            _menuApplicationService = menuApplicationService;
            _dialogService = dialogService;
            _messageBoxDialogService = messageBoxDialogService;

        }
        /// <summary>
        /// 初期データをCSVファイルから非同期で読み込み、画面の初期化を行います。
        /// </summary>
        private async Task LoadInitialDataFromDbAsync()
        {
            // 1. 外部パネルのLEDをOFF、電源スイッチを12Vに設定
            _operationPanel.SetInspectionLedControl(LedOnOff.LedOff);
            _operationPanel.SetGreenLedControl(LedOnOff.LedOff);
            _operationPanel.SetRedLedControl(LedOnOff.LedOff);
            _operationPanel.SetPowerSwitch12VoltControl();

            // 2. データバインディング用の値を設定
            string menuCsvPass = _generalSettings.ApplicationFolder + _generalSettings.MenuFolder + _generalSettings.MenuCsvFileName;
            MenuDataBinding = _menuApplicationService.LoadMenu(menuCsvPass);

            // 3. SharedMenuInfoDto に初期値を設定
            _sharedMenuInfo.Title = _generalSettings.MainNo;   //"仕様書番号";
            _sharedMenuInfo.SubMenus[0].SubTitle = _generalSettings.SubNo;     //"追番";

        }

        /// <summary>
        /// フォルダパスの検証および AppState への準備を行います（引数なし版）
        /// </summary>
        public bool ValidateAndPrepareFolders()
        {
            return ValidateAndPrepareFolders(out _, out _);
        }
        /// <summary>
        /// 編集設定の検証および AppState への準備を行います
        /// </summary>
        /// <returns></returns>
        public bool ValidateAndPrepareForEditSettingsView()
        {
            if (!ValidateAndPrepareFolders(out string targetFolder, out string InspectionItemFolder))
            {
                return false;
            }
            _sharedMenuInfo.SubMenus[0].Folder = targetFolder;
            return true;
        }
        /// <summary>
        /// フォルダパスの検証および AppState への準備を行い、パスを出力します
        /// </summary>
        private bool ValidateAndPrepareFolders(out string mainFolder, out string inspectionItemFolder)
        {
            mainFolder = string.Empty;
            inspectionItemFolder = string.Empty;

            if (string.IsNullOrEmpty(_sharedMenuInfo.Title) || string.IsNullOrEmpty(_sharedMenuInfo.SubMenus[0].SubTitle))
            {
                _dialogService.ShowMessage("仕様書番号または追番が選択されていません。");
                return false;
            }

            if (!TryGetSelectedFolders(out mainFolder, out inspectionItemFolder))
            {
                _dialogService.ShowMessage("選択されたSubIDが見つかりません。");
                return false;
            }

            // 1. メインフォルダの存在チェック
            if (string.IsNullOrEmpty(mainFolder) || !Directory.Exists(mainFolder))
            {
                _dialogService.ShowMessage($"検査フォルダが見つかりません。\nパス: {mainFolder}");
                return false;
            }

            // 2. 検査項目フォルダの存在チェック
            if (string.IsNullOrEmpty(inspectionItemFolder) || !Directory.Exists(inspectionItemFolder))
            {
                _dialogService.ShowMessage($"検査項目用フォルダが見つかりません。\nパス: {inspectionItemFolder}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        private bool TryGetSelectedFolders(out string targetFolder, out string inspectionItemFolder)
        {
            targetFolder = string.Empty;
            inspectionItemFolder = string.Empty;

            // MenuInfo や MainMenus が未初期化の場合は安全に弾く
            if (MenuDataBinding.MainMenus == null)
            {
                return false;
            }

            // 1. すべての MainMenu の中にある SubMenus から、SelectedSubId に一致するものを探す
            // ※もし SelectedSubId が string 型の場合は int.TryParse 等で型を合わせて比較してください
            BindSubMenuDto targetSubMenu = MenuDataBinding.MainMenus
                .SelectMany(main => main.SubMenus)
                .FirstOrDefault(sub => sub.SubId == sub.SelectedIndex); // SelectedSubIdの型に合わせて調整

            if (targetSubMenu == null)
            {
                return false;
            }

            // 2. 親のメインメニュー側からフォルダを取得する場合、または親側にもフォルダがある構造ならここから取得
            // ※今回は DataSubMenu に Folder / InspectionItemFolder が定義されているため、そこから直接取得します
            targetFolder = targetSubMenu.Folder;
            inspectionItemFolder = targetSubMenu.InspectionItemFolder;


            return true;
        }
        private void CheckFolder(string Folder)
        {
            string CheckdatFile = Folder + System.IO.Path.DirectorySeparatorChar + _generalSettings.CheckFileName;
            if (System.IO.File.Exists(CheckdatFile))
            {
                DataTable dtCheckDat = new DataSetItems.CheckDatDataTable();

                mc.loadCheckdat(dtCheckDat, CheckdatFile);
                if (dtCheckDat.Rows.Count > 0)
                {
                    if (_sharedMenuInfo.MainTitle != dtCheckDat.Rows[0]["Title"].ToString() || _sharedMenuInfo.SubTitle != dtCheckDat.Rows[0]["SubTitle"].ToString())
                    {
                        _messageBoxDialogService.ShowMessage("【重要】\n\n選択した仕様書番号または追番が保存データと違っています。\n\nフォルダが、別の検査のものと重複している可能性があります。確認してください。", _generalSettings.ApplicationName);
                    }
                }
                else
                {
                    //中身がないので、CheckdatFileを新規作成する
                    mc.newCheckdatFile(CheckdatFile, "未設定", MainTitle, SubTitle, "");
                }
            }
            else
            {
                //ファイルがないので、CheckdatFileを新規作成する
                mc.newCheckdatFile(CheckdatFile, "未設定", MainTitle, SubTitle, "");
            }
        }
        // ① 画面読み込み時に実行したい処理を [RelayCommand] 化する
        [RelayCommand]
        private async Task InitializeAsync()
        {
            // 画面ロード時に行いたい非同期処理（データ取得など）
            await LoadInitialDataFromDbAsync();
        }
        /// <summary>
        /// 操作マニュアル（PDF 等）を外部プロセスで開きます。
        /// </summary>
        public void OpenManual()
        {
            string filePath = _generalSettings.ApplicationFolder + _generalSettings.ManualPath;
            string option = "";

            try
            {
                // 外部プロセスとしてマニュアルを起動
                System.Diagnostics.Process.Start(filePath, option);
            }
            catch (Exception ex)
            {
                // ユーザーへの通知（※メッセージ表示サービス等を経由するとより理想的です）
                _messageBoxDialogService.ShowMessage(
                    $"操作マニュアルを表示できません。\n\n表示ファイル：{filePath}\n理由：{ex.Message}",
                    _generalSettings.ApplicationName);
            }
        }
        private string _filterText = string.Empty;

        /// <summary>
        /// メニューフィルター用文字列
        /// </summary>
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                }
            }
        }
        /// <summary>
        /// 現在のフィルター文字列に基づいて BindingSource にフィルターを適用します。
        /// </summary>
        /// <param name="bindingSource">対象の BindingSource</param>
        public void ApplyMenuFilter(BindingSource bindingSource)
        {
            if (bindingSource == null) return;

            if (string.IsNullOrEmpty(FilterText))
            {
                bindingSource.Filter = null;
            }
            else
            {
                // シングルクォーテーションのエスケープ（''に置換）を行って安全に前方一致を設定
                string safeText = FilterText.Replace("'", "''");
                bindingSource.Filter = $"Title Like '{safeText}*'";
            }
        }

        /// <summary>
        /// 選択された SubID に紐づくフォルダパスを取得し、アナログ入出力モニターの外部プロセスを起動します。
        /// </summary>
        /// <param name="selectedSubId">選択中の SubID</param>
        /// <returns>SubID が存在し、モニターの起動処理に成功した場合は true。見つからなかった場合は false</returns>
        public bool LaunchAnalogAioMonitor(object? selectedSubId)
        {
            if (selectedSubId == null) return false;

            // DataSet から対象の SubID の行を検索
            DataTable dtSubMain = mc.DataSetTopMenu.menuSub;
            DataRow[] dtSubRows = dtSubMain.Select($"SubID='{selectedSubId}'");

            if (dtSubRows == null || dtSubRows.Length == 0)
            {
                return false;
            }

            DataRow founddtSubRow = dtSubRows[0];
            string itemFolder = founddtSubRow["Folder"]?.ToString() ?? string.Empty;

            // アナログ I/O モニターの起動情報を作成
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = _generalSettings.AioMonitorExePath,
                Arguments = $"-d \"{itemFolder}\""
            };

            System.Diagnostics.Process.Start(psi);
            return true;
        }
        /// <summary>
        /// folder の存在チェックと選択値の妥当性を検証し、フォルダパスを取得します。
        /// </summary>
        private bool ValidateAndGetFolder(out string? folder)
        {
            folder = null;

            if (_sharedMenuInfo.MainId == BindSubMenuDto.Unselected || _sharedMenuInfo.SubId == BindSubMenuDto.Unselected)
            {
                _navigationService.ShowMessageBox("仕様書番号または追番が選択されていません。\n\n仕様書番号または追番を選択してください。");
                return false;
            }

            if (DataSetTopMenu?.Tables["menuSub"] == null) return false;

            DataRow[]? dtSubRows = DataSetTopMenu.Tables["menuSub"]?.Select($"SubID='{_sharedMenuInfo.SelectedSubId}'");
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
        /// <summary>
        /// 画面クローズ時に OpeningView.cs の OnFormClosed から呼び出されるクリーンアップ処理
        /// </summary>
        public void CleanupResources()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 実際のリソース解放ロジック
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // 1. イベントハンドラーの購読解除（メモリーリーク防止）
                // 例: _appState.StateChanged -= OnAppStateChanged;

                // 2. ViewModel 固有の IDisposable オブジェクトの破棄
                // 例: _timer?.Dispose();
                // 例: _previewImage?.Dispose();

                // 3. アプリケーション状態（AppState）の整理（必要に応じて）
                // ※ AppState 自体は Singleton のため破棄してはいけませんが、
                //   一時的なフィルタ用テキスト等を初期化したい場合はここで行います。
                // _appState.FilterText = string.Empty;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            CleanupResources();
        }
        /// <summary>
        /// メインメニューの選択変更時に、Viewからデータを受け取るメソッド
        /// </summary>
        public void UpdateMainMenuSelection(int selectedIndex, int mainId)
        {
            if (MenuDataBinding?.MainMenus == null) return;

            // 1. すべてのメインメニューの選択状態を更新（選ばれたもの以外は未選択(-1)にする）
            for (int i = 0; i < MenuDataBinding.MainMenus.Count; i++)
            {
                MenuDataBinding.MainMenus[i].SelectedIndex = (i == selectedIndex) ? selectedIndex : -1;
            }

            // 2. 未選択（解除）された場合の処理
            if (selectedIndex < 0 || mainId == BindMainMenuDto.Unselected)
            {
                _sharedMenuInfo.MainId = BindMainMenuDto.Unselected;
                _sharedMenuInfo.MainTitle = string.Empty;

                // 必要であればサブメニュー側の情報もここでクリアする
                // _sharedMenuInfo.SubId = BindSubMenuDto.Unselected;
                // _sharedMenuInfo.SubTitle = string.Empty;
                return;
            }

            // 3. 選択されたメインメニューのオブジェクトを特定して _sharedMenuInfo に設定する
            var selectedMenu = MenuDataBinding.MainMenus.FirstOrDefault(m => m.MainId == mainId);
            if (selectedMenu != null)
            {
                _sharedMenuInfo.MainId = selectedMenu.MainId;       //
                _sharedMenuInfo.MainTitle = selectedMenu.MainTitle; //[cite: 2]
            }
        }

        /// <summary>
        /// サブメニューの選択変更時に、Viewから呼び出されるメソッド
        /// </summary>
        /// <param name="parentMainId">親メニューのID</param>
        /// <param name="selectedIndex">サブメニューの選択インデックス</param>
        /// <param name="selectedSub">選択されたサブメニューオブジェクト (選択解除時はnull)</param>
        public void UpdateSubMenuSelection(int parentMainId, int selectedIndex, BindSubMenuDto? selectedSub)
        {
            // 1. 該当するメインメニューを取得（MenuBindDto の MainMenus から探す）
            var parentMenu = MenuDataBinding?.MainMenus?.FirstOrDefault(m => m.MainId == parentMainId);
            if (parentMenu == null || parentMenu.SubMenus == null) return;

            // 2. 各サブメニューの選択状態（SelectedIndexなど）を更新する
            for (int i = 0; i < parentMenu.SubMenus.Count; i++)
            {
                // 選択された行ならそのインデックス、違えば未選択を表す定数などを設定
                parentMenu.SubMenus[i].SelectedIndex = (i == selectedIndex) ? selectedIndex : BindSubMenuDto.Unselected;
            }

            // 3. 選択解除の場合（未選択状態）のクリア処理
            if (selectedIndex < 0 || selectedSub == null)
            {
                _sharedMenuInfo.SubId = BindSubMenuDto.Unselected;
                _sharedMenuInfo.SubTitle = string.Empty;
                _sharedMenuInfo.Folder = string.Empty;
                _sharedMenuInfo.InspectionItemFolder = string.Empty;
                return;
            }

            // 4. 選択されたサブメニューの情報を SharedMenuInfo へ反映
            _sharedMenuInfo.SubId = selectedSub.SubId;
            _sharedMenuInfo.SubTitle = selectedSub.SubTitle;

            // フォルダパスが null の場合は空文字にフォールバック
            _sharedMenuInfo.Folder = selectedSub.Folder ?? string.Empty;
            _sharedMenuInfo.InspectionItemFolder = selectedSub.InspectionItemFolder ?? string.Empty;
        }
    }
}