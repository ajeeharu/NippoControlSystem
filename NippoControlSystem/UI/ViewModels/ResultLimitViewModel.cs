using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 測定規格限界値（ResultLimitView）画面用 ViewModel
    /// </summary>
    public class ResultLimitViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataSetItems _dataSetItems;
        private int _tNo;
        private string _selectionCell = "";
        private bool _isBusy;
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// データ保持用の DataSetItems
        /// </summary>
        public DataSetItems DataSetItems
        {
            get => _dataSetItems;
            set
            {
                if (_dataSetItems != value)
                {
                    _dataSetItems = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MaxTNo));
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                }
            }
        }

        /// <summary>
        /// 現在選択されているテスト番号 Index (0-based)
        /// </summary>
        public int TNo
        {
            get => _tNo;
            set
            {
                if (_tNo != value)
                {
                    _tNo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayTNo));
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                }
            }
        }

        /// <summary>
        /// 画面表示用テスト番号文字列 (1-based: 例 "1")
        /// </summary>
        public string DisplayTNo
        {
            get => (TNo + 1).ToString();
            set
            {
                if (int.TryParse(value, out int parsedNo))
                {
                    int index = parsedNo - 1;
                    if (index >= 0 && index <= MaxTNo)
                    {
                        TNo = index;
                    }
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 選択されているセル識別文字列
        /// </summary>
        public string SelectionCell
        {
            get => _selectionCell;
            set { if (_selectionCell != value) { _selectionCell = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 処理中（Busy状態）フラグ
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            private set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// ListDat テーブルの最大行数インデックス
        /// </summary>
        public int MaxTNo => (DataSetItems?.ListDat?.Rows.Count ?? 1) - 1;

        /// <summary>
        /// 次へ進めるかどうか
        /// </summary>
        public bool CanGoNext => DataSetItems != null && TNo < MaxTNo;

        /// <summary>
        /// 前へ戻れるかどうか
        /// </summary>
        public bool CanGoPrevious => DataSetItems != null && TNo > 0;
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 画面を閉じる要求イベント
        /// </summary>
        public event EventHandler RequestCloseWindow;
        #endregion

        #region Constructors
        public ResultLimitViewModel()
        {
        }

        public ResultLimitViewModel(DataSetItems dataSetItems, int tNo = 0)
        {
            DataSetItems = dataSetItems;
            TNo = tNo;
        }
        #endregion

        #region Business Logic & Paging
        /// <summary>
        /// 次のテスト番号へ進む
        /// </summary>
        public void Next()
        {
            if (!CanGoNext) return;

            try
            {
                IsBusy = true;
                TNo++;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 前のテスト番号へ戻る
        /// </summary>
        public void Previous()
        {
            if (!CanGoPrevious) return;

            try
            {
                IsBusy = true;
                TNo--;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        public void Close()
        {
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
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