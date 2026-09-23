namespace NippoControlSystem.Domain.Interfaces
{
    public interface ICdioNative
    {
        // 共通関数
        CdioErrorCode Init(string deviceName, out short id);
        CdioErrorCode Exit(short id);
        CdioErrorCode ResetDevice(short id);
        CdioErrorCode GetErrorString(int errorCode, out string errorString);

        // デジタルフィルタ関数
        CdioErrorCode SetDigitalFilter(short id, short filterValue);
        CdioErrorCode GetDigitalFilter(short id, out short filterValue);

        // 入出力方向関数
        CdioErrorCode SetIoDirection(short id, uint dwDir);
        CdioErrorCode GetIoDirection(short id, out uint dwDir);
        CdioErrorCode SetIoDirectionEx(short id, uint dwDir);
        CdioErrorCode GetIoDirectionEx(short id, out uint dwDir);
        CdioErrorCode Set8255Mode(short id, short chipNo, short ctrlWord);
        CdioErrorCode Get8255Mode(short id, short chipNo, out short ctrlWord);

        // 単一入出力関数
        CdioErrorCode InpByte(short id, short portNo, out byte data);
        CdioErrorCode InpBit(short id, short bitNo, out byte data);
        CdioErrorCode OutByte(short id, short portNo, byte data);
        CdioErrorCode OutBit(short id, short bitNo, byte data);
        CdioErrorCode EchoBackByte(short id, short portNo, out byte data);
        CdioErrorCode EchoBackBit(short id, short bitNo, out byte data);

        // 複数入出力関数
        CdioErrorCode InpMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CdioErrorCode InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        CdioErrorCode OutMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CdioErrorCode OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        CdioErrorCode EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CdioErrorCode EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data);

        // 割り込み・トリガ関数
        CdioErrorCode NotifyInterrupt(short id, short intBit, short logic, int hWnd);
        CdioErrorCode NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd);
        CdioErrorCode StopNotifyTrg(short id, short trgBit);

        // デバイス情報関数
        CdioErrorCode GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3);
        CdioErrorCode QueryDeviceName(short index, out string deviceName, out string device);
        CdioErrorCode GetDeviceType(string device, out short deviceType);
        CdioErrorCode GetMaxPorts(short id, out short inPortNum, out short outPortNum);

        // バスバスマスタ (DM) 関数
        CdioErrorCode DmSetDirection(short id, short direction);
        CdioErrorCode DmGetDirection(short id, out short direction);
        CdioErrorCode DmSetStandAlone(short id);
        CdioErrorCode DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        CdioErrorCode DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        CdioErrorCode DmSetStartTrigger(short id, short direction, short start);
        CdioErrorCode DmSetStartPattern(short id, uint pattern, uint mask);
        CdioErrorCode DmSetClockTrigger(short id, short direction, short clock);
        CdioErrorCode DmSetInternalClock(short id, short direction, uint clock, short unit);
        CdioErrorCode DmSetStopTrigger(short id, short direction, short stop);
        CdioErrorCode DmSetStopNumber(short id, short direction, uint stopNumber);
        CdioErrorCode DmFifoReset(short id, short reset);
        CdioErrorCode DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing);
        CdioErrorCode DmSetTransferStartWait(short id, short time);
        CdioErrorCode DmTransferStart(short id, short direction);
        CdioErrorCode DmTransferStop(short id, short direction);
        CdioErrorCode DmGetStatus(short id, short direction, out uint status, out uint err);
        CdioErrorCode DmGetCount(short id, short direction, out uint count, out uint carry);
        CdioErrorCode DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry);
        CdioErrorCode DmSetStopEvent(short id, short direction, int hWnd);
        CdioErrorCode DmSetCountEvent(short id, short direction, uint count, int hWnd);

        // デモ用関数
        CdioErrorCode SetDemoByte(short id, short portNo, byte data);
        CdioErrorCode SetDemoBit(short id, short bitNo, byte data);
    }
}