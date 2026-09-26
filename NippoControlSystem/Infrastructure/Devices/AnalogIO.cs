using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;
using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Services;


#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.Infrastructure.Devices
{
    public class AnalogIO
    {
        //for CONTEC Digital I/O device
        private readonly ICaioDevice _aioDevice;
        private bool m_AioEmu = false;
        private bool m_Connected = false;
        private bool m_Inited = false;
        private short m_AIOid = -1;
        private string? m_LastErrorString = null;
        private int m_AImax = 0;
        private int m_AOmax = 0;
        string? m_DeviceNameAIO = null;       //"AIO000";
                                              //private string m_titleText = "Cyc.IO.AIO";      //タイトル

        // コンストラクタで ICaioDevice を受け取る
        public AnalogIO(ICaioDevice aioDevice)
        {
            _aioDevice = aioDevice ?? throw new ArgumentNullException(nameof(aioDevice));

            AIO_LogLevel = Default.DIO_LogLevel;
            m_AImax = Default.AImax;
            m_AOmax = Default.AOmax;
            m_DeviceNameAIO = Default.DeviceNameAIO;

            this.Init();
        }


        Settings Default = Settings.GetInstance();
        Log.LogLevel AIO_LogLevel = Log.LogLevel.LOG_INFO;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~AnalogIO()
        {
            this.Exit();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // プロパティ
        public string DeviceNameAIO
        {
            get
            {
                return m_DeviceNameAIO;
            }
            set
            {
                m_DeviceNameAIO = value;
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
        public int AImax
        {
            get { return m_AImax; }
            //set { m_DImax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public int AOmax
        {
            get { return m_AOmax; }
            //set { m_DOmax = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Proparty
        public short AIOid
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
        public CioDeviceErrorCode Init()
        {
            // Initialization handling
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                if (m_AIOid != 0)
                {
                    _aioDevice.Exit(m_AIOid);
                    m_AIOid = 0;
                }
                Ret = _aioDevice.Init(m_DeviceNameAIO, out m_AIOid);
                GetErrorString("dio.Init", (int)Ret);
            }

            else
            {
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Exit()
        public CioDeviceErrorCode Exit()
        {
            // Exit handling of device
            CioDeviceErrorCode Ret = 0;

            if (m_AioEmu == false)
            {
                if (m_AIOid != 0)
                {
                    Ret = _aioDevice.Exit(m_AIOid);

                    GetErrorString("dio.Exit", (int)Ret);
                    m_AIOid = 0;
                }

                else
                {
                    //m_AIOid
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
        public CioDeviceErrorCode SetAiRangeAll(int AiRange)
        {
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //入力レンジの設定
                Ret = _aioDevice.SetAiRangeAll(m_AIOid, (short)AiRange);   //AiRange = CaioConst.PM10
                if (Ret != 0)
                {
                    GetErrorString("aio.SetAiRangeAll", (int)Ret);
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
        public CioDeviceErrorCode SingleAiEx(int AiChannel, out float AiData)
        {
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //入力レンジの設定
                Ret = _aioDevice.SingleAiEx(m_AIOid, (short)AiChannel, out AiData);
                if (Ret != 0)
                {
                    GetErrorString("aio.SingleAiEx", (int)Ret);
                }
                AiData = (AiData * Default.AiCalib_A[AiChannel] + Default.AiCalib_B[AiChannel]) * Default.AiMulti;
            }
            else
            {
                AiData = 0.0f;
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //MultiAi()
        public CioDeviceErrorCode MultiAi(float[] AiData)
        {
            short AiChannels = (short)AiData.Length;
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //入力レンジの設定
                Ret = _aioDevice.MultiAiEx(m_AIOid, AiChannels, AiData);
                if (Ret != 0)
                {
                    GetErrorString("aio.MultiAi", (int)Ret);
                }
                for (int i = 0; i < AiChannels; i++)
                {
                    AiData[i] = (AiData[i] * Default.AiCalib_A[i] + Default.AiCalib_B[i]) * Default.AiMulti;
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
        //SingleAoEx()
        public CioDeviceErrorCode SingleAoEx(int AiChannel, float AoData)
        {
            CioDeviceErrorCode Ret = 0;
            double AoDataCalib;
            if (m_AioEmu == false)
            {
                //アナログ出力
                AoDataCalib = (AoData * AoData * Default.AoCalib_A[AiChannel] + AoData * Default.AoCalib_B[AiChannel] + Default.AoCalib_C[AiChannel]) / Default.AoDiv;
                Ret = _aioDevice.SingleAoEx(m_AIOid, (short)AiChannel, (float)AoDataCalib);
                if (Ret != 0)
                {
                    GetErrorString("aio.SingleAoEx", (int)Ret);
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //MultiAoEx()
        public CioDeviceErrorCode MultiAoEx(float[] AoData)
        {
            short AoMaxChannels = (short)AoData.Length;
            float[] AoDataCalib = new float[AoData.Length];
            for (int i = 0; i < AoData.Length; i++)
            {
                AoDataCalib[i] = (AoData[i] * AoData[i] * Default.AoCalib_A[i] + AoData[i] * Default.AoCalib_B[i] + Default.AoCalib_C[i]) / Default.AoDiv;
            }
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //アナログ出力
                Ret = _aioDevice.MultiAoEx(m_AIOid, AoMaxChannels, AoDataCalib);
                if (Ret != 0)
                {
                    GetErrorString("aio.MultiAoEx", (int)Ret);
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //getGreenSw()
        public int getGreenSw()
        {
            int Value = 0;
            CioDeviceErrorCode Ret = InputDiBit(0, out Value);
            if (Ret != 0) Value = 0;
            return Value;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //getRedSw()
        public int getRedSw()
        {
            int Value = 0;
            CioDeviceErrorCode Ret = InputDiBit(1, out Value);
            if (Ret != 0) Value = 0;
            return Value;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //InputDiBit()
        public CioDeviceErrorCode InputDiBit(int DiBitNo, out int DiData)
        {
            CioDeviceErrorCode Ret = 0;
            short sDiData;
            if (m_AioEmu == false)
            {
                //デジタル入力
                Ret = _aioDevice.InputDiBit(m_AIOid, (short)DiBitNo, out sDiData);
                DiData = sDiData == 0 ? 1 : 0;
                if (Ret != 0)
                {
                    GetErrorString("aio.InputDiBit", (int)Ret);
                }
            }
            else
            {
                DiData = 0;
            }
            return Ret;
        }
        int PowerVoltEchoBackData = 0;
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Power12()
        public CioDeviceErrorCode setPowerVolt(int DoData)
        {
            PowerVoltEchoBackData = DoData;
            return OutputDoBit(0, DoData);  //ON=24V,  OFF=12V
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //EchoBackPowerV()
        public CioDeviceErrorCode EchoBackPowerVolt()
        {
            return (CioDeviceErrorCode)PowerVoltEchoBackData;  //ON=24V,  OFF=12V
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Power12()
        public CioDeviceErrorCode SetPower12V()
        {
            return setPowerVolt(0);  //ON=24V,  OFF=12V
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //Power12()
        public CioDeviceErrorCode SetPower24V()
        {
            return setPowerVolt(1);  //ON=24V,  OFF=12V
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //GreenLamp()
        public CioDeviceErrorCode setGreenLamp(int DoData)
        {
            return OutputDoBit(1, (short)(DoData == 0 ? 1 : 0));
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //RedLamp()
        public CioDeviceErrorCode setRedLamp(int DoData)
        {
            return OutputDoBit(2, (short)(DoData == 0 ? 1 : 0));
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //InspctLamp()
        public CioDeviceErrorCode setInspctLamp(int DoData)
        {
            return OutputDoBit(3, (short)(DoData == 0 ? 1 : 0));
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //OutputDoBit()
        public CioDeviceErrorCode OutputDoBit(int DoBitNo, int DoData)
        {
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //デジタル出力
                Ret = _aioDevice.OutputDoBit(m_AIOid, (short)DoBitNo, (short)DoData);
                if (Ret != 0)
                {
                    GetErrorString("aio.OutputDoBit", (int)Ret);
                }
            }
            else
            {
                //m_AioEmu
            }
            return Ret;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //InputDiBit()
        public CioDeviceErrorCode InputDiByte(int DiBitNo, out int DiData)
        {
            CioDeviceErrorCode Ret = 0;
            short sDiData;
            if (m_AioEmu == false)
            {
                //デジタル入力
                Ret = _aioDevice.InputDiByte(m_AIOid, (short)DiBitNo, out sDiData);
                DiData = sDiData;
                if (Ret != 0)
                {
                    GetErrorString("aio.InputDiByte", (int)Ret);
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
        public CioDeviceErrorCode OutputDoByte(int DoBitNo, int DoData)
        {
            CioDeviceErrorCode Ret = 0;
            if (m_AioEmu == false)
            {
                //デジタル出力
                Ret = _aioDevice.OutputDoByte(m_AIOid, (short)DoBitNo, (short)DoData);
                if (Ret != 0)
                {
                    GetErrorString("aio.OutputDoByte", (int)Ret);
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
                    _aioDevice.GetErrorString(Ret, out ErrorString);
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
