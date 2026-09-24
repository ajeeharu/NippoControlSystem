using NippoControlSystem.Infrastructure.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class PinAnalogOutputViewModel : INotifyPropertyChanged
    {
        #region Fields & Dependencies
        private readonly Settings _defaultSettings = Settings.GetInstance();

        private int _selectPin;
        private string _aoValue = "";
        private string _aoSw = "";
        private int _selectedAoSwIndex = -1;
        private string _errorMessage = "";
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// 選択されたピン番号
        /// </summary>
        public int SelectPin
        {
            get => _selectPin;
            set
            {
                if (_selectPin != value)
                {
                    _selectPin = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsVoltInputVisible));
                }
            }
        }

        /// <summary>
        /// 出力電圧値文字列
        /// </summary>
        public string AoValue
        {
            get => _aoValue;
            set { if (_aoValue != value) { _aoValue = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// アナログ出力スイッチ状態文字列 (AoSwichStat)
        /// </summary>
        public string AoSw
        {
            get => _aoSw;
            set
            {
                if (_aoSw != value)
                {
                    _aoSw = value;
                    OnPropertyChanged();
                    UpdateSelectedAoSwIndexFromStat();
                }
            }
        }

        /// <summary>
        /// コンボボックス用の選択肢リスト (AoSwichGuide)
        /// </summary>
        public List<string> AoSwichGuideList => _defaultSettings.AoSwichGuide?.ToList() ?? [];

        /// <summary>
        /// コンボボックスの選択インデックス
        /// </summary>
        public int SelectedAoSwIndex
        {
            get => _selectedAoSwIndex;
            set { if (_selectedAoSwIndex != value) { _selectedAoSwIndex = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 電圧入力項目（TextBox / Label）の表示・非表示判定（SelectPin < 2 の場合表示）
        /// </summary>
        public bool IsVoltInputVisible => SelectPin < 2;

        /// <summary>
        /// 画面ガイダンスメッセージ
        /// </summary>
        public string NoticeVoltText => string.Format("出力電圧({0}～{1}V)", _defaultSettings.AoVoltMin, _defaultSettings.AoVoltMax);

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            private set { _errorMessage = value; OnPropertyChanged(); }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 入力検証エラー時のメッセージ表示要求イベント
        /// </summary>
        public event EventHandler<string> ShowValidationErrorRequested;

        /// <summary>
        /// 確定処理完了によるダイアログ閉じ要求イベント（DialogResult.OK 相当）
        /// </summary>
        public event EventHandler RequestCloseDialogOk;

        /// <summary>
        /// キャンセル処理によるダイアログ閉じ要求イベント
        /// </summary>
        public event EventHandler RequestCloseDialogCancel;
        #endregion

        #region Constructors
        public PinAnalogOutputViewModel()
        {
        }

        public PinAnalogOutputViewModel(int selectPin, string aoValue, string aoSw)
        {
            SelectPin = selectPin;
            AoValue = aoValue ?? "";
            AoSw = aoSw ?? "";
            InitializeState();
        }
        #endregion

        #region Initialization & Logic
        public void InitializeState()
        {
            UpdateSelectedAoSwIndexFromStat();

            if (IsVoltInputVisible)
            {
                if (!string.IsNullOrEmpty(AoValue) && double.TryParse(AoValue, out _))
                {
                    // AoValue は維持
                }
                else
                {
                    AoValue = null;
                }
            }
        }

        private void UpdateSelectedAoSwIndexFromStat()
        {
            int index = 0;
            if (_defaultSettings.AoSwichStat != null)
            {
                for (int i = 0; i < _defaultSettings.AoSwichStat.Length; i++)
                {
                    if (AoSw == _defaultSettings.AoSwichStat[i])
                    {
                        index = i;
                        break;
                    }
                }
            }
            SelectedAoSwIndex = index;
        }

        /// <summary>
        /// OKボタン押下時または確定キー入力時の検証・確定処理
        /// </summary>
        public bool ValidateAndSubmit()
        {
            // 電圧値入力項目の表示時かつ値が存在する場合の検証
            if (IsVoltInputVisible && !string.IsNullOrEmpty(AoValue))
            {
                if (!double.TryParse(AoValue, out double outVoltValue))
                {
                    ShowError("出力電圧値に正しい数値を入力してください。");
                    return false;
                }

                if (!(outVoltValue >= _defaultSettings.AoVoltMin && outVoltValue <= _defaultSettings.AoVoltMax))
                {
                    ShowError(string.Format("出力電圧値 {0} から {1} までの数値を入力してください。", _defaultSettings.AoVoltMin, _defaultSettings.AoVoltMax));
                    return false;
                }
            }

            // コンボボックス選択値から AoSw を更新
            if (SelectedAoSwIndex == -1 || _defaultSettings.AoSwichStat == null || SelectedAoSwIndex >= _defaultSettings.AoSwichStat.Length)
            {
                AoSw = null;
            }
            else
            {
                AoSw = _defaultSettings.AoSwichStat[SelectedAoSwIndex];
            }

            // 検証成功時、ダイアログ閉じイベントを発行
            RequestCloseDialogOk?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Escapeキー押下等によるキャンセル処理
        /// </summary>
        public void Cancel()
        {
            RequestCloseDialogCancel?.Invoke(this, EventArgs.Empty);
        }

        private void ShowError(string message)
        {
            ErrorMessage = message;
            ShowValidationErrorRequested?.Invoke(this, message);
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