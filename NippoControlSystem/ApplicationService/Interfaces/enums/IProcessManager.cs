namespace NippoControlSystem.ApplicationService.Interfaces.enums
{
    /// <summary>
    /// ShowWindowAsync 関数で使用する表示コマンド
    /// </summary>
    public enum ShowWindowCommand : int
    {
        SW_HIDE = 0,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 11
    }
}