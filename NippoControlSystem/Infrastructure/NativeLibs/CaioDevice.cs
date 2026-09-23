using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CaioDevice : ICaioDevice
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
        public CioDeviceErrorCode Init(string deviceName, out short id) => (CioDeviceErrorCode)NativeAioInit(deviceName, out id);
        public CioDeviceErrorCode Exit(short id) => (CioDeviceErrorCode)NativeAioExit(id);
        public CioDeviceErrorCode ResetDevice(short id) => (CioDeviceErrorCode)NativeAioResetDevice(id);

        public CioDeviceErrorCode GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeAioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return (CioDeviceErrorCode)ret;
        }

        public CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device)
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

        public CioDeviceErrorCode GetDeviceType(string device, out short deviceType) => (CioDeviceErrorCode)NativeAioGetDeviceType(device, out deviceType);
        public CioDeviceErrorCode SetControlFilter(short id, short signal, float value) => (CioDeviceErrorCode)NativeAioSetControlFilter(id, signal, value);
        public CioDeviceErrorCode GetControlFilter(short id, short signal, out float value) => (CioDeviceErrorCode)NativeAioGetControlFilter(id, signal, out value);
        public CioDeviceErrorCode ResetProcess(short id) => (CioDeviceErrorCode)NativeAioResetProcess(id);

        public CioDeviceErrorCode SingleAi(short id, short aiChannel, out int aiData) => (CioDeviceErrorCode)NativeAioSingleAi(id, aiChannel, out aiData);
        public CioDeviceErrorCode SingleAiEx(short id, short aiChannel, out float aiData) => (CioDeviceErrorCode)NativeAioSingleAiEx(id, aiChannel, out aiData);
        public CioDeviceErrorCode MultiAi(short id, short aiChannels, int[] aiData) => (CioDeviceErrorCode)NativeAioMultiAi(id, aiChannels, aiData);
        public CioDeviceErrorCode MultiAiEx(short id, short aiChannels, float[] aiData) => (CioDeviceErrorCode)NativeAioMultiAiEx(id, aiChannels, aiData);
        public CioDeviceErrorCode GetAiResolution(short id, out short aiResolution) => (CioDeviceErrorCode)NativeAioGetAiResolution(id, out aiResolution);
        public CioDeviceErrorCode SetAiInputMethod(short id, short aiInputMethod) => (CioDeviceErrorCode)NativeAioSetAiInputMethod(id, aiInputMethod);
        public CioDeviceErrorCode GetAiInputMethod(short id, out short aiInputMethod) => (CioDeviceErrorCode)NativeAioGetAiInputMethod(id, out aiInputMethod);
        public CioDeviceErrorCode GetAiMaxChannels(short id, out short aiMaxChannels) => (CioDeviceErrorCode)NativeAioGetAiMaxChannels(id, out aiMaxChannels);
        public CioDeviceErrorCode SetAiChannel(short id, short aiChannel, short enabled) => (CioDeviceErrorCode)NativeAioSetAiChannel(id, aiChannel, enabled);
        public CioDeviceErrorCode GetAiChannel(short id, short aiChannel, out short enabled) => (CioDeviceErrorCode)NativeAioGetAiChannel(id, aiChannel, out enabled);
        public CioDeviceErrorCode SetAiChannels(short id, short aiChannels) => (CioDeviceErrorCode)NativeAioSetAiChannels(id, aiChannels);
        public CioDeviceErrorCode GetAiChannels(short id, out short aiChannels) => (CioDeviceErrorCode)NativeAioGetAiChannels(id, out aiChannels);
        public CioDeviceErrorCode SetAiRange(short id, short aiChannel, short aiRange) => (CioDeviceErrorCode)NativeAioSetAiRange(id, aiChannel, aiRange);
        public CioDeviceErrorCode SetAiRangeAll(short id, short aiRange) => (CioDeviceErrorCode)NativeAioSetAiRangeAll(id, aiRange);
        public CioDeviceErrorCode GetAiRange(short id, short aiChannel, out short aiRange) => (CioDeviceErrorCode)NativeAioGetAiRange(id, aiChannel, out aiRange);
        public CioDeviceErrorCode SetAiSamplingClock(short id, float aiSamplingClock) => (CioDeviceErrorCode)NativeAioSetAiSamplingClock(id, aiSamplingClock);
        public CioDeviceErrorCode GetAiSamplingClock(short id, out float aiSamplingClock) => (CioDeviceErrorCode)NativeAioGetAiSamplingClock(id, out aiSamplingClock);
        public CioDeviceErrorCode StartAi(short id) => (CioDeviceErrorCode)NativeAioStartAi(id);
        public CioDeviceErrorCode StartAiSync(short id, int timeOut) => (CioDeviceErrorCode)NativeAioStartAiSync(id, timeOut);
        public CioDeviceErrorCode StopAi(short id) => (CioDeviceErrorCode)NativeAioStopAi(id);
        public CioDeviceErrorCode GetAiStatus(short id, out int aiStatus) => (CioDeviceErrorCode)NativeAioGetAiStatus(id, out aiStatus);
        public CioDeviceErrorCode GetAiSamplingCount(short id, out int aiSamplingCount) => (CioDeviceErrorCode)NativeAioGetAiSamplingCount(id, out aiSamplingCount);
        public CioDeviceErrorCode GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData) => (CioDeviceErrorCode)NativeAioGetAiSamplingData(id, ref aiSamplingTimes, aiData);
        public CioDeviceErrorCode GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData) => (CioDeviceErrorCode)NativeAioGetAiSamplingDataEx(id, ref aiSamplingTimes, aiData);
        public CioDeviceErrorCode ResetAiStatus(short id) => (CioDeviceErrorCode)NativeAioResetAiStatus(id);
        public CioDeviceErrorCode ResetAiMemory(short id) => (CioDeviceErrorCode)NativeAioResetAiMemory(id);

        public CioDeviceErrorCode SingleAo(short id, short aoChannel, int aoData) => (CioDeviceErrorCode)NativeAioSingleAo(id, aoChannel, aoData);
        public CioDeviceErrorCode SingleAoEx(short id, short aoChannel, float aoData) => (CioDeviceErrorCode)NativeAioSingleAoEx(id, aoChannel, aoData);
        public CioDeviceErrorCode MultiAo(short id, short aoChannels, int[] aoData) => (CioDeviceErrorCode)NativeAioMultiAo(id, aoChannels, aoData);
        public CioDeviceErrorCode MultiAoEx(short id, short aoChannels, float[] aoData) => (CioDeviceErrorCode)NativeAioMultiAoEx(id, aoChannels, aoData);
        public CioDeviceErrorCode GetAoResolution(short id, out short aoResolution) => (CioDeviceErrorCode)NativeAioGetAoResolution(id, out aoResolution);
        public CioDeviceErrorCode SetAoChannels(short id, short aoChannels) => (CioDeviceErrorCode)NativeAioSetAoChannels(id, aoChannels);
        public CioDeviceErrorCode GetAoChannels(short id, out short aoChannels) => (CioDeviceErrorCode)NativeAioGetAoChannels(id, out aoChannels);
        public CioDeviceErrorCode SetAoRange(short id, short aoChannel, short aoRange) => (CioDeviceErrorCode)NativeAioSetAoRange(id, aoChannel, aoRange);
        public CioDeviceErrorCode SetAoRangeAll(short id, short aoRange) => (CioDeviceErrorCode)NativeAioSetAoRangeAll(id, aoRange);
        public CioDeviceErrorCode GetAoRange(short id, short aoChannel, out short aoRange) => (CioDeviceErrorCode)NativeAioGetAoRange(id, aoChannel, out aoRange);
        public CioDeviceErrorCode SetAoSamplingClock(short id, float aoSamplingClock) => (CioDeviceErrorCode)NativeAioSetAoSamplingClock(id, aoSamplingClock);
        public CioDeviceErrorCode GetAoSamplingClock(short id, out float aoSamplingClock) => (CioDeviceErrorCode)NativeAioGetAoSamplingClock(id, out aoSamplingClock);
        public CioDeviceErrorCode StartAo(short id) => (CioDeviceErrorCode)NativeAioStartAo(id);
        public CioDeviceErrorCode StopAo(short id) => (CioDeviceErrorCode)NativeAioStopAo(id);
        public CioDeviceErrorCode GetAoStatus(short id, out int aoStatus) => (CioDeviceErrorCode)NativeAioGetAoStatus(id, out aoStatus);
        public CioDeviceErrorCode ResetAoStatus(short id) => (CioDeviceErrorCode)NativeAioResetAoStatus(id);

        public CioDeviceErrorCode InputDiBit(short id, short diBit, out short diData) => (CioDeviceErrorCode)NativeAioInputDiBit(id, diBit, out diData);
        public CioDeviceErrorCode OutputDoBit(short id, short doBit, short doData) => (CioDeviceErrorCode)NativeAioOutputDoBit(id, doBit, doData);
        public CioDeviceErrorCode InputDiByte(short id, short diPort, out short diData) => (CioDeviceErrorCode)NativeAioInputDiByte(id, diPort, out diData);
        public CioDeviceErrorCode OutputDoByte(short id, short doPort, short doData) => (CioDeviceErrorCode)NativeAioOutputDoByte(id, doPort, doData);
        public CioDeviceErrorCode SetDioDirection(short id, int dir) => (CioDeviceErrorCode)NativeAioSetDioDirection(id, dir);
        public CioDeviceErrorCode GetDioDirection(short id, out int dir) => (CioDeviceErrorCode)NativeAioGetDioDirection(id, out dir);
        #endregion
    }
}