using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    class libDataGridView
    {
        static public void dataGridView_SelectionClear(DataGridView dataGridView1)
        {
            //現在のセルのある行から下にペーストする
            foreach (DataGridViewCell c in dataGridView1.SelectedCells)
            {
                    //選択されているセルをクリアする
                    c.Selected = false;
            }
            foreach (DataGridViewRow c in dataGridView1.SelectedRows)
            {
                //選択されているセルをクリアする
                c.Selected = false;
            }
        }

        static public void dataGridView_Clear(DataGridView dataGridView1, int StartEnableColumn)
        {
            //現在のセルのある行から下にペーストする
            foreach (DataGridViewCell c in dataGridView1.SelectedCells)
            {
                if (c.ColumnIndex >= StartEnableColumn)
                {
                    //選択されているセルをクリアする
                    c.Value = null;
                }
            }
        }

        public delegate string delegateGetiDpLMT(DataGridView dataGridView1, int ColumnIndex, int RowIndex);
        static public void dataGridView_Copy(DataGridView dataGridView1, int StartEnableColumn, delegateGetiDpLMT getiDpLMT)
        {
            StringBuilder builder = new StringBuilder();
            //複数選択時
            int RowsCount = libDataGridView.countSelectedRows(dataGridView1.SelectedCells);
            int ColumnCount = libDataGridView.countSelectedColumn(dataGridView1.SelectedCells);
            //
            int insertRowIndex = libDataGridView.minSelectedRows(dataGridView1);
            int insertColumnIndex = libDataGridView.minSelectedColumn(dataGridView1);

            //現在のセルのある行から下にコピーする
            for (int iRow = 0; iRow < RowsCount; iRow++)
            {
                insertColumnIndex = libDataGridView.minSelectedColumn(dataGridView1);
                for (int iColumn = 0; iColumn < ColumnCount; iColumn++)
                {
                    if (insertColumnIndex >= StartEnableColumn)   //20161020
                    {
                        //選択されているセルをコピーする
                        string CellValue = dataGridView1[insertColumnIndex, insertRowIndex].Value.ToString();
                        if (CellValue == "iDp" || CellValue == "nDp")   //20180806-2
                        {
                            CellValue += getiDpLMT(dataGridView1, insertColumnIndex, insertRowIndex);
                        }
                        CellValue = CellValue.Replace("\t", "\\t");
                        CellValue = CellValue.Replace("\"", "\"\"");
                        builder.Append("\"" + CellValue + "\"" + ((iColumn + 1) == ColumnCount ? Environment.NewLine : "\t"));
                    }
                    insertColumnIndex++;
                }
                //builder.Append(Environment.NewLine);
                insertRowIndex++;
            }
            if (builder.Length > 0)
            {
                Clipboard.SetText(builder.ToString());
            }
        }

        static public bool allWaysTrue(string strPaste, int ColumnIndex, int RowIndex)
        {
            return true;
        }

        static public bool SetNulliDpLMT(DataGridView dataGridView1, int ColumnIndex, int RowIndex, string setValue)   //setValue = "iDp:1.234:9.876"
        {
            return true;
        }

        static public void dataGridView_Paste(DataGridView dataGridView1, int StartEnableColumn)
        {
            //dataGridView_Paste(dataGridView1, StartEnableColumn, new delegateCanPaste(allWaysTrue));
            dataGridView_Paste(dataGridView1, StartEnableColumn, new delegateCanPaste(allWaysTrue), new delegateSetiDpLMT(SetNulliDpLMT));
        }

        static public void dataGridView_Paste(DataGridView dataGridView1, int StartEnableColumn, delegateCanPaste CanPaste)
        {
            dataGridView_Paste(dataGridView1, StartEnableColumn, CanPaste, new delegateSetiDpLMT(SetNulliDpLMT));
        }

        // delegateCanPaste という名前のデリゲート型を定義
        public delegate bool delegateCanPaste(string strPaste, int ColumnIndex, int RowIndex);
        public delegate bool delegateSetiDpLMT(DataGridView dataGridView1, int ColumnIndex, int RowIndex, string setValue);   //setValue = "iDp:1.234:9.876"
        static public void dataGridView_Paste(DataGridView dataGridView1, int StartEnableColumn, delegateCanPaste CanPaste, delegateSetiDpLMT SetiDpLMT)
        {
            //現在のセルのある行から下にペーストする
            //if (dataGridView1.CurrentCell == null)
            //    return;
            int insertRowIndex = 0;
            int insertColumnIndex = 0;

            //クリップボードの内容を取得して、行で分ける
            string pasteText = Clipboard.GetText();
            if (string.IsNullOrEmpty(pasteText))
                return;
            //20160907
            //複数選択時
            int RowsCount = libDataGridView.countSelectedRows(dataGridView1.SelectedCells);
            int ColumnCount = libDataGridView.countSelectedColumn(dataGridView1.SelectedCells);
            //
            insertRowIndex = libDataGridView.minSelectedRows(dataGridView1);
            insertColumnIndex = libDataGridView.minSelectedColumn(dataGridView1);
            insertColumnIndex = Math.Max(insertColumnIndex, StartEnableColumn);

            //タブで分割
            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_Paste pasteText={0}", pasteText));
            List<List<string>> List3 = libDataGridView.CsvToArrayList3(pasteText);
            List<string>? vals = null;

            for (int iRow = 0; iRow < System.Math.Min(RowsCount, List3.Count); iRow++)
            {
                vals = List3[iRow];
                for (int iColumn = 0; iColumn < System.Math.Min(ColumnCount, vals.Count); iColumn++)
                {
                    if (vals[iColumn].IndexOf("iDp") == 0 || vals[iColumn].IndexOf("nDp") == 0)
                    {
                        string[] valsSplit = vals[iColumn].Split(':');
                        if (valsSplit.Length == 3)
                        {
                            dataGridView1[insertColumnIndex + iColumn, insertRowIndex + iRow].Value = valsSplit[0];
                            //ここで、LMTを書き込む20180817
                            if (SetiDpLMT(dataGridView1, insertColumnIndex + iColumn, insertRowIndex + iRow, vals[iColumn].ToString()))
                            {
                                //Susses
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("SetiDpLMT Error:" + vals[iColumn].ToString());
                            }

                        }
                        else if (valsSplit.Length == 1)    //"iDp"か"nDp"で、LMTがないとき（基本これはないと思う）
                        {
                            if (CanPaste(vals[iColumn], insertColumnIndex + iColumn, insertRowIndex + iRow))
                            {
                                dataGridView1[insertColumnIndex + iColumn, insertRowIndex + iRow].Value = vals[iColumn];
                            }
                        }
                    }
                    else
                    {
                        //通常（"iDp"、"nDp"以外）
                        if (CanPaste(vals[iColumn], insertColumnIndex + iColumn, insertRowIndex + iRow))
                        {
                            dataGridView1[insertColumnIndex + iColumn, insertRowIndex + iRow].Value = vals[iColumn];
                        }
                    }
                }
            }
        }

        static public int countSelectedRows(DataGridView dataGridView1)
        {
            return countSelectedRows(dataGridView1.SelectedCells);
        }

        static public int minSelectedRows(DataGridView dataGridView1)
        {
            return minSelectedRows(dataGridView1.SelectedCells);
        }

        static public int countSelectedColumn(DataGridView dataGridView1)
        {
            return countSelectedColumn(dataGridView1.SelectedCells);
        }

        static public int minSelectedColumn(DataGridView dataGridView1)
        {
            return minSelectedColumn(dataGridView1.SelectedCells);
        }

        static public int countSelectedRows(DataGridViewSelectedCellCollection SelectedCells)
        {
            int minRow = -1;
            int maxRow = 0;

            if (SelectedCells != null && SelectedCells.Count > 0)
            {
                minRow = int.MaxValue;
                maxRow = int.MinValue;

                foreach (DataGridViewCell c in SelectedCells)
                {
                    minRow = Math.Min(minRow, c.RowIndex);
                    maxRow = Math.Max(maxRow, c.RowIndex);
                }
            }
            return maxRow - minRow + 1;
        }

        static public int minSelectedRows(DataGridViewSelectedCellCollection SelectedCells)
        {
            int minRow = 0;

            if (SelectedCells != null && SelectedCells.Count > 0)
            {
                minRow = int.MaxValue;

                foreach (DataGridViewCell c in SelectedCells)
                {
                    minRow = Math.Min(minRow, c.RowIndex);
                }
            }

            return minRow;
        }

        static public int countSelectedColumn(DataGridViewSelectedCellCollection SelectedCells)
        {
            int minRow = -1;
            int maxRow = 0;

            if (SelectedCells != null && SelectedCells.Count > 0)
            {
                minRow = int.MaxValue;
                maxRow = int.MinValue;


                foreach (DataGridViewCell c in SelectedCells)
                {
                    minRow = Math.Min(minRow, c.ColumnIndex);
                    maxRow = Math.Max(maxRow, c.ColumnIndex);
                }
            }
            return maxRow - minRow + 1;
        }

        static public int minSelectedColumn(DataGridViewSelectedCellCollection SelectedCells)
        {
            int minRow = 0;

            if (SelectedCells != null && SelectedCells.Count > 0)
            {
                minRow = int.MaxValue;
                foreach (DataGridViewCell c in SelectedCells)
                {
                    minRow = Math.Min(minRow, c.ColumnIndex);
                }
            }
            return minRow;
        }

        /// <summary>
        /// CSVをArrayListに変換
        /// http://dobon.net/vb/dotnet/file/readcsvfile.html
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        /// コピペ用にカンマ区切りでなく、TAB区切り
        static public List<List<string>> CsvToArrayList3(string csvText)
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
                        csvText[endPos] != '\t' && csvText[endPos] != '\n')
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
                        csvText[endPos] != '\t' && csvText[endPos] != '\n')
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

        static public void DebugPrintDataTable(System.Data.DataSet DataSource, string DataMember)
        {
            //this.dataGridView_GndAIO.DataSource = mc.DataSetItems;
            //this.dataGridView_GndAIO.DataMember = "View_GndAIO";

            //foreach (DataRow dtMainRow in mc.DataSetTopMenu.menuMain.Rows)
            //{
            //    Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "frmOpenning", string.Format("menuMain:{0}:{1}", dtMainRow["MainID"], dtMainRow["Title"]));
            //    foreach (DataRow dtSubRow in mc.DataSetTopMenu.menuSub.Select(string.Format("MainID='{0}'", dtMainRow["MainID"])))
            //    {
            //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "frmOpenning", string.Format("menuSub :{0}:{1}:{2}:{3}", dtSubRow["MainID"], dtSubRow["SubID"], dtSubRow["SubTitle"], dtSubRow["Folder"]));
            //    }

            //    //DataSet to ListBox
            //}

            System.Data.DataTable dtable = DataSource.Tables[DataMember];
            string ColumnsData = "";
            foreach (System.Data.DataColumn dColumn in dtable.Columns)
            {
                ColumnsData += dColumn.Caption + ", ";
            }
            System.Diagnostics.Debug.WriteLine(string.Format("DebugPrintDataTable_Column ={0}", ColumnsData));
            foreach (System.Data.DataRow dRow in dtable.Rows)
            {
                ColumnsData = ""; 
                for (int i = 0; i < dtable.Columns.Count; i++)
                {
                    ColumnsData += dRow[i].ToString() + ", ";
                }
                System.Diagnostics.Debug.WriteLine(string.Format("DebugPrintDataTable_DataRow={0}", ColumnsData));
            }

        }

    }
}
