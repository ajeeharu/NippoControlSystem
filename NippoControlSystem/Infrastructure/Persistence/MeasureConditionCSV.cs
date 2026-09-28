using System.Data;
using System.IO;
using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public partial class MeasureCondition
    {
        public System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
        public string Part = ".part";

        string? CheckDatiDsLo = null;
        string? CheckDatiDsHi = null;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool isCsvHeaderRead { get; set; }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // method saveResultCsv (ファイルIOを1回にまとめて高速化)
        public void saveResultCsv(DataSetItems myDataSetItems, string Folder)
        {
            DateTime dt = this.TestEndDT;
            string resultCsvPass = System.IO.Path.Combine(Folder, string.Format("{0:yyyy}-log.csv", dt));
            string resultListPass = System.IO.Path.Combine(Folder, string.Format("A{0:yyyyMMdd_HHmmss}.csv", dt));

            try
            {
                // 1. サマリログの追記書き込み
                using (var sr = new StreamWriter(resultCsvPass, true, enc))
                {
                    string passFail = (this.InspecStat == enumInspectStat.Stat_NormalEND) ? "Pass" : "Fail";
                    var lineValues = new[]
                    {
                        EncloseDoubleQuotes(string.Format("{0:yyyy.MM.dd HH:mm:ss}", dt)),
                        EncloseDoubleQuotes(this.SerialNo),
                        EncloseDoubleQuotes(this.GoNo),
                        EncloseDoubleQuotes(this.Zuban),
                        EncloseDoubleQuotes(this.Edaban),
                        EncloseDoubleQuotes(this.InspecStartStatString),
                        EncloseDoubleQuotes(passFail)
                    };
                    sr.WriteLine(string.Join(",", lineValues));
                }

                // 2. 詳細ログファイルの保存（メモリ上で組み上げて1回で書き込み）
                this.saveInspectItemWithResult(myDataSetItems.ListDatResult, resultListPass);

                // 3. 関連データファイル（Listdat, Portdat, Checkdat）を1つのストリームでまとめて追記
                string CheckdatFile = System.IO.Path.Combine(Folder, Default.Checkdat);
                string ListdatFile = System.IO.Path.Combine(Folder, Default.Listdat);
                string PortdatFile = System.IO.Path.Combine(Folder, Default.Portdat);

                using (var sr = new StreamWriter(resultListPass, true, enc))
                {
                    AppendFileContentIfExist(sr, ListdatFile);
                    AppendFileContentIfExist(sr, PortdatFile);
                    AppendFileContentIfExist(sr, CheckdatFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
        }

        private void AppendFileContentIfExist(StreamWriter writer, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                string content = this.ReadAllText(filePath, enc);
                if (!string.IsNullOrEmpty(content))
                {
                    writer.WriteLine();
                    writer.Write(content);
                }
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // method saveMenuCsv (StringBuilder を使った高速化)
        public void saveMenuCsv(DataSetTopMenu DataSetTopMenu, string menuCsvPass)
        {
            DataTable dtMain = DataSetTopMenu.menuMain;
            DataTable dtSubMain = DataSetTopMenu.menuSub;

            try
            {
                dtMain.AcceptChanges();
                dtSubMain.AcceptChanges();

                var sb = new StringBuilder();

                using (var sr = new StreamWriter(menuCsvPass, false, enc))
                {
                    foreach (DataRow dtMainRow in dtMain.Rows)
                    {
                        sb.Clear();
                        sb.Append(EncloseDoubleQuotesIfNeed(dtMainRow["Title"].ToString()));
                        string mainIdStr = dtMainRow["MainID"].ToString();

                        for (int i = 0; i < dtSubMain.Rows.Count; i++)
                        {
                            if (dtSubMain.Rows[i]["MainID"].ToString() == mainIdStr)
                            {
                                sb.Append(",")
                                  .Append(EncloseDoubleQuotesIfNeed(dtSubMain.Rows[i]["SubTitle"].ToString()))
                                  .Append(",")
                                  .Append(EncloseDoubleQuotesIfNeed(dtSubMain.Rows[i]["Folder"].ToString()));
                            }
                        }
                        sr.WriteLine(sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // method saveInspectItem (StringBuilder 導入とファイル処理整理)
        public bool saveInspectItem(DataTable ListDat, string ListdatFile, bool withResult, bool checkChanged)
        {
            bool returnValue = false;
            ListDat.AcceptChanges();
            string tempPartFile = ListdatFile + Part;

            try
            {
                var sb = new StringBuilder();

                using (var sr = new StreamWriter(tempPartFile, false, enc))
                {
                    foreach (DataRow dtItemRow in ListDat.Rows)
                    {
                        sb.Clear();
                        bool isFirstCol = true;

                        for (int i = 0; i < ListDat.Columns.Count; i++)
                        {
                            string colName = ListDat.Columns[i].ColumnName;
                            if (colName == "InspectID") continue;

                            string strField = dtItemRow[i].ToString();
                            if (i == 1) strField = strField.Replace(Environment.NewLine, "\\n");

                            if (colName == "Result" && !withResult)
                            {
                                strField = "";
                            }

                            if (!isFirstCol) sb.Append(",");
                            sb.Append(EncloseDoubleQuotes(strField));
                            isFirstCol = false;
                        }
                        sr.WriteLine(sb.ToString());
                    }
                }

                if (File.Exists(ListdatFile))
                {
                    if (checkChanged)
                    {
                        returnValue = !isFileSame(tempPartFile, ListdatFile);
                        File.Delete(tempPartFile);
                    }
                    else
                    {
                        File.Delete(ListdatFile);
                        File.Move(tempPartFile, ListdatFile);
                        returnValue = true;
                    }
                }
                else
                {
                    if (checkChanged)
                    {
                        returnValue = true;
                        File.Delete(tempPartFile);
                    }
                    else
                    {
                        File.Move(tempPartFile, ListdatFile);
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return returnValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // method savePortdatFile (StringBuilder 導入)
        public bool savePortdatFile(DataTable dtPortDat, string PortdatFile, bool checkChanged)
        {
            bool returnValue = false;
            dtPortDat.AcceptChanges();
            if (dtPortDat.Rows.Count == 0) return false;

            DataRow dtItemRow = dtPortDat.Rows[0];
            string tempPartFile = PortdatFile + Part;

            try
            {
                var sb = new StringBuilder();

                using (var sr = new StreamWriter(tempPartFile, false, enc))
                {
                    // DIO
                    for (int ii = 0; ii < Default.DioNames.Length; ii++)
                    {
                        sb.Clear();
                        for (int j = 0; j < Default.DioNums[ii]; j++)
                        {
                            int jj = Default.DioNums[ii] - j;
                            string iFieldName = string.Format("{0}-{1:00}", Default.DioNames[ii], jj);
                            if (j != 0) sb.Append(",");
                            sb.Append(EncloseDoubleQuotes(dtItemRow[iFieldName].ToString()));
                        }
                        sr.WriteLine(sb.ToString());
                    }

                    // AO
                    sb.Clear();
                    for (int ii = 0; ii < Default.AoSwichNum; ii++)
                    {
                        int jj = Default.AoSwichNum - ii;
                        string iFieldName = string.Format("{0}-{1:0}", Default.AoName, jj);
                        if (ii != 0) sb.Append(",");
                        sb.Append(EncloseDoubleQuotes(dtItemRow[iFieldName].ToString()));
                    }
                    sr.WriteLine(sb.ToString());

                    // AI
                    sb.Clear();
                    for (int ii = 0; ii < Default.AiNum; ii++)
                    {
                        int jj = Default.AiNum - ii;
                        string iFieldName = string.Format("{0}-{1:0}", Default.AiName, jj);
                        if (ii != 0) sb.Append(",");
                        sb.Append(EncloseDoubleQuotes(dtItemRow[iFieldName].ToString()));
                    }
                    sr.WriteLine(sb.ToString());

                    // GND
                    sb.Clear();
                    bool isFirst = true;
                    for (int ii = 0; ii < Default.GndNames.Length; ii++)
                    {
                        for (int j = 0; j < Default.GndNums[ii]; j++)
                        {
                            int jj = Default.GndNums[ii] - j;
                            string iFieldName = string.Format("{0}-{1:0}", Default.GndNames[ii], jj);
                            if (!isFirst) sb.Append(",");
                            sb.Append(EncloseDoubleQuotes(dtItemRow[iFieldName].ToString()));
                            isFirst = false;
                        }
                    }
                    sr.WriteLine(sb.ToString());
                }

                if (File.Exists(PortdatFile))
                {
                    if (checkChanged)
                    {
                        returnValue = !isFileSame(tempPartFile, PortdatFile);
                        File.Delete(tempPartFile);
                    }
                    else
                    {
                        File.Delete(PortdatFile);
                        File.Move(tempPartFile, PortdatFile);
                        returnValue = true;
                    }
                }
                else
                {
                    File.Move(tempPartFile, PortdatFile);
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return returnValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // method saveCheckdat (StringBuilder 導入)
        public bool saveCheckdat(DataTable dtCheckDat, string CheckdatFile, bool checkChanged)
        {
            bool returnValue = false;
            dtCheckDat.AcceptChanges();
            string tempPartFile = CheckdatFile + Part;

            try
            {
                var sb = new StringBuilder();

                using (var sr = new StreamWriter(tempPartFile, false, enc))
                {
                    foreach (DataRow dtItemnRow in dtCheckDat.Rows)
                    {
                        sb.Clear();
                        for (int i = 0; i < dtCheckDat.Columns.Count; i++)
                        {
                            if (i > 0) sb.Append(",");
                            string value = dtItemnRow[i].ToString();

                            if (i == 3)
                            {
                                sb.Append(EncloseDoubleQuotesIfNeed(value));
                            }
                            else
                            {
                                sb.Append(EncloseDoubleQuotes(value));
                            }
                        }
                        sr.WriteLine(sb.ToString());
                    }
                }

                if (File.Exists(CheckdatFile))
                {
                    if (checkChanged)
                    {
                        returnValue = !isFileSame(tempPartFile, CheckdatFile);
                        File.Delete(tempPartFile);
                    }
                    else
                    {
                        File.Delete(CheckdatFile);
                        File.Move(tempPartFile, CheckdatFile);
                        returnValue = true;
                    }
                }
                else
                {
                    if (checkChanged)
                    {
                        returnValue = true;
                        File.Delete(tempPartFile);
                    }
                    else
                    {
                        File.Move(tempPartFile, CheckdatFile);
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("書き込みができません。書込許可があるか確認して下さい。\n[{0}]", ex.Message), Default.ApplicationName);
            }
            return returnValue;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        // 改善版 ReadAllText（リソース解放の修正と不要ループの削減）
        public string ReadAllText(string fileName, System.Text.Encoding enc)
        {
            if (!File.Exists(fileName)) return null;

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var cReader = new StreamReader(fs, enc))
            {
                return cReader.ReadToEnd();
            }
        }
    }
}