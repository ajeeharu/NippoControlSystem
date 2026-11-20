using NippoControlSystem.Domain.Models;

namespace NippoControlSystem.Domain.Interfaces
{
    // メニューデータを読み込むインターフェースと実装
    public interface IMenuRepository
    {
        Task<List<MenuMainItem>> GetMenuItemsAsync();
    }

    public class MenuRepository : IMenuRepository
    {
        public async Task<List<MenuMainItem>> GetMenuItemsAsync()
        {
            // JSON/XMLファイルやDB等から MenuMainItem のリストを生成して返す
            return new List<MenuMainItem>
        {
            new MenuMainItem { MainID = 1, Title = "導通検査" },
            new MenuMainItem { MainID = 2, Title = "抵抗検査" }
        };
        }
    }
}