using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.Domain.Models;
using NippoControlSystem.Infrastructure.Persistence;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace NippoControlSystem.ApplicationService.Services
{
    public partial class MeasureConditionService
    {
        // ==========================================
        // 1. 定数およびフィールド（データ宣言）を最上部に配置
        // ==========================================
        private CsvFileRepository _repository;
        private readonly InspectionSettings _settings;
        private const string FileExtension = ".part";
        [JsonIgnore]
        public string? CheckDataIdsLo { get; set; } = null;
        [JsonIgnore]
        public string? CheckDataIdsHi { get; set; } = null;
        [JsonIgnore]
        public bool IsCsvHeaderRead { get; set; }
        [JsonIgnore]
        public DataSetTopMenu? DataSetTopMenu { get; set; } = null;

        public List<string> ListInspectionItemsType { get; set; } = new List<string>();
        public InspectionStatus CurrentInspectionStatus { get; set; } = InspectionStatus.Stop;
        public InspectionStatus InspectionStartStatus { get; set; } = InspectionStatus.Stop;

        [JsonIgnore]
        public string InspectionStatusString => _enumStatusString[(int)CurrentInspectionStatus];

        [JsonIgnore]
        public string InspectionStartStatusString => _enumStartStatusString[(int)InspectionStartStatus];

        [JsonIgnore]
        public int TestStepNo { get; set; } = 0;
        [JsonIgnore]
        public int DioReadStatus { get; set; }
        [JsonIgnore]
        public int DioWriteStatus { get; set; }

        public string? SerialNo { get; set; } = null;
        public string? GoNo { get; set; } = null;
        public string? Zuban { get; set; } = null;
        public string? Edaban { get; set; } = null;

        public DateTime TestStartDT { get; set; } = DateTime.MinValue;
        public DateTime TestEndDateTime { get; set; } = DateTime.MinValue;

        public int GreenSwitchStatus { get; set; } = 0;
        public int RedSwitchStatus { get; set; } = 0;

        // 辞書やマッピング状態などのデータ群
        public enum InspectionStatus
        {
            Stop = 0,
            NormalEnd,
            FailEnd,
            ForceToEnd,
            MidStarted,
            NormalStart,
            TimeOut,
            ManualEnd
        }
        // 1. クラスのフィールドとして状態を保持する（初期値は Stop）
        private InspectionStatus _inspectionStatus = InspectionStatus.Stop;

        // 2. 外部から読み取れるようにプロパティを用意する（必要に応じて）
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="repository"></param>
        public MeasureConditionService(InspectionSettings settings, CsvFileRepository repository)
        {
            _settings = settings;
            _repository = repository;
        }
        public InspectionStatus CurrentStatus => _inspectionStatus;
        public void UpdateStatus(InspectionStatus newStatus)
        {
            _inspectionStatus = newStatus;
        }
        // CSV保存の一括ユースケース
        public void ExportResultCsv(DataSetItems dataSetItems, MeasureCondition state, string folderPath)
        {
            // 1. ドメインモデルの作成
            var record = new MeasureLogRecord
            {
                TestEndDateTime = state.TestEndDateTime,
                SerialNo = state.SerialNo,
                GoNo = state.GoNo,
                Zuban = state.Zuban,
                Edaban = state.Edaban,
                InspectionStartStatusString = state.InspectionStartStatusString,
                IsPass = (state.InspectionStartStatus == MeasureCondition.InspectionStatus.NormalEnd)
            };

            // 2. ログ保存
            _repository.SaveResultLog(record, folderPath);

            // 3. 詳細ログの保存
            string detailCsvPath = Path.Combine(folderPath, string.Format("A{0:yyyyMMdd_HHmmss}.csv", state.TestEndDateTime));
            _repository.SaveInspectItem(dataSetItems.ListDatResult, detailCsvPath, withResult: true, checkChanged: false);

            // 4. 関連データの追記
            AppendIfExist(folderPath, _settings.ListFileName, detailCsvPath);
            AppendIfExist(folderPath, _settings.PortFileName, detailCsvPath);
            AppendIfExist(folderPath, _settings.CheckFileName, detailCsvPath);
        }

        // 文字列変換のユースケース
        public bool ConvertTextFile(string filePath, System.Func<string, string> convertFunc)
        {
            string content = _repository.ReadAllText(filePath);
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
        // 文字列変換のユースケース
        public static string GetStatusString(InspectionStatus status, bool isStartContext = false)
        {
            return status switch
            {
                InspectionStatus.Stop => "停  止",
                InspectionStatus.NormalEnd => "検査終了",
                InspectionStatus.FailEnd => "検査停止",
                InspectionStatus.ForceToEnd => "強制完全",
                InspectionStatus.MidStarted => "途中開始",
                // NormalStart のときだけ文脈に応じて文字を変える
                InspectionStatus.NormalStart => isStartContext ? "通常検査" : "検査中",
                InspectionStatus.TimeOut => "timeout",
                InspectionStatus.ManualEnd => "中  止",
                _ => "不明"
            };
        }
        public static bool executeiDhiDb(string ListFileNameFile)
        {
            return convertTextD(ListFileNameFile, convertiDhiDb);
        }

        public static bool executeiDbiDh(string ListFileNameFile)
        {
            return convertTextD(ListFileNameFile, convertiDbiDh);
        }

        public static string convertiDhiDb(string contents)
        {
            if (string.IsNullOrEmpty(contents)) return string.Empty;

            if (System.Text.RegularExpressions.Regex.IsMatch(contents, "(iDh|nDh)"))
            {
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "iDh", "iDb");
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "nDh", "nDb");
                return contents;
            }
            return string.Empty;
        }

        public static string convertiDbiDh(string contents)
        {
            if (string.IsNullOrEmpty(contents)) return string.Empty;

            if (System.Text.RegularExpressions.Regex.IsMatch(contents, "(iDb|nDb)"))
            {
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "iDb", "iDh");
                contents = System.Text.RegularExpressions.Regex.Replace(contents, "nDb", "nDh");
                return contents;
            }
            return string.Empty;
        }

        public delegate string delegateonvertH2D(string contents);

        public static bool convertTextD(string ListFileNameFile, delegateonvertH2D convertH2D)
        {
            var listdat = new FileInfo(ListFileNameFile);
            if (!listdat.Exists) return false;

            string allText = ReadAllText(listdat, enc);
            string convertedText = convertH2D(allText);

            if (convertedText != string.Empty)
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
        public static string ReadAllText(FileInfo fileInfo3, Encoding enc)
        {
            if (!fileInfo3.Exists) return string.Empty;

            using (var fs = fileInfo3.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var cReader = new StreamReader(fs, enc))
            {
                return cReader.ReadToEnd();
            }
        }
        public void loadListdatFile(DataTable ListDat, string ListdatFile, bool HistView)
        {
            if (string.IsNullOrEmpty(ListdatFile) || !System.IO.File.Exists(ListdatFile))
            {
                // ファイルがないときは、Itemをクリアして空行を追加
                ListDat.Rows.Clear();
                DataRow emptyRow = ListDat.NewRow();
                emptyRow["Type"] = 0;
                ListDat.Rows.Add(emptyRow);
                return;
            }

            System.Diagnostics.Debug.WriteLine("loadListdatFile ListdatFile:" + ListdatFile);

            // ファイルを全行読み込み、CSVを展開する
            string stBuffer = this.ReadAllText(ListdatFile, enc);
            List<List<string>> csvRecords = CsvToArrayList2(stBuffer);

            // --- 【動的な列数の算出】設定値から必要な最小フィールド数を計算する ---
            int totalDioCount = 0;
            for (int ii = 0; ii < Default.DioNames.Length; ii++)
            {
                totalDioCount += Default.DioNums[ii];
            }

            int minFieldsBasic = 3 + totalDioCount + Default.AoNum + (Default.AiNum * 2); // 従来の 277 に相当
            int minFieldsWithAOSw = minFieldsBasic + Default.AoSwichNum;                 // 従来の 285 に相当
            int minFieldsWithResult = minFieldsWithAOSw + 1;                             // 従来の 286 に相当
            const int MinFieldsWithLimits = 799;                                         // リミット値付きの判定用

            ListDat.Rows.Clear();

            for (int i = 0; i < csvRecords.Count; i++)
            {
                List<string> csvFields = csvRecords[i];

                // データの最低要件を満たしているかチェック
                if (csvFields.Count < minFieldsBasic)
                {
                    System.Diagnostics.Debug.WriteLine($"csvFields.Count error: {csvFields.Count}");
                    continue;
                }

                DataRow dtItemRow = ListDat.NewRow();
                int idx = 0;

                // 1. 基本情報
                dtItemRow["Title"] = csvFields[idx++];
                dtItemRow["Guide"] = csvFields[idx++].Replace("\\n", Environment.NewLine);
                dtItemRow["Type"] = csvFields[idx++];
                dtItemRow["Result"] = null;

                // 2. DIO (デジタル入出力)
                for (int ii = 0; ii < Default.DioNames.Length; ii++)
                {
                    for (int j = 0; j < Default.DioNums[ii]; j++)
                    {
                        int jj = Default.DioNums[ii] - j; // 32 → 01 の逆順
                        string fieldData = csvFields[idx++];
                        string fieldDataNew = fieldData;

                        if (!HistView)
                        {
                            fieldDataNew = ResolveDioStatus(fieldData, ii);
                        }

                        dtItemRow[string.Format("{0}-{1:00}", Default.DioNames[ii], jj)] = fieldDataNew;
                    }
                }

                // 3. AO (アナログ出力)
                for (int ii = 0; ii < Default.AoNum; ii++)
                {
                    int jj = Default.AoNum - ii;
                    dtItemRow[string.Format("{0}-{1:0}", Default.AoName, jj)] = csvFields[idx++];
                }

                // 4. AI (アナログ入力: Low/Highペア)
                for (int ii = 0; ii < Default.AiNum; ii++)
                {
                    int jj = Default.AiNum - ii;
                    dtItemRow[string.Format("{0}-{1:0}L", Default.AiName, jj)] = csvFields[idx++];
                    dtItemRow[string.Format("{0}-{1:0}H", Default.AiName, jj)] = csvFields[idx++];
                }

                // 5. AOSw と Result の拡張データ
                if (csvFields.Count >= minFieldsWithAOSw)
                {
                    for (int ii = 0; ii < Default.AoSwichNum; ii++)
                    {
                        int jj = Default.AoSwichNum - ii;
                        string stat0 = Default.AoSwichStat[0];
                        string stat1 = Default.AoSwichStat[1];
                        dtItemRow[string.Format("{0}-{1:0}", Default.AoSwichName, jj)] =
                            csvFields[idx++] == stat0 ? stat0 : stat1;
                    }

                    if (csvFields.Count >= minFieldsWithResult)
                    {
                        dtItemRow["Result"] = csvFields[idx++];
                    }
                }

                // 6. Lmt High/Low (リミット値)
                if (csvFields.Count >= MinFieldsWithLimits)
                {
                    string hiLimit = csvFields[idx++];
                    dtItemRow["iDs-HiLMT"] = !string.IsNullOrEmpty(hiLimit) ? hiLimit : CheckDatiDsHi;

                    string loLimit = csvFields[idx++];
                    dtItemRow["iDs-LoLMT"] = !string.IsNullOrEmpty(loLimit) ? loLimit : CheckDatiDsLo;

                    for (int ii = 0; ii < Default.DioNames.Length; ii++)
                    {
                        for (int j = 0; j < Default.DioNums[ii]; j++)
                        {
                            int jj = Default.DioNums[ii] - j;

                            string hiVal = csvFields[idx++];
                            if (!string.IsNullOrEmpty(hiVal))
                            {
                                dtItemRow[string.Format("{0}-{1:00}H", Default.DioNames[ii], jj)] = hiVal;
                            }

                            string loVal = csvFields[idx++];
                            if (!string.IsNullOrEmpty(loVal))
                            {
                                dtItemRow[string.Format("{0}-{1:00}L", Default.DioNames[ii], jj)] = loVal;
                            }
                        }
                    }
                }

                ListDat.Rows.Add(dtItemRow);
            }

            ListDat.AcceptChanges();
        }

        /// <summary>
        /// 新旧機種のDIOステータスを解決・変換するヘルパーメソッド
        /// </summary>
        private string ResolveDioStatus(string fieldData, int dioGroupIndex)
        {
            if (dicDioStat.ContainsKey(fieldData) || dicDioHistStat.ContainsKey(fieldData))
            {
                return fieldData;
            }

            // 旧機種のデータを新機種に変更
            if (dioGroupIndex < 4) // DO系
            {
                return dicDoStat.ContainsKey(fieldData) ? dicDoStat[fieldData] : "";
            }
            else // DI系
            {
                return dicDiStat.ContainsKey(fieldData) ? dicDiStat[fieldData] : "";
            }
        }
    }
}