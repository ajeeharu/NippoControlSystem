using NippoControlSystem.Domain.Interfaces;
using System.Runtime.InteropServices;
using System.Text;

// CONTEC社等のAIO / CAIO用DLLラッパー

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CaioNative : ICaioNative
    {
        private const string LibraryName = "caio.dll";

        #region Native Imports (LibraryImport)

        // 共通関数
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

        // アナログ入力関数
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

        // アナログ出力関数
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

        // デジタル入出力関数
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

        #region Public Wrapper Methods (ICaioNative Implementation)

        public int Init(string deviceName, out short id) => NativeAioInit(deviceName, out id);
        public int Exit(short id) => NativeAioExit(id);
        public int ResetDevice(short id) => NativeAioResetDevice(id);

        public int GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeAioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return ret;
        }

        public int QueryDeviceName(short index, out string deviceName, out string device)
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
            return ret;
        }

        public int GetDeviceType(string device, out short deviceType) => NativeAioGetDeviceType(device, out deviceType);
        public int SetControlFilter(short id, short signal, float value) => NativeAioSetControlFilter(id, signal, value);
        public int GetControlFilter(short id, short signal, out float value) => NativeAioGetControlFilter(id, signal, out value);
        public int ResetProcess(short id) => NativeAioResetProcess(id);

        public int SingleAi(short id, short aiChannel, out int aiData) => NativeAioSingleAi(id, aiChannel, out aiData);
        public int SingleAiEx(short id, short aiChannel, out float aiData) => NativeAioSingleAiEx(id, aiChannel, out aiData);
        public int MultiAi(short id, short aiChannels, int[] aiData) => NativeAioMultiAi(id, aiChannels, aiData);
        public int MultiAiEx(short id, short aiChannels, float[] aiData) => NativeAioMultiAiEx(id, aiChannels, aiData);
        public int GetAiResolution(short id, out short aiResolution) => NativeAioGetAiResolution(id, out aiResolution);
        public int SetAiInputMethod(short id, short aiInputMethod) => NativeAioSetAiInputMethod(id, aiInputMethod);
        public int GetAiInputMethod(short id, out short aiInputMethod) => NativeAioGetAiInputMethod(id, out aiInputMethod);
        public int GetAiMaxChannels(short id, out short aiMaxChannels) => NativeAioGetAiMaxChannels(id, out aiMaxChannels);
        public int SetAiChannel(short id, short aiChannel, short enabled) => NativeAioSetAiChannel(id, aiChannel, enabled);
        public int GetAiChannel(short id, short aiChannel, out short enabled) => NativeAioGetAiChannel(id, aiChannel, out enabled);
        public int SetAiChannels(short id, short aiChannels) => NativeAioSetAiChannels(id, aiChannels);
        public int GetAiChannels(short id, out short aiChannels) => NativeAioGetAiChannels(id, out aiChannels);
        public int SetAiRange(short id, short aiChannel, short aiRange) => NativeAioSetAiRange(id, aiChannel, aiRange);
        public int SetAiRangeAll(short id, short aiRange) => NativeAioSetAiRangeAll(id, aiRange);
        public int GetAiRange(short id, short aiChannel, out short aiRange) => NativeAioGetAiRange(id, aiChannel, out aiRange);
        public int SetAiSamplingClock(short id, float aiSamplingClock) => NativeAioSetAiSamplingClock(id, aiSamplingClock);
        public int GetAiSamplingClock(short id, out float aiSamplingClock) => NativeAioGetAiSamplingClock(id, out aiSamplingClock);
        public int StartAi(short id) => NativeAioStartAi(id);
        public int StartAiSync(short id, int timeOut) => NativeAioStartAiSync(id, timeOut);
        public int StopAi(short id) => NativeAioStopAi(id);
        public int GetAiStatus(short id, out int aiStatus) => NativeAioGetAiStatus(id, out aiStatus);
        public int GetAiSamplingCount(short id, out int aiSamplingCount) => NativeAioGetAiSamplingCount(id, out aiSamplingCount);
        public int GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData) => NativeAioGetAiSamplingData(id, ref aiSamplingTimes, aiData);
        public int GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData) => NativeAioGetAiSamplingDataEx(id, ref aiSamplingTimes, aiData);
        public int ResetAiStatus(short id) => NativeAioResetAiStatus(id);
        public int ResetAiMemory(short id) => NativeAioResetAiMemory(id);

        public int SingleAo(short id, short aoChannel, int aoData) => NativeAioSingleAo(id, aoChannel, aoData);
        public int SingleAoEx(short id, short aoChannel, float aoData) => NativeAioSingleAoEx(id, aoChannel, aoData);
        public int MultiAo(short id, short aoChannels, int[] aoData) => NativeAioMultiAo(id, aoChannels, aoData);
        public int MultiAoEx(short id, short aoChannels, float[] aoData) => NativeAioMultiAoEx(id, aoChannels, aoData);
        public int GetAoResolution(short id, out short aoResolution) => NativeAioGetAoResolution(id, out aoResolution);
        public int SetAoChannels(short id, short aoChannels) => NativeAioSetAoChannels(id, aoChannels);
        public int GetAoChannels(short id, out short aoChannels) => NativeAioGetAoChannels(id, out aoChannels);
        public int SetAoRange(short id, short aoChannel, short aoRange) => NativeAioSetAoRange(id, aoChannel, aoRange);
        public int SetAoRangeAll(short id, short aoRange) => NativeAioSetAoRangeAll(id, aoRange);
        public int GetAoRange(short id, short aoChannel, out short aoRange) => NativeAioGetAoRange(id, aoChannel, out aoRange);
        public int SetAoSamplingClock(short id, float aoSamplingClock) => NativeAioSetAoSamplingClock(id, aoSamplingClock);
        public int GetAoSamplingClock(short id, out float aoSamplingClock) => NativeAioGetAoSamplingClock(id, out aoSamplingClock);
        public int StartAo(short id) => NativeAioStartAo(id);
        public int StopAo(short id) => NativeAioStopAo(id);
        public int GetAoStatus(short id, out int aoStatus) => NativeAioGetAoStatus(id, out aoStatus);
        public int ResetAoStatus(short id) => NativeAioResetAoStatus(id);

        public int InputDiBit(short id, short diBit, out short diData) => NativeAioInputDiBit(id, diBit, out diData);
        public int OutputDoBit(short id, short doBit, short doData) => NativeAioOutputDoBit(id, doBit, doData);
        public int InputDiByte(short id, short diPort, out short diData) => NativeAioInputDiByte(id, diPort, out diData);
        public int OutputDoByte(short id, short doPort, short doData) => NativeAioOutputDoByte(id, doPort, doData);
        public int SetDioDirection(short id, int dir) => NativeAioSetDioDirection(id, dir);
        public int GetDioDirection(short id, out int dir) => NativeAioGetDioDirection(id, out dir);

        #endregion
    }
}