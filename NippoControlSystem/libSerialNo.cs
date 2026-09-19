using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    class libSerialNo
    {
        //static
        static string mSerialNoConfig = @"SerialNo.config";    //SerialNo.config
        //CSVファイルに書き込むときに使うEncoding
        static public System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

        static public string GetSerialNo(string DefaultSerialNo)
        {
            //設定のプロパティ
            string SerialNo = null;
            string SerialNoPath = GetSerialNoPath();

            if (System.IO.File.Exists(SerialNoPath))
            {
                System.IO.FileInfo fileInfo3 = new System.IO.FileInfo(SerialNoPath);
                System.IO.FileStream sr1 = fileInfo3.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);   //ファイルを共有モードで開くための設定
                using (System.IO.StreamReader cReader = new System.IO.StreamReader(sr1, enc))
                {
                    // 読み込みできる文字がなくなるまで繰り返す
                    if (!cReader.EndOfStream)
                    {
                        SerialNo = cReader.ReadLine();
                    }
                }
            }
            if (string.IsNullOrEmpty(SerialNo))
            {
                SerialNo = DefaultSerialNo;
            }

            return SerialNo;
        }

        static public string AddSerialNo(string DefaultSerialNo, int SerialSuffixLength, string CurrSerialNo)
        {
            //設定のプロパティ
            string SerialNo = CurrSerialNo;
            //string SerialNoPath = GetSerialNoPath();
            int NumberingLength = 0;
            if (string.IsNullOrEmpty(SerialNo))
            {
                SerialNo = DefaultSerialNo;
                return SerialNo;
            }
            for (int i = 0; i < Math.Min(SerialSuffixLength, SerialNo.Length); i++)
            {
                char digit =SerialNo[SerialNo.Length-i-1];
                if (digit >= '0' && digit <= '9')
                {
                    NumberingLength = (i+1);
                }
                else
                {
                    break;
                }
            }
            if (NumberingLength > 0)
            {
                string Suffix = SerialNo.Substring(SerialNo.Length - NumberingLength , NumberingLength);
                string MainNo = SerialNo.Substring(0, SerialNo.Length - NumberingLength);
                int Suffixdigit = 0;
                if (int.TryParse(Suffix, out Suffixdigit))
                {
                    Suffixdigit++;  //+1する
                    string stringFormat = string.Format("D{0}",NumberingLength);
                    SerialNo = MainNo + Suffixdigit.ToString(stringFormat);
                }
            }
            SaveSerialNo(SerialNo);

            return SerialNo;
        }

        static public string SaveSerialNo(string SerialNo)
        {
            //設定のプロパティ
            //string SerialNo = GetSerialNo(DefaultSerialNo);
            string SerialNoPath = GetSerialNoPath();

            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(SerialNoPath, false, enc))   //allways append false
            {
                sr.WriteLine(SerialNo);
            }

            return SerialNo;
        }

        public static string GetSerialNoPath()
        {
            //return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar
            //    + Cyc.IO.Settings.mApplicationNameEn + System.IO.Path.DirectorySeparatorChar + Cyc.IO.Settings.mSettingsHolder + mSerialNoConfig;
            return System.IO.Path.GetDirectoryName(Cyc.IO.Settings.GetSettingPath()) + System.IO.Path.DirectorySeparatorChar
                + mSerialNoConfig;
        }
    }
}
