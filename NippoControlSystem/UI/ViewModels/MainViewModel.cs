using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
//using NippoControlSystem.Services;

namespace NippoControlSystem.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();

        #region Fields & Dependencies
        //private readonly ISoundService _soundService;
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly Cyc.IO.cDio _dio = Cyc.IO.cDio.GetInstance();
        private readonly Cyc.IO.Aio _aio = Cyc.IO.Aio.GetInstance();
        private readonly Cyc.IO.Ai2Di _ai2di = Cyc.IO.Ai2Di.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        private DataSetItems _myDataSetItems;
        private int _autoTimeOut = 2; // デフォルト 2分
        private bool _stopButtonClick = false;

        private readonly float[,] _aiDataPop;
        private readonly float[] _aiDataInput;
        #endregion

        #region Properties (View Binding Targets)
        // ヘッダー・タイトル情報
        public string MainNo => _defaultSettings.MainNo;
        public string SubNo => _defaultSettings.SubNo;
        public string ItemName => _defaultSettings.Item;
        public string SerialTitle => _defaultSettings.SerialTitle;
        public string GokiTitle => _defaultSettings.GokiTitle;

        private string _serial;
        public string Serial
        {
            get => _serial;
            set { if (_serial != value) { _serial = value; OnPropertyChanged(); } }
        }

        private string _goNo;
        public string GoNo
        {
            get => _goNo;
            set { if (_goNo != value) { _goNo = value; OnPropertyChanged(); } }
        }

        private string _zuban;
        public string Zuban
        {
            get => _zuban;
            set { if (_zuban != value) { _zuban = value; OnPropertyChanged(); } }
        }

        private string _edaban;
        public string Edaban
        {
            get => _edaban;
            set { if (_edaban != value) { _edaban = value; OnPropertyChanged(); } }
        }

        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string Folder { get; set; }

        private int _currentTNo;
        public int CurrentTNo
        {
            get => _currentTNo;
            set { if (_currentTNo != value) { _currentTNo = value; OnPropertyChanged(); } }
        }

        private string _guideText;
        public string GuideText
        {
            get => _guideText;
            set { if (_guideText != value) { _guideText = value; OnPropertyChanged(); } }
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { if (_statusText != value) { _statusText = value; OnPropertyChanged(); } }
        }

        private string _powerVoltText;
        public string PowerVoltText
        {
            get => _powerVoltText;
            set { if (_powerVoltText != value) { _powerVoltText = value; OnPropertyChanged(); } }
        }

        private bool _isResultOKVisible;
        public bool IsResultOKVisible
        {
            get => _isResultOKVisible;
            set { if (_isResultOKVisible != value) { _isResultOKVisible = value; OnPropertyChanged(); } }
        }

        private bool _isResultNGVisible;
        public bool IsResultNGVisible
        {
            get => _isResultNGVisible;
            set { if (_isResultNGVisible != value) { _isResultNGVisible = value; OnPropertyChanged(); } }
        }

        private bool _isButtonsEnabled = true;
        public bool IsButtonsEnabled
        {
            get => _isButtonsEnabled;
            set { if (_isButtonsEnabled != value) { _isButtonsEnabled = value; OnPropertyChanged(); } }
        }

        public int AutoTimeOut
        {
            get => _autoTimeOut;
            set
            {
                if (_autoTimeOut != value)
                {
                    _autoTimeOut = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TimeoutMenuText));
                }
            }
        }

        public string TimeoutMenuText => string.Format("タイムアウト {0}分", AutoTimeOut);

        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            private set { _myDataSetItems = value; OnPropertyChanged(); }
        }

        public float[] AiDataAve { get; private set; }
        #endregion

        #region Events (Delegates for View Operations)
        public event EventHandler RequestClose;
        public event EventHandler RequestShowSerialView;
        public event EventHandler RequestShowSettingView;
        public event EventHandler RequestShowResultView;
        public event EventHandler RequestShowDebugView;
        public event EventHandler RequestShowVersionView;
        public event EventHandler<string> ShowMessageBoxRequested;
        public event EventHandler RequestRedrawMeters;
        #endregion

        #region Constructor
        //public MainViewModel(ISoundService soundService = null)
        public MainViewModel()
        {
            // _soundService = soundService ?? new SoundService();

            AiDataAve = new float[_defaultSettings.AiNum];
            _aiDataPop = new float[_defaultSettings.AiAveTimes, _defaultSettings.AiNum];
            _aiDataInput = new float[_defaultSettings.AiNum];
        }
        #endregion

        #region Initialization & Teardown
        public void Initialize()
        {
            Serial = libSerialNo.GetSerialNo(_defaultSettings.Serial);
            GoNo = "";

            // DataSet のロード
            MyDataSetItems = _mc.createDataSetItems(Folder, MainTitle, SubTitle);
            if (MyDataSetItems == null)
            {
                ShowMessageBoxRequested?.Invoke(this, "検査データセットの読み込みに失敗しました。");
                RequestClose?.Invoke(this, EventArgs.Empty);
                return;
            }

            IsButtonsEnabled = true;

            // ボード初期化
            int iRetDio = _dio.Init();
            if (iRetDio != 0) GuideText = "DIOボードの初期化エラー";

            int iRetAio = _aio.Init();
            if (iRetAio != 0) GuideText = "AIOボードの初期化エラー";

            _aio.setInspctLamp(0);
            _aio.setGreenLamp(0);

            int iRetAi2Di = _ai2di.Init();
            if (iRetAi2Di != 0) GuideText = "AIボードの初期化エラー";

            // 電圧設定
            string powerVolt = MyDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
            PowerVoltText = string.Format("{0}V", powerVolt == "1" ? 24 : 12);
            if (powerVolt == "1") _aio.SetPower24V();
            else _aio.SetPower12V();

            CurrentTNo = 0;
            IsResultNGVisible = false;
            IsResultOKVisible = false;

            _mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            StatusText = _mc.InspecStatString;
        }

        public void OnFormClosing()
        {
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
            _aio.setInspctLamp(0);

            _mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            // _soundService.StopSound();

            AllClear();
            _dio.Exit();
            _aio.Exit();
        }
        #endregion

        #region Business Logic & Timers
        /// <summary>
        /// アナログ入力データ取得およびメーター描画更新タイマー（timerDrawing_Tick 相当）
        /// </summary>
        public void OnTimerDrawingTick()
        {
            _aio.MultiAi(_aiDataInput);

            // シフト処理
            for (int i = 0; i < (_defaultSettings.AiAveTimes - 1); i++)
            {
                for (int j = 0; j < _defaultSettings.AiNum; j++)
                {
                    _aiDataPop[_defaultSettings.AiAveTimes - i - 1, j] = _aiDataPop[_defaultSettings.AiAveTimes - i - 2, j];
                }
            }

            for (int j = 0; j < _defaultSettings.AiNum; j++)
            {
                _aiDataPop[0, j] = _aiDataInput[j];
            }

            // 平均値算出
            for (int j = 0; j < _defaultSettings.AiNum; j++)
            {
                float work = 0f;
                for (int i = 0; i < _defaultSettings.AiAveTimes; i++)
                {
                    work += _aiDataPop[i, j];
                }
                AiDataAve[j] = work / (float)_defaultSettings.AiAveTimes;
            }

            // View にメーター再描画要求
            RequestRedrawMeters?.Invoke(this, EventArgs.Empty);

            StatusText = _mc.InspecStatString;

            // 検査中ランプ制御
            if (_mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted)
            {
                _aio.setInspctLamp(1);
            }
            else
            {
                _aio.setInspctLamp(0);
            }
        }

        /// <summary>
        /// スイッチ状態読み取りタイマー（timerReadSw_Tick 相当）
        /// </summary>
        public void OnTimerReadSwTick(int greenLampValue, int redLampValue)
        {
            if (_mc.GreenSwitchStat != 0 && greenLampValue == 0 && _aio.getGreenSw() == 0)
            {
                _mc.GreenSwitchStat = 0;
            }

            if (_mc.RedSwitchStat != 0 && redLampValue == 0 && _aio.getRedSw() == 0)
            {
                _mc.RedSwitchStat = 0;
            }

            _aio.setGreenLamp(_aio.getGreenSw());
            _aio.setRedLamp(_aio.getRedSw());
        }

        public void ExecuteStartInspection()
        {
            if (_mc.InspecStat == MeasureCondition.enumInspectStat.Stat_STOP ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalEND ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_FailEND)
            {
                _stopButtonClick = false;
                // 検査開始処理呼び出し (MainInspectionViewModel 等と連携)
            }
        }

        public void ExecuteStopInspection()
        {
            _stopButtonClick = true;
        }

        public void ExecuteClose()
        {
            if (_mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted ||
                _mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd)
            {
                ShowMessageBoxRequested?.Invoke(this, "検査中なので、終了できません。");
                return;
            }

            if (_mc.InspecStat != MeasureCondition.enumInspectStat.Stat_STOP)
            {
                _aio.setInspctLamp(0);
                _mc.SerialNo = Serial;
                _mc.GoNo = GoNo;
                _mc.Zuban = Zuban;
                _mc.Edaban = Edaban;

                RequestShowSerialView?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }

        public void OpenManual()
        {
            string filePath = Path.Combine(_defaultSettings.ApplicationFloder, _defaultSettings.ManualPath);
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ee)
            {
                ShowMessageBoxRequested?.Invoke(this, string.Format("操作マニュアルを表示できません。\n\n表示ファイル：{0}\n理由：{1}", filePath, ee.Message));
            }
        }

        private void AllClear()
        {
            int iDoLength = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;
            for (int i = 0; i < Default.DioNames.Length; i++)
            {
                for (int j = 0; j < Default.DioNums[i]; j++)
                {
                    int iPOS = i * Default.DioNums[0] + j;
                    _nio_OUT(iPOS, Cyc.IO.NippoDIO.IO_STAT.oOP);
                }
            }

            for (int j = 0; j < Default.AoNum; j++)
            {
                _aio.SingleAoEx(j, 0f);
            }

            for (int j = 0; j < Default.AoSwichNum; j++)
            {
                _ai2di.NippoAIO_SW(j, 0);
            }

            _aio.SetPower12V();
        }

        private void _nio_OUT(int pos, Cyc.IO.NippoDIO.IO_STAT stat)
        {
            // ニッポーDIOボード出力のヘルパーメソッド呼び出し
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