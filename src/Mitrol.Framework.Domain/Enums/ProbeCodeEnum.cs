namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;
    /// <summary>
    /// Definizione dei codici di palpatura programmati nelle operazioni del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum ProbeCodeEnum : int
    {
        /// <summary>
        /// Palpatura non programmata
        /// </summary>
        [EnumSerializationName("0")]
        [EnumField("Palpatura non programmata", true, "LBL_PROBECODE_NONE")]
        None = 0,

        /// <summary>
        /// Senza palpatura (annullamento delle palpature attive dai NODE superiori)
        /// </summary>
        [EnumSerializationName("70")]
        [EnumField("Senza palpatura", true, "LBL_PROBECODE_M70")]
        M70 = 70,

        M801 = 801,
        M802 = 802,
        M803 = 803,
        M804 = 804,
        M805 = 805,
        M806 = 806,
        M807 = 807,
        M808 = 808,
        M809 = 809,
        M810 = 810,
        M811 = 811,
        M812 = 812,
        M813 = 813,
        M814 = 814,
        M815 = 815,
        M816 = 816,
        M817 = 817,
        M818 = 818,
        M819 = 819,
        M820 = 820,
        M821 = 821,
        M822 = 822,
        M823 = 823,
        M824 = 824,
        M825 = 825,
        M826 = 826,
        M827 = 827,
        M828 = 828,
        M829 = 829,
        M830 = 830,
        M831 = 831,
        M832 = 832,
        M833 = 833,
        M834 = 834,
        M835 = 835,
        M836 = 836,
        M837 = 837,
        M838 = 838,
        M839 = 839,
        M840 = 840,

        /// <summary>
        /// Palpatura per la testa di foratura con dispositivo laser (GEMINI)
        /// </summary>
        [EnumSerializationName("843")]
        [EnumField("Palpatura per la testa di foratura con dispositivo laser", true, "LBL_PROBECODE_M843")]
        M843 = 843,

        M844 = 844,
        M845 = 845,
        M846 = 846,
        M847 = 847,
        M848 = 848,
        M849 = 849,
        M850 = 850,
        M851 = 851,

        /// <summary>
        /// Palpatura per la testa di foratura con utensile speciale TS70 (GEMINI)
        /// </summary>
        [EnumSerializationName("853")]
        [EnumField("Palpatura per la testa di foratura con utensile speciale TS70", true, "LBL_PROBECODE_M853")]
        M853 = 853,

        M854 = 854,
        M855 = 855,
        M856 = 856,
        M857 = 857,
        M858 = 858,
        M859 = 859,
        M860 = 860,
        M861 = 861,

        /// <summary>
        /// Palpatura per la testa di foratura con dispositivo meccanico (GEMINI)
        /// </summary>
        [EnumSerializationName("863")]
        [EnumField("Palpatura per la testa di foratura con dispositivo meccanico", true, "LBL_PROBECODE_M863")]
        M863 = 863,

        M864 = 864,
        M865 = 865,
        M866 = 866,
        M867 = 867,
        M868 = 868,
        M869 = 869,
        M870 = 870,
        M871 = 871,
        M873 = 873,
        M874 = 874,
        M875 = 875,
        M876 = 876,
        M877 = 877,
        M878 = 878,
        M879 = 879,
        M880 = 880
    }
}
