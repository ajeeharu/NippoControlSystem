using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class PinNameViewModel : INotifyPropertyChanged
    {
        #region Fields
        private int _selectPin;
        private string _pinName = "";
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
        /// 端子名[cite: 9]
        /// </summary>
        public string PinName
        {
            get => _pinName;
            set { if (_pinName != value) { _pinName = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 確定処理完了によるダイアログ閉じ要求イベント（DialogResult.OK 相当）[cite: 9]
        /// </summary>
        public event EventHandler RequestCloseDialogOk;

        /// <summary>
        /// キャンセル処理によるダイアログ閉じ要求イベント[cite: 9]
        /// </summary>
        public event EventHandler RequestCloseDialogCancel;
        #endregion

        #region Constructors
        public PinNameViewModel()
        {
        }

        public PinNameViewModel(int selectPin, string pinName)
        {
            SelectPin = selectPin;
            PinName = pinName ?? "";
        }
        #endregion

        #region Logic
        /// <summary>
        /// OKボタン押下時またはEnterキー入力時の確定処理[cite: 9]
        /// </summary>
        public void Submit()
        {
            RequestCloseDialogOk?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Escapeキー押下等によるキャンセル処理[cite: 9]
        /// </summary>
        public void Cancel()
        {
            RequestCloseDialogCancel?.Invoke(this, EventArgs.Empty);
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