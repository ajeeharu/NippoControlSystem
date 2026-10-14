using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace NippoControlSystem.ApplicationService.Configurations
{
    public struct niCellStyle
    {
        [XmlIgnore, JsonIgnore]
        public Color ForeColor { get; set; }
        [XmlIgnore, JsonIgnore]
        public Color SelectionForeColor { get; set; }
        public FontStyle FontStyle { get; set; }
        public string ForeColorHex
        {
            get => ColorTranslator.ToHtml(this.ForeColor);
            set => this.ForeColor = ColorTranslator.FromHtml(value);
        }
        public string SelectionForeColorHex
        {
            get => ColorTranslator.ToHtml(this.SelectionForeColor);
            set => this.SelectionForeColor = ColorTranslator.FromHtml(value);
        }
    }
    public class PeripheralSettings
    {
        // Windows
        public required string WindowStyle { get; set; }
        public required string HideTaskbar { get; set; }

        // Wav
        public required string PlaySoundEnable { get; set; }
        public required string okWavlPath { get; set; }
        public required string ngWavlPath { get; set; }
        public required string pauseWavPath { get; set; }

        // Print
        public bool DefaultPageSettings_Landscape { get; set; }
        public int DefaultPageSettings_Margins_Left { get; set; }
        public int DefaultPageSettings_Margins_Right { get; set; }
        public int DefaultPageSettings_Margins_Top { get; set; }
        public int DefaultPageSettings_Margins_Bottom { get; set; }

        // Color Styles
        public required niCellStyle[] CellStyles { get; set; }

        // External Monitor
        public required string AioMonitorExePath { get; set; }
    }
}