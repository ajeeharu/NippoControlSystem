using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class PinAnalogInputViewModel : INotifyPropertyChanged
    {
        #region Fields & Dependencies
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();

        private int _selectPin;
        private string _lowerValue = "";
        private string _upperValue = "";
        private string _errorMessage = "";
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// 選択されたピン番号
        /// </summary>
        public int SelectPin
        {
            get => _selectPin;
            set { if (_selectPin != value) { _selectPin = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 下限値文字列[cite: 6]
        /// </summary>
        public string LowerValue
        {
            get => _lowerValue;
            set { if (_lowerValue != value) { _lowerValue = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 上限値文字列[cite: 6]
        /// </summary>
        public string UpperValue
        {
            get => _upperValue;
            set { if (_upperValue != value) { _upperValue = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 画面上に表示する注意・ガイダンスメッセージ[cite: 6]
        /// </summary>
        public string NoticeText => string.Format(
            "アナログ入力の期待値 範囲（電圧の上限と下限）を入力してください。 （{0}～{1}V)",
            _defaultSettings.AiVoltMin,
            _defaultSettings.AiVoltMax);

        /// <summary>
        /// バリデーション時のエラーメッセージ（必要に応じてUI側でバインディングまたはダイアログ表示）
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            private set { _errorMessage = value; OnPropertyChanged(); }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 入力検証エラー時のメッセージ表示要求イベント[cite: 6]
        /// </summary>
        public event EventHandler<string> ShowValidationErrorRequested;

        /// <summary>
        /// 確定処理完了によるダイアログ閉じ要求イベント（DialogResult.OK 相当）[cite: 6]
        /// </summary>
        public event EventHandler RequestCloseDialogOk;

        /// <summary>
        /// キャンセル処理によるダイアログ閉じ要求イベント[cite: 6]
        /// </summary>
        public event EventHandler RequestCloseDialogCancel;
        #endregion

        #region Constructor
        public PinAnalogInputViewModel()
        {
        }

        public PinAnalogInputViewModel(int selectPin, string lowerValue, string upperValue)
        {
            SelectPin = selectPin;
            LowerValue = lowerValue ?? "";
            UpperValue = upperValue ?? "";
        }
        #endregion

        #region Business Logic & Validation
        /// <summary>
        /// OKボタン押下時またはEnterキー押下時の検証・確定処理[cite: 6]
        /// </summary>
        public bool ValidateAndSubmit()
        {
            double lower = double.MinValue;
            double upper = double.MaxValue;

            // 下限値の検証[cite: 6]
            if (!string.IsNullOrEmpty(LowerValue))
            {
                if (!double.TryParse(LowerValue, out lower))
                {
                    ShowError("下限値に正しい数値を入力してください。");
                    return false;
                }

                if (!(lower >= _defaultSettings.AiVoltMin && lower <= _defaultSettings.AiVoltMax))
                {
                    ShowError(string.Format("下限値は {0} から {1} までの数値を入力してください。", _defaultSettings.AiVoltMin, _defaultSettings.AiVoltMax));
                    return false;
                }
            }

            // 上限値の検証[cite: 6]
            if (!string.IsNullOrEmpty(UpperValue))
            {
                if (!double.TryParse(UpperValue, out upper))
                {
                    ShowError("上限値に正しい数値を入力してください。");
                    return false;
                }

                if (!(upper >= _defaultSettings.AiVoltMin && upper <= _defaultSettings.AiVoltMax))
                {
                    ShowError(string.Format("上限値は {0} から {1} までの数値を入力してください。", _defaultSettings.AiVoltMin, _defaultSettings.AiVoltMax));
                    return false;
                }
            }

            // 相対値の検証（上限と下限の大小関係）[cite: 6]
            if (!(lower == double.MinValue && upper == double.MaxValue))
            {
                if (lower > upper)
                {
                    ShowError("上限値は、下限値より大きい数値を入力してください。");
                    return false;
                }
            }

            // 検証成功時、ダイアログ閉じイベントをトリガー[cite: 6]
            RequestCloseDialogOk?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Escapeキー押下等によるキャンセル処理[cite: 6]
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