using NippoControlSystem.ApplicationService.Common;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.Infrastructure.Persistence;
using System.IO;

namespace NippoControlSystem.ApplicationServices.Services
{
    public class MenuApplicationService : CsvApplicationServiceBase
    {
        private readonly CsvFileRepository _csvFileRepository;

        public MenuApplicationService(CsvFileRepository csvFileRepository)
        {
            _csvFileRepository = csvFileRepository;
        }

        public MenuInfoSettings LoadMenu(string menuCsvPath)
        {
            // 1. 存在チェック
            if (!_csvFileRepository.Exists(menuCsvPath))
            {
                throw new FileNotFoundException($"メニューCSVファイルが見つかりません: {menuCsvPath}");
            }

            // 2. リポジトリ経由でテキスト取得
            string? csvContent = _csvFileRepository.ReadAllText(menuCsvPath);
            if (string.IsNullOrEmpty(csvContent))
            {
                return new MenuInfoSettings();
            }

            var menuConfig = new MenuInfoSettings();

            // 3. 共通のパース処理を利用して行ごとに処理する
            // ※ ParseCsvContent が内部で CsvToArrayList2 のような行・列のリストに分解してくれる前提
            var records = ParseCsvContentToRecords(csvContent); // 共通基底側のメソッドを想定

            foreach (var csvFields in records)
            {
                // Fields数は奇数であること（Title + [SubTitle, Folder] のペア）
                if (csvFields.Length > 0 && csvFields.Length % 2 == 1)
                {
                    var mainItem = new DataMainMenu
                    {
                        Title = csvFields[0]
                    };

                    // 1つ飛ばしで SubTitle と Folder をペアで取得
                    for (int j = 1; j < csvFields.Length; j += 2)
                    {
                        mainItem.SubMenus.Add(new DataSubMenu
                        {
                            SubTitle = csvFields[j],
                            Folder = csvFields[j + 1]
                        });
                    }

                    menuConfig.MainMenus.Add(mainItem);
                }
            }

            return menuConfig;
        }
    }
}