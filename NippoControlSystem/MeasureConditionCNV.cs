using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class MeasureCondition
    {
        public bool executeiDhiDb(string ListdatFile)
        {
            //sample call convertiDhiDb
            return convertTextD(ListdatFile, convertiDhiDb);
        }

        public bool executeiDbiDh(string ListdatFile)
        {
            //sample call convertiDbiDh
            return convertTextD(ListdatFile, convertiDbiDh);
        }

        public string convertiDhiDb(string contents)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(contents, "(iDh|nDh)"))
            {
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "iDh", "iDb");
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "nDh", "nDb");
                return contents;
            }
            return null;
        }

        public string convertiDbiDh(string contents)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(contents, "(iDb|nDb)"))
            {
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "iDb", "iDh");
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "nDb", "nDh");
                return contents;
            }
            return null;
        }

        public delegate string delegateonvertH2D(string contents);
        public bool convertTextD(string ListdatFile, delegateonvertH2D convertH2D)
        {
            System.IO.FileInfo Listdat = new System.IO.FileInfo(ListdatFile);
            bool returnStat = false;

            //string DirectoryName = null;

            //ListBox1に結果を表示する
            if (Listdat.Exists)
            {
                //20160916 従来と合わせるため、将来削除、そのため完全ディレクトリ制として、Checkdatの内容を書き換えない
                //また、Folderの位置は、親位置となり、SubFolderはFolderに含まない
                //Dir優先、Checkdatの一つ上のフォルダをSubTitle、さらにもう一つ上のフォルダをTitleにする
                //DirectoryName = System.IO.Path.GetDirectoryName(Listdat.FullName);

                string AllText = this.ReadAllText(Listdat, enc);
                string convertedText = convertH2D(AllText);
                if (convertedText != null)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("convH2D ... {0}", Listdat.FullName));
                    //nullのときは、IsMatchなし
                    WriteAllText(Listdat, convertedText, enc);
                    returnStat = true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("No conv ... {0}", Listdat.FullName));
                }
            }
            return returnStat;
        }

        /// <summary>
        /// ReadAllText,System.IO.File.ReadAllTextは、エンコードが正しく読み込まれないようなので、自前で作成
        /// </summary>
        /// 
        public void WriteAllText(System.IO.FileInfo fileInfo3, string contents, System.Text.Encoding enc)
        {
            //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(path);
            System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
            using (System.IO.StreamWriter cWriter = new System.IO.StreamWriter(sr1, enc))
            {
                cWriter.Write(contents);
            }

            return;
        }

        /// <summary>
        /// ReadAllText,System.IO.File.ReadAllTextは、エンコードが正しく読み込まれないようなので、自前で作成
        /// </summary>
        public string ReadAllText(System.IO.FileInfo fileInfo3, System.Text.Encoding enc)
        {
            //string stBuffer = System.IO.File.ReadAllText(CheckdatFile, enc);

            string stBuffer = null; //制御文字(crとか)取り除くため、一旦すべて読み込む20160914

            //System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(fileName);
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
    }
}
