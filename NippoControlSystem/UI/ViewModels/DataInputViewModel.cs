using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NippoControlSystem.UI.ViewModels
{
    public class DataInputViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataSetItems? _myDataSetItems;
        private int _startTNo;
        private int _tNo;
        private string _title = string.Empty;
        private string _guide = string.Empty;
        private int _typeIndex;
        private bool _isBusy;
        private string _errorMessage = string.Empty;

        // 設定の参照 (既存の Cyc.IO.Settings などから取得)
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        #endregion

        #region Properties
        public DataSetItems? MyDataSetItems
        {
            get => _myDataSetItems;
            set
            {
                if (_myDataSetItems != value)
                {
                    _myDataSetItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StartTNo
        {
            get => _startTNo;
            set
            {
                if (_startTNo != value)
                {
                    _startTNo = value;
                    OnPropertyChanged();
                    TNo = value;
                }
            }
        }

        public int TNo
        {
            get => _tNo;
            set
            {
                if (_tNo != value)
                {
                    _tNo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayTNo));
                    LoadDataForCurrentTNo();
                }
            }
        }

        /// <summary>
        /// 画面表示用 (1-based index)
        /// </summary>
        public int DisplayTNo
        {
            get => _tNo + 1;
            set
            {
                if (value > 0 && MyDataSetItems?.ListDat != null && value <= MyDataSetItems.ListDat.Rows.Count)
                {
                    TNo = value - 1;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    ErrorMessage = "整数を入力して下さい";
                }
            }
        }

        public string Title
        {
            get => _title;
            set { if (_title != value) { _title = value; OnPropertyChanged(); } }
        }

        public string Guide
        {
            get => _guide;
            set { if (_guide != value) { _guide = value; OnPropertyChanged(); } }
        }

        public int TypeIndex
        {
            get => _typeIndex;
            set { if (_typeIndex != value) { _typeIndex = value; OnPropertyChanged(); } }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { if (_errorMessage != value) { _errorMessage = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Commands
        public ICommand PreviousCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ViewLimitCommand { get; }
        #endregion

        #region Delegates / Events for Navigation
        // View側のダイアログ表示や画面遷移を呼び出すためのアクション
        public Action RequestClose { get; set; }
        public Func<string, bool> RequestConfirmAddNewRow { get; set; }
        public Action<string> RequestShowLimitView { get; set; }
        #endregion

        #region Constructor
        public DataInputViewModel()
        {
            PreviousCommand = new RelayCommand(OnPrevious, () => !IsBusy);
            NextCommand = new RelayCommand(OnNext, () => !IsBusy);
            CloseCommand = new RelayCommand(OnClose, () => !IsBusy);
            ViewLimitCommand = new RelayCommand(OnViewLimit, () => !IsBusy);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// データ表示の初期化（Form_Load相当）
        /// </summary>
        public void InitializeData()
        {
            if (MyDataSetItems == null) return;

            DisplayPortNames();
            TNo = StartTNo;
        }

        /// <summary>
        /// 現在の入力値をDataSet(ListDat)へ保存する（dataGridView_DA_Restore相当）
        /// </summary>
        public void RestoreCurrentData()
        {
            if (MyDataSetItems?.ListDat == null || TNo < 0 || TNo >= MyDataSetItems.ListDat.Rows.Count) return;

            DataRow dtListDatRow = MyDataSetItems.ListDat.Rows[TNo];
            dtListDatRow["Title"] = Title;
            dtListDatRow["Guide"] = Guide;
            dtListDatRow["Type"] = TypeIndex;

            // DIOデータ保存
            for (int i = 0; i < _defaultSettings.DioNames.Length; i++)
            {
                DataTable? dtViewDIO = MyDataSetItems.Tables[$"View_{_defaultSettings.DioNames[i]}"];
                if (dtViewDIO == null) continue;

                for (int j = 0; j < _defaultSettings.DioNums[i]; j++)
                {
                    string iFieldName = $"{_defaultSettings.DioNames[i]}-{j + 1:00}";
                    if (j < dtViewDIO.Rows.Count)
                    {
                        dtListDatRow[iFieldName] = dtViewDIO.Rows[j]["IO"];
                    }
                }
            }

            // AOデータ保存
            DataTable? dtViewAO = MyDataSetItems.Tables[$"View_{_defaultSettings.AoName}"];
            if (dtViewAO != null)
            {
                for (int i = 0; i < _defaultSettings.AoSwichNum; i++)
                {
                    string iFieldName = $"{_defaultSettings.AoName}-{i + 1:0}";
                    if (i < _defaultSettings.AoNum && i < dtViewAO.Rows.Count)
                    {
                        dtListDatRow[iFieldName] = dtViewAO.Rows[i]["Value"];
                    }
                    string iSwFieldName = $"{_defaultSettings.AoSwichName}-{i + 1:0}";
                    if (i < dtViewAO.Rows.Count)
                    {
                        dtListDatRow[iSwFieldName] = dtViewAO.Rows[i]["Enable"];
                    }
                }
            }

            // AIデータ保存
            DataTable? dtViewAI = MyDataSetItems.Tables[$"View_{_defaultSettings.AiName}"];
            if (dtViewAI != null)
            {
                for (int i = 0; i < _defaultSettings.AiNum; i++)
                {
                    if (i < dtViewAI.Rows.Count)
                    {
                        string iFieldNameLow = $"{_defaultSettings.AiName}-{i + 1:0}L";
                        dtListDatRow[iFieldNameLow] = dtViewAI.Rows[i]["Lower"];

                        string iFieldNameHigh = $"{_defaultSettings.AiName}-{i + 1:0}H";
                        dtListDatRow[iFieldNameHigh] = dtViewAI.Rows[i]["Upper"];
                    }
                }
            }

            dtListDatRow.AcceptChanges();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// PortDatからの名称反映（dataGridView_DA_Display相当）
        /// </summary>
        private void DisplayPortNames()
        {
            if (MyDataSetItems?.PortDat == null || MyDataSetItems.PortDat.Rows.Count == 0) return;

            DataRow dtPortDatRow = MyDataSetItems.PortDat.Rows[0];

            // DIO View テーブル反映
            for (int i = 0; i < _defaultSettings.DioNames.Length; i++)
            {
                DataTable? dtViewDIO = MyDataSetItems.Tables[$"View_{_defaultSettings.DioNames[i]}"];
                if (dtViewDIO == null) continue;

                for (int j = 0; j < _defaultSettings.DioNums[i]; j++)
                {
                    string iFieldName = $"{_defaultSettings.DioNames[i]}-{j + 1:00}";
                    if (j < dtViewDIO.Rows.Count)
                    {
                        dtViewDIO.Rows[j]["Name"] = dtPortDatRow[iFieldName];
                    }
                }
                dtViewDIO.AcceptChanges();
            }

            // AI View テーブル反映
            DataTable? dtViewAI = MyDataSetItems.Tables[$"View_{_defaultSettings.AiName}"];
            if (dtViewAI != null)
            {
                for (int j = 0; j < _defaultSettings.AiNum; j++)
                {
                    string iFieldName = $"{_defaultSettings.AiName}-{j + 1:0}";
                    if (j < dtViewAI.Rows.Count)
                    {
                        dtViewAI.Rows[j]["Name"] = dtPortDatRow[iFieldName];
                    }
                }
                dtViewAI.AcceptChanges();
            }

            // AO View テーブル反映
            DataTable? dtViewAO = MyDataSetItems.Tables[$"View_{_defaultSettings.AoName}"];
            if (dtViewAO != null)
            {
                int loopCount = Math.Max(_defaultSettings.AoNum, _defaultSettings.AoSwichNum);
                for (int j = 0; j < loopCount; j++)
                {
                    string iFieldName = $"{_defaultSettings.AoName}-{j + 1:0}";
                    if (j < dtViewAO.Rows.Count)
                    {
                        dtViewAO.Rows[j]["Name"] = dtPortDatRow[iFieldName];
                    }
                }
                dtViewAO.AcceptChanges();
            }

            // GND View テーブル反映
            for (int i = 0; i < _defaultSettings.GndNames.Length; i++)
            {
                DataTable? dtViewGndDIO = MyDataSetItems.Tables[$"View_{_defaultSettings.GndNames[i]}"];
                DataTable? dtViewGndAIO = MyDataSetItems.Tables["View_GndAIO"];
                if (dtViewGndDIO == null) continue;

                for (int j = 0; j < _defaultSettings.GndNums[i]; j++)
                {
                    string iFieldName = $"{_defaultSettings.GndNames[i]}-{j + 1:0}";
                    if (j < dtViewGndDIO.Rows.Count)
                    {
                        dtViewGndDIO.Rows[j]["Name"] = dtPortDatRow[iFieldName];
                    }

                    if (dtViewGndAIO != null && j < dtViewGndAIO.Rows.Count)
                    {
                        if (_defaultSettings.GndNames[i] == "GndAO")
                        {
                            dtViewGndAIO.Rows[j]["AoName"] = dtPortDatRow[iFieldName];
                        }
                        else if (_defaultSettings.GndNames[i] == "GndAI")
                        {
                            dtViewGndAIO.Rows[j]["AiName"] = dtPortDatRow[iFieldName];
                        }
                    }
                }
                dtViewGndDIO.AcceptChanges();
            }
        }

        /// <summary>
        /// TNoに対応するデータの読み込み（dataGridView_DA_Dat相当）
        /// </summary>
        private void LoadDataForCurrentTNo()
        {
            if (MyDataSetItems?.ListDat == null || TNo < 0 || TNo >= MyDataSetItems.ListDat.Rows.Count) return;

            DataRow dtListDatRow = MyDataSetItems.ListDat.Rows[TNo];
            Title = dtListDatRow["Title"]?.ToString() ?? string.Empty;
            Guide = dtListDatRow["Guide"]?.ToString() ?? string.Empty;

            if (int.TryParse(dtListDatRow["Type"]?.ToString(), out int parsedType))
            {
                TypeIndex = parsedType;
            }
            else
            {
                TypeIndex = 0;
            }

            // DIO データ読み込み
            for (int i = 0; i < _defaultSettings.DioNames.Length; i++)
            {
                DataTable? dtViewDIO = MyDataSetItems.Tables[$"View_{_defaultSettings.DioNames[i]}"];
                if (dtViewDIO == null) continue;

                for (int j = 0; j < _defaultSettings.DioNums[i]; j++)
                {
                    string iFieldName = $"{_defaultSettings.DioNames[i]}-{j + 1:00}";
                    if (j < dtViewDIO.Rows.Count)
                    {
                        dtViewDIO.Rows[j]["IO"] = dtListDatRow[iFieldName];
                    }
                }
                dtViewDIO.AcceptChanges();
            }

            // AO データ読み込み
            DataTable? dtViewAO = MyDataSetItems.Tables[$"View_{_defaultSettings.AoName}"];
            if (dtViewAO != null)
            {
                for (int i = 0; i < _defaultSettings.AoSwichNum; i++)
                {
                    string iFieldName = $"{_defaultSettings.AoName}-{i + 1:0}";
                    if (i < _defaultSettings.AoNum && i < dtViewAO.Rows.Count)
                    {
                        dtViewAO.Rows[i]["Value"] = dtListDatRow[iFieldName];
                    }

                    string iSwFieldName = $"{_defaultSettings.AoSwichName}-{i + 1:0}";
                    if (i < dtViewAO.Rows.Count)
                    {
                        dtViewAO.Rows[i]["Enable"] = dtListDatRow[iSwFieldName];
                    }
                }
                dtViewAO.AcceptChanges();
            }

            // AI データ読み込み
            DataTable? dtViewAI = MyDataSetItems.Tables[$"View_{_defaultSettings.AiName}"];
            if (dtViewAI != null)
            {
                for (int i = 0; i < _defaultSettings.AiNum; i++)
                {
                    if (i < dtViewAI.Rows.Count)
                    {
                        string iFieldNameLow = $"{_defaultSettings.AiName}-{i + 1:0}L";
                        dtViewAI.Rows[i]["Lower"] = dtListDatRow[iFieldNameLow];

                        string iFieldNameHigh = $"{_defaultSettings.AiName}-{i + 1:0}H";
                        dtViewAI.Rows[i]["Upper"] = dtListDatRow[iFieldNameHigh];
                    }
                }
                dtViewAI.AcceptChanges();
            }
        }

        private void OnPrevious()
        {
            IsBusy = true;
            try
            {
                RestoreCurrentData();
                if (TNo > 0)
                {
                    TNo--;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnNext()
        {
            IsBusy = true;
            try
            {
                RestoreCurrentData();

                int totalCount = MyDataSetItems?.ListDat?.Rows.Count ?? 0;
                if (TNo < totalCount - 1)
                {
                    TNo++;
                }
                else
                {
                    bool addNew = RequestConfirmAddNewRow?.Invoke("最後の項目です。\n\n追加しますか？") ?? false;
                    if (addNew && MyDataSetItems?.ListDat != null)
                    {
                        DataTable? dtInspectItem = MyDataSetItems.ListDat;
                        DataRow InspectItemRowNew = dtInspectItem.NewRow();
                        dtInspectItem.Rows.InsertAt(InspectItemRowNew, dtInspectItem.Rows.Count);
                        TNo++;
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnClose()
        {
            IsBusy = true;
            try
            {
                RestoreCurrentData();
                RequestClose?.Invoke();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnViewLimit()
        {
            RestoreCurrentData();
            // 初期選択セル文字列 (例: "A00")
            string selectionCell = "A00";
            RequestShowLimitView?.Invoke(selectionCell);
        }
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