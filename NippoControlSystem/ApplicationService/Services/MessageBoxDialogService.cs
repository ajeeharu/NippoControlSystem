using NippoControlSystem.ApplicationService.Interfaces;
using System.Runtime.Versioning;

namespace NippoControlSystem.ApplicationService.Services
{
    [SupportedOSPlatform("windows")]
    public class MessageBoxDialogService : IDialogService
    {
        public void ShowMessage(string message, string title = "通知")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ShowConfirmation(string message, string title = "確認")
        {
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}
