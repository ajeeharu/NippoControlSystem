namespace NippoControlSystem.Infrastructure.NativeLibs
{
    internal static partial class WindowService
    {
        private const string User32 = "user32.dll";
        /// <summary>
        /// Windows API: FindWindowW (Unicode版)
        /// </summary>
        //[LibraryImport(User32, EntryPoint = "SHBrowseForFolderA", StringMarshalling = StringMarshalling.Utf16)]
        //IntPtr SHBrowseForFolder(LPBROWSEINFOA lpbi);
    }
}