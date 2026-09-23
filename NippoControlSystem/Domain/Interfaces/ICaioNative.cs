
// CONTEC社等のAIO / CAIO用DLLラッパー

namespace NippoControlSystem.Domain.Interfaces
{
    public interface ICaioNative
    {
        // 共通関数
        int Init(string deviceName, out short id);
        int Exit(short id);
        int ResetDevice(short id);
        int GetErrorString(int errorCode, out string errorString);
        int QueryDeviceName(short index, out string deviceName, out string device);
        int GetDeviceType(string device, out short deviceType);
        int SetControlFilter(short id, short signal, float value);
        int GetControlFilter(short id, short signal, out float value);
        int ResetProcess(short id);

        // アナログ入力関数
        int SingleAi(short id, short aiChannel, out int aiData);
        int SingleAiEx(short id, short aiChannel, out float aiData);
        int MultiAi(short id, short aiChannels, int[] aiData);
        int MultiAiEx(short id, short aiChannels, float[] aiData);
        int GetAiResolution(short id, out short aiResolution);
        int SetAiInputMethod(short id, short aiInputMethod);
        int GetAiInputMethod(short id, out short aiInputMethod);
        int GetAiMaxChannels(short id, out short aiMaxChannels);
        int SetAiChannel(short id, short aiChannel, short enabled);
        int GetAiChannel(short id, short aiChannel, out short enabled);
        int SetAiChannels(short id, short aiChannels);
        int GetAiChannels(short id, out short aiChannels);
        int SetAiRange(short id, short aiChannel, short aiRange);
        int SetAiRangeAll(short id, short aiRange);
        int GetAiRange(short id, short aiChannel, out short aiRange);
        int SetAiSamplingClock(short id, float aiSamplingClock);
        int GetAiSamplingClock(short id, out float aiSamplingClock);
        int StartAi(short id);
        int StartAiSync(short id, int timeOut);
        int StopAi(short id);
        int GetAiStatus(short id, out int aiStatus);
        int GetAiSamplingCount(short id, out int aiSamplingCount);
        int GetAiSamplingData(short id, ref int aiSamplingTimes, int[] aiData);
        int GetAiSamplingDataEx(short id, ref int aiSamplingTimes, float[] aiData);
        int ResetAiStatus(short id);
        int ResetAiMemory(short id);

        // アナログ出力関数
        int SingleAo(short id, short aoChannel, int aoData);
        int SingleAoEx(short id, short aoChannel, float aoData);
        int MultiAo(short id, short aoChannels, int[] aoData);
        int MultiAoEx(short id, short aoChannels, float[] aoData);
        int GetAoResolution(short id, out short aoResolution);
        int SetAoChannels(short id, short aoChannels);
        int GetAoChannels(short id, out short aoChannels);
        int SetAoRange(short id, short aoChannel, short aoRange);
        int SetAoRangeAll(short id, short aoRange);
        int GetAoRange(short id, short aoChannel, out short aoRange);
        int SetAoSamplingClock(short id, float aoSamplingClock);
        int GetAoSamplingClock(short id, out float aoSamplingClock);
        int StartAo(short id);
        int StopAo(short id);
        int GetAoStatus(short id, out int aoStatus);
        int ResetAoStatus(short id);

        // デジタル入出力関数
        int InputDiBit(short id, short diBit, out short diData);
        int OutputDoBit(short id, short doBit, short doData);
        int InputDiByte(short id, short diPort, out short diData);
        int OutputDoByte(short id, short doPort, short doData);
        int SetDioDirection(short id, int dir);
        int GetDioDirection(short id, out int dir);
    }
}