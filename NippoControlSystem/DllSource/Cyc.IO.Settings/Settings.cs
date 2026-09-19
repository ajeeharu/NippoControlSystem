using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace Cyc.IO
{
    public class Settings
    {
        //http://dobon.net/vb/dotnet/programing/storeappsettings.html
        //アプリケーションの設定を保存する

        //Windows
        public string WindowStyle { get; set; }
        public string HideTaskbar { get; set; }

        //設定のプロパティ
        //Main
        public string ApplicationName { get; set; }
        public string ApplicationNameEn { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string ApplicationFloder { get; set; }
        public string SettingsHolder { get; set; }
        public string DefaultConfigFilename { get; set; }
        public string MenuFolder { get; set; }
        public string MenuExtention { get; set; }
        public string DataFolder { get; set; }
        public string DataExtention { get; set; }
        public string HistViewFolder { get; set; }

        public string MeasureConditionPath { get; set; }
        public string ManualPath { get; set; }
        public int LoggingLevel { get; set; }
        public string Serial { get; set; }
        public int SerialSuffixLength { get; set; }
        public string Item { get; set; }
        public string MainNo { get; set; }
        public string SubNo { get; set; }
        public string SerialTitle { get; set; }
        public string GokiTitle { get; set; }
        public string Checkdat { get; set; }
        public string Listdat { get; set; }
        public string Histdat { get; set; }
        public string Portdat { get; set; }
        public string Menucsv { get; set; }
        public string menuDirPrior { get; set; }

        public string[] DioNames { get; set; }
        public int[] DioNums { get; set; }
        public string AoName { get; set; }
        public int AoNum { get; set; }
        public string AoSwichName { get; set; }
        public string[] AoSwichStat { get; set; }
        public string[] AoSwichGuide { get; set; }
        public int AoSwichNum { get; set; }
        public int AoSwichStartBit { get; set; }
        public string AiName { get; set; }
        public int AiNum { get; set; }
        public int AiCurrentNum { get; set; }
        public string[] GndNames { get; set; }
        public int[] GndNums { get; set; }
        public string[] DioStat { get; set; }       //新システムでのStat
        public string[] DioStatOld { get; set; }       //新システムでのStat
        public string[] DioStatGuide { get; set; }
        public string[] DiStatOld { get; set; } //変換は、Old→Newの対応
        public string[] DiStatNew { get; set; }
        public string[] DoStatOld { get; set; }
        public string[] DoStatNew { get; set; }
        public string[] DioHistStat { get; set; }
        public int InspectRetryMax { get; set; }
        public string[] inspection_TypeText { get; set; }
        public string AioMonitorExePath { get; set; }
        public int AutoTimeOut { get; set; }    //一時停止,繰り返し時、この時間を過ぎたら、強制NGとする 20170619
        public string[] CheckDatLMTFields { get; set; }    //CheckDatLMTFields
        public float[][] CheckDatLMT { get; set; }    //CheckDatLimit

        //Wav
        public string PlaySoundEnable { get; set; }
        public string okWavlPath { get; set; }
        public string ngWavlPath { get; set; }
        public string pauseWavPath { get; set; }
        //Print
        public bool DefaultPageSettings_Landscape { get; set; }
        public int DefaultPageSettings_Margins_Left { get; set; }
        public int DefaultPageSettings_Margins_Right { get; set; }
        public int DefaultPageSettings_Margins_Top { get; set; }
        public int DefaultPageSettings_Margins_Bottom { get; set; }
        ////Color
        public struct niCellStyle
        {
            [XmlIgnore] // XmlSerializer から隠す
            public System.Drawing.Color ForeColor;
            [XmlIgnore] // XmlSerializer から隠す
            public System.Drawing.Color SelectionForeColor;
            public System.Drawing.FontStyle fontstyle;
            public niCellStyle(System.Drawing.Color ForeColor, System.Drawing.Color SelectionForeColor, System.Drawing.FontStyle fontstyle)
            {
                this.ForeColor = ForeColor;
                this.SelectionForeColor = SelectionForeColor;
                this.fontstyle = fontstyle;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            [Browsable(false)]
            [XmlElement("ForeColor")]
            public string ForeColorFontAsString
            {
                get { return ColorTranslator.ToHtml(this.ForeColor); }
                set { this.ForeColor = ColorTranslator.FromHtml(value); }
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            [Browsable(false)]
            [XmlElement("SelectionForeColor")]
            public string SelectionForeColorFontAsString
            {
                get { return ColorTranslator.ToHtml(this.SelectionForeColor); }
                set { this.SelectionForeColor = ColorTranslator.FromHtml(value); }
            }
        }

        //public System.Windows.Forms.DataGridViewCellStyle [] CellStyles { get; set; }
        public niCellStyle[] CellStyles { get; set; }

        //Cyc.IO.DIO
        public string[] DeviceNameDO { get; set; }
        //public int[] DImax { get; set; }
        public int DO_Pmax { get; set; }
        //public int AO_USE_IN_DO { get; set; }
        public Cyc.IO.Log.LogLevel DIO_LogLevel { get; set; }

        //Cyc.IO.AIO
        public string DeviceNameAIO { get; set; }
        public int AImax { get; set; }
        public int AOmax { get; set; }
        public Cyc.IO.Log.LogLevel AIO_LogLevel { get; set; }
        public int AiAveTimes { get; set; }
        public float AoVoltMin { get; set; }
        public float AoVoltMax { get; set; }
        public float AiVoltMin { get; set; }
        public float AiVoltMax { get; set; }
        public float[] AoCalib_A { get; set; }
        public float[] AoCalib_B { get; set; }
        public float[] AoCalib_C { get; set; }
        public float[] AiCalib_A { get; set; }
        public float[] AiCalib_B { get; set; }
        public float AoDiv { get; set; }
        public float AiMulti { get; set; }

        //Cyc.IO.AI2DI
        public string [] DeviceNameAI2DI { get; set; }
        public int AI2DI_BDmax { get; set; }
        public int AI2DI_AImax { get; set; }
        public int AI2DI_DImax { get; set; }
        public int AI2DI_DOmax { get; set; }
        public Cyc.IO.Log.LogLevel AI2DI_LogLevel { get; set; }
        public int AI2DI_AveTimes { get; set; }
        public float AI2DI_VoltMin { get; set; }
        public float AI2DI_VoltMax { get; set; }
        public float AI2V_A { get; set; }
        public float AI2V_B { get; set; }
        public float AI2I_A { get; set; }
        public float AI2I_B { get; set; }
        public int[] AI2DI_DeviceChannel { get; set; }


        //コンストラクタ
        public Settings()
        {
            //Windows
            WindowStyle = "2";
            HideTaskbar = @"0";

            //Main
            ApplicationName = "配線チェッカー";
            ApplicationNameEn = @"";            //"wireChecker"  mApplicationNameEnで上書きされる 
            ApplicationFloder = @"";            //System.Environment.GetFolderPath(Environment.SpecialFolder.Personal))で上書きされる。
            DefaultConfigFilename = @"";        //@"Default.config"  mDefaultConfigFilenameで上書きされる
            SettingsHolder = @"";               //@"Settings\"  mSettingsHolderで上書きされる
            MenuFolder = @"Settings\";
            MenuExtention = ".hcm";
            DataFolder = @"C:\Checker2\";
            DataExtention = ".csv";
            HistViewFolder = "HistView";
            MeasureConditionPath = @"MeasureCondition.Config";
            ManualPath = @"Settings\操作説明書.pdf";
            LoggingLevel = 1;
            Serial = "4A0000001";
            SerialSuffixLength = 7;
            Item = "品名";
            MainNo = "仕様書番号";
            SubNo = "追番";
            SerialTitle = "製造番号";
            GokiTitle = "号機";
            Checkdat = "Check.dat";
            Listdat = "List.dat";
            Histdat = "Hist.dat";
            Portdat = "Port.dat";
            Menucsv = "Menu.csv";
            menuDirPrior = "Yes";
            DioNames = new string[] { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" };
            DioNums = new int[] { 32, 32, 32, 32, 32, 32, 32, 32 }; //DH 32->28 20160715
            AoName = "AO";
            AoNum = 2;
            AoSwichName = "AoSw";
            AoSwichStat = new string[] { "", "Y" };
            AoSwichGuide = new string[] { "無効(空白)", "有効" };
            AoSwichNum = 8;
            AoSwichStartBit = 0;
            AiName = "AI";
            AiNum = 8;
            AiCurrentNum = 2;
            GndNames = new string[] { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" };
            GndNums = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            DioStatGuide = new string[] { "oOP：Open出力", 
                                          "oGN：GND出力",
                                          "oHi：High出力",
                                          "iOP：Open入力",
                                          "iGN：GND入力",
                                          "iHi：High入力",
                                          "iDo：Open入力",
                                          "iDh：High入力",
                                          "iDb：High入力",
                                          "iDc：High入力",
                                          "iDs：High入力",
                                          "iDp：High入力",
                                          "iTo：ﾃｽﾄ入力",
                                          "nOP：Open補集合",
                                          "nGN：GND補集合",
                                          "nHi：High補集合",
                                          "nDo：Open補集合",
                                          "nDh：High補集合",
                                          "nDb：High補集合",
                                          "nDc：High補集合",
                                          "nDs：High補集合",
                                          "nDp：High補集合",
                                          "nTo：ﾃｽﾄ補集合",
                                          "dat：電流表示",
                                          //"dmA：電流表示", //20180817
                                          //"dV：電圧表示",  //20180817
                                          "空欄：指定なし" };
            //DioStat = new string[] { "oOP", "oGN", "oHi", "iOP", "iGN", "iHi", "iDo", "iDh", "iTo", "" };   //oOPが先頭になるよう並び替え20161111 iTo 20170126
            //DioStatOld = new string[] { "OFF", "OFF", "ON", "OFF", "OFF", "ON", "ON", "OFF", "", "" };      // iDh、iDgを追加   、Cyc.IO.NippoDIO.IO_STATと並びを合わせること
            DioStat = new string[DioStatGuide.Length];      //DioStatGuideが長くなったので、ソフト的に生成
            DioStatOld = new string[DioStatGuide.Length];      //DioStatGuideが長くなったので、ソフト的に生成
            for(int i=0; i<DioStatGuide.Length; i++)
            {
                string [] sp = DioStatGuide[i].Split('：');
                if (sp.Length == 2)
                {
                    if( sp[0] == "空欄")
                    {
                        DioStat[i] = "";    //
                    }
                    else{
                    DioStat[i] = sp[0];     //"oOP"とか
                    }
                    switch (sp[0])
                    {


                        case "iDb":
                        case "iDc":
                        case "iDs":
                        case "iDp":
                        case "nDo":
                            DioStatOld[i] = "iDh";
                            break;

                        case "nOP":
                            DioStatOld[i] = "iHi";
                            break;

                        case "nGN":
                            DioStatOld[i] = "iHi";
                            break;

                        case "nHi":
                            DioStatOld[i] = "iGN";
                            break;

                        case "nDh":
                            DioStatOld[i] = "iDo";
                            break;

                        case "nDb":
                        case "nDc":
                        case "nDs":
                        case "nDp":
                        case "iDo":
                            DioStatOld[i] = "iDo";
                            break;

                        case "nTo":
                        case "dat":
                        case "空欄":
                            DioStatOld[i] = "";
                            break;
                        default:
                            DioStatOld[i] = sp[0];
                            break;
                    }
                }
            }

            DoStatOld = new string[] { "ON", "OFF" };   //ここにない項目は、空白に変換
            DoStatNew = new string[] { "oHi", "oOP" };
            DiStatOld = new string[] { "ON", "OFF" };   //ここにない項目は、空白に変換
            //DiStatNew = new string[] { "iHi", "iOP" };
            DiStatNew = new string[] { "iDh", "iDo" };  //20170107 iDhを標準にする
            DioHistStat = new string[] { "OPEN", "GND", "HIGH", "ERR" };
            InspectRetryMax = 10;   //通常検査で、連続NGのとき、異常終了とする。
            inspection_TypeText = new string[] { "通常検査", "一時停止", "繰り返し" };
            AioMonitorExePath = @"C:\Users\Nippo_Shibukawa\Documents\Project\vs2010\Cyc.IO.Aio\AioMonitor\bin\Debug\AioMonitor.exe";
            AutoTimeOut = 2;   //秒

            CheckDatLMTFields = new string[] { "-HiLMT", "-LoLMT" };
            //                        12V iOP-HiLMT,iGN-HiLMT,iHi-HiLMT,iDo-HiLMT,iDh-HiLMT,iDb-HiLMT,iDc-HiLMT,iDs-HiLMT,iDp-HiLMT,iTo-HiLMT
            //                        12V iOP-LoLMT,iGN-LoLMT,iHi-LoLMT,iDo-LoLMT,iDh-LoLMT,iDb-LoLMT,iDc-LoLMT,iDs-LoLMT,iDp-LoLMT,iTo-LoLMT
            //                        24V iOP-HiLMT,iGN-HiLMT,iHi-HiLMT,iDo-HiLMT,iDh-HiLMT,iDb-HiLMT,iDc-HiLMT,iDs-HiLMT,iDp-HiLMT,iTo-HiLMT
            //                        24V iOP-LoLMT,iGN-LoLMT,iHi-LoLMT,iDo-LoLMT,iDh-LoLMT,iDb-LoLMT,iDc-LoLMT,iDs-LoLMT,iDp-LoLMT,iTo-LoLMT
            CheckDatLMT = new float[4][];
            CheckDatLMT[0] = new float[] { 8.00000f, 2.00000f, 15.0000f, 0.20000f, 60.0000f, 60.0000f, 8.00000f, 60.0000f, 60.0000f, 0.20000f };    //12V High-Limut
            CheckDatLMT[1] = new float[] { 4.00000f, 0.00000f, 10.0000f, 0.00000f, 0.30000f, 9.00000f, 6.00000f, 0.30000f, 0.30000f, 0.00000f };    //12V Low-Limut
            CheckDatLMT[2] = new float[] { 20.0000f, 2.00000f, 30.0000f, 0.20000f, 60.0000f, 60.0000f, 10.1000f, 60.0000f, 60.0000f, 0.20000f };    //24V High-Limut
            CheckDatLMT[3] = new float[] { 8.00000f, 0.00000f, 20.0000f, 0.00000f, 0.30000f, 17.0000f, 8.10000f, 0.30000f, 0.30000f, 0.00000f };    //24V Low-Limut

            //Wav
            PlaySoundEnable = "Yes";
            okWavlPath = @"Settings\ok.wav";
            ngWavlPath = @"Settings\ng.wav";
            pauseWavPath = @"Settings\pause.wav";
            //Print
            DefaultPageSettings_Landscape = false;
            DefaultPageSettings_Margins_Left = 100;
            DefaultPageSettings_Margins_Right = 100;
            DefaultPageSettings_Margins_Top = 100;
            DefaultPageSettings_Margins_Bottom = 100;
            //Color
            CellStyles = new niCellStyle[] { 
                new niCellStyle ( System.Drawing.Color.Red,   System.Drawing.Color.Red,   System.Drawing.FontStyle.Regular ),// 0 "oOP：Open出力", 
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Bold    ),// 1 "oGN：GND出力",
                new niCellStyle ( System.Drawing.Color.Red,   System.Drawing.Color.Red,   System.Drawing.FontStyle.Bold    ),// 2 "oHi：High出力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 3 "iOP：Open入力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 4 "iGN：GND入力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 5 "iHi：High入力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 6 "iDo：Open入力",
                new niCellStyle ( System.Drawing.Color.Blue,  System.Drawing.Color.Blue,  System.Drawing.FontStyle.Bold    ),// 7 "iDh：High入力",
                new niCellStyle ( System.Drawing.Color.FromArgb(0, 175, 80), System.Drawing.Color.FromArgb(0, 175, 80), System.Drawing.FontStyle.Bold ),// 8 "iDb：High入力",
                new niCellStyle ( System.Drawing.Color.FromArgb(255, 192, 80), System.Drawing.Color.FromArgb(255, 192, 80), System.Drawing.FontStyle.Bold ),// 9 "iDc：High入力", 
                new niCellStyle ( System.Drawing.Color.FromArgb(80, 255, 192), System.Drawing.Color.FromArgb(80, 255, 192), System.Drawing.FontStyle.Bold ),// 10 "iDs：High入力",
                new niCellStyle ( System.Drawing.Color.FromArgb(192, 80, 255), System.Drawing.Color.FromArgb(192, 80, 255), System.Drawing.FontStyle.Bold ),// 11 "iDp：High入力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 12 "iTo：ﾃｽﾄ入力",
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 13 予備
                new niCellStyle ( System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.FontStyle.Regular ),// 14 予備 
            };

            //Cyc.IO.DIO
            DeviceNameDO = new string[] { "DIO000", "DIO001", "DIO002", "DIO003", "DIO004", "DIO005", "DIO006", "DIO007" };
            DO_Pmax = 128;  
            //AO_USE_IN_DO = 8;
            DIO_LogLevel = Log.LogLevel.LOG_INFO;

            //Cyc.IO.AIO
            DeviceNameAIO = "AIO004";
            AImax = 8;
            AOmax = 2;
            AIO_LogLevel = Log.LogLevel.LOG_INFO;
            AiAveTimes = 5;
            AoVoltMin = 0.0f;
            AoVoltMax = 15.0f;  //20161116
            AiVoltMin = 0.0f;
            AiVoltMax = 15.0f;  //20161116
            AoCalib_A = new float[] { 0.0f, 0.0f };
            AoCalib_B = new float[] { 1.0f, 1.0f };
            AoCalib_C = new float[] { 0.0f, 0.0f };
            AiCalib_A = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, };
            AiCalib_B = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, };
            /*
             *  <AoCalib_A>
             *    <float>-6E-05</float>
             *    <float>-6E-05</float>
             *  </AoCalib_A>
             *  <AoCalib_B>
             *    <float>1.0145</float>
             *    <float>1.0155</float>
             *  </AoCalib_B>
             *  <AoCalib_C>
             *    <float>0.0136</float>
             *    <float>0.0129</float>
             *  </AoCalib_C>
             * */

            //AoCalib_A = new float[] { -6E-05f, -6E-05f };
            //AoCalib_B = new float[] { 1.0145f, 1.0155f };
            //AoCalib_C = new float[] { 0.0136f, 0.0129f };
            //AiCalib_A = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, };
            //AiCalib_B = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, };
            AoDiv = (AoVoltMax - AoVoltMin) / 10.0f;       // 3.0f; 20161121
            AiMulti = (AoVoltMax - AiVoltMin) / 10.0f;     // 3.0f; 20161121

            //Cyc.IO.AI2DI
            DeviceNameAI2DI = new string[] { "AIO000", "AIO001", "AIO002", "AIO003" };           //20180713

            AI2DI_BDmax = 4;    //ボード枚数
            AI2DI_AImax = 64;    //１ボード当たりの点数
            AI2DI_DImax = 4;    //１ボード当たりの点数
            AI2DI_DOmax = 4;    //１ボード当たりの点数
            AI2DI_LogLevel = Cyc.IO.Log.LogLevel.LOG_INFO;
            AI2DI_AveTimes = 10;
            AI2DI_VoltMin = -10.0f;
            AI2DI_VoltMax = 0.0f;  //V
            AI2V_A = -3.0f; //電圧変換 = 入力電圧 * AI2V_A + AI2V_B
            AI2V_B = 0.0f; //
            AI2I_A = -6.0f; //電流変換mA = 入力電圧 * AI2I_A + AI2I_B
            AI2I_B = 0.0f; //
            AI2DI_DeviceChannel = new int[] { 0,   8,  1,  9,  2, 10,  3, 11, 
                                        16, 24, 17, 25, 18, 26, 19, 27,
                                        32, 40, 33, 41, 34, 42, 35, 43,
                                        48, 56, 49, 57, 50, 58, 51, 59,
                                         4, 12,  5, 13,  6, 14,  7, 15,
                                        20, 28, 21, 29, 22, 30, 23, 31,
                                        36, 44, 37, 45, 38, 46, 39, 47,
                                        52, 60, 53, 61, 54, 62, 55, 63 };
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //static
        public static string mApplicationNameEn = @"wireChecker";
        public static string mSettingsHolder = @"Settings\";
        static string mDefaultConfigFilename = @"Default.config";
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //GetInstance
        [NonSerialized()]
        private static Settings m_instance;
        public static Settings GetInstance()
        {
            if (m_instance == null)
            {
                if (System.IO.File.Exists(GetSettingPath()))
                {
                    m_instance = LoadFromXmlFile();
                    //m_instance.ApplicationFloder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + mApplicationNameEn + System.IO.Path.DirectorySeparatorChar;
                    m_instance.ApplicationFloder = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(GetSettingPath())) + System.IO.Path.DirectorySeparatorChar;   //
                }
                else
                {
                    m_instance = new Settings();
                    //m_instance.ApplicationFloder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + mApplicationNameEn + System.IO.Path.DirectorySeparatorChar;
                    m_instance.ApplicationFloder = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(GetSettingPath())) + System.IO.Path.DirectorySeparatorChar;   //
                    m_instance.ApplicationNameEn = mApplicationNameEn;
                    m_instance.SettingsHolder = mSettingsHolder;
                    m_instance.DefaultConfigFilename = mDefaultConfigFilename;
                }
            }
            return m_instance;
        }

        /// <summary>
        /// 設定をXMLファイルから読み込み復元する
        /// </summary>
        public static Settings LoadFromXmlFile()
        {
            string path = GetSettingPath();

            System.IO.FileStream fs = new System.IO.FileStream(path,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Xml.Serialization.XmlSerializer xs =
                new System.Xml.Serialization.XmlSerializer(
                    typeof(Settings));
            //読み込んで逆シリアル化する
            object obj = xs.Deserialize(fs);
            fs.Close();

            //m_instance = (Settings)obj;
            return (Settings)obj;
        }

        /// <summary>
        /// 設定をXMLファイルから読み込み復元する
        /// </summary>
        public static Settings ReloadFromXmlFile()
        {
            string path = GetSettingPath();

            System.IO.FileStream fs = new System.IO.FileStream(path,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Xml.Serialization.XmlSerializer xs =
                new System.Xml.Serialization.XmlSerializer(
                    typeof(Settings));
            //読み込んで逆シリアル化する
            object obj = xs.Deserialize(fs);
            fs.Close();

            //m_instance = (Settings)obj;
            if (m_instance != null)
            {
                //Buffer.BlockCopy(obj, 0, m_instance, 0, System.Runtime.InteropServices.Marshal.SizeOf(Settings));
                //int size = System.Runtime.InteropServices.Marshal.SizeOf(m_instance);
                //byte[] temp = new byte[size];
                //Marshal.Copy(obj, temp, 0, size);
                //Marshal.Copy(temp, 0, m_instance, size);
                //m_instance.format定格電圧 = ((Settings)obj).format定格電圧;
            }
            return (Settings)obj;
        }

        /// <summary>
        /// 現在の設定をXMLファイルに保存する
        /// </summary>
        public static void SaveToXmlFile()
        {
            string path = GetSettingPath();

            System.IO.FileStream fs = new System.IO.FileStream(path,
                System.IO.FileMode.Create,
                System.IO.FileAccess.Write);
            System.Xml.Serialization.XmlSerializer xs =
                new System.Xml.Serialization.XmlSerializer(
                typeof(Settings));
            //シリアル化して書き込む
            xs.Serialize(fs, m_instance);
            fs.Close();
        }

        /// <summary>
        /// 設定をバイナリファイルから読み込み復元する
        /// </summary>
        public static void LoadFromBinaryFile()
        {
            // BinaryFormatter は非推奨のため、既存の XML 読み込みにフォールバックします
            m_instance = LoadFromXmlFile();
        }

        /// <summary>
        /// 現在の設定をバイナリファイルに保存する
        /// </summary>
        public static void SaveToBinaryFile()
        {
            // BinaryFormatter は非推奨のため、既存の XML 保存にフォールバックします
            SaveToXmlFile();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //GetSettingPath()
        public static string GetSettingPath()
        {
            //string path = @"D:\Host\Settings\Default.config";
            //string ApplicationFloder = System.Configuration.ConfigurationManager.AppSettings["Application Floder"];   //for WireChexker
            string path = System.Configuration.ConfigurationManager.AppSettings["Default.config Path"];   //for LCD
            //string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + DefaultPass.GetInstance().SettingsSubHolder + DefaultPass.GetInstance().DefaultConfigFilename;
            //string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + mApplicationNameEn + System.IO.Path.DirectorySeparatorChar + mSettingsHolder + mDefaultConfigFilename;
            return path;
        }

        public static string GetExePath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
            //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";
        }

    }
}
