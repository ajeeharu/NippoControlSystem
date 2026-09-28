using NippoControlSystem.Domain.Interfaces.enums;
using NippoControlSystem.Infrastructure.NativeLibs;

namespace NippoControlSystem.Infrastructure.Devices
{
    public class DigitalIO
    {
        //for CONTEC Digital I/O device
        CdioDevice dio = null;
        private int m_cDioEmu = 0;
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
            public int DevNo;      //数字部
            public int UnitNo;      //Unit部
        }

        //DeviceItem辞書
        Dictionary<string, tagDeviceItem> dicDeviceItem = null;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static DigitalIO m_instance = null;
        public static DigitalIO GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new DigitalIO();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public DigitalIO()
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
        //Proparty
        public int DO_Pmax
        {
            get { return m_DO_Pmax; }
            //set { m_DOmax = value; }
        }

        public CioDeviceErrorCode Init()
        {
            // Initialization handling
            CioDeviceErrorCode Ret = 0;
            CioDeviceErrorCode returnValue = 0;
            if (m_cDioEmu == 0)
            {
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

    }
}
