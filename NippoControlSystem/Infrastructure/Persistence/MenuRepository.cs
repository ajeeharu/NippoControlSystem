using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.Infrastructure.Persistence.Xsd;
using System.IO;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public interface IMenuRepository
    {
        MainMenuList GetShareMenuInfo();
        void SaveShareMenuInfo(MainMenuList menuInfo);
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly string _filePath;

        public MenuRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("メニューファイルのパスが指定されていません。", nameof(filePath));
            }

            _filePath = filePath;
        }

        public MainMenuList GetShareMenuInfo()
        {
            var dataSet = new DataSetTopMenu();
            if (File.Exists(_filePath))
            {
                dataSet.ReadXml(_filePath);
            }

            var shareMenuInfo = new MainMenuList();

            foreach (DataSetTopMenu.menuMainRow mainRow in dataSet.menuMain)
            {
                string title = mainRow["Title"] is DBNull
                    ? string.Empty
                    : (mainRow.Title ?? string.Empty);

                var mainItem = new MainMenuItem
                {
                    MainID = mainRow.MainID,
                    Title = title
                };

                foreach (var subRow in mainRow.GetmenuSubRows())
                {
                    string subTitle = subRow["SubTitle"] is DBNull
                        ? string.Empty
                        : (subRow.SubTitle ?? string.Empty);

                    string? folder = subRow["Folder"] is DBNull
                        ? null
                        : subRow.Folder;

                    mainItem.SubMenus.Add(new SubMenuItem
                    {
                        MainID = subRow.MainID,
                        SubID = subRow.SubID,
                        SubTitle = subTitle,
                        Folder = folder
                    });
                }

                shareMenuInfo.MainMenus.Add(mainItem);
            }

            return shareMenuInfo;
        }

        public void SaveShareMenuInfo(MainMenuList menuInfo)
        {
            ArgumentNullException.ThrowIfNull(menuInfo);

            var dataSet = new DataSetTopMenu();

            foreach (var mainItem in menuInfo.MainMenus)
            {
                var mainRow = dataSet.menuMain.NewmenuMainRow();
                mainRow.MainID = mainItem.MainID;
                mainRow.Title = mainItem.Title ?? string.Empty;
                dataSet.menuMain.AddmenuMainRow(mainRow);

                foreach (var subItem in mainItem.SubMenus)
                {
                    var subRow = dataSet.menuSub.NewmenuSubRow();
                    subRow.MainID = mainItem.MainID;
                    subRow.SubID = subItem.SubID;
                    subRow.SubTitle = subItem.SubTitle ?? string.Empty;
                    subRow.Folder = subItem.Folder ?? string.Empty;
                    dataSet.menuSub.AddmenuSubRow(subRow);
                }
            }

            dataSet.WriteXml(_filePath);
        }
    }
}