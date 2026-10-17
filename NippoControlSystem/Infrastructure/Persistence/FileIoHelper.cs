using System.IO;
using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class FileIoHelper
    {
        // 読み込み（エンコーディングを必ず外から指定させる）
        public string? ReadAllText(string filePath, Encoding encoding)
        {
            if (!File.Exists(filePath)) return null;

            FileInfo fileInfo = new FileInfo(filePath);
            using (FileStream fs = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader cReader = new StreamReader(fs, encoding))
            {
                return cReader.ReadToEnd();
            }
        }

        // 書き込み
        public void WriteAllText(string filePath, string contents, Encoding encoding)
        {
            File.WriteAllText(filePath, contents, encoding);
        }

        // 追記
        public void AppendLine(string filePath, string line, Encoding encoding)
        {
            using (StreamWriter sr = new StreamWriter(filePath, true, encoding))
            {
                sr.WriteLine(line);
            }
        }

        public void AppendText(string filePath, string text, Encoding encoding)
        {
            using (StreamWriter sr = new StreamWriter(filePath, true, encoding))
            {
                sr.Write(text);
            }
        }

        public bool Exists(string filePath) => File.Exists(filePath);
        public void Delete(string filePath) { if (File.Exists(filePath)) File.Delete(filePath); }
    }
}
