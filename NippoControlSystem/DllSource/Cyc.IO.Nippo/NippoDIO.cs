using NippoControlSystem.Domain.Interfaces;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace Cyc.IO
{
    public class NippoDIO
    {
        //public enum IO_STAT
        //{
        //    OPEN = 0,
        //    GND,
        //    HIGH,
        //    ERR,
        //    READ,
        //}
        public enum IO_STAT //2016/11/11 IO_STATをDioStatに合わせる
        {
            oOP = 0,
            oGN,
            oHi,
            iOP,
            iGN,
            iHi,
            iDo,    //Di Open
            iDh,    //Di 標準High
            iDb,    //Di タイプB High
            iDc,    //Di タイプC High
            iDs,    //Di ステップ毎 High
            iDp,    //Di ポート毎 High
            iTo,
            nOP,
            nGN,
            nHi,
            nDo,
            nDh,
            nDb,
            nDc,
            nDs,
            nDp,
            nTo,
            dat,
            //dV, 
            NoOP,
            ERR,    //iOP時、Low+Low入力
            iRD,    //DoEcho時、Read設定
            iDRD,   //DoEcho時、iD指定
        }
        public enum ERR_STAT
        {
            NO_ERROR = 0,  //No error
            POINT_NO_OVER,    //PointNo Over
            IO_STAT_ERROR,
            DIO_ERROR,
        }

        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.cDio dio = null;
        //private int[] m_DImax = null;
        private int m_DO_Pmax = 0;

        //Cyc.IO.Log.LogLevel NippoDIOLoglevel = Cyc.IO.Log.LogLevel.LOG_ALERT;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //変数
        //以下の初期値は、Defaultで上書きされます。
        //int NUM_DI_BIT_OF_POINT = 0;  //DIの１ポイント当たりのBIT数
        int NUM_DO_BIT_OF_POINT = 4;    //DOの１ポイント当たりのBIT数
        int MAX_POINT = 256;            //(SUM( m_DOmax ) - m_AO_USE_IN_DO) / NUM_DO_BIT_OF_POINT

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        private static NippoDIO m_instance = null;
        public static NippoDIO GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new NippoDIO();
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public NippoDIO()
        {
            // 
            // TODO: コンストラクタ ロジックをここに追加してください。
            //
            dio = Cyc.IO.cDio.GetInstance();
            dio.Init();
            //m_DImax = dio.DImax;
            m_DO_Pmax = dio.DO_Pmax;
            //m_AO_USE_IN_DO = dio.AO_USE_IN_DO;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~NippoDIO()
        {
        }

        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //private string m_titleText = "Cyc.IO.NippoDIO";      //タイトル

        ////プロパティMyUnitNo
        //public int MyUnitNo
        //{
        //    get { return m_UnitNo; }
        //    set { m_UnitNo = value; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //	[初期化ルーチン]
        public uint Init()
        {
            uint Ret = 0U;                                   //出力OFF
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //	[終了処理ルーチン]
        public uint Close()
        {
            return 0U;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoDIO_OUT()：Nippo仕様のIOボードへ出力する
        //iPointNo = 0 ～ 256 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public ERR_STAT NippoDIO_OUT(int iPointNo, IO_STAT iStat)
        {
            int iUnit = -1;
            int iBitNo = -1;
            int iPointNo2 = iPointNo * NUM_DO_BIT_OF_POINT;
            int[] oData = new int[NUM_DO_BIT_OF_POINT];
            CdioErrorCode dioErr = CdioErrorCode.DIO_ERR_SUCCESS;
            ERR_STAT thisStat = ERR_STAT.NO_ERROR;
            if (iPointNo >= 0 && iPointNo < MAX_POINT)
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                iUnit = iPointNo2 / m_DO_Pmax; //DOは、各ボード共同じ点数を想定
                iBitNo = iPointNo2 % m_DO_Pmax;
                thisStat = ERR_STAT.NO_ERROR;
                oData[0] = 0;
                oData[1] = 0;
                oData[2] = 0;
                oData[3] = 0;
                switch (iStat)
                {
                    case IO_STAT.oOP:
                    case IO_STAT.iTo:   //20180912
                        //oData[0] = 0;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;

                    case IO_STAT.iOP:
                    case IO_STAT.iGN:
                    case IO_STAT.iHi:
                    case IO_STAT.nOP:
                    case IO_STAT.nGN:
                    case IO_STAT.nHi:
                    case IO_STAT.iRD://20180717
                        //oData[0] = 0;
                        //oData[1] = 0;
                        oData[2] = 1;
                        oData[3] = 1;
                        break;

                    case IO_STAT.iDo:
                    case IO_STAT.iDh:
                    case IO_STAT.iDb:
                    case IO_STAT.iDc:
                    case IO_STAT.iDs:
                    case IO_STAT.iDp:
                    //case IO_STAT.iTo:   //20170126
                    case IO_STAT.nDo:
                    case IO_STAT.nDh:
                    case IO_STAT.nDb:
                    case IO_STAT.nDc:
                    case IO_STAT.nDs:
                    case IO_STAT.nDp:
                    case IO_STAT.nTo:
                    case IO_STAT.dat:
                    //case IO_STAT.dV:
                    case IO_STAT.iDRD:   //20180717
                        //oData[0] = 0;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        oData[3] = 1;
                        break;
                    case IO_STAT.oGN:
                        //oData[0] = 0;
                        oData[1] = 1;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;
                    case IO_STAT.oHi:
                        oData[0] = 1;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;
                    default:
                        thisStat = ERR_STAT.IO_STAT_ERROR;
                        break;
                }

                if (thisStat == ERR_STAT.NO_ERROR)
                {
                    dioErr = dio.OutMultiBit(iUnit, iBitNo, oData);

                    if (dioErr != (int)CdioErrorCode.DIO_ERR_SUCCESS)
                    {
                        thisStat = ERR_STAT.DIO_ERROR;
                    }
                }

                return thisStat;
            }
            else
            {
                return ERR_STAT.POINT_NO_OVER;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoDIO_Echo()：Nippo仕様のIOボードから出力のエコーを得る
        //iPointNo = 0 ～ 256 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public int NippoDIO_Echo(int iPointNo, out IO_STAT iStat)
        {
            int iUnit = -1;
            int iBitNo = -1;
            int iPointNo2 = iPointNo * NUM_DO_BIT_OF_POINT;
            int[] iData = new int[NUM_DO_BIT_OF_POINT];
            CdioErrorCode dioErr = CdioErrorCode.DIO_ERR_SUCCESS;
            int iOutSwData = 0;
            int iInSwData = 0;
            ERR_STAT thisStat = ERR_STAT.NO_ERROR;

            iStat = IO_STAT.ERR;

            if (iPointNo >= 0 && iPointNo < MAX_POINT)
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                iUnit = iPointNo2 / m_DO_Pmax; //DOは、各ボード共同じ点数を想定
                iBitNo = iPointNo2 % m_DO_Pmax;
                thisStat = ERR_STAT.NO_ERROR;
                //
                dioErr = dio.EchoBackMultiBit(iUnit, iBitNo, iData);
                //dioErr2 = dio.EchoBackBit(DO_SW_UNITNO , iPointNo % 32, out iSwData);ボード１枚時の調整用
                if (dioErr == CdioErrorCode.DIO_ERR_SUCCESS)
                {

                    if (iOutSwData == 0 && iInSwData == 0)      //iHi入力リレー、iDh入力リレー共にOFF
                    {
                        //出力SWがON
                        if (iData[0] == 1)
                        {
                            if (iData[1] == 0 && iData[2] == 0 && iData[3] == 0)
                            {
                                iStat = IO_STAT.oHi;
                            }
                            //else {
                            //    iStat = IO_STAT.ERR;
                            //}
                        }
                        else if (iData[1] == 1)
                        {
                            if (iData[0] == 0 && iData[2] == 0 && iData[3] == 0)
                            {
                                iStat = IO_STAT.oGN;
                            }
                            //else {
                            //    iStat = IO_STAT.ERR;
                            //}
                        }
                        else if (iData[3] == 1)     //iHi or iDh
                        {
                            if (iData[0] == 0 && iData[1] == 0)
                            {
                                if (iData[2] == 1)
                                {
                                    iStat = IO_STAT.iRD;
                                }
                                else
                                {
                                    iStat = IO_STAT.iDRD;
                                }
                            }
                            //else {
                            //    iStat = IO_STAT.ERR;
                            //}
                        }
                        else
                        {
                            if (iData[2] == 0)
                            {
                                iStat = IO_STAT.oOP;
                            }
                            //else {
                            //    iStat = IO_STAT.ERR;
                            //}
                        }
                    }
                }
                else
                {
                    thisStat = ERR_STAT.DIO_ERROR;
                }
                return (int)thisStat;
            }
            else
            {
                return (int)ERR_STAT.POINT_NO_OVER;
            }
        }
    }
}
