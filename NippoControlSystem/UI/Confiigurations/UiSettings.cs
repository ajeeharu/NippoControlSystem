namespace NippoControlSystem.UI.Configurations
{
    public class UiSettings
    {
        public string WindowStyle { get; set; } = string.Empty;
        public bool HideTaskbar { get; set; } = false;

        // Sound
        public bool PlaySoundEnable { get; set; } = false;
        public string OkWavPath { get; set; } = string.Empty;
        public string NgWavPath { get; set; } = string.Empty;
        public string PauseWavPath { get; set; } = string.Empty;

        // Print
        public PrintSettings DefaultPageSettings { get; set; } = new PrintSettings();
        public class PrintSettings
        {
            public bool Landscape { get; set; }
            public MarginSettings Margins { get; set; } = new MarginSettings();
        }

        public class MarginSettings
        {
            public int Left { get; set; }
            public int Right { get; set; }
            public int Top { get; set; }
            public int Bottom { get; set; }
        }
    }
