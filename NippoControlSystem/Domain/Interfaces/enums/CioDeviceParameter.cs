namespace NippoControlSystem.Domain.Interfaces.enums
{
    public enum AnalogIoParameter
    {
        // 外部制御信号
        AIO_AIF_CLOCK = 0,          //アナログ入力外部クロック
        AIO_AIF_START = 1,          //アナログ入力外部開始トリガ
        AIO_AIF_STOP = 2,           //アナログ入力外部停止トリガ
        AIO_AOF_CLOCK = 3,          //アナログ出力外部クロック
        AIO_AOF_START = 4,          //アナログ出力外部開始トリガ
        AIO_AOF_STOP = 5,           //アナログ出力外部停止トリガ
    }
    public enum AnalogIoRange
    {
        // 入出力レンジ
        PM10 = 0,           //±10V
        PM5 = 1,            //±5V
        PM25 = 2,           //±2.5V
        PM125 = 3,          //±1.25V
        PM1 = 4,            //±1V
        PM0625 = 5,         //±0.625V
        PM05 = 6,           //±0.5V
        PM03125 = 7,            //±0.3125V
        PM025 = 8,          //±0.25V
        PM0125 = 9,         //±0.125V
        PM01 = 10,          //±0.1V
        PM005 = 11,         //±0.05V
        PM0025 = 12,            //±0.025V
        PM00125 = 13,           //±0.0125V
        PM001 = 14,         //±0.01V
        P10 = 50,           //0～10V
        P5 = 51,            //0～5V
        P4095 = 52,         //0～4.095V
        P25 = 53,           //0～2.5V
        P125 = 54,          //0～1.25V
        P1 = 55,            //0～1V
        P05 = 56,           //0～0.5V
        P025 = 57,          //0～0.25V
        P01 = 58,           //0～0.1V
        P005 = 59,          //0～0.05V
        P0025 = 60,         //0～0.025V
        P00125 = 61,            //0～0.0125V
        P001 = 62,          //0～0.01V
        P20MA = 100,            //0～20mA
        P4TO20MA = 101,         //4～20mA
        P1TO5 = 150,            //1～5V
    }
    public enum AnalogInputEvent
    {
        // アナログ入力イベント
        AIE_START = 0x00000002, //AD変換開始条件成立イベント
        AIE_RPTEND = 0x00000010,    //リピート終了イベント
        AIE_END = 0x00000020,   //デバイス動作終了イベント
        AIE_DATA_NUM = 0x00000080,  //指定サンプリング回数格納イベント
        AIE_DATA_TSF = 0x00000100,  //指定転送数毎イベント
        AIE_OFERR = 0x00010000, //オーバーフローイベント
        AIE_SCERR = 0x00020000, //サンプリングクロックエラーイベント
        AIE_ADERR = 0x00040000, //AD変換エラーイベント
    }
    public enum AnalogOutputEvent
    {
        // アナログ出力イベント
        AOE_START = 0x00000002, //DA変換開始条件成立イベント
        AOE_RPTEND = 0x00000010,    //リピート終了イベント
        AOE_END = 0x00000020,   //デバイス動作終了イベント
        AOE_DATA_NUM = 0x00000080,  //指定サンプリング回数出力イベント
        AOE_DATA_TSF = 0x00000100,  //指定転送数毎イベント
        AOE_SCERR = 0x00020000, //サンプリングクロックエラーイベント
        AOE_DAERR = 0x00040000, //DA変換エラーイベント
    }
    public enum CounterEvent
    {
        // カウンタイベント
        CNTE_DATA_NUM = 0x00000010, //比較カウント一致イベント
        CNTE_ORERR = 0x00010000,    //カウントオーバーランイベント
        CNTE_ERR = 0x00020000,  //カウンタ動作エラー
    }
    public enum TimerEvent
    {
        // タイマイベント
        TME_INT = 0x00000001,   //インターバル成立イベント
    }
    public enum AnalogInputStatus
    {
        // アナログ入力ステータス
        AIS_BUSY = 0x00000001,  //デバイス動作中
        AIS_START_TRG = 0x00000002, //開始トリガ待ち
        AIS_DATA_NUM = 0x00000010,  //指定サンプリング回数格納
        AIS_OFERR = 0x00010000, //オーバーフロー
        AIS_SCERR = 0x00020000, //サンプリングクロックエラー
        AIS_AIERR = 0x00040000, //AD変換エラー
        AIS_DRVERR = 0x00080000,    //ドライバスペックエラー
    }
    public enum AnalogOutputStatus
    {
        // アナログ出力ステータス
        AOS_BUSY = 0x00000001,  //デバイス動作中
        AOS_START_TRG = 0x00000002, //開始トリガ待ち
        AOS_DATA_NUM = 0x00000010,  //指定サンプリング回数出力
        AOS_SCERR = 0x00020000, //サンプリングクロックエラー
        AOS_AOERR = 0x00040000, //DA変換エラー
        AOS_DRVERR = 0x00080000,    //ドライバスペックエラー
    }
    public enum CounterStatus
    {
        // カウンタステータス
        CNTS_BUSY = 0x00000001, //カウンタ動作中
        CNTS_DATA_NUM = 0x00000010, //比較カウント一致
        CNTS_ORERR = 0x00010000,    //オーバーラン
        CNTS_ERR = 0x00020000,  //カウンタ動作エラー
    }
    public enum AnalogInputMessage
    {
        // アナログ入力メッセージ
        AIOM_AIE_START = 0x1000,        //AD変換開始条件成立イベント
        AIOM_AIE_RPTEND = 0x1001,       //リピート終了イベント
        AIOM_AIE_END = 0x1002,      //デバイス動作終了イベント
        AIOM_AIE_DATA_NUM = 0x1003,     //指定サンプリング回数格納イベント
        AIOM_AIE_DATA_TSF = 0x1007,     //指定転送数毎イベント
        AIOM_AIE_OFERR = 0x1004,        //オーバーフローイベント
        AIOM_AIE_SCERR = 0x1005,        //サンプリングクロックエラーイベント
        AIOM_AIE_ADERR = 0x1006,        //AD変換エラーイベント
    }
    public enum AnalogOutputMessage
    {
        // アナログ出力メッセージ
        AIOM_AOE_START = 0x1020,        //DA変換開始条件成立イベント
        AIOM_AOE_RPTEND = 0x1021,       //リピート終了イベント
        AIOM_AOE_END = 0x1022,      //デバイス動作終了イベント
        AIOM_AOE_DATA_NUM = 0x1023,     //指定サンプリング回数出力イベント
        AIOM_AOE_DATA_TSF = 0x1027,     //指定転送数毎イベント
        AIOM_AOE_SCERR = 0x1025,        //サンプリングクロックエラーイベント
        AIOM_AOE_DAERR = 0x1026,        //DA変換エラーイベント
    }
    public enum CounterMessage
    {
        // カウンタメッセージ
        AIOM_CNTE_DATA_NUM = 0x1042,        //比較カウント一致イベント
        AIOM_CNTE_ORERR = 0x1043,       //カウントオーバーランイベント
        AIOM_CNTE_ERR = 0x1044,     //カウント動作エラーイベント
        // Mデバイス用カウンタメッセージ
        AIOM_CNTM_COUNTUP_CH0 = 0x1070,     // カウントアップ、チャネル番号0
        AIOM_CNTM_COUNTUP_CH1 = 0x1071,     //         "                   1
    }
    public enum TimerMessage
    {
        // タイマメッセージ
        AIOM_TME_INT = 0x1060,      //インターバル成立イベント
        AIOM_CNTM_TIME_UP = 0x1090,     //タイムアップ
        AIOM_CNTM_COUNTER_ERROR = 0x1091,       //カウンタエラー
        AIOM_CNTM_CARRY_BORROW = 0x1092,        //キャリー／ボロー
    }
    public enum AnalogIoAttachedData
    {
        // アナログ入力添付データ
        AIAT_AI = 0x00000001,   //アナログ入力付属情報
        AIAT_AO0 = 0x00000100,  //アナログ出力データ
        AIAT_DIO0 = 0x00010000, //デジタル入出力データ
        AIAT_CNT0 = 0x01000000, //カウンタチャネル０データ
        AIAT_CNT1 = 0x02000000, //カウンタチャネル１データ
    }
    public enum CounterMode
    {
        // カウンタ動作モード
        CNT_LOADPRESET = 0x0000001, //プリセットカウント値のロード
        CNT_LOADCOMP = 0x0000002,   //比較カウント値のロード
    }
    public enum EventControllerSignal
    {
        // イベントコントローラ接続先信号
        AIOECU_DEST_AI_CLK = 4,         //アナログ入力サンプリングクロック
        AIOECU_DEST_AI_START = 0,           //アナログ入力変換開始信号
        AIOECU_DEST_AI_STOP = 2,            //アナログ入力変換停止信号
        AIOECU_DEST_AO_CLK = 36,            //アナログ出力サンプリングクロック
        AIOECU_DEST_AO_START = 32,          //アナログ出力変換開始信号
        AIOECU_DEST_AO_STOP = 34,           //アナログ出力変換停止信号
        AIOECU_DEST_CNT0_UPCLK = 134,           //カウンタ０アップクロック信号
        AIOECU_DEST_CNT1_UPCLK = 135,           //カウンタ１アップクロック信号
        AIOECU_DEST_CNT0_START = 128,           //カウンタ０、タイマ０動作開始信号
        AIOECU_DEST_CNT1_START = 129,           //カウンタ１、タイマ１動作開始信号
        AIOECU_DEST_CNT0_STOP = 130,            //カウンタ０、タイマ０動作停止信号
        AIOECU_DEST_CNT1_STOP = 131,            //カウンタ１、タイマ１動作停止信号
        AIOECU_DEST_MASTER1 = 104,          //同期バスマスタ信号１
        AIOECU_DEST_MASTER2 = 105,          //同期バスマスタ信号２
        AIOECU_DEST_MASTER3 = 106,          //同期バスマスタ信号３
    }
    public enum EventControllerSource
    {
        // イベントコントローラ接続元信号
        AIOECU_SRC_OPEN = -1,           //未接続
        AIOECU_SRC_AI_CLK = 4,          //アナログ入力内部クロック信号
        AIOECU_SRC_AI_EXTCLK = 146,         //アナログ入力外部クロック信号
        AIOECU_SRC_AI_TRGSTART = 144,           //アナログ入力外部トリガ開始信号
        AIOECU_SRC_AI_LVSTART = 28,         //アナログ入力レベルトリガ開始信号
        AIOECU_SRC_AI_STOP = 17,            //アナログ入力変換回数終了信号（遅延なし）
        AIOECU_SRC_AI_STOP_DELAY = 18,          //アナログ入力変換回数終了信号（遅延あり）
        AIOECU_SRC_AI_LVSTOP = 29,          //アナログ入力レベルトリガ停止信号
        AIOECU_SRC_AI_TRGSTOP = 145,            //アナログ入力外部トリガ停止信号
        AIOECU_SRC_AO_CLK = 66,         //アナログ出力内部クロック信号
        AIOECU_SRC_AO_EXTCLK = 149,         //アナログ出力外部クロック信号
        AIOECU_SRC_AO_TRGSTART = 147,           //アナログ出力外部トリガ開始信号
        AIOECU_SRC_AO_STOP_FIFO = 352,          //アナログ出力指定回数出力終了信号（FIFO使用）
        AIOECU_SRC_AO_STOP_RING = 80,           //アナログ出力指定回数出力終了信号（RING使用）
        AIOECU_SRC_AO_TRGSTOP = 148,            //アナログ出力外部トリガ停止信号
        AIOECU_SRC_CNT0_UPCLK = 150,            //カウンタ０アップクロック信号
        AIOECU_SRC_CNT1_UPCLK = 152,            //カウンタ１アップクロック信号
        AIOECU_SRC_CNT0_CMP = 288,          //カウンタ０比較カウント一致
        AIOECU_SRC_CNT1_CMP = 289,          //カウンタ１比較カウント一致
        AIOECU_SRC_SLAVE1 = 136,            //同期バススレーブ信号１
        AIOECU_SRC_SLAVE2 = 137,            //同期バススレーブ信号２
        AIOECU_SRC_SLAVE3 = 138,            //同期バススレーブ信号３
        AIOECU_SRC_START = 384,         //Ai, Ao, Cnt, Tmソフトウェア開始信号
        AIOECU_SRC_STOP = 385,          //Ai, Ao, Cnt, Tmソフトウェア停止信号
    }

    public enum DeviceType
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
    }
    public enum DioParameter
    {
        //-------------------------------------------------
        //	Parameters
        //-------------------------------------------------
        //	I/O(for Sample)
        DIO_MAX_ACCS_PORTS = 256,
        //	DioNotifyInt:Logic
        DIO_INT_NONE = 0,
        DIO_INT_RISE = 1,
        DIO_INT_FALL = 2,
    }
    public enum DioTrigger
    {
        //	DioNotifyTrg:TrgKind
        DIO_TRG_RISE = 1,
        DIO_TRG_FALL = 2,
    }
    public enum DioMessage
    {
        //	Message
        DIOM_INTERRUPT = 0x1300,
        DIOM_TRIGGER = 0x1340,
        DIO_DMM_STOP = 0x1350,
        DIO_DMM_COUNT = 0x1360,
    }
    public enum DioInfo
    {
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
    }
    public enum DioDefinition
    {
        //	Direction
        PI_32 = 1,
        PO_32 = 2,
        PIO_1616 = 3,
    }
    public enum DioDirection
    {
        DIODM_DIR_IN = 0x1,
        DIODM_DIR_OUT = 0x2,
    }
    public enum DioStartTrigger
    {
        //  Start
        DIODM_START_SOFT = 1,
        DIODM_START_EXT_RISE = 2,
        DIODM_START_EXT_FALL = 3,
        DIODM_START_PATTERN = 4,
        DIODM_START_EXTSIG_1 = 5,
        DIODM_START_EXTSIG_2 = 6,
        DIODM_START_EXTSIG_3 = 7,
    }
    public enum DioClock
    {
        //  Clock
        DIODM_CLK_CLOCK = 1,
        DIODM_CLK_EXT_TRG = 2,
        DIODM_CLK_HANDSHAKE = 3,
        DIODM_CLK_EXTSIG_1 = 4,
        DIODM_CLK_EXTSIG_2 = 5,
        DIODM_CLK_EXTSIG_3 = 6,
    }
    public enum DioTimerUnit
    {
        //  Internal Clock
        DIODM_TIM_UNIT_S = 1,
        DIODM_TIM_UNIT_MS = 2,
        DIODM_TIM_UNIT_US = 3,
        DIODM_TIM_UNIT_NS = 4,
    }
    public enum DioStopTrigger
    {
        //  Stop
        DIODM_STOP_SOFT = 1,
        DIODM_STOP_EXT_RISE = 2,
        DIODM_STOP_EXT_FALL = 3,
        DIODM_STOP_NUM = 4,
        DIODM_STOP_EXTSIG_1 = 5,
        DIODM_STOP_EXTSIG_2 = 6,
        DIODM_STOP_EXTSIG_3 = 7,
    }
    public enum DioExternalSignal
    {
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
    }
    public enum DioDMStatus
    {
        //	Status
        DIODM_STATUS_BMSTOP = 0x1,
        DIODM_STATUS_PIOSTART = 0x2,
        DIODM_STATUS_PIOSTOP = 0x4,
        DIODM_STATUS_TRGIN = 0x8,
        DIODM_STATUS_OVERRUN = 0x10,
    }
    public enum DioDMError
    {
        //	Error
        DIODM_STATUS_FIFOEMPTY = 0x1,
        DIODM_STATUS_FIFOFULL = 0x2,
        DIODM_STATUS_SGOVERIN = 0x4,
        DIODM_STATUS_TRGERR = 0x8,
        DIODM_STATUS_CLKERR = 0x10,
        DIODM_STATUS_SLAVEHALT = 0x20,
        DIODM_STATUS_MASTERHALT = 0x40,
    }
    public enum DioDMControl
    {
        //	Reset
        DIODM_RESET_FIFO_IN = 0x02,
        DIODM_RESET_FIFO_OUT = 0x04,
    }
    public enum DioDMMode
    {
        //	Buffer Ring
        DIODM_WRITE_ONCE = 0,
        DIODM_WRITE_RING = 1
    }
}
