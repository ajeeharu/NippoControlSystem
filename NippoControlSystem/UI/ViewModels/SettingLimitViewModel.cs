using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 限界値・判定基準設定画面（SettingLimitView）用 ViewModel
    /// </summary>
    public class SettingLimitViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _mainTitle = string.Empty;
        private string _subTitle = string.Empty;
        private string _folder = string.Empty;
        private DataSetItems _myDataSetItems;

        private string _lblMainNo = string.Empty;
        private string _lblSubNo = string.Empty;
        private string _lblItem = string.Empty;
        private string _voltText = string.Empty;

        // 各電流下限値ラベルの前景色
        private Color _foreColorIDo;
        private Color _foreColorIDh;
        private Color _foreColorIDb;
        private Color _foreColorIDc;
        private Color _foreColorIDs;
        private Color _foreColorIDp;
        private Color _foreColorITo;
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// メインタイトル（仕様書番号など）
        /// </summary>
        public string MainTitle
        {
            get => _mainTitle;
            set { if (_mainTitle != value) { _mainTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// サブタイトル（追番など）
        /// </summary>
        public string SubTitle
        {
            get => _subTitle;
            set { if (_subTitle != value) { _subTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// フォルダパス
        /// </summary>
        public string Folder
        {
            get => _folder;
            set { if (_folder != value) { _folder = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// データセット
        /// </summary>
        public DataSetItems MyDataSetItems
        {
            get => _myDataSetItems;
            set
            {
                if (_myDataSetItems != value)
                {
                    _myDataSetItems = value;
                    OnPropertyChanged();
                    InitializeViewData();
                }
            }
        }

        // ラベル表示文言
        public string LblMainNo
        {
            get => _lblMainNo;
            private set { if (_lblMainNo != value) { _lblMainNo = value; OnPropertyChanged(); } }
        }

        public string LblSubNo
        {
            get => _lblSubNo;
            private set { if (_lblSubNo != value) { _lblSubNo = value; OnPropertyChanged(); } }
        }

        public string LblItem
        {
            get => _lblItem;
            private set { if (_lblItem != value) { _lblItem = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 電圧表示文字列 (例: "12V" または "24V")
        /// </summary>
        public string VoltText
        {
            get => _voltText;
            private set { if (_voltText != value) { _voltText = value; OnPropertyChanged(); } }
        }

        #region ForeColor Properties
        public Color ForeColorIDo
        {
            get => _foreColorIDo;
            private set { if (_foreColorIDo != value) { _foreColorIDo = value; OnPropertyChanged(); } }
        }
        public Color ForeColorIDh
        {
            get => _foreColorIDh;
            private set { if (_foreColorIDh != value) { _foreColorIDh = value; OnPropertyChanged(); } }
        }
        public Color ForeColorIDb
        {
            get => _foreColorIDb;
            private set { if (_foreColorIDb != value) { _foreColorIDb = value; OnPropertyChanged(); } }
        }
        public Color ForeColorIDc
        {
            get => _foreColorIDc;
            private set { if (_foreColorIDc != value) { _foreColorIDc = value; OnPropertyChanged(); } }
        }
        public Color ForeColorIDs
        {
            get => _foreColorIDs;
            private set { if (_foreColorIDs != value) { _foreColorIDs = value; OnPropertyChanged(); } }
        }
        public Color ForeColorIDp
        {
            get => _foreColorIDp;
            private set { if (_foreColorIDp != value) { _foreColorIDp = value; OnPropertyChanged(); } }
        }
        public Color ForeColorITo
        {
            get => _foreColorITo;
            private set { if (_foreColorITo != value) { _foreColorITo = value; OnPropertyChanged(); } }
        }
        #endregion
        #endregion

        #region Events (Delegates for View Operations)
        /// <summary>
        /// 画面を閉じる要求イベント
        /// </summary>
        public event EventHandler RequestCloseWindow;

        /// <summary>
        /// 端子編集画面（PortView）の表示要求イベント (DataSetItemsを渡す)
        /// </summary>
        public event EventHandler<DataSetItems> RequestShowPortView;

        /// <summary>
        /// 確認メッセージボックスの表示要求デリゲート (メッセージ, タイトル) -> 戻り値: DialogResult
        /// </summary>
        public event Func<string, string, bool> RequestConfirmDialog;
        #endregion

        #region Constructors & Initialization
        public SettingLimitViewModel()
        {
            InitializeSettings();
        }

        public SettingLimitViewModel(DataSetItems dataSetItems) : this()
        {
            MyDataSetItems = dataSetItems;
        }

        /// <summary>
        /// Settings からの固定文言・配色等の初期化
        /// </summary>
        private void InitializeSettings()
        {
            var defaultSettings = Settings.GetInstance();

            LblMainNo = defaultSettings.MainNo; // "仕様書番号"
            LblSubNo = defaultSettings.SubNo;   // "追番"
            LblItem = defaultSettings.Item;     // "品名"

            // セルスタイルの配色設定
            int baseOffset = (int)NippoDIO.IO_STAT.oOP;
            ForeColorIDo = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDo - baseOffset].ForeColor;
            ForeColorIDh = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDh - baseOffset].ForeColor;
            ForeColorIDb = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDb - baseOffset].ForeColor;
            ForeColorIDc = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDc - baseOffset].ForeColor;
            ForeColorIDs = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDs - baseOffset].ForeColor;
            ForeColorIDp = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iDp - baseOffset].ForeColor;
            ForeColorITo = defaultSettings.CellStyles[(int)NippoDIO.IO_STAT.iTo - baseOffset].ForeColor;
        }

        /// <summary>
        /// DataSetItems がセットされた際の初期表示データの構築
        /// </summary>
        public void InitializeViewData()
        {
            if (MyDataSetItems?.CheckDat == null || MyDataSetItems.CheckDat.Rows.Count == 0) return;

            DataRow firstRow = MyDataSetItems.CheckDat.Rows[0];
            string strVolt = firstRow["Volt"]?.ToString() ?? "0";
            VoltText = GetVoltString(strVolt);
        }
        #endregion

        #region Business Logic
        private static string GetVoltString(string valueString)
        {
            string[] voltString = ["12V", "24V"];
            return (valueString == "1" ? voltString[1] : voltString[0]);
        }

        /// <summary>
        /// 電圧（12V / 24V）の切り替え処理
        /// </summary>
        public void ChangeVolt()
        {
            if (MyDataSetItems?.CheckDat == null || MyDataSetItems.CheckDat.Rows.Count == 0) return;

            var defaultSettings = Settings.GetInstance();
            int itemFound = 0;

            DataRow dtCheckDatRow = MyDataSetItems.CheckDat.Rows[itemFound];
            string strVolt = dtCheckDatRow["Volt"]?.ToString() ?? "0";
            string newVolt = (strVolt == "1" ? "0" : "1");
            string newVoltStr = GetVoltString(newVolt);

            // ユーザー確認の問いあわせ
            string message = string.Format("電圧を{0}に変更しますか？", newVoltStr);
            bool isConfirmed = RequestConfirmDialog?.Invoke(message, defaultSettings.ApplicationName) ?? false;

            if (!isConfirmed)
            {
                return;
            }

            // 電圧値の反映とデータテーブル（CheckDat）の閾値更新
            VoltText = newVoltStr;
            dtCheckDatRow["Volt"] = newVolt;

            int iDo_Length = (int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.iOP + 1;
            int mVoltIdx = (newVolt == "1" ? 2 : 0); // 0:12V, 2:24V

            for (int i = 0; i < iDo_Length; i++)
            {
                // Hi 限界値設定
                string hiField = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + defaultSettings.CheckDatLMTFields[0];
                dtCheckDatRow[hiField] = defaultSettings.CheckDatLMT[mVoltIdx][i].ToString("F1");

                // Lo 限界値設定
                string loField = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + defaultSettings.CheckDatLMTFields[1];
                dtCheckDatRow[loField] = defaultSettings.CheckDatLMT[mVoltIdx + 1][i].ToString("F1");
            }

            MyDataSetItems.CheckDat.AcceptChanges();
        }

        /// <summary>
        /// 端子編集画面のオープン要求
        /// </summary>
        public void OpenEditTanshi()
        {
            RequestShowPortView?.Invoke(this, MyDataSetItems);
        }

        /// <summary>
        /// 保存して閉じる（あるいはそのまま画面を閉じる）
        /// </summary>
        public void SaveAndClose()
        {
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
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