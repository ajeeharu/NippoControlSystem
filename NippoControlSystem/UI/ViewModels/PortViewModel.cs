using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// ポート・端子名設定画面用の ViewModel
    /// </summary>
    public class PortViewModel : INotifyPropertyChanged
    {
        #region Fields & Dependencies
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private DataSetItems _dataSetItems;
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// データ保持用の DataSetItems
        /// </summary>
        public DataSetItems DataSetItems
        {
            get => _dataSetItems;
            set
            {
                if (_dataSetItems != value)
                {
                    _dataSetItems = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 端子名編集ダイアログ（PinNameView）の表示要求イベント
        /// 引数: (int selectPin, string currentPinName, Action<string> onNameUpdated)
        /// </summary>
        public event Action<int, string, Action<string>> RequestEditPinName;

        /// <summary>
        /// 保存して画面を閉じる要求イベント
        /// </summary>
        public event EventHandler RequestCloseWindow;

        /// <summary>
        /// 変更を取り消して画面を閉じる要求イベント
        /// </summary>
        public event EventHandler RequestCancelAndCloseWindow;
        #endregion

        #region Constructors
        public PortViewModel()
        {
        }

        public PortViewModel(DataSetItems dataSetItems)
        {
            _dataSetItems = dataSetItems;
        }
        #endregion

        #region Business Logic & Data Sync
        /// <summary>
        /// PortDat から各 View_* テーブル（DIO, AI, AO, GND）へ端子名を展開
        /// </summary>
        public void LoadPortNamesToTables()
        {
            if (DataSetItems?.PortDat == null || DataSetItems.PortDat.Rows.Count == 0)
                return;

            DataRow dtPortDatRow = DataSetItems.PortDat.Rows[0];

            // DIO (DA～DH) の読み込み[cite: 9]
            for (int i = 0; i < _defaultSettings.DioNames.Length; i++)
            {
                string dioName = _defaultSettings.DioNames[i];
                DataTable dtViewDIO = DataSetItems.Tables[$"View_{dioName}"];
                if (dtViewDIO == null) continue;

                int loopCount = (i == 7) ? 28 : _defaultSettings.DioNums[i]; // Hは28個[cite: 9]
                for (int j = 0; j < loopCount; j++)
                {
                    string fieldName = $"{dioName}-{(j + 1):00}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewDIO.Rows.Count)
                    {
                        dtViewDIO.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewDIO.AcceptChanges();
            }

            // AI の読み込み[cite: 9]
            DataTable dtViewAI = DataSetItems.Tables[$"View_{_defaultSettings.AiName}"];
            if (dtViewAI != null)
            {
                for (int j = 0; j < _defaultSettings.AiNum; j++)
                {
                    string fieldName = $"{_defaultSettings.AiName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAI.Rows.Count)
                    {
                        dtViewAI.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewAI.AcceptChanges();
            }

            // AO の読み込み[cite: 9]
            DataTable dtViewAO = DataSetItems.Tables[$"View_{_defaultSettings.AoName}"];
            if (dtViewAO != null)
            {
                int aoLoopCount = Math.Max(_defaultSettings.AoNum, _defaultSettings.AoSwichNum);
                for (int j = 0; j < aoLoopCount; j++)
                {
                    string fieldName = $"{_defaultSettings.AoName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAO.Rows.Count)
                    {
                        dtViewAO.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewAO.AcceptChanges();
            }

            // GND (GndDA～GndAI) の読み込み[cite: 9]
            for (int i = 0; i < _defaultSettings.GndNames.Length; i++)
            {
                string gndName = _defaultSettings.GndNames[i];
                DataTable dtViewGnd = DataSetItems.Tables[$"View_{gndName}"];
                if (dtViewGnd == null) continue;

                for (int j = 0; j < _defaultSettings.GndNums[i]; j++)
                {
                    string fieldName = $"{gndName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewGnd.Rows.Count)
                    {
                        dtViewGnd.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewGnd.AcceptChanges();
            }
        }

        /// <summary>
        /// 各 View_* テーブルから PortDat へ端子名を書き戻して確定保存[cite: 9]
        /// </summary>
        public void SaveTablesToPortDat()
        {
            if (DataSetItems?.PortDat == null || DataSetItems.PortDat.Rows.Count == 0)
                return;

            DataRow dtPortDatRow = DataSetItems.PortDat.Rows[0];

            // DIO (DA～DH) の保存[cite: 9]
            for (int i = 0; i < _defaultSettings.DioNames.Length; i++)
            {
                string dioName = _defaultSettings.DioNames[i];
                DataTable dtViewDIO = DataSetItems.Tables[$"View_{dioName}"];
                if (dtViewDIO == null) continue;

                for (int j = 0; j < _defaultSettings.DioNums[i]; j++)
                {
                    string fieldName = $"{dioName}-{(j + 1):00}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewDIO.Rows.Count)
                    {
                        dtPortDatRow[fieldName] = dtViewDIO.Rows[j]["Name"];
                    }
                }
            }

            // AI の保存[cite: 9]
            DataTable dtViewAI = DataSetItems.Tables[$"View_{_defaultSettings.AiName}"];
            if (dtViewAI != null)
            {
                for (int j = 0; j < _defaultSettings.AiNum; j++)
                {
                    string fieldName = $"{_defaultSettings.AiName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAI.Rows.Count)
                    {
                        dtPortDatRow[fieldName] = dtViewAI.Rows[j]["Name"];
                    }
                }
            }

            // AO の保存[cite: 9]
            DataTable dtViewAO = DataSetItems.Tables[$"View_{_defaultSettings.AoName}"];
            if (dtViewAO != null)
            {
                int aoLoopCount = Math.Max(_defaultSettings.AoNum, _defaultSettings.AoSwichNum);
                for (int j = 0; j < aoLoopCount; j++)
                {
                    string fieldName = $"{_defaultSettings.AoName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAO.Rows.Count)
                    {
                        dtPortDatRow[fieldName] = dtViewAO.Rows[j]["Name"];
                    }
                }
            }

            // GND の保存[cite: 9]
            for (int i = 0; i < _defaultSettings.GndNames.Length; i++)
            {
                string gndName = _defaultSettings.GndNames[i];
                DataTable dtViewGnd = DataSetItems.Tables[$"View_{gndName}"];
                if (dtViewGnd == null) continue;

                for (int j = 0; j < _defaultSettings.GndNums[i]; j++)
                {
                    string fieldName = $"{gndName}-{(j + 1):0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewGnd.Rows.Count)
                    {
                        dtPortDatRow[fieldName] = dtViewGnd.Rows[j]["Name"];
                    }
                }
            }

            dtPortDatRow.AcceptChanges();
        }

        /// <summary>
        /// ダブルクリック等による端子名編集処理を呼び出す[cite: 9]
        /// </summary>
        public void EditPinName(int rowIndex, string currentName, Action<string> applyNewNameAction)
        {
            if (rowIndex < 0) return;
            RequestEditPinName?.Invoke(rowIndex, currentName, applyNewNameAction);
        }

        /// <summary>
        /// 保存して終了[cite: 9]
        /// </summary>
        public void SaveAndClose()
        {
            SaveTablesToPortDat();
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 変更を破棄して終了[cite: 9]
        /// </summary>
        public void CancelAndClose()
        {
            DataSetItems?.RejectChanges();
            RequestCancelAndCloseWindow?.Invoke(this, EventArgs.Empty);
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