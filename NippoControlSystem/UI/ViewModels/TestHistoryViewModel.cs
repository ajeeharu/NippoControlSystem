using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 過去検査履歴画面（TestHistoryView）用 ViewModel
    /// </summary>
    public class TestHistoryViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly Cyc.IO.cDio _dio = Cyc.IO.cDio.GetInstance();
        private readonly Cyc.IO.Aio _aio = Cyc.IO.Aio.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        private string _historyFile = "";
        private string _mainTitle;
        private string _subTitle;
        private string _folder;

        private string _mainNo;
        private string _subNo;
        private string _item;
        private string _serialTitle;
        private string _gokiTitle;

        private string _powerVoltText;
        private string _serialNo;
        private string _goNo;
        private string _statusText;
        private bool _isResultOkVisible;
        private bool _isResultNgVisible;

        private int _currentTNo = 0;
        private string _testNoText = "1";
        private string _inspectTypeText;
        private string _currentResultText;
        private string _guideText;

        private DataSetItems _myDataSetItems;
        #endregion

        #region Properties (View Binding Targets)
        public string HistoryFile
        {
            get => _historyFile;
            set { if (_historyFile != value) { _historyFile = value; OnPropertyChanged(); } }
        }

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
            set { if (_folder != value) { _folder = value; OnPropertyChanged(); } }
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

        public string SerialTitle
        {
            get => _serialTitle;
            private set { if (_serialTitle != value) { _serialTitle = value; OnPropertyChanged(); } }
        }

        public string GokiTitle
        {
            get => _gokiTitle;
            private set { if (_gokiTitle != value) { _gokiTitle = value; OnPropertyChanged(); } }
        }

        public string PowerVoltText
        {
            get => _powerVoltText;
            private set { if (_powerVoltText != value) { _powerVoltText = value; OnPropertyChanged(); } }
        }

        public string SerialNo
        {
            get => _serialNo;
            private set { if (_serialNo != value) { _serialNo = value; OnPropertyChanged(); } }
        }

        public string GoNo
        {
            get => _goNo;
            private set { if (_goNo != value) { _goNo = value; OnPropertyChanged(); } }
        }

        public string StatusText
        {
            get => _statusText;
            private set { if (_statusText != value) { _statusText = value; OnPropertyChanged(); } }
        }

        public bool IsResultOkVisible
        {
            get => _isResultOkVisible;
            private set { if (_isResultOkVisible != value) { _isResultOkVisible = value; OnPropertyChanged(); } }
        }

        public bool IsResultNgVisible
        {
            get => _isResultNgVisible;
            private set { if (_isResultNgVisible != value) { _isResultNgVisible = value; OnPropertyChanged(); } }
        }

        public int CurrentTNo
        {
            get => _currentTNo;
            set
            {
                if (_currentTNo != value)
                {
                    _currentTNo = value;
                    OnPropertyChanged();
                    UpdateSelectedTNoDetails();
                }
            }
        }

        public string TestNoText
        {
            get => _testNoText;
            private set { if (_testNoText != value) { _testNoText = value; OnPropertyChanged(); } }
        }

        public string InspectTypeText
        {
            get => _inspectTypeText;
            private set { if (_inspectTypeText != value) { _inspectTypeText = value; OnPropertyChanged(); } }
        }

        public string CurrentResultText
        {
            get => _currentResultText;
            private set { if (_currentResultText != value) { _currentResultText = value; OnPropertyChanged(); } }
        }

        public string GuideText
        {
            get => _guideText;
            private set { if (_guideText != value) { _guideText = value; OnPropertyChanged(); } }
        }

        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            private set { if (_myDataSetItems != value) { _myDataSetItems = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events / Delegates
        /// <summary>
        /// ダイアログ表示要求 (メッセージ, タイトル, コールバック)
        /// </summary>
        public event Action<string, string> RequestShowMessage;

        /// <summary>
        /// 単一テスト結果詳細画面（ResultView）を表示するための要求
        /// </summary>
        public event Action<DataSetItems, int> RequestOpenResultView;

        /// <summary>
        /// データ入力画面を表示するための要求
        /// </summary>
        public event Action RequestOpenDataInputView;

        /// <summary>
        /// 画面（Form）印刷要求
        /// </summary>
        public event Action RequestPrintForm;

        /// <summary>
        /// 画面を閉じる要求
        /// </summary>
        public event Action RequestCloseView;
        #endregion

        #region Constructors
        public TestHistoryViewModel()
        {
        }

        public TestHistoryViewModel(string historyFile, string mainTitle, string subTitle, string folder)
        {
            HistoryFile = historyFile;
            MainTitle = mainTitle;
            SubTitle = subTitle;
            Folder = folder;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// 画面ロード時の初期化処理
        /// </summary>
        public bool InitializeData()
        {
            // ラベルタイトルの設定
            MainNo = _defaultSettings.MainNo;
            SubNo = _defaultSettings.SubNo;
            Item = _defaultSettings.Item;
            SerialTitle = _defaultSettings.SerialTitle;
            GokiTitle = _defaultSettings.GokiTitle;

            // データセット作成
            MyDataSetItems = _mc.createDataSetItems(Folder, MainTitle, SubTitle);
            if (MyDataSetItems == null)
            {
                return false;
            }

            // 履歴ファイル読み込み・解析
            if (!LoadHistoryData())
            {
                return false;
            }

            // 電圧表示
            if (MyDataSetItems.CheckDat.Rows.Count > 0)
            {
                string powerVolt = MyDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
                PowerVoltText = string.Format("{0}V", powerVolt == "1" ? 24 : 12);
            }

            // 初期選択アイテムの反映
            UpdateSelectedTNoDetails();

            return true;
        }

        /// <summary>
        /// 過去ログファイル（HistoryFile）からのデータセット生成
        /// </summary>
        private bool LoadHistoryData()
        {
            if (string.IsNullOrEmpty(HistoryFile) || !File.Exists(HistoryFile))
            {
                return false;
            }

            string alltext = _mc.ReadAllText(HistoryFile, _mc.enc);
            string[] splitedText = alltext.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string histFolder = Path.Combine(_defaultSettings.ApplicationFloder, _defaultSettings.SettingsHolder, _defaultSettings.HistViewFolder);
            if (!Directory.Exists(histFolder))
            {
                Directory.CreateDirectory(histFolder);
            }

            string checkdatFile = Path.Combine(histFolder, _defaultSettings.Checkdat);
            string listdatFile = Path.Combine(histFolder, _defaultSettings.Listdat);
            string portdatFile = Path.Combine(histFolder, _defaultSettings.Portdat);
            string histdatFile = Path.Combine(histFolder, _defaultSettings.Histdat);

            if (splitedText.Length >= 4)
            {
                File.WriteAllText(histdatFile, splitedText[0], _mc.enc);
                File.WriteAllText(listdatFile, splitedText[1], _mc.enc);
                File.WriteAllText(portdatFile, splitedText[2], _mc.enc);
                File.WriteAllText(checkdatFile, splitedText[3], _mc.enc);
            }

            if (!File.Exists(checkdatFile))
            {
                RequestShowMessage?.Invoke(string.Format("Check.datファイルが見つかりません。({0})", checkdatFile), _defaultSettings.ApplicationName);
                return false;
            }

            _mc.loadCheckdat(MyDataSetItems.CheckDat, checkdatFile);
            _mc.loadListdatFile(MyDataSetItems.ListDat, listdatFile);
            _mc.loadPortdatFile(MyDataSetItems.PortDat, portdatFile);
            _mc.loadListdatFile(MyDataSetItems.ListDatResult, histdatFile, true);

            for (int i = 0; i < MyDataSetItems.ListDat.Rows.Count; i++)
            {
                if (i < MyDataSetItems.ListDatResult.Rows.Count)
                {
                    MyDataSetItems.ListDat.Rows[i]["Result"] = MyDataSetItems.ListDatResult.Rows[i]["Result"];
                }
            }

            // ログCSVの解析とヘッダ情報（シリアル・号機・結果判定）復元
            ParseCsvLogHeader();

            return true;
        }

        private void ParseCsvLogHeader()
        {
            string origFolder = Path.GetDirectoryName(HistoryFile);
            string origFilename = Path.GetFileName(HistoryFile);
            if (string.IsNullOrEmpty(origFilename) || origFilename.Length < 15) return;

            string resultCsvPath = Path.Combine(origFolder, string.Format("{0}-log.csv", origFilename.Substring(1, 4)));
            if (!File.Exists(resultCsvPath)) return;

            string resultCsvText = _mc.ReadAllText(resultCsvPath, _mc.enc);
            string resultDate = string.Format("{0}.{1}.{2} {3}:{4}:{5}",
                origFilename.Substring(1, 4),
                origFilename.Substring(5, 2),
                origFilename.Substring(7, 2),
                origFilename.Substring(10, 2),
                origFilename.Substring(12, 2),
                origFilename.Substring(14, 2));

            List<List<string>> resultCsv = _mc.CsvToArrayList2(resultCsvText);
            foreach (List<string> columns in resultCsv)
            {
                if (columns.Count >= 7 && resultDate == columns[0])
                {
                    SerialNo = columns[1];
                    GoNo = columns[2];
                    StatusText = columns[5];

                    if (columns[6] == "Pass")
                    {
                        IsResultOkVisible = true;
                        IsResultNgVisible = false;
                    }
                    else
                    {
                        IsResultOkVisible = false;
                        IsResultNgVisible = true;
                    }
                    break;
                }
            }
        }
        #endregion

        #region Operations & Logic Commands

        #region T# Selection / Navigation
        /// <summary>
        /// 選択されている T# の変更時に各種表示項目を更新
        /// </summary>
        public void SelectTNo(int tNo)
        {
            if (tNo >= 0 && MyDataSetItems?.ListDat != null && tNo < MyDataSetItems.ListDat.Rows.Count)
            {
                CurrentTNo = tNo;
            }
        }

        public void StepNextTNo()
        {
            if (MyDataSetItems?.ListDat != null && CurrentTNo + 1 < MyDataSetItems.ListDat.Rows.Count)
            {
                CurrentTNo++;
            }
        }

        public void StepPrevTNo()
        {
            if (CurrentTNo > 0)
            {
                CurrentTNo--;
            }
        }

        private void UpdateSelectedTNoDetails()
        {
            if (MyDataSetItems?.ListDat == null || CurrentTNo < 0 || CurrentTNo >= MyDataSetItems.ListDat.Rows.Count)
            {
                return;
            }

            TestNoText = (CurrentTNo + 1).ToString();
            DataRow row = MyDataSetItems.ListDat.Rows[CurrentTNo];

            InspectTypeText = GetInspectTypeString(row["Type"]?.ToString());
            CurrentResultText = row["Result"]?.ToString();
            GuideText = row["Guide"]?.ToString();
        }

        private string GetInspectTypeString(string typeValue)
        {
            if (int.TryParse(typeValue, out int typeIndex))
            {
                if (typeIndex >= 0 && typeIndex < _defaultSettings.inspection_TypeText.Length)
                {
                    return _defaultSettings.inspection_TypeText[typeIndex];
                }
            }
            return string.Empty;
        }
        #endregion

        #region Hardware Cleanups & Form Closing
        /// <summary>
        /// 画面が閉じた際の各種リソース解放処理
        /// </summary>
        public void CleanupOnClose()
        {
            _mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            try
            {
                _dio.Exit();
                _aio.Exit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hardware exit error: {ex.Message}");
            }
        }
        #endregion

        #region View Action Requests
        public void OpenResultView()
        {
            RequestOpenResultView?.Invoke(MyDataSetItems, CurrentTNo);
        }

        public void OpenDataInputView()
        {
            RequestOpenDataInputView?.Invoke();
        }

        public void PrintScreen()
        {
            RequestPrintForm?.Invoke();
        }

        public void CloseView()
        {
            RequestCloseView?.Invoke();
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