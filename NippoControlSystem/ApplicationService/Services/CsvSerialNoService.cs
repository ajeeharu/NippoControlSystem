using NippoControlSystem.Infrastructure.Persistence;
using System.Text;

namespace NippoControlSystem.ApplicationService.Services
{
    public class CsvSerialNoService
    {
        private readonly FileIoHelper _fileIoHelper;
        private readonly string _serialNoConfigName = "SerialNo.config";
        private readonly Encoding _enc = Encoding.GetEncoding("Shift_JIS");

        // DIコンテナから FileIoHelper を注入してもらう
        public CsvSerialNoService(FileIoHelper fileIoHelper)
        {
            _fileIoHelper = fileIoHelper;
        }

        public string GetSerialNo(string defaultSerialNo)
        {
            string serialNoPath = GetSerialNoPath();
            string? serialNo = null;

            if (_fileIoHelper.Exists(serialNoPath))
            {
                // FileIoHelper を経由して安全に読み込み
                serialNo = _fileIoHelper.ReadAllText(serialNoPath, _enc);
            }

            if (string.IsNullOrEmpty(serialNo))
            {
                serialNo = defaultSerialNo;
            }

            return serialNo.Trim();
        }

        public string AddSerialNo(string defaultSerialNo, int serialSuffixLength, string currSerialNo, string path)
        {
            string serialNo = currSerialNo;
            int numberingLength = 0;

            if (string.IsNullOrEmpty(serialNo))
            {
                serialNo = defaultSerialNo;
                return serialNo;
            }

            for (int i = 0; i < Math.Min(serialSuffixLength, serialNo.Length); i++)
            {
                char digit = serialNo[serialNo.Length - i - 1];
                if (digit >= '0' && digit <= '9')
                {
                    numberingLength = (i + 1);
                }
                else
                {
                    break;
                }
            }

            if (numberingLength > 0)
            {
                string suffix = serialNo.Substring(serialNo.Length - numberingLength, numberingLength);
                string mainNo = serialNo.Substring(0, serialNo.Length - numberingLength);

                if (int.TryParse(suffix, out int suffixDigit))
                {
                    suffixDigit++;  // +1する
                    string stringFormat = string.Format("D{0}", numberingLength);
                    serialNo = mainNo + suffixDigit.ToString(stringFormat);
                }
            }

            SaveSerialNo(serialNo);
            return serialNo;
        }

        public string SaveSerialNo(string serialNo)
        {
            string serialNoPath = GetSerialNoPath();

            // FileIoHelper を経由してファイルに書き込み
            _fileIoHelper.WriteAllText(serialNoPath, serialNo, _enc);

            return serialNo;
        }

        public string GetSerialNoPath()
        {
            return System.IO.Path.GetDirectoryName(AppSettingsProvider.GetSettingPath()) + System.IO.Path.DirectorySeparatorChar
                + _serialNoConfigName;
        }
    }
}