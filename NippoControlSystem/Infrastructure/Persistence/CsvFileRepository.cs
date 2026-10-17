using System.Text;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class CsvFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _shiftJis = Encoding.GetEncoding("Shift_JIS"); // CSVはShift_JIS

        public CsvFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public string ReadCsvText(string filePath)
        {
            return _ioHelper.ReadAllText(filePath, _shiftJis); // Shift_JISを指定
        }

        public void AppendLine(string filePath, string line)
        {
            _ioHelper.AppendLine(filePath, line, _shiftJis); // Shift_JISを指定
        }

        public void AppendText(string filePath, string text)
        {
            _ioHelper.AppendText(filePath, text, _shiftJis); // Shift_JISを指定
        }

        public bool Exists(string filePath) => _ioHelper.Exists(filePath);
    }
}
