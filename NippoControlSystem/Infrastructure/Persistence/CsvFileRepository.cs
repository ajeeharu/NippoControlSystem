using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class CsvFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _shiftJis = Encoding.GetEncoding("Shift_JIS");

        public CsvFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper ?? throw new ArgumentNullException(nameof(ioHelper));
        }

        public string? ReadAllText(string filePath)
        {
            return _ioHelper.ReadAllText(filePath, _shiftJis);
        }

        public void AppendLine(string filePath, string line)
        {
            _ioHelper.AppendLine(filePath, line, _shiftJis);
        }

        public void AppendText(string filePath, string text)
        {
            _ioHelper.AppendText(filePath, text, _shiftJis);
        }

        public void WriteAllText(string filePath, string contents)
        {
            _ioHelper.WriteAllText(filePath, contents, _shiftJis);
        }

        public void Delete(string filePath)
        {
            _ioHelper.Delete(filePath);
        }

        public bool Exists(string filePath)
        {
            return _ioHelper.Exists(filePath);
        }
    }
}