using NippoControlSystem.Domain.Interfaces;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CaioNativeLib : ICaioNative
    {
        private const string LibraryName = "caio.dll";

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
        public CaioErrorCode Init(string deviceName, out short id) => (CaioErrorCode)NativeAioInit(deviceName, out id);
        public CaioErrorCode Exit(short id) => (CaioErrorCode)NativeAioExit(id);
        public CaioErrorCode ResetDevice(short id) => (CaioErrorCode)NativeAioResetDevice(id);

        public CaioErrorCode GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeAioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return (CaioErrorCode)ret;
        }

        public CaioErrorCode QueryDeviceName(short index, out string deviceName, out string device)
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
            return (CaioErrorCode)ret;
        }

        public CaioErrorCode GetDeviceType(string device, out short deviceType) => (CaioErrorCode)NativeAioGetDeviceType(device, out deviceType);
        public CaioErrorCode SetControlFilter(short id, short signal, float value) => (CaioErrorCode)NativeAioSetControlFilter(id, signal, value);
        public CaioErrorCode GetControlFilter(short id, short signal, out float value) => (CaioErrorCode)NativeAioGetControlFilter(id, signal, out value);
        public CaioErrorCode ResetProcess(short id) => (CaioErrorCode)NativeAioResetProcess(id);

        public CaioErrorCode SingleAi(short id, short aiChannel, out int aiData) => (CaioErrorCode)NativeAioSingleAi(id, aiChannel, out aiData);
        public CaioErrorCode SingleAiEx(short id, short aiChannel, out float aiData) => (CaioErrorCode)NativeAioSingleAiEx(id, aiChannel, out aiData);
        public CaioErrorCode MultiAi(short id, short aiChannels, int[] aiData) => (CaioErrorCode)NativeAioMultiAi(id, aiChannels, aiData);
        public CaioErrorCode MultiAiEx(short id, short aiChannels, float[] aiData) => (CaioErrorCode)NativeAioMultiAiEx(id, aiChannels, aiData);
        public CaioErrorCode GetAiResolution(short id, out short aiResolution) => (CaioErrorCode)NativeAioGetAiResolution(id, out aiResolution);
        public CaioErrorCode SetAiInputMethod(short id, short aiInputMethod) => (CaioErrorCode)NativeAioSetAiInputMethod(id, aiInputMethod);
        public CaioErrorCode GetAiInputMethod(short id, out short aiInputMethod) => (CaioErrorCode)NativeAioGetAiInputMethod(id, out aiInputMethod);
        public CaioErrorCode GetAiMaxChannels(short id, out short aiMaxChannels) => (CaioErrorCode)NativeAioGetAiMaxChannels(id, out aiMaxChannels);
        public CaioErrorCode SetAiChannel(short id, short aiChannel, short enabled) => (CaioErrorCode)NativeAioSetAiChannel(id, aiChannel, enabled);
        public CaioErrorCode GetAiChannel(short id, short aiChannel, out short enabled) => (CaioErrorCode)NativeAioGetAiChannel(id, aiChannel, out enabled);
        public CaioErrorCode SetAiChannels(short id, short aiChannels) => (CaioErrorCode)NativeAioSetAiChannels(id, aiChannels);
        public CaioErrorCode GetAiChannels(short id, out short aiChannels) => (CaioErrorCode)NativeAioGetAiChannels(id, out aiChannels);
        public CaioErrorCode SetAiRange(short id, short aiChannel, short aiRange) => (CaioErrorCode)NativeAioSetAiRange(id, aiChannel, aiRange);
        public CaioErrorCode SetAiRangeAll(short id, short aiRange) => (CaioErrorCode)NativeAioSetAiRangeAll(id, aiRange);
        public CaioErrorCode GetAiRange(short id, short aiChannel, out short aiRange) => (CaioErrorCode)NativeAioGetAiRange(id, aiChannel, out aiRange);
        public CaioErrorCode SetAiSamplingClock(short id, float aiSamplingClock) => (CaioErrorCode)NativeAioSetAiSamplingClock(id, aiSamplingClock);
        public CaioErrorCode GetAiSamplingClock(short id, out float aiSamplingClock) => (CaioErrorCode)NativeAioGetAiSamplingClock(id, out aiSamplingClock);
        public CaioErrorCode StartAi(short id) => (CaioErrorCode)NativeAioStartAi(id);
        public CaioErrorCode StartAiSync(short id, int timeOut) => (CaioErrorCode)NativeAioStartAiSync(id, timeOut);
        public CaioErrorCode StopAi(short id) => (CaioErrorCode)NativeAioStopAi(id);
        public CaioErrorCode GetAiStatus(short id, out int aiStatus) => (CaioErrorCode)NativeAioGetAiStatus(id, out aiStatus);
        public CaioErrorCode GetAiSamplingCount(short id, out int aiSamplingCount) => (CaioErrorCode)NativeAioGetAiSamplingCount(id, out aiSamplingCount);
        public CaioErrorCode GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData) => (CaioErrorCode)NativeAioGetAiSamplingData(id, ref aiSamplingTimes, aiData);
        public CaioErrorCode GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData) => (CaioErrorCode)NativeAioGetAiSamplingDataEx(id, ref aiSamplingTimes, aiData);
        public CaioErrorCode ResetAiStatus(short id) => (CaioErrorCode)NativeAioResetAiStatus(id);
        public CaioErrorCode ResetAiMemory(short id) => (CaioErrorCode)NativeAioResetAiMemory(id);

        public CaioErrorCode SingleAo(short id, short aoChannel, int aoData) => (CaioErrorCode)NativeAioSingleAo(id, aoChannel, aoData);
        public CaioErrorCode SingleAoEx(short id, short aoChannel, float aoData) => (CaioErrorCode)NativeAioSingleAoEx(id, aoChannel, aoData);
        public CaioErrorCode MultiAo(short id, short aoChannels, int[] aoData) => (CaioErrorCode)NativeAioMultiAo(id, aoChannels, aoData);
        public CaioErrorCode MultiAoEx(short id, short aoChannels, float[] aoData) => (CaioErrorCode)NativeAioMultiAoEx(id, aoChannels, aoData);
        public CaioErrorCode GetAoResolution(short id, out short aoResolution) => (CaioErrorCode)NativeAioGetAoResolution(id, out aoResolution);
        public CaioErrorCode SetAoChannels(short id, short aoChannels) => (CaioErrorCode)NativeAioSetAoChannels(id, aoChannels);
        public CaioErrorCode GetAoChannels(short id, out short aoChannels) => (CaioErrorCode)NativeAioGetAoChannels(id, out aoChannels);
        public CaioErrorCode SetAoRange(short id, short aoChannel, short aoRange) => (CaioErrorCode)NativeAioSetAoRange(id, aoChannel, aoRange);
        public CaioErrorCode SetAoRangeAll(short id, short aoRange) => (CaioErrorCode)NativeAioSetAoRangeAll(id, aoRange);
        public CaioErrorCode GetAoRange(short id, short aoChannel, out short aoRange) => (CaioErrorCode)NativeAioGetAoRange(id, aoChannel, out aoRange);
        public CaioErrorCode SetAoSamplingClock(short id, float aoSamplingClock) => (CaioErrorCode)NativeAioSetAoSamplingClock(id, aoSamplingClock);
        public CaioErrorCode GetAoSamplingClock(short id, out float aoSamplingClock) => (CaioErrorCode)NativeAioGetAoSamplingClock(id, out aoSamplingClock);
        public CaioErrorCode StartAo(short id) => (CaioErrorCode)NativeAioStartAo(id);
        public CaioErrorCode StopAo(short id) => (CaioErrorCode)NativeAioStopAo(id);
        public CaioErrorCode GetAoStatus(short id, out int aoStatus) => (CaioErrorCode)NativeAioGetAoStatus(id, out aoStatus);
        public CaioErrorCode ResetAoStatus(short id) => (CaioErrorCode)NativeAioResetAoStatus(id);

        public CaioErrorCode InputDiBit(short id, short diBit, out short diData) => (CaioErrorCode)NativeAioInputDiBit(id, diBit, out diData);
        public CaioErrorCode OutputDoBit(short id, short doBit, short doData) => (CaioErrorCode)NativeAioOutputDoBit(id, doBit, doData);
        public CaioErrorCode InputDiByte(short id, short diPort, out short diData) => (CaioErrorCode)NativeAioInputDiByte(id, diPort, out diData);
        public CaioErrorCode OutputDoByte(short id, short doPort, short doData) => (CaioErrorCode)NativeAioOutputDoByte(id, doPort, doData);
        public CaioErrorCode SetDioDirection(short id, int dir) => (CaioErrorCode)NativeAioSetDioDirection(id, dir);
        public CaioErrorCode GetDioDirection(short id, out int dir) => (CaioErrorCode)NativeAioGetDioDirection(id, out dir);
        #endregion
    }
}