using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class CheckDatFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        // 既存 Check.dat が Shift_JIS の場合はこちらを使用（新仕様で UTF-8 統一なら Encoding.UTF8 に変更）
        private readonly Encoding _encoding = Encoding.GetEncoding("Shift_JIS");

        public CheckDatFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper ?? throw new ArgumentNullException(nameof(ioHelper));
        }

        public string? ReadAllText(string filePath)
        {
            return _ioHelper.ReadAllText(filePath, _encoding);
        }

        public void WriteAllText(string filePath, string contents)
        {
            _ioHelper.WriteAllText(filePath, contents, _encoding);
        }

        public void AppendLine(string filePath, string line)
        {
            _ioHelper.AppendLine(filePath, line, _encoding);
        }

        public void AppendText(string filePath, string text)
        {
            _ioHelper.AppendText(filePath, text, _encoding);
        }

        public bool Exists(string filePath)
        {
            return _ioHelper.Exists(filePath);
        }

        public void Delete(string filePath)
        {
            _ioHelper.Delete(filePath);
        }

        public void Move(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            _ioHelper.Move(sourceFilePath, destFilePath, overwrite);
        }

        public bool AreFilesIdentical(string filePathA, string filePathB)
        {
            return _ioHelper.AreFilesIdentical(filePathA, filePathB);
        }
    }
}