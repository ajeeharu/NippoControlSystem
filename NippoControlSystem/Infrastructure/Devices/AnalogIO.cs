using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Infrastructure.Devices
{
    public class AnalogIO : IDisposable
    {
        private readonly ICaioDevice _aioDevice;
        private HardwareSettings _settings;
        /// <summary>
        ///  AIOid は、アナログモジュールのDevice IDを保持するための変数です。Init() メソッドで初期化され、Exit() メソッドで解放されます。
        /// </summary>
        private short m_AIOid;
        private string? m_LastErrorString = null;
        const int MaxRetries = 5;
        /// <summary>
        /// コンストラクタで ICaioDevice と HardwareSettings を受け取る
        /// </summary>
        /// <param name="aioDevice"></param>
        /// <param name="hardwareSettings"></param>
        public AnalogIO(ICaioDevice aioDevice, HardwareSettings hardwareSettings)
        {
            _aioDevice = aioDevice;
            _settings = hardwareSettings;

            Init();
        }
        /// <summary>
        /// Dispose メソッドでリソースを解放する
        /// </summary>
        public void Dispose()
        {
            Exit();
            GC.SuppressFinalize(this);
        }

        // Init()
        public CioDeviceErrorCode Init()
        {
            CioDeviceErrorCode Ret;
            int retryCount = 0;
            do
            {
                Ret = _aioDevice.Init(_settings.DeviceNameAIO, out m_AIOid);
                // ログ（何回目のリトライでどのエラーが出たか記録）
                GetErrorString($"_aioDevice.Init (Try: {retryCount + 1})", (int)Ret);
                if (Ret == CioDeviceErrorCode.ERR_SUCCESS)
                {
                    break;
                }
                // ハードウェアの復帰やビジー解除を待つためなウェイト（例: 50ミリ秒）
                System.Threading.Thread.Sleep(50);
            } while (retryCount++ < MaxRetries);
            return Ret;
        }

        // Exit()
        public CioDeviceErrorCode Exit()
        {
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            if (m_AIOid != 0)
            {
                Ret = _aioDevice.Exit(m_AIOid);
                GetErrorString("_dioNative.Exit", (int)Ret);
                m_AIOid = 0;
            }
            return Ret;
        }

        // SetAiRangeAll()
        public CioDeviceErrorCode SetAiRangeAll(int AiRange)
        {
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            Ret = _aioDevice.SetAiRangeAll(m_AIOid, (short)AiRange);
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS)
            {
                GetErrorString("aio.SetAiRangeAll", (int)Ret);
            }
            return Ret;
        }

        // MultiAi()
        public CioDeviceErrorCode MultiAi(float[] AiData)
        {
            short AiChannels = (short)AiData.Length;
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;

            Ret = _aioDevice.MultiAiEx(m_AIOid, AiChannels, AiData);
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS)
            {
                GetErrorString("aio.MultiAi", (int)Ret);
            }
            for (int i = 0; i < AiChannels; i++)
            {
                AiData[i] = (AiData[i] * _settings.AiCalib_A[i] + _settings.AiCalib_B[i]) * _settings.AiMulti;
            }
            return Ret;
        }

        // SingleAoEx()
        public CioDeviceErrorCode SingleAoEx(int AiChannel, float AoData)
        {
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            double AoDataCalib;

            AoDataCalib = (AoData * AoData * _settings.AoCalib_A[AiChannel] + AoData * _settings.AoCalib_B[AiChannel] + _settings.AoCalib_C[AiChannel]) / _settings.AoDiv;
            Ret = _aioDevice.SingleAoEx(m_AIOid, (short)AiChannel, (float)AoDataCalib);
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS)
            {
                GetErrorString("aio.SingleAoEx", (int)Ret);
            }
            return Ret;
        }
        // getGreenSw()
        public int getGreenSw()
        {
            int Value = 0;
            CioDeviceErrorCode Ret = InputDiBit(_settings.DI_BIT_GREEN_SWITCH, out Value);
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS) Value = 0;
            return Value;
        }

        // getRedSw()
        public int getRedSw()
        {
            int Value = 0;
            CioDeviceErrorCode Ret = InputDiBit(_settings.DI_BIT_RED_SWITCH, out Value);
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS) Value = 0;
            return Value;
        }

        // InputDiBit()
        private CioDeviceErrorCode InputDiBit(int DiBitNo, out int DiData)
        {
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            short sDiData;

            Ret = _aioDevice.InputDiBit(m_AIOid, (short)DiBitNo, out sDiData);
            DiData = sDiData == 0 ? 1 : 0;
            if (Ret != CioDeviceErrorCode.ERR_SUCCESS)
            {
                GetErrorString("aio.InputDiBit", (int)Ret);
            }
            return Ret;
        }

        // setPowerVolt()
        public CioDeviceErrorCode setPowerVolt(int DoData)
        {
            return OutputDoBit(_settings.DO_BIT_SWITCHING_POWER, DoData);
        }

        public CioDeviceErrorCode setPower12V()
        {
            return setPowerVolt(0);
        }

        public CioDeviceErrorCode setPower24V()
        {
            return setPowerVolt(1);
        }

        public CioDeviceErrorCode setGreenLamp(int DoData)
        {
            return OutputDoBit(_settings.DO_BIT_GREEN_LED, (short)(DoData == 0 ? 1 : 0));
        }

        public CioDeviceErrorCode setRedLamp(int DoData)
        {
            return OutputDoBit(_settings.DO_BIT_RED_LED, (short)(DoData == 0 ? 1 : 0));
        }

        public CioDeviceErrorCode setInspctLamp(int DoData)
        {
            return OutputDoBit(_settings.DO_BIT_INSPECTION_LED, (short)(DoData == 0 ? 1 : 0));
        }

        // OutputDoBit()
        private CioDeviceErrorCode OutputDoBit(int DoBitNo, int DoData)
        {
            CioDeviceErrorCode Ret = 0;

            Ret = _aioDevice.OutputDoBit(m_AIOid, (short)DoBitNo, (short)DoData);
            if (Ret != 0)
            {
                GetErrorString("aio.OutputDoBit", (int)Ret);
            }
            return Ret;
        }

        // GetErrorString()
        public void GetErrorString(string funcName, int Ret)
        {
            if (Ret != 0)
            {
                if (Ret == -1)
                {
                    m_LastErrorString = "アナログモジュールでエラーが発生しました。処理を終了します。\n" + funcName + " : " + System.Convert.ToString(Ret) + " : Driver not installed";
                }
                else
                {
                    string ErrorString;
                    _aioDevice.GetErrorString(Ret, out ErrorString);
                    m_LastErrorString = "アナログモジュールでエラーが発生しました。処理を終了します。\n" + funcName + " : " + System.Convert.ToString(Ret) + " : " + ErrorString;
                }
            }
            else
            {
                m_LastErrorString = null;
            }
        }
    }
}