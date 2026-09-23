using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NippoControlSystem.UI.ViewModels
{
    public class DebugNoViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataSetItems _myDataSetItems;
        private int _currentTNo;
        private int _selectedTNoIndex = -1;
        private MeasureCondition.enumInspectStat _startStat;

        private int _greenSwitchLampValue;
        private int _redSwitchLampValue;

        // ドメイン/ハードウェア依存クラスの参照
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly Cyc.IO.Aio _aio = Cyc.IO.Aio.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        // 監視用タイマー (WinForms Timer 等を抽象化または直接利用)
        private System.Windows.Forms.Timer _timerReadSw;
        #endregion

        #region Properties
        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            set
            {
                if (_myDataSetItems != value)
                {
                    _myDataSetItems = value;
                    OnPropertyChanged();
                    InitializeTNoItems();
                }
            }
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
                }
            }
        }

        public int SelectedTNoIndex
        {
            get => _selectedTNoIndex;
            set
            {
                if (_selectedTNoIndex != value)
                {
                    _selectedTNoIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public MeasureCondition.enumInspectStat StartStat
        {
            get => _startStat;
            set
            {
                if (_startStat != value)
                {
                    _startStat = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ComboBox 表示用の T# 番号リスト (1, 2, 3...)
        /// </summary>
        public ObservableCollection<string> TNoItems { get; } = new ObservableCollection<string>();

        /// <summary>
        /// 緑スイッチの LampValue (ON: 1, OFF: 0)
        /// </summary>
        public int GreenSwitchLampValue
        {
            get => _greenSwitchLampValue;
            set
            {
                if (_greenSwitchLampValue != value)
                {
                    _greenSwitchLampValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 赤スイッチの LampValue (ON: 1, OFF: 0)
        /// </summary>
        public int RedSwitchLampValue
        {
            get => _redSwitchLampValue;
            set
            {
                if (_redSwitchLampValue != value)
                {
                    _redSwitchLampValue = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand StartCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Events / Actions for View Interaction
        /// <summary>
        /// ダイアログを閉じる要求 (引数: DialogResult が true なら OK, false なら Cancel)
        /// </summary>
        public Action<bool> RequestClose { get; set; }

        /// <summary>
        /// エラーメッセージ表示要求
        /// </summary>
        public Action<string, string> RequestShowMessage { get; set; }
        #endregion

        #region Constructor
        public DebugNoViewModel()
        {
            StartCommand = new RelayCommand(OnStart);
            CancelCommand = new RelayCommand(OnCancel);

            InitializeTimer();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 画面ロード時の初期化処理
        /// </summary>
        public void Initialize()
        {
            InitializeTNoItems();
            SelectedTNoIndex = CurrentTNo;

            _timerReadSw?.Enabled = true;
        }

        /// <summary>
        /// 画面が閉じられた際のクリーンアップ処理 (frmDebugNo_FormClosed 相当)
        /// </summary>
        public void Cleanup()
        {
            _timerReadSw?.Enabled = false;

            // Lamp消灯
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
        }

        /// <summary>
        /// コンボボックスのテキスト入力からインデックスを同期 (Validating 相当)
        /// </summary>
        public void ValidateAndSetTNoText(string text)
        {
            for (int i = 0; i < TNoItems.Count; i++)
            {
                if (TNoItems[i] == text)
                {
                    SelectedTNoIndex = i;
                    return;
                }
            }
        }
        #endregion

        #region Private Methods
        private void InitializeTNoItems()
        {
            TNoItems.Clear();
            if (MyDataSetItems?.ListDat == null) return;

            int listItemsCount = MyDataSetItems.ListDat.Count;
            for (int i = 0; i < listItemsCount; i++)
            {
                TNoItems.Add((i + 1).ToString());
            }
        }

        private void InitializeTimer()
        {
            _timerReadSw = new System.Windows.Forms.Timer
            {
                Interval = 100 // 必要に応じたインターバル設定
            };
            _timerReadSw.Tick += TimerReadSw_Tick;
        }

        private void OnStart()
        {
            if (SelectedTNoIndex == -1)
            {
                RequestShowMessage?.Invoke("T#が範囲外です。", _defaultSettings.ApplicationName);
                return;
            }

            CurrentTNo = SelectedTNoIndex;
            StartStat = MeasureCondition.enumInspectStat.Stat_MidStarted;

            _timerReadSw.Enabled = false;
            RequestClose?.Invoke(true); // DialogResult.OK
        }

        private void OnCancel()
        {
            StartStat = MeasureCondition.enumInspectStat.Stat_STOP;

            _timerReadSw.Enabled = false;
            RequestClose?.Invoke(false); // DialogResult.Cancel
        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            if (GreenSwitchDown())
            {
                // GreenSwが押された
                OnStart();
                return;
            }
            else if (RedSwitchDown())
            {
                // RedSwが押された
                OnCancel();
                return;
            }

            // OFFしたら、GreenSwitchStat(ON中)をリセットする
            if (_mc.GreenSwitchStat != 0)
            {
                if (GreenSwitchLampValue == 0 && _aio.getGreenSw() == 0)
                {
                    _mc.GreenSwitchStat = 0;
                }
            }

            // OFFしたら、RedSwitchStat(ON中)をリセットする
            if (_mc.RedSwitchStat != 0)
            {
                if (RedSwitchLampValue == 0 && _aio.getRedSw() == 0)
                {
                    _mc.RedSwitchStat = 0;
                }
            }
        }

        private bool GreenSwitchDown()
        {
            if (_mc.GreenSwitchStat != 0)
            {
                if (GreenSwitchLampValue == 0 && _aio.getGreenSw() == 0)
                {
                    _mc.GreenSwitchStat = 0;
                }
                return false;
            }
            else
            {
                if (GreenSwitchLampValue == 0 && _aio.getGreenSw() == 0)
                {
                    return false;
                }
            }

            _mc.GreenSwitchStat = 1;
            GreenSwitchLampValue = 0;
            return true;
        }

        private bool RedSwitchDown()
        {
            if (_mc.RedSwitchStat != 0)
            {
                if (RedSwitchLampValue == 0 && _aio.getRedSw() == 0)
                {
                    _mc.RedSwitchStat = 0;
                }
                return false;
            }
            else
            {
                if (RedSwitchLampValue == 0 && _aio.getRedSw() == 0)
                {
                    return false;
                }
            }

            _mc.RedSwitchStat = 1;
            RedSwitchLampValue = 0;
            return true;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }
        #endregion
    }
}