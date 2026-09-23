using NippoControlSystem.Domain.Interfaces.enums;
using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.NativeLibs;
using NippoControlSystem.Infrastructure.Persistence;
using NippoControlSystem.Infrastructure.Services;
using System.Data;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.Infrastructure.Devices
{
    public class cDio
    {
        //for CONTEC Digital I/O device
        CdioDevice dio = null;
        private int m_cDioEmu = 0;
        private bool m_Connected = false;
        private bool m_Inited = false;
        private short[] m_DOid = null;
        private string? m_LastErrorString = null;
        private int m_DO_Pmax = 0;
        string[] m_DeviceNameDO = null;       //{ "DIO000", "DIO001", "DIO002", "DIO003", "DIO004", "DIO005", "DIO006", "DIO007"};
        private string m_titleText = "Cyc.IO.DIO";      //タイトル
        //private int [] inpData;
        //private int m_AO_USE_IN_DO = 0;

        Settings Default = Settings.GetInstance();
        Log.LogLevel DIO_LogLevel = Log.LogLevel.LOG_INFO;

        public class tagDeviceItem
        {
            //public int DeviceManagerIdx;     //axDBDeviceManager1=0, axDBDeviceManager2=1 ...
            public string DevType;  //DeviceManager or IO type ... R0,W0,,DI,DO
            //public int index;      //axDBDeviceManager inner index
            public string DevSize; //B:Bit, W:Word, D:Double Word
            //public string DevSign; //not used
            public int DevPoint;   //点数
            public string DevName; //MR00302 etc
            public string DevEnd;  //DM3004 ~ DM3005
            public string DevTitle; //名称
            //public DATABUILDERAXLibLB.DBPlcDevice Devtype;    //タイプ、
            public int DevNo;      //数字部
            public int UnitNo;      //Unit部
            public string Comment;      //備考
            public string Prefix;      //DI,DO時のIかO
        }

        //DeviceItem辞書
        Dictionary<string, tagDeviceItem> dicDeviceItem = null;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static cDio m_instance = null;
        public static cDio GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new cDio();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public cDio()
        {
            dio = new CdioDevice();
            DIO_LogLevel = Default.DIO_LogLevel;
            dicDeviceItem = new Dictionary<string, tagDeviceItem>();

            //m_DImax = Default.DImax;
            m_DO_Pmax = Default.DO_Pmax;
            //m_DeviceNameDI = Default.DeviceNameDI;  //{ "PI000", "PI001", "PI002", "PI003" };
            m_DeviceNameDO = Default.DeviceNameDO;  //{ "PO000", "PO001", "PO002", "PO003" };
            //m_AO_USE_IN_DO = Default.AO_USE_IN_DO;  //アナログ出力のON/OFFに使用するDO点数=8
            //m_DIid = new short[m_DeviceNameDI.Length];
            m_DOid = new short[m_DeviceNameDO.Length];

            Init();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~cDio()
        {
            this.Exit();
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        //// プロパティ
        //public string[] DeviceNameDI
        //{
        //    get
        //    {
        //        return Default.DeviceNameDI;
        //    }
        //    set
        //    {
        //        Default.DeviceNameDI = value;
        //    }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // プロパティ
        public string[] DeviceNameDO
        {
            get
            {
                return Default.DeviceNameDO;
            }
            set
            {
                Default.DeviceNameDO = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // プロパティ
        public string SettingsHolder
        {
            get
            {
                return Default.SettingsHolder;
            }
            set
            {
                Default.SettingsHolder = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int cDioEmu
        {
            get { return m_cDioEmu; }
            set { m_cDioEmu = value; }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////Proparty
        //public int [] DImax
        //{
        //    get { return m_DImax; }
        //    //set { m_DImax = value; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int DO_Pmax
        {
            get { return m_DO_Pmax; }
            //set { m_DOmax = value; }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////Proparty
        //public short[] DIid
        //{
        //    get { return m_DIid; }
        //    set { m_DIid = value; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public short[] DOid
        {
            get { return m_DOid; }
            set { m_DOid = value; }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////Proparty
        //public int AO_USE_IN_DO
        //{
        //    get { return m_AO_USE_IN_DO; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public bool Connected
        {
            get { return m_Connected; }
            //set { m_Inited = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public bool Inited
        {
            get { return m_Inited; }
            set { m_Inited = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public Dictionary<string, tagDeviceItem> DeviceItem
        {
            get { return dicDeviceItem; }
            //set { m_Inited = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public string LastErrorString
        {
            get { return m_LastErrorString; }
        }

        //        //--------1---------2---------3---------4---------5---------6---------7---------8
        //        //インデキサ
        //        public int this[string DevTitle]
        //        {
        //            set
        //            {
        //                //if (m_cDioEmu != 0) return;
        //                if (dicDeviceItem.ContainsKey(DevTitle))
        //                {
        //                    int DeviceNo = dicDeviceItem[DevTitle].DevNo;
        //                    int UnitNo = dicDeviceItem[DevTitle].UnitNo;
        //                    int errcode = OutBit(UnitNo, DeviceNo, value);
        //                    if (errcode != 0)
        //                    {
        //                        throw (new Exception(string.Format("{0}:Set:{1}:OutBit異常", m_titleText, DevTitle)));
        //                    }
        //                    inpData[DeviceNo] = value;
        //                }
        //                else
        //                {
        //                    if (m_cDioEmu == 0)
        //                    {
        //                        throw (new Exception(string.Format("{0}:Set:{1}:DevTitle該当なし", m_titleText, DevTitle)));
        //                    }
        //                    else
        //                    {
        //                        //inpData[DeviceNo] = value;
        //                    }
        //                }
        //            }
        //            get
        //            {
        //                //if (m_cDioEmu != 0) return 0;
        //                int ReturnValue = 0;
        //                if (dicDeviceItem.ContainsKey(DevTitle))
        //                {
        //                    int DeviceNo = dicDeviceItem[DevTitle].DevNo;
        //                    int UnitNo = dicDeviceItem[DevTitle].UnitNo;
        //#if DirectInp == fa 
        //                    if (DeviceNo < 32)
        //                    {
        //                        int errcode = InpBit(UnitNo, DeviceNo, out ReturnValue);
        //                        if (errcode != 0)
        //                        {
        //                            throw (new Exception(string.Format("{0}:Set:{1}:InpBit異常", m_titleText, DevTitle)));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ReturnValue = inpData[DeviceNo];
        //                    }
        //#else
        //                    ReturnValue = inpData[DeviceNo];
        //#endif
        //                }
        //                else
        //                {
        //                    if (m_cDioEmu == 0)
        //                    {
        //                        throw (new Exception(string.Format("{0}:get:{1}:DevTitle該当なし", m_titleText, DevTitle)));
        //                    }
        //                }
        //                return ReturnValue;
        //            }
        //        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Init()
        public CioDeviceErrorCode Init()
        {
            // Initialization handling
            CioDeviceErrorCode Ret = 0;
            CioDeviceErrorCode returnValue = 0;
            if (m_cDioEmu == 0)
            {
                //for (int i = 0; i < m_DeviceNameDI.Length; i++)
                //{
                //    if (m_DIid[i] != 0)
                //    {
                //        dio.Exit(m_DIid[i]);
                //        m_DIid[i] = 0;
                //    }
                //    Ret = dio.Init(m_DeviceNameDI[i], out m_DIid[i]);
                //    GetErrorString("dio.Init", Ret);
                //    if (Ret != 0)
                //    {
                //        returnValue = Ret;
                //    }
                //}
                for (int i = 0; i < m_DeviceNameDO.Length; i++)
                {
                    if (m_DOid[i] != 0)
                    {
                        dio.Exit(m_DOid[i]);
                        m_DOid[i] = 0;
                    }
                    Ret = dio.Init(m_DeviceNameDO[i], out m_DOid[i]);
                    GetErrorString("dio.Init", (int)Ret);
                    if (Ret != 0)
                    {
                        returnValue = Ret;
                    }
                }
            }
            else
            {
            }
            return returnValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Exit()
        public CioDeviceErrorCode Exit()
        {
            // Exit handling of device
            CioDeviceErrorCode Ret = 0;
            CioDeviceErrorCode returnValue = 0;
            byte[] oData = new byte[m_DO_Pmax / 8];
            short[] PortNo = new short[m_DO_Pmax / 8];

            for (int i = 0; i < m_DO_Pmax / 8; i++)
                PortNo[i] = (short)i;

            if (m_cDioEmu == 0)
            {
                //if (m_DIid != null)
                //{
                //    for (int i = 0; i < m_DeviceNameDI.Length; i++)
                //    {
                //        Ret = dio.Exit(m_DIid[i]);
                //        if (Ret != 0)
                //        {
                //            returnValue = Ret;
                //        }
                //        GetErrorString("dio.Exit", Ret);
                //        m_DIid[i] = 0;
                //    }
                //    //m_DIid = null;
                //}
                //else
                //{
                //    //m_DIid
                //}
                if (m_DOid != null)
                {
                    for (int i = 0; i < m_DeviceNameDO.Length; i++)
                    {
                        if (m_DOid[i] != 0)
                        {
                            dio.OutMultiByte(m_DOid[i], PortNo, (short)(m_DO_Pmax / 8), oData);
                        }

                        Ret = dio.Exit(m_DOid[i]);
                        if (Ret != 0)
                        {
                            returnValue = Ret;
                        }
                        GetErrorString("dio.Exit", (int)Ret);
                    }
                    //m_DIid = null;
                }
                else
                {
                    //m_DOid
                }
            }
            else
            {
                //m_cDioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutBit()
        public CioDeviceErrorCode OutBit(int iUnit, int iBitNo, int iData)
        {
            CioDeviceErrorCode Ret;
            if (m_cDioEmu == 0)
            {
                //short shortBitNo = (short)(iBitNo - m_DImax[iUnit]);   //各Unitの入力分を引く
                short shortBitNo = (short)(iBitNo);   //各Unitの入力分を引く
                byte byteData = (byte)iData;
                //-----------------------------
                // Port output
                //-----------------------------
                Ret = dio.OutBit(m_DOid[iUnit], shortBitNo, byteData);
                //Cyc.IO.Log.WriteLine(DIO_LogLevel, "cDIO:OutBit", string.Format("Id={0},Bit={1},Data={2}", iUnit, iBitNo, iData));
                //-----------------------------
                // Error process
                //-----------------------------
                GetErrorString("dio.OutBit", (int)Ret);
            }
            else
            {
                Ret = 0;
            }

            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutBit()
        public CioDeviceErrorCode EchoBackBit(int iUnit, int iBitNo, out int oData)
        {
            CioDeviceErrorCode Ret;
            if (m_cDioEmu == 0)
            {
                //short shortBitNo = (short)(iBitNo - m_DImax[iUnit]);   //各Unitの入力分を引く
                short shortBitNo = (short)(iBitNo);   //各Unitの入力分を引く
                byte byteData = 0;
                //-----------------------------
                // Port output
                //-----------------------------
                Ret = dio.EchoBackBit(m_DOid[iUnit], shortBitNo, out byteData);
                oData = (int)byteData;
                //Cyc.IO.Log.WriteLine(DIO_LogLevel, "cDIO:EchoBackBit", string.Format("Id={0},Bit={1},Data={2}", iUnit, iBitNo, oData));
                //-----------------------------
                // Error process
                //-----------------------------
                GetErrorString("dio.EchoBackBit", (int)Ret);
            }
            else
            {
                Ret = 0;
                oData = 0;
            }

            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutBit()
        public CioDeviceErrorCode OutBit(string DevTitle, int oData)
        {
            if (m_cDioEmu != 0) return 0;
            CioDeviceErrorCode ReturnValue = 0;
            if (dicDeviceItem.ContainsKey(DevTitle))
            {
                int DeviceNo = dicDeviceItem[DevTitle].DevNo;
                int UnitNo = dicDeviceItem[DevTitle].UnitNo;
                CioDeviceErrorCode errcode = OutBit(UnitNo, DeviceNo, oData);
                ReturnValue = errcode;
            }
            else
            {
                throw (new Exception(string.Format("{0}:get:{1}:DevTitle該当なし", m_titleText, DevTitle)));
            }
            return ReturnValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutMultiBit()
        public CioDeviceErrorCode OutMultiBit(int iUnit, int iBitNo, int[] iData)
        {
            CioDeviceErrorCode Ret;
            if (m_cDioEmu == 0)
            {
                short[] shortPortNo = new short[iData.Length];
                byte[] byteData = new byte[iData.Length];
                //-----------------------------
                // Set data
                //-----------------------------
                for (int i = 0; i < iData.Length; i++)
                {
                    byteData[i] = (byte)iData[i];
                    shortPortNo[i] = (short)(iBitNo + i);
                }
                //-----------------------------
                // Port output
                //-----------------------------
                Ret = dio.OutMultiBit(m_DOid[iUnit], shortPortNo, (short)iData.Length, byteData);
                //-----------------------------
                // Error process
                //-----------------------------
                GetErrorString("dio.OutMultiBit", (int)Ret);
            }
            else
            {
                Ret = 0;
            }

            return Ret;
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////InpBit()
        //public int InpBit(int iUnit, int iBitNo, out int oData)
        //{
        //    int Ret;
        //    if (m_cDioEmu == 0)
        //    {
        //        short shortBitNo = (short)iBitNo;
        //        byte byteData;
        //        //-----------------------------
        //        // Port output
        //        //-----------------------------
        //        Ret = dio.InpBit(m_DIid[iUnit], shortBitNo, out byteData);
        //        oData = byteData;
        //        //-----------------------------
        //        // Error process
        //        //-----------------------------
        //        GetErrorString("dio.InpBit", Ret);
        //    }
        //    else
        //    {
        //        Ret = 0;
        //        oData = -1;
        //    }

        //    return Ret;
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////InpBit()
        //public int InpBit(string DevTitle, out int oData)
        //{
        //    oData = 0;
        //    if (m_cDioEmu != 0) return 0;
        //    int ReturnValue = 0;
        //    if (dicDeviceItem.ContainsKey(DevTitle))
        //    {
        //        int DeviceNo = dicDeviceItem[DevTitle].DevNo;
        //        int UnitNo = dicDeviceItem[DevTitle].UnitNo;
        //        int errcode = InpBit(UnitNo,DeviceNo, out ReturnValue);
        //        ReturnValue = errcode;
        //    }
        //    else
        //    {
        //        throw (new Exception(string.Format("{0}:get:{1}:DevTitle該当なし", m_titleText, DevTitle)));
        //    }
        //    return ReturnValue;
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////InpMultiBit()
        //public int InpMultiBit(int iUnit, int iBitNo, int[] oData)
        //{
        //    int Ret;
        //    if (m_cDioEmu == 0)
        //    {
        //        short[] shortPortNo = new short[oData.Length];
        //        byte[] byteData = new byte[oData.Length];
        //        //-----------------------------
        //        // Set data
        //        //-----------------------------
        //        for (int i = 0; i < oData.Length; i++)
        //        {
        //            shortPortNo[i] = (short)(iBitNo + i);
        //        }
        //        //-----------------------------
        //        // Port output
        //        //-----------------------------
        //        Ret = dio.InpMultiBit(m_DIid[iUnit], shortPortNo, (short)oData.Length, byteData);
        //        //-----------------------------
        //        // Get data
        //        //-----------------------------
        //        for (int i = 0; i < oData.Length; i++)
        //        {
        //            oData[i] = byteData[i];
        //        }
        //        //-----------------------------
        //        // Error process
        //        //-----------------------------
        //        GetErrorString("dio.InpMultiBit", Ret);
        //    }
        //    else
        //    {
        //        Ret = 0;
        //    }

        //    return Ret;
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////InpMultiBit()
        //public int InpAllBit(int iUnit)
        //{
        //    //if (oData.Length < m_DImax)
        //    //{
        //    //    throw (new Exception(string.Format("{0}:InpAllBit:{1}:InpAllBit:oDataサイズ異常", m_titleText)));
        //    //}
        //    int ReturnValue = 0;
        //    if (m_cDioEmu == 0)
        //    {
        //        int errcode = InpMultiBit(0,0, inpData);
        //        ReturnValue = errcode;
        //    }
        //    return ReturnValue;
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //DioEchoBackMultiBit  ()
        public CioDeviceErrorCode EchoBackMultiBit(int iUnit, int iBitNo, int[] oData)
        {
            CioDeviceErrorCode Ret;
            if (m_cDioEmu == 0)
            {
                short[] shortPortNo = new short[oData.Length];
                byte[] byteData = new byte[oData.Length];
                //-----------------------------
                // Set data
                //-----------------------------
                for (int i = 0; i < oData.Length; i++)
                {
                    shortPortNo[i] = (short)(iBitNo + i);
                }
                //-----------------------------
                // Port output
                //-----------------------------
                Ret = dio.EchoBackMultiBit(m_DOid[iUnit], shortPortNo, (short)oData.Length, byteData);
                //-----------------------------
                // Get data
                //-----------------------------
                for (int i = 0; i < oData.Length; i++)
                {
                    oData[i] = byteData[i];
                }
                //-----------------------------
                // Error process
                //-----------------------------
                GetErrorString("dio.EchoBackMultiBit", (int)Ret);
            }
            else
            {
                Ret = 0;
            }

            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //GetErrorString()
        public void GetErrorString(string funcName, int Ret)
        {
            if (Ret != 0)
            {
                if (Ret == -1)
                {
                    m_LastErrorString = "デジタル出力モジュールでエラーが発生しました。処理を終了します。" + "\n" + funcName + " : " + System.Convert.ToString(Ret) + " : " + "Driver not installed";
                }
                else
                {
                    string ErrorString;
                    dio.GetErrorString(Ret, out ErrorString);
                    m_LastErrorString = "デジタル出力モジュールでエラーが発生しました。処理を終了します。" + "\n" + funcName + " : " + System.Convert.ToString(Ret) + " : " + ErrorString;
                }
            }
            else
            {
                m_LastErrorString = null;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //CreateDict()
        public void CreateDict()
        {
            string DevTypeExpressions = @"^([A-Z]+)([0-9]+)$";
            //string DevTypeExpressions = @"^([0-9]*)([IiOo])-([0-7]+)$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(DevTypeExpressions);


            //Excelシートから、通信するデバイスをDeviceManagerに登録する
            ExcelToDataset excel = ExcelToDataset.GetInstance();
            string PlcHostIfExcelFile = Default.SettingsHolder + @"\" /*+ Default.PlcHostIfExcelFile*/;
            //DataSet ds = excel.Read(@"PLC-HOST間IF.xlsx");
            DataSet ds = excel.Read(PlcHostIfExcelFile);
            //辞書をクリアする
            dicDeviceItem.Clear();
            if (ds.Tables.Count != 2)
            {
                throw (new Exception(string.Format("{0}:Init:Excelファイル不正", m_titleText)));
            }

            for (int tableIndex = 0; tableIndex < ds.Tables.Count; tableIndex++)
            {
                DataTable table = ds.Tables[tableIndex];

                foreach (DataRow dr in table.Rows)
                {
                    tagDeviceItem dvItem = new tagDeviceItem();

                    dvItem.DevTitle = dr[1].ToString();     //名称
                    dvItem.DevSize = dr[2].ToString();      //サイズ
                    dvItem.DevName = dr[3].ToString();      //開始
                    dvItem.DevEnd = dr[4].ToString();       //終了
                    dvItem.Comment = dr[6].ToString();      //備考

                    if (dvItem.DevTitle == "" || dvItem.DevName == "")
                        continue;


                    if (regex.IsMatch(dvItem.DevName))
                    {
                        System.Text.RegularExpressions.Match m = regex.Match(dvItem.DevName);

                        string Prefix = m.Groups[1].Value;
                        string DeviceNo = m.Groups[2].Value;

                        if (Prefix == "R") //PLCリレー(Rxxxx)のみを対象
                        {
                            if (DeviceNo.Length >= 3)
                            {
                                int iDeviceNo1 = Convert.ToInt32(DeviceNo.Substring(0, DeviceNo.Length - 2));
                                int iDeviceNo2 = Convert.ToInt32(DeviceNo.Substring(DeviceNo.Length - 2));
                                int DeviceNoOctal = 0;

                                dvItem.UnitNo = 0;
                                dvItem.Prefix = Prefix;
                                switch (iDeviceNo1)
                                {
                                    case 100:
                                    case 101:
                                        DeviceNoOctal = (iDeviceNo1 - 100) * 16 + iDeviceNo2 + 32;  //R10000 -> O-40
                                        break;

                                    case 200:
                                    case 201:
                                        DeviceNoOctal = (iDeviceNo1 - 200) * 16 + iDeviceNo2;   //R20000 -> I-00
                                        break;

                                    default:
                                        throw (new Exception(string.Format("{0}:Init:DevNameが正しくない1:{1}", m_titleText, dvItem.DevTitle)));

                                }
                                dvItem.DevNo = DeviceNoOctal;

                                dicDeviceItem.Add(dvItem.DevTitle, dvItem);
                                Log.WriteLine(DIO_LogLevel, "Cyc.IO.cDio:Init", string.Format("{1}:{2}:{3}<-{0}", dvItem.DevTitle, dvItem.DevName, (dvItem.Prefix).ToString(), dvItem.DevNo.ToString()));
                            }
                        }
                    }
                    else
                    {
                        throw (new Exception(string.Format("{0}:Init:DevNameが正しくない2:{1}", m_titleText, dvItem.DevTitle)));
                    }
                }
            }
        }


    }
}
