using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.Domain.Interfaces.enums;
using NippoControlSystem.Infrastructure.NativeLibs;

namespace NippoControlSystem.Infrastructure.Devices
{
    public class DigitalIO : IDisposable
    {
        //for CONTEC Digital I/O device
        private readonly CdioDevice _dioNative;
        private readonly HardwareSettings _settings;
        /// <summary>
        /// DOid は、デジタル出力モジュールのDevice IDを保持するための変数です。Init() メソッドで初期化され、Exit() メソッドで解放されます。
        /// </summary>
        private short[] m_DOid;
        const int MaxRetries = 5;
        private string? m_LastErrorString = null;


        /// <summary>
        /// DigitalIO クラスのコンストラクタ
        /// </summary>
        /// <param name="dioNative"></param>
        /// <param name="harudwareSettings"></param>
        public DigitalIO(CdioDevice dioNative, HardwareSettings harudwareSettings)
        {
            _dioNative = dioNative;
            _settings = harudwareSettings;

            m_DOid = new short[_settings.DeviceNameDO.Length];
            Init();
        }
        public void Dispose()
        {
            Exit();
            GC.SuppressFinalize(this);
        }

        public CioDeviceErrorCode Init()
        {
            // Initialization handling
            CioDeviceErrorCode Ret = CioDeviceErrorCode.ERR_SUCCESS;
            for (int i = 0; i < _settings.DeviceNameDO.Length; i++)
            {
                int retryCount = 0;
                do
                {
                    Ret = _dioNative.Init(_settings.DeviceNameDO[i], out m_DOid[i]);
                    GetErrorString($"_dioNative.Init (Try: {i + 1})", (int)Ret);
                    if (Ret == CioDeviceErrorCode.ERR_SUCCESS)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(50);
                } while (retryCount++ < MaxRetries);
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Exit()
        public CioDeviceErrorCode Exit()
        {
            // Exit handling of device
            CioDeviceErrorCode Ret = 0;
            CioDeviceErrorCode returnValue = 0;
            byte[] oData = new byte[_settings.DO_Pmax / 8];
            short[] PortNo = new short[_settings.DO_Pmax / 8];

            for (int i = 0; i < _settings.DO_Pmax / 8; i++)
                PortNo[i] = (short)i;

            if (m_DOid != null)
            {
                for (int i = 0; i < _settings.DeviceNameDO.Length; i++)
                {
                    if (m_DOid[i] != 0)
                    {
                        _dioNative.OutMultiByte(m_DOid[i], PortNo, (short)(_settings.DO_Pmax / 8), oData);
                    }

                    Ret = _dioNative.Exit(m_DOid[i]);
                    if (Ret != 0)
                    {
                        returnValue = Ret;
                    }
                    GetErrorString("_dioNative.Exit", (int)Ret);
                }
                //m_DIid = null;
            }
            else
            {
                //m_DOid
            }
            return Ret;
        }
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
                    _dioNative.GetErrorString(Ret, out ErrorString);
                    m_LastErrorString = "デジタル出力モジュールでエラーが発生しました。処理を終了します。" + "\n" + funcName + " : " + System.Convert.ToString(Ret) + " : " + ErrorString;
                }
            }
            else
            {
                m_LastErrorString = null;
            }
        }
        public CioDeviceErrorCode OutMultiBit(int iUnit, int iBitNo, int[] iData)
        {
            CioDeviceErrorCode Ret;
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
            Ret = _dioNative.OutMultiBit(m_DOid[iUnit], shortPortNo, (short)iData.Length, byteData);
            //-----------------------------
            // Error process
            //-----------------------------
            GetErrorString("dio.OutMultiBit", (int)Ret);

            return Ret;
        }
        public CioDeviceErrorCode EchoBackMultiBit(int iUnit, int iBitNo, int[] oData)
        {
            CioDeviceErrorCode Ret;
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
            Ret = _dioNative.EchoBackMultiBit(m_DOid[iUnit], shortPortNo, (short)oData.Length, byteData);
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
            return Ret;
        }
    }
}
