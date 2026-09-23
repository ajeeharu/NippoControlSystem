namespace NippoControlSystem.Domain.Interfaces
{
    public enum CdioErrorCode
    {
        //-------------------------------------------------
        //	Type definition
        //-------------------------------------------------
        DEVICE_TYPE_ISA = 0,    //	ISA or C bus
        DEVICE_TYPE_PC = 1, //	PCI bus
        DEVICE_TYPE_PCMCIA = 2, //	PCMCIA
        DEVICE_TYPE_USB = 3,    //	USB
        DEVICE_TYPE_FIT = 4,    //	FIT
        DEVICE_TYPE_CARDBUS = 5,    //	CardBus

        //-------------------------------------------------
        //	Parameters
        //-------------------------------------------------
        //	I/O(for Sample)
        DIO_MAX_ACCS_PORTS = 256,
        //	DioNotifyInt:Logic
        DIO_INT_NONE = 0,
        DIO_INT_RISE = 1,
        DIO_INT_FALL = 2,
        //	DioNotifyTrg:TrgKind
        DIO_TRG_RISE = 1,
        DIO_TRG_FALL = 2,
        //	Message
        DIOM_INTERRUPT = 0x1300,
        DIOM_TRIGGER = 0x1340,
        DIO_DMM_STOP = 0x1350,
        DIO_DMM_COUNT = 0x1360,
        //	Device Information
        IDIO_DEVICE_TYPE = 0,   //	device type.							Param1:short
        IDIO_NUMBER_OF_8255 = 1,    //	Number of 8255 chip.					Param1:int
        IDIO_IS_8255_BOARD = 2, //	Is 8255	board?							Param1:BOOL(True/False)
        IDIO_NUMBER_OF_DI_BIT = 3,  //	Number of digital input bit.			Param1:int
        IDIO_NUMBER_OF_DO_BIT = 4,  //	Number of digital outout bit.			Param1:int
        IDIO_NUMBER_OF_DI_PORT = 5, //	Number of digital input	port.			Param1:int
        IDIO_NUMBER_OF_DO_PORT = 6, //	Number of digital output port.			Param1:int
        IDIO_IS_POSITIVE_LOGIC = 7, //	Is positive logic?						Param1:BOOL(True/False)
        IDIO_IS_ECHO_BACK = 8,  //	Can echo back output port?				Param1:BOOL(True/False)
        IDIO_IS_DIRECTION = 9,  //	Can DioSetIoDirection function be used?	Param1:int(1:true, 0:false)
        IDIO_IS_FILTER = 10,    //	Can digital filter be used?				Param1:int(1:true, 0:false)
        IDIO_NUMBER_OF_INT_BIT = 11,    //	Number of interrupt bit.				Param1:short

        //	DM
        //	Direction
        PI_32 = 1,
        PO_32 = 2,
        PIO_1616 = 3,
        DIODM_DIR_IN = 0x1,
        DIODM_DIR_OUT = 0x2,

        //  Start
        DIODM_START_SOFT = 1,
        DIODM_START_EXT_RISE = 2,
        DIODM_START_EXT_FALL = 3,
        DIODM_START_PATTERN = 4,
        DIODM_START_EXTSIG_1 = 5,
        DIODM_START_EXTSIG_2 = 6,
        DIODM_START_EXTSIG_3 = 7,

        //  Clock
        DIODM_CLK_CLOCK = 1,
        DIODM_CLK_EXT_TRG = 2,
        DIODM_CLK_HANDSHAKE = 3,
        DIODM_CLK_EXTSIG_1 = 4,
        DIODM_CLK_EXTSIG_2 = 5,
        DIODM_CLK_EXTSIG_3 = 6,

        //  Internal Clock
        DIODM_TIM_UNIT_S = 1,
        DIODM_TIM_UNIT_MS = 2,
        DIODM_TIM_UNIT_US = 3,
        DIODM_TIM_UNIT_NS = 4,

        //  Stop
        DIODM_STOP_SOFT = 1,
        DIODM_STOP_EXT_RISE = 2,
        DIODM_STOP_EXT_FALL = 3,
        DIODM_STOP_NUM = 4,
        DIODM_STOP_EXTSIG_1 = 5,
        DIODM_STOP_EXTSIG_2 = 6,
        DIODM_STOP_EXTSIG_3 = 7,

        //	ExtSig
        DIODM_EXT_START_SOFT_IN = 1,
        DIODM_EXT_STOP_SOFT_IN = 2,
        DIODM_EXT_CLOCK_IN = 3,
        DIODM_EXT_EXT_TRG_IN = 4,
        DIODM_EXT_START_EXT_RISE_IN = 5,
        DIODM_EXT_START_EXT_FALL_IN = 6,
        DIODM_EXT_START_PATTERN_IN = 7,
        DIODM_EXT_STOP_EXT_RISE_IN = 8,
        DIODM_EXT_STOP_EXT_FALL_IN = 9,
        DIODM_EXT_CLOCK_ERROR_IN = 10,
        DIODM_EXT_HANDSHAKE_IN = 11,
        DIODM_EXT_TRNSNUM_IN = 12,

        DIODM_EXT_START_SOFT_OUT = 101,
        DIODM_EXT_STOP_SOFT_OUT = 102,
        DIODM_EXT_CLOCK_OUT = 103,
        DIODM_EXT_EXT_TRG_OUT = 104,
        DIODM_EXT_START_EXT_RISE_OUT = 105,
        DIODM_EXT_START_EXT_FALL_OUT = 106,
        DIODM_EXT_STOP_EXT_RISE_OUT = 107,
        DIODM_EXT_STOP_EXT_FALL_OUT = 108,
        DIODM_EXT_CLOCK_ERROR_OUT = 109,
        DIODM_EXT_HANDSHAKE_OUT = 110,
        DIODM_EXT_TRNSNUM_OUT = 111,

        //	Status
        DIODM_STATUS_BMSTOP = 0x1,
        DIODM_STATUS_PIOSTART = 0x2,
        DIODM_STATUS_PIOSTOP = 0x4,
        DIODM_STATUS_TRGIN = 0x8,
        DIODM_STATUS_OVERRUN = 0x10,

        //	Error
        DIODM_STATUS_FIFOEMPTY = 0x1,
        DIODM_STATUS_FIFOFULL = 0x2,
        DIODM_STATUS_SGOVERIN = 0x4,
        DIODM_STATUS_TRGERR = 0x8,
        DIODM_STATUS_CLKERR = 0x10,
        DIODM_STATUS_SLAVEHALT = 0x20,
        DIODM_STATUS_MASTERHALT = 0x40,

        //	Reset
        DIODM_RESET_FIFO_IN = 0x02,
        DIODM_RESET_FIFO_OUT = 0x04,

        //	Buffer Ring
        DIODM_WRITE_ONCE = 0,
        DIODM_WRITE_RING = 1,

        //-------------------------------------------------
        //	Error	codes
        //-------------------------------------------------
        //	Initialize	Error
        //	Common
        DIO_ERR_SUCCESS = 0,        //	normal completed
        DIO_ERR_INI_RESOURCE = 1,       //	invalid resource reference specified
        DIO_ERR_INI_INTERRUPT = 2,      //	invalid interrupt routine registered
        DIO_ERR_INI_MEMORY = 3,     //	invalid memory allocationed
        DIO_ERR_INI_REGISTRY = 4,       //	invalid registry accesse

        DIO_ERR_SYS_RECOVERED_FROM_STANDBY = 7,     //	Execute DioResetDevice function because the device has recovered from standby mode.
        DIO_ERR_INI_NOT_FOUND_SYS_FILE = 8,     //	Because the Cdio.sys file is not found, it is not possible to initialize it.
        DIO_ERR_INI_DLL_FILE_VERSION = 9,       //	Because version information on the Cdio.dll file cannot be acquired, it is not possible to initialize it.
        DIO_ERR_INI_SYS_FILE_VERSION = 10,      //	Because version information on the Cdio.sys file cannot be acquired, it is not possible to initialize it.
        DIO_ERR_INI_NO_MATCH_DRV_VERSION = 11,      //	Because version information on Cdio.dll and Cdio.sys is different, it is not possible to initialize it.

        //	DLL	Error
        //	Common
        DIO_ERR_DLL_DEVICE_NAME = 10000,    //	invalid device name specified.
        DIO_ERR_DLL_INVALID_ID = 10001, //	invalid ID specified.
        DIO_ERR_DLL_CALL_DRIVER = 10002,    //	not call the driver.(Invalid device I/O controller)
        DIO_ERR_DLL_CREATE_FILE = 10003,    //	not create the file.(Invalid CreateFile)
        DIO_ERR_DLL_CLOSE_FILE = 10004, //	not close the file.(Invalid CloseFile)
        DIO_ERR_DLL_CREATE_THREAD = 10005,  //	not create the thread.(Invalid CreateThread)
        DIO_ERR_INFO_INVALID_DEVICE = 10050,    //	invalid device infomation specified .Please check the spell.
        DIO_ERR_INFO_NOT_FIND_DEVICE = 10051,   //	not find the available device
        DIO_ERR_INFO_INVALID_INFOTYPE = 10052,  //	specified device infomation type beyond the limit

        //	DIO
        DIO_ERR_DLL_BUFF_ADDRESS = 10100,   //	invalid data buffer address
        DIO_ERR_DLL_HWND = 10200,   //	window handle beyond the limit
        DIO_ERR_DLL_TRG_KIND = 10300,   //	trigger kind beyond the limit

        //	SYS	Error
        //	Common
        DIO_ERR_SYS_MEMORY = 20000, //	not secure memory
        DIO_ERR_SYS_NOT_SUPPORTED = 20001,  //	this board couldn't use this function
        DIO_ERR_SYS_BOARD_EXECUTING = 20002,    //	board is behaving, not execute
        DIO_ERR_SYS_USING_OTHER_PROCESS = 20003,    //	other process is using the device, not execute

        STATUS_SYS_USB_CRC = 20020, //	the last data packet received from end point exist CRC error
        STATUS_SYS_USB_BTSTUFF = 20021, //	the last data packet received from end point exist bit stuffing offense error
        STATUS_SYS_USB_DATA_TOGGLE_MISMATCH = 20022,    //	the last data packet received from end point exist toggle packet mismatch error
        STATUS_SYS_USB_STALL_PID = 20023,   //	end point return STALL packet identifier
        STATUS_SYS_USB_DEV_NOT_RESPONDING = 20024,  //	device don't respond to token(IN), don't support handshake
        STATUS_SYS_USB_PID_CHECK_FAILURE = 20025,
        STATUS_SYS_USB_UNEXPECTED_PID = 20026,  //	invalid packet identifier received
        STATUS_SYS_USB_DATA_OVERRUN = 20027,    //	end point return data quantity overrun
        STATUS_SYS_USB_DATA_UNDERRUN = 20028,   //	end point return data quantity underrun
        STATUS_SYS_USB_BUFFER_OVERRUN = 20029,  //	IN transmit specified buffer overrun
        STATUS_SYS_USB_BUFFER_UNDERRUN = 20030, //	OUT transmit specified buffer underrun
        STATUS_SYS_USB_ENDPOINT_HALTED = 20031, //	end point status is STALL, not transmit
        STATUS_SYS_USB_NOT_FOUND_DEVINFO = 20032,   //	not found device infomation
        STATUS_SYS_USB_ACCESS_DENIED = 20033,   //	Access denied
        STATUS_SYS_USB_INVALID_HANDLE = 20034,  //	Invalid handle

        //	DIO
        DIO_ERR_SYS_PORT_NO = 20100,    //	board No. beyond the limit
        DIO_ERR_SYS_PORT_NUM = 20101,   //	board number beyond the limit
        DIO_ERR_SYS_BIT_NO = 20102, //	bit No. beyond the limit
        DIO_ERR_SYS_BIT_NUM = 20103,    //	bit number beyond the limit
        DIO_ERR_SYS_BIT_DATA = 20104,   //	bit data beyond the limit of 0 to 1
        DIO_ERR_SYS_INT_BIT = 20200,    //	interrupt bit beyond the limit
        DIO_ERR_SYS_INT_LOGIC = 20201,  //	interrupt logic beyond the limit
        DIO_ERR_SYS_TIM = 20300,    //	timer value beyond the limit
        DIO_ERR_SYS_FILTER = 20400, //	filter number beyond the limit
        DIO_ERR_SYS_IODIRECTION = 20500,    //	Direction value is out of range

        //  DM
        DIO_ERR_SYS_SIGNAL = 21000, //	Usable signal is outside the setting range.
        DIO_ERR_SYS_START = 21001,  //	Usable start conditions are outside the setting range.
        DIO_ERR_SYS_CLOCK = 21002,  //	Clock conditions are outside the setting range.
        DIO_ERR_SYS_CLOCK_VAL = 21003,  //	Clock value is outside the setting range.
        DIO_ERR_SYS_CLOCK_UNIT = 21004, //	Clock value unit is outside the setting range.
        DIO_ERR_SYS_STOP = 21005,   //	Stop conditions are outside the setting range.
        DIO_ERR_SYS_STOP_NUM = 21006,   //	Stop number is outside the setting range.
        DIO_ERR_SYS_RESET = 21007,  //	Contents of reset are outside the setting range.
        DIO_ERR_SYS_LEN = 21008,    //	Data number is outside the setting range.
        DIO_ERR_SYS_RING = 21009,   //	Buffer repetition use setup is outside the setting range.
        DIO_ERR_SYS_COUNT = 21010,  //	Data transmission number is outside the setting range.
        DIO_ERR_DM_BUFFER = 21100,  //	Buffer was too large and has not secured.
        DIO_ERR_DM_LOCK_MEMORY = 21101, //	Memory has not been locked.
        DIO_ERR_DM_PARAM = 21102,   //	Parameter error
        DIO_ERR_DM_SEQUENCE = 21103	//	Procedure error of execution

    }
}
