using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;

#pragma warning disable
#nullable disable

namespace NippoControlSystem.Infrastructure.Devices
{
    public class Ai2Di : IDisposable
    {
        private readonly ICaioDevice _aioNative;
        private HardwareSettings _settings;
        /// <summary>
        ///  AIOid は、アナログモジュールのDevice IDを保持するための変数です。Init() メソッドで初期化され、Exit() メソッドで解放されます。
        /// </summary>
        private short[] m_AIOid;
        const int MaxRetries = 5;

        private string? m_LastErrorString = null;
        int[] m_OutputDoBitEcho = null;

        // DIコンテナ経由で ICaioDevice を受け取るコンストラクタ
        public Ai2Di(ICaioDevice aioNative, HardwareSettings hardwareSettings)
        {
            _aioNative = aioNative;
            _settings = hardwareSettings;

            m_OutputDoBitEcho = new int[_settings.AI2DI_BDmax * _settings.AI2DI_DOmax];
            m_AIOid = new short[_settings.AI2DI_BDmax];
            Init();
        }
        public void Dispose()
        {
            Exit();
            GC.SuppressFinalize(this);
        }
        public CioDeviceErrorCode Init()
        {
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            for (int i = 0; i < _settings.AI2DI_BDmax; i++)
            {
                int retryCount = 0;
                do
                {
                    Ret = _aioNative.Init(_settings.DeviceNameAI2DI[i], out m_AIOid[i]);
                    GetErrorString("_dioNative.Init", (int)Ret);
                    if (Ret == CioDeviceErrorCode.ERR_SUCCESS)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(50);
                } while (retryCount++ < MaxRetries);
            }
            return Ret;
        }

        public CioDeviceErrorCode Exit()
        {
            CioDeviceErrorCode Ret = 0;
            for (int i = 0; i < _settings.AI2DI_BDmax; i++)
            {
                if (m_AIOid[i] != 0)
                {
                    Ret = _aioNative.Exit(m_AIOid[i]);
                    GetErrorString("_dioNative.Exit", (int)Ret);
                    m_AIOid[i] = 0;
                }
            }
            return Ret;
        }

        public CioDeviceErrorCode SetAiRangeAll(int AiRange)
        {
            CioDeviceErrorCode Ret = 0;
            for (int i = 0; i < _settings.AI2DI_BDmax; i++)
            {
                Ret = _aioNative.SetAiRangeAll(m_AIOid[i], (short)AiRange);
                if (Ret != 0)
                {
                    GetErrorString("aio.SetAiRangeAll", (int)Ret);
                }
            }
            return Ret;
        }

        public CioDeviceErrorCode SingleAiEx(int AiChannel, out float AiData)
        {
            CioDeviceErrorCode Ret = 0;
            AiData = 0f;
            if (AiChannel >= 0 && AiChannel < (_settings.AI2DI_BDmax * _settings.AI2DI_AImax))
            {
                int iUnit = AiChannel / _settings.AI2DI_AImax;
                int iChannel = _settings.AI2DI_DeviceChannel[AiChannel % _settings.AI2DI_AImax];
                Ret = _aioNative.SingleAiEx(m_AIOid[iUnit], (short)iChannel, out AiData);
                if (Ret != 0)
                {
                    GetErrorString("aio.SingleAiEx", (int)Ret);
                }
                if (AiData < _settings.AI2DI_VoltMin)
                {
                    AiData = _settings.AI2DI_VoltMin;
                }
                else if (AiData > _settings.AI2DI_VoltMax)
                {
                    AiData = _settings.AI2DI_VoltMax;
                }
            }
            else
            {
                Ret = (CioDeviceErrorCode)1;
            }
            return Ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AiData"></param>
        /// <returns></returns>
        public CioDeviceErrorCode MultiAiEx(float[] AiData)
        {
            int AiChannels = AiData.Length;
            int startChannel = 0;
            float[] AiBuf = new float[_settings.AI2DI_AImax];
            CioDeviceErrorCode Ret = 0;
            float ScanData;

            for (int i = 0; i < _settings.AI2DI_BDmax; i++)
            {
                int readAiChannels = (AiChannels <= _settings.AI2DI_AImax ? AiChannels : _settings.AI2DI_AImax);
                Ret = _aioNative.MultiAiEx(m_AIOid[i], (short)readAiChannels, AiBuf);
                AiChannels -= readAiChannels;
                if (Ret != 0)
                {
                    GetErrorString("aio.MultiAi", (int)Ret);
                    break;
                }
                for (int j = 0; j < readAiChannels; j++)
                {
                    int DevChannel = _settings.AI2DI_DeviceChannel[j];
                    ScanData = AiBuf[DevChannel];
                    if (ScanData < _settings.AI2DI_VoltMin)
                    {
                        ScanData = _settings.AI2DI_VoltMin;
                    }
                    else if (ScanData > _settings.AI2DI_VoltMax)
                    {
                        ScanData = _settings.AI2DI_VoltMax;
                    }
                    AiData[startChannel + j] = ScanData;
                }
                startChannel += readAiChannels;
            }
            return Ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPointNo"></param>
        /// <param name="iStat"></param>
        /// <returns></returns>
        public CioDeviceErrorCode NippoAIO_SW(int iPointNo, int iStat)
        {
            int iPointNo2 = iPointNo;
            CioDeviceErrorCode dioErr = CioDeviceErrorCode.ERR_SUCCESS;
            CioDeviceErrorCode thisStat = CioDeviceErrorCode.ERR_SUCCESS;
            if (iPointNo >= 0 && iPointNo < (_settings.AI2DI_BDmax * _settings.AI2DI_DOmax))
            {
                dioErr = OutputDoBit(iPointNo2, iStat);
                if (dioErr != 0)
                {
                    thisStat = CioDeviceErrorCode.ERR_SYS_BIT_DATA;
                }
            }
            else
            {
                thisStat = CioDeviceErrorCode.ERR_SYS_PARAM;
            }
            return thisStat;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoBitNo"></param>
        /// <param name="DoData"></param>
        /// <returns></returns>
        public CioDeviceErrorCode OutputDoBit(int DoBitNo, int DoData)
        {
            CioDeviceErrorCode Ret = 0;
            Ret = _aioNative.OutputDoBit(m_AIOid[DoBitNo / _settings.AI2DI_BDmax], (short)(DoBitNo % _settings.AI2DI_BDmax), (short)DoData);
            m_OutputDoBitEcho[DoBitNo] = DoData;

            if (Ret != 0)
            {
                GetErrorString("aio.OutputDoBit", (int)Ret);
            }
            return Ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoBitNo"></param>
        /// <param name="DoData"></param>
        /// <returns></returns>
        public CioDeviceErrorCode OutputDoBitEcho(int DoBitNo, out int DoData)
        {
            DoData = m_OutputDoBitEcho[DoBitNo];
            return CioDeviceErrorCode.ERR_SUCCESS;
        }
        /// <summary>
        /// Gets the error string for a given function and return code.
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="Ret"></param>
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
                    _aioNative.GetErrorString(Ret, out ErrorString);
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