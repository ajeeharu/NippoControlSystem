using NippoControlSystem.ApplicationService.Common;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.Infrastructure.Persistence;
using System.IO;

namespace NippoControlSystem.ApplicationServices.Services
{
    public class InspectionDataService : CsvApplicationServiceBase
    {
        private readonly CsvFileRepository _csvFileRepository;

        public InspectionDataService(CsvFileRepository csvFileRepository)
        {
            _csvFileRepository = csvFileRepository;
        }

        public InspectionDataDto LoadData(string filePath)
        {
            // 1. 存在チェック
            if (!_csvFileRepository.Exists(filePath))
            {
                throw new FileNotFoundException($"CSVファイルが見つかりません: {filePath}");
            }
            // 2. リポジトリ経由でテキスト取得（Shift_JIS等はリポジトリ側で隠蔽済み）
            string? csvContent = _csvFileRepository.ReadAllText(filePath);
            // 3. ParseCsvContent から List<InspectionRecordDto> を受け取る
            List<InspectionRecordDto> recordList = ParseCsvContent(csvContent, values => new InspectionRecordDto
            {
                InspectionDate = values[0],
                ItemName = values[1],
                ResultValue = values[2]
            }, skipHeader: true);

            // または、メソッドの戻り値が InspectionDataDto の場合
            return new InspectionDataDto
            {
                Records = recordList
            };
        }
    }
}