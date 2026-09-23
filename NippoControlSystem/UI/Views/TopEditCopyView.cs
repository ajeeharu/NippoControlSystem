using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class TopEditCopyView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();

        public TopEditCopyView()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ MainTitle
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SubTitle
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ Folder
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Folder
        {
            get;
            set;
        }


        private void frmTopEditCopy_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEditCopy"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEditCopy"]);

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblMainNo_Copyed.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";
            this.lblSubNo_Coped.Text = Default.SubNo;     //"追番";

            ////DataSet
            //this.bindingSource1.DataSource = myDataSetTopMenu;
            //this.bindingSource1.DataMember = "menuMain";
            ////int itemFound = this.bindingSource1.Find("Title", "5C0-00700");
            //int itemFound = this.bindingSource1.Find("MainID", SelectMainNo);
            //if (itemFound >= 0)
            //{
            //    bindingSource1.Position = itemFound;
            //}
            //if(this.textBox_Zuban.DataBindings.Count == 0)
            //    this.textBox_Zuban.DataBindings.Add("Text", this.bindingSource1, "Title");

            //this.bindingSource2.DataSource = myDataSetTopMenu;
            //this.bindingSource2.DataMember = "menuSub";
            ////this.bindingSource2.DataMember = "menuMain.menuMain_menuSub";
            ////this.bindingSource2.DisplayMember = "menuMain.menuMain_menuSub.SubTitle";
            ////this.bindingSource2.DataMember = "menuSub";
            //itemFound = this.bindingSource2.Find("SubID", SelectSubNo);
            //if (itemFound >= 0)
            //{
            //    bindingSource2.Position = itemFound;
            //}
            //if (this.textBox_Edaban.DataBindings.Count == 0)
            //{
            //    this.textBox_Edaban.DataBindings.Add("Text", this.bindingSource2, "SubTitle");
            //    this.textBox_Folder.DataBindings.Add("Text", this.bindingSource2, "Folder");
            //}

            this.textBox_Zuban.Text = MainTitle;
            this.textBox_Edaban.Text = SubTitle;
            this.textBox_Folder.Text = Folder;

            this.textBox_Zuban_Copyed.Text = this.textBox_Zuban.Text;
            this.textBox_Edaban_Copyed.Text = null;
            this.textBox_Folder_Copyed.Text = null;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_Zuban_Copyed.Text))
            {
                System.Windows.Forms.MessageBox.Show("仕様書番号が指定されていません。\n\n仕様書番号を指定して下さい。", Default.ApplicationName);
                return;
            }
            if (string.IsNullOrEmpty(this.textBox_Edaban_Copyed.Text))
            {
                System.Windows.Forms.MessageBox.Show("追番を入力して下さい。", Default.ApplicationName);
                return;
            }
            else
            {
                if (string.Equals(this.textBox_Edaban_Copyed.Text, this.textBox_Edaban.Text))
                {
                    System.Windows.Forms.MessageBox.Show("複製元と違う追番を入力してください。", Default.ApplicationName);
                    return;
                }
            }
            if (string.IsNullOrEmpty(this.textBox_Folder_Copyed.Text))
            {
                System.Windows.Forms.MessageBox.Show("フォルダが指定されていません。\n\nフォルダを指定して下さい。", Default.ApplicationName);
                return;
            }
            else
            {
                if (!System.IO.Directory.Exists(this.textBox_Folder_Copyed.Text))
                {
                    DialogResult dr = System.Windows.Forms.MessageBox.Show("フォルダが見つかりません。\n\n作成しますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                    if (dr != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                    try
                    {
                        System.IO.Directory.CreateDirectory(this.textBox_Folder_Copyed.Text);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("フォルダが作成できません。\n\n別のフォルダを指定して下さい。", Default.ApplicationName);
                        return;
                    }
                }
                else
                {
                    if (string.Equals(this.textBox_Folder.Text, this.textBox_Folder_Copyed.Text))
                    {
                        System.Windows.Forms.MessageBox.Show("このフォルダは指定できません。\n\n別のフォルダを指定して下さい。", Default.ApplicationName);
                        return;
                    }
                }
            }
            //各ファイル名
            string Zuban_Parent = this.textBox_Zuban.Text;
            string Edaban_Parent = this.textBox_Edaban.Text;
            string Folder_Parent = this.textBox_Folder.Text;
//
            string Zuban_Copyed = this.textBox_Zuban_Copyed.Text;
            string Edaban_Copyed = this.textBox_Edaban_Copyed.Text;
            string Folder_Copyed = this.textBox_Folder_Copyed.Text;
            //DataTable dtMain = myDataSetTopMenu.menuMain;
            //DataTable dtSubMain = myDataSetTopMenu.menuSub;
            //コピーを実施、元ファイルがあること、先ファイルがあるときは、確認する
            //検査ファイル(list.dat)
            if (System.IO.File.Exists(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Listdat))
            {
                if (System.IO.File.Exists(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Listdat))
                {
                    DialogResult dr = System.Windows.Forms.MessageBox.Show("検査ファイルが既にあります。\n\n上書きしますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        //上書きは、既にファイルを削除してから、コピーする
                        System.IO.File.Delete(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Listdat);
                        System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Listdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Listdat);
                    }
                }
                else
                {
                    //複製元にあり、コピー先にないときは、普通にコピーする
                    System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Listdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Listdat);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(string.Format("複製元の検査ファイルが見つかりません。\n\nコピーできませんでした。"), Default.ApplicationName);
            }

            //設定ファイル(Checkdat.dat)
            if (System.IO.File.Exists(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Checkdat))
            {
                if (System.IO.File.Exists(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat))
                {
                    DialogResult dr = System.Windows.Forms.MessageBox.Show("設定ファイルが既にあります。\n\n上書きしますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        //上書きは、既にファイルを削除してから、コピーする
                        System.IO.File.Delete(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
                        System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Checkdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
                    }
                }
                else
                {
                    //複製元にあり、コピー先にないときは、普通にコピーする
                    System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Checkdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
                }
            }
            else
            {
                //設定ファイル(Checkdat.dat)がないときは、新規に作成する
                //既にあれば、まず削除する
                if (System.IO.File.Exists(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat))
                {
                    System.IO.File.Delete(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
                }
                //新規に作成する
                mc.newCheckdatFile(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Checkdat, "未設定", textBox_Zuban_Copyed.Text, textBox_Edaban_Copyed.Text, "");
                //メッセージを表示
                System.Windows.Forms.MessageBox.Show(string.Format("複製元の設定ファイルが見つかりません。\n\n新規に作成しました。"), Default.ApplicationName);
            }

            //ポート名ファイル(port.dat)
            if (System.IO.File.Exists(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Portdat))
            {
                if (System.IO.File.Exists(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Portdat))
                {
                    DialogResult dr = System.Windows.Forms.MessageBox.Show("ポート名ファイルが既にあります。\n\n上書きしますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.IO.File.Delete(Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Portdat);
                        System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Portdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Portdat);
                    }
                }
                else
                {
                    //複製元にあり、コピー先にないときは、普通にコピーする
                    System.IO.File.Copy(Folder_Parent + System.IO.Path.DirectorySeparatorChar + Default.Portdat, Folder_Copyed + System.IO.Path.DirectorySeparatorChar + Default.Portdat);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(string.Format("複製元のポート名ファイルが見つかりません。\n\nコピーできませんでした。"), Default.ApplicationName);
            }

            ////DataSetを更新する。
            ////DataRow[] dtSubMainRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", SelectSubNoNew));
            //DataRow[] dtSubMainRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", SelectSubNo));
            //if (dtSubMainRowCurr.Length > 0)
            //{
            //    int CurrentIndexof = dtSubMain.Rows.IndexOf(dtSubMainRowCurr[0]);
            //    //dtSubMainRowCurr[0]["SubTitle"] = Edaban_Copyed; //Edaban_Copyed
            //    //dtSubMainRowCurr[0]["Folder"] = Folder_Copyed;
            //    DataRow dtSubMainRowNew = dtSubMain.NewRow();
            //    dtSubMainRowNew["MainID"] = dtSubMainRowCurr[0]["MainID"];//MainIDに連動させる
            //    dtSubMainRowNew["SubTitle"] = Edaban_Copyed;   // CurrentRowIndex.ToString();
            //    dtSubMainRowNew["Folder"] = Folder_Copyed;
            //    if (CurrentIndexof == -1)
            //    {
            //        dtSubMain.Rows.Add(dtSubMainRowNew);
            //        this.SelectSubNoNew = 0;
            //    }
            //    else
            //    {
            //        dtSubMain.Rows.InsertAt(dtSubMainRowNew, CurrentIndexof + 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
            //        //dataGridView_Edaban.Sort(this.dataGridView_Edaban.Columns["ColumnSubSortID"], sortDirection);
            //        //this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex + 1];
            //        this.SelectSubNoNew = CurrentIndexof + 1;
            //    }
            //}

            MainTitle = this.textBox_Zuban_Copyed.Text;
            SubTitle = this.textBox_Edaban_Copyed.Text;
            Folder = this.textBox_Folder_Copyed.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void frmTopEditCopy_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmTopEditCopy_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmTopEditCopy_FormClosing");
        }

        private void CommandFolderBrowser_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
            //dialogPositioningWindow();
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダの選択";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            //fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (System.IO.Directory.Exists(Default.DataFolder))
            {
                //最初に選択するフォルダを指定する
                //RootFolder以下にあるフォルダである必要がある
                fbd.SelectedPath = Default.DataFolder;
            }
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                Console.WriteLine(fbd.SelectedPath);
                if (string.Equals(this.textBox_Folder.Text, fbd.SelectedPath))
                {
                    System.Windows.Forms.MessageBox.Show("このフォルダは指定できません。\n\n別のフォルダを指定して下さい。", Default.ApplicationName);
                    fbd.Dispose();
                    return;
                }
                textBox_Folder_Copyed.Text = fbd.SelectedPath;
            }
            fbd.Dispose();
        }

        private void dialogPositioningWindow()
        {
            //ディスプレイの高さ
            int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //ディスプレイの幅
            int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            Form dialogPositioningWindow = new Form();
            dialogPositioningWindow.Left = w / 2;
            dialogPositioningWindow.Top = h / 2;
            //dialogPositioningWindow.Width = w /2;
            //dialogPositioningWindow.Height = w /2;
            //dialogPositioningWindow = WindowStyle.None;
            //dialogPositioningWindow.ResizeMode = w /2;
            dialogPositioningWindow.Show();

        }
    }
}