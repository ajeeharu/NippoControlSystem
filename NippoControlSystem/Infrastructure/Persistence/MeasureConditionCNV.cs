using System.IO;
using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public partial class MeasureCondition
    {
        public bool executeiDhiDb(string ListdatFile)
        {
            return convertTextD(ListdatFile, convertiDhiDb);
        }

        public bool executeiDbiDh(string ListdatFile)
        {
            return convertTextD(ListdatFile, convertiDbiDh);
        }

        public string convertiDhiDb(string contents)
        {
            if (string.IsNullOrEmpty(contents)) return null;

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
            if (string.IsNullOrEmpty(contents)) return null;

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
            var listdat = new FileInfo(ListdatFile);
            if (!listdat.Exists) return false;

            string allText = this.ReadAllText(listdat, enc);
            string convertedText = convertH2D(allText);

            if (convertedText != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("convH2D ... {0}", listdat.FullName));
                WriteAllText(listdat, convertedText, enc);
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("No conv ... {0}", listdat.FullName));
                return false;
            }
        }

        // 改善版 WriteAllText
        public void WriteAllText(FileInfo fileInfo3, string contents, Encoding enc)
        {
            using (var fs = fileInfo3.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var cWriter = new StreamWriter(fs, enc))
            {
                cWriter.Write(contents);
            }
        }

        // 改善版 ReadAllText (FileInfo オーバーロード)
        public string ReadAllText(FileInfo fileInfo3, Encoding enc)
        {
            if (!fileInfo3.Exists) return null;

            using (var fs = fileInfo3.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var cReader = new StreamReader(fs, enc))
            {
                return cReader.ReadToEnd();
            }
        }
    }
}