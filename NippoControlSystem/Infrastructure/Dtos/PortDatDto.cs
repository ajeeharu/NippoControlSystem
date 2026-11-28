using System;
using System.Xml.Serialization;

namespace NippoControlSystem.Infrastructure.Dtos
{
    /// <summary>
    /// PortDat テーブルに対応する DTO クラス
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "PortDat")]
    public class PortDatDto
    {
        public string? Title { get; set; }

        public string? Guide { get; set; }

        public string? Type { get; set; }

        [XmlElement("DA-32")]
        public string? _DA_32 { get; set; }

        [XmlElement("DA-31")]
        public string? _DA_31 { get; set; }

        [XmlElement("DA-30")]
        public string? _DA_30 { get; set; }

        [XmlElement("DA-29")]
        public string? _DA_29 { get; set; }

        [XmlElement("DA-28")]
        public string? _DA_28 { get; set; }

        [XmlElement("DA-27")]
        public string? _DA_27 { get; set; }

        [XmlElement("DA-26")]
        public string? _DA_26 { get; set; }

        [XmlElement("DA-25")]
        public string? _DA_25 { get; set; }

        [XmlElement("DA-24")]
        public string? _DA_24 { get; set; }

        [XmlElement("DA-23")]
        public string? _DA_23 { get; set; }

        [XmlElement("DA-22")]
        public string? _DA_22 { get; set; }

        [XmlElement("DA-21")]
        public string? _DA_21 { get; set; }

        [XmlElement("DA-20")]
        public string? _DA_20 { get; set; }

        [XmlElement("DA-19")]
        public string? _DA_19 { get; set; }

        [XmlElement("DA-18")]
        public string? _DA_18 { get; set; }

        [XmlElement("DA-17")]
        public string? _DA_17 { get; set; }

        [XmlElement("DA-16")]
        public string? _DA_16 { get; set; }

        [XmlElement("DA-15")]
        public string? _DA_15 { get; set; }

        [XmlElement("DA-14")]
        public string? _DA_14 { get; set; }

        [XmlElement("DA-13")]
        public string? _DA_13 { get; set; }

        [XmlElement("DA-12")]
        public string? _DA_12 { get; set; }

        [XmlElement("DA-11")]
        public string? _DA_11 { get; set; }

        [XmlElement("DA-10")]
        public string? _DA_10 { get; set; }

        [XmlElement("DA-09")]
        public string? _DA_09 { get; set; }

        [XmlElement("DA-08")]
        public string? _DA_08 { get; set; }

        [XmlElement("DA-07")]
        public string? _DA_07 { get; set; }

        [XmlElement("DA-06")]
        public string? _DA_06 { get; set; }

        [XmlElement("DA-05")]
        public string? _DA_05 { get; set; }

        [XmlElement("DA-04")]
        public string? _DA_04 { get; set; }

        [XmlElement("DA-03")]
        public string? _DA_03 { get; set; }

        [XmlElement("DA-02")]
        public string? _DA_02 { get; set; }

        [XmlElement("DA-01")]
        public string? _DA_01 { get; set; }

        [XmlElement("DB-32")]
        public string? _DB_32 { get; set; }

        [XmlElement("DB-31")]
        public string? _DB_31 { get; set; }

        [XmlElement("DB-30")]
        public string? _DB_30 { get; set; }

        [XmlElement("DB-29")]
        public string? _DB_29 { get; set; }

        [XmlElement("DB-28")]
        public string? _DB_28 { get; set; }

        [XmlElement("DB-27")]
        public string? _DB_27 { get; set; }

        [XmlElement("DB-26")]
        public string? _DB_26 { get; set; }

        [XmlElement("DB-25")]
        public string? _DB_25 { get; set; }

        [XmlElement("DB-24")]
        public string? _DB_24 { get; set; }

        [XmlElement("DB-23")]
        public string? _DB_23 { get; set; }

        [XmlElement("DB-22")]
        public string? _DB_22 { get; set; }

        [XmlElement("DB-21")]
        public string? _DB_21 { get; set; }

        [XmlElement("DB-20")]
        public string? _DB_20 { get; set; }

        [XmlElement("DB-19")]
        public string? _DB_19 { get; set; }

        [XmlElement("DB-18")]
        public string? _DB_18 { get; set; }

        [XmlElement("DB-17")]
        public string? _DB_17 { get; set; }

        [XmlElement("DB-16")]
        public string? _DB_16 { get; set; }

        [XmlElement("DB-15")]
        public string? _DB_15 { get; set; }

        [XmlElement("DB-14")]
        public string? _DB_14 { get; set; }

        [XmlElement("DB-13")]
        public string? _DB_13 { get; set; }

        [XmlElement("DB-12")]
        public string? _DB_12 { get; set; }

        [XmlElement("DB-11")]
        public string? _DB_11 { get; set; }

        [XmlElement("DB-10")]
        public string? _DB_10 { get; set; }

        [XmlElement("DB-09")]
        public string? _DB_09 { get; set; }

        [XmlElement("DB-08")]
        public string? _DB_08 { get; set; }

        [XmlElement("DB-07")]
        public string? _DB_07 { get; set; }

        [XmlElement("DB-06")]
        public string? _DB_06 { get; set; }

        [XmlElement("DB-05")]
        public string? _DB_05 { get; set; }

        [XmlElement("DB-04")]
        public string? _DB_04 { get; set; }

        [XmlElement("DB-03")]
        public string? _DB_03 { get; set; }

        [XmlElement("DB-02")]
        public string? _DB_02 { get; set; }

        [XmlElement("DB-01")]
        public string? _DB_01 { get; set; }

        [XmlElement("DC-32")]
        public string? _DC_32 { get; set; }

        [XmlElement("DC-31")]
        public string? _DC_31 { get; set; }

        [XmlElement("DC-30")]
        public string? _DC_30 { get; set; }

        [XmlElement("DC-29")]
        public string? _DC_29 { get; set; }

        [XmlElement("DC-28")]
        public string? _DC_28 { get; set; }

        [XmlElement("DC-27")]
        public string? _DC_27 { get; set; }

        [XmlElement("DC-26")]
        public string? _DC_26 { get; set; }

        [XmlElement("DC-25")]
        public string? _DC_25 { get; set; }

        [XmlElement("DC-24")]
        public string? _DC_24 { get; set; }

        [XmlElement("DC-23")]
        public string? _DC_23 { get; set; }

        [XmlElement("DC-22")]
        public string? _DC_22 { get; set; }

        [XmlElement("DC-21")]
        public string? _DC_21 { get; set; }

        [XmlElement("DC-20")]
        public string? _DC_20 { get; set; }

        [XmlElement("DC-19")]
        public string? _DC_19 { get; set; }

        [XmlElement("DC-18")]
        public string? _DC_18 { get; set; }

        [XmlElement("DC-17")]
        public string? _DC_17 { get; set; }

        [XmlElement("DC-16")]
        public string? _DC_16 { get; set; }

        [XmlElement("DC-15")]
        public string? _DC_15 { get; set; }

        [XmlElement("DC-14")]
        public string? _DC_14 { get; set; }

        [XmlElement("DC-13")]
        public string? _DC_13 { get; set; }

        [XmlElement("DC-12")]
        public string? _DC_12 { get; set; }

        [XmlElement("DC-11")]
        public string? _DC_11 { get; set; }

        [XmlElement("DC-10")]
        public string? _DC_10 { get; set; }

        [XmlElement("DC-09")]
        public string? _DC_09 { get; set; }

        [XmlElement("DC-08")]
        public string? _DC_08 { get; set; }

        [XmlElement("DC-07")]
        public string? _DC_07 { get; set; }

        [XmlElement("DC-06")]
        public string? _DC_06 { get; set; }

        [XmlElement("DC-05")]
        public string? _DC_05 { get; set; }

        [XmlElement("DC-04")]
        public string? _DC_04 { get; set; }

        [XmlElement("DC-03")]
        public string? _DC_03 { get; set; }

        [XmlElement("DC-02")]
        public string? _DC_02 { get; set; }

        [XmlElement("DC-01")]
        public string? _DC_01 { get; set; }

        [XmlElement("DD-32")]
        public string? _DD_32 { get; set; }

        [XmlElement("DD-31")]
        public string? _DD_31 { get; set; }

        [XmlElement("DD-30")]
        public string? _DD_30 { get; set; }

        [XmlElement("DD-29")]
        public string? _DD_29 { get; set; }

        [XmlElement("DD-28")]
        public string? _DD_28 { get; set; }

        [XmlElement("DD-27")]
        public string? _DD_27 { get; set; }

        [XmlElement("DD-26")]
        public string? _DD_26 { get; set; }

        [XmlElement("DD-25")]
        public string? _DD_25 { get; set; }

        [XmlElement("DD-24")]
        public string? _DD_24 { get; set; }

        [XmlElement("DD-23")]
        public string? _DD_23 { get; set; }

        [XmlElement("DD-22")]
        public string? _DD_22 { get; set; }

        [XmlElement("DD-21")]
        public string? _DD_21 { get; set; }

        [XmlElement("DD-20")]
        public string? _DD_20 { get; set; }

        [XmlElement("DD-19")]
        public string? _DD_19 { get; set; }

        [XmlElement("DD-18")]
        public string? _DD_18 { get; set; }

        [XmlElement("DD-17")]
        public string? _DD_17 { get; set; }

        [XmlElement("DD-16")]
        public string? _DD_16 { get; set; }

        [XmlElement("DD-15")]
        public string? _DD_15 { get; set; }

        [XmlElement("DD-14")]
        public string? _DD_14 { get; set; }

        [XmlElement("DD-13")]
        public string? _DD_13 { get; set; }

        [XmlElement("DD-12")]
        public string? _DD_12 { get; set; }

        [XmlElement("DD-11")]
        public string? _DD_11 { get; set; }

        [XmlElement("DD-10")]
        public string? _DD_10 { get; set; }

        [XmlElement("DD-09")]
        public string? _DD_09 { get; set; }

        [XmlElement("DD-08")]
        public string? _DD_08 { get; set; }

        [XmlElement("DD-07")]
        public string? _DD_07 { get; set; }

        [XmlElement("DD-06")]
        public string? _DD_06 { get; set; }

        [XmlElement("DD-05")]
        public string? _DD_05 { get; set; }

        [XmlElement("DD-04")]
        public string? _DD_04 { get; set; }

        [XmlElement("DD-03")]
        public string? _DD_03 { get; set; }

        [XmlElement("DD-02")]
        public string? _DD_02 { get; set; }

        [XmlElement("DD-01")]
        public string? _DD_01 { get; set; }

        [XmlElement("DE-32")]
        public string? _DE_32 { get; set; }

        [XmlElement("DE-31")]
        public string? _DE_31 { get; set; }

        [XmlElement("DE-30")]
        public string? _DE_30 { get; set; }

        [XmlElement("DE-29")]
        public string? _DE_29 { get; set; }

        [XmlElement("DE-28")]
        public string? _DE_28 { get; set; }

        [XmlElement("DE-27")]
        public string? _DE_27 { get; set; }

        [XmlElement("DE-26")]
        public string? _DE_26 { get; set; }

        [XmlElement("DE-25")]
        public string? _DE_25 { get; set; }

        [XmlElement("DE-24")]
        public string? _DE_24 { get; set; }

        [XmlElement("DE-23")]
        public string? _DE_23 { get; set; }

        [XmlElement("DE-22")]
        public string? _DE_22 { get; set; }

        [XmlElement("DE-21")]
        public string? _DE_21 { get; set; }

        [XmlElement("DE-20")]
        public string? _DE_20 { get; set; }

        [XmlElement("DE-19")]
        public string? _DE_19 { get; set; }

        [XmlElement("DE-18")]
        public string? _DE_18 { get; set; }

        [XmlElement("DE-17")]
        public string? _DE_17 { get; set; }

        [XmlElement("DE-16")]
        public string? _DE_16 { get; set; }

        [XmlElement("DE-15")]
        public string? _DE_15 { get; set; }

        [XmlElement("DE-14")]
        public string? _DE_14 { get; set; }

        [XmlElement("DE-13")]
        public string? _DE_13 { get; set; }

        [XmlElement("DE-12")]
        public string? _DE_12 { get; set; }

        [XmlElement("DE-11")]
        public string? _DE_11 { get; set; }

        [XmlElement("DE-10")]
        public string? _DE_10 { get; set; }

        [XmlElement("DE-09")]
        public string? _DE_09 { get; set; }

        [XmlElement("DE-08")]
        public string? _DE_08 { get; set; }

        [XmlElement("DE-07")]
        public string? _DE_07 { get; set; }

        [XmlElement("DE-06")]
        public string? _DE_06 { get; set; }

        [XmlElement("DE-05")]
        public string? _DE_05 { get; set; }

        [XmlElement("DE-04")]
        public string? _DE_04 { get; set; }

        [XmlElement("DE-03")]
        public string? _DE_03 { get; set; }

        [XmlElement("DE-02")]
        public string? _DE_02 { get; set; }

        [XmlElement("DE-01")]
        public string? _DE_01 { get; set; }

        [XmlElement("DF-32")]
        public string? _DF_32 { get; set; }

        [XmlElement("DF-31")]
        public string? _DF_31 { get; set; }

        [XmlElement("DF-30")]
        public string? _DF_30 { get; set; }

        [XmlElement("DF-29")]
        public string? _DF_29 { get; set; }

        [XmlElement("DF-28")]
        public string? _DF_28 { get; set; }

        [XmlElement("DF-27")]
        public string? _DF_27 { get; set; }

        [XmlElement("DF-26")]
        public string? _DF_26 { get; set; }

        [XmlElement("DF-25")]
        public string? _DF_25 { get; set; }

        [XmlElement("DF-24")]
        public string? _DF_24 { get; set; }

        [XmlElement("DF-23")]
        public string? _DF_23 { get; set; }

        [XmlElement("DF-22")]
        public string? _DF_22 { get; set; }

        [XmlElement("DF-21")]
        public string? _DF_21 { get; set; }

        [XmlElement("DF-20")]
        public string? _DF_20 { get; set; }

        [XmlElement("DF-19")]
        public string? _DF_19 { get; set; }

        [XmlElement("DF-18")]
        public string? _DF_18 { get; set; }

        [XmlElement("DF-17")]
        public string? _DF_17 { get; set; }

        [XmlElement("DF-16")]
        public string? _DF_16 { get; set; }

        [XmlElement("DF-15")]
        public string? _DF_15 { get; set; }

        [XmlElement("DF-14")]
        public string? _DF_14 { get; set; }

        [XmlElement("DF-13")]
        public string? _DF_13 { get; set; }

        [XmlElement("DF-12")]
        public string? _DF_12 { get; set; }

        [XmlElement("DF-11")]
        public string? _DF_11 { get; set; }

        [XmlElement("DF-10")]
        public string? _DF_10 { get; set; }

        [XmlElement("DF-09")]
        public string? _DF_09 { get; set; }

        [XmlElement("DF-08")]
        public string? _DF_08 { get; set; }

        [XmlElement("DF-07")]
        public string? _DF_07 { get; set; }

        [XmlElement("DF-06")]
        public string? _DF_06 { get; set; }

        [XmlElement("DF-05")]
        public string? _DF_05 { get; set; }

        [XmlElement("DF-04")]
        public string? _DF_04 { get; set; }

        [XmlElement("DF-03")]
        public string? _DF_03 { get; set; }

        [XmlElement("DF-02")]
        public string? _DF_02 { get; set; }

        [XmlElement("DF-01")]
        public string? _DF_01 { get; set; }

        [XmlElement("DG-32")]
        public string? _DG_32 { get; set; }

        [XmlElement("DG-31")]
        public string? _DG_31 { get; set; }

        [XmlElement("DG-30")]
        public string? _DG_30 { get; set; }

        [XmlElement("DG-29")]
        public string? _DG_29 { get; set; }

        [XmlElement("DG-28")]
        public string? _DG_28 { get; set; }

        [XmlElement("DG-27")]
        public string? _DG_27 { get; set; }

        [XmlElement("DG-26")]
        public string? _DG_26 { get; set; }

        [XmlElement("DG-25")]
        public string? _DG_25 { get; set; }

        [XmlElement("DG-24")]
        public string? _DG_24 { get; set; }

        [XmlElement("DG-23")]
        public string? _DG_23 { get; set; }

        [XmlElement("DG-22")]
        public string? _DG_22 { get; set; }

        [XmlElement("DG-21")]
        public string? _DG_21 { get; set; }

        [XmlElement("DG-20")]
        public string? _DG_20 { get; set; }

        [XmlElement("DG-19")]
        public string? _DG_19 { get; set; }

        [XmlElement("DG-18")]
        public string? _DG_18 { get; set; }

        [XmlElement("DG-17")]
        public string? _DG_17 { get; set; }

        [XmlElement("DG-16")]
        public string? _DG_16 { get; set; }

        [XmlElement("DG-15")]
        public string? _DG_15 { get; set; }

        [XmlElement("DG-14")]
        public string? _DG_14 { get; set; }

        [XmlElement("DG-13")]
        public string? _DG_13 { get; set; }

        [XmlElement("DG-12")]
        public string? _DG_12 { get; set; }

        [XmlElement("DG-11")]
        public string? _DG_11 { get; set; }

        [XmlElement("DG-10")]
        public string? _DG_10 { get; set; }

        [XmlElement("DG-09")]
        public string? _DG_09 { get; set; }

        [XmlElement("DG-08")]
        public string? _DG_08 { get; set; }

        [XmlElement("DG-07")]
        public string? _DG_07 { get; set; }

        [XmlElement("DG-06")]
        public string? _DG_06 { get; set; }

        [XmlElement("DG-05")]
        public string? _DG_05 { get; set; }

        [XmlElement("DG-04")]
        public string? _DG_04 { get; set; }

        [XmlElement("DG-03")]
        public string? _DG_03 { get; set; }

        [XmlElement("DG-02")]
        public string? _DG_02 { get; set; }

        [XmlElement("DG-01")]
        public string? _DG_01 { get; set; }

        [XmlElement("DH-32")]
        public string? _DH_32 { get; set; }

        [XmlElement("DH-31")]
        public string? _DH_31 { get; set; }

        [XmlElement("DH-30")]
        public string? _DH_30 { get; set; }

        [XmlElement("DH-29")]
        public string? _DH_29 { get; set; }

        [XmlElement("DH-28")]
        public string? _DH_28 { get; set; }

        [XmlElement("DH-27")]
        public string? _DH_27 { get; set; }

        [XmlElement("DH-26")]
        public string? _DH_26 { get; set; }

        [XmlElement("DH-25")]
        public string? _DH_25 { get; set; }

        [XmlElement("DH-24")]
        public string? _DH_24 { get; set; }

        [XmlElement("DH-23")]
        public string? _DH_23 { get; set; }

        [XmlElement("DH-22")]
        public string? _DH_22 { get; set; }

        [XmlElement("DH-21")]
        public string? _DH_21 { get; set; }

        [XmlElement("DH-20")]
        public string? _DH_20 { get; set; }

        [XmlElement("DH-19")]
        public string? _DH_19 { get; set; }

        [XmlElement("DH-18")]
        public string? _DH_18 { get; set; }

        [XmlElement("DH-17")]
        public string? _DH_17 { get; set; }

        [XmlElement("DH-16")]
        public string? _DH_16 { get; set; }

        [XmlElement("DH-15")]
        public string? _DH_15 { get; set; }

        [XmlElement("DH-14")]
        public string? _DH_14 { get; set; }

        [XmlElement("DH-13")]
        public string? _DH_13 { get; set; }

        [XmlElement("DH-12")]
        public string? _DH_12 { get; set; }

        [XmlElement("DH-11")]
        public string? _DH_11 { get; set; }

        [XmlElement("DH-10")]
        public string? _DH_10 { get; set; }

        [XmlElement("DH-09")]
        public string? _DH_09 { get; set; }

        [XmlElement("DH-08")]
        public string? _DH_08 { get; set; }

        [XmlElement("DH-07")]
        public string? _DH_07 { get; set; }

        [XmlElement("DH-06")]
        public string? _DH_06 { get; set; }

        [XmlElement("DH-05")]
        public string? _DH_05 { get; set; }

        [XmlElement("DH-04")]
        public string? _DH_04 { get; set; }

        [XmlElement("DH-03")]
        public string? _DH_03 { get; set; }

        [XmlElement("DH-02")]
        public string? _DH_02 { get; set; }

        [XmlElement("DH-01")]
        public string? _DH_01 { get; set; }

        [XmlElement("AO-8")]
        public string? _AO_8 { get; set; }

        [XmlElement("AO-7")]
        public string? _AO_7 { get; set; }

        [XmlElement("AO-6")]
        public string? _AO_6 { get; set; }

        [XmlElement("AO-5")]
        public string? _AO_5 { get; set; }

        [XmlElement("AO-4")]
        public string? _AO_4 { get; set; }

        [XmlElement("AO-3")]
        public string? _AO_3 { get; set; }

        [XmlElement("AO-2")]
        public string? _AO_2 { get; set; }

        [XmlElement("AO-1")]
        public string? _AO_1 { get; set; }

        [XmlElement("AI-8")]
        public string? _AI_8 { get; set; }

        [XmlElement("AI-7")]
        public string? _AI_7 { get; set; }

        [XmlElement("AI-6")]
        public string? _AI_6 { get; set; }

        [XmlElement("AI-5")]
        public string? _AI_5 { get; set; }

        [XmlElement("AI-4")]
        public string? _AI_4 { get; set; }

        [XmlElement("AI-3")]
        public string? _AI_3 { get; set; }

        [XmlElement("AI-2")]
        public string? _AI_2 { get; set; }

        [XmlElement("AI-1")]
        public string? _AI_1 { get; set; }

        [XmlElement("GndDA-5")]
        public string? _GndDA_5 { get; set; }

        [XmlElement("GndDA-4")]
        public string? _GndDA_4 { get; set; }

        [XmlElement("GndDA-3")]
        public string? _GndDA_3 { get; set; }

        [XmlElement("GndDA-2")]
        public string? _GndDA_2 { get; set; }

        [XmlElement("GndDA-1")]
        public string? _GndDA_1 { get; set; }

        [XmlElement("GndDB-5")]
        public string? _GndDB_5 { get; set; }

        [XmlElement("GndDB-4")]
        public string? _GndDB_4 { get; set; }

        [XmlElement("GndDB-3")]
        public string? _GndDB_3 { get; set; }

        [XmlElement("GndDB-2")]
        public string? _GndDB_2 { get; set; }

        [XmlElement("GndDB-1")]
        public string? _GndDB_1 { get; set; }

        [XmlElement("GndDC-5")]
        public string? _GndDC_5 { get; set; }

        [XmlElement("GndDC-4")]
        public string? _GndDC_4 { get; set; }

        [XmlElement("GndDC-3")]
        public string? _GndDC_3 { get; set; }

        [XmlElement("GndDC-2")]
        public string? _GndDC_2 { get; set; }

        [XmlElement("GndDC-1")]
        public string? _GndDC_1 { get; set; }

        [XmlElement("GndDD-5")]
        public string? _GndDD_5 { get; set; }

        [XmlElement("GndDD-4")]
        public string? _GndDD_4 { get; set; }

        [XmlElement("GndDD-3")]
        public string? _GndDD_3 { get; set; }

        [XmlElement("GndDD-2")]
        public string? _GndDD_2 { get; set; }

        [XmlElement("GndDD-1")]
        public string? _GndDD_1 { get; set; }

        [XmlElement("GndDE-5")]
        public string? _GndDE_5 { get; set; }

        [XmlElement("GndDE-4")]
        public string? _GndDE_4 { get; set; }

        [XmlElement("GndDE-3")]
        public string? _GndDE_3 { get; set; }

        [XmlElement("GndDE-2")]
        public string? _GndDE_2 { get; set; }

        [XmlElement("GndDE-1")]
        public string? _GndDE_1 { get; set; }

        [XmlElement("GndDF-5")]
        public string? _GndDF_5 { get; set; }

        [XmlElement("GndDF-4")]
        public string? _GndDF_4 { get; set; }

        [XmlElement("GndDF-3")]
        public string? _GndDF_3 { get; set; }

        [XmlElement("GndDF-2")]
        public string? _GndDF_2 { get; set; }

        [XmlElement("GndDF-1")]
        public string? _GndDF_1 { get; set; }

        [XmlElement("GndDG-5")]
        public string? _GndDG_5 { get; set; }

        [XmlElement("GndDG-4")]
        public string? _GndDG_4 { get; set; }

        [XmlElement("GndDG-3")]
        public string? _GndDG_3 { get; set; }

        [XmlElement("GndDG-2")]
        public string? _GndDG_2 { get; set; }

        [XmlElement("GndDG-1")]
        public string? _GndDG_1 { get; set; }

        [XmlElement("GndDH-5")]
        public string? _GndDH_5 { get; set; }

        [XmlElement("GndDH-4")]
        public string? _GndDH_4 { get; set; }

        [XmlElement("GndDH-3")]
        public string? _GndDH_3 { get; set; }

        [XmlElement("GndDH-2")]
        public string? _GndDH_2 { get; set; }

        [XmlElement("GndDH-1")]
        public string? _GndDH_1 { get; set; }

        [XmlElement("GndAO-5")]
        public string? _GndAO_5 { get; set; }

        [XmlElement("GndAO-4")]
        public string? _GndAO_4 { get; set; }

        [XmlElement("GndAO-3")]
        public string? _GndAO_3 { get; set; }

        [XmlElement("GndAO-2")]
        public string? _GndAO_2 { get; set; }

        [XmlElement("GndAO-1")]
        public string? _GndAO_1 { get; set; }

        [XmlElement("GndAI-5")]
        public string? _GndAI_5 { get; set; }

        [XmlElement("GndAI-4")]
        public string? _GndAI_4 { get; set; }

        [XmlElement("GndAI-3")]
        public string? _GndAI_3 { get; set; }

        [XmlElement("GndAI-2")]
        public string? _GndAI_2 { get; set; }

        [XmlElement("GndAI-1")]
        public string? _GndAI_1 { get; set; }

        public int? PortDatID { get; set; }
    }
}
