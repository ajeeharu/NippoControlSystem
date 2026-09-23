using NippoControlSystem.Domain.Interfaces;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CdioNative : ICdioNative
    {
        private const string LibraryName = "cdio.dll";

        #region Native Imports (LibraryImport)
        [LibraryImport(LibraryName, EntryPoint = "DioInit", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioInit(string deviceName, out short id);

        [LibraryImport(LibraryName, EntryPoint = "DioExit")]
        private static partial int NativeDioExit(short id);

        [LibraryImport(LibraryName, EntryPoint = "DioResetDevice")]
        private static partial int NativeDioResetDevice(short id);

        [LibraryImport(LibraryName, EntryPoint = "DioGetErrorString")]
        private static partial int NativeDioGetErrorString(int errorCode, [Out] byte[] errorString);

        [LibraryImport(LibraryName, EntryPoint = "DioSetDigitalFilter")]
        private static partial int NativeDioSetDigitalFilter(short id, short filterValue);

        [LibraryImport(LibraryName, EntryPoint = "DioGetDigitalFilter")]
        private static partial int NativeDioGetDigitalFilter(short id, out short filterValue);

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

        [LibraryImport(LibraryName, EntryPoint = "DioNotifyInterrupt")]
        private static partial int NativeDioNotifyInterrupt(short id, short intBit, short logic, int hWnd);

        [LibraryImport(LibraryName, EntryPoint = "DioNotifyTrg")]
        private static partial int NativeDioNotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd);

        [LibraryImport(LibraryName, EntryPoint = "DioStopNotifyTrg")]
        private static partial int NativeDioStopNotifyTrg(short id, short trgBit);

        [LibraryImport(LibraryName, EntryPoint = "DioGetDeviceInfo", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioGetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3);

        [LibraryImport(LibraryName, EntryPoint = "DioQueryDeviceName")]
        private static partial int NativeDioQueryDeviceName(short index, [Out] byte[] deviceName, [Out] byte[] device);

        [LibraryImport(LibraryName, EntryPoint = "DioGetDeviceType", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int NativeDioGetDeviceType(string device, out short deviceType);

        [LibraryImport(LibraryName, EntryPoint = "DioGetMaxPorts")]
        private static partial int NativeDioGetMaxPorts(short id, out short inPortNum, out short outPortNum);

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

        [LibraryImport(LibraryName, EntryPoint = "DioSetDemoByte")]
        private static partial int NativeDioSetDemoByte(short id, short portNo, byte data);

        [LibraryImport(LibraryName, EntryPoint = "DioSetDemoBit")]
        private static partial int NativeDioSetDemoBit(short id, short bitNo, byte data);
        #endregion

        #region Public Wrapper Methods
        public CdioErrorCode Init(string deviceName, out short id) => (CdioErrorCode)NativeDioInit(deviceName, out id);
        public CdioErrorCode Exit(short id) => (CdioErrorCode)NativeDioExit(id);
        public CdioErrorCode ResetDevice(short id) => (CdioErrorCode)NativeDioResetDevice(id);

        public CdioErrorCode GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeDioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return (CdioErrorCode)ret;
        }

        public CdioErrorCode SetDigitalFilter(short id, short filterValue) => (CdioErrorCode)NativeDioSetDigitalFilter(id, filterValue);
        public CdioErrorCode GetDigitalFilter(short id, out short filterValue) => (CdioErrorCode)NativeDioGetDigitalFilter(id, out filterValue);

        public CdioErrorCode SetIoDirection(short id, uint dwDir) => (CdioErrorCode)NativeDioSetIoDirection(id, dwDir);
        public CdioErrorCode GetIoDirection(short id, out uint dwDir) => (CdioErrorCode)NativeDioGetIoDirection(id, out dwDir);
        public CdioErrorCode SetIoDirectionEx(short id, uint dwDir) => (CdioErrorCode)NativeDioSetIoDirectionEx(id, dwDir);
        public CdioErrorCode GetIoDirectionEx(short id, out uint dwDir) => (CdioErrorCode)NativeDioGetIoDirectionEx(id, out dwDir);
        public CdioErrorCode Set8255Mode(short id, short chipNo, short ctrlWord) => (CdioErrorCode)NativeDioSet8255Mode(id, chipNo, ctrlWord);
        public CdioErrorCode Get8255Mode(short id, short chipNo, out short ctrlWord) => (CdioErrorCode)NativeDioGet8255Mode(id, chipNo, out ctrlWord);

        public CdioErrorCode InpByte(short id, short portNo, out byte data) => (CdioErrorCode)NativeDioInpByte(id, portNo, out data);
        public CdioErrorCode InpBit(short id, short bitNo, out byte data) => (CdioErrorCode)NativeDioInpBit(id, bitNo, out data);
        public CdioErrorCode OutByte(short id, short portNo, byte data) => (CdioErrorCode)NativeDioOutByte(id, portNo, data);
        public CdioErrorCode OutBit(short id, short bitNo, byte data) => (CdioErrorCode)NativeDioOutBit(id, bitNo, data);
        public CdioErrorCode EchoBackByte(short id, short portNo, out byte data) => (CdioErrorCode)NativeDioEchoBackByte(id, portNo, out data);
        public CdioErrorCode EchoBackBit(short id, short bitNo, out byte data) => (CdioErrorCode)NativeDioEchoBackBit(id, bitNo, out data);

        public CdioErrorCode InpMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CdioErrorCode)NativeDioInpMultiByte(id, portNo, portNum, data);
        public CdioErrorCode InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CdioErrorCode)NativeDioInpMultiBit(id, bitNo, bitNum, data);
        public CdioErrorCode OutMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CdioErrorCode)NativeDioOutMultiByte(id, portNo, portNum, data);
        public CdioErrorCode OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CdioErrorCode)NativeDioOutMultiBit(id, bitNo, bitNum, data);
        public CdioErrorCode EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CdioErrorCode)NativeDioEchoBackMultiByte(id, portNo, portNum, data);
        public CdioErrorCode EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CdioErrorCode)NativeDioEchoBackMultiBit(id, bitNo, bitNum, data);
        public CdioErrorCode NotifyInterrupt(short id, short intBit, short logic, int hWnd) => (CdioErrorCode)NativeDioNotifyInterrupt(id, intBit, logic, hWnd);
        public CdioErrorCode NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd) => (CdioErrorCode)NativeDioNotifyTrg(id, trgBit, trgKind, tim, hWnd);
        public CdioErrorCode StopNotifyTrg(short id, short trgBit) => (CdioErrorCode)NativeDioStopNotifyTrg(id, trgBit);

        public CdioErrorCode GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3)
            => (CdioErrorCode)NativeDioGetDeviceInfo(device, infoType, out param1, out param2, out param3);

        public CdioErrorCode QueryDeviceName(short index, out string deviceName, out string device)
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
            return (CdioErrorCode)ret;
        }

        public CdioErrorCode GetDeviceType(string device, out short deviceType) => (CdioErrorCode)NativeDioGetDeviceType(device, out deviceType);
        public CdioErrorCode GetMaxPorts(short id, out short inPortNum, out short outPortNum) => (CdioErrorCode)NativeDioGetMaxPorts(id, out inPortNum, out outPortNum);

        public CdioErrorCode DmSetDirection(short id, short direction) => (CdioErrorCode)NativeDioDmSetDirection(id, direction);
        public CdioErrorCode DmGetDirection(short id, out short direction) => (CdioErrorCode)NativeDioDmGetDirection(id, out direction);
        public CdioErrorCode DmSetStandAlone(short id) => (CdioErrorCode)NativeDioDmSetStandAlone(id);
        public CdioErrorCode DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => (CdioErrorCode)NativeDioDmSetMaster(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public CdioErrorCode DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => (CdioErrorCode)NativeDioDmSetSlave(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public CdioErrorCode DmSetStartTrigger(short id, short direction, short start) => (CdioErrorCode)NativeDioDmSetStartTrigger(id, direction, start);
        public CdioErrorCode DmSetStartPattern(short id, uint pattern, uint mask) => (CdioErrorCode)NativeDioDmSetStartPattern(id, pattern, mask);
        public CdioErrorCode DmSetClockTrigger(short id, short direction, short clock) => (CdioErrorCode)NativeDioDmSetClockTrigger(id, direction, clock);
        public CdioErrorCode DmSetInternalClock(short id, short direction, uint clock, short unit) => (CdioErrorCode)NativeDioDmSetInternalClock(id, direction, clock, unit);
        public CdioErrorCode DmSetStopTrigger(short id, short direction, short stop) => (CdioErrorCode)NativeDioDmSetStopTrigger(id, direction, stop);
        public CdioErrorCode DmSetStopNumber(short id, short direction, uint stopNumber) => (CdioErrorCode)NativeDioDmSetStopNumber(id, direction, stopNumber);
        public CdioErrorCode DmFifoReset(short id, short reset) => (CdioErrorCode)NativeDioDmFifoReset(id, reset);
        public CdioErrorCode DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing) => (CdioErrorCode)NativeDioDmSetBuffer(id, direction, buffer, length, isRing);
        public CdioErrorCode DmSetTransferStartWait(short id, short time) => (CdioErrorCode)NativeDioDmSetTransferStartWait(id, time);
        public CdioErrorCode DmTransferStart(short id, short direction) => (CdioErrorCode)NativeDioDmTransferStart(id, direction);
        public CdioErrorCode DmTransferStop(short id, short direction) => (CdioErrorCode)NativeDioDmTransferStop(id, direction);
        public CdioErrorCode DmGetStatus(short id, short direction, out uint status, out uint err) => (CdioErrorCode)NativeDioDmGetStatus(id, direction, out status, out err);
        public CdioErrorCode DmGetCount(short id, short direction, out uint count, out uint carry) => (CdioErrorCode)NativeDioDmGetCount(id, direction, out count, out carry);
        public CdioErrorCode DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry)
            => (CdioErrorCode)NativeDioDmGetWritePointer(id, direction, out writePointer, out count, out carry);
        public CdioErrorCode DmSetStopEvent(short id, short direction, int hWnd) => (CdioErrorCode)NativeDioDmSetStopEvent(id, direction, hWnd);
        public CdioErrorCode DmSetCountEvent(short id, short direction, uint count, int hWnd) => (CdioErrorCode)NativeDioDmSetCountEvent(id, direction, count, hWnd);

        public CdioErrorCode SetDemoByte(short id, short portNo, byte data) => (CdioErrorCode)NativeDioSetDemoByte(id, portNo, data);
        public CdioErrorCode SetDemoBit(short id, short bitNo, byte data) => (CdioErrorCode)NativeDioSetDemoBit(id, bitNo, data);
        #endregion
    }
}