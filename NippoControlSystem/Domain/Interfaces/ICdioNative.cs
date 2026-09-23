// CONTEC社 デジタル入出力 DIO用DLLラッパー

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public interface ICdioNative
    {
        // 共通関数
        int Init(string deviceName, out short id);
        int Exit(short id);
        int ResetDevice(short id);
        int GetErrorString(int errorCode, out string errorString);

        // デジタルフィルタ関数
        int SetDigitalFilter(short id, short filterValue);
        int GetDigitalFilter(short id, out short filterValue);

        // 入出力方向関数
        int SetIoDirection(short id, uint dwDir);
        int GetIoDirection(short id, out uint dwDir);
        int SetIoDirectionEx(short id, uint dwDir);
        int GetIoDirectionEx(short id, out uint dwDir);
        int Set8255Mode(short id, short chipNo, short ctrlWord);
        int Get8255Mode(short id, short chipNo, out short ctrlWord);

        // 単一入出力関数
        int InpByte(short id, short portNo, out byte data);
        int InpBit(short id, short bitNo, out byte data);
        int OutByte(short id, short portNo, byte data);
        int OutBit(short id, short bitNo, byte data);
        int EchoBackByte(short id, short portNo, out byte data);
        int EchoBackBit(short id, short bitNo, out byte data);

        // 複数入出力関数
        int InpMultiByte(short id, short[] portNo, short portNum, byte[] data);
        int InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        int OutMultiByte(short id, short[] portNo, short portNum, byte[] data);
        int OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        int EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data);
        int EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data);

        // 割り込み・トリガ関数
        int NotifyInterrupt(short id, short intBit, short logic, int hWnd);
        int NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd);
        int StopNotifyTrg(short id, short trgBit);

        // デバイス情報関数
        int GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3);
        int QueryDeviceName(short index, out string deviceName, out string device);
        int GetDeviceType(string device, out short deviceType);
        int GetMaxPorts(short id, out short inPortNum, out short outPortNum);

        // バスバスマスタ (DM) 関数
        int DmSetDirection(short id, short direction);
        int DmGetDirection(short id, out short direction);
        int DmSetStandAlone(short id);
        int DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        int DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        int DmSetStartTrigger(short id, short direction, short start);
        int DmSetStartPattern(short id, uint pattern, uint mask);
        int DmSetClockTrigger(short id, short direction, short clock);
        int DmSetInternalClock(short id, short direction, uint clock, short unit);
        int DmSetStopTrigger(short id, short direction, short stop);
        int DmSetStopNumber(short id, short direction, uint stopNumber);
        int DmFifoReset(short id, short reset);
        int DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing);
        int DmSetTransferStartWait(short id, short time);
        int DmTransferStart(short id, short direction);
        int DmTransferStop(short id, short direction);
        int DmGetStatus(short id, short direction, out uint status, out uint err);
        int DmGetCount(short id, short direction, out uint count, out uint carry);
        int DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry);
        int DmSetStopEvent(short id, short direction, int hWnd);
        int DmSetCountEvent(short id, short direction, uint count, int hWnd);

        // デモ用関数
        int SetDemoByte(short id, short portNo, byte data);
        int SetDemoBit(short id, short bitNo, byte data);
    }
}