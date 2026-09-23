#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.Infrastructure.Services
{
    public class Log
    {
        public enum LogLevel
        {
            //#define KERN_EMERG    "<0>"  /* システムが使用不能         */
            //#define KERN_ALERT    "<1>"  /* 直ちに対処が必要            */
            //#define KERN_CRIT     "<2>"  /* 致命的な状態                  */
            //#define KERN_ERR      "<3>"  /* エラー状態                     */
            //#define KERN_WARNING  "<4>"  /* 警告状態                        */
            //#define KERN_NOTICE   "<5>"  /* 通常状態だが大事な情報           */
            //#define KERN_INFO     "<6>"  /* 通知                              */
            //#define KERN_DEBUG    "<7>"  /* デバッグレベルの情報        */
            LOG_EMERG = 0,
            LOG_ALERT,
            LOG_CRIT,
            LOG_ERR,
            LOG_WARNING,
            LOG_NOTICE,
            LOG_INFO,
            LOG_DEBUG,
        }
        //
        static LogLevel m_LoggingLevel = 0;
        static string? LoggingOnStr = null;
        static string? LogFilePath = null;
        static string? LogSubFolder = null;
        static int lastLogFileDay = 0;

        //現在実行しているAssemblyを取得する
        //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        static System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();

        static public void WriteLine(LogLevel iLoggingLevel, string moduleName, string msg)
        {
            if ((int)m_LoggingLevel >= (int)iLoggingLevel)
            {
                setDefaultTraceListener1();
                System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff") + " " + iLoggingLevel.ToString() + " " + moduleName + " " + msg);
            }
        }

        //static public void WriteLine(LogLevel iLoggingLevel, string moduleName, string msg)
        //{
        //    WriteLine((int)iLoggingLevel, moduleName, msg);
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //LoggingLevel
        static public LogLevel LoggingLevel
        {
            get { return m_LoggingLevel; }
            set
            {
                m_LoggingLevel = value;
                setDefaultTraceListener();
            }
        }

        static public string GetSettingPath()
        {
            //string path = Path.Combine(
            //    Environment.GetFolderPath(
            //        Environment.SpecialFolder.ApplicationData),
            //    Application.CompanyName + "\\" + Application.ProductName +
            ////    "\\" + Application.ProductName + ".config");
            //string path = GetApplicationDataPath();
            //string path = @"D:\Host\Settings\Default.config";
            string path = System.Configuration.ConfigurationManager.AppSettings["Default.config Path"];
            return path;
            //            this.Text = Properties.Settings.Default.Title;

        }

        static public string GetApplicationDataPath()
        {
            //string DataPath = Properties.Settings.Default.CycIOLogDataPath;
            string DataPath = System.Configuration.ConfigurationManager.AppSettings["Cyc.IO.Log.DataPath"];
            //20101027 Windows7対応。Windows7/WindowsServer2008以降は、データをApplicationDataに作成する。
            //Version.Major 
            //3 : Windows NT 3 ~ Windows NT 3.51
            //4 : Windows NT 4.0
            //5 : Windows 2000 ,Windows XP, Windows Server 2003
            //6 : Windows Vista, Windows Server 2008, Windows 7, Windows Server 2008 R2
            if (string.IsNullOrEmpty(DataPath) && System.Environment.OSVersion.Platform == System.PlatformID.Win32NT && System.Environment.OSVersion.Version.Major > 4)
            {
                DataPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                DataPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            }

            //FolderNameは、Exe名のexeを取った名前とする
            DataPath += (System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));

            //該当のフォルダがなければ作る
            if (!System.IO.Directory.Exists(DataPath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(DataPath);
                }
                catch
                {
                    throw;
                }
            }
            return DataPath;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //DefaultTraceListenerの設定
        static public void setDefaultTraceListener()
        {
            if (LoggingOnStr == null)
            {
                //LogSubFolder = Properties.Settings.Default.CycIOLogSubFolder;//CycIOLogSubFolder
                LogSubFolder = System.Configuration.ConfigurationManager.AppSettings["Cyc.IO.Log.SubFolder"]; //CycIOLogSubFolder
                if (string.IsNullOrEmpty(LogSubFolder))
                {
                    LogSubFolder = "LOGFILE";
                }
                //LoggingOnStr = Properties.Settings.Default.CycIOLogLoggingLevel;//CycIOLogLoggingLevel
                LoggingOnStr = System.Configuration.ConfigurationManager.AppSettings["Cyc.IO.Log.LoggingLevel"];//CycIOLogLoggingLevel
                if (!LogLevel.TryParse(LoggingOnStr, out m_LoggingLevel))
                {
                    LoggingOnStr = "LOG_INFO";
                    m_LoggingLevel = LogLevel.LOG_INFO;
                }
                LogFilePath = GetApplicationDataPath() + System.IO.Path.DirectorySeparatorChar + LogSubFolder;
            }

            if (m_LoggingLevel != LogLevel.LOG_EMERG)
            {
                //if (!System.IO.Path.IsPathRooted(LogFilePath))
                //    LogSubFolder = GetApplicationDataPath() + LogSubFolder + System.IO.Path.DirectorySeparatorChar;
                if (!System.IO.Directory.Exists(LogFilePath))
                {
                    System.IO.Directory.CreateDirectory(LogFilePath);
                }
            }

            setDefaultTraceListener1();

        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //DefaultTraceListenerの設定
        static private void setDefaultTraceListener1()
        {
            if (m_LoggingLevel != 0)
            {
                //ログファイルを記録する。
                if (lastLogFileDay != System.DateTime.Now.Day)
                {
                    lastLogFileDay = System.DateTime.Now.Day;
                    //Iniファイルから、LogFilePathを読み出す
                    string NewLogFile = LogFilePath + System.IO.Path.DirectorySeparatorChar + @"log" + lastLogFileDay.ToString("00") + ".log";
                    //日付により、ファイルを削除
                    if (System.IO.File.Exists(NewLogFile))
                    {
                        if ((DateTime.Now - System.IO.File.GetCreationTime(NewLogFile)) > new TimeSpan(24, 0, 0))
                        {
                            //24時間以上前なら、ファイルを削除
                            System.IO.File.Delete(NewLogFile);
                        }
                    }
                    //string FullPath = System.IO.Path.GetFullPath(NewLogFile);
                    //DefaultTraceListenerオブジェクトを取得
                    System.Diagnostics.DefaultTraceListener drl = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Trace.Listeners["Default"];
                    //System.Diagnostics.Debug.WriteLine("setDefaultTraceListener " + drl.LogFileName);
                    //LogFileNameを変更する
                    drl.LogFileName = NewLogFile;
                    //Startメッセージを書き込む
                    WriteLine(LogLevel.LOG_ALERT, asm.GetName().Name, "Program Start");
                }
            }
        }

        static public int PurgeLogFiles()
        {
            int returnStatus = 1;

            //string FullPath = System.IO.Path.GetFullPath(NewLogFile);
            //DefaultTraceListenerオブジェクトを取得
            System.Diagnostics.DefaultTraceListener drl = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Trace.Listeners["Default"];
            //System.Diagnostics.Debug.WriteLine("setDefaultTraceListener " + drl.LogFileName);
            //LogFileNameを変更する
            drl.LogFileName = null;

            //ログファイルを削除する。
            if (System.IO.Directory.Exists(LogFilePath))
            {
                foreach (string file in System.IO.Directory.GetFiles(LogFilePath, "*.log"))
                {
                    System.IO.FileInfo cFileInfo = new System.IO.FileInfo(file);
                    // 読み取り専用属性がある場合は、読み取り専用属性を解除する
                    if ((cFileInfo.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                    {
                        cFileInfo.Attributes = System.IO.FileAttributes.Normal;
                    }

                    // ファイルを削除する
                    cFileInfo.Delete();
                }
                System.IO.Directory.Delete(LogFilePath);
            }
            //PurgeLogFiles以降はログしない
            m_LoggingLevel = 0;

            return returnStatus;
        }
    }
}
