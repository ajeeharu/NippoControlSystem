namespace NippoControlSystem.UI.Confiigurations
{
    public class UiSettings
    {
        public string WindowStyle { get; set; } = string.Empty;
        public string HideTaskbar { get; set; } = string.Empty;

        // Sound
        public string PlaySoundEnable { get; set; } = string.Empty;
        public string OkWavPath { get; set; } = string.Empty;
        public string NgWavPath { get; set; } = string.Empty;
        public string PauseWavPath { get; set; } = string.Empty;

        // Print
        public bool DefaultPageSettings_Landscape { get; set; }
        public int DefaultPageSettings_Margins_Left { get; set; }
        public int DefaultPageSettings_Margins_Right { get; set; }
        public int DefaultPageSettings_Margins_Top { get; set; }
        public int DefaultPageSettings_Margins_Bottom { get; set; }
    }
}
