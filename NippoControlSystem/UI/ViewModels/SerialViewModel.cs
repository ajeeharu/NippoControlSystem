using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.Persistence;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// シリアル番号・号機入力画面（SerialView）用 ViewModel
    /// </summary>
    public class SerialViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _mainTitle = string.Empty;
        private string _subTitle = string.Empty;
        private string _folder = string.Empty;
        private string _serialNo = string.Empty;
        private string _goNo = string.Empty;
        private string _serialTitle = string.Empty;
        private string _gokiTitle = string.Empty;
        private DataSetItems _myDataSetItems;
        private readonly Aio _aio;

        // ハードウェアSW状態保持用
        private int _lastGreenSwitch = 0;
        private int _lastRedSwitch = 0;
        private const bool WorkingLampON = false; // 稼働時点灯設定（20160907）
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// メインタイトル
        /// </summary>
        public string MainTitle
        {
            get => _mainTitle;
            set { if (_mainTitle != value) { _mainTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// サブタイトル
        /// </summary>
        public string SubTitle
        {
            get => _subTitle;
            set { if (_subTitle != value) { _subTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 保存先フォルダパス
        /// </summary>
        public string Folder
        {
            get => _folder;
            set { if (_folder != value) { _folder = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 製造番号 (SerialNo)
        /// </summary>
        public string SerialNo
        {
            get => _serialNo;
            set
            {
                if (_serialNo != value)
                {
                    _serialNo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSave));
                }
            }
        }

        /// <summary>
        /// 号機番号 (GoNo)
        /// </summary>
        public string GoNo
        {
            get => _goNo;
            set
            {
                if (_goNo != value)
                {
                    _goNo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSave));
                }
            }
        }

        /// <summary>
        /// シリアル番号ラベルタイトル
        /// </summary>
        public string SerialTitle
        {
            get => _serialTitle;
            private set { if (_serialTitle != value) { _serialTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 号機タイトル
        /// </summary>
        public string GokiTitle
        {
            get => _gokiTitle;
            private set { if (_gokiTitle != value) { _gokiTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// データセット
        /// </summary>
        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            set { if (_myDataSetItems != value) { _myDataSetItems = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 保存処理を実行できるか（必須項目入力チェック）
        /// </summary>
        public bool CanSave => !string.IsNullOrWhiteSpace(SerialNo) && !string.IsNullOrWhiteSpace(GoNo);
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 保存成功およびダイアログ終了要求イベント
        /// </summary>
        public event EventHandler RequestCloseSuccess;

        /// <summary>
        /// キャンセル要求イベント
        /// </summary>
        public event EventHandler RequestCancel;

        /// <summary>
        /// メッセージボックス表示要求イベント (メッセージ内容, タイトル)
        /// </summary>
        public event Action<string, string> RequestShowMessage;

        /// <summary>
        /// メインフォーム画面のスクリーンショット取得要求イベント (取得後のBitmapを返すデリゲート)
        /// </summary>
        public event Func<Bitmap> RequestCaptureMainScreen;

        /// <summary>
        /// メインフォームへのシリアル/号機更新要求イベント (Serial, GoNo)
        /// </summary>
        public event Action<string, string> RequestUpdateMainFormSerial;
        #endregion

        #region Constructor & Initialization
        public SerialViewModel(Aio aio)
        {
            InitializeSettings();
            _aio = aio;
        }

        /// <summary>
        /// 初期設定とMeasureConditionからの初期値読み込み
        /// </summary>
        public void InitializeSettings()
        {
            var defaultSettings = Settings.GetInstance();
            var mc = MeasureCondition.GetInstance();

            SerialTitle = string.Format("{0}を入力して下さい。", defaultSettings.SerialTitle);
            GokiTitle = string.Format("{0}を入力して下さい。", defaultSettings.GokiTitle);

            SerialNo = mc.SerialNo ?? string.Empty;
            GoNo = mc.GoNo ?? string.Empty;
        }
        #endregion

        #region User & Hardware Operations
        /// <summary>
        /// 保存＆終了コマンド処理
        /// </summary>
        public bool SaveAndClose()
        {
            var defaultSettings = Settings.GetInstance();
            var mc = MeasureCondition.GetInstance();

            if (string.IsNullOrWhiteSpace(SerialNo))
            {
                RequestShowMessage?.Invoke(
                    string.Format("{0}を入力して下さい。", defaultSettings.SerialTitle),
                    defaultSettings.ApplicationName
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(GoNo))
            {
                RequestShowMessage?.Invoke(
                    string.Format("{0}を入力して下さい。", defaultSettings.GokiTitle),
                    defaultSettings.ApplicationName
                );
                return false;
            }

            mc.SerialNo = SerialNo;
            mc.GoNo = GoNo;

            // スクリーンショット保存
            Bitmap bm = RequestCaptureMainScreen?.Invoke();
            if (bm != null)
            {
                string resultBitMapPath = Path.Combine(Folder ?? string.Empty, string.Format("A{0:yyyyMMdd_HHmmss}.bmp", mc.TestEndDT));
                try
                {
                    bm.Save(resultBitMapPath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to save screenshot: {ex.Message}");
                }
            }

            // CSV保存
            mc.saveResultCsv(MyDataSetItems, Folder);

            // メインフォーム側のシリアル更新処理
            string updatedSerial = mc.SerialNo;
            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalEND)
            {
                updatedSerial = libSerialNo.AddSerialNo(defaultSettings.Serial, defaultSettings.SerialSuffixLength, mc.SerialNo);
            }
            RequestUpdateMainFormSerial?.Invoke(updatedSerial, mc.GoNo);

            RequestCloseSuccess?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public void Cancel()
        {
            RequestCancel?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// ハードウェア（AIO/DIO）スイッチ状態の定期読み取り処理（Timer Tickから呼び出し）
        /// </summary>
        public void OnHardwareTimerTick()
        {
            int newGreenSwitch = _aio.getGreenSw();
            int newRedSwitch = _aio.getRedSw();

            // ランプ制御
            _aio.setGreenLamp(newGreenSwitch ^ (WorkingLampON ? 1 : 0));
            _aio.setRedLamp(newRedSwitch ^ (WorkingLampON ? 1 : 0));

            // 緑ボタン押下検知 (1 -> 0 への立ち下がり)
            if (newGreenSwitch == 0 && _lastGreenSwitch == 1)
            {
                if (CanSave)
                {
                    SaveAndClose();
                }
            }

            // 赤ボタン押下検知 (1 -> 0 への立ち下がり)
            if (newRedSwitch == 0 && _lastRedSwitch == 1)
            {
                Cancel();
            }

            _lastGreenSwitch = newGreenSwitch;
            _lastRedSwitch = newRedSwitch;
        }

        /// <summary>
        /// 画面終了時のハードウェアランプ消灯処理
        /// </summary>
        public void CleanupHardware()
        {
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
        }
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