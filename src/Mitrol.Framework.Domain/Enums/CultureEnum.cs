﻿namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    public enum CultureEnum : int
    {
        [DatabaseDisplayName("it-IT")]
        [Description("Italiano")]
        ITA=1,
        [DatabaseDisplayName("en-GB")]
        [Description("English")]
        ING,
        [DatabaseDisplayName("en-US")]
        [Description("English (USA)")]
        USA,
        [DatabaseDisplayName("de-DE")]
        [Description("Deutsch")]
        TED,
        [DatabaseDisplayName("fr-FR")]
        [Description("Français")]
        FRA,
        [DatabaseDisplayName("es-ES")]
        [Description("Español")]
        SPA,
        [DatabaseDisplayName("pt-BR")]
        [Description("Português")]
        POR,
        [DatabaseDisplayName("fi-FI")]
        [Description("Suomi")]
        FIN,
        [DatabaseDisplayName("nl-NL")]
        [Description("Nederlands")]
        OLA,
        [DatabaseDisplayName("ru-RU")]
        [Description("Russian")]
        RUS,
        [DatabaseDisplayName("ro-RO")]
        [Description("Romana")]
        RUM,
        [DatabaseDisplayName("nb-NO")]
        [Description("Norsk")]
        NOR,
        [DatabaseDisplayName("sl-SI")]
        [Description("Slovenský")]
        SLO,
        [DatabaseDisplayName("pl-PL")]
        [Description("Polski")]
        PL,
        [DatabaseDisplayName("tr-TR")]
        [Description("Türkçe")]
        TR,
        [DatabaseDisplayName("el-GR")]
        [Description("Greek")]
        GR,
        [DatabaseDisplayName("hu-HU")]
        [Description("Magyar")]
        HU,
        [DatabaseDisplayName("da-DK")]
        [Description("Danish")]
        DA,
        [DatabaseDisplayName("cs-CZ")]
        [Description("Cesky")]
        CZE,
        [DatabaseDisplayName("zh-CN")]
        [Description("中文")]
        CIN,
        [DatabaseDisplayName("ko-KR")]
        [Description("Korean")]
        KR,
        [DatabaseDisplayName("sv-SE")]
        [Description("Svenska")]
        SVE,
        [DatabaseDisplayName("ja-JP")]
        [Description("日本語")]
        JPN
    }

}