using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Infrastructure.Devices
{
    public class NippoDIO : IDisposable
    {
        private readonly DigitalIO _dio;
        private readonly HardwareSettings _settings;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //変数
        //以下の初期値は、Defaultで上書きされます。
        private const int NUM_DO_BIT_OF_POINT = 4;    //DOの１ポイント当たりのBIT数
        private const int MAX_POINT = 256;            //(SUM( m_DOmax ) - m_AO_USE_IN_DO) / NUM_DO_BIT_OF_POINT

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dio"></param>
        /// <param name="dioNative"></param>
        /// <param name="settings"></param>
        public NippoDIO(DigitalIO dio, HardwareSettings settings)
        {
            _dio = dio;
            _settings = settings;

            _dio.Init();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoDIO_OUT()：Nippo仕様のIOボードへ出力する
        //iPointNo = 0 ～ 256 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public CioDeviceErrorCode NippoDIO_OUT(int iPointNo, IO_STATUS iStatus)
        {
            int iUnit = -1;
            int iBitNo = -1;
            int iPointNo2 = iPointNo * NUM_DO_BIT_OF_POINT;
            int[] oData = new int[NUM_DO_BIT_OF_POINT];
            CioDeviceErrorCode dioErr = CioDeviceErrorCode.ERR_SUCCESS;
            CioDeviceErrorCode thisStat = CioDeviceErrorCode.ERR_SUCCESS;
            if (iPointNo >= 0 && iPointNo < MAX_POINT)
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                iUnit = iPointNo2 / _settings.DO_Pmax; ; //DOは、各ボード共同じ点数を想定
                iBitNo = iPointNo2 % _settings.DO_Pmax; ;
                thisStat = CioDeviceErrorCode.ERR_SUCCESS;
                oData[0] = 0;
                oData[1] = 0;
                oData[2] = 0;
                oData[3] = 0;
                switch (iStatus)
                {
                    case IO_STATUS.oOP:
                    case IO_STATUS.iTo:   //20180912
                        //oData[0] = 0;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;

                    case IO_STATUS.iOP:
                    case IO_STATUS.iGN:
                    case IO_STATUS.iHi:
                    case IO_STATUS.nOP:
                    case IO_STATUS.nGN:
                    case IO_STATUS.nHi:
                    case IO_STATUS.iRD://20180717
                        //oData[0] = 0;
                        //oData[1] = 0;
                        oData[2] = 1;
                        oData[3] = 1;
                        break;

                    case IO_STATUS.iDo:
                    case IO_STATUS.iDh:
                    case IO_STATUS.iDb:
                    case IO_STATUS.iDc:
                    case IO_STATUS.iDs:
                    case IO_STATUS.iDp:
                    //case IO_STATUS.iTo:   //20170126
                    case IO_STATUS.nDo:
                    case IO_STATUS.nDh:
                    case IO_STATUS.nDb:
                    case IO_STATUS.nDc:
                    case IO_STATUS.nDs:
                    case IO_STATUS.nDp:
                    case IO_STATUS.nTo:
                    case IO_STATUS.dat:
                    //case IO_STATUS.dV:
                    case IO_STATUS.iDRD:   //20180717
                        //oData[0] = 0;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        oData[3] = 1;
                        break;
                    case IO_STATUS.oGN:
                        //oData[0] = 0;
                        oData[1] = 1;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;
                    case IO_STATUS.oHi:
                        oData[0] = 1;
                        //oData[1] = 0;
                        //oData[2] = 0;
                        //oData[3] = 0;
                        break;
                    default:
                        thisStat = CioDeviceErrorCode.ERR_SYS_PARAM;
                        break;
                }

                if (thisStat == CioDeviceErrorCode.ERR_SUCCESS)
                {
                    dioErr = _dio.OutMultiBit(iUnit, iBitNo, oData);

                    if (dioErr != (int)CioDeviceErrorCode.ERR_SUCCESS)
                    {
                        thisStat = CioDeviceErrorCode.ERR_SYS_BIT_DATA;
                    }
                }

                return thisStat;
            }
            else
            {
                return CioDeviceErrorCode.ERR_SYS_PARAM;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //NippoDIO_Echo()：Nippo仕様のIOボードから出力のエコーを得る
        //iPointNo = 0 ～ 256 ポイント番号
        //iStat = OPEN、LOW、HIGH
        public int NippoDIO_Echo(int iPointNo, out IO_STATUS iStat)
        {
            int iUnit = -1;
            int iBitNo = -1;
            int iPointNo2 = iPointNo * NUM_DO_BIT_OF_POINT;
            int[] iData = new int[NUM_DO_BIT_OF_POINT];
            CioDeviceErrorCode dioErr = CioDeviceErrorCode.ERR_SUCCESS;
            int iOutSwData = 0;
            int iInSwData = 0;
            CioDeviceErrorCode thisStat = CioDeviceErrorCode.ERR_SUCCESS;

            iStat = IO_STATUS.ERR;

            if (iPointNo >= 0 && iPointNo < MAX_POINT)
            {
                //実際に出力を行うOutMultiBit(int iUnit, int iBitNo, int[] iData)
                iUnit = iPointNo2 / _settings.DO_Pmax; ; //DOは、各ボード共同じ点数を想定
                iBitNo = iPointNo2 % _settings.DO_Pmax; ;
                thisStat = CioDeviceErrorCode.ERR_SUCCESS;
                //
                dioErr = _dio.EchoBackMultiBit(iUnit, iBitNo, iData);
                //dioErr2 = _dioNative.EchoBackBit(DO_SW_UNITNO , iPointNo % 32, out iSwData);ボード１枚時の調整用
                if (dioErr == CioDeviceErrorCode.ERR_SUCCESS)
                {

                    if (iOutSwData == 0 && iInSwData == 0)      //iHi入力リレー、iDh入力リレー共にOFF
                    {
                        //出力SWがON
                        if (iData[0] == 1)
                        {
                            if (iData[1] == 0 && iData[2] == 0 && iData[3] == 0)
                            {
                                iStat = IO_STATUS.oHi;
                            }
                            //else {
                            //    iStat = IO_STATUS.ERR;
                            //}
                        }
                        else if (iData[1] == 1)
                        {
                            if (iData[0] == 0 && iData[2] == 0 && iData[3] == 0)
                            {
                                iStat = IO_STATUS.oGN;
                            }
                            //else {
                            //    iStat = IO_STATUS.ERR;
                            //}
                        }
                        else if (iData[3] == 1)     //iHi or iDh
                        {
                            if (iData[0] == 0 && iData[1] == 0)
                            {
                                if (iData[2] == 1)
                                {
                                    iStat = IO_STATUS.iRD;
                                }
                                else
                                {
                                    iStat = IO_STATUS.iDRD;
                                }
                            }
                            //else {
                            //    iStat = IO_STATUS.ERR;
                            //}
                        }
                        else
                        {
                            if (iData[2] == 0)
                            {
                                iStat = IO_STATUS.oOP;
                            }
                            //else {
                            //    iStat = IO_STATUS.ERR;
                            //}
                        }
                    }
                }
                else
                {
                    thisStat = CioDeviceErrorCode.ERR_SYS_BIT_DATA;
                }
                return (int)thisStat;
            }
            else
            {
                return (int)CioDeviceErrorCode.ERR_SYS_PARAM;
            }
        }
    }
}
