using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NippoControlSystem.UI.ViewModels
{
    public enum DebugStartOption
    {
        ForceToEnd,  // radioButton1_0: 強制的に最後の項目まで検査
        FromCurrent, // radioButton1_1: 現在のカーソル行から開始
        SelectNo     // radioButton1_2: 開始位置を指定して開始
    }

    public class DebugViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataSetItems _myDataSetItems;
        private int _currentTNo;
        private MeasureCondition.enumInspectStat _startStat;

        private DebugStartOption _selectedStartOption = DebugStartOption.ForceToEnd;

        private int _greenSwitchLampValue = 1;
        private int _redSwitchLampValue = 1;

        // ドメイン/ハードウェア依存クラスの参照
        private readonly Cyc.IO.Aio _aio = Cyc.IO.Aio.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        // 稼働時点灯フラグに基づくランプON/OFF値設定
        private const bool WorkingLampON = false;
        private const int OpSwLampOn = WorkingLampON ? 0 : 1;
        private const int OpSwLampOff = 0;

        // ハードウェアスイッチ監視用タイマー
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
        /// ラジオボタン選択オプション
        /// </summary>
        public DebugStartOption SelectedStartOption
        {
            get => _selectedStartOption;
            set
            {
                if (_selectedStartOption != value)
                {
                    _selectedStartOption = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsForceToEndSelected));
                    OnPropertyChanged(nameof(IsFromCurrentSelected));
                    OnPropertyChanged(nameof(IsSelectNoSelected));
                }
            }
        }

        // View 側の RadioButton とバインドするためのヘルパープロパティ
        public bool IsForceToEndSelected
        {
            get => SelectedStartOption == DebugStartOption.ForceToEnd;
            set { if (value) SelectedStartOption = DebugStartOption.ForceToEnd; }
        }

        public bool IsFromCurrentSelected
        {
            get => SelectedStartOption == DebugStartOption.FromCurrent;
            set { if (value) SelectedStartOption = DebugStartOption.FromCurrent; }
        }

        public bool IsSelectNoSelected
        {
            get => SelectedStartOption == DebugStartOption.SelectNo;
            set { if (value) SelectedStartOption = DebugStartOption.SelectNo; }
        }

        /// <summary>
        /// 緑スイッチの LampValue
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
        /// 赤スイッチの LampValue
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
        public ICommand GreenSwitchDownCommand { get; }
        public ICommand GreenSwitchUpCommand { get; }
        public ICommand RedSwitchDownCommand { get; }
        public ICommand RedSwitchUpCommand { get; }
        #endregion

        #region Events / Actions for View Interaction
        /// <summary>
        /// 画面を閉じる要求 (引数: DialogResult が true なら OK, false なら Cancel)
        /// </summary>
        public Action<bool> RequestClose { get; set; }

        /// <summary>
        /// 番号指定ダイアログ (DebugNoView) の表示要求
        /// Func<DataSetItems, (bool dialogResult, int newTNo, MeasureCondition.enumInspectStat newStat)>
        /// </summary>
        public Func<DataSetItems, (bool dialogResult, int newTNo, MeasureCondition.enumInspectStat newStat)> RequestShowDebugNoDialog { get; set; }
        #endregion

        #region Constructor
        public DebugViewModel()
        {
            StartCommand = new RelayCommand(OnStart);
            CancelCommand = new RelayCommand(OnCancel);

            GreenSwitchDownCommand = new RelayCommand(OnGreenSwitchMouseDown);
            GreenSwitchUpCommand = new RelayCommand(OnGreenSwitchMouseUp);
            RedSwitchDownCommand = new RelayCommand(OnRedSwitchMouseDown);
            RedSwitchUpCommand = new RelayCommand(OnRedSwitchMouseUp);

            InitializeTimer();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 画面ロード時の初期化処理 (frmDebug_Load 相当)
        /// </summary>
        public void Initialize()
        {
            if (_timerReadSw != null)
            {
                _timerReadSw.Enabled = true;
            }
        }

        /// <summary>
        /// 画面が閉じられた際のクリーンアップ処理 (frmDebug_FormClosed 相当)
        /// </summary>
        public void Cleanup()
        {
            if (_timerReadSw != null)
            {
                _timerReadSw.Enabled = false;
            }

            // Lamp消灯
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
        }
        #endregion

        #region Private Methods
        private void InitializeTimer()
        {
            _timerReadSw = new System.Windows.Forms.Timer
            {
                Interval = 100
            };
            _timerReadSw.Tick += TimerReadSw_Tick;
        }

        private void OnStart()
        {
            switch (SelectedStartOption)
            {
                case DebugStartOption.ForceToEnd:
                    // 強制的に最後の項目まで検査する
                    CurrentTNo = 0; // 最初から
                    StartStat = MeasureCondition.enumInspectStat.Stat_ForceToEnd;
                    _timerReadSw.Enabled = false;
                    RequestClose?.Invoke(true); // DialogResult.OK
                    break;

                case DebugStartOption.FromCurrent:
                    // 現在のカーソル行から検査を開始する
                    StartStat = MeasureCondition.enumInspectStat.Stat_MidStarted;
                    _timerReadSw.Enabled = false;
                    RequestClose?.Invoke(true); // DialogResult.OK
                    break;

                case DebugStartOption.SelectNo:
                    // 開始位置を指定して検査を開始する
                    _timerReadSw.Enabled = false;
                    if (RequestShowDebugNoDialog != null)
                    {
                        var result = RequestShowDebugNoDialog.Invoke(MyDataSetItems);
                        if (result.dialogResult)
                        {
                            CurrentTNo = result.newTNo;
                            StartStat = result.newStat;
                            RequestClose?.Invoke(true); // DialogResult.OK
                        }
                    }
                    break;
            }
        }

        private void OnCancel()
        {
            StartStat = MeasureCondition.enumInspectStat.Stat_STOP;
            if (_timerReadSw != null)
            {
                _timerReadSw.Enabled = false;
            }
            RequestClose?.Invoke(false); // DialogResult.Cancel
        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            if (GreenSwitchDown())
            {
                OnStart();
                return;
            }
            else if (RedSwitchDown())
            {
                OnCancel();
                return;
            }

            if (_mc.GreenSwitchStat != 0)
            {
                if (GreenSwitchLampValue == 0 && _aio.getGreenSw() == 0)
                {
                    _mc.GreenSwitchStat = 0;
                }
            }

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

        #region Switch Label Mouse Event Handlers
        private void OnGreenSwitchMouseDown()
        {
            GreenSwitchLampValue = 0;
            _aio.setGreenLamp(OpSwLampOn);
        }

        private void OnGreenSwitchMouseUp()
        {
            GreenSwitchLampValue = 1;
            _aio.setGreenLamp(OpSwLampOff);
        }

        private void OnRedSwitchMouseDown()
        {
            RedSwitchLampValue = 0;
            _aio.setRedLamp(OpSwLampOn);
        }

        private void OnRedSwitchMouseUp()
        {
            RedSwitchLampValue = 1;
            _aio.setRedLamp(OpSwLampOff);
        }
        #endregion
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