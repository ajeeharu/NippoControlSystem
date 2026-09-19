using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class MeasureCondition
    {
        //CSVファイルに書き込むときに使うEncoding
        public System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
        public string Part = ".part";
        //loadListdatFileで、iDsが空欄のとき、これを使う
        string CheckDatiDsLo = null;
        string CheckDatiDsHi = null;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ isCsvHeaderRead
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool isCsvHeaderRead
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveResultCsv
        public void saveResultCsv(DataSetItems myDataSetItems, string Folder)
        {
            DateTime dt = this.TestEndDT;
            string msgLine = "";
            string resultCsvPass = string.Format("{0}{1:yyyy}-log.csv", Folder + System.IO.Path.DirectorySeparatorChar, dt);
            //string resultListPass = string.Format("{0}A{1:yyyyMMdd_HHmmss}.dat", Folder + System.IO.Path.DirectorySeparatorChar, dt);
            string resultListPass = string.Format("{0}A{1:yyyyMMdd_HHmmss}.csv", Folder + System.IO.Path.DirectorySeparatorChar, dt);   //20180827 拡張子をcsvに変更

            try
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(resultCsvPass, true, enc))   //allways append
                {
                    msgLine = EncloseDoubleQuotes(string.Format("{0:yyyy.MM.dd HH:mm:ss}", dt)) + ",";
                    msgLine += EncloseDoubleQuotes(this.SerialNo) + ",";
                    msgLine += EncloseDoubleQuotes(this.GoNo) + ",";
                    msgLine += EncloseDoubleQuotes(this.Zuban) + ",";
                    msgLine += EncloseDoubleQuotes(this.Edaban) + ",";
                    msgLine += EncloseDoubleQuotes(this.InspecStartStatString) + ",";
                    msgLine += EncloseDoubleQuotes(this.InspecStat == enumInspectStat.Stat_NormalEND ? "Pass" : "Fail");
                    sr.WriteLine(msgLine);
                    //sr.WriteLine("");       //空行
                }
                //ListDatResult
                //this.saveInspectItem(myDataSetItems.ListDatResult, resultListPass, false);
                this.saveInspectItemWithResult(myDataSetItems.ListDatResult, resultListPass);

                //File path
                string CheckdatFile = Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
                string ListdatFile = Folder + System.IO.Path.DirectorySeparatorChar + Default.Listdat;
                string PortdatFile = Folder + System.IO.Path.DirectorySeparatorChar + Default.Portdat;

                //Add 
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(resultListPass, true, enc))   //allways append
                {
                    //Add ListDat
                    if (!string.IsNullOrEmpty(ListdatFile) && System.IO.File.Exists(ListdatFile))
                    {
                        //string stBuffer = System.IO.File.ReadAllText(ListdatFile, enc);   20160915
                        string stBuffer = this.ReadAllText(ListdatFile, enc);
                        sr.WriteLine();
                        sr.Write(stBuffer);
                    }
                    //Add PortdatFile
                    if (!string.IsNullOrEmpty(PortdatFile) && System.IO.File.Exists(PortdatFile))
                    {
                        //string stBuffer = System.IO.File.ReadAllText(PortdatFile, enc);   20160915
                        string stBuffer = this.ReadAllText(PortdatFile, enc);
                        sr.WriteLine();
                        sr.Write(stBuffer);
                    }
                    //Add CheckdatFile
                    if (!string.IsNullOrEmpty(CheckdatFile) && System.IO.File.Exists(CheckdatFile))
                    {
                        //string stBuffer = System.IO.File.ReadAllText(CheckdatFile, enc);  20160915
                        string stBuffer = this.ReadAllText(CheckdatFile, enc);
                        sr.WriteLine();
                        sr.Write(stBuffer);
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception 20180931
                System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadMenuCsv
        public void loadMenuCsv(DataSetTopMenu DataSetTopMenu, string menuCsvPass)
        {
            if (!string.IsNullOrEmpty(menuCsvPass) && System.IO.File.Exists(menuCsvPass))
            {
                DataTable dtMain = DataSetTopMenu.menuMain;
                dtMain.Rows.Clear();
                DataTable dtSubMain = DataSetTopMenu.menuSub;
                dtSubMain.Rows.Clear();
                int ID = 0;
                //int SubID = 0;

                //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(menuCsvPass);
                //System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
                //using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
                //{
                //    // 読み込みできる文字がなくなるまで繰り返す
                //    while (!cReader.EndOfStream)
                //    { }
                //}
                // ファイルを 全行読み込む
                string stBuffer = this.ReadAllText(menuCsvPass, enc);
                if (stBuffer == null) return;
                // CSVを展開する。
                List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                for (int i = 0; i < csvRecords.Count; i++)
                {
                    //SubID = 0;
                    List<string> csvFields = csvRecords[i];

                    //Fields数は奇数であること
                    if (csvFields.Count % 2 == 1)
                    {
                        // 読み込んだものを追加で格納する
                        DataRow dtMainRow = dtMain.NewRow();
                        //dtMainRow["MainID"] = ID;
                        dtMainRow["Title"] = csvFields[0];
                        dtMain.Rows.Add(dtMainRow);

                        for (int j = 1; j < csvFields.Count; j += 2)
                        {
                            // 読み込んだものを追加で格納する
                            DataRow dtSubMainRow = dtSubMain.NewRow();
                            //dtSubMainRow["MainID"] = ID;
                            dtSubMainRow["MainID"] = dtMain.Rows[dtMain.Rows.Count - 1]["MainID"];//MainIDに連動させる
                            dtSubMainRow["SubTitle"] = csvFields[j];
                            dtSubMainRow["Folder"] = csvFields[j + 1];
                            dtSubMain.Rows.Add(dtSubMainRow);
                        }
                    }
                }
                ID++;

                dtMain.AcceptChanges();
                dtSubMain.AcceptChanges();
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveMenuCsv
        public void saveMenuCsv(DataSetTopMenu DataSetTopMenu, string menuCsvPass)
        {
            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSubMain = DataSetTopMenu.menuSub;
            int MainID = 0;
            int SubID = 0;
            string msgLine = "";

            try
            {
                dtMain.AcceptChanges(); //20160915
                dtSubMain.AcceptChanges(); //20160915
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(menuCsvPass, false, enc))   //no append
                {
                    foreach (DataRow dtMainRow in dtMain.Rows)
                    {
                        msgLine = EncloseDoubleQuotesIfNeed(dtMainRow["Title"].ToString());
                        MainID = int.Parse(dtMainRow["MainID"].ToString());
                        //foreach (DataRow dtSubRow in dtSubMain.Select(string.Format("MainID='{0}'", MainID)))
                        //{
                        //    SubID = int.Parse(dtSubRow["SubID"].ToString());
                        //    msgLine += "," + EncloseDoubleQuotesIfNeed(dtSubRow["SubTitle"].ToString()) + "," + EncloseDoubleQuotesIfNeed(dtSubRow["Folder"].ToString());
                        //}
                        for (int i = 0; i < dtSubMain.Rows.Count; i++)  //20170908-1
                        {
                            if (dtSubMain.Rows[i]["MainID"].ToString() == MainID.ToString())
                            {
                                SubID = int.Parse(dtSubMain.Rows[i]["SubID"].ToString());
                                msgLine += "," + EncloseDoubleQuotesIfNeed(dtSubMain.Rows[i]["SubTitle"].ToString()) + "," + EncloseDoubleQuotesIfNeed(dtSubMain.Rows[i]["Folder"].ToString());
                            }
                        }
                        sr.WriteLine(msgLine);
                        //sr.WriteLine("");       //空行
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception 20180931
                System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }

        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        //public void loadListdatFile(DataSetItems DataSetItems, string ListdatFile)
        public void loadListdatFile(DataTable ListDat, string ListdatFile)
        {
            loadListdatFile(ListDat, ListdatFile, false);   //2パラのときは、通常（Histではない）
        }
        public void loadListdatFile(DataTable ListDat, string ListdatFile, bool HistView)
        {
            if (!string.IsNullOrEmpty(ListdatFile) && System.IO.File.Exists(ListdatFile))
            {
                //DataTable dtItem = DataSetItems.ListDat;
                ListDat.Rows.Clear();
                int ID = 0;
                string fieldData = null;
                string fieldDataNew = null;
                //bool firstLine = true;
                //string stBuffer = null; //制御文字(crとか)取り除くため、一旦すべて読み込む20160914

                //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(ListdatFile);
                //System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
                //using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
                //{
                //    // 読み込みできる文字がなくなるまで繰り返す
                //    while (!cReader.EndOfStream)
                //    {
                //        // ファイルを 1 行読み込む
                //        stBuffer += cReader.ReadLine() + "\n";  //20160914
                //    }
                //}

                System.Diagnostics.Debug.WriteLine("loadListdatFile ListdatFile:" + ListdatFile.ToString());
                // ファイルを 全行読み込む
                string stBuffer = this.ReadAllText(ListdatFile, enc);

                // CSVを展開する。
                List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                for (int i = 0; i < csvRecords.Count; i++)
                {
                    List<string> csvFields = csvRecords[i];

                    //Fields数は奇数であること
                    if (csvFields.Count >= 277)
                    {
                        // 読み込んだものを追加で格納する
                        DataRow dtItemRow = ListDat.NewRow();
                        int idx = 0;
                        dtItemRow["Title"] = csvFields[idx++];
                        dtItemRow["Guide"] = csvFields[idx++].Replace("\\n", Environment.NewLine);
                        dtItemRow["Type"] = csvFields[idx++];
                        dtItemRow["Result"] = null;
                        //DIO
                        for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                        {
                            for (int j = 0; j < Default.DioNums[ii]; j++)       //{ 32, 32, 32, 32, 32, 32, 32, 32 }
                            {
                                //if (HistView && i == 1)
                                //{
                                //    System.Diagnostics.Debug.WriteLine(string.Format("loadListdatFile: {0}:{1}", idx, csvFields[idx]));
                                //}

                                int jj = Default.DioNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                                fieldData = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない

                                if (!HistView)
                                {
                                    //新機種の状態かを確認
                                    if (dicDioStat.ContainsKey(fieldData))
                                    {
                                        fieldDataNew = fieldData;
                                    }
                                    else if (dicDioHistStat.ContainsKey(fieldData))    //20161107 検査結果ならそのまま
                                    {
                                        fieldDataNew = fieldData;
                                    }
                                    else
                                    {
                                        //旧機種のデータを新機種に変更
                                        if (ii < 4)
                                        {
                                            //DO
                                            if (dicDoStat.ContainsKey(fieldData))
                                            {
                                                // 存在したら
                                                fieldDataNew = dicDoStat[fieldData];
                                            }
                                            else
                                            {
                                                // 存在しない場合
                                                fieldDataNew = "";
                                            }

                                        }
                                        else
                                        {
                                            //DI
                                            if (dicDiStat.ContainsKey(fieldData))
                                            {
                                                // 存在したら
                                                fieldDataNew = dicDiStat[fieldData];
                                            }
                                            else
                                            {
                                                // 存在しない場合
                                                fieldDataNew = "";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //20180731 HistViewのときは、fieldDataをチェックしない
                                    fieldDataNew = fieldData;
                                }
                                dtItemRow[string.Format("{0}-{1:00}", Default.DioNames[ii], jj)] = fieldDataNew;  //DataSetは名前参照なので順序は影響ない
                            }
                        }
                        //AO
                        for (int ii = 0; ii < Default.AoNum; ii++)     // "AO"
                        {
                            int jj = Default.AoNum - ii;                           //データが逆順に並んでいるので（2→1）
                            dtItemRow[string.Format("{0}-{1:0}", Default.AoName, jj)] = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                        }
                        //AI
                        for (int ii = 0; ii < Default.AiNum; ii++)     // "AI"
                        {
                            int jj = Default.AiNum - ii;                           //データが逆順に並んでいるので（8→1）
                            dtItemRow[string.Format("{0}-{1:0}L", Default.AiName, jj)] = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                            dtItemRow[string.Format("{0}-{1:0}H", Default.AiName, jj)] = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                        }
                        if (csvFields.Count >= 285)
                        {
                            //AOSw
                            for (int ii = 0; ii < Default.AoSwichNum; ii++)     // "AoSw"
                            {
                                int jj = Default.AoSwichNum - ii;                           //データが逆順に並んでいるので（2→1）
                                dtItemRow[string.Format("{0}-{1:0}", Default.AoSwichName, jj)] = csvFields[idx++] == Default.AoSwichStat[0] ? Default.AoSwichStat[0] : Default.AoSwichStat[1];  //DataSetは名前参照なので順序は影響ない
                            }
                            if (csvFields.Count >= 286)
                            {
                                dtItemRow["Result"] = csvFields[idx++]; //Result
                            }
                        }
                        //Lmt High/Low 20180719
                        //Fields数は奇数であること
                        if (csvFields.Count >= 799)
                        {
                            fieldData = csvFields[idx++];  //iDs-HiLMT
                            if (!string.IsNullOrEmpty(fieldData))
                            {
                                dtItemRow["iDs-HiLMT"] = fieldData;  //DataSetは名前参照なので順序は影響ない
                            }
                            else
                            {
                                dtItemRow["iDs-HiLMT"] = CheckDatiDsHi; //20180801-2
                            }
                            fieldData = csvFields[idx++];  //iDs-LoLMT
                            if (!string.IsNullOrEmpty(fieldData))
                            {
                                dtItemRow["iDs-LoLMT"] = fieldData;  //DataSetは名前参照なので順序は影響ない
                            }
                            else
                            {
                                dtItemRow["iDs-LoLMT"] = CheckDatiDsLo; //20180801-2
                            }

                            // 読み込んだものを追加で格納する
                            //LMT Low/High
                            for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                            {
                                for (int j = 0; j < Default.DioNums[ii]; j++)       //{ 32, 32, 32, 32, 32, 32, 32, 32 }
                                {
                                    int jj = Default.DioNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                                    fieldData = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                                    //if (firstLine) System.Diagnostics.Debug.WriteLine(string.Format("saveInspectItem:ColumnName[{0}] = {1} ... {2}", idx, string.Format("{0}-{1:00}L", Default.DioNames[ii], jj), fieldData));   //for Debug
                                    //firstLine = false;
                                    if (!string.IsNullOrEmpty(fieldData))
                                    {
                                        dtItemRow[string.Format("{0}-{1:00}H", Default.DioNames[ii], jj)] = fieldData;  //DataSetは名前参照なので順序は影響ない
                                    }
                                    fieldData = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                                    if (!string.IsNullOrEmpty(fieldData))
                                    {
                                        dtItemRow[string.Format("{0}-{1:00}L", Default.DioNames[ii], jj)] = fieldData;  //DataSetは名前参照なので順序は影響ない
                                    }
                                }
                            }
                        }
                        ListDat.Rows.Add(dtItemRow);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("csvFields.Count error:{0}", csvFields.Count));
                    }
                }
                ID++;
                ListDat.AcceptChanges();
            }
            else
            {
                //ファイルがないときは、Itemをクリア
                ListDat.Rows.Clear();
                //空行を追加
                DataRow dtItemRow = ListDat.NewRow();
                dtItemRow["Type"] = 0;
                ListDat.Rows.Add(dtItemRow);
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method checkChangedInspectItem
        public bool checkChangedInspectItem(DataTable ListDat, string ListdatFile)
        {
            bool withResult = false;
            bool checkChanged = true;
            return saveInspectItem(ListDat, ListdatFile, withResult, checkChanged);
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveInspectItem
        public bool saveInspectItem(DataTable ListDat, string ListdatFile)
        {
            bool withResult = false;
            bool checkChanged = false;
            return saveInspectItem(ListDat, ListdatFile, withResult, checkChanged);
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveInspectItem
        public bool saveInspectItemWithResult(DataTable ListDat, string ListdatFile)
        {
            bool withResult = true;
            bool checkChanged = false;
            return saveInspectItem(ListDat, ListdatFile, withResult, checkChanged);
        }
        
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveInspectItem
        public bool saveInspectItem(DataTable ListDat, string ListdatFile, bool withResult, bool checkChanged)
        {
            bool retuenValue = false;   //20170216
            //DataTable dtItem = DataSetItems.ListDat;
            string msgLine = "";
            string strField = "";
            ListDat.AcceptChanges();    //20160915
            //bool firstLine = true;

            try
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(ListdatFile + Part, false, enc))   //no append
                {
                    foreach (DataRow dtItemnRow in ListDat.Rows)
                    {
                        msgLine = "";
                        for (int i = 0; i < ListDat.Columns.Count - 0; i++)
                        {
                            strField = dtItemnRow[i].ToString();
                            if (i == 1) strField = strField.Replace(Environment.NewLine, "\\n");
                            //if (!(ListDat.Columns[i].ColumnName == "InspectID" || (ListDat.Columns[i].ColumnName == "Result" && withoutResult)))
                            //if (!(ListDat.Columns[i].ColumnName == "InspectID" || (ListDat.Columns[i].ColumnName == "Result" && !withResult)))  //20160216-5
                            if (!(ListDat.Columns[i].ColumnName == "InspectID"))  //20180719
                            {
                                //if (firstLine) System.Diagnostics.Debug.WriteLine(string.Format("saveInspectItem:ColumnName[{0}] = {1} ", i, ListDat.Columns[i].ColumnName));
                                if (ListDat.Columns[i].ColumnName == "Result" && !withResult)
                                {
                                    strField = "";
                                }
                                if (i > 0) msgLine += ",";
                                //msgLine += EncloseDoubleQuotesIfNeed(strField);
                                msgLine += EncloseDoubleQuotes(strField);   //無条件にDoubleQuotesで囲う
                            }
                        }
                        sr.WriteLine(msgLine);
                        //sr.WriteLine("");       //空行
                        //System.Diagnostics.Debug.WriteLine(msgLine);
                        //firstLine = false;
                    }
                }
                //内容が異なるときのみ、ファイルを更新する
                if (System.IO.File.Exists(ListdatFile))
                {
                    if (checkChanged)
                    {
                        retuenValue = !isFileSame(ListdatFile + Part, ListdatFile);
                        //FileCompareが終わったので、新しい方を消す
                        System.IO.File.Delete(ListdatFile + Part);
                    }
                    else
                    {
                        //if (isFileSame(ListdatFile + Part, ListdatFile))
                        //{
                        //    //内容が同じときは、新しい方を消す
                        //    System.IO.File.Delete(ListdatFile + Part);
                        //    retuenValue = false;     //新しくファイルを生成しないときはfalse
                        //}
                        //else
                        //{
                        //内容が異なるときは、古い方を消して、リネームする
                        System.IO.File.Delete(ListdatFile);
                        System.IO.File.Move(ListdatFile + Part, ListdatFile);
                        retuenValue = true;     //新しくファイルを生成したときはtrue
                        //}
                    }
                }
                else
                {
                    if (checkChanged)
                    {
                        retuenValue = true;     //旧ファイルがないときは、常にtrue
                        //FileCompareが終わったので、新しい方を消す
                        System.IO.File.Delete(ListdatFile + Part);
                    }
                    else
                    {
                        //もともとのファイルがないときは、リネームのみする
                        System.IO.File.Move(ListdatFile + Part, ListdatFile);
                        retuenValue = true;     //新しくファイルを生成したときはtrue
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception 20180931
                System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return retuenValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        public bool clearListdatLMT(DataTable ListDat)
        {
#if zero
            string iFeildName;
            string iDioStat;
            for (int TNo = 0; TNo < ListDat.Rows.Count; TNo++)
            {
                DataRow dtListDatRow = ListDat.Rows[TNo];

                for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                {
                    //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                    for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                    {
                        int jj = j + 1;
                        iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                        iDioStat = dtListDatRow[iFeildName].ToString();
                        if (iDioStat == "iDp" || iDioStat == "nDp")
                        {
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(dtListDatRow[iFeildName + "L"].ToString()))
                            {
                                dtListDatRow[iFeildName + "L"] = null;
                            }
                            if (string.IsNullOrEmpty(dtListDatRow[iFeildName + "H"].ToString()))
                            {
                                dtListDatRow[iFeildName + "H"] = null;
                            }
                        }
                    }
                }
            }
#endif
            return true;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        public string convToOldVerListdat(string ListdatFile, string OldVerListdatFile)
        {
            int ID = 0;
            string fieldData = null;
            string fieldDataNew = null;
            string msgLine = "";
            string errmsg = "";
            int lineNo = 0;

            if (!string.IsNullOrEmpty(ListdatFile) && System.IO.File.Exists(ListdatFile))
            {
                //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(ListdatFile);
                //System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
                //using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
                //{
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(OldVerListdatFile + Part, false, enc))   //no append
                {
                    //// 読み込みできる文字がなくなるまで繰り返す
                    //while (!cReader.EndOfStream)
                    //{
                    //    // ファイルを 1 行読み込む
                    //    string stBuffer = cReader.ReadLine();
                    // ファイルを 全行読み込む
                    string stBuffer = this.ReadAllText(ListdatFile, enc);
                    // CSVを展開する。
                    List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                    for (int i = 0; i < csvRecords.Count; i++)   
                    {
                        List<string> csvFields = csvRecords[i];
                        msgLine = "";
                        lineNo++;

                        //Fields数は奇数であること
                        if (csvFields.Count >= 277)
                        {
                            // 読み込んだものを追加で格納する
                            int idx = 0;
                            msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ","; //Title
                            msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ","; //Guide
                            msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ","; //Type
                            //DIO
                            for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                            {
                                for (int j = 0; j < Default.DioNums[ii]; j++)       //{ 32, 32, 32, 32, 32, 32, 32, 32 }
                                {
                                    fieldData = csvFields[idx++];  //DataSetは名前参照なので順序は影響ない
                                    //新機種の状態かを確認
                                    if (fieldData == "")
                                    {
                                        fieldDataNew = "";
                                    }
                                    else if (dicDioStatOld.ContainsKey(fieldData))
                                    {
                                        //if (ii < 4)
                                        //{
                                        //    //OUT
                                        //    if (fieldData[0] == 'o')
                                        //    {
                                        //        fieldDataNew = dicDioStatOld[fieldData];
                                        //    }
                                        //    else
                                        //    {
                                        //        fieldDataNew = fieldData;   //eraor時はそのまま入れる
                                        //        errmsg += string.Format("T#:{0} {1}-{2} {3}:このPortは、出力のみ可能です。\n", lineNo, Default.DioNames[ii], 32 - j, fieldData);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    //IN
                                        //    if (fieldData[0] == 'i')
                                        //    {
                                        //        fieldDataNew = dicDioStatOld[fieldData];
                                        //    }
                                        //    else
                                        //    {
                                        //        fieldDataNew = fieldData;   //eraor時はそのまま入れる
                                        //        errmsg += string.Format("T#:{0} {1}-{2} {3}:このPortは、入力のみ可能です。\n", lineNo, Default.DioNames[ii], 32 - j, fieldData);
                                        //    }
                                        //}
                                        //キーがあればそのまま入れる 20180831
                                        fieldDataNew = dicDioStatOld[fieldData];
                                    }
                                    else if (dicDoStat.ContainsKey(fieldData) || dicDiStat.ContainsKey(fieldData))  //OldTypeが指定されたとき
                                    {
                                        fieldDataNew = fieldData;               //そのまま入れる
                                    }
                                    else
                                    {
                                        fieldDataNew = fieldData;               //eraor時はそのまま入れる
                                        errmsg += string.Format("T#:{0} {1}-{2} {3}：認識できない文字列が設定されています。\n", lineNo, Default.DioNames[ii], 32 - j, fieldData);
                                    }
                                    msgLine += EncloseDoubleQuotes(fieldDataNew) + ",";
                                }
                            }
                            //AO
                            for (int ii = 0; ii < Default.AoNum; ii++)     // "AO"
                            {
                                msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ",";  //string.Format("{0}-{1:0}", Default.AoName, jj)
                            }
                            //AI
                            for (int ii = 0; ii < Default.AiNum; ii++)     // "AI"
                            {
                                msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ",";  //string.Format("{0}-{1:0}L", Default.AiName, jj)
                                msgLine += EncloseDoubleQuotes(csvFields[idx++]) + ",";  //string.Format("{0}-{1:0}H", Default.AiName, jj)
                            }
                            sr.WriteLine(msgLine.Substring(0, msgLine.Length - 1));     //1号機は、最大286(3+256+2+8+16+1)

                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("csvFields.Count error:{0}", csvFields.Count));
                        }
                    }
                    ID++;
                }
            }
            //    }
            //}
            return errmsg;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        public string convToOldVerCheckdat(string CheckdatFile, string OldVerCheckdatFile)
        {
            int ID = 0;
            string msgLine = "";
            string errmsg = "";
            int lineNo = 0;

            if (!string.IsNullOrEmpty(CheckdatFile) && System.IO.File.Exists(CheckdatFile))
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(OldVerCheckdatFile + Part, false, enc))   //no append
                {
                    //// 読み込みできる文字がなくなるまで繰り返す
                    //while (!cReader.EndOfStream)
                    //{
                    //    // ファイルを 1 行読み込む
                    //    string stBuffer = cReader.ReadLine();
                    // ファイルを 全行読み込む
                    string stBuffer = this.ReadAllText(CheckdatFile, enc);
                    // CSVを展開する。
                    List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                    for (int i = 0; i < csvRecords.Count; i++)   //1号機は、最大3
                    {
                        List<string> csvFields = csvRecords[i];
                        msgLine = "";
                        lineNo++;

                        for (int iCol = 0; iCol < 4; iCol++)    //やっぱ４つ要るfor (int iCol = 0; iCol < 3; iCol++)
                        {
                            // 読み込んだものを追加で格納する
                            if (iCol == 3)     //20180914-2 電圧は、""で囲わない
                            {
                                msgLine += EncloseDoubleQuotesIfNeed(csvFields[iCol]) + ","; //idx→iCol 20180914-1
                            }
                            else
                            {
                                msgLine += EncloseDoubleQuotes(csvFields[iCol]) + ","; //idx→iCol 20180914-1
                            }
                        }
                        sr.WriteLine(msgLine.Substring(0, msgLine.Length - 1));
                        ID++;
                    }
                }
            }
            return errmsg;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        //public void loadPortdatFile(DataSetItems DataSetItems, string PortdatFile)
        public void loadPortdatFile(DataTable dtPortDat, string PortdatFile)
        {
            if (!string.IsNullOrEmpty(PortdatFile) && System.IO.File.Exists(PortdatFile))
            {
                //DataTable dtPortDat = DataSetItems.PortDat;
                dtPortDat.Rows.Clear();
                int ID = 0;

                //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(PortdatFile);
                //System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
                //using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
                //{
                //    // 読み込みできる文字がなくなるまで繰り返す
                //    while (!cReader.EndOfStream)
                //    {
                //        // ファイルを 1 行読み込む
                //        string stBuffer = cReader.ReadToEnd();
                // ファイルを 全行読み込む
                string stBuffer = this.ReadAllText(PortdatFile, enc);
                // CSVを展開する。
                List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                if (csvRecords.Count >= 11)
                {
                    int idx = 0;
                    List<string> csvFields;
                    // 読み込んだものを追加で格納する
                    DataRow dtItemRow = dtPortDat.NewRow();
                    //DIO
                    for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                    {
                        csvFields = csvRecords[idx++];
                        for (int j = 0; j < Default.DioNums[ii]; j++)       //{ 32, 32, 32, 32, 32, 32, 32, 32 }
                        {
                            int jj = Default.DioNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                            //dtItemRow[string.Format("{0}-{1:00}", Default.DioNames[ii], jj)] = csvFields[j];  //DataSetは名前参照なので順序は影響ない
                            dtItemRow[string.Format("{0}-{1:00}", Default.DioNames[ii], jj)] = suplessCTRN(csvFields[j]);  //DataSetは名前参照なので順序は影響ない,\nを削除する
                        }
                    }
                    //AO
                    csvFields = csvRecords[idx++];
                    for (int ii = 0; ii < System.Math.Max(Default.AoNum, csvFields.Count); ii++)     // "AO"
                    {
                        int jj = System.Math.Max(Default.AoNum, csvFields.Count) - ii;                           //データが逆順に並んでいるので（2→1）
                        dtItemRow[string.Format("{0}-{1:0}", Default.AoName, jj)] = suplessCTRN(csvFields[ii]);  //DataSetは名前参照なので順序は影響ない
                    }
                    //AI
                    csvFields = csvRecords[idx++];
                    for (int ii = 0; ii < Default.AiNum; ii++)     // "AI"
                    {
                        int jj = Default.AiNum - ii;                           //データが逆順に並んでいるので（8→1）
                        dtItemRow[string.Format("{0}-{1:0}", Default.AiName, jj)] = suplessCTRN(csvFields[ii]);  //DataSetは名前参照なので順序は影響ない
                    }
                    //GND
                    csvFields = csvRecords[idx++];
                    int nn = 0;
                    for (int ii = 0; ii < Default.GndNames.Length; ii++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
                    {
                        for (int j = 0; j < Default.GndNums[ii]; j++)       //{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                        {
                            int jj = Default.GndNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                            dtItemRow[string.Format("{0}-{1:0}", Default.GndNames[ii], jj)] = suplessCTRN(csvFields[nn++]);  //DataSetは名前参照なので順序は影響ない
                        }
                    }
                    //if (csvRecords.Count >= 12)
                    //{
                    //    csvFields = csvRecords[idx++];
                    //    //AOSw
                    //    for (int ii = 0; ii < (Default.AoSwichNum - Default.AoNum); ii++)     // "AoSw"
                    //    {
                    //        int jj = Default.AoSwichNum - ii;                           //データが逆順に並んでいるので（2→1）
                    //        dtItemRow[string.Format("{0}-{1:0}", Default.AoName, jj)] = csvFields[ii];  //Port名はAO
                    //    }
                    //}
                    dtPortDat.Rows.Add(dtItemRow);
                    //PortDisp

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("csvRecords.Count error:{0}", csvRecords.Count));
                    //    }
                    //}
                    ID++;
                }
                dtPortDat.AcceptChanges();
                //DataTable PortDisp = DataSetItems.PortDisp;
                //PortDisp.Rows.Clear();
                //DataRow dtPortDatRow = dtPortDat.Rows[0];
                //for (int i=0;i< Default.DioNums[0]; i++)    //32loop
                //{
                //    DataRow dtPortDispRow = DataSetItems.PortDisp.NewRow();
                //    for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                //    {
                //            string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[ii], i+1);
                //            string oFeildName = string.Format("Port{0}", Default.DioNames[ii]);

                //            dtPortDispRow[oFeildName] = dtPortDatRow[iFeildName];  //DataSetは名前参照なので順序は影響ない
                //    }
                //    DataSetItems.PortDisp.Rows.Add(dtPortDispRow);
                //}
                //for(int i=0;i< DataSetItems.PortDisp.Rows.Count;i++)
                //{
                //    System.Diagnostics.Debug.WriteLine(string.Format("DataSetItems.PortDisp.Rows:{0}", DataSetItems.PortDisp.Rows[i][0]));
                //}
            }
            else
            {
                //ファイルがないときは、Itemをクリア
                dtPortDat.Rows.Clear();
                //空行を追加
                DataRow dtItemRow = dtPortDat.NewRow();
                dtPortDat.Rows.Add(dtItemRow);
            }
        }

        private char char0a = (char)0x0a;
        private string suplessCTRN(string s)
        {
            return s.Replace(char0a.ToString(), "");  //\nを削除する
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method checkChangedPortdatFile 20170216
        public bool checkChangedPortdatFile(DataTable dtPortDat, string PortdatFile)
        {
            bool checkChanged = true;
            return savePortdatFile(dtPortDat, PortdatFile, checkChanged);   //checkChanged
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method savePortdatFile 20170216
        public bool savePortdatFile(DataTable dtPortDat, string PortdatFile)
        {
            bool checkChanged = false;
            return savePortdatFile(dtPortDat, PortdatFile, checkChanged);      //Save
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method savePortdatFile
        public bool savePortdatFile(DataTable dtPortDat, string PortdatFile, bool checkChanged)
        {
            bool retuenValue = false;   //20170216
            dtPortDat.AcceptChanges(); //20160915
            DataRow dtItemRow = dtPortDat.Rows[0];
            string msgLine = "";

            try
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(PortdatFile + Part, false, enc))   //no append
                {
                    //DIO
                    for (int ii = 0; ii < Default.DioNames.Length; ii++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
                    {
                        msgLine = "";
                        for (int j = 0; j < Default.DioNums[ii]; j++)       //{ 32, 32, 32, 32, 32, 32, 32, 32 }
                        {
                            int jj = Default.DioNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                            //dtItemRow[string.Format("{0}-{1:00}", Default.DioNames[ii], jj)] = csvFields[j];  //DataSetは名前参照なので順序は影響ない
                            string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[ii], jj);
                            if (j != 0) msgLine += ",";
                            msgLine += EncloseDoubleQuotes(dtItemRow[iFeildName].ToString());
                        }
                        sr.WriteLine(msgLine);
                    }
                    //AO
                    msgLine = "";
                    for (int ii = 0; ii < Default.AoSwichNum; ii++)     // "AO"
                    {
                        int jj = Default.AoSwichNum - ii;                           //データが逆順に並んでいるので（2→1）
                        //dtItemRow[string.Format("{0}-{1:0}", Default.AoName, jj)] = csvFields[ii];  //DataSetは名前参照なので順序は影響ない
                        string iFeildName = string.Format("{0}-{1:0}", Default.AoName, jj);  //DataSetは名前参照なので順序は影響ない
                        if (ii != 0) msgLine += ",";
                        msgLine += EncloseDoubleQuotes(dtItemRow[iFeildName].ToString());
                    }
                    sr.WriteLine(msgLine);
                    //AI
                    msgLine = "";
                    for (int ii = 0; ii < Default.AiNum; ii++)     // "AO"
                    {
                        int jj = Default.AiNum - ii;                           //データが逆順に並んでいるので（8→1）
                        //dtItemRow[string.Format("{0}-{1:0}", Default.AiName, jj)] = csvFields[ii];  //DataSetは名前参照なので順序は影響ない
                        string iFeildName = string.Format("{0}-{1:0}", Default.AiName, jj);  //DataSetは名前参照なので順序は影響ない
                        if (ii != 0) msgLine += ",";
                        msgLine += EncloseDoubleQuotes(dtItemRow[iFeildName].ToString());
                    }
                    sr.WriteLine(msgLine);
                    //GND
                    msgLine = "";
                    for (int ii = 0; ii < Default.GndNames.Length; ii++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
                    {
                        for (int j = 0; j < Default.GndNums[ii]; j++)       //{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                        {
                            int jj = Default.GndNums[ii] - j;                            //データが逆順に並んでいるので（32→01）
                            //dtItemRow[string.Format("{0}-{1:0}", Default.GndNames[ii], jj)] = csvFields[nn++];  //DataSetは名前参照なので順序は影響ない
                            string iFeildName = string.Format("{0}-{1:0}", Default.GndNames[ii], jj);  //DataSetは名前参照なので順序は影響ない
                            if (!(ii == 0 && j == 0)) msgLine += ",";
                            msgLine += EncloseDoubleQuotes(dtItemRow[iFeildName].ToString());
                        }
                    }
                    sr.WriteLine(msgLine);
                    ////AOSw PortにAoSw8～2を出力
                    //msgLine = "";
                    //for (int ii = 0; ii < (Default.AoSwichNum- Default.AoNum); ii++)     // "AoSw"
                    //{
                    //    int jj = Default.AoSwichNum - ii;                           //データが逆順に並んでいるので（2→1）
                    //    //dtItemRow[string.Format("{0}-{1:0}", Default.AoSwichName, jj)] = csvFields[ii];  //DataSetは名前参照なので順序は影響ない
                    //    string iFeildName = string.Format("{0}-{1:0}", Default.AoName, jj);  //DataSetは名前参照なので順序は影響ない
                    //    if (ii != 0) msgLine += ",";
                    //    msgLine += EncloseDoubleQuotes(dtItemRow[iFeildName].ToString());
                    //}
                    //sr.WriteLine(msgLine);
                }
                //内容が異なるときのみ、ファイルを更新する→やめ、Save時は必ずSaveする 20170216
                if (System.IO.File.Exists(PortdatFile))
                {
                    if (checkChanged)
                    {
                        retuenValue = !isFileSame(PortdatFile + Part, PortdatFile); //旧ファイルがあるとき
                        //FileCompareが終わったので、新しい方を消す
                        System.IO.File.Delete(PortdatFile + Part);
                    }
                    else
                    {
                        //    if (isFileSame(PortdatFile + Part, PortdatFile))
                        //    {
                        //        //内容が同じときは、新しい方を消す
                        //        System.IO.File.Delete(PortdatFile + Part);
                        //        retuenValue = false;     //新しくファイルを生成しないときはfalse
                        //    }
                        //    else
                        //    {
                        //内容が異なるときは、古い方を消して、リネームする
                        System.IO.File.Delete(PortdatFile);
                        System.IO.File.Move(PortdatFile + Part, PortdatFile);
                        retuenValue = true;     //新しくファイルを生成したときはtrue
                    }
                    //}
                }
                else
                {
                    //もともとのファイルがないときは、リネームのみする
                    System.IO.File.Move(PortdatFile + Part, PortdatFile);
                    retuenValue = true;     //新しくファイルを生成したときはtrue
                }
            }
            catch (Exception ex)
            {
                //Exception 20180931
                System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return retuenValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method loadInspectItem
        public void loadCheckdat(DataTable dtCheckDat, string CheckdatFile)
        {
            //20180723 CheckDatFields
            //string [] dtCheckDatFields = CheckDatLMTFields = new string[] { "-HiLMT", "-LoLMT" };
            string dtCheckDatFields;
            //DataTable dtCheckDat = DataSetItems.CheckDat;

            if (!string.IsNullOrEmpty(CheckdatFile) && System.IO.File.Exists(CheckdatFile))
            {
                //string stBuffer = System.IO.File.ReadAllText(CheckdatFile, enc);
                string stBuffer = this.ReadAllText(CheckdatFile, enc);
                // CSVを展開する。
                List<List<string>> csvRecords = CsvToArrayList2(stBuffer);
                if (csvRecords.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("Check.datファイルのデータが正しくありません。({0})", CheckdatFile));
                    return;
                }
                List<string> csvFields = csvRecords[0];
                //"リレーパネル","5C0-00700","00",1
                //MainID.Titleが登録済みか確認する
                string Title = csvFields[1];
                if (csvFields.Count < 4)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("Check.datファイルのデータが正しくありません。({0})", CheckdatFile));
                    return;
                }
                dtCheckDat.Rows.Clear();
                DataRow dtCheckDatRow = dtCheckDat.NewRow();
                dtCheckDatRow["Item"] = csvFields[0];       //"BOX relay (SH106-AA3)"
                dtCheckDatRow["Title"] = csvFields[1];       //329-0000"
                dtCheckDatRow["SubTitle"] = csvFields[2];       //"166"

                string sVolt = csvFields[3] == "1" ? "1" : "0";
                dtCheckDatRow["Volt"] = sVolt;       //"1"
                //12V 24V
                int intVoltIdx = (sVolt == "1" ? 2 : 0); //0:12V 2:24V
                //LMT 20180723
                int iDo_Length = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;

                for (int i = 0; i < iDo_Length; i++)
                {
                    //HiLMT
                    dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                    int fieldIdx = 4 + i * 2;
                    if (fieldIdx < csvFields.Count && !string.IsNullOrEmpty(csvFields[fieldIdx]))
                    {
                        dtCheckDatRow[dtCheckDatFields] = csvFields[fieldIdx];    //iOP-HiLMT～iTo-HiLMT
                    }
                    else
                    {
                        dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[intVoltIdx+0][i].ToString("F1");    //iOP-HiLMT～iTo-HiLMT 20180911-3 LMTがない時は、Defaultから調達
                    }
                    //LoLMT
                    dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                    fieldIdx = 4 + i * 2 + 1;   //LoLMTなので、+1
                    if (fieldIdx < csvFields.Count && !string.IsNullOrEmpty(csvFields[fieldIdx]))
                    {
                        dtCheckDatRow[dtCheckDatFields] = csvFields[fieldIdx];    //iOP-HiLMT～iTo-HiLMT
                    }
                    else
                    {
                        dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[intVoltIdx+1][i].ToString("F1");    //iOP-LoLMT～iTo-LoLMT 20180911-3 LMTがない時は、Defaultから調達
                    }
                }
                dtCheckDat.Rows.Add(dtCheckDatRow);
            }
            dtCheckDat.AcceptChanges();
        }


        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method checkChangedCheckdat 20170216
        public bool checkChangedCheckdat(DataTable dtCheckDat, string CheckdatFile)
        {
            bool checkChanged = true;
            return saveCheckdat(dtCheckDat, CheckdatFile, checkChanged);
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveCheckdat 20170216
        public bool saveCheckdat(DataTable dtCheckDat, string CheckdatFile)
        {
            bool checkChanged = false;
            return saveCheckdat(dtCheckDat, CheckdatFile, checkChanged);
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method saveCheckdat
        public bool saveCheckdat(DataTable dtCheckDat, string CheckdatFile, bool checkChanged)
        {
            bool retuenValue = false;   //20170216

            //DataTable dtCheckDat = DataSetItems.CheckDat;
            string msgLine = "";
            dtCheckDat.AcceptChanges(); //2016915

            try
            {
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(CheckdatFile + Part, false, enc))   //no append
                {
                    foreach (DataRow dtItemnRow in dtCheckDat.Rows)
                    {
                        msgLine = "";
                        for (int i = 0; i < dtCheckDat.Columns.Count; i++)
                        {
                            if (i > 0) msgLine += ",";
                            //msgLine += EncloseDoubleQuotesIfNeed(dtItemnRow[i].ToString());
                            //if (dtItemnRow[i].GetType() == Type.GetType("System.Int32"))
                            if (i == 3)     //20170216-6
                            {
                                msgLine += EncloseDoubleQuotesIfNeed(dtItemnRow[i].ToString());  //DoubleQuotesで囲わない
                            }
                            else
                            {
                                msgLine += EncloseDoubleQuotes(dtItemnRow[i].ToString());  //無条件にDoubleQuotesで囲う
                            }

                        }
                        sr.WriteLine(msgLine);
                        //sr.WriteLine("");       //空行
                    }
                }
                //内容が異なるときのみ、ファイルを更新する→やめ、Save時は必ずSaveする 20170216
                if (System.IO.File.Exists(CheckdatFile))
                {
                    if (checkChanged)       //20170216
                    {
                        retuenValue = !isFileSame(CheckdatFile + Part, CheckdatFile);   //ファイルがあるとき
                        //FileCompareが終わったので、新しい方を消す
                        System.IO.File.Delete(CheckdatFile + Part);
                    }
                    else
                    {
                        //if (isFileSame(CheckdatFile + Part, CheckdatFile))
                        //{
                        //    //内容が同じときは、新しい方を消す
                        //    System.IO.File.Delete(CheckdatFile + Part);
                        //    retuenValue = false;     //新しくファイルを生成しないときはfalse
                        //}
                        //else
                        //{
                        //内容が異なるときは、古い方を消して、リネームする
                        System.IO.File.Delete(CheckdatFile);
                        System.IO.File.Move(CheckdatFile + Part, CheckdatFile);
                        retuenValue = true;     //新しくファイルを生成したときはtrue

                        //}
                    }
                }
                else
                {
                    if (checkChanged)       //20170216
                    {
                        retuenValue = true;     //ファイルがないとき
                        //FileCompareが終わったので、新しい方を消す
                        System.IO.File.Delete(CheckdatFile + Part);
                    }
                    else
                    {                //もともとのファイルがないときは、リネームのみする
                        System.IO.File.Move(CheckdatFile + Part, CheckdatFile);
                        retuenValue = true;     //新しくファイルを生成したときはtrue
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception 20180931
                System.Windows.Forms.MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return retuenValue;
        }

        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        //private bool FileCompare(string file1, string file2)
        private bool isFileSame(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            System.IO.FileStream fs1;
            System.IO.FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new System.IO.FileStream(file1, System.IO.FileMode.Open);
            fs2 = new System.IO.FileStream(file2, System.IO.FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //method topMenuFromFolder
        public void topMenuFromFolder(DataSetTopMenu DataSetTopMenu, string menuFolderPass)
        {
            //"menuFolderPass"以下の"Check.dat"ファイルをすべて取得する
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(menuFolderPass);
            System.IO.FileInfo[] files =
                di.GetFiles(Default.Checkdat, System.IO.SearchOption.AllDirectories);

            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSubMain = DataSetTopMenu.menuSub;
            int NewMainID = -1;

            string Title = null;
            string SubTitle = null;
            string DirectoryName = null;
            string Folder = null;
            DataTable dtCheckDat = new DataSetItems.CheckDatDataTable();

            //ListBox1に結果を表示する
            foreach (System.IO.FileInfo Checkdat in files)
            {
                //20160916 従来と合わせるため、将来削除、そのため完全ディレクトリ制として、Checkdatの内容を書き換えない
                //また、Folderの位置は、親位置となり、SubFolderはFolderに含まない
                //Dir優先、Checkdatの一つ上のフォルダをSubTitle、さらにもう一つ上のフォルダをTitleにする
                Folder = System.IO.Path.GetDirectoryName(Checkdat.FullName);    //20161203 Folderは直上のDirectoryとする
                SubTitle = System.IO.Path.GetFileName(Folder);
                DirectoryName = System.IO.Path.GetDirectoryName(Folder);
                if (DirectoryName.ToLower() == menuFolderPass.ToLower())
                {
                    SubTitle = "XX";  // System.IO.Path.GetFileName(Folder);
                    DirectoryName = Folder;
                }
                //Title = System.IO.Path.GetFileName(DirectoryName);
                Title = DirectoryName.Substring(menuFolderPass.Length + 1);   //+1は、￥をとる為
                //if (SubTitle == Default.HistViewFolder)
                //{
                //    break;
                //}
                DataRow[] dtMainRows = dtMain.Select(string.Format("Title='{0}'", Title));
                if (dtMainRows.Length == 0)
                {
                    //MainIDが未登録なので登録する
                    DataRow dtMainRowNew = dtMain.NewRow();
                    dtMainRowNew["Title"] = Title;
                    dtMain.Rows.Add(dtMainRowNew); //Add
                    NewMainID = int.Parse(dtMain.Rows[dtMain.Rows.Count - 1]["MainID"].ToString());
                }
                else
                {
                    NewMainID = int.Parse(dtMainRows[0]["MainID"].ToString());
                }
                //MainID.SubTitleが登録済みか確認する
                int FoundCount = 0;
                foreach (DataRow dtSubRow in dtSubMain.Select(string.Format("MainID='{0}'", NewMainID)))
                {
                    if (dtSubRow["SubTitle"].ToString() == SubTitle)
                    {
                        FoundCount++;
                        break;
                    }
                }
                if (FoundCount == 0)
                {
                    //SubTitleが未登録なので登録する
                    DataRow dtSubMainRowNew = dtSubMain.NewRow();
                    dtSubMainRowNew["MainID"] = NewMainID;//MainIDに連動させる
                    dtSubMainRowNew["SubTitle"] = SubTitle;
                    dtSubMainRowNew["Folder"] = Folder;
                    dtSubMain.Rows.Add(dtSubMainRowNew);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("SubTitleが重複するため、メニューに登録できません。\n\n フォルダ構成を見直してください。\n\n[{0}]", Checkdat.FullName));
                }
            }
        }


        /// <summary>
        /// CSVをArrayListに変換
        /// http://dobon.net/vb/dotnet/file/readcsvfile.html
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public List<List<string>> CsvToArrayList2(string csvText)
        {
            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] { '\r', '\n' });

            List<List<string>> csvRecords = new List<List<string>>();
            List<string> csvFields = new List<string>();

            int csvTextLength = csvText.Length;
            int startPos = 0, endPos = 0;
            string field = "";

            while (true)
            {
                //空白を飛ばす
                while (startPos < csvTextLength &&
                    (csvText[startPos] == ' ' || csvText[startPos] == '\t'))
                {
                    startPos++;
                }

                //データの最後の位置を取得
                if (startPos < csvTextLength && csvText[startPos] == '"')
                {
                    //"で囲まれているとき
                    //最後の"を探す
                    endPos = startPos;
                    while (true)
                    {
                        endPos = csvText.IndexOf('"', endPos + 1);
                        if (endPos < 0)
                        {
                            throw new ApplicationException("\"が不正");
                        }
                        //"が2つ続かない時は終了
                        if (endPos + 1 == csvTextLength || csvText[endPos + 1] != '"')
                        {
                            break;
                        }
                        //"が2つ続く
                        endPos++;
                    }

                    //一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos + 1);
                    //""を"にする
                    field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");

                    endPos++;
                    //空白を飛ばす
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n')
                    {
                        endPos++;
                    }
                }
                else
                {
                    //"で囲まれていない
                    //カンマか改行の位置
                    endPos = startPos;
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n')
                    {
                        endPos++;
                    }

                    //一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos);
                    //後の空白を削除
                    field = field.TrimEnd();
                }

                //フィールドの追加
                csvFields.Add(field);

                //行の終了か調べる
                if (endPos >= csvTextLength || csvText[endPos] == '\n')
                {
                    //行の終了
                    //レコードの追加
                    //csvFields.TrimToSize();
                    csvRecords.Add(csvFields);
                    csvFields = new List<string>();

                    if (endPos >= csvTextLength)
                    {
                        //終了
                        break;
                    }
                }

                //次のデータの開始位置
                startPos = endPos + 1;
            }

            //csvRecords.TrimToSize();
            return csvRecords;
        }


        /// <summary>
        /// http://dobon.net/vb/dotnet/file/writecsvfile.html
        /// 必要ならば、文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }

        /// <summary>
        /// ReadAllText,System.IO.File.ReadAllTextは、エンコードが正しく読み込まれないようなので、自前で作成
        /// </summary>
        public string ReadAllText(string fileName, System.Text.Encoding enc)
        {
            //string stBuffer = System.IO.File.ReadAllText(CheckdatFile, enc);

            string stBuffer = null; //制御文字(crとか)取り除くため、一旦すべて読み込む20160914

            System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(fileName);
            System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
            using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
            {
                while (!cReader.EndOfStream)
                {
                    // ファイルを 1 行読み込む
                    stBuffer = cReader.ReadToEnd();
                }
            }

            return stBuffer;
        }
        /// <summary>
        /// DataSetItemsは、fileからDataSetItemsを生成する
        /// </summary>
        public DataSetItems createDataSetItems(string ItemFolder, string MainTitle, string SubTitle)
        {
            DataSetItems myDataSetItems = null;

            if (!System.IO.Directory.Exists(ItemFolder))
            {
                //設定ファイルのフォルダがないときは、処理中断する
                return myDataSetItems;
            }

            myDataSetItems = new NippoControlSystem.DataSetItems();

            string CheckdatFile = ItemFolder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
            string ListdatFile = ItemFolder + System.IO.Path.DirectorySeparatorChar + Default.Listdat;
            string PortdatFile = ItemFolder + System.IO.Path.DirectorySeparatorChar + Default.Portdat;
            //Checkdat
            if (!System.IO.File.Exists(CheckdatFile))
            {
                //ないときは、CheckdatFileを新規作成する
                this.newCheckdatFile(CheckdatFile, "未設定", MainTitle, SubTitle, "");
            }
            this.loadCheckdat(myDataSetItems.CheckDat, CheckdatFile);
            //loadListdatFileで、iDsが空欄のとき、これを使う
            string CheckDatiDsLo = myDataSetItems.CheckDat.Rows[0]["iDs-LoLMT"].ToString();
            string CheckDatiDsHi = myDataSetItems.CheckDat.Rows[0]["iDs-LoLMT"].ToString();

            this.loadListdatFile(myDataSetItems.ListDat, ListdatFile);
            this.loadPortdatFile(myDataSetItems.PortDat, PortdatFile);

            //View_DA
            for (int i = 0; i < Default.DioNames.Length; i++)
            {
                DataSetItems.View_DIODataTable dtView_DIO = new DataSetItems.View_DIODataTable();
                dtView_DIO.TableName = string.Format("View_{0}", Default.DioNames[i]);      //View_DA～DH
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    DataRow DIORow = dtView_DIO.NewRow();
                    dtView_DIO.Rows.Add(DIORow);
                }
                myDataSetItems.Tables.Add(dtView_DIO);
                myDataSetItems.Tables[dtView_DIO.TableName].AcceptChanges();
            }
            //View_DALMT 20180731
            for (int i = 0; i < Default.DioNames.Length; i++)
            {
                DataSetItems.View_DIOLMTDataTable dtView_DIOLMT = new DataSetItems.View_DIOLMTDataTable();
                dtView_DIOLMT.TableName = string.Format("View_{0}LMT", Default.DioNames[i]);      //View_DA～DH
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    DataRow DIORow = dtView_DIOLMT.NewRow();
                    dtView_DIOLMT.Rows.Add(DIORow);
                }
                myDataSetItems.Tables.Add(dtView_DIOLMT);
                myDataSetItems.Tables[dtView_DIOLMT.TableName].AcceptChanges();
            }
            //dtView_GndDIO
            for (int i = 0; i < Default.GndNames.Length; i++)
            {
                DataSetItems.View_GndDIODataTable dtView_GndDIO = new DataSetItems.View_GndDIODataTable();
                dtView_GndDIO.TableName = string.Format("View_{0}", Default.GndNames[i]);      //View_GndDA～View_GndAI
                for (int j = 0; j < Default.GndNums[i]; j++)
                {
                    DataRow GndDIORow = dtView_GndDIO.NewRow();
                    dtView_GndDIO.Rows.Add(GndDIORow);
                }
                myDataSetItems.Tables.Add(dtView_GndDIO);
                myDataSetItems.Tables[dtView_GndDIO.TableName].AcceptChanges();
            }
            //dtView_AI
            DataSetItems.View_AIDataTable dtView_AI = myDataSetItems.View_AI;
            for (int j = 0; j < Default.AiNum; j++)
            {
                DataRow dtView_AIRow = dtView_AI.NewRow();
                dtView_AI.Rows.Add(dtView_AIRow);
            }
            myDataSetItems.View_AI.AcceptChanges();
            //dtView_AO
            DataSetItems.View_AODataTable dtView_AO = myDataSetItems.View_AO;
            for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)
            {
                DataRow dtView_AORow = dtView_AO.NewRow();
                dtView_AORow["Enable"] = "";
                dtView_AO.Rows.Add(dtView_AORow);
            }
            myDataSetItems.View_AO.AcceptChanges();
            //dtView_AOM for Monitor
            DataSetItems.View_AODataTable dtView_AOM = new DataSetItems.View_AODataTable();
            dtView_AOM.TableName = string.Format("View_{0}M", Default.AoName);      //View_AOM
            for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)
            {
                DataRow dtView_AOMRow = dtView_AOM.NewRow();
                dtView_AOMRow["Enable"] = "";
                dtView_AOM.Rows.Add(dtView_AOMRow);
            }
            myDataSetItems.Tables.Add(dtView_AOM);
            myDataSetItems.View_AO.AcceptChanges();
            //dtView_GndAIO
            DataSetItems.View_GndAIODataTable dtView_GndAIO = myDataSetItems.View_GndAIO;
            for (int j = 0; j < Default.GndNums[8]; j++)
            {
                DataRow dtView_GndAIORow = dtView_GndAIO.NewRow();
                dtView_GndAIO.Rows.Add(dtView_GndAIORow);
            }
            myDataSetItems.View_GndAIO.AcceptChanges();

            return myDataSetItems;
        }

        public void newCheckdatFile(string CheckdatFile, string Item, string MainTitle, string SubTitle, string Volt)
        {
            //ファイルがないので、CheckdatFileを新規作成する
            DataTable dtCheckDat = new DataSetItems.CheckDatDataTable();
            //dtCheckDat.Rows.Clear();
            DataRow dtCheckDatRow = dtCheckDat.NewRow();
            dtCheckDatRow["Item"] = Item;       //"BOX relay (SH106-AA3)"
            dtCheckDatRow["Title"] = MainTitle;       //329-0000"
            dtCheckDatRow["SubTitle"] = SubTitle;       //"166"
            dtCheckDatRow["Volt"] = Volt;       //Volt "1"
            //20180831
            //LMT 20180723
            string dtCheckDatFields = null;
            int iDo_Length = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;
            //12V 24V
            int mVoltIdx = (Volt == "1" ? 2 : 0); //0:12V 2:24V

            for (int i = 0; i < iDo_Length; i++)
            {
                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx][i].ToString("F1");    //iOP-HiLMT～iTo-HiLMT
                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx + 1][i].ToString("F1");    //iOP-LoLMT～iTo-LoLMT
            }

            dtCheckDat.Rows.Add(dtCheckDatRow);
            dtCheckDat.AcceptChanges();
            this.saveCheckdat(dtCheckDat, CheckdatFile);
        }

        public void debugPrintDT(DataTable table)
        {
            string sBuf = "";
            DataColumnCollection Columns = table.Columns;
            for (int i = 0; i < Columns.Count; i++)
            {
                if (sBuf != "") sBuf += ",";
                sBuf += Columns[i].Caption;
            }
            System.Diagnostics.Debug.WriteLine(string.Format("{0}", sBuf));


            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                sBuf = "";
                for (int j = 0; j < Columns.Count; j++)
                {
                    if (sBuf != "") sBuf += ",";
                    sBuf += row[j].ToString();
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0}", sBuf));
            }
        }
    }
}
