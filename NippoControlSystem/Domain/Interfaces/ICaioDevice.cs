using NippoControlSystem.Domain.Interfaces.enums;
namespace NippoControlSystem.Domain.Interfaces
{
    public interface ICaioDevice
    {
        // 共通関数
        CioDeviceErrorCode Init(string deviceName, out short id);
        CioDeviceErrorCode Exit(short id);
        CioDeviceErrorCode ResetDevice(short id);
        CioDeviceErrorCode GetErrorString(int errorCode, out string errorString);
        CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device);
        CioDeviceErrorCode GetDeviceType(string device, out short deviceType);
        CioDeviceErrorCode SetControlFilter(short id, short signal, float value);
        CioDeviceErrorCode GetControlFilter(short id, short signal, out float value);
        CioDeviceErrorCode ResetProcess(short id);

        // アナログ入力関数
        CioDeviceErrorCode SingleAi(short id, short aiChannel, out int aiData);
        CioDeviceErrorCode SingleAiEx(short id, short aiChannel, out float aiData);
        CioDeviceErrorCode MultiAi(short id, short aiChannels, int[] aiData);
        CioDeviceErrorCode MultiAiEx(short id, short aiChannels, float[] aiData);
        CioDeviceErrorCode GetAiResolution(short id, out short aiResolution);
        CioDeviceErrorCode SetAiInputMethod(short id, short aiInputMethod);
        CioDeviceErrorCode GetAiInputMethod(short id, out short aiInputMethod);
        CioDeviceErrorCode GetAiMaxChannels(short id, out short aiMaxChannels);
        CioDeviceErrorCode SetAiChannel(short id, short aiChannel, short enabled);
        CioDeviceErrorCode GetAiChannel(short id, short aiChannel, out short enabled);
        CioDeviceErrorCode SetAiChannels(short id, short aiChannels);
        CioDeviceErrorCode GetAiChannels(short id, out short aiChannels);
        CioDeviceErrorCode SetAiRange(short id, short aiChannel, short aiRange);
        CioDeviceErrorCode SetAiRangeAll(short id, short aiRange);
        CioDeviceErrorCode GetAiRange(short id, short aiChannel, out short aiRange);
        CioDeviceErrorCode SetAiSamplingClock(short id, float aiSamplingClock);
        CioDeviceErrorCode GetAiSamplingClock(short id, out float aiSamplingClock);
        CioDeviceErrorCode StartAi(short id);
        CioDeviceErrorCode StartAiSync(short id, int timeOut);
        CioDeviceErrorCode StopAi(short id);
        CioDeviceErrorCode GetAiStatus(short id, out int aiStatus);
        CioDeviceErrorCode GetAiSamplingCount(short id, out int aiSamplingCount);
        CioDeviceErrorCode GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData);
        CioDeviceErrorCode GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData);
        CioDeviceErrorCode ResetAiStatus(short id);
        CioDeviceErrorCode ResetAiMemory(short id);

        // アナログ出力関数
        CioDeviceErrorCode SingleAo(short id, short aoChannel, int aoData);
        CioDeviceErrorCode SingleAoEx(short id, short aoChannel, float aoData);
        CioDeviceErrorCode MultiAo(short id, short aoChannels, int[] aoData);
        CioDeviceErrorCode MultiAoEx(short id, short aoChannels, float[] aoData);
        CioDeviceErrorCode GetAoResolution(short id, out short aoResolution);
        CioDeviceErrorCode SetAoChannels(short id, short aoChannels);
        CioDeviceErrorCode GetAoChannels(short id, out short aoChannels);
        CioDeviceErrorCode SetAoRange(short id, short aoChannel, short aoRange);
        CioDeviceErrorCode SetAoRangeAll(short id, short aoRange);
        CioDeviceErrorCode GetAoRange(short id, short aoChannel, out short aoRange);
        CioDeviceErrorCode SetAoSamplingClock(short id, float aoSamplingClock);
        CioDeviceErrorCode GetAoSamplingClock(short id, out float aoSamplingClock);
        CioDeviceErrorCode StartAo(short id);
        CioDeviceErrorCode StopAo(short id);
        CioDeviceErrorCode GetAoStatus(short id, out int aoStatus);
        CioDeviceErrorCode ResetAoStatus(short id);

        // デジタル入出力関数
        CioDeviceErrorCode InputDiBit(short id, short diBit, out short diData);
        CioDeviceErrorCode OutputDoBit(short id, short doBit, short doData);
        CioDeviceErrorCode InputDiByte(short id, short diPort, out short diData);
        CioDeviceErrorCode OutputDoByte(short id, short doPort, short doData);
        CioDeviceErrorCode SetDioDirection(short id, int dir);
        CioDeviceErrorCode GetDioDirection(short id, out int dir);
    }
}