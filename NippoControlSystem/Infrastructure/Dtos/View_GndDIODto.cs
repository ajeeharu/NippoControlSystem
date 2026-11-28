using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_GndDIO テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_GndDIO")]
    public class View_GndDIODto
    {
        public int? Pin { get; set; }

        public string? Name { get; set; }
    }
}
