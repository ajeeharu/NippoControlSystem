using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_GndAIO テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_GndAIO")]
    public class View_GndAIODto
    {
        public int? Pin { get; set; }

        public string? AoName { get; set; }

        public string? AiName { get; set; }
    }
}
