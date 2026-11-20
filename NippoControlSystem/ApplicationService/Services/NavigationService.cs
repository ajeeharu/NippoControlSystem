using NippoControlSystem.ApplicationService.Interfaces;
using System.IO;

namespace NippoControlSystem.ApplicationService.Services
{
    public class NavigationService(IServiceProvider serviceProvider) : INavigationService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TForm">表示したいダイアログ（フォーム）の型</typeparam>
        /// <param name="isModal">モーダル（モダールダイアログ）として表示するか、
        ///   モードレス（通常ウィンドウ）として表示するかを切り替え</param>
        /// <param name="configureView">ダイアログを表示する直前に、
        /// 　フォームの設定やデータの受け渡しを行うためのコールバック関数（ラムダ式）</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public DialogResult NavigateTo<TForm>(bool isModal = true, Action<TForm>? configureView = null) where TForm : Form
        {
            if (_serviceProvider.GetService(typeof(TForm)) is TForm form)
            {
                configureView?.Invoke(form);

                if (isModal)
                {
                    using (form)
                    {
                        return form.ShowDialog();
                    }
                }
                else
                {
                    form.Show();
                    return DialogResult.OK;
                }
            }

            throw new InvalidOperationException($"Form '{typeof(TForm).Name}' がDIコンテナに登録されていないか、取得できませんでした。");
        }
        /// <summary>
        /// 　Dialog表示
        /// </summary>
        /// <typeparam name="TForm">表示したいダイアログ（フォーム）の型</typeparam>
        /// <param name="owner">呼び出し元のフォーム（オーナーウィンドウ）を指定
        /// 　「オーナーを一時的に隠す」制御を行っている、nullの場合は隠す処理をskip</param>
        /// <param name="configureView">ダイアログを表示する直前に、
        /// 　フォームの設定やデータの受け渡しを行うためのコールバック関数（ラムダ式）</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public DialogResult NavigateAndHideOwner<TForm>(Form? owner, Action<TForm>? configureView = null) where TForm : Form
        {
            if (_serviceProvider.GetService(typeof(TForm)) is TForm form)
            {
                configureView?.Invoke(form);

                owner?.Hide();
                try
                {
                    using (form)
                    {
                        return form.ShowDialog();
                    }
                }
                finally
                {
                    owner?.Show();
                }
            }

            throw new InvalidOperationException($"Form '{typeof(TForm).Name}' がDIコンテナに登録されていないか、取得できませんでした。");
        }

        public DialogResult ShowMessageBox(string message, string caption = "確認", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        public string? ShowOpenFileDialog(string? initialFolder, string filter = "すべてのファイル(*.*)|*.*")
        {
            using var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.Exists(initialFolder) ? initialFolder : string.Empty;
            ofd.Filter = filter;
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return null;
        }
    }
}