using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// 指定したFormを表示します（モーダルまたはモードレス）。
        /// </summary>
        DialogResult NavigateTo<TForm>(bool isModal = true, Action<TForm> configureView = null!) where TForm : Form;

        /// <summary>
        /// 親画面（owner）を一時的に非表示にして、指定したFormをモーダル表示し、閉じたら親画面を再表示します。
        /// </summary>
        DialogResult NavigateAndHideOwner<TForm>(Form? owner, Action<TForm> configureView = null!) where TForm : Form;

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        DialogResult ShowMessageBox(string message, string caption = "確認", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information);

        /// <summary>
        /// ファイル選択ダイアログを表示し、選択されたファイルパスを返します。
        /// </summary>
        string? ShowOpenFileDialog(string? initialFolder, string filter = "すべてのファイル(*.*)|*.*");
    }
}
