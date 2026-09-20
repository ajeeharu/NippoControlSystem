using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using Cyc.IO;
using System.ComponentModel;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    // NippoDebug Compile Error #5
    [DesignerCategory("code")]
    public partial class MainView : Form
    {
        bool lastHasError = true;   //20140410

        bool withError = false;      //検査結果。
        //System.Diagnostics.Stopwatch restartTimer = new System.Diagnostics.Stopwatch();

        //StopWatch 2017/06/19
        System.Diagnostics.Stopwatch timeOutStopwatch = new System.Diagnostics.Stopwatch();
        //Timeout 毎回Resetするので、Settingを使わない2017/07/03
        //int AutoTimeOut = 1; //Default 1分
        int AutoTimeOut = 2; //Default 2分に変更 20170908
        //CheckDatLMT
        float[] CheckDatLMTLo = new float[(int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1];
        float[] CheckDatLMTHi = new float[(int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1];
        float CheckDatiDsLo = 0f;
        float CheckDatiDsHi = 0f;

        private void inspect_Run(int Tno, MeasureCondition.enumInspectStat stat)
        {
            if (myDataSetItems.ListDat.Count == 0)
            {
                return;     //ListDatが空のときは、開始しない
            }
            this.TNo = Tno;    //testNo
            //ListDatResultを作成する。
            DataTable dtListDatResult = myDataSetItems.ListDatResult;
            dtListDatResult.Rows.Clear();
            for (int i = 0; i < myDataSetItems.ListDat.Count; i++)
            {
                DataRow dtListDatResultNewRow = dtListDatResult.NewRow();
                dtListDatResultNewRow["Title"] = myDataSetItems.ListDat.Rows[i]["Title"];
                dtListDatResultNewRow["Type"] = myDataSetItems.ListDat.Rows[i]["Type"];
                dtListDatResultNewRow["Guide"] = myDataSetItems.ListDat.Rows[i]["Guide"];
                dtListDatResult.Rows.Add(dtListDatResultNewRow);
                //結果をクリア
                myDataSetItems.ListDat.Rows[i]["Result"] = "";
            }
            dtListDatResult.AcceptChanges();
            PowerVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString(); //20170126
            if (PowerVolt == "1")
            {
                aio.SetPower24V();
            }
            else
            {
                aio.SetPower12V();
            }
            //LMT
            int iDo_Length = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;
            string dtCheckDatFields;
            int idx24 = (PowerVolt == "1"? 2:0);
            for (int i = 0; i < iDo_Length; i++)
            {
                CheckDatLMTHi[i] = Default.CheckDatLMT[idx24+0][i];
                CheckDatLMTLo[i] = Default.CheckDatLMT[idx24+1][i];

                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                //CheckDatLMTHi[i] = float.Parse(myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString());    //iOP-HiLMT～iTo-HiLMT
                float.TryParse(myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString(), out CheckDatLMTHi[i]); //20180911 nullに備える20180911-3
                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                //CheckDatLMTLo[i] = float.Parse(myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString());    //iOP-LoLMT～iTo-LoLMT
                float.TryParse(myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString(), out CheckDatLMTLo[i]); //20180911 nullに備える20180911-3
            }
            int idxiDs = ((int)Cyc.IO.NippoDIO.IO_STAT.iDs-(int)Cyc.IO.NippoDIO.IO_STAT.iOP);
            {
                CheckDatiDsHi = CheckDatLMTHi[idxiDs];   //各Step毎のiDs-HiLMT、iDs-LoLMTが空欄に備えて、初期値を保存しておく
                CheckDatiDsLo = CheckDatLMTLo[idxiDs];
            }

            //検査時、電圧と状態を黄色表示にする
            this.textBox_Volt.ForeColor = System.Drawing.Color.Yellow;
            this.textBox_Status.ForeColor = System.Drawing.Color.Yellow;
            this.textBox_Guide.Text = null;
            //画面のボタンを不許可にする
            button_Enable(false);

            //20180831 wav対応
            lastrichText = null;  //20180831
            lastrichTextTno = -1;  //20180831

            mc.InspecStat = stat;
            mc.InspecStartStat = stat;
            TNoRetryNo = 0;     //RetryCounter
            lastHasError = true;    //20170410
            this.textBox_TestNo.Text = TNo.ToString();
            this.textBox_CurrentResult.Text = "";
            this.label_ResultNG.Visible = false;
            this.label_ResultOK.Visible = false;
            withError = false;
            mc.TestStartDT = DateTime.Now;
            //TNoの行を選択する
            this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, TNo];
            this.dataGridView_Inspection.FirstDisplayedScrollingRowIndex = TNo - 13 < 0 ? 0 : TNo - 13;
            this.textBox_TestNo.Text = (TNo + 1).ToString();
            //StopWatchを起動する。規定時間内に終了しないときは、強制NG終了とする
            this.timeOutStopwatch.Restart();
            this.textBoxRemainTime.Visible = true;
            this.labelRemainTime.Visible = true;
        }
        private void inspect_Stop()
        {
            mc.InspecStat = MeasureCondition.enumInspectStat.Stat_FailEND;
            this.timerInspect.Enabled = false;
        }

        private void timerInspect_Tick_Do(object sender, EventArgs e)
        {
            bool hasError = false;      //検査結果。
            bool nowError = false;      //検査結果。 2014040
            int loopEnd = 0;
            //const int NoSwitch = 0;
            const int GreenSwitch = 1;
            const int RedSwitch = 2;
            const int TimeoutSwitch = 3;

            //残り時間表示
            this.textBoxRemainTime.Text = ((this.AutoTimeOut * 60000 - this.timeOutStopwatch.ElapsedMilliseconds + 800) / 1000).ToString();     //+500 四捨五入のため 20180818
            this.textBoxRemainTime.Refresh();

            //検査条件で
            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_STOP ||
                mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalEND ||
                mc.InspecStat == MeasureCondition.enumInspectStat.Stat_FailEND)
            {
                if ((GreenSwitchDown())) //20170126
                {
                    stopButtonClick = false;
                    inspect_Run(0, MeasureCondition.enumInspectStat.Stat_NormalStart);
                }
#if DEVICE_DEBUG
                inspect_Run(0, MeasureCondition.enumInspectStat.Stat_NormalStart);
#endif
                return;
            }
            DataTable dtListDatResult = myDataSetItems.ListDatResult;
            DataTable dtListDat = myDataSetItems.ListDat;

            while (true)
            {
                DataRow dtListDatROW = dtListDat.Rows[TNo];
                DataRow dtListDatResultRow = dtListDatResult.Rows[TNo];
                //this.textBox_Guide.Text = dtListDatResultRow["Guide"].ToString();
                //this.textBox_Guide.Text = dtListDatROW["Guide"].ToString();     //20170112
                richTextConvert(this.textBox_Guide,  dtListDatROW["Guide"].ToString());     //20180831
                string InsType = dtListDatResultRow["Type"].ToString();
                this.textBox_InspectType.Text = getInspectType(dtListDatROW["Type"].ToString());

                if (InsType == "1" && TNoRetryNo == 0)
                {
                    //一時停止の初回なら、
                    //PlaySoundPause();
                    PlaySoundPause(PlaySoundWav);   //20180907
                }

                //hasError = inspect_Exec(TNo);      //とりあえず、検査する。
                nowError = inspect_Exec(TNo);      //とりあえず、検査する。
                hasError = nowError || lastHasError;    //連続Passのとき、Passとする。（今回か前回がエラーなら、今回エラーとする。）20170410
                lastHasError = nowError;

                textBox_CurrentResult.Text = hasError ? "Fail" : "Pass";
                if (TNo == 62)
                {
                    Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", string.Format("timerInspect_Tick TNo={0}", TNo));
                }
                //20170410 １回目は判定しない。（時間経過と共に、PassからFailに変化する場合があるので、初回判定しないで、次回から判定することでちょっと待つことになる）
                if (TNoRetryNo == 0)
                {
                    //初回なら、
                    TNoRetryNo++;
                    return;
                }

                if (RedSwitchDown())   //赤ボタン（中止）が押された？
                {
                    loopEnd = RedSwitch;
                    //次のステップへ
                }
                else if (InsType == "0")
                {
                    //通常
                    if (hasError)
                    {
                        TNoRetryNo++;
                        if (TNoRetryNo < Default.InspectRetryMax)
                        {
                            return;     //次回
                        }
                        else
                        {
                            //次のステップへ
                        }
                    }
                    else
                    {
                        //次のステップへ
                    }
                }
                else if (InsType == "1")
                {
                    if ((GreenSwitchDown()) /* && hasError == false */) //20160728立会コメントにより、falseでもボタン可とする。
                    {
                        loopEnd = GreenSwitch;
                        //次のステップへ
                    }
                    else if (RedSwitchDown())
                    {
                        loopEnd = RedSwitch;
                        //次のステップへ
                    }
                    else if (this.timeOutStopwatch.ElapsedMilliseconds >= (this.AutoTimeOut * 60000))    //20170619 時間経過で強制NG
                        {
                            //loopEnd = RedSwitch;
                            loopEnd = TimeoutSwitch;    //20170703
                            //次のステップへ
                        }
                    else
                    {
                        TNoRetryNo = 1;
                        return;
                    }
                }
                else //(InsType == "2")
                {
                    if ((GreenSwitchDown())) //20160915 繰り返しでも、次へで次に進む
                    {
                        loopEnd = GreenSwitch;
                        //次のステップへ
                    }
                    else if (RedSwitchDown())    //繰り返しのとき、停止ボタンを先に見る20160915
                    {
                        loopEnd = RedSwitch;
                        //次のステップへ
                    }
                    else if (this.timeOutStopwatch.ElapsedMilliseconds >= (this.AutoTimeOut * 60000))  //20170619 時間経過で強制NG
                    {
                        //loopEnd = RedSwitch;
                        loopEnd = TimeoutSwitch;    //20170703
                        //次のステップへ
                    }
                    //繰り返し
                    else if (hasError)
                    {
                        TNoRetryNo = 1;
                        return;     //次回
                    }
                    else
                    {
                        //次のステップへ
                        if (hasError  == false)
                        {
                            //音を鳴らす 20170417
                            //PlaySoundPause();
                            PlaySoundPause(PlaySoundWav);   //20180907
                        }
                    }
                }
                //検査結果を保存する
                //強制か、エラーなしのときは、検査の最後なら終了
                dtListDatROW["Result"] = (hasError ? "Fail" : "Pass");
                dtListDatResultRow["Result"] = (hasError ? "Fail" : "Pass");
                dtListDatROW.AcceptChanges();
                dtListDatResultRow.AcceptChanges();
                //次のステップへ、エラーなら終了、パスなら次へ
                if (loopEnd == RedSwitch)   //赤ボタン（中止）が押された？
                {
                    dtListDatROW["Result"] = "Stop";      //従来通りにする 20180906 FailENDと区別する
                    dtListDatResultRow["Result"] = "Stop";
                    //inspectEnd(MeasureCondition.enumInspectStat.Stat_FailEND);
                    inspectEnd(MeasureCondition.enumInspectStat.Stat_ManualEND);    //20180906 手動END
                    break;
                }
                else if (loopEnd == TimeoutSwitch)   //Timeout？　2017/07/03
                {
                    inspectEnd(MeasureCondition.enumInspectStat.Stat_TimeOut);
                    break;
                }
                else if (hasError && mc.InspecStat != MeasureCondition.enumInspectStat.Stat_ForceToEnd)
                {
                    //エラー発生で、強制でないときは終了
                    inspectEnd(MeasureCondition.enumInspectStat.Stat_FailEND);
                    break;
                }
                else
                {
                    if (hasError) withError = true;
                    if (TNo < (dtListDatResult.Rows.Count - 1))
                    {
                        TNo++;      //次の検査へ
                        TNoRetryNo = 0;
                        lastHasError = true;    //20170410
                        //TNoの行を選択する
                        libDataGridView.dataGridView_SelectionClear(this.dataGridView_Inspection);
                        this.dataGridView_Inspection.Rows[TNo].Selected = true;   //20170114
                        this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, TNo];
                        this.dataGridView_Inspection.FirstDisplayedScrollingRowIndex = TNo - 13 < 0 ? 0 : TNo - 13;
                        this.textBox_TestNo.Text = (TNo + 1).ToString();
                        //念のため、Power切り替えを実施
                        PowerVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString(); //20170126
                        if (PowerVolt == "1")
                        {
                            aio.SetPower24V();
                        }
                        else
                        {
                            aio.SetPower12V();
                        }
                        //StopWatchを起動する。規定時間内に終了しないときは、強制NG終了とする
                        this.timeOutStopwatch.Restart();
                    }
                    else
                    {
                        //完了
                        if (withError)
                        {
                            inspectEnd(MeasureCondition.enumInspectStat.Stat_FailEND);
                        }
                        else
                        {
                            inspectEnd(MeasureCondition.enumInspectStat.Stat_NormalEND);
                        }
                        break;
                    }
                }
            }
        }

        private void inspectEnd(MeasureCondition.enumInspectStat stat)
        {
            string strStat;
            //if (stat == MeasureCondition.enumInspectStat.Stat_TimeOut)
            //{
            //    mc.InspecStat = MeasureCondition.enumInspectStat.Stat_FailEND;
            //}
            //else
            //{
            //    mc.InspecStat = stat;
            //}
            //if (stat == MeasureCondition.enumInspectStat.Stat_NormalEND)
            //{
            //    strStat = "Pass";
            //    this.label_ResultOK.Visible = true;
            //    PlaySoundOk();
            //}
            //else
            //{
            //    strStat = "Fail";
            //    this.label_ResultNG.Visible = true;
            //    //dtListDatROW["Result"] = "Stop";      //従来通りにする
            //    //dtListDatResultRow["Result"] = "Stop";
            //    PlaySoundNg();
            //}
            switch (stat)
            {
                case MeasureCondition.enumInspectStat.Stat_NormalEND:
                    mc.InspecStat = MeasureCondition.enumInspectStat.Stat_NormalEND;    //20180914-1
                    strStat = "Pass";
                    this.label_ResultOK.Visible = true;
                    PlaySoundOk();
                    break;

                case MeasureCondition.enumInspectStat.Stat_TimeOut:
                    mc.InspecStat = MeasureCondition.enumInspectStat.Stat_FailEND;
                    strStat = "Fail";
                    this.label_ResultNG.Visible = true;
                    PlaySoundNg();
                    break;

                case MeasureCondition.enumInspectStat.Stat_ManualEND:
                    mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
                    strStat = "中断";
                    //this.label_ResultNG.Visible = true;   //手動中断時は、○×表示をしない。サウンドを鳴らさない
                    //PlaySoundNg();                    
                    break;

                default:
                    mc.InspecStat = stat;
                    strStat = "Fail";
                    this.label_ResultNG.Visible = true;
                    PlaySoundNg();
                    break;
            }

            this.textBox_Guide.Text = strStat;
            //ボタンを元に戻す
            button_Enable(true);
            //検査終了時、電圧と状態を水色表示に戻す
            this.textBox_Volt.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Status.ForeColor = System.Drawing.Color.Cyan;
            mc.TestEndDT = DateTime.Now;
            //出力をクリアする
            allClear();
            //StopWatchを停止する。
            this.timeOutStopwatch.Stop();   //2017/07/03

            //if (stat == MeasureCondition.enumInspectStat.Stat_NormalEND)
            //{
            //    //this.textBox_Serial.Text = libSerialNo.AddSerialNo(Default.Serial,Default.SerialSuffixLength, this.textBox_Serial.Text);
            //}
            //else
            //{
            //    if (stat == MeasureCondition.enumInspectStat.Stat_TimeOut)
            //    {
            //        System.Windows.Forms.MessageBox.Show("タイムアウトが発生しました", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    }
            //    else
            //    {
            //        button_ResultView_Click(dataGridView_Inspection, new EventArgs());
            //    }
            //}
            switch (stat)
            {
                case MeasureCondition.enumInspectStat.Stat_NormalEND:
                    break;

                case MeasureCondition.enumInspectStat.Stat_ManualEND:
                    break;

                case MeasureCondition.enumInspectStat.Stat_TimeOut:
                    System.Windows.Forms.MessageBox.Show("タイムアウトが発生しました", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;

                default:
                    button_ResultView_Click(dataGridView_Inspection, new EventArgs());
                    break;
            }

            //restartTimer.Restart();
            //StopWatchを停止する。
            //this.timeOutStopwatch.Stop();
            this.textBoxRemainTime.Visible = false;
            this.labelRemainTime.Visible = false;
        }

        private string getInspectType(string InspectType)
        {
            int inst_Type;
            if (int.TryParse(InspectType, out inst_Type))
            {
                if (inst_Type >= 0 && inst_Type < Default.inspection_TypeText.Length)
                {
                    return Default.inspection_TypeText[inst_Type];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private bool inspect_Exec(int TNo)
        {
            //DOを実行
            string DioStat = "";
            bool hasError = false;
            DataTable dtListDat = myDataSetItems.ListDat;
            DataTable dtListDatResult = myDataSetItems.ListDatResult;
            DataRow dtListDatRow = dtListDat.Rows[TNo];
            DataRow dtListDatResultRow = dtListDatResult.Rows[TNo];
            int iPOS = 0;
            int iRet = 0;
            string iFeildName = "";
            string iFeildName2 = "";
            float ScannerValue;
            float AiData;

            float[] AiAll = new float[Default.AI2DI_BDmax * Default.AI2DI_AImax];
            iRet = ai2di.MultiAiEx(AiAll);  //New AI

            //DO Start
            Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec -0", "DO Start");
            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop for (int j = 0; j < Default.DioNums[i]; j++)
                {
                    int jj = j + 1;
                    iPOS = i * Default.DioNums[0] + j;
                    iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    DioStat = dtListDatRow[iFeildName].ToString();
                    iRet = nio.NippoDIO_OUT(iPOS, (Cyc.IO.NippoDIO.IO_STAT)mc.dicDioStat[DioStat]);
                }
            }
            //DI Start
            //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", "DI Start");
            //DIを実行
            Cyc.IO.NippoDIO.IO_STAT statCurr = Cyc.IO.NippoDIO.IO_STAT.ERR;
            Cyc.IO.NippoDIO.IO_STAT statEcho = Cyc.IO.NippoDIO.IO_STAT.ERR;
            //Cyc.IO.NippoDIO.IO_STAT statIN = Cyc.IO.NippoDIO.IO_STAT.ERR;
            string errStat = "";
            //bool errLimitStat = false;

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop for (int j = 0; j < Default.DioNums[i]; j++)
                {
                    int jj = j + 1;
                    iPOS = i * Default.DioNums[0] + j;
                    iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    DioStat = dtListDatRow[iFeildName].ToString();
                    if (!mc.dicDioStat.ContainsKey(DioStat))
                    {
                        DioStat = "";
                    }
                    //iDs ステップ毎の上下限値を設定,Nullの時は、Checkdatの初期値を設定
                    if (string.IsNullOrEmpty(dtListDatRow["iDs-LoLMT"].ToString()))
                    {
                        CheckDatLMTLo[(int)Cyc.IO.NippoDIO.IO_STAT.iDs - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = CheckDatiDsLo;
                    }
                    else
                    {
                        CheckDatLMTLo[(int)Cyc.IO.NippoDIO.IO_STAT.iDs - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = float.Parse(dtListDatRow["iDs-LoLMT"].ToString());  //iDs-LoLMT
                    }
                    if (string.IsNullOrEmpty(dtListDatRow["iDs-HiLMT"].ToString()))
                    {
                        CheckDatLMTHi[(int)Cyc.IO.NippoDIO.IO_STAT.iDs - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = CheckDatiDsHi;
                    }
                    else
                    {
                        CheckDatLMTHi[(int)Cyc.IO.NippoDIO.IO_STAT.iDs - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = float.Parse(dtListDatRow["iDs-HiLMT"].ToString());  //iDs-HiLMT
                    }
                    statCurr = (Cyc.IO.NippoDIO.IO_STAT)mc.dicDioStat[DioStat]; //現在の設定
                    //各端子のモードとEchoがあっていることを確認
                    iRet = nio.NippoDIO_Echo(iPOS, out statEcho);  //IOのEcho
                    switch (statCurr)
                    {
                        case Cyc.IO.NippoDIO.IO_STAT.iOP:
                        case Cyc.IO.NippoDIO.IO_STAT.iGN:
                        case Cyc.IO.NippoDIO.IO_STAT.iHi:
                        case Cyc.IO.NippoDIO.IO_STAT.nHi:
                        case Cyc.IO.NippoDIO.IO_STAT.nOP:
                        case Cyc.IO.NippoDIO.IO_STAT.nGN:
                            //case Cyc.IO.NippoDIO.IO_STAT.dV:
                            //iRet = ai2di.SingleAiEx(iPOS, out ScannerValue);  //New AI
                            ScannerValue = AiAll[iPOS];
                            AiData = ScannerValue * Default.AI2V_A + Default.AI2V_B;
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.iDo:
                        case Cyc.IO.NippoDIO.IO_STAT.iDh:
                        case Cyc.IO.NippoDIO.IO_STAT.iDb:
                        case Cyc.IO.NippoDIO.IO_STAT.iDc:
                        case Cyc.IO.NippoDIO.IO_STAT.iDs:
                        case Cyc.IO.NippoDIO.IO_STAT.iDp:
                        case Cyc.IO.NippoDIO.IO_STAT.iTo:
                        case Cyc.IO.NippoDIO.IO_STAT.nDo:
                        case Cyc.IO.NippoDIO.IO_STAT.nDh:
                        case Cyc.IO.NippoDIO.IO_STAT.nDb:
                        case Cyc.IO.NippoDIO.IO_STAT.nDc:
                        case Cyc.IO.NippoDIO.IO_STAT.nDs:
                        case Cyc.IO.NippoDIO.IO_STAT.nDp:
                        case Cyc.IO.NippoDIO.IO_STAT.nTo:
                        case Cyc.IO.NippoDIO.IO_STAT.dat:
                            //case Cyc.IO.NippoDIO.IO_STAT.dmA:
                            //iRet = ai2di.SingleAiEx(iPOS, out ScannerValue);  //New AI
                            ScannerValue = AiAll[iPOS];
                            AiData = ScannerValue * Default.AI2I_A + Default.AI2I_B;
                            //Get Limit for Point
                            if (statCurr == Cyc.IO.NippoDIO.IO_STAT.iDp || statCurr == Cyc.IO.NippoDIO.IO_STAT.nDp)
                            {
                                CheckDatLMTLo[(int)Cyc.IO.NippoDIO.IO_STAT.iDp - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = float.Parse(dtListDatRow[iFeildName + "L"].ToString());  //iOP-LoLMT
                                CheckDatLMTHi[(int)Cyc.IO.NippoDIO.IO_STAT.iDp - (int)Cyc.IO.NippoDIO.IO_STAT.iOP] = float.Parse(dtListDatRow[iFeildName + "H"].ToString());  //iOP-HiLMT
                            }
                            break;

                        default:
                            AiData = -1;
                            break;
                    }

                    errStat = null;
                    int StatIdx = 0;
                    //errLimitStat = false;
                    switch (statCurr)
                    {
                        case Cyc.IO.NippoDIO.IO_STAT.oOP:
                            if (statEcho != statCurr /* || statIN != Cyc.IO.NippoDIO.IO_STAT.oOP */)  //oOPはstatINを判断しない
                            {
                                errStat = statEcho.ToString();
                            }
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.oGN:
                            if (statEcho != statCurr /* || statIN != Cyc.IO.NippoDIO.IO_STAT.iGN*/)  //20170107 oGN時、同時入力はなくなったので、statINを判断しない
                            {
                                errStat = statEcho.ToString();
                            }
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.oHi:
                            if (statEcho != statCurr /* || statIN != Cyc.IO.NippoDIO.IO_STAT.iHi*/)  //20170107 oHi時、同時入力はなくなったので、statINを判断しない 
                            {
                                errStat = statEcho.ToString();
                            }
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.iOP:
                        case Cyc.IO.NippoDIO.IO_STAT.iGN:
                        case Cyc.IO.NippoDIO.IO_STAT.iHi:
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.iOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iRD)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.nOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.nOP:
                        case Cyc.IO.NippoDIO.IO_STAT.nGN:
                        case Cyc.IO.NippoDIO.IO_STAT.nHi:
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.nOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iRD)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.iDo:
                        case Cyc.IO.NippoDIO.IO_STAT.iDh:
                        case Cyc.IO.NippoDIO.IO_STAT.iDb:
                        case Cyc.IO.NippoDIO.IO_STAT.iDc:
                        case Cyc.IO.NippoDIO.IO_STAT.iDs:
                        case Cyc.IO.NippoDIO.IO_STAT.iDp:
                        //case Cyc.IO.NippoDIO.IO_STAT.iTo:
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.iOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iDRD)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.nOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            //for DEBUG
                            if (iFeildName == "DB-04")
                            {
                                Console.WriteLine(string.Format("{0} frmMainInsp.cs:inspect_Exec:{1}:{2}", System.DateTime.Now.ToLongTimeString(), iFeildName, dtListDatResultRow[iFeildName + "L"]));
                            }

                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.iTo:   //2018/0921-1 iToは、oOpで出力している
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.iOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.oOP)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.nOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            //for DEBUG
                            if (iFeildName == "DB-04")
                            {
                                Console.WriteLine(string.Format("{0} frmMainInsp.cs:inspect_Exec:{1}:{2}", System.DateTime.Now.ToLongTimeString(), iFeildName, dtListDatResultRow[iFeildName + "L"]));
                            }

                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.nDo:
                        case Cyc.IO.NippoDIO.IO_STAT.nDh:
                        case Cyc.IO.NippoDIO.IO_STAT.nDb:
                        case Cyc.IO.NippoDIO.IO_STAT.nDc:
                        case Cyc.IO.NippoDIO.IO_STAT.nDs:
                        case Cyc.IO.NippoDIO.IO_STAT.nDp:
                        //case Cyc.IO.NippoDIO.IO_STAT.nTo:
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.nOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iDRD)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            break;

                        case Cyc.IO.NippoDIO.IO_STAT.nTo:       //20180921 iTo/nToはoOpで出力している。
                            StatIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.nOP;
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.oOP)
                            {
                                errStat = statEcho.ToString();
                            }
                            else
                            {
                                if (!(AiData <= CheckDatLMTHi[StatIdx] && AiData >= CheckDatLMTLo[StatIdx]))
                                {
                                    errStat = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + StatIdx)).ToString();
                                    //errLimitStat = true;
                                }
                            }
                            dtListDatResultRow[iFeildName + "H"] = CheckDatLMTHi[StatIdx].ToString("F1");
                            dtListDatResultRow[iFeildName + "L"] = CheckDatLMTLo[StatIdx].ToString("F1");
                            break;


                        //case Cyc.IO.NippoDIO.IO_STAT.dmA:
                        case Cyc.IO.NippoDIO.IO_STAT.dat:
                            if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iDRD)
                            {
                                errStat = statEcho.ToString();
                            }
                            break;

                        //case Cyc.IO.NippoDIO.IO_STAT.dV:
                        //    if (statEcho != Cyc.IO.NippoDIO.IO_STAT.iDRD)
                        //    {
                        //        errStat = statEcho.ToString();
                        //    }
                        //    break;

                        default:
                            //その他？20180806
                            if (statCurr != Cyc.IO.NippoDIO.IO_STAT.NoOP)
                            {
                                errStat = statEcho.ToString();
                            }
                            break;
                    }

                    if (errStat == null)
                    {
                        if (AiData == -1.0f)
                        {
                            dtListDatResultRow[iFeildName] = "";
                        }
                        else
                        {
                            dtListDatResultRow[iFeildName] = string.Format(":{0:F1}", AiData);
                        }
                    }
                    else
                    {
                        //dtListDatResultRow[iFeildName] = errStat;
                        //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", string.Format("DI:Fail iFeildName={0},Stat={1}", iFeildName, stat.ToString()));
                        if (AiData == -1.0f)
                        {
                            dtListDatResultRow[iFeildName] = errStat;
                        }
                        else
                        {
                            dtListDatResultRow[iFeildName] = string.Format("{0}:{1:F1}", errStat, AiData);
                        }
                        hasError = true;
                    }
                }
            }
                
            //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", "AO Start");
            //AOを実行
            float AO_Value;
            string AO_String = "";

            for (int j = 0; j < Default.AoNum; j++)    //32loop
            {
                int jj = j + 1;
                iFeildName = string.Format("{0}-{1:0}", Default.AoName, jj);
                AO_String = dtListDatRow[iFeildName].ToString();
                if (string.IsNullOrEmpty(AO_String))
                {
                    AO_Value = 0f;
                }
                else
                {
                    if (!float.TryParse(AO_String, out AO_Value))
                    {
                        AO_Value = 0f;
                    }
                }
                aio.SingleAoEx(j, AO_Value);
            }
            //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", "AO_Switch Start");
            //AO_Switch
            for (int j = 0; j < Default.AoSwichNum; j++)    //32loop
            {
                int jj = j + 1;
                iFeildName = string.Format("{0}-{1:0}", Default.AoSwichName, jj);
                AO_String = dtListDatRow[iFeildName].ToString();
                if (AO_String == Default.AoSwichStat[1])    //20161013-1
                {
                    ai2di.NippoAIO_SW(j, 1);      //ON
                }
                else
                {
                    ai2di.NippoAIO_SW(j, 0);      //OFF
                }
            }
            //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", "AI Start");
            //AI
            //float[] AiData = new float[8];
            float AiLowValue = float.MinValue;
            float AiUpperValue = float.MaxValue;
            string AiLowString;
            string AiUpperString;
            //int ret = aio.MultiAi(AiData);
            for (int j = 0; j < Default.AiNum; j++)
            {
                int jj = j + 1;
                errStat = "";
                iFeildName = string.Format("{0}-{1:0}L", Default.AiName, jj);
                AiLowString = dtListDatRow[iFeildName].ToString();
                iFeildName2 = string.Format("{0}-{1:0}H", Default.AiName, jj);
                AiUpperString = dtListDatRow[iFeildName2].ToString();
                dtListDatResultRow[iFeildName] = null;  //ErrorClear

                if (!string.IsNullOrEmpty(AiLowString))
                {
                    if (!float.TryParse(AiLowString, out AiLowValue))
                    {
                        AiLowValue = float.MinValue;
                    }
                    if (AiDataAve[j] < AiLowValue)
                    {
                        errStat = "Low";
                    }
                }
                if (!string.IsNullOrEmpty(AiUpperString))
                {
                    if (!float.TryParse(AiUpperString, out AiUpperValue))
                    {
                        AiUpperValue = float.MaxValue;
                    }
                    if (AiDataAve[j] > AiUpperValue)
                    {
                        errStat = "High";
                    }
                }
                if (errStat != "")
                {
                    //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", string.Format("AI:Fail iFeildName={0},Value {1}<{2}<{3}", iFeildName, AiLowValue, AiDataAve[j], AiUpperValue));
                    dtListDatResultRow[iFeildName] = errStat;   //Low側にNGワード
                    hasError = true;
                }
                if (!string.IsNullOrEmpty(AiLowString) || !string.IsNullOrEmpty(AiUpperString))
                {
                    dtListDatResultRow[iFeildName2] = string.Format("{0:F1}", AiDataAve[j]);    //Upper側に数値（常時）
                }
            }
            //Cyc.IO.Log.WriteLine(Main_LogLevel, "inspect_Exec", "AI End");

            dtListDatResultRow.AcceptChanges();

            return hasError;
        }

        private int allClear()
        {
            int iPOS;
            int iRet = 0; ;
            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    int jj = j + 1;
                    iPOS = i * Default.DioNums[0] + j;
                    //oOP
                    iRet |= nio.NippoDIO_OUT(iPOS, Cyc.IO.NippoDIO.IO_STAT.oOP);
                }
            }
            //AOを実行
            float AO_Value = 0f;

            for (int j = 0; j < Default.AoNum; j++)    //32loop
            {
                iRet |= aio.SingleAoEx(j, AO_Value);
            }
            //AO_Switch
            for (int j = 0; j < Default.AoSwichNum; j++)    //32loop
            {
                iRet |= ai2di.NippoAIO_SW(j, 0);      //OFF
            }
            iRet |= aio.SetPower12V();  //12V
            return iRet;
        }

        string lastrichText = null;  //20180831
        int lastrichTextTno = -1;  //20180831
        string PlaySoundWav = null; //20180906   Guideに*.wavがあったら、wavファイルが入る。通常のwavの代わりに鳴らす
        private void richTextConvert(System.Windows.Forms.RichTextBox RichTextBox1, string baseText)
        {
            //System.Windows.Forms.RichTextBox RichTextBox1 = this.richTextBox1;
            //string baseText = textBox1.Text;
            if (lastrichText == baseText && lastrichTextTno == TNo) return;
            lastrichText = baseText;
            lastrichTextTno = TNo;
            PlaySoundWav = null;

            //Regexオブジェクトを作成
            System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(
                    @"\{.*?\}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //BoidをFontStyleに追加したFontを作成する
            Font baseFont = RichTextBox1.SelectionFont;
            Font fnt = new Font(baseFont.FontFamily,
                baseFont.Size,
                baseFont.Style | FontStyle.Bold);

            RichTextBox1.Text = null;
            while (baseText.Length >= 0)
            {
                //TextBox1.Text内で正規表現と一致する対象を1つ検索
                System.Text.RegularExpressions.Match m = r.Match(baseText);

                int Pos = -1;
                if ((m.Success))
                {
                    Pos = baseText.IndexOf(m.Value);
                    if (Pos == -1) break;   // 基本的に、ここにはこないはず
                    string pretext = baseText.Substring(0, Pos);
                    baseText = baseText.Substring(Pos + m.Value.Length);

                    string keyWord = m.Value.Substring(1);  //先頭の｛を取る
                    keyWord = keyWord.Substring(0, keyWord.Length - 1); //最後の｝を取る

                    if (keyWord.Length >= 5 && keyWord.Substring(keyWord.Length - 4).ToUpper() == ".WAV")    //a.wav?
                    {
                        //文字列を挿入する
                        RichTextBox1.SelectedText = pretext;    //20180913 {*.wav}の前を表示する
                        //ここで音を鳴らす
                        PlaySoundWav = keyWord;
                    }
                    else
                    {
                        Color color1 = Color.Empty;
                        try
                        {
                            //色コードとして処理
                            color1 = ColorTranslator.FromHtml(keyWord);
                            if (color1 == Color.Black)
                            {
                                color1 = Color.FromArgb(255, 255, 128);
                            }
                        }
                        catch
                        {
                        }
                        //文字列を挿入する
                        RichTextBox1.SelectedText = pretext;

                        //赤にする
                        //RichTextBox1.SelectionColor = Color.Red;
                        if (color1 != Color.Empty)
                        {
                            RichTextBox1.SelectionColor = color1;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //最後の文字列を挿入する
            if (baseText.Length > 0)
            {
                RichTextBox1.SelectedText = baseText;
            }
        }

     
        ////OK.WAVファイルを再生する-> PlaySoundPause()に統合
        //private void PlaySound1(string WavFileName)
        //{
        //    string wavPath = Default.ApplicationFloder + Default.SettingsHolder + WavFileName;
        //    if (Default.PlaySoundEnable[0] == 'Y')
        //    {
        //        if (System.IO.File.Exists(wavPath))
        //        {
        //            PlaySound(wavPath);
        //        }
        //    }
        //}
    }
}
