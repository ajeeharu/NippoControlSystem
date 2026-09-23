using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NippoControlSystem.UI.ViewModels
{
    public class DataInputLimitViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _testNo = string.Empty;
        private string _title = "入出力検査";
        private string _guide = string.Empty;
        private string _inspectionType = string.Empty;
        private string _result = string.Empty;
        private bool _isBusy;
        private int _pageNo = 1;
        private int _totalPages = 2;
        private string _volt = string.Empty;

        // 電流上下限値フィールド (iDb, iDc, iDs)
        private double _iDbLowLimit;
        private double _iDbHighLimit;
        private double _iDcLowLimit;
        private double _iDcHighLimit;
        private double _iDsLowLimit;
        private double _iDsHighLimit;

        // スイッチランプ表示状態
        private bool _isRedSwitchOn;
        private bool _isGreenSwitchOn;
        #endregion

        #region Properties
        public string TestNo
        {
            get => _testNo;
            set { if (_testNo != value) { _testNo = value; OnPropertyChanged(); } }
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

        public string InspectionType
        {
            get => _inspectionType;
            set { if (_inspectionType != value) { _inspectionType = value; OnPropertyChanged(); } }
        }

        public string Result
        {
            get => _result;
            set { if (_result != value) { _result = value; OnPropertyChanged(); } }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
        }

        public int PageNo
        {
            get => _pageNo;
            set { if (_pageNo != value) { _pageNo = value; OnPropertyChanged(); } }
        }

        public int TotalPages
        {
            get => _totalPages;
            set { if (_totalPages != value) { _totalPages = value; OnPropertyChanged(); } }
        }

        public string Volt
        {
            get => _volt;
            set { if (_volt != value) { _volt = value; OnPropertyChanged(); } }
        }

        // iDb 電流閾値
        public double IDbLowLimit
        {
            get => _iDbLowLimit;
            set { if (_iDbLowLimit != value) { _iDbLowLimit = value; OnPropertyChanged(); } }
        }

        public double IDbHighLimit
        {
            get => _iDbHighLimit;
            set { if (_iDbHighLimit != value) { _iDbHighLimit = value; OnPropertyChanged(); } }
        }

        // iDc 電流閾値
        public double IDcLowLimit
        {
            get => _iDcLowLimit;
            set { if (_iDcLowLimit != value) { _iDcLowLimit = value; OnPropertyChanged(); } }
        }

        public double IDcHighLimit
        {
            get => _iDcHighLimit;
            set { if (_iDcHighLimit != value) { _iDcHighLimit = value; OnPropertyChanged(); } }
        }

        // iDs 電流閾値
        public double IDsLowLimit
        {
            get => _iDsLowLimit;
            set { if (_iDsLowLimit != value) { _iDsLowLimit = value; OnPropertyChanged(); } }
        }

        public double IDsHighLimit
        {
            get => _iDsHighLimit;
            set { if (_iDsHighLimit != value) { _iDsHighLimit = value; OnPropertyChanged(); } }
        }

        // Switch Lamp 状態
        public bool IsRedSwitchOn
        {
            get => _isRedSwitchOn;
            set { if (_isRedSwitchOn != value) { _isRedSwitchOn = value; OnPropertyChanged(); } }
        }

        public bool IsGreenSwitchOn
        {
            get => _isGreenSwitchOn;
            set { if (_isGreenSwitchOn != value) { _isGreenSwitchOn = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Commands
        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        #endregion

        #region Constructor
        public DataInputLimitViewModel()
        {
            // コマンドの初期化例
            NextCommand = new RelayCommand(OnNext);
            PreviousCommand = new RelayCommand(OnPrevious);
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(OnPrint);
            NextPageCommand = new RelayCommand(OnNextPage);
            PreviousPageCommand = new RelayCommand(OnPreviousPage);
        }
        #endregion

        #region Command Methods
        private void OnNext()
        {
            // 「次へ」ボタン処理
        }

        private void OnPrevious()
        {
            // 「前へ」ボタン処理
        }

        private void OnClose()
        {
            // 画面終了処理
        }

        private void OnPrint()
        {
            // 印刷処理
        }

        private void OnNextPage()
        {
            if (PageNo < TotalPages)
            {
                PageNo++;
            }
        }

        private void OnPreviousPage()
        {
            if (PageNo > 1)
            {
                PageNo--;
            }
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

    #region Helper RelayCommand
    public class RelayCommand(System.Action execute, System.Func<bool>? canExecute = null) : ICommand
    {
        private readonly System.Action _execute = execute ?? throw new System.ArgumentNullException(nameof(execute));
        private readonly System.Func<bool>? _canExecute = canExecute;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();

        public event System.EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
    #endregion
}