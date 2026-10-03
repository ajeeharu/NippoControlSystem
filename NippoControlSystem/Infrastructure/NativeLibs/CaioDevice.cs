using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CaioDevice : ICaioDevice
    {
        private const string LibraryName = "caio.dll";

        // スレッドセーフ確保用のセマフォ (同時実行数: 1)
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        #region Native Imports (LibraryImport)
        [LibraryImport(LibraryName, EntryPoint = "AioInit", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeAioInit(string deviceName, out short id);

        [LibraryImport(LibraryName, EntryPoint = "AioExit")]
        private static partial int NativeAioExit(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioResetDevice")]
        private static partial int NativeAioResetDevice(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioGetErrorString", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeAioGetErrorString(int errorCode, [Out] byte[] errorString);

        [LibraryImport(LibraryName, EntryPoint = "AioQueryDeviceName", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeAioQueryDeviceName(short index, [Out] byte[] deviceName, [Out] byte[] device);

        [LibraryImport(LibraryName, EntryPoint = "AioGetDeviceType", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeAioGetDeviceType(string device, out short deviceType);

        [LibraryImport(LibraryName, EntryPoint = "AioSetControlFilter")]
        private static partial int NativeAioSetControlFilter(short id, short signal, float value);

        [LibraryImport(LibraryName, EntryPoint = "AioGetControlFilter")]
        private static partial int NativeAioGetControlFilter(short id, short signal, out float value);

        [LibraryImport(LibraryName, EntryPoint = "AioResetProcess")]
        private static partial int NativeAioResetProcess(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioSingleAi")]
        private static partial int NativeAioSingleAi(short id, short aiChannel, out int aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioSingleAiEx")]
        private static partial int NativeAioSingleAiEx(short id, short aiChannel, out float aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioMultiAi")]
        private static partial int NativeAioMultiAi(short id, short aiChannels, [In] int[] aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioMultiAiEx")]
        private static partial int NativeAioMultiAiEx(short id, short aiChannels, [In] float[] aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiResolution")]
        private static partial int NativeAioGetAiResolution(short id, out short aiResolution);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiInputMethod")]
        private static partial int NativeAioSetAiInputMethod(short id, short aiInputMethod);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiInputMethod")]
        private static partial int NativeAioGetAiInputMethod(short id, out short aiInputMethod);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiMaxChannels")]
        private static partial int NativeAioGetAiMaxChannels(short id, out short aiMaxChannels);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiChannel")]
        private static partial int NativeAioSetAiChannel(short id, short aiChannel, short enabled);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiChannel")]
        private static partial int NativeAioGetAiChannel(short id, short aiChannel, out short enabled);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiChannels")]
        private static partial int NativeAioSetAiChannels(short id, short aiChannels);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiChannels")]
        private static partial int NativeAioGetAiChannels(short id, out short aiChannels);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiRange")]
        private static partial int NativeAioSetAiRange(short id, short aiChannel, short aiRange);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiRangeAll")]
        private static partial int NativeAioSetAiRangeAll(short id, short aiRange);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiRange")]
        private static partial int NativeAioGetAiRange(short id, short aiChannel, out short aiRange);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAiSamplingClock")]
        private static partial int NativeAioSetAiSamplingClock(short id, float aiSamplingClock);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiSamplingClock")]
        private static partial int NativeAioGetAiSamplingClock(short id, out float aiSamplingClock);

        [LibraryImport(LibraryName, EntryPoint = "AioStartAi")]
        private static partial int NativeAioStartAi(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioStartAiSync")]
        private static partial int NativeAioStartAiSync(short id, int timeOut);

        [LibraryImport(LibraryName, EntryPoint = "AioStopAi")]
        private static partial int NativeAioStopAi(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiStatus")]
        private static partial int NativeAioGetAiStatus(short id, out int aiStatus);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiSamplingCount")]
        private static partial int NativeAioGetAiSamplingCount(short id, out int aiSamplingCount);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiSamplingData")]
        private static partial int NativeAioGetAiSamplingData(short id, ref int aiSamplingTimes, [Out] int[] aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAiSamplingDataEx")]
        private static partial int NativeAioGetAiSamplingDataEx(short id, ref int aiSamplingTimes, [Out] float[] aiData);

        [LibraryImport(LibraryName, EntryPoint = "AioResetAiStatus")]
        private static partial int NativeAioResetAiStatus(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioResetAiMemory")]
        private static partial int NativeAioResetAiMemory(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioSingleAo")]
        private static partial int NativeAioSingleAo(short id, short aoChannel, int aoData);

        [LibraryImport(LibraryName, EntryPoint = "AioSingleAoEx")]
        private static partial int NativeAioSingleAoEx(short id, short aoChannel, float aoData);

        [LibraryImport(LibraryName, EntryPoint = "AioMultiAo")]
        private static partial int NativeAioMultiAo(short id, short aoChannels, [In] int[] aoData);

        [LibraryImport(LibraryName, EntryPoint = "AioMultiAoEx")]
        private static partial int NativeAioMultiAoEx(short id, short aoChannels, [In] float[] aoData);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAoResolution")]
        private static partial int NativeAioGetAoResolution(short id, out short aoResolution);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAoChannels")]
        private static partial int NativeAioSetAoChannels(short id, short aoChannels);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAoChannels")]
        private static partial int NativeAioGetAoChannels(short id, out short aoChannels);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAoRange")]
        private static partial int NativeAioSetAoRange(short id, short aoChannel, short aoRange);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAoRangeAll")]
        private static partial int NativeAioSetAoRangeAll(short id, short aoRange);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAoRange")]
        private static partial int NativeAioGetAoRange(short id, short aoChannel, out short aoRange);

        [LibraryImport(LibraryName, EntryPoint = "AioSetAoSamplingClock")]
        private static partial int NativeAioSetAoSamplingClock(short id, float aoSamplingClock);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAoSamplingClock")]
        private static partial int NativeAioGetAoSamplingClock(short id, out float aoSamplingClock);

        [LibraryImport(LibraryName, EntryPoint = "AioStartAo")]
        private static partial int NativeAioStartAo(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioStopAo")]
        private static partial int NativeAioStopAo(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioGetAoStatus")]
        private static partial int NativeAioGetAoStatus(short id, out int aoStatus);

        [LibraryImport(LibraryName, EntryPoint = "AioResetAoStatus")]
        private static partial int NativeAioResetAoStatus(short id);

        [LibraryImport(LibraryName, EntryPoint = "AioInputDiBit")]
        private static partial int NativeAioInputDiBit(short id, short diBit, out short diData);

        [LibraryImport(LibraryName, EntryPoint = "AioOutputDoBit")]
        private static partial int NativeAioOutputDoBit(short id, short doBit, short doData);

        [LibraryImport(LibraryName, EntryPoint = "AioInputDiByte")]
        private static partial int NativeAioInputDiByte(short id, short diPort, out short diData);

        [LibraryImport(LibraryName, EntryPoint = "AioOutputDoByte")]
        private static partial int NativeAioOutputDoByte(short id, short doPort, short doData);

        [LibraryImport(LibraryName, EntryPoint = "AioSetDioDirection")]
        private static partial int NativeAioSetDioDirection(short id, int dir);

        [LibraryImport(LibraryName, EntryPoint = "AioGetDioDirection")]
        private static partial int NativeAioGetDioDirection(short id, out int dir);
        #endregion

        #region Public Wrapper Methods
        public CioDeviceErrorCode Init(string deviceName, out short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioInit(deviceName, out id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode Exit(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioExit(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetDevice(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioResetDevice(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetErrorString(int errorCode, out string errorString)
        {
            _semaphore.Wait();
            try
            {
                byte[] buffer = new byte[256];
                int ret = NativeAioGetErrorString(errorCode, buffer);
                errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
                return (CioDeviceErrorCode)ret;
            }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device)
        {
            _semaphore.Wait();
            try
            {
                byte[] nameBuf = new byte[256];
                byte[] devBuf = new byte[256];
                int ret = NativeAioQueryDeviceName(index, nameBuf, devBuf);
                if (ret == 0)
                {
                    deviceName = Encoding.Default.GetString(nameBuf).TrimEnd('\0');
                    device = Encoding.Default.GetString(devBuf).TrimEnd('\0');
                }
                else
                {
                    deviceName = string.Empty;
                    device = string.Empty;
                }
                return (CioDeviceErrorCode)ret;
            }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetDeviceType(string device, out short deviceType)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetDeviceType(device, out deviceType); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetControlFilter(short id, short signal, float value)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetControlFilter(id, signal, value); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetControlFilter(short id, short signal, out float value)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetControlFilter(id, signal, out value); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetProcess(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioResetProcess(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SingleAi(short id, short aiChannel, out int aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSingleAi(id, aiChannel, out aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SingleAiEx(short id, short aiChannel, out float aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSingleAiEx(id, aiChannel, out aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode MultiAi(short id, short aiChannels, int[] aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioMultiAi(id, aiChannels, aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode MultiAiEx(short id, short aiChannels, float[] aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioMultiAiEx(id, aiChannels, aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiResolution(short id, out short aiResolution)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiResolution(id, out aiResolution); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiInputMethod(short id, short aiInputMethod)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiInputMethod(id, aiInputMethod); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiInputMethod(short id, out short aiInputMethod)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiInputMethod(id, out aiInputMethod); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiMaxChannels(short id, out short aiMaxChannels)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiMaxChannels(id, out aiMaxChannels); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiChannel(short id, short aiChannel, short enabled)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiChannel(id, aiChannel, enabled); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiChannel(short id, short aiChannel, out short enabled)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiChannel(id, aiChannel, out enabled); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiChannels(short id, short aiChannels)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiChannels(id, aiChannels); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiChannels(short id, out short aiChannels)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiChannels(id, out aiChannels); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiRange(short id, short aiChannel, short aiRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiRange(id, aiChannel, aiRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiRangeAll(short id, short aiRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiRangeAll(id, aiRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiRange(short id, short aiChannel, out short aiRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiRange(id, aiChannel, out aiRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAiSamplingClock(short id, float aiSamplingClock)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAiSamplingClock(id, aiSamplingClock); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiSamplingClock(short id, out float aiSamplingClock)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiSamplingClock(id, out aiSamplingClock); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StartAi(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioStartAi(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StartAiSync(short id, int timeOut)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioStartAiSync(id, timeOut); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StopAi(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioStopAi(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiStatus(short id, out int aiStatus)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiStatus(id, out aiStatus); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiSamplingCount(short id, out int aiSamplingCount)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiSamplingCount(id, out aiSamplingCount); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiSamplingData(id, ref aiSamplingTimes, aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAiSamplingDataEx(id, ref aiSamplingTimes, aiData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetAiStatus(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioResetAiStatus(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetAiMemory(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioResetAiMemory(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SingleAo(short id, short aoChannel, int aoData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSingleAo(id, aoChannel, aoData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SingleAoEx(short id, short aoChannel, float aoData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSingleAoEx(id, aoChannel, aoData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode MultiAo(short id, short aoChannels, int[] aoData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioMultiAo(id, aoChannels, aoData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode MultiAoEx(short id, short aoChannels, float[] aoData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioMultiAoEx(id, aoChannels, aoData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAoResolution(short id, out short aoResolution)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAoResolution(id, out aoResolution); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAoChannels(short id, short aoChannels)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAoChannels(id, aoChannels); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAoChannels(short id, out short aoChannels)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAoChannels(id, out aoChannels); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAoRange(short id, short aoChannel, short aoRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAoRange(id, aoChannel, aoRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAoRangeAll(short id, short aoRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAoRangeAll(id, aoRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAoRange(short id, short aoChannel, out short aoRange)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAoRange(id, aoChannel, out aoRange); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetAoSamplingClock(short id, float aoSamplingClock)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetAoSamplingClock(id, aoSamplingClock); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAoSamplingClock(short id, out float aoSamplingClock)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAoSamplingClock(id, out aoSamplingClock); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StartAo(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioStartAo(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StopAo(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioStopAo(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetAoStatus(short id, out int aoStatus)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetAoStatus(id, out aoStatus); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetAoStatus(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioResetAoStatus(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InputDiBit(short id, short diBit, out short diData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioInputDiBit(id, diBit, out diData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutputDoBit(short id, short doBit, short doData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioOutputDoBit(id, doBit, doData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InputDiByte(short id, short diPort, out short diData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioInputDiByte(id, diPort, out diData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutputDoByte(short id, short doPort, short doData)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioOutputDoByte(id, doPort, doData); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetDioDirection(short id, int dir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioSetDioDirection(id, dir); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetDioDirection(short id, out int dir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeAioGetDioDirection(id, out dir); }
            finally { _semaphore.Release(); }
        }
        #endregion
    }
}