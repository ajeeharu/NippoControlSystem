using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// PortDisp テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "PortDisp")]
    public class PortDispDto
    {
        public int? PinNo { get; set; }

        public string? PortDA { get; set; }

        public string? PortDB { get; set; }

        public string? PortDC { get; set; }

        public string? PortDD { get; set; }

        public string? PortDE { get; set; }

        public string? PortDF { get; set; }

        public string? PortDG { get; set; }

        public string? PortDH { get; set; }

        public string? PortAO { get; set; }

        public string? PortAI { get; set; }

        public string? PortGndDA { get; set; }

        public string? PortGndDB { get; set; }

        public string? PortGndDC { get; set; }

        public string? PortGndDD { get; set; }

        public string? PortGndDE { get; set; }

        public string? PortGndDF { get; set; }

        public string? PortGndDG { get; set; }

        public string? PortGndDH { get; set; }

        public string? PortGndAO { get; set; }

        public string? PortGndAI { get; set; }

        public string? DataDA { get; set; }

        public string? DataDB { get; set; }

        public string? DataDC { get; set; }

        public string? DataDD { get; set; }

        public string? DataDE { get; set; }

        public string? DataDF { get; set; }

        public string? DataDG { get; set; }

        public string? DataDH { get; set; }

        public string? DataAO { get; set; }

        public string? DataAI { get; set; }

        public string? DataAOSw { get; set; }
    }
}
