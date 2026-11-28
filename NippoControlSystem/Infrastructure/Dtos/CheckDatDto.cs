using System.Xml.Serialization;

namespace NippoControlSystem.Application.Dtos
{
    /// <summary>
    /// CheckDat テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "CheckDat")]
    public class CheckDatDto
    {
        public string? Item { get; set; }

        public string? Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Volt { get; set; }

        [XmlElement("iOP-HiLMT")]
        public string? _iOP_HiLMT { get; set; }

        [XmlElement("iOP-LoLMT")]
        public string? _iOP_LoLMT { get; set; }

        [XmlElement("iGN-HiLMT")]
        public string? _iGN_HiLMT { get; set; }

        [XmlElement("iGN-LoLMT")]
        public string? _iGN_LoLMT { get; set; }

        [XmlElement("iHi-HiLMT")]
        public string? _iHi_HiLMT { get; set; }

        [XmlElement("iHi-LoLMT")]
        public string? _iHi_LoLMT { get; set; }

        [XmlElement("iDo-HiLMT")]
        public string? _iDo_HiLMT { get; set; }

        [XmlElement("iDo-LoLMT")]
        public string? _iDo_LoLMT { get; set; }

        [XmlElement("iDh-HiLMT")]
        public string? _iDh_HiLMT { get; set; }

        [XmlElement("iDh-LoLMT")]
        public string? _iDh_LoLMT { get; set; }

        [XmlElement("iDb-HiLMT")]
        public string? _iDb_HiLMT { get; set; }

        [XmlElement("iDb-LoLMT")]
        public string? _iDb_LoLMT { get; set; }

        [XmlElement("iDc-HiLMT")]
        public string? _iDc_HiLMT { get; set; }

        [XmlElement("iDc-LoLMT")]
        public string? _iDc_LoLMT { get; set; }

        [XmlElement("iDs-HiLMT")]
        public string? _iDs_HiLMT { get; set; }

        [XmlElement("iDs-LoLMT")]
        public string? _iDs_LoLMT { get; set; }

        [XmlElement("iDp-HiLMT")]
        public string? _iDp_HiLMT { get; set; }

        [XmlElement("iDp-LoLMT")]
        public string? _iDp_LoLMT { get; set; }

        [XmlElement("iTo-HiLMT")]
        public string? _iTo_HiLMT { get; set; }

        [XmlElement("iTo-LoLMT")]
        public string? _iTo_LoLMT { get; set; }
    }
}
