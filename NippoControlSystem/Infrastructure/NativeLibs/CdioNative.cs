using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CdioNative : ICdioNative
    {
        private const string LibraryName = "cdio.dll";

        #region Native Imports (LibraryImport)

        // 共通関数
        [LibraryImport(LibraryName, EntryPoint = "DioInit", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioInit(string deviceName, out short id);

        [LibraryImport(LibraryName, EntryPoint = "DioExit")]
        private static partial int NativeDioExit(short id);

        [LibraryImport(LibraryName, EntryPoint = "DioResetDevice")]
        private static partial int NativeDioResetDevice(short id);

        [LibraryImport(LibraryName, EntryPoint = "DioGetErrorString")]
        private static partial int NativeDioGetErrorString(int errorCode, [Out] byte[] errorString);

        // デジタルフィルタ関数
        [LibraryImport(LibraryName, EntryPoint = "DioSetDigitalFilter")]
        private static partial int NativeDioSetDigitalFilter(short id, short filterValue);

        [LibraryImport(LibraryName, EntryPoint = "DioGetDigitalFilter")]
        private static partial int NativeDioGetDigitalFilter(short id, out short filterValue);

        // 入出力方向関数
        [LibraryImport(LibraryName, EntryPoint = "DioSetIoDirection")]
        private static partial int NativeDioSetIoDirection(short id, uint dwDir);

        [LibraryImport(LibraryName, EntryPoint = "DioGetIoDirection")]
        private static partial int NativeDioGetIoDirection(short id, out uint dwDir);

        [LibraryImport(LibraryName, EntryPoint = "DioSetIoDirectionEx")]
        private static partial int NativeDioSetIoDirectionEx(short id, uint dwDir);

        [LibraryImport(LibraryName, EntryPoint = "DioGetIoDirectionEx")]
        private static partial int NativeDioGetIoDirectionEx(short id, out uint dwDir);

        [LibraryImport(LibraryName, EntryPoint = "DioSet8255Mode")]
        private static partial int NativeDioSet8255Mode(short id, short chipNo, short ctrlWord);

        [LibraryImport(LibraryName, EntryPoint = "DioGet8255Mode")]
        private static partial int NativeDioGet8255Mode(short id, short chipNo, out short ctrlWord);

        // 単一入出力関数
        [LibraryImport(LibraryName, EntryPoint = "DioInpByte")]
        private static partial int NativeDioInpByte(short id, short portNo, out byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioInpBit")]
        private static partial int NativeDioInpBit(short id, short bitNo, out byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioOutByte")]
        private static partial int NativeDioOutByte(short id, short portNo, byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioOutBit")]
        private static partial int NativeDioOutBit(short id, short bitNo, byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioEchoBackByte")]
        private static partial int NativeDioEchoBackByte(short id, short portNo, out byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioEchoBackBit")]
        private static partial int NativeDioEchoBackBit(short id, short bitNo, out byte data);

        // 複数入出力関数
        [LibraryImport(LibraryName, EntryPoint = "DioInpMultiByte")]
        private static partial int NativeDioInpMultiByte(short id, [In] short[] portNo, short portNum, [Out] byte[] data);

        [LibraryImport(LibraryName, EntryPoint = "DioInpMultiBit")]
        private static partial int NativeDioInpMultiBit(short id, [In] short[] bitNo, short bitNum, [Out] byte[] data);

        [LibraryImport(LibraryName, EntryPoint = "DioOutMultiByte")]
        private static partial int NativeDioOutMultiByte(short id, [In] short[] portNo, short portNum, [In] byte[] data);

        [LibraryImport(LibraryName, EntryPoint = "DioOutMultiBit")]
        private static partial int NativeDioOutMultiBit(short id, [In] short[] bitNo, short bitNum, [In] byte[] data);

        [LibraryImport(LibraryName, EntryPoint = "DioEchoBackMultiByte")]
        private static partial int NativeDioEchoBackMultiByte(short id, [In] short[] portNo, short portNum, [Out] byte[] data);

        [LibraryImport(LibraryName, EntryPoint = "DioEchoBackMultiBit")]
        private static partial int NativeDioEchoBackMultiBit(short id, [In] short[] bitNo, short bitNum, [Out] byte[] data);

        // 割り込み・トリガ関数
        [LibraryImport(LibraryName, EntryPoint = "DioNotifyInterrupt")]
        private static partial int NativeDioNotifyInterrupt(short id, short intBit, short logic, int hWnd);

        [LibraryImport(LibraryName, EntryPoint = "DioNotifyTrg")]
        private static partial int NativeDioNotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd);

        [LibraryImport(LibraryName, EntryPoint = "DioStopNotifyTrg")]
        private static partial int NativeDioStopNotifyTrg(short id, short trgBit);

        // デバイス情報関数
        [LibraryImport(LibraryName, EntryPoint = "DioGetDeviceInfo", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioGetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3);

        [LibraryImport(LibraryName, EntryPoint = "DioQueryDeviceName")]
        private static partial int NativeDioQueryDeviceName(short index, [Out] byte[] deviceName, [Out] byte[] device);

        [LibraryImport(LibraryName, EntryPoint = "DioGetDeviceType", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioGetDeviceType(string device, out short deviceType);

        [LibraryImport(LibraryName, EntryPoint = "DioGetMaxPorts")]
        private static partial int NativeDioGetMaxPorts(short id, out short inPortNum, out short outPortNum);

        // バスバスマスタ (DM) 関数
        [LibraryImport(LibraryName, EntryPoint = "DioDmSetDirection")]
        private static partial int NativeDioDmSetDirection(short id, short direction);

        [LibraryImport(LibraryName, EntryPoint = "DioDmGetDirection")]
        private static partial int NativeDioDmGetDirection(short id, out short direction);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStandAlone")]
        private static partial int NativeDioDmSetStandAlone(short id);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetMaster")]
        private static partial int NativeDioDmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetSlave")]
        private static partial int NativeDioDmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStartTrigger")]
        private static partial int NativeDioDmSetStartTrigger(short id, short direction, short start);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStartPattern")]
        private static partial int NativeDioDmSetStartPattern(short id, uint pattern, uint mask);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetClockTrigger")]
        private static partial int NativeDioDmSetClockTrigger(short id, short direction, short clock);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetInternalClock")]
        private static partial int NativeDioDmSetInternalClock(short id, short direction, uint clock, short unit);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStopTrigger")]
        private static partial int NativeDioDmSetStopTrigger(short id, short direction, short stop);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStopNumber")]
        private static partial int NativeDioDmSetStopNumber(short id, short direction, uint stopNumber);

        [LibraryImport(LibraryName, EntryPoint = "DioDmFifoReset")]
        private static partial int NativeDioDmFifoReset(short id, short reset);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetBuffer")]
        private static partial int NativeDioDmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetTransferStartWait")]
        private static partial int NativeDioDmSetTransferStartWait(short id, short time);

        [LibraryImport(LibraryName, EntryPoint = "DioDmTransferStart")]
        private static partial int NativeDioDmTransferStart(short id, short direction);

        [LibraryImport(LibraryName, EntryPoint = "DioDmTransferStop")]
        private static partial int NativeDioDmTransferStop(short id, short direction);

        [LibraryImport(LibraryName, EntryPoint = "DioDmGetStatus")]
        private static partial int NativeDioDmGetStatus(short id, short direction, out uint status, out uint err);

        [LibraryImport(LibraryName, EntryPoint = "DioDmGetCount")]
        private static partial int NativeDioDmGetCount(short id, short direction, out uint count, out uint carry);

        [LibraryImport(LibraryName, EntryPoint = "DioDmGetWritePointer")]
        private static partial int NativeDioDmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetStopEvent")]
        private static partial int NativeDioDmSetStopEvent(short id, short direction, int hWnd);

        [LibraryImport(LibraryName, EntryPoint = "DioDmSetCountEvent")]
        private static partial int NativeDioDmSetCountEvent(short id, short direction, uint count, int hWnd);

        // デモ用関数
        [LibraryImport(LibraryName, EntryPoint = "DioSetDemoByte")]
        private static partial int NativeDioSetDemoByte(short id, short portNo, byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioSetDemoBit")]
        private static partial int NativeDioSetDemoBit(short id, short bitNo, byte data);

        #endregion

        #region Public Wrapper Methods (ICdioNative Implementation)

        public int Init(string deviceName, out short id) => NativeDioInit(deviceName, out id);
        public int Exit(short id) => NativeDioExit(id);
        public int ResetDevice(short id) => NativeDioResetDevice(id);

        public int GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeDioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return ret;
        }

        public int SetDigitalFilter(short id, short filterValue) => NativeDioSetDigitalFilter(id, filterValue);
        public int GetDigitalFilter(short id, out short filterValue) => NativeDioGetDigitalFilter(id, out filterValue);

        public int SetIoDirection(short id, uint dwDir) => NativeDioSetIoDirection(id, dwDir);
        public int GetIoDirection(short id, out uint dwDir) => NativeDioGetIoDirection(id, out dwDir);
        public int SetIoDirectionEx(short id, uint dwDir) => NativeDioSetIoDirectionEx(id, dwDir);
        public int GetIoDirectionEx(short id, out uint dwDir) => NativeDioGetIoDirectionEx(id, out dwDir);
        public int Set8255Mode(short id, short chipNo, short ctrlWord) => NativeDioSet8255Mode(id, chipNo, ctrlWord);
        public int Get8255Mode(short id, short chipNo, out short ctrlWord) => NativeDioGet8255Mode(id, chipNo, out ctrlWord);

        public int InpByte(short id, short portNo, out byte data) => NativeDioInpByte(id, portNo, out data);
        public int InpBit(short id, short bitNo, out byte data) => NativeDioInpBit(id, bitNo, out data);
        public int OutByte(short id, short portNo, byte data) => NativeDioOutByte(id, portNo, data);
        public int OutBit(short id, short bitNo, byte data) => NativeDioOutBit(id, bitNo, data);
        public int EchoBackByte(short id, short portNo, out byte data) => NativeDioEchoBackByte(id, portNo, out data);
        public int EchoBackBit(short id, short bitNo, out byte data) => NativeDioEchoBackBit(id, bitNo, out data);

        public int InpMultiByte(short id, short[] portNo, short portNum, byte[] data) => NativeDioInpMultiByte(id, portNo, portNum, data);
        public int InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => NativeDioInpMultiBit(id, bitNo, bitNum, data);
        public int OutMultiByte(short id, short[] portNo, short portNum, byte[] data) => NativeDioOutMultiByte(id, portNo, portNum, data);
        public int OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => NativeDioOutMultiBit(id, bitNo, bitNum, data);
        public int EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data) => NativeDioEchoBackMultiByte(id, portNo, portNum, data);
        public int EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => NativeDioEchoBackMultiBit(id, bitNo, bitNum, data);

        public int NotifyInterrupt(short id, short intBit, short logic, int hWnd) => NativeDioNotifyInterrupt(id, intBit, logic, hWnd);
        public int NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd) => NativeDioNotifyTrg(id, trgBit, trgKind, tim, hWnd);
        public int StopNotifyTrg(short id, short trgBit) => NativeDioStopNotifyTrg(id, trgBit);

        public int GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3)
            => NativeDioGetDeviceInfo(device, infoType, out param1, out param2, out param3);

        public int QueryDeviceName(short index, out string deviceName, out string device)
        {
            byte[] nameBuf = new byte[256];
            byte[] devBuf = new byte[256];
            int ret = NativeDioQueryDeviceName(index, nameBuf, devBuf);
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

        public int GetDeviceType(string device, out short deviceType) => NativeDioGetDeviceType(device, out deviceType);
        public int GetMaxPorts(short id, out short inPortNum, out short outPortNum) => NativeDioGetMaxPorts(id, out inPortNum, out outPortNum);

        public int DmSetDirection(short id, short direction) => NativeDioDmSetDirection(id, direction);
        public int DmGetDirection(short id, out short direction) => NativeDioDmGetDirection(id, out direction);
        public int DmSetStandAlone(short id) => NativeDioDmSetStandAlone(id);
        public int DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => NativeDioDmSetMaster(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public int DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => NativeDioDmSetSlave(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public int DmSetStartTrigger(short id, short direction, short start) => NativeDioDmSetStartTrigger(id, direction, start);
        public int DmSetStartPattern(short id, uint pattern, uint mask) => NativeDioDmSetStartPattern(id, pattern, mask);
        public int DmSetClockTrigger(short id, short direction, short clock) => NativeDioDmSetClockTrigger(id, direction, clock);
        public int DmSetInternalClock(short id, short direction, uint clock, short unit) => NativeDioDmSetInternalClock(id, direction, clock, unit);
        public int DmSetStopTrigger(short id, short direction, short stop) => NativeDioDmSetStopTrigger(id, direction, stop);
        public int DmSetStopNumber(short id, short direction, uint stopNumber) => NativeDioDmSetStopNumber(id, direction, stopNumber);
        public int DmFifoReset(short id, short reset) => NativeDioDmFifoReset(id, reset);
        public int DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing) => NativeDioDmSetBuffer(id, direction, buffer, length, isRing);
        public int DmSetTransferStartWait(short id, short time) => NativeDioDmSetTransferStartWait(id, time);
        public int DmTransferStart(short id, short direction) => NativeDioDmTransferStart(id, direction);
        public int DmTransferStop(short id, short direction) => NativeDioDmTransferStop(id, direction);
        public int DmGetStatus(short id, short direction, out uint status, out uint err) => NativeDioDmGetStatus(id, direction, out status, out err);
        public int DmGetCount(short id, short direction, out uint count, out uint carry) => NativeDioDmGetCount(id, direction, out count, out carry);
        public int DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry)
            => NativeDioDmGetWritePointer(id, direction, out writePointer, out count, out carry);
        public int DmSetStopEvent(short id, short direction, int hWnd) => NativeDioDmSetStopEvent(id, direction, hWnd);
        public int DmSetCountEvent(short id, short direction, uint count, int hWnd) => NativeDioDmSetCountEvent(id, direction, count, hWnd);

        public int SetDemoByte(short id, short portNo, byte data) => NativeDioSetDemoByte(id, portNo, data);
        public int SetDemoBit(short id, short bitNo, byte data) => NativeDioSetDemoBit(id, bitNo, data);

        #endregion
    }
}