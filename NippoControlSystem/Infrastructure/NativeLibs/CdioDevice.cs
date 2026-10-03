using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;
using System.Runtime.InteropServices;
using System.Text;

namespace NippoControlSystem.Infrastructure.NativeLibs
{
    public unsafe partial class CdioDevice : ICdioDevice
    {
        private const string LibraryName = "cdio.dll";

        // スレッドセーフ確保用のセマフォ (同時実行数: 1)
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

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
        public CioDeviceErrorCode Init(string deviceName, out short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioInit(deviceName, out id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode Exit(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioExit(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode ResetDevice(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioResetDevice(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetErrorString(int errorCode, out string errorString)
        {
            _semaphore.Wait();
            try
            {
                byte[] buffer = new byte[256];
                int ret = NativeDioGetErrorString(errorCode, buffer);
                errorString = ret == 0 ? Encoding.Default.GetString(buffer).TrimEnd('\0') : string.Empty;
                return (CioDeviceErrorCode)ret;
            }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetDigitalFilter(short id, short filterValue)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSetDigitalFilter(id, filterValue); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetDigitalFilter(short id, out short filterValue)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetDigitalFilter(id, out filterValue); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetIoDirection(short id, uint dwDir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSetIoDirection(id, dwDir); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetIoDirection(short id, out uint dwDir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetIoDirection(id, out dwDir); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetIoDirectionEx(short id, uint dwDir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSetIoDirectionEx(id, dwDir); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetIoDirectionEx(short id, out uint dwDir)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetIoDirectionEx(id, out dwDir); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode Set8255Mode(short id, short chipNo, short ctrlWord)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSet8255Mode(id, chipNo, ctrlWord); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode Get8255Mode(short id, short chipNo, out short ctrlWord)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGet8255Mode(id, chipNo, out ctrlWord); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InpByte(short id, short portNo, out byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioInpByte(id, portNo, out data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InpBit(short id, short bitNo, out byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioInpBit(id, bitNo, out data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutByte(short id, short portNo, byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioOutByte(id, portNo, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutBit(short id, short bitNo, byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioOutBit(id, bitNo, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode EchoBackByte(short id, short portNo, out byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioEchoBackByte(id, portNo, out data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode EchoBackBit(short id, short bitNo, out byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioEchoBackBit(id, bitNo, out data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InpMultiByte(short id, short[] portNo, short portNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioInpMultiByte(id, portNo, portNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode InpMultiBit(short id, short[] bitNo, short bitNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioInpMultiBit(id, bitNo, bitNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutMultiByte(short id, short[] portNo, short portNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioOutMultiByte(id, portNo, portNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode OutMultiBit(short id, short[] bitNo, short bitNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioOutMultiBit(id, bitNo, bitNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode EchoBackMultiByte(short id, short[] portNo, short portNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioEchoBackMultiByte(id, portNo, portNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode EchoBackMultiBit(short id, short[] bitNo, short bitNum, byte[] data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioEchoBackMultiBit(id, bitNo, bitNum, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode NotifyInterrupt(short id, short intBit, short logic, int hWnd)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioNotifyInterrupt(id, intBit, logic, hWnd); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode NotifyTrg(short id, short trgBit, short trgKind, int tim, int hWnd)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioNotifyTrg(id, trgBit, trgKind, tim, hWnd); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode StopNotifyTrg(short id, short trgBit)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioStopNotifyTrg(id, trgBit); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetDeviceInfo(string device, short infoType, out int param1, out int param2, out int param3)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetDeviceInfo(device, infoType, out param1, out param2, out param3); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode QueryDeviceName(short index, out string deviceName, out string device)
        {
            _semaphore.Wait();
            try
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
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetDeviceType(string device, out short deviceType)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetDeviceType(device, out deviceType); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode GetMaxPorts(short id, out short inPortNum, out short outPortNum)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioGetMaxPorts(id, out inPortNum, out outPortNum); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetDirection(short id, short direction)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetDirection(id, direction); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmGetDirection(short id, out short direction)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmGetDirection(id, out direction); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStandAlone(short id)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStandAlone(id); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetMaster(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetMaster(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetSlave(short id, short extSig1, short extSig2, short extSig3, short masterHalt, short slaveHalt)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetSlave(id, extSig1, extSig2, extSig3, masterHalt, slaveHalt); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStartTrigger(short id, short direction, short start)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStartTrigger(id, direction, start); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStartPattern(short id, uint pattern, uint mask)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStartPattern(id, pattern, mask); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetClockTrigger(short id, short direction, short clock)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetClockTrigger(id, direction, clock); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetInternalClock(short id, short direction, uint clock, short unit)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetInternalClock(id, direction, clock, unit); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStopTrigger(short id, short direction, short stop)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStopTrigger(id, direction, stop); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStopNumber(short id, short direction, uint stopNumber)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStopNumber(id, direction, stopNumber); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmFifoReset(short id, short reset)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmFifoReset(id, reset); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetBuffer(short id, short direction, IntPtr buffer, uint length, short isRing)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetBuffer(id, direction, buffer, length, isRing); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetTransferStartWait(short id, short time)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetTransferStartWait(id, time); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmTransferStart(short id, short direction)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmTransferStart(id, direction); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmTransferStop(short id, short direction)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmTransferStop(id, direction); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmGetStatus(short id, short direction, out uint status, out uint err)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmGetStatus(id, direction, out status, out err); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmGetCount(short id, short direction, out uint count, out uint carry)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmGetCount(id, direction, out count, out carry); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmGetWritePointer(short id, short direction, out uint writePointer, out uint count, out uint carry)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmGetWritePointer(id, direction, out writePointer, out count, out carry); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetStopEvent(short id, short direction, int hWnd)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetStopEvent(id, direction, hWnd); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode DmSetCountEvent(short id, short direction, uint count, int hWnd)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioDmSetCountEvent(id, direction, count, hWnd); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetDemoByte(short id, short portNo, byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSetDemoByte(id, portNo, data); }
            finally { _semaphore.Release(); }
        }

        public CioDeviceErrorCode SetDemoBit(short id, short bitNo, byte data)
        {
            _semaphore.Wait();
            try { return (CioDeviceErrorCode)NativeDioSetDemoBit(id, bitNo, data); }
            finally { _semaphore.Release(); }
        }
        #endregion
    }
}