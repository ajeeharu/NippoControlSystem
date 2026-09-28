using NippoControlSystem.Domain.Models;
using NippoControlSystem.Domain.Repositories;
using NippoControlSystem.Domain.Services;
using System.IO;

namespace NippoControlSystem.ApplicationService.Services
{
    public class MeasureConditionService
    {
        private readonly IMeasureConditionRepository _repository;
        private readonly IConditionTextConverter _converter;

        // DIコンテナ経由で依存を注入
        public MeasureConditionService(IMeasureConditionRepository repository, IConditionTextConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }

        // CSV保存の一括ユースケース
        public void ExportResultCsv(DataSetItems dataSetItems, MeasureConditionState state, string folderPath)
        {
            // 1. ドメインモデルの作成
            var record = new MeasureLogRecord
            {
                TestEndDT = state.TestEndDT,
                SerialNo = state.SerialNo,
                GoNo = state.GoNo,
                Zuban = state.Zuban,
                Edaban = state.Edaban,
                InspecStartStatString = state.InspecStartStatString,
                IsPass = (state.InspecStat == enumInspectStat.Stat_NormalEND)
            };

            // 2. ログ保存
            _repository.SaveResultLog(record, folderPath);

            // 3. 詳細ログの保存
            string detailCsvPath = Path.Combine(folderPath, string.Format("A{0:yyyyMMdd_HHmmss}.csv", state.TestEndDT));
            _repository.SaveInspectItem(dataSetItems.ListDatResult, detailCsvPath, withResult: true, checkChanged: false);

            // 4. 関連データの追記
            AppendIfExist(folderPath, Default.Listdat, detailCsvPath);
            AppendIfExist(folderPath, Default.Portdat, detailCsvPath);
            AppendIfExist(folderPath, Default.Checkdat, detailCsvPath);
        }

        // 文字列変換のユースケース
        public bool ConvertTextFile(string filePath, System.Func<string, string> convertFunc)
        {
            string content = _repository.ReadText(filePath);
            if (content == null) return false;

            string converted = convertFunc(content);
            if (converted != null)
            {
                // リポジトリ経由で保存
                return true;
            }
            return false;
        }

        private void AppendIfExist(string folder, string fileName, string targetPath)
        {
            string sourcePath = Path.Combine(folder, fileName);
            string content = _repository.ReadText(sourcePath);
            _repository.AppendText(targetPath, content);
        }
    }
}