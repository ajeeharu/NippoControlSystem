using System.IO;
using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class FileIoHelper
    {
        private static readonly Encoding DefaultEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        // 読み込み（省略時はUTF-8、CSVなど指定が必要なときだけEncodingを渡す）
        public string? ReadAllText(string filePath, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }

            var targetEncoding = encoding ?? DefaultEncoding;
            var fileInfo = new FileInfo(filePath);
            using var fs = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fs, targetEncoding);
            return reader.ReadToEnd();
        }

        // 書き込み
        public void WriteAllText(string filePath, string contents, Encoding? encoding = null)
        {
            File.WriteAllText(filePath, contents, encoding ?? DefaultEncoding);
        }

        // 追記（1行）
        public void AppendLine(string filePath, string line, Encoding? encoding = null)
        {
            using var writer = new StreamWriter(filePath, append: true, encoding ?? DefaultEncoding);
            writer.WriteLine(line);
        }

        // 追記（テキスト）
        public void AppendText(string filePath, string text, Encoding? encoding = null)
        {
            using var writer = new StreamWriter(filePath, append: true, encoding ?? DefaultEncoding);
            writer.Write(text);
        }

        public bool Exists(string filePath) =>
            !string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath);

        public void Delete(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void Move(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            File.Move(sourceFilePath, destFilePath, overwrite);
        }

        public bool AreFilesIdentical(string filePathA, string filePathB)
        {
            if (!Exists(filePathA) || !Exists(filePathB))
            {
                return false;
            }

            if (string.Equals(filePathA, filePathB, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            using var fsA = new FileStream(filePathA, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var fsB = new FileStream(filePathB, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (fsA.Length != fsB.Length)
            {
                return false;
            }

            int byteA;
            int byteB;
            do
            {
                byteA = fsA.ReadByte();
                byteB = fsB.ReadByte();
            }
            while (byteA == byteB && byteA != -1);

            return byteA == byteB;
        }
    }
}