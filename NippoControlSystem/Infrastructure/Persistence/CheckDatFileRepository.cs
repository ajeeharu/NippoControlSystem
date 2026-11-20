using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class CheckDatFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _utf8 = Encoding.UTF8; // JSONはUTF-8
        public CheckDatFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }
        public string? ReadAllText(string filePath)
        {
            return _ioHelper.ReadAllText(filePath, _utf8);
        }
        public void AppendLine(string filePath, string line)
        {
            _ioHelper.AppendLine(filePath, line, _utf8);
        }

        public void AppendText(string filePath, string text)
        {
            _ioHelper.AppendText(filePath, text, _utf8);
        }

        public bool Exists(string filePath)
        {
            return _ioHelper.Exists(filePath);
        }
    }
}
