namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title = "通知");
        bool ShowConfirmation(string message, string title = "確認");
    }
}
