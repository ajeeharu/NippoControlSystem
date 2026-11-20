using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.Infrastructure.Persistence;
using System.IO;

namespace NippoControlSystem.ApplicationServices.Services
{
    public class MenuApplicationService
    {
        private readonly CsvFileRepository _csvFileRepository;
        private readonly IMenuRepository _menuRepository;
        public MenuApplicationService(IMenuRepository menuRepository, CsvFileRepository csvFileRepository)
        {
            _menuRepository = menuRepository;
            _csvFileRepository = csvFileRepository;
        }

        public MenuBindDto LoadMenu(string menuCsvPath)
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
                return new MenuBindDto();
            }

            var menuConfig = new MenuBindDto();

            // 3. 共通のパース処理を利用して行ごとに処理する
            // ※ ParseCsvContent が内部で CsvToArrayList2 のような行・列のリストに分解してくれる前提
            var records = _csvFileRepository.ParseCsvContentToRecords(csvContent); // 共通基底側のメソッドを想定

            foreach (var csvFields in records)
            {
                // Fields数は奇数であること（Title + [SubTitle, Folder] のペア）
                if (csvFields.Length > 0 && csvFields.Length % 2 == 1)
                {
                    var mainItem = new BindMainMenuDto
                    {
                        MainTitle = csvFields[0]
                    };

                    // 1つ飛ばしで SubTitle と Folder をペアで取得
                    for (int j = 1; j < csvFields.Length; j += 2)
                    {
                        mainItem.SubMenus.Add(new BindSubMenuDto
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