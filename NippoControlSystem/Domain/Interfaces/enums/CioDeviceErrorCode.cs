namespace NippoControlSystem.Domain.Interfaces.enums
{
    public enum CioDeviceErrorCode
    {
        //-------------------------------------------------
        //	Error	codes
        //-------------------------------------------------
        //	Initialize	Error
        //	Common
        ERR_SUCCESS = 0,        //	normal completed
        ERR_INI_RESOURCE = 1,       //	invalid resource reference specified
        ERR_INI_INTERRUPT = 2,      //	invalid interrupt routine registered
        ERR_INI_MEMORY = 3,     //	invalid memory allocationed
        ERR_INI_REGISTRY = 4,       //	invalid registry accesse

        ERR_SYS_RECOVERED_FROM_STANDBY = 7,     //	Execute DioResetDevice function because the device has recovered from standby mode.
        ERR_INI_NOT_FOUND_SYS_FILE = 8,     //	Because the Cdio.sys file is not found, it is not possible to initialize it.
        ERR_INI_DLL_FILE_VERSION = 9,       //	Because version information on the Cdio.dll file cannot be acquired, it is not possible to initialize it.
        ERR_INI_SYS_FILE_VERSION = 10,      //	Because version information on the Cdio.sys file cannot be acquired, it is not possible to initialize it.
        ERR_INI_NO_MATCH_DRV_VERSION = 11,      //	Because version information on Cdio.dll and Cdio.sys is different, it is not possible to initialize it.

        //	DLL	Error
        //	Common
        ERR_DLL_DEVICE_NAME = 10000,    //	invalid device name specified.
        ERR_DLL_INVALID_ID = 10001, //	invalid ID specified.
        ERR_DLL_CALL_DRIVER = 10002,    //	not call the driver.(Invalid device I/O controller)
        ERR_DLL_CREATE_FILE = 10003,    //	not create the file.(Invalid CreateFile)
        ERR_DLL_CLOSE_FILE = 10004, //	not close the file.(Invalid CloseFile)
        ERR_DLL_CREATE_THREAD = 10005,  //	not create the thread.(Invalid CreateThread)
        ERR_INFO_INVALID_DEVICE = 10050,    //	invalid device infomation specified .Please check the spell.
        ERR_INFO_NOT_FIND_DEVICE = 10051,   //	not find the available device
        ERR_INFO_INVALID_INFOTYPE = 10052,  //	specified device infomation type beyond the limit

        //	DIO
        ERR_DLL_BUFF_ADDRESS = 10100,   //	invalid data buffer address
        ERR_DLL_HWND = 10200,   //	window handle beyond the limit
        ERR_DLL_TRG_KIND = 10300,   //	trigger kind beyond the limit

        //	SYS	Error
        //	Common
        ERR_SYS_MEMORY = 20000, //	not secure memory
        ERR_SYS_NOT_SUPPORTED = 20001,  //	this board couldn't use this function
        ERR_SYS_BOARD_EXECUTING = 20002,    //	board is behaving, not execute
        ERR_SYS_USING_OTHER_PROCESS = 20003,    //	other process is using the device, not execute

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
        ERR_SYS_PORT_NO = 20100,    //	board No. beyond the limit
        ERR_SYS_PORT_NUM = 20101,   //	board number beyond the limit
        ERR_SYS_BIT_NO = 20102, //	bit No. beyond the limit
        ERR_SYS_BIT_NUM = 20103,    //	bit number beyond the limit
        ERR_SYS_BIT_DATA = 20104,   //	bit data beyond the limit of 0 to 1
        ERR_SYS_INT_BIT = 20200,    //	interrupt bit beyond the limit
        ERR_SYS_INT_LOGIC = 20201,  //	interrupt logic beyond the limit
        ERR_SYS_TIM = 20300,    //	timer value beyond the limit
        ERR_SYS_FILTER = 20400, //	filter number beyond the limit
        ERR_SYS_IODIRECTION = 20500,    //	Direction value is out of range

        //  DM
        ERR_SYS_SIGNAL = 21000, //	Usable signal is outside the setting range.
        ERR_SYS_START = 21001,  //	Usable start conditions are outside the setting range.
        ERR_SYS_CLOCK = 21002,  //	Clock conditions are outside the setting range.
        ERR_SYS_CLOCK_VAL = 21003,  //	Clock value is outside the setting range.
        ERR_SYS_CLOCK_UNIT = 21004, //	Clock value unit is outside the setting range.
        ERR_SYS_STOP = 21005,   //	Stop conditions are outside the setting range.
        ERR_SYS_STOP_NUM = 21006,   //	Stop number is outside the setting range.
        ERR_SYS_RESET = 21007,  //	Contents of reset are outside the setting range.
        ERR_SYS_LEN = 21008,    //	Data number is outside the setting range.
        ERR_SYS_RING = 21009,   //	Buffer repetition use setup is outside the setting range.
        ERR_SYS_COUNT = 21010,  //	Data transmission number is outside the setting range.
        ERR_DM_BUFFER = 21100,  //	Buffer was too large and has not secured.
        ERR_DM_LOCK_MEMORY = 21101, //	Memory has not been locked.
        ERR_DM_PARAM = 21102,   //	Parameter error
        ERR_DM_SEQUENCE = 21103 //	Procedure error of execution

    }
}
