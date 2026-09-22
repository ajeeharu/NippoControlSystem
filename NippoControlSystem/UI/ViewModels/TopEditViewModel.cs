using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// メニュー選択モード (図番/メイン vs 枝番/サブ)
    /// </summary>
    public enum EditTargetMode
    {
        Main,
        Sub
    }

    /// <summary>
    /// トップメニュー編集画面（TopEditView）用 ViewModel
    /// </summary>
    public class TopEditViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        private DataSetTopMenu _dataSetTopMenu;
        private EditTargetMode _targetMode = EditTargetMode.Main;

        private string _lblMainNoText;
        private string _lblSubNoText;

        private int _selectedMainRowIndex = -1;
        private int _selectedSubRowIndex = -1;
        #endregion

        #region Properties
        /// <summary>
        /// トップメニューデータセット
        /// </summary>
        public DataSetTopMenu DataSetTopMenu
        {
            get => _dataSetTopMenu;
            set { if (_dataSetTopMenu != value) { _dataSetTopMenu = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 現在の操作対象（図番 / 枝番）
        /// </summary>
        public EditTargetMode TargetMode
        {
            get => _targetMode;
            set { if (_targetMode != value) { _targetMode = value; OnPropertyChanged(); } }
        }

        public string LblMainNoText
        {
            get => _lblMainNoText;
            private set { if (_lblMainNoText != value) { _lblMainNoText = value; OnPropertyChanged(); } }
        }

        public string LblSubNoText
        {
            get => _lblSubNoText;
            private set { if (_lblSubNoText != value) { _lblSubNoText = value; OnPropertyChanged(); } }
        }

        public int SelectedMainRowIndex
        {
            get => _selectedMainRowIndex;
            set { if (_selectedMainRowIndex != value) { _selectedMainRowIndex = value; OnPropertyChanged(); } }
        }

        public int SelectedSubRowIndex
        {
            get => _selectedSubRowIndex;
            set { if (_selectedSubRowIndex != value) { _selectedSubRowIndex = value; OnPropertyChanged(); } }
        }
        #endregion

        #region UI Request Events
        /// <summary>
        /// メッセージボックス表示要求
        /// </summary>
        public event Action<string, string, MessageBoxButtons, MessageBoxIcon, Action<DialogResult>> RequestShowDialog;

        /// <summary>
        /// フォルダ選択ダイアログ要求 (説明文字, 初期パス, 結果コールバック)
        /// </summary>
        public event Action<string, string, Action<string>> RequestSelectFolder;

        /// <summary>
        /// ファイル開くダイアログ要求 (フィルター, 初期パス, 結果ファイルパスコールバック)
        /// </summary>
        public event Action<string, string, Action<string>> RequestOpenFileDialog;

        /// <summary>
        /// ファイル保存ダイアログ要求 (フィルター, 初期パス, 結果ファイルパスコールバック)
        /// </summary>
        public event Action<string, string, Action<string>> RequestSaveFileDialog;

        /// <summary>
        /// 子画面（編集/新規入力画面）表示要求
        /// </summary>
        public event Action<string, string, string, Action<string, string, string>> RequestShowInputEditDialog;

        /// <summary>
        /// 子画面（複製画面）表示要求
        /// </summary>
        public event Action<string, string, string, Action<string, string, bool>> RequestShowCopyDialog;

        /// <summary>
        /// 画面を閉じる要求
        /// </summary>
        public event Action RequestCloseView;

        /// <summary>
        /// DataGridViewのグリッド再描画要求
        /// </summary>
        public event Action RequestRefreshGrid;
        #endregion

        #region Constructors
        public TopEditViewModel()
        {
        }

        public TopEditViewModel(DataSetTopMenu dataSet)
        {
            DataSetTopMenu = dataSet;
        }
        #endregion

        #region Initialization & Form Events
        public void InitializeData()
        {
            LblMainNoText = _defaultSettings.MainNo;
            LblSubNoText = $"{_defaultSettings.SubNo}とフォルダ";
            TargetMode = EditTargetMode.Main;
        }

        public void HandleFormClosing()
        {
            // キャンセル終了時等のため変更を破棄
            DataSetTopMenu?.RejectChanges();
        }
        #endregion

        #region Operations & Business Logic

        #region Save & Close
        public void SaveAndClose()
        {
            if (DataSetTopMenu != null)
            {
                DataSetTopMenu.AcceptChanges();
                string menuCsvPath = Path.Combine(_defaultSettings.ApplicationFloder, _defaultSettings.MenuFolder, _defaultSettings.Menucsv);
                _mc.saveMenuCsv(DataSetTopMenu, menuCsvPath);
            }
            RequestCloseView?.Invoke();
        }
        #endregion

        #region CRUD Operations (Insert, Edit, Delete, Copy)
        public void EditCurrentRow(DataRow mainRow, DataRow subRow)
        {
            if (mainRow == null) return;

            // 枝番がない場合は新規挿入を実行
            if (subRow == null)
            {
                InsertRow(mainRow, subRow);
                return;
            }

            TargetMode = EditTargetMode.Sub;

            string mainTitle = mainRow["Title"]?.ToString() ?? "";
            string subTitle = subRow["SubTitle"]?.ToString() ?? "";
            string folder = subRow["Folder"]?.ToString() ?? "";

            RequestShowInputEditDialog?.Invoke(mainTitle, subTitle, folder, (newMainTitle, newSubTitle, newFolder) =>
            {
                if (mainTitle != newMainTitle) mainRow["Title"] = newMainTitle;
                if (subTitle != newSubTitle) subRow["SubTitle"] = newSubTitle;
                if (folder != newFolder) subRow["Folder"] = newFolder;

                RequestRefreshGrid?.Invoke();
            });
        }

        public void InsertRow(DataRow mainRow, DataRow subRow)
        {
            if (DataSetTopMenu == null) return;

            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSub = DataSetTopMenu.menuSub;

            if (TargetMode == EditTargetMode.Main)
            {
                DataRow dtMainRowNew = dtMain.NewRow();
                int newMainID;

                if (mainRow != null)
                {
                    int index = dtMain.Rows.IndexOf(mainRow);
                    dtMain.Rows.InsertAt(dtMainRowNew, index + 1);
                    newMainID = Convert.ToInt32(dtMain.Rows[index + 1]["MainID"]);
                }
                else
                {
                    dtMain.Rows.Add(dtMainRowNew);
                    newMainID = Convert.ToInt32(dtMainRowNew["MainID"]);
                }
                dtMain.AcceptChanges();

                DataRow dtSubRowNew = dtSub.NewRow();
                dtSubRowNew["MainID"] = newMainID;
                dtSubRowNew["SubTitle"] = "";
                dtSubRowNew["Folder"] = "";
                dtSub.Rows.Add(dtSubRowNew);
            }
            else
            {
                if (mainRow == null) return;

                DataRow dtSubRowNew = dtSub.NewRow();
                dtSubRowNew["MainID"] = mainRow["MainID"];
                dtSubRowNew["SubTitle"] = "";
                dtSubRowNew["Folder"] = "";

                if (subRow == null)
                {
                    dtSub.Rows.Add(dtSubRowNew);
                }
                else
                {
                    int subID = Convert.ToInt32(subRow["SubID"]);
                    DataRow[] rows = dtSub.Select($"SubID='{subID}'");
                    if (rows.Length > 0)
                    {
                        int index = dtSub.Rows.IndexOf(rows[0]);
                        if (dtSub.Rows.Count - 1 == index)
                        {
                            dtSub.Rows.Add(dtSubRowNew);
                        }
                        else
                        {
                            dtSub.Rows.InsertAt(dtSubRowNew, index + 1);
                        }
                    }
                }
            }
        }

        public void DeleteRow(DataRow mainRow, DataRow subRow, int selectedCount)
        {
            if (selectedCount > 1)
            {
                ShowMessageBox("複数行の削除はできません", _defaultSettings.ApplicationName);
                return;
            }

            if (DataSetTopMenu == null) return;

            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSub = DataSetTopMenu.menuSub;

            if (TargetMode == EditTargetMode.Main)
            {
                if (mainRow == null) return;

                int mainID = Convert.ToInt32(mainRow["MainID"]);
                mainRow.Delete();

                foreach (DataRow childRow in dtSub.Select($"MainID='{mainID}'"))
                {
                    childRow.Delete();
                }
            }
            else
            {
                if (subRow == null) return;
                subRow.Delete();
            }
        }

        public void CopyRow(DataRow mainRow, DataRow subRow)
        {
            if (mainRow == null || subRow == null)
            {
                ShowMessageBox("仕様書番号と追番を指定して下さい。", _defaultSettings.ApplicationName);
                return;
            }

            string mainTitle = mainRow["Title"]?.ToString();
            string subTitle = subRow["SubTitle"]?.ToString();
            string folder = subRow["Folder"]?.ToString();

            if (string.IsNullOrEmpty(mainTitle) || string.IsNullOrEmpty(subTitle) || string.IsNullOrEmpty(folder))
            {
                ShowMessageBox("仕様書番号と追番を指定して下さい。", _defaultSettings.ApplicationName);
                return;
            }

            DataTable dtSub = DataSetTopMenu.menuSub;
            DataRow dtSubRowNew = dtSub.NewRow();
            dtSubRowNew["MainID"] = mainRow["MainID"];
            dtSubRowNew["SubTitle"] = "";
            dtSubRowNew["Folder"] = "";

            int subID = Convert.ToInt32(subRow["SubID"]);
            DataRow[] rows = dtSub.Select($"SubID='{subID}'");
            if (rows.Length > 0)
            {
                int index = dtSub.Rows.IndexOf(rows[0]);
                if (index == -1) dtSub.Rows.Add(dtSubRowNew);
                else dtSub.Rows.InsertAt(dtSubRowNew, index + 1);
            }

            TargetMode = EditTargetMode.Sub;

            RequestShowCopyDialog?.Invoke(mainTitle, subTitle, folder, (newSubTitle, newFolder, isOk) =>
            {
                if (isOk)
                {
                    dtSubRowNew["SubTitle"] = newSubTitle;
                    dtSubRowNew["Folder"] = newFolder;
                }
                RequestRefreshGrid?.Invoke();
            });
        }
        #endregion

        #region Move Up / Down
        public void MoveUp(DataRow mainRow, int currentSubIndex)
        {
            if (DataSetTopMenu == null) return;

            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSub = DataSetTopMenu.menuSub;

            if (TargetMode == EditTargetMode.Main)
            {
                if (mainRow == null) return;
                int index = dtMain.Rows.IndexOf(mainRow);
                if (index > 0)
                {
                    int mainID = Convert.ToInt32(mainRow["MainID"]);
                    DataRow newRow = dtMain.NewRow();
                    newRow["Title"] = mainRow["Title"];
                    dtMain.Rows.InsertAt(newRow, index - 1);

                    int newMainID = Convert.ToInt32(dtMain.Rows[index - 1]["MainID"]);
                    mainRow.Delete();

                    foreach (DataRow child in dtSub.Select($"MainID='{mainID}'"))
                    {
                        child["MainID"] = newMainID;
                    }
                }
            }
            else
            {
                if (mainRow == null || currentSubIndex <= 0) return;

                int mainID = Convert.ToInt32(mainRow["MainID"]);
                DataRow[] childRows = dtSub.Select($"MainID='{mainID}'");

                if (currentSubIndex < childRows.Length)
                {
                    object[] itemArray1 = childRows[currentSubIndex].ItemArray;
                    object[] itemArray2 = childRows[currentSubIndex - 1].ItemArray;

                    for (int i = 2; i < itemArray1.Length; i++)
                    {
                        childRows[currentSubIndex][i] = itemArray2[i];
                        childRows[currentSubIndex - 1][i] = itemArray1[i];
                    }
                }
            }
        }

        public void MoveDown(DataRow mainRow, int currentSubIndex, int totalSubCount)
        {
            if (DataSetTopMenu == null) return;

            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSub = DataSetTopMenu.menuSub;

            if (TargetMode == EditTargetMode.Main)
            {
                if (mainRow == null) return;
                int index = dtMain.Rows.IndexOf(mainRow);
                if (index >= 0 && index + 1 < dtMain.Rows.Count)
                {
                    int mainID = Convert.ToInt32(mainRow["MainID"]);
                    DataRow newRow = dtMain.NewRow();
                    newRow["Title"] = mainRow["Title"];
                    dtMain.Rows.InsertAt(newRow, index + 2);

                    int newMainID = Convert.ToInt32(dtMain.Rows[index + 2]["MainID"]);
                    mainRow.Delete();

                    foreach (DataRow child in dtSub.Select($"MainID='{mainID}'"))
                    {
                        child["MainID"] = newMainID;
                    }
                }
            }
            else
            {
                if (mainRow == null || currentSubIndex < 0 || currentSubIndex >= totalSubCount - 1) return;

                int mainID = Convert.ToInt32(mainRow["MainID"]);
                DataRow[] childRows = dtSub.Select($"MainID='{mainID}'");

                if (currentSubIndex + 1 < childRows.Length)
                {
                    object[] itemArray1 = childRows[currentSubIndex].ItemArray;
                    object[] itemArray2 = childRows[currentSubIndex + 1].ItemArray;

                    for (int i = 2; i < itemArray1.Length; i++)
                    {
                        childRows[currentSubIndex][i] = itemArray2[i];
                        childRows[currentSubIndex + 1][i] = itemArray1[i];
                    }
                }
            }
        }
        #endregion

        #region File & Folder Commands
        public void LoadFromFolder(bool clearData)
        {
            RequestSelectFolder?.Invoke("フォルダを指定してください。", _defaultSettings.DataFolder, selectedPath =>
            {
                if (string.IsNullOrEmpty(selectedPath) || DataSetTopMenu == null) return;

                if (clearData)
                {
                    DataSetTopMenu.menuMain.Rows.Clear();
                    DataSetTopMenu.menuSub.Rows.Clear();
                }

                _mc.topMenuFromFolder(DataSetTopMenu, selectedPath);
                RequestRefreshGrid?.Invoke();
            });
        }

        public void OpenMenuCsv()
        {
            string filter = "hcmファイル(*.hcm;*.csv)|*.hcm;*.csv|すべてのファイル(*.*)|*.*";
            string initDir = Path.Combine(_defaultSettings.ApplicationFloder, _defaultSettings.MenuFolder);

            RequestOpenFileDialog?.Invoke(filter, initDir, filePath =>
            {
                if (string.IsNullOrEmpty(filePath) || DataSetTopMenu == null) return;

                _mc.loadMenuCsv(DataSetTopMenu, filePath);
                RequestRefreshGrid?.Invoke();
            });
        }

        public void SaveMenuCsv()
        {
            string filter = "hcmファイル(*.hcm;*.csv)|*.hcm;*.csv|すべてのファイル(*.*)|*.*";
            string initDir = Path.Combine(_defaultSettings.ApplicationFloder, _defaultSettings.MenuFolder);

            RequestSaveFileDialog?.Invoke(filter, initDir, filePath =>
            {
                if (string.IsNullOrEmpty(filePath) || DataSetTopMenu == null) return;

                _mc.saveMenuCsv(DataSetTopMenu, filePath);
            });
        }
        #endregion

        #region Format Conversion Logic (Old Version / DB Conversion)
        public void ConvertToOldVerSingle(DataRow mainRow, DataRow subRow)
        {
            if (mainRow == null || subRow == null) return;

            string mainTitle = mainRow["Title"]?.ToString();
            string subTitle = subRow["SubTitle"]?.ToString();
            string folder = subRow["Folder"]?.ToString();

            GetSaveOldVerFolder(outFolder =>
            {
                if (string.IsNullOrEmpty(outFolder)) return;

                if (ConvToOldVer(mainTitle, subTitle, folder, outFolder))
                {
                    ShowMessageBox("変換は正常に終了しました。", _defaultSettings.ApplicationName);
                }
                else
                {
                    string logFile = Path.Combine(outFolder, $"conv{DateTime.Now:yyyyMMdd}.log");
                    ShowMessageBox($"変換エラーがありました。変換ログを確認してください。\n{logFile}", _defaultSettings.ApplicationName);
                }
            });
        }

        public void ConvertToOldVerAll()
        {
            if (DataSetTopMenu == null) return;

            GetSaveOldVerFolder(outFolder =>
            {
                if (string.IsNullOrEmpty(outFolder)) return;

                bool isSuccess = true;
                foreach (DataRow mainRow in DataSetTopMenu.menuMain)
                {
                    foreach (DataRow subRow in mainRow.GetChildRows("menuMain_menuSub"))
                    {
                        string mainTitle = mainRow["Title"]?.ToString();
                        string subTitle = subRow["SubTitle"]?.ToString();
                        string folder = subRow["Folder"]?.ToString();

                        isSuccess &= ConvToOldVer(mainTitle, subTitle, folder, outFolder);
                    }
                }

                if (isSuccess)
                {
                    ShowMessageBox("変換は正常に終了しました。", _defaultSettings.ApplicationName);
                }
                else
                {
                    string logFile = Path.Combine(outFolder, $"conv{DateTime.Now:yyyyMMdd}.log");
                    ShowMessageBox($"変換エラーがありました。変換ログを確認してください。\n{logFile}", _defaultSettings.ApplicationName);
                }
            });
        }

        public void ExecuteDbiDhConversion(DataRow subRow, bool isDhiDb)
        {
            if (subRow == null) return;

            string folder = subRow["Folder"]?.ToString();
            string listDatFile = Path.Combine(folder, _defaultSettings.Listdat);

            if (!File.Exists(listDatFile))
            {
                ShowMessageBox($"List.datファイルが見つかりません。({listDatFile})", _defaultSettings.ApplicationName);
                return;
            }

            bool result = isDhiDb ? _mc.executeiDhiDb(listDatFile) : _mc.executeiDbiDh(listDatFile);

            if (result)
            {
                ShowMessageBox("変換は正常に終了しました。", _defaultSettings.ApplicationName);
            }
            else
            {
                ShowMessageBox("変換できませんでした。\n該当の項目がないか、ファイルが存在しません", _defaultSettings.ApplicationName);
            }
        }

        private void GetSaveOldVerFolder(Action<string> onFolderSelected)
        {
            string desc = $"フォルダを指定してください。({_defaultSettings.MainNo}/{_defaultSettings.SubNo}は自動的に付加されます)";
            RequestSelectFolder?.Invoke(desc, _defaultSettings.DataFolder, onFolderSelected);
        }

        private bool ConvToOldVer(string mainTitle, string subTitle, string folder, string outFolder)
        {
            string errMsg = FolderCheck(mainTitle, subTitle, folder, outFolder);
            if (errMsg != "") return FalseAndLog(mainTitle, subTitle, outFolder, errMsg);

            string outFullPass = Path.Combine(outFolder, mainTitle, subTitle) + Path.DirectorySeparatorChar;
            string checkDatFile = Path.Combine(folder, _defaultSettings.Checkdat);
            string listDatFile = Path.Combine(folder, _defaultSettings.Listdat);
            string portDatFile = Path.Combine(folder, _defaultSettings.Portdat);

            errMsg = _mc.convToOldVerCheckdat(checkDatFile, outFullPass + _defaultSettings.Checkdat);
            if (!string.IsNullOrEmpty(errMsg)) return FalseAndLog(mainTitle, subTitle, outFolder, errMsg);

            File.Move(outFullPass + _defaultSettings.Checkdat + _mc.Part, outFullPass + _defaultSettings.Checkdat);

            File.Copy(portDatFile, outFullPass + _defaultSettings.Portdat, true);

            errMsg = _mc.convToOldVerListdat(listDatFile, outFullPass + _defaultSettings.Listdat);
            if (!string.IsNullOrEmpty(errMsg)) return FalseAndLog(mainTitle, subTitle, outFolder, errMsg);

            File.Move(outFullPass + _defaultSettings.Listdat + _mc.Part, outFullPass + _defaultSettings.Listdat);

            return true;
        }

        private bool FalseAndLog(string mainTitle, string subTitle, string outFolder, string errMsg)
        {
            string logFile = Path.Combine(outFolder, $"conv{DateTime.Now:yyyyMMdd}.log");
            using (var sw = new StreamWriter(logFile, true, _mc.enc))
            {
                sw.WriteLine($"{mainTitle}-{subTitle} NG");
                sw.Write(errMsg);
            }
            return false;
        }

        private string FolderCheck(string mainTitle, string subTitle, string folder, string outFolder)
        {
            string outFullPass = Path.Combine(outFolder, mainTitle, subTitle) + Path.DirectorySeparatorChar;

            string checkDatFile = Path.Combine(folder, _defaultSettings.Checkdat);
            string listDatFile = Path.Combine(folder, _defaultSettings.Listdat);
            string portDatFile = Path.Combine(folder, _defaultSettings.Portdat);

            if (!File.Exists(checkDatFile)) return $"NoFile - Check.dat ({checkDatFile})";
            if (!File.Exists(listDatFile)) return $"NoFile - List.dat ({listDatFile})";
            if (!File.Exists(portDatFile)) return $"NoFile - Port.dat ({portDatFile})";

            Directory.CreateDirectory(outFullPass);

            if (File.Exists(outFullPass + _defaultSettings.Checkdat)) File.Delete(outFullPass + _defaultSettings.Checkdat);
            if (File.Exists(outFullPass + _defaultSettings.Listdat)) File.Delete(outFullPass + _defaultSettings.Listdat);
            if (File.Exists(outFullPass + _defaultSettings.Portdat)) File.Delete(outFullPass + _defaultSettings.Portdat);

            return "";
        }

        private void ShowMessageBox(string message, string title)
        {
            RequestShowDialog?.Invoke(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, null);
        }
        #endregion

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}