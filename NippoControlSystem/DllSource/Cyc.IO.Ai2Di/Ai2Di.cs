using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace Cyc.IO
{
    public class Ai2Di
    {
        //for CONTEC Digital I/O device
        CaioCs.Caio? aio = null;
        private bool m_AioEmu = false;
        private bool m_Connected = false;
        private bool m_Inited = false;
        private short [] m_AIOid;
        private string? m_LastErrorString = null;
        private int m_AI2DI_BDmax = 0;
        private int m_AI2DI_AImax = 0;
        private int m_AI2DI_DImax = 0;
        private int m_AI2DI_DOmax = 0;
        string[] m_DeviceNameAI2DI = null;       //"AIO000";
        int[] m_OutputDoBitEcho = null;
        //private string m_titleText = "Cyc.IO.AI2DI";      //タイトル

        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.Log.LogLevel AIO_LogLevel = Log.LogLevel.LOG_INFO;

        public enum ERR_STAT
        {
            NO_ERROR = 0,  //No error
            POINT_NO_OVER,    //PointNo Over
            IO_STAT_ERROR,
            DIO_ERROR,
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static Ai2Di? m_instance = null;
        public static Ai2Di GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new Ai2Di();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public Ai2Di()
        {
            aio = new CaioCs.Caio();
            AIO_LogLevel = Default.DIO_LogLevel;

            m_AI2DI_BDmax = Default.AI2DI_BDmax;    //ボード枚数
            m_AI2DI_AImax = Default.AI2DI_AImax;    //AI点数/ボード
            m_AI2DI_DImax = Default.AI2DI_DImax;    //DI点数/ボード
            m_AI2DI_DOmax = Default.AI2DI_DOmax;    //DO点数/ボード
            m_DeviceNameAI2DI = new string[m_AI2DI_BDmax];
            m_OutputDoBitEcho = new int[m_AI2DI_BDmax * m_AI2DI_DOmax];
            for (int i = 0; i < m_AI2DI_BDmax; i++)
            {
                m_DeviceNameAI2DI[i] = Default.DeviceNameAI2DI[i];
            }
            m_AIOid = new short[m_AI2DI_BDmax];
            this.Init();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~Ai2Di()
        {
            this.Exit();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // プロパティ
        public string[] DeviceNameAI2DI
        {
            get
            {
                return m_DeviceNameAI2DI;
            }
            set
            {
                m_DeviceNameAI2DI = value;
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
        public bool AioEmu
        {
            get { return m_AioEmu; }
            set { m_AioEmu = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int AI2DI_BDmax
        {
            get { return m_AI2DI_BDmax; }
            //set { m_DImax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int AI2DI_AImax
        {
            get { return m_AI2DI_AImax; }
            //set { m_DOmax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int AI2DI_DImax
        {
            get { return m_AI2DI_DImax; }
            //set { m_DOmax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int AI2DI_DOmax
        {
            get { return m_AI2DI_DOmax; }
            //set { m_DOmax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public short [] AIOid
        {
            set { m_AIOid = value; }
        }

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
        public string LastErrorString
        {
            get { return m_LastErrorString; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Init()
        public int Init()
        {
            // Initialization handling
            int Ret = 0;
            if (m_AioEmu == false)
            {
                for (int i = 0; i < m_AI2DI_BDmax; i++)
                {
                    if (m_AIOid[i] != 0)
                    {
                        aio.Exit(m_AIOid[i]);
                        m_AIOid[i] = 0;
                    }
                    Ret = aio.Init(m_DeviceNameAI2DI[i], out m_AIOid[i]);
                    GetErrorString("dio.Init", Ret);
                }
            }

            else
            {
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Exit()
        public int Exit()
        {
            // Exit handling of device
            int Ret = 0;

            if (m_AioEmu == false)
            {
                for (int i = 0; i < m_AI2DI_BDmax; i++)
                {
                    if (m_AIOid[i] != 0)
                    {
                        Ret = aio.Exit(m_AIOid[i]);

                        GetErrorString("dio.Exit", Ret);
                        m_AIOid[i] = 0;
                    }

                    else
                    {
                        //m_AIOid
                    }
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //SetAiRangeAll()
        public int SetAiRangeAll(int AiRange)
        {
            int Ret = 0;
            if (m_AioEmu == false)
            {
                for (int i = 0; i < AI2DI_BDmax; i++)
                {
                    //入力レンジの設定
                    Ret = aio.SetAiRangeAll(m_AIOid[i], (short)AiRange);   //AiRange = CaioConst.PM10
                    if (Ret != 0)
                    {
                        GetErrorString("aio.SetAiRangeAll", Ret);
                    }
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //SingleAiEx()
        public int SingleAiEx(int AiChannel, out float AiData)
        {
            int Ret = 0;
            AiData = 0f;
            if (m_AioEmu == false)
            {
                if (AiChannel >= 0 && AiChannel < (m_AI2DI_BDmax * m_AI2DI_AImax))
                {
                    int iUnit = AiChannel / AI2DI_AImax;
                    int iChannel = Default.AI2DI_DeviceChannel[AiChannel % AI2DI_AImax];
                    //入力レンジの設定
                    Ret = aio.SingleAiEx(m_AIOid[iUnit], (short)iChannel, out AiData);
                    if (Ret != 0)
                    {
                        GetErrorString("aio.SingleAiEx", Ret);
                    }
                    if (AiData < Default.AI2DI_VoltMin)
                    {
                        AiData = Default.AI2DI_VoltMin;
                    }
                    else if(AiData > Default.AI2DI_VoltMax)
                    {
                        AiData = Default.AI2DI_VoltMax;
                    }
                }
                else
                {
                    Ret = 1;    //AiChannel Over
                }
            }
            else
            {
                AiData = 0.0f;
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //MultiAi()
        public int MultiAiEx(float[] AiData)
        {
            int AiChannels = AiData.Length;
            int startChannel = 0;
            float [] AiBuf = new float[m_AI2DI_AImax];
            int Ret = 0;
            float ScanData;
            if (m_AioEmu == false)
            {
                for (int i = 0; i < AI2DI_BDmax; i++)
                {
                    //入力レンジの設定
                    int readAiChannels = (AiChannels <= m_AI2DI_AImax? AiChannels:m_AI2DI_AImax);
                    Ret = aio.MultiAiEx(m_AIOid[i], (short)readAiChannels, AiBuf);
                    AiChannels -= readAiChannels;
                    if (Ret != 0)
                    {
                        GetErrorString("aio.MultiAi", Ret);
                        break;
                    }
                    for (int j = 0; j < readAiChannels; j++)
                    {
                        int DevChannel = Default.AI2DI_DeviceChannel[j];
                        ScanData = AiBuf[DevChannel];
                        if (ScanData < Default.AI2DI_VoltMin)
                        {
                            ScanData = Default.AI2DI_VoltMin;
                        }
                        else if (ScanData > Default.AI2DI_VoltMax)
                        {
                            ScanData = Default.AI2DI_VoltMax;
                        }
                        AiData[startChannel + j] = ScanData;
                    }
                    startChannel += readAiChannels;
                }
            }
            else
            {
                //m_AioEmu
                for (int i = 0; i < AiChannels; i++)
                {
                    AiData[i] = 0.0f;
                }
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoAIO_SW()：Nippo仕様のAOのSWをコントロールする
        //iPointNo = 0 ～ 7 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public int NippoAIO_SW(int iPointNo, int iStat)
        {
            int iPointNo2 = iPointNo;
            int dioErr = 0;
            ERR_STAT thisStat = ERR_STAT.NO_ERROR;
            if (iPointNo >= 0 && iPointNo < (m_AI2DI_BDmax * m_AI2DI_DOmax))
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                dioErr = OutputDoBit(iPointNo2, iStat);
                if (dioErr != 0)
                {
                    thisStat = ERR_STAT.DIO_ERROR;
                }
            }
            else
            {
                thisStat = ERR_STAT.POINT_NO_OVER;
            }
            return (int)thisStat;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoAIO_SW()：Nippo仕様のAOのSWをコントロールする
        //iPointNo = 0 ～ 7 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public int NippoAIO_SWecho(int iPointNo, out int iStat)
        {
            int iPointNo2 = iPointNo + Default.AoSwichStartBit;
            int dioErr = 0;
            ERR_STAT thisStat = ERR_STAT.NO_ERROR;
            if (iPointNo >= 0 && iPointNo < (m_AI2DI_BDmax * m_AI2DI_DOmax))
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                dioErr = OutputDoBitEcho(iPointNo2, out iStat);
                if (dioErr != 0)
                {
                    thisStat = ERR_STAT.DIO_ERROR;
                }
            }
            else
            {
                iStat = 0;
                thisStat = ERR_STAT.POINT_NO_OVER;
            }
            return (int)thisStat;

        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //InputDiBit()
        public int InputDiBit(int DiBitNo, out int DiData)
        {
            int Ret = 0;
            short sDiData;
            if (m_AioEmu == false)
            {
                //デジタル入力
                Ret = aio.InputDiBit(m_AIOid[DiBitNo / AI2DI_BDmax], (short)(DiBitNo % AI2DI_BDmax), out sDiData);
                DiData = sDiData==0?1:0;
                if (Ret != 0)
                {
                    GetErrorString("aio.InputDiBit", Ret);
                }
            }
            else
            {
                DiData = 0;
            }
            return Ret;
        }
        //int PowerVoltEchoBackData = 0;


        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutputDoBit()
        public int OutputDoBit(int DoBitNo, int DoData)
        {
            int Ret = 0;
            if (m_AioEmu == false)
            {
                //デジタル出力
                Ret = aio.OutputDoBit(m_AIOid[DoBitNo / AI2DI_BDmax], (short)(DoBitNo % AI2DI_BDmax), (short)DoData);
                m_OutputDoBitEcho[DoBitNo] = DoData;

                if (Ret != 0)
                {
                    GetErrorString("aio.OutputDoBit", Ret);
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutputDoBit()
        public int OutputDoBitEcho(int DoBitNo, out int DoData)
        {
            int Ret = 0;
            if (m_AioEmu == false)
            {
                //デジタル出力Echo
                //Ret = aio.OutputDoBit(m_AIOid[DoBitNo / AI2DI_BDmax], (short)(DoBitNo % AI2DI_BDmax), (short)DoData);
                DoData = m_OutputDoBitEcho[DoBitNo];
                if (Ret != 0)
                {
                    GetErrorString("aio.OutputDoBit", Ret);
                }
            }
            else
            {
                //m_AioEmu
                DoData = 0;
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //InputDiBit()
        public int InputDiByte(int DiBitNo, out int DiData)
        {
            int Ret = 0;
            DiData = 0;
            short sDiData;
            if (m_AioEmu == false)
            {
                for (int i = 0; i < AI2DI_BDmax; i++)
                {
                    //デジタル入力
                    Ret = aio.InputDiByte(m_AIOid[i], (short)DiBitNo, out sDiData);
                    DiData = sDiData;
                    if (Ret != 0)
                    {
                        GetErrorString("aio.InputDiByte", Ret);
                    }
                }
            }
            else
            {
                DiData = 0;
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutputDoByte()
        public int OutputDoByte(int DoBitNo, int DoData)
        {
            int Ret = 0;
            if (m_AioEmu == false)
            {
                for (int i = 0; i < AI2DI_BDmax; i++)
                {
                    //デジタル出力
                    Ret = aio.OutputDoByte(m_AIOid[i], (short)DoBitNo, (short)DoData);
                    if (Ret != 0)
                    {
                        GetErrorString("aio.OutputDoByte", Ret);
                    }
                }
            }
            else
            {
                //m_AioEmu
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
                    aio.GetErrorString(Ret, out ErrorString);
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
