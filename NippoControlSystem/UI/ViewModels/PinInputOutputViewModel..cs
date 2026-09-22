using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    public class PinInputOutputViewModel : INotifyPropertyChanged
    {
        #region Fields & Dependencies
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();

        private int _selectPin;
        private string _pinIO = "";
        private string _iDpH = "";
        private string _iDpL = "";
        private string _iDsH = "";
        private string _iDsL = "";
        private int _selectedPinIOIndex = -1;
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
        /// デジタル入出力ステータス文字列 (DioStat)[cite: 6]
        /// </summary>
        public string PinIO
        {
            get => _pinIO;
            set
            {
                if (_pinIO != value)
                {
                    _pinIO = value;
                    OnPropertyChanged();
                    UpdateSelectedPinIOIndexFromStat();
                    UpdateGroupEnableStates();
                }
            }
        }

        /// <summary>
        /// iDp 電流上限値[cite: 6]
        /// </summary>
        public string IDpH
        {
            get => _iDpH;
            set { if (_iDpH != value) { _iDpH = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// iDp 電流下限値[cite: 6]
        /// </summary>
        public string IDpL
        {
            get => _iDpL;
            set { if (_iDpL != value) { _iDpL = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// iDs 電流上限値[cite: 6]
        /// </summary>
        public string IDsH
        {
            get => _iDsH;
            set { if (_iDsH != value) { _iDsH = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// iDs 電流下限値[cite: 6]
        /// </summary>
        public string IDsL
        {
            get => _iDsL;
            set { if (_iDsL != value) { _iDsL = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// コンボボックス用の選択肢リスト (DioStatGuide)[cite: 6]
        /// </summary>
        public List<string> DioStatGuideList => _defaultSettings.DioStatGuide?.ToList() ?? new List<string>();

        /// <summary>
        /// コンボボックスの選択インデックス[cite: 6]
        /// </summary>
        public int SelectedPinIOIndex
        {
            get => _selectedPinIOIndex;
            set
            {
                if (_selectedPinIOIndex != value)
                {
                    _selectedPinIOIndex = value;
                    OnPropertyChanged();
                    OnSelectedIndexChanged();
                }
            }
        }

        private bool _isDpGroupEnabled;
        /// <summary>
        /// iDp グループボックスの有効化フラグ[cite: 6]
        /// </summary>
        public bool IsDpGroupEnabled
        {
            get => _isDpGroupEnabled;
            private set { if (_isDpGroupEnabled != value) { _isDpGroupEnabled = value; OnPropertyChanged(); } }
        }

        private bool _isDsGroupEnabled;
        /// <summary>
        /// iDs グループボックスの有効化フラグ[cite: 6]
        /// </summary>
        public bool IsDsGroupEnabled
        {
            get => _isDsGroupEnabled;
            private set { if (_isDsGroupEnabled != value) { _isDsGroupEnabled = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 確定処理完了によるダイアログ閉じ要求イベント（DialogResult.OK 相当）[cite: 6]
        /// </summary>
        public event EventHandler RequestCloseDialogOk;

        /// <summary>
        /// キャンセル処理によるダイアログ閉じ要求イベント[cite: 6]
        /// </summary>
        public event EventHandler RequestCloseDialogCancel;
        #endregion

        #region Constructors
        public PinInputOutputViewModel()
        {
        }

        public PinInputOutputViewModel(int selectPin, string pinIO, string iDpH, string iDpL, string iDsH, string iDsL)
        {
            SelectPin = selectPin;
            PinIO = pinIO ?? "";
            IDpH = iDpH ?? "";
            IDpL = iDpL ?? "";
            IDsH = iDsH ?? "";
            IDsL = iDsL ?? "";
            InitializeState();
        }
        #endregion

        #region Initialization & Logic
        public void InitializeState()
        {
            UpdateSelectedPinIOIndexFromStat();
            UpdateGroupEnableStates();
        }

        private void UpdateSelectedPinIOIndexFromStat()
        {
            int index = _defaultSettings.DioStat.Length - 1;
            if (_defaultSettings.DioStat != null)
            {
                for (int i = 0; i < _defaultSettings.DioStat.Length; i++)
                {
                    if (PinIO == _defaultSettings.DioStat[i])
                    {
                        index = i;
                        break;
                    }
                }
            }
            SelectedPinIOIndex = index;
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedPinIOIndex >= 0 && SelectedPinIOIndex < _defaultSettings.DioStat.Length)
            {
                _pinIO = _defaultSettings.DioStat[SelectedPinIOIndex];
                OnPropertyChanged(nameof(PinIO));
            }
            UpdateGroupEnableStates();
        }

        private void UpdateGroupEnableStates()
        {
            switch (PinIO)
            {
                case "iDp":
                case "nDp":
                    IsDpGroupEnabled = true;
                    IsDsGroupEnabled = false;
                    break;
                case "iDs":
                case "nDs":
                    IsDpGroupEnabled = false;
                    IsDsGroupEnabled = true;
                    break;
                default:
                    IsDpGroupEnabled = false;
                    IsDsGroupEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// OKボタン押下時または確定キー入力時の処理[cite: 6]
        /// </summary>
        public void Submit()
        {
            if (SelectedPinIOIndex == -1 || _defaultSettings.DioStat == null || SelectedPinIOIndex >= _defaultSettings.DioStat.Length)
            {
                PinIO = null;
            }
            else
            {
                PinIO = _defaultSettings.DioStat[SelectedPinIOIndex];
                switch (PinIO)
                {
                    case "iDp":
                    case "nDp":
                        // Current values bound to IDpL/IDpH remain active
                        break;
                    case "iDs":
                    case "nDs":
                        // Current values bound to IDsL/IDsH remain active
                        break;
                    default:
                        IDpL = "";
                        IDpH = "";
                        IDsL = "";
                        IDsH = "";
                        break;
                }
            }

            RequestCloseDialogOk?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Escapeキー押下等によるキャンセル処理[cite: 6]
        /// </summary>
        public void Cancel()
        {
            RequestCloseDialogCancel?.Invoke(this, EventArgs.Empty);
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