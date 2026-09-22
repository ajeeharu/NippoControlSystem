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
    public partial class VersionView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        public VersionView()
        {
            InitializeComponent();
        }

        private void frmVersion_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmVersion"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmVersion"]);

            // C#
            // バージョン名（AssemblyInformationalVersion属性）を取得
            string appVersion = Application.ProductVersion;
            // 製品名（AssemblyProduct属性）を取得
            string appProductName = Application.ProductName;
            // 会社名（AssemblyCompany属性）を取得
            string appCompanyName = Application.CompanyName;
            // C# AssemblyVersion属性
            System.Reflection.Assembly mainAssembly = System.Reflection.Assembly.GetEntryAssembly();
            System.Reflection.AssemblyName mainAssemName = mainAssembly.GetName();
            // バージョン名（AssemblyVersion属性）を取得
            Version appAssemblyVersion = mainAssemName.Version;
            // コピーライト情報を取得
            string appCopyright = "-";
            object[] CopyrightArray =
              mainAssembly.GetCustomAttributes(
                typeof(System.Reflection.AssemblyCopyrightAttribute), false);
            if ((CopyrightArray != null) && (CopyrightArray.Length > 0))
            {
                appCopyright =
                  ((System.Reflection.AssemblyCopyrightAttribute)CopyrightArray[0]).Copyright;
            }

            // 詳細情報を取得
            string appDescription = "-";
            object[] DescriptionArray =
              mainAssembly.GetCustomAttributes(
                typeof(System.Reflection.AssemblyDescriptionAttribute), false);
            if ((DescriptionArray != null) && (DescriptionArray.Length > 0))
            {
                appDescription =
                  ((System.Reflection.AssemblyDescriptionAttribute)DescriptionArray[0]).Description;
            }
            //Formに表示
            this.Text = string.Format("{0} のバージョン情報", appProductName);
            //
            this.label_CompanyName.Text = appCompanyName;
            this.label_ProductName.Text = appProductName;
            string[] spritedVersion = appVersion.Split('.');
            DateTime da = DateTime.Parse(@"2000年1月1日")+ TimeSpan.FromDays( double.Parse(spritedVersion[2]))+ TimeSpan.FromSeconds(double.Parse(spritedVersion[3])*2d);
            this.label_Version.Text = string.Format("Version  {0}   {1:yyyy/MM/dd  HH:mm:ss}",appVersion, da);
            this.label_Copyright.Text = appCopyright;
            this.label_Description.Text = appDescription;

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVersion_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmVersion_FormClosing");
        }

        private void frmVersion_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}