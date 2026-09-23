using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Domain.Interfaces
{
    public interface ICdioDevice
    {
        // 共通関数
        CioDeviceErrorCode Init(string deviceName, out short id);
        CioDeviceErrorCode Exit(short id);
        CioDeviceErrorCode ResetDevice(short id);
        CioDeviceErrorCode GetErrorString(int errorCode, out string errorString);

        // デジタルフィルタ関数
        CioDeviceErrorCode SetDigitalFilter(short id, short filterValue);
        CioDeviceErrorCode GetDigitalFilter(short id, out short filterValue);

        // 入出力方向関数
        CioDeviceErrorCode SetIoDirection(short id, uint dwDir);
        CioDeviceErrorCode GetIoDirection(short id, out uint dwDir);
        CioDeviceErrorCode SetIoDirectionEx(short id, uint dwDir);
        CioDeviceErrorCode GetIoDirectionEx(short id, out uint dwDir);
        CioDeviceErrorCode Set8255Mode(short id, short chipNo, short ctrlWord);
        CioDeviceErrorCode Get8255Mode(short id, short chipNo, out short ctrlWord);

        // 単一入出力関数
        CioDeviceErrorCode InpByte(short id, short portNo, out byte data);
        CioDeviceErrorCode InpBit(short id, short bitNo, out byte data);
        CioDeviceErrorCode OutByte(short id, short portNo, byte data);
        CioDeviceErrorCode OutBit(short id, short bitNo, byte data);
        CioDeviceErrorCode EchoBackByte(short id, short portNo, out byte data);
        CioDeviceErrorCode EchoBackBit(short id, short bitNo, out byte data);

        // 複数入出力関数
        CioDeviceErrorCode InpMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CioDeviceErrorCode InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        CioDeviceErrorCode OutMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CioDeviceErrorCode OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data);
        CioDeviceErrorCode EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data);
        CioDeviceErrorCode EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data);

        // 割り込み・トリガ関数
        CioDeviceErrorCode NotifyInterrupt(short id, short intBit, short logic, int hWnd);
        CioDeviceErrorCode NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd);
        CioDeviceErrorCode StopNotifyTrg(short id, short trgBit);

        // デバイス情報関数
        CioDeviceErrorCode GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3);
        CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device);
        CioDeviceErrorCode GetDeviceType(string device, out short deviceType);
        CioDeviceErrorCode GetMaxPorts(short id, out short inPortNum, out short outPortNum);

        // バスバスマスタ (DM) 関数
        CioDeviceErrorCode DmSetDirection(short id, short direction);
        CioDeviceErrorCode DmGetDirection(short id, out short direction);
        CioDeviceErrorCode DmSetStandAlone(short id);
        CioDeviceErrorCode DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        CioDeviceErrorCode DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);
        CioDeviceErrorCode DmSetStartTrigger(short id, short direction, short start);
        CioDeviceErrorCode DmSetStartPattern(short id, uint pattern, uint mask);
        CioDeviceErrorCode DmSetClockTrigger(short id, short direction, short clock);
        CioDeviceErrorCode DmSetInternalClock(short id, short direction, uint clock, short unit);
        CioDeviceErrorCode DmSetStopTrigger(short id, short direction, short stop);
        CioDeviceErrorCode DmSetStopNumber(short id, short direction, uint stopNumber);
        CioDeviceErrorCode DmFifoReset(short id, short reset);
        CioDeviceErrorCode DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing);
        CioDeviceErrorCode DmSetTransferStartWait(short id, short time);
        CioDeviceErrorCode DmTransferStart(short id, short direction);
        CioDeviceErrorCode DmTransferStop(short id, short direction);
        CioDeviceErrorCode DmGetStatus(short id, short direction, out uint status, out uint err);
        CioDeviceErrorCode DmGetCount(short id, short direction, out uint count, out uint carry);
        CioDeviceErrorCode DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry);
        CioDeviceErrorCode DmSetStopEvent(short id, short direction, int hWnd);
        CioDeviceErrorCode DmSetCountEvent(short id, short direction, uint count, int hWnd);

        // デモ用関数
        CioDeviceErrorCode SetDemoByte(short id, short portNo, byte data);
        CioDeviceErrorCode SetDemoBit(short id, short bitNo, byte data);
    }
}