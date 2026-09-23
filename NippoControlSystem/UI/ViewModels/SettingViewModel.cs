using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 検査設定画面（SettingView）用 ViewModel
    /// </summary>
    public class SettingViewModel : INotifyPropertyChanged
    {
        #region Win32 API Imports (メモ帳連携用)
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            public const uint WM_KEYDOWN = 0x100;
        }
        #endregion

        #region Fields
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly MeasureCondition _measureCondition = MeasureCondition.GetInstance();

        private string _mainTitle;
        private string _subTitle;
        private string _folder;
        private string _mainNo;
        private string _subNo;
        private string _item;
        private string _voltText;
        private string _folderDirText;

        private DataSetItems _myDataSetItems;
        private DataTable _savedListDat;
        private int _itemFound = -1;
        #endregion

        #region Properties (View Binding Targets)
        public string MainTitle
        {
            get => _mainTitle;
            set { if (_mainTitle != value) { _mainTitle = value; OnPropertyChanged(); } }
        }

        public string SubTitle
        {
            get => _subTitle;
            set { if (_subTitle != value) { _subTitle = value; OnPropertyChanged(); } }
        }

        public string Folder
        {
            get => _folder;
            set
            {
                if (_folder != value)
                {
                    _folder = value;
                    OnPropertyChanged();
                    FolderDirText = string.Format("フォルダ = {0}", _folder);
                }
            }
        }

        public string MainNo
        {
            get => _mainNo;
            private set { if (_mainNo != value) { _mainNo = value; OnPropertyChanged(); } }
        }

        public string SubNo
        {
            get => _subNo;
            private set { if (_subNo != value) { _subNo = value; OnPropertyChanged(); } }
        }

        public string Item
        {
            get => _item;
            private set { if (_item != value) { _item = value; OnPropertyChanged(); } }
        }

        public string VoltText
        {
            get => _voltText;
            private set { if (_voltText != value) { _voltText = value; OnPropertyChanged(); } }
        }

        public string FolderDirText
        {
            get => _folderDirText;
            private set { if (_folderDirText != value) { _folderDirText = value; OnPropertyChanged(); } }
        }

        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            private set { if (_myDataSetItems != value) { _myDataSetItems = value; OnPropertyChanged(); } }
        }

        public bool CanUndo => _savedListDat != null;
        #endregion

        #region Events / Delegates
        /// <summary>
        /// ダイアログ表示要求 (メッセージ, タイトル, ボタンタイプ, ダイアログ結果コールバック)
        /// </summary>
        public event Action<string, string, MessageBoxButtons, Action<DialogResult>> RequestShowDialog;

        /// <summary>
        /// 端子編集画面を表示するための要求
        /// </summary>
        public event Action<DataSetItems> RequestOpenPortView;

        /// <summary>
        /// データ入力画面を表示するための要求 (T#インデックス)
        /// </summary>
        public event Action<int, DataSetItems> RequestOpenDataInputView;

        /// <summary>
        /// HLリミット設定画面を表示するための要求
        /// </summary>
        public event Action<DataSetItems, string> RequestOpenSettingLimitView;

        /// <summary>
        /// 画面を閉じる指示（DialogResultを伴う）
        /// </summary>
        public event Action<DialogResult> RequestCloseView;
        #endregion

        #region Constructors
        public SettingViewModel()
        {
        }

        public SettingViewModel(string mainTitle, string subTitle, string folder)
        {
            MainTitle = mainTitle;
            SubTitle = subTitle;
            Folder = folder;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Form_Load 時に呼ぶ初期化処理
        /// </summary>
        public bool InitializeData()
        {
            MainNo = _defaultSettings.MainNo;
            SubNo = _defaultSettings.SubNo;
            Item = _defaultSettings.Item;

            MyDataSetItems = _measureCondition.createDataSetItems(Folder, MainTitle, SubTitle);
            if (MyDataSetItems == null)
            {
                return false;
            }

            if (MyDataSetItems.CheckDat.Rows.Count > 0)
            {
                _itemFound = MyDataSetItems.CheckDat.Rows.IndexOf(MyDataSetItems.CheckDat.Rows[0]);
                string strVolt = MyDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
                VoltText = GetVoltString(strVolt);
            }

            return true;
        }

        private static string GetVoltString(string valueString)
        {
            string[] voltString = { "12V", "24V" };
            return (valueString == "1" ? voltString[1] : voltString[0]);
        }
        #endregion

        #region Logic Commands & Operations

        #region Voltage Change
        /// <summary>
        /// 電圧設定の切り替え
        /// </summary>
        public void ChangeVolt()
        {
            if (_itemFound < 0 || MyDataSetItems == null) return;

            string strVolt = MyDataSetItems.CheckDat.Rows[_itemFound]["Volt"].ToString();
            string newVolt = (strVolt == "1" ? "0" : "1");

            RequestShowDialog?.Invoke(
                string.Format("電圧を{0}に変更しますか？", GetVoltString(newVolt)),
                _defaultSettings.ApplicationName,
                MessageBoxButtons.YesNo,
                result =>
                {
                    if (result != DialogResult.Yes) return;

                    VoltText = GetVoltString(newVolt);
                    MyDataSetItems.CheckDat.Rows[0]["Volt"] = newVolt;
                    DataRow dtCheckDatRow = MyDataSetItems.CheckDat.Rows[_itemFound];

                    int iDo_Length = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;
                    int mVoltIdx = (newVolt == "1" ? 2 : 0); // 0:12V, 2:24V

                    for (int i = 0; i < iDo_Length; i++)
                    {
                        string dtCheckDatFieldsHi = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + _defaultSettings.CheckDatLMTFields[0];
                        dtCheckDatRow[dtCheckDatFieldsHi] = _defaultSettings.CheckDatLMT[mVoltIdx][i].ToString("F1");

                        string dtCheckDatFieldsLo = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + _defaultSettings.CheckDatLMTFields[1];
                        dtCheckDatRow[dtCheckDatFieldsLo] = _defaultSettings.CheckDatLMT[mVoltIdx + 1][i].ToString("F1");
                    }
                }
            );
        }
        #endregion

        #region List Operations (Add/Delete/Move)
        /// <summary>
        /// 検査項目の追加
        /// </summary>
        public int AddItem(int currentIndex)
        {
            DataTable? dtInspectItem = MyDataSetItems.ListDat;
            DataRow InspectItemRowNew = dtInspectItem.NewRow();

            if (dtInspectItem.Rows.Count > 0 && currentIndex >= 0)
            {
                dtInspectItem.Rows.InsertAt(InspectItemRowNew, currentIndex + 1);
                dtInspectItem.AcceptChanges();
                return currentIndex + 1;
            }
            else
            {
                dtInspectItem.Rows.Add(InspectItemRowNew);
                return dtInspectItem.Rows.Count - 1;
            }
        }

        /// <summary>
        /// 選択されている検査項目の削除
        /// </summary>
        public void DeleteItem(int minRows, int countRows, Action proceedDeleteCallback)
        {
            DataTable dtInspectItem = MyDataSetItems.ListDat;
            if (dtInspectItem.Rows.Count == 0) return;

            if (countRows == dtInspectItem.Rows.Count)
            {
                RequestShowDialog?.Invoke(
                    "本当に全部を削除するのですか？",
                    _defaultSettings.ApplicationName,
                    MessageBoxButtons.YesNo,
                    result =>
                    {
                        if (result == DialogResult.Yes)
                        {
                            ExecuteDelete(minRows, countRows);
                            proceedDeleteCallback?.Invoke();
                        }
                    }
                );
            }
            else
            {
                ExecuteDelete(minRows, countRows);
                proceedDeleteCallback?.Invoke();
            }
        }

        private void ExecuteDelete(int minRows, int countRows)
        {
            DataTable? dtInspectItem = MyDataSetItems.ListDat;
            for (int i = 0; i < countRows; i++)
            {
                if (minRows + countRows - 1 - i < dtInspectItem.Rows.Count)
                {
                    dtInspectItem.Rows.RemoveAt(minRows + countRows - 1 - i);
                }
            }
            dtInspectItem.AcceptChanges();

            if (dtInspectItem.Rows.Count == 0)
            {
                AddItem(-1);
            }
        }

        /// <summary>
        /// 項目の上移動
        /// </summary>
        public int MoveUpItem(int currentIndex)
        {
            DataTable dtInspectItem = MyDataSetItems.ListDat;
            if (dtInspectItem.Rows.Count >= 2 && currentIndex > 0)
            {
                DataRow dtInspectItemRowCurr = dtInspectItem.NewRow();
                dtInspectItemRowCurr.ItemArray = dtInspectItem.Rows[currentIndex].ItemArray;
                dtInspectItem.Rows.InsertAt(dtInspectItemRowCurr, currentIndex - 1);
                dtInspectItem.Rows.RemoveAt(currentIndex + 1);
                return currentIndex - 1;
            }
            return currentIndex;
        }

        /// <summary>
        /// 項目の下移動
        /// </summary>
        public int MoveDownItem(int currentIndex)
        {
            DataTable? dtInspectItem = MyDataSetItems.ListDat;
            if (dtInspectItem.Rows.Count >= 2 && currentIndex >= 0 && currentIndex < dtInspectItem.Rows.Count - 1)
            {
                DataRow dtInspectItemRowCurr = dtInspectItem.NewRow();
                dtInspectItemRowCurr.ItemArray = dtInspectItem.Rows[currentIndex].ItemArray;
                dtInspectItem.Rows.InsertAt(dtInspectItemRowCurr, currentIndex + 2);
                dtInspectItem.Rows.RemoveAt(currentIndex);
                return currentIndex + 1;
            }
            return currentIndex;
        }
        #endregion

        #region Undo & Acceptance History
        /// <summary>
        /// Undo用に現在の状態を記憶
        /// </summary>
        public void AcceptChangesForUndo()
        {
            if (MyDataSetItems?.Tables["ListDat"] != null)
            {
                _savedListDat = MyDataSetItems.Tables["ListDat"].Copy();
                OnPropertyChanged(nameof(CanUndo));
            }
        }

        /// <summary>
        /// Undoの実行
        /// </summary>
        public void ExecuteUndo()
        {
            if (_savedListDat != null && MyDataSetItems?.Tables["ListDat"] != null)
            {
                MyDataSetItems.Tables["ListDat"].Rows.Clear();
                foreach (DataRow row in _savedListDat.Rows)
                {
                    MyDataSetItems.Tables["ListDat"].ImportRow(row);
                }
                _savedListDat = null;
                OnPropertyChanged(nameof(CanUndo));
            }
        }
        #endregion

        #region Save & Close Logic
        /// <summary>
        /// 保存して閉じる処理の流れをコントロール
        /// </summary>
        public void SaveAndClose()
        {
            string portPath = Path.Combine(Folder, _defaultSettings.Portdat);
            string listPath = Path.Combine(Folder, _defaultSettings.Listdat);
            string checkPath = Path.Combine(Folder, _defaultSettings.Checkdat);

            bool isChanged = _measureCondition.checkChangedPortdatFile(MyDataSetItems.PortDat, portPath)
                          || _measureCondition.checkChangedInspectItem(MyDataSetItems.ListDat, listPath)
                          || _measureCondition.checkChangedCheckdat(MyDataSetItems.CheckDat, checkPath);

            if (!isChanged)
            {
                RequestShowDialog?.Invoke(
                    "変更はありませんが、保存しますか？",
                    _defaultSettings.ApplicationName,
                    MessageBoxButtons.YesNo,
                    result =>
                    {
                        if (result != DialogResult.Yes)
                        {
                            RequestCloseView?.Invoke(DialogResult.OK);
                            return;
                        }
                        PromptHistoryAndSave(portPath, listPath, checkPath);
                    }
                );
            }
            else
            {
                PromptHistoryAndSave(portPath, listPath, checkPath);
            }
        }

        private void PromptHistoryAndSave(string portPath, string listPath, string checkPath)
        {
            RequestShowDialog?.Invoke(
                "変更履歴を作成しますか？",
                _defaultSettings.ApplicationName,
                MessageBoxButtons.YesNo,
                result =>
                {
                    if (result == DialogResult.Yes)
                    {
                        string filePath = Path.Combine(Folder, "変更履歴.txt");
                        if (CloseNotepad(filePath))
                        {
                            StartNotepad(filePath);
                        }
                    }

                    // データの永続化
                    _measureCondition.savePortdatFile(MyDataSetItems.PortDat, portPath);
                    _measureCondition.clearListdatLMT(MyDataSetItems.ListDat);
                    _measureCondition.saveInspectItem(MyDataSetItems.ListDat, listPath);
                    _measureCondition.saveCheckdat(MyDataSetItems.CheckDat, checkPath);

                    RequestCloseView?.Invoke(DialogResult.OK);
                }
            );
        }
        #endregion

        #region Notepad Control
        public void OpenInspectionHistory()
        {
            string filePath = Path.Combine(Folder, "変更履歴.txt");
            if (CloseNotepad(filePath))
            {
                OpenNotepad(filePath);
            }
        }

        private void StartNotepad(string filePath)
        {
            if (!Directory.Exists(Folder)) return;

            try
            {
                if (!File.Exists(filePath))
                {
                    using (File.CreateText(filePath)) { }
                }

                string currentContent = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
                string newHeader = string.Format("〇{0}\r\n\r\n", DateTime.Now.ToString());
                File.WriteAllText(filePath, newHeader + currentContent);

                Process p = Process.Start("notepad.exe", filePath);
                p?.WaitForInputIdle();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Notepad open error: {ex.Message}");
            }
        }

        private static bool CloseNotepad(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string title = string.Format("{0} - メモ帳", fileName);

            IntPtr hWnd = NativeMethods.FindWindow(null, title);
            if (hWnd == IntPtr.Zero) return true;

            try
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out int processId);
                Process p = Process.GetProcessById(processId);
                p.CloseMainWindow();
                p.Close();

                Stopwatch sw = Stopwatch.StartNew();
                while (true)
                {
                    hWnd = NativeMethods.FindWindow(null, title);
                    if (hWnd == IntPtr.Zero) break;

                    if (sw.ElapsedMilliseconds > 3000) return false;
                    System.Threading.Thread.Sleep(50);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static void OpenNotepad(string filePath)
        {
            if (!File.Exists(filePath)) return;
            Process p = Process.Start("notepad.exe", filePath);
            p?.WaitForInputIdle();
        }
        #endregion

        #region Child Form Open Requests
        public void OpenPortView()
        {
            RequestOpenPortView?.Invoke(MyDataSetItems);
        }

        public void OpenDataInputView(int rowIndex)
        {
            if (rowIndex < 0) return;
            RequestOpenDataInputView?.Invoke(rowIndex, MyDataSetItems);
        }

        public void OpenSettingLimitView()
        {
            RequestOpenSettingLimitView?.Invoke(MyDataSetItems, Folder);
            if (MyDataSetItems?.CheckDat.Rows.Count > 0)
            {
                string strVolt = MyDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
                VoltText = GetVoltString(strVolt);
            }
        }
        #endregion

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}