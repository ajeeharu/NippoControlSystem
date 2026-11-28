using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_DIO テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_DIO")]
    public class View_DIODto
    {
        public int? Pin { get; set; }

        public string? Name { get; set; }

        public string? IO { get; set; }

        public string? STAT { get; set; }
    }
}
