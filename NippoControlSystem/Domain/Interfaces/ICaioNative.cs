namespace NippoControlSystem.Domain.Interfaces
{
    public interface ICaioNative
    {
        // 共通関数
        CaioErrorCode Init(string deviceName, out short id);
        CaioErrorCode Exit(short id);
        CaioErrorCode ResetDevice(short id);
        CaioErrorCode GetErrorString(int errorCode, out string errorString);
        CaioErrorCode QueryDeviceName(short index, out string deviceName, out string device);
        CaioErrorCode GetDeviceType(string device, out short deviceType);
        CaioErrorCode SetControlFilter(short id, short signal, float value);
        CaioErrorCode GetControlFilter(short id, short signal, out float value);
        CaioErrorCode ResetProcess(short id);

        // アナログ入力関数
        CaioErrorCode SingleAi(short id, short aiChannel, out int aiData);
        CaioErrorCode SingleAiEx(short id, short aiChannel, out float aiData);
        CaioErrorCode MultiAi(short id, short aiChannels, int[] aiData);
        CaioErrorCode MultiAiEx(short id, short aiChannels, float[] aiData);
        CaioErrorCode GetAiResolution(short id, out short aiResolution);
        CaioErrorCode SetAiInputMethod(short id, short aiInputMethod);
        CaioErrorCode GetAiInputMethod(short id, out short aiInputMethod);
        CaioErrorCode GetAiMaxChannels(short id, out short aiMaxChannels);
        CaioErrorCode SetAiChannel(short id, short aiChannel, short enabled);
        CaioErrorCode GetAiChannel(short id, short aiChannel, out short enabled);
        CaioErrorCode SetAiChannels(short id, short aiChannels);
        CaioErrorCode GetAiChannels(short id, out short aiChannels);
        CaioErrorCode SetAiRange(short id, short aiChannel, short aiRange);
        CaioErrorCode SetAiRangeAll(short id, short aiRange);
        CaioErrorCode GetAiRange(short id, short aiChannel, out short aiRange);
        CaioErrorCode SetAiSamplingClock(short id, float aiSamplingClock);
        CaioErrorCode GetAiSamplingClock(short id, out float aiSamplingClock);
        CaioErrorCode StartAi(short id);
        CaioErrorCode StartAiSync(short id, int timeOut);
        CaioErrorCode StopAi(short id);
        CaioErrorCode GetAiStatus(short id, out int aiStatus);
        CaioErrorCode GetAiSamplingCount(short id, out int aiSamplingCount);
        CaioErrorCode GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData);
        CaioErrorCode GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData);
        CaioErrorCode ResetAiStatus(short id);
        CaioErrorCode ResetAiMemory(short id);

        // アナログ出力関数
        CaioErrorCode SingleAo(short id, short aoChannel, int aoData);
        CaioErrorCode SingleAoEx(short id, short aoChannel, float aoData);
        CaioErrorCode MultiAo(short id, short aoChannels, int[] aoData);
        CaioErrorCode MultiAoEx(short id, short aoChannels, float[] aoData);
        CaioErrorCode GetAoResolution(short id, out short aoResolution);
        CaioErrorCode SetAoChannels(short id, short aoChannels);
        CaioErrorCode GetAoChannels(short id, out short aoChannels);
        CaioErrorCode SetAoRange(short id, short aoChannel, short aoRange);
        CaioErrorCode SetAoRangeAll(short id, short aoRange);
        CaioErrorCode GetAoRange(short id, short aoChannel, out short aoRange);
        CaioErrorCode SetAoSamplingClock(short id, float aoSamplingClock);
        CaioErrorCode GetAoSamplingClock(short id, out float aoSamplingClock);
        CaioErrorCode StartAo(short id);
        CaioErrorCode StopAo(short id);
        CaioErrorCode GetAoStatus(short id, out int aoStatus);
        CaioErrorCode ResetAoStatus(short id);

        // デジタル入出力関数
        CaioErrorCode InputDiBit(short id, short diBit, out short diData);
        CaioErrorCode OutputDoBit(short id, short doBit, short doData);
        CaioErrorCode InputDiByte(short id, short diPort, out short diData);
        CaioErrorCode OutputDoByte(short id, short doPort, short doData);
        CaioErrorCode SetDioDirection(short id, int dir);
        CaioErrorCode GetDioDirection(short id, out int dir);
    }
}