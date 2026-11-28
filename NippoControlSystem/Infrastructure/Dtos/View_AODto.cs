using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_AO テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_AO")]
    public class View_AODto
    {
        public int? Pin { get; set; }

        public string? Name { get; set; }

        public string? Value { get; set; }

        public string? Enable { get; set; }
    }
}
