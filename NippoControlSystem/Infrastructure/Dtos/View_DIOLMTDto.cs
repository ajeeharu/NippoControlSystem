using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_DIOLMT テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_DIOLMT")]
    public class View_DIOLMTDto
    {
        public int? Pin { get; set; }

        public string? Name { get; set; }

        public string? LowLMT { get; set; }

        public string? MeasValue { get; set; }

        public string? HighLMT { get; set; }
    }
}
