using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 測定結果表示画面（ResultView）用 ViewModel
    /// </summary>
    public class ResultViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataSetItems _dataSetItems;
        private int _tNo;
        private string _title = string.Empty;
        private string _guide = string.Empty;
        private string _typeText = string.Empty;
        private string _resultText = string.Empty;
        private bool _isBusy;
        private string _playSoundWav;
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
                    OnPropertyChanged(nameof(MaxTNo));
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                    UpdateDisplayData();
                }
            }
        }

        /// <summary>
        /// 現在選択されているテスト番号 Index (0-based)
        /// </summary>
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
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                    UpdateDisplayData();
                }
            }
        }

        /// <summary>
        /// 画面表示用テスト番号文字列 (1-based)
        /// </summary>
        public string DisplayTNo
        {
            get => (TNo + 1).ToString();
            set
            {
                if (int.TryParse(value, out int parsedNo))
                {
                    int index = parsedNo - 1;
                    if (index >= 0 && index <= MaxTNo)
                    {
                        TNo = index;
                    }
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// タイトル文字列
        /// </summary>
        public string Title
        {
            get => _title;
            private set { if (_title != value) { _title = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// ガイド（説明）文字列
        /// </summary>
        public string Guide
        {
            get => _guide;
            private set { if (_guide != value) { _guide = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 検査タイプ表示文字列
        /// </summary>
        public string TypeText
        {
            get => _typeText;
            private set { if (_typeText != value) { _typeText = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 測定結果文字列
        /// </summary>
        public string ResultText
        {
            get => _resultText;
            private set { if (_resultText != value) { _resultText = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 処理中（Busy状態）フラグ
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            private set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// ガイド内で指定された再生対象WAVファイル名
        /// </summary>
        public string PlaySoundWav
        {
            get => _playSoundWav;
            private set { if (_playSoundWav != value) { _playSoundWav = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// ListDat テーブルの最大行数インデックス
        /// </summary>
        public int MaxTNo => (DataSetItems?.ListDat?.Rows.Count ?? 1) - 1;

        /// <summary>
        /// 次へ進めるかどうか
        /// </summary>
        public bool CanGoNext => DataSetItems != null && TNo < MaxTNo;

        /// <summary>
        /// 前へ戻れるかどうか
        /// </summary>
        public bool CanGoPrevious => DataSetItems != null && TNo > 0;
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 画面を閉じる要求イベント
        /// </summary>
        public event EventHandler RequestCloseWindow;

        /// <summary>
        /// 限界値画面（ResultLimitView）の表示要求イベント
        /// </summary>
        public event EventHandler<string> RequestShowLimitView;

        /// <summary>
        /// 画面印刷の要求イベント
        /// </summary>
        public event EventHandler RequestPrintScreen;
        #endregion

        #region Constructors
        public ResultViewModel()
        {
        }

        public ResultViewModel(DataSetItems dataSetItems, int tNo = 0)
        {
            DataSetItems = dataSetItems;
            TNo = tNo;
        }
        #endregion

        #region Business Logic & Display Refresh
        /// <summary>
        /// 現在の TNo に基づき、データテーブルや表示データを更新・構築する
        /// </summary>
        public void UpdateDisplayData()
        {
            if (DataSetItems == null || DataSetItems.ListDat == null || TNo < 0 || TNo >= DataSetItems.ListDat.Rows.Count)
                return;

            DataRow dtListDatRow = DataSetItems.ListDat.Rows[TNo];
            DataRow dtListDatResultRow = null;
            if (DataSetItems.ListDatResult != null && DataSetItems.ListDatResult.Rows.Count > TNo)
            {
                dtListDatResultRow = DataSetItems.ListDatResult.Rows[TNo];
            }

            // 基本フィールド情報の設定
            Title = dtListDatRow["Title"]?.ToString() ?? string.Empty;
            Guide = dtListDatRow["Guide"]?.ToString() ?? string.Empty;
            ResultText = dtListDatRow["Result"]?.ToString() ?? string.Empty;

            // 検査タイプの解析
            ParseInspectionType(dtListDatRow["Type"]?.ToString());

            // PortDatおよびView_*テーブルの更新処理
            UpdatePortDisplay();
            UpdateDataTables(dtListDatRow, dtListDatResultRow);

            // ガイドテキスト内のメタデータ（.wavファイル指定など）の抽出
            ParseGuideMetaData(Guide);
        }

        private void ParseInspectionType(string typeValue)
        {
            var defaultSettings = Cyc.IO.Settings.GetInstance();
            if (int.TryParse(typeValue, out int instType))
            {
                if (instType >= 0 && instType < defaultSettings.inspection_TypeText.Length)
                {
                    TypeText = defaultSettings.inspection_TypeText[instType];
                    return;
                }
            }
            TypeText = string.Empty;
        }

        private void UpdatePortDisplay()
        {
            if (DataSetItems.PortDat == null || DataSetItems.PortDat.Rows.Count == 0) return;

            var defaultSettings = Cyc.IO.Settings.GetInstance();
            DataRow dtPortDatRow = DataSetItems.PortDat.Rows[0];

            // DIO 表示名の割り当て
            for (int i = 0; i < defaultSettings.DioNames.Length; i++)
            {
                string tableName = $"View_{defaultSettings.DioNames[i]}";
                if (DataSetItems.Tables.Contains(tableName))
                {
                    DataTable dtViewDIO = DataSetItems.Tables[tableName];
                    for (int j = 0; j < defaultSettings.DioNums[i]; j++)
                    {
                        string fieldName = $"{defaultSettings.DioNames[i]}-{j + 1:00}";
                        if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewDIO.Rows.Count)
                        {
                            dtViewDIO.Rows[j]["Name"] = dtPortDatRow[fieldName];
                        }
                    }
                    dtViewDIO.AcceptChanges();
                }
            }

            // AI 表示名の割り当て
            string aiTableName = $"View_{defaultSettings.AiName}";
            if (DataSetItems.Tables.Contains(aiTableName))
            {
                DataTable dtViewAI = DataSetItems.Tables[aiTableName];
                for (int j = 0; j < defaultSettings.AiNum; j++)
                {
                    string fieldName = $"{defaultSettings.AiName}-{j + 1:0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAI.Rows.Count)
                    {
                        dtViewAI.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewAI.AcceptChanges();
            }

            // AO 表示名の割り当て
            string aoTableName = $"View_{defaultSettings.AoName}";
            if (DataSetItems.Tables.Contains(aoTableName))
            {
                DataTable dtViewAO = DataSetItems.Tables[aoTableName];
                int maxAo = Math.Max(defaultSettings.AoNum, defaultSettings.AoSwichNum);
                for (int j = 0; j < maxAo; j++)
                {
                    string fieldName = $"{defaultSettings.AoName}-{j + 1:0}";
                    if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewAO.Rows.Count)
                    {
                        dtViewAO.Rows[j]["Name"] = dtPortDatRow[fieldName];
                    }
                }
                dtViewAO.AcceptChanges();
            }

            // GND 表示名の割り当て
            for (int i = 0; i < defaultSettings.GndNames.Length; i++)
            {
                string tableName = $"View_{defaultSettings.GndNames[i]}";
                if (DataSetItems.Tables.Contains(tableName))
                {
                    DataTable dtViewGndDIO = DataSetItems.Tables[tableName];
                    DataTable dtViewGndAIO = DataSetItems.Tables.Contains("View_GndAIO") ? DataSetItems.Tables["View_GndAIO"] : null;

                    for (int j = 0; j < defaultSettings.GndNums[i]; j++)
                    {
                        string fieldName = $"{defaultSettings.GndNames[i]}-{j + 1:0}";
                        if (dtPortDatRow.Table.Columns.Contains(fieldName) && j < dtViewGndDIO.Rows.Count)
                        {
                            dtViewGndDIO.Rows[j]["Name"] = dtPortDatRow[fieldName];
                            if (dtViewGndAIO != null && j < dtViewGndAIO.Rows.Count)
                            {
                                if (defaultSettings.GndNames[i] == "GndAO")
                                    dtViewGndAIO.Rows[j]["AoName"] = dtPortDatRow[fieldName];
                                else if (defaultSettings.GndNames[i] == "GndAI")
                                    dtViewGndAIO.Rows[j]["AiName"] = dtPortDatRow[fieldName];
                            }
                        }
                    }
                    dtViewGndDIO.AcceptChanges();
                }
            }
        }

        private void UpdateDataTables(DataRow dtListDatRow, DataRow dtListDatResultRow)
        {
            var defaultSettings = Cyc.IO.Settings.GetInstance();

            // DIO の IO ステータス設定
            for (int i = 0; i < defaultSettings.DioNames.Length; i++)
            {
                string tableName = $"View_{defaultSettings.DioNames[i]}";
                if (!DataSetItems.Tables.Contains(tableName)) continue;

                DataTable dtViewDIO = DataSetItems.Tables[tableName];
                for (int j = 0; j < defaultSettings.DioNums[i]; j++)
                {
                    string fieldName = $"{defaultSettings.DioNames[i]}-{j + 1:00}";
                    string val = dtListDatRow[fieldName]?.ToString() ?? "";

                    if (val == "dat" && dtListDatResultRow != null && dtListDatResultRow.Table.Columns.Contains(fieldName))
                    {
                        string doStat = dtListDatResultRow[fieldName]?.ToString() ?? "";
                        string[] split = doStat.Split(':');
                        dtViewDIO.Rows[j]["IO"] = split.Length == 2 ? split[1] : val;
                    }
                    else
                    {
                        dtViewDIO.Rows[j]["IO"] = val;
                    }
                }
                dtViewDIO.AcceptChanges();
            }

            // AO の設定
            string aoTableName = $"View_{defaultSettings.AoName}";
            if (DataSetItems.Tables.Contains(aoTableName))
            {
                DataTable dtViewAO = DataSetItems.Tables[aoTableName];
                for (int i = 0; i < defaultSettings.AoSwichNum; i++)
                {
                    int ii = i + 1;
                    string aoFieldName = $"{defaultSettings.AoName}-{ii:0}";
                    if (i < defaultSettings.AoNum && dtListDatRow.Table.Columns.Contains(aoFieldName))
                    {
                        dtViewAO.Rows[i]["Value"] = dtListDatRow[aoFieldName];
                    }

                    string swFieldName = $"{defaultSettings.AoSwichName}-{ii:0}";
                    if (dtListDatRow.Table.Columns.Contains(swFieldName))
                    {
                        dtViewAO.Rows[i]["Enable"] = dtListDatRow[swFieldName];
                    }
                }
                dtViewAO.AcceptChanges();
            }

            // AI の設定
            string aiTableName = $"View_{defaultSettings.AiName}";
            if (DataSetItems.Tables.Contains(aiTableName))
            {
                DataTable dtViewAI = DataSetItems.Tables[aiTableName];
                for (int i = 0; i < defaultSettings.AiNum; i++)
                {
                    int ii = i + 1;
                    string lowerField = $"{defaultSettings.AiName}-{ii:0}L";
                    string upperField = $"{defaultSettings.AiName}-{ii:0}H";

                    if (dtListDatRow.Table.Columns.Contains(lowerField))
                        dtViewAI.Rows[i]["Lower"] = dtListDatRow[lowerField];
                    if (dtListDatRow.Table.Columns.Contains(upperField))
                        dtViewAI.Rows[i]["Upper"] = dtListDatRow[upperField];

                    if (dtListDatResultRow != null && dtListDatResultRow.Table.Columns.Contains(lowerField))
                    {
                        dtViewAI.Rows[i]["Value"] = dtListDatResultRow[lowerField];
                    }
                }
                dtViewAI.AcceptChanges();
            }
        }

        private void ParseGuideMetaData(string guideText)
        {
            PlaySoundWav = null;
            if (string.IsNullOrEmpty(guideText)) return;

            var matches = System.Text.RegularExpressions.Regex.Matches(guideText, @"\{.*?\}");
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string keyWord = match.Value.Trim('{', '}');
                if (keyWord.Length >= 5 && keyWord.Substring(keyWord.Length - 4).Equals(".WAV", StringComparison.OrdinalIgnoreCase))
                {
                    PlaySoundWav = keyWord;
                    break;
                }
            }
        }
        #endregion

        #region User Commands
        /// <summary>
        /// 次のテスト番号へ進む
        /// </summary>
        public void Next()
        {
            if (!CanGoNext) return;

            try
            {
                IsBusy = true;
                TNo++;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 前のテスト番号へ戻る
        /// </summary>
        public void Previous()
        {
            if (!CanGoPrevious) return;

            try
            {
                IsBusy = true;
                TNo--;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        public void Close()
        {
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 限界値（Limit）画面を表示する
        /// </summary>
        /// <param name="selectionCell">選択セル位置（例: "A00"）</param>
        public void OpenResultLimit(string selectionCell)
        {
            RequestShowLimitView?.Invoke(this, selectionCell);
        }

        /// <summary>
        /// 画面を印刷する
        /// </summary>
        public void Print()
        {
            RequestPrintScreen?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }
        #endregion
    }
}