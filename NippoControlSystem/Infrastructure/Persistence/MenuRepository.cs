using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.Infrastructure.Persistence.Xsd;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public interface IMenuRepository
    {
        MainMenuList GetShareMenuInfo();
        void SaveShareMenuInfo(MainMenuList menuInfo);
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly string _filePath = "path_to_menu.xml"; // 読み書きするXMLのパス

        public MainMenuList GetShareMenuInfo()
        {
            var dataSet = new DataSetTopMenu();
            if (System.IO.File.Exists(_filePath))
            {
                dataSet.ReadXml(_filePath);
            }

            var shareMenuInfo = new MainMenuList();

            // DataSet -> 階層DTO (ShareMenuInfo) へのマッピング
            foreach (DataSetTopMenu.menuMainRow mainRow in dataSet.menuMain)
            {
                var mainItem = new MainMenuItem
                {
                    MainID = mainRow.MainID,
                    Title = mainRow.IsTitleNull() ? string.Empty : mainRow.Title
                };

                foreach (var subRow in mainRow.GetmenuSubRows())
                {
                    mainItem.SubMenus.Add(new SubMenuItem
                    {
                        MainID = subRow.MainID,
                        SubID = subRow.SubID,
                        SubTitle = subRow.SubTitle,
                        Folder = subRow.IsFolderNull() ? null : subRow.Folder
                    });
                }

                shareMenuInfo.MainMenus.Add(mainItem);
            }
            return shareMenuInfo;
        }

        public void SaveShareMenuInfo(MainMenuList menuInfo)
        {
            var dataSet = new DataSetTopMenu();

            // 階層DTO -> DataSet への逆マッピング（Update用）
            foreach (var mainItem in menuInfo.MainMenus)
            {
                var mainRow = dataSet.menuMain.NewmenuMainRow();
                mainRow.MainID = mainItem.MainID;
                mainRow.Title = mainItem.Title;
                dataSet.menuMain.AddmenuMainRow(mainRow);

                foreach (var subItem in mainItem.SubMenus)
                {
                    var subRow = dataSet.menuSub.NewmenuSubRow();
                    subRow.MainID = mainItem.MainID; // 親子の紐付け
                    subRow.SubID = subItem.SubID;
                    subRow.SubTitle = subItem.SubTitle;
                    subRow.Folder = subItem.Folder ?? string.Empty;
                    dataSet.menuSub.AddmenuSubRow(subRow);
                }
            }
            // XMLファイル等へ保存
            dataSet.WriteXml(_filePath);
        }
    }
}