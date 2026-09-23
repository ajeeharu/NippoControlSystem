using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class TopEditInputView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();

        public TopEditInputView()
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

        private void frmTopEditInput_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEditInput"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEditInput"]);

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";

            //DataSet
            this.textBox_Zuban.Text = this.MainTitle;
            this.textBox_Edaban.Text = this.SubTitle;
            this.textBox_Folder.Text = this.Folder;
            this.textBox_Zuban.SelectionStart = 0;
            this.textBox_Zuban.SelectionLength = 0;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(this.textBox_Folder.Text))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("フォルダが見つかりません。\n\n作成しますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(this.textBox_Folder.Text);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("フォルダが作成できません。\n\n別のフォルダを指定して下さい。", Default.ApplicationName);
                        return;
                    }
                }
            }
            this.MainTitle =this.textBox_Zuban.Text;
            this.SubTitle = this.textBox_Edaban.Text;
            this.Folder = this.textBox_Folder.Text;
            this.Close();
        }

        private void frmTopEditInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmTopEditInput_FormClosing");
        }

        private void frmTopEditInput_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
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
                this.textBox_Folder.Text = fbd.SelectedPath;
                //20170126 フォルダ選択時、空なら、仕様書番号と枝番を入れる
                if (string.IsNullOrEmpty(this.textBox_Zuban.Text))
                {
                    try
                    {

                        this.textBox_Zuban.Text = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(fbd.SelectedPath));
                    }
                    finally
                    {
                    }
                }
                if (string.IsNullOrEmpty(this.textBox_Edaban.Text))
                {
                    try
                    {

                        this.textBox_Edaban.Text = System.IO.Path.GetFileName(fbd.SelectedPath);
                    }
                    finally
                    {
                    }
                }
            }
            //Dis
            fbd.Dispose();
        }
    }
}
