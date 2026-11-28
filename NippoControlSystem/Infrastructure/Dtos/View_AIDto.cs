using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// View_AI テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "View_AI")]
    public class View_AIDto
    {
        public int? Pin { get; set; }

        public string? Name { get; set; }

        public string? Lower { get; set; }

        public string? Value { get; set; }

        public string? Upper { get; set; }

        public string? STAT { get; set; }
    }
}
