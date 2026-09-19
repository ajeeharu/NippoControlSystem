using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class MeasureCondition
    {
        TimeSpan m_measElapsedTime = TimeSpan.FromSeconds(0);
        DateTime m_measDateTime = DateTime.MinValue;
        static Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();

        public enum enumInspectStat
        {
            Stat_STOP = 0,       //停止:実行前
            Stat_NormalEND,      //検査終了（正常終了）
            Stat_FailEND,        //検査停止（手動停止）、 異常停止
            Stat_ForceToEnd,     //強制完全
            Stat_MidStarted,     //途中開始
            Stat_NormalStart,    //通常検査
            Stat_TimeOut,        //timeout
            Stat_ManualEND,      //手動停止 20180906 Stat_FailENDと分ける
        }
        string[] m_enumStatString = new string[] { "停  止", "検査終了", "検査停止", "強制完全", "途中開始", "検査中", "timeout", "中  止" };
        string[] m_enumStartStatString = new string[] { "停  止", "検査終了", "検査停止", "強制完全", "途中開始", "通常検査", "timeout", "中  止" }; 

        //Save/Restore
        string m_MeasName = null;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //メンバ変数：設定格納変数
        static MeasureCondition m_instance = null;
        static public MeasureCondition GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new MeasureCondition();
                m_instance.Load(Default.ApplicationFloder + Default.SettingsHolder + Default.MeasureConditionPath);
            }
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //コンストラクタ
        public MeasureCondition()
        {
            //// データテーブルの作成
            ////m_LumiDS = new LumiDS();
            //m_LumiDS = new System.Data.DataSet();
            //System.Data.DataTable dt = new LimiDS.LumiDTDataTable();
            //m_LumiDS.Tables.Add(dt);


            //DioStat Default.DioStatOld, Default.DioStat
            for (int i = 0; i < Default.DioStat.Length; i++)    //{ "oHi", "oGN", "oOP", "iHi", "iGN", "iOP", "" }
            {
                //新機種の状態
                this.dicDioStat.Add(Default.DioStat[i], i);
            }
            //DioStatOld Default.DioStatOld, Default.DioStat
            for (int i = 0; i < Default.DioStat.Length; i++)    //{ "oHi", "oGN", "oOP", "iHi", "iGN", "iOP", "" }-> { "ON", "OFF", "OFF", "ON", "OFF", "OFF", "" }
            {
                //新機種->旧機種の状態
                this.dicDioStatOld.Add(Default.DioStat[i], Default.DioStatOld[i]);
            }
            //旧機種からの変換用DO
            for (int i = 0; i < Default.DoStatOld.Length; i++)
            {
                //旧機種->新機種の状態
                if (i < Default.DoStatNew.Length)
                {
                    if (!this.dicDoStat.ContainsKey(Default.DoStatOld[i]))     //ON,OFF
                    {
                        this.dicDoStat.Add(Default.DoStatOld[i], Default.DoStatNew[i]);
                    }
                }
            }
            //旧機種からの変換用DI
            for (int i = 0; i < Default.DiStatOld.Length; i++)
            {
                //旧機種->新機種の状態
                if (i < Default.DiStatNew.Length)
                {
                    if (!this.dicDiStat.ContainsKey(Default.DiStatOld[i]))     //ON,OF
                    {
                        this.dicDiStat.Add(Default.DiStatOld[i], Default.DiStatNew[i]);
                    }
                }
            }
            //DioHistStat検査読込の状態 20161107
            for (int i = 0; i < Default.DioHistStat.Length; i++)
            {
                if (!this.dicDioHistStat.ContainsKey(Default.DioHistStat[i]))     //"OPEN", "GND", "HIGH", "ERR"
                {
                    this.dicDioHistStat.Add(Default.DioHistStat[i], Default.DioHistStat[i]);
                }
            }


            //Clear
            this.Clear();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //デコンストラクタ
        //public void Dispose()
        ~MeasureCondition()
        {
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //public void Save()
        public void Save()
        {
            //this.Save(this.m_FileName);
            this.Save(Default.ApplicationFloder + Default.SettingsHolder + Default.MeasureConditionPath);
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //public void Save()
        public void Save(string SaveFileName)
        {
            try
            {
                ////ArrayListに挿入できるオブジェクト型のType配列を作成
                //Type[] et = new Type[] { typeof(Measure) };

                //シリアライズする
                string serializFile = SaveFileName;
                //System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MeasureContainer), et);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MeasureCondition));

                using (System.IO.FileStream stream = new System.IO.FileStream(serializFile, System.IO.FileMode.Create))
                {
                    //this.m_MeasName = System.IO.Path.GetFileNameWithoutExtension(SaveFileName);
                    serializer.Serialize(stream, this);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ファイル保存中にエラーが発生しました。\n" + ex.Message, Default.ApplicationName);
            }
        }
        
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //public void Load()
        public MeasureCondition Load(string LoadFileName)
        {
            MeasureCondition new_instance;

            //string serializFile = string.Format(Cyc.IO.IniFileHandler.GetApplicationDataPath() + @"{0}.xml", RestoreFileName);
            //string serializFile = m_SaveSpecPath + @"\" + LoadFileName + @".spec";
            string serializFile = LoadFileName;
            if (System.IO.File.Exists(serializFile))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MeasureCondition));
                    using (System.IO.FileStream stream = new System.IO.FileStream(serializFile, System.IO.FileMode.Open))
                    {
                        new_instance = (MeasureCondition)serializer.Deserialize(stream);
                        //ファイル名を保存する
                        //new_instance.m_MeasName = System.IO.Path.GetFileNameWithoutExtension(LoadFileName);
                        this.Copy2Me(new_instance);

                        return m_instance;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("ファイル読込中にエラーが発生しました。\n" + ex.Message, Default.ApplicationName);
                }
                return null;

            }
            else
            {
                //m_instance = new MeasureCondition();
                ////ファイル名を保存する
                //m_instance.m_FileName = LoadFileName;
                return null;
            }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        //static class DeepCopyUtils
        //{
        //    public static object DeepCopy(this object target)
        //    {

        //        object result;
        //        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter b = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        //        System.IO.MemoryStream mem = new System.IO.MemoryStream();

        //        try
        //        {
        //            b.Serialize(mem, target);
        //            mem.Position = 0;
        //            result = b.Deserialize(mem);
        //        }
        //        finally
        //        {
        //            mem.Close();
        //        }

        //        return result;

        //    }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        public static MeasureCondition DeepCopy(MeasureCondition target)
        {
            if (target == null) return null;
            var xs = new XmlSerializer(typeof(MeasureCondition));
            using (var mem = new MemoryStream())
            {
                xs.Serialize(mem, target);
                mem.Position = 0;
                return (MeasureCondition)xs.Deserialize(mem);
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        public void Copy2Me(MeasureCondition mc)
        {
            this.m_MeasName = mc.m_MeasName;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //public void New()
        public static MeasureCondition New()
        {
            m_instance = new MeasureCondition();
            return m_instance;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------
        //Clear
        public void Clear()
        {
            this.m_InspecStat = enumInspectStat.Stat_STOP;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ DataSetTopMenu
        DataSetTopMenu m_DataSetTopMenu = null;
        public DataSetTopMenu DataSetTopMenu
        {
            get { return m_DataSetTopMenu; }
            set { m_DataSetTopMenu = value; }
        }

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ DataSetTopMenuEdit
        //DataSetTopMenu m_DataSetTopMenuEdit = null;
        //public DataSetTopMenu DataSetTopMenuEdit
        //{
        //    get { return m_DataSetTopMenuEdit; }
        //    set { m_DataSetTopMenuEdit = value; }
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ DataSetItems
        //DataSetItems m_DataSetItems = null;
        //public DataSetItems DataSetItems
        //{
        //    get { return m_DataSetItems; }
        //    set { m_DataSetItems = value; }
        //}

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ listInspectItems
        //List<string> m_listInspectItems = null;
        //public List<string> listInspectItems
        //{
        //    get { return m_listInspectItems; }
        //    set { m_listInspectItems = value; }
        //}

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ listInspectItemsType
        List<string> m_listInspectItemsType = null;
        public List<string> listInspectItemsType
        {
            get { return m_listInspectItemsType; }
            set { m_listInspectItemsType = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_dicDoStat
        Dictionary<string, string> m_dicDoStat = new Dictionary<string, string>();
        public Dictionary<string, string> dicDoStat
        {
            get { return m_dicDoStat; }
            set { m_dicDoStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_dicDiStatt
        Dictionary<string, string> m_dicDiStat = new Dictionary<string, string>();
        public Dictionary<string, string> dicDiStat
        {
            get { return m_dicDiStat; }
            set { m_dicDiStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_dicDoStat
        Dictionary<string, int> m_dicDioStat = new Dictionary<string, int>();
        public Dictionary<string, int> dicDioStat
        {
            get { return m_dicDioStat; }
            set { m_dicDioStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_dicDioStatOld
        Dictionary<string, string> m_dicDioStatOld = new Dictionary<string, string>();
        public Dictionary<string, string> dicDioStatOld
        {
            get { return m_dicDioStatOld; }
            set { m_dicDioStatOld = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_dicDioHistStat
        Dictionary<string, string> m_dicDioHistStat = new Dictionary<string, string>();
        public Dictionary<string, string> dicDioHistStat
        {
            get { return m_dicDioHistStat; }
            set { m_dicDioHistStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_InspecStat
        enumInspectStat m_InspecStat = new enumInspectStat();
        public enumInspectStat InspecStat
        {
            get { return m_InspecStat; }
            set { m_InspecStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ m_InspecStat
        enumInspectStat m_InspectStartStat = new enumInspectStat();
        //検査の開始条件を保存、検査開始時にm_InspectStartStat←m_InspecStatする。
            //Stat_STOP = 0,       //停止
            //Stat_FailEND,        //検査停止 異常停止
            //Stat_ForceToEnd,     //強制完全
            //Stat_NormalStart,    //通常検査
            //Stat_MidStarted,     //途中開始
            //Stat_TestStarted,    //検査中

        public enumInspectStat InspecStartStat
        {
            get { return m_InspectStartStat; }
            set { m_InspectStartStat = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ InspecStatString
        public string InspecStatString
        {
            get
            {
                return m_enumStatString[(int)m_InspecStat];
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ InspecStatString
        public string InspecStartStatString
        {
            get
            {
                return m_enumStartStatString[(int)m_InspectStartStat];
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        int m_TestStepNo = 0;      //テスト実行のステップ：
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int TestStepNo
        {
            get { return this.m_TestStepNo; }
            set { this.m_TestStepNo = value; }
        }


        //--------1---------2---------3---------4---------5---------6---------7---------8
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int DioReadStatus     //DioReadStatus
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int DioWriteStatus     //WriteStatus
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:製造番号：以下は結果保存frmSerial.csでのみ使用
        string m_SerialNo = null;
        public string SerialNo
        {
            get { return m_SerialNo; }
            set { m_SerialNo = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:号機番号
        string m_GoNo = null;
        public string GoNo
        {
            get { return m_GoNo; }
            set { m_GoNo = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:仕様書番号
        string m_Zuban = null;
        public string Zuban
        {
            get { return m_Zuban; }
            set { m_Zuban = value; }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:追番
        string m_Edaban = null;
        public string Edaban
        {
            get { return m_Edaban; }
            set { m_Edaban = value; }
        }
        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ:フォルダ：以下は結果保存frmSerial.csでのみ使用：ここまで
        //string m_Folder = null;
        //public string Folder
        //{
        //    get { return m_Folder; }
        //    set { m_Folder = value; }
        //}
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:開始日付時刻
        DateTime m_TestStartDT = DateTime.MinValue;
        public DateTime TestStartDT
        {
            get { return m_TestStartDT; }
            set { m_TestStartDT = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:終了日付時刻
        DateTime m_TestEndDT = DateTime.MinValue;
        public DateTime TestEndDT
        {
            get { return m_TestEndDT; }
            set { m_TestEndDT = value; }
        }
        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ:CheckdatFile
        //string m_CheckdatFile = null;
        //public string CheckdatFile
        //{
        //    get { return m_CheckdatFile; }
        //    set { m_CheckdatFile = value; }
        //}
        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ:ListdatFile
        //string m_ListdatFile = null;
        //public string ListdatFile
        //{
        //    get { return m_ListdatFile; }
        //    set { m_ListdatFile = value; }
        //}
        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ:PortdatFile
        //string m_PortdatFile = null;
        //public string PortdatFile
        //{
        //    get { return m_PortdatFile; }
        //    set { m_PortdatFile = value; }
        //}
        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ:HistdatFile
        //string m_HistdatFile = null;
        //public string HistdatFile
        //{
        //    get { return m_HistdatFile; }
        //    set { m_HistdatFile = value; }
        //}
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:GreenSwitchStat
        int m_GreenSwitchStat = 0;
        public int GreenSwitchStat
        {
            get { return m_GreenSwitchStat; }
            set { m_GreenSwitchStat = value; }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ:GreenSwitchStat
        int m_RedSwitchStat = 0;
        public int RedSwitchStat
        {
            get { return m_RedSwitchStat; }
            set { m_RedSwitchStat = value; }
        }

    }
}
