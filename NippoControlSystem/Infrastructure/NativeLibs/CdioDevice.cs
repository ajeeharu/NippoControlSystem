using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CdioDevice : ICdioDevice
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
        public CioDeviceErrorCode Init(string deviceName, out short id) => (CioDeviceErrorCode)NativeDioInit(deviceName, out id);
        public CioDeviceErrorCode Exit(short id) => (CioDeviceErrorCode)NativeDioExit(id);
        public CioDeviceErrorCode ResetDevice(short id) => (CioDeviceErrorCode)NativeDioResetDevice(id);

        public CioDeviceErrorCode GetErrorString(int errorCode, out string errorString)
        {
            byte[] buffer = new byte[256];
            int ret = NativeDioGetErrorString(errorCode, buffer);
            errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
            return (CioDeviceErrorCode)ret;
        }

        public CioDeviceErrorCode SetDigitalFilter(short id, short filterValue) => (CioDeviceErrorCode)NativeDioSetDigitalFilter(id, filterValue);
        public CioDeviceErrorCode GetDigitalFilter(short id, out short filterValue) => (CioDeviceErrorCode)NativeDioGetDigitalFilter(id, out filterValue);

        public CioDeviceErrorCode SetIoDirection(short id, uint dwDir) => (CioDeviceErrorCode)NativeDioSetIoDirection(id, dwDir);
        public CioDeviceErrorCode GetIoDirection(short id, out uint dwDir) => (CioDeviceErrorCode)NativeDioGetIoDirection(id, out dwDir);
        public CioDeviceErrorCode SetIoDirectionEx(short id, uint dwDir) => (CioDeviceErrorCode)NativeDioSetIoDirectionEx(id, dwDir);
        public CioDeviceErrorCode GetIoDirectionEx(short id, out uint dwDir) => (CioDeviceErrorCode)NativeDioGetIoDirectionEx(id, out dwDir);
        public CioDeviceErrorCode Set8255Mode(short id, short chipNo, short ctrlWord) => (CioDeviceErrorCode)NativeDioSet8255Mode(id, chipNo, ctrlWord);
        public CioDeviceErrorCode Get8255Mode(short id, short chipNo, out short ctrlWord) => (CioDeviceErrorCode)NativeDioGet8255Mode(id, chipNo, out ctrlWord);

        public CioDeviceErrorCode InpByte(short id, short portNo, out byte data) => (CioDeviceErrorCode)NativeDioInpByte(id, portNo, out data);
        public CioDeviceErrorCode InpBit(short id, short bitNo, out byte data) => (CioDeviceErrorCode)NativeDioInpBit(id, bitNo, out data);
        public CioDeviceErrorCode OutByte(short id, short portNo, byte data) => (CioDeviceErrorCode)NativeDioOutByte(id, portNo, data);
        public CioDeviceErrorCode OutBit(short id, short bitNo, byte data) => (CioDeviceErrorCode)NativeDioOutBit(id, bitNo, data);
        public CioDeviceErrorCode EchoBackByte(short id, short portNo, out byte data) => (CioDeviceErrorCode)NativeDioEchoBackByte(id, portNo, out data);
        public CioDeviceErrorCode EchoBackBit(short id, short bitNo, out byte data) => (CioDeviceErrorCode)NativeDioEchoBackBit(id, bitNo, out data);

        public CioDeviceErrorCode InpMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CioDeviceErrorCode)NativeDioInpMultiByte(id, portNo, portNum, data);
        public CioDeviceErrorCode InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CioDeviceErrorCode)NativeDioInpMultiBit(id, bitNo, bitNum, data);
        public CioDeviceErrorCode OutMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CioDeviceErrorCode)NativeDioOutMultiByte(id, portNo, portNum, data);
        public CioDeviceErrorCode OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CioDeviceErrorCode)NativeDioOutMultiBit(id, bitNo, bitNum, data);
        public CioDeviceErrorCode EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data) => (CioDeviceErrorCode)NativeDioEchoBackMultiByte(id, portNo, portNum, data);
        public CioDeviceErrorCode EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data) => (CioDeviceErrorCode)NativeDioEchoBackMultiBit(id, bitNo, bitNum, data);
        public CioDeviceErrorCode NotifyInterrupt(short id, short intBit, short logic, int hWnd) => (CioDeviceErrorCode)NativeDioNotifyInterrupt(id, intBit, logic, hWnd);
        public CioDeviceErrorCode NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd) => (CioDeviceErrorCode)NativeDioNotifyTrg(id, trgBit, trgKind, tim, hWnd);
        public CioDeviceErrorCode StopNotifyTrg(short id, short trgBit) => (CioDeviceErrorCode)NativeDioStopNotifyTrg(id, trgBit);

        public CioDeviceErrorCode GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3)
            => (CioDeviceErrorCode)NativeDioGetDeviceInfo(device, infoType, out param1, out param2, out param3);

        public CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device)
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
            return (CioDeviceErrorCode)ret;
        }

        public CioDeviceErrorCode GetDeviceType(string device, out short deviceType) => (CioDeviceErrorCode)NativeDioGetDeviceType(device, out deviceType);
        public CioDeviceErrorCode GetMaxPorts(short id, out short inPortNum, out short outPortNum) => (CioDeviceErrorCode)NativeDioGetMaxPorts(id, out inPortNum, out outPortNum);

        public CioDeviceErrorCode DmSetDirection(short id, short direction) => (CioDeviceErrorCode)NativeDioDmSetDirection(id, direction);
        public CioDeviceErrorCode DmGetDirection(short id, out short direction) => (CioDeviceErrorCode)NativeDioDmGetDirection(id, out direction);
        public CioDeviceErrorCode DmSetStandAlone(short id) => (CioDeviceErrorCode)NativeDioDmSetStandAlone(id);
        public CioDeviceErrorCode DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => (CioDeviceErrorCode)NativeDioDmSetMaster(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public CioDeviceErrorCode DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
            => (CioDeviceErrorCode)NativeDioDmSetSlave(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt);
        public CioDeviceErrorCode DmSetStartTrigger(short id, short direction, short start) => (CioDeviceErrorCode)NativeDioDmSetStartTrigger(id, direction, start);
        public CioDeviceErrorCode DmSetStartPattern(short id, uint pattern, uint mask) => (CioDeviceErrorCode)NativeDioDmSetStartPattern(id, pattern, mask);
        public CioDeviceErrorCode DmSetClockTrigger(short id, short direction, short clock) => (CioDeviceErrorCode)NativeDioDmSetClockTrigger(id, direction, clock);
        public CioDeviceErrorCode DmSetInternalClock(short id, short direction, uint clock, short unit) => (CioDeviceErrorCode)NativeDioDmSetInternalClock(id, direction, clock, unit);
        public CioDeviceErrorCode DmSetStopTrigger(short id, short direction, short stop) => (CioDeviceErrorCode)NativeDioDmSetStopTrigger(id, direction, stop);
        public CioDeviceErrorCode DmSetStopNumber(short id, short direction, uint stopNumber) => (CioDeviceErrorCode)NativeDioDmSetStopNumber(id, direction, stopNumber);
        public CioDeviceErrorCode DmFifoReset(short id, short reset) => (CioDeviceErrorCode)NativeDioDmFifoReset(id, reset);
        public CioDeviceErrorCode DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing) => (CioDeviceErrorCode)NativeDioDmSetBuffer(id, direction, buffer, length, isRing);
        public CioDeviceErrorCode DmSetTransferStartWait(short id, short time) => (CioDeviceErrorCode)NativeDioDmSetTransferStartWait(id, time);
        public CioDeviceErrorCode DmTransferStart(short id, short direction) => (CioDeviceErrorCode)NativeDioDmTransferStart(id, direction);
        public CioDeviceErrorCode DmTransferStop(short id, short direction) => (CioDeviceErrorCode)NativeDioDmTransferStop(id, direction);
        public CioDeviceErrorCode DmGetStatus(short id, short direction, out uint status, out uint err) => (CioDeviceErrorCode)NativeDioDmGetStatus(id, direction, out status, out err);
        public CioDeviceErrorCode DmGetCount(short id, short direction, out uint count, out uint carry) => (CioDeviceErrorCode)NativeDioDmGetCount(id, direction, out count, out carry);
        public CioDeviceErrorCode DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry)
            => (CioDeviceErrorCode)NativeDioDmGetWritePointer(id, direction, out writePointer, out count, out carry);
        public CioDeviceErrorCode DmSetStopEvent(short id, short direction, int hWnd) => (CioDeviceErrorCode)NativeDioDmSetStopEvent(id, direction, hWnd);
        public CioDeviceErrorCode DmSetCountEvent(short id, short direction, uint count, int hWnd) => (CioDeviceErrorCode)NativeDioDmSetCountEvent(id, direction, count, hWnd);

        public CioDeviceErrorCode SetDemoByte(short id, short portNo, byte data) => (CioDeviceErrorCode)NativeDioSetDemoByte(id, portNo, data);
        public CioDeviceErrorCode SetDemoBit(short id, short bitNo, byte data) => (CioDeviceErrorCode)NativeDioSetDemoBit(id, bitNo, data);
        #endregion
    }
}