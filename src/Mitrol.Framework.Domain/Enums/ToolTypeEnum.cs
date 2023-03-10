namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di utensili
    /// </summary>
    //[TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotDefined")]
    public enum ToolTypeEnum : int
    {
        All = 1,
        NotDefined = 0,
        TS11 = 11,
        TS12 = 12,
        TS13 = 13,
        TS14 = 14,
        TS15 = 15,
        TS16 = 16,
        TS17 = 17,
        TS18 = 18,
        TS19 = 19,
        TS20 = 20,
        TS32 = 32,
        TS33 = 33,
        TS34 = 34,
        TS35 = 35,
        TS36 = 36,
        TS38 = 38,
        TS39 = 39,
        TS40 = 40,
        TS41 = 41,
        TS50 = 50,
        TS51 = 51,
        TS52 = 52,
        TS53 = 53,
        TS54 = 54,
        TS55 = 55,
        TS56 = 56,
        TS57 = 57,
        TS61 = 61,
        TS62 = 62,
        TS68 = 68,
        TS69 = 69,
        TS70 = 70,
        TS71 = 71,
        TS73 = 73,
        TS74 = 74,
        TS75 = 75,
        TS76 = 76,
        TS77 = 77,
        TS78 = 78,
        TS79 = 79,
        TS80 = 80,
        TS86 = 86,
        TS87 = 87,
        TS88 = 88,
        TS89 = 89
    }

    public static class ToolTypeEnumExtensions
    {
        /// <summary>
        /// TS di foratura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsForo(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS32
            || ts == ToolTypeEnum.TS33
            || ts == ToolTypeEnum.TS40;

        /// <summary>
        /// TS di svasatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsSvasa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS35;

        /// <summary>
        /// TS di foratura + svasatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsForaSvasa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS73;

        /// <summary>
        /// TS di foratura + sbavatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsForaSbava(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS74;

        /// <summary>
        /// TS di sbavatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsSbava(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS34
            || ts == ToolTypeEnum.TS36;

        /// <summary>
        /// TS di lamatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsLama(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS38;

        /// <summary>
        /// TS di bulinatura copn bulino
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsBulina(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS39;

        /// <summary>
        /// TS di carotatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsCarotatura(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS40;

        /// <summary>
        /// TS di foratura inclinata
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsForoInclinato(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS69;

        /// <summary>
        /// TS di bulinatura con punta per forare
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsForaBulina(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS33;

        /// <summary>
        /// TS di maschiatura con maschio
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMaschia(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS41;

        /// <summary>
        /// TS generico di foratura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsFora(this ToolTypeEnum ts)
            => ts.IsTsForo()
            || ts.IsTsCarotatura()
            || ts.IsTsSbava()
            || ts.IsTsSvasa()
            || ts.IsTsLama()
            || ts.IsTsBulina()
            || ts.IsTsForaSvasa()
            || ts.IsTsForaSbava();


        /// <summary>
        /// TS generico di foratura o maschiatura o fresatura o palpatura o incavatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsFoMa(this ToolTypeEnum ts)
            => ts.IsTsFora()
            || ts.IsTsMaschia()
            || ts.IsTsFresa()
            || ts.IsTsPalpa()
            || ts.IsTsIncav()
            || ts.IsTsMaschiaFresa()
            || ts.IsTsForoInclinato();


        /// <summary>
        /// TS di maschiatura con fresa
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMaschiaFresa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS71;

        /// <summary>
        /// TS di scribing
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsScribing(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS68
            || ts == ToolTypeEnum.TS76
            || ts == ToolTypeEnum.TS77
            || ts == ToolTypeEnum.TS78
            || ts == ToolTypeEnum.TS79;

        /// <summary>
        /// TS di scribing con utensile rotante
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsScribingRotante(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS76;

        /// <summary>
        /// TS di scribing con utensile rotante
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsScribingPneumatico(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS79;

        /// <summary>
        /// TS di scribing con matita rotante
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMatitaRotante(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS77;

        /// <summary>
        /// TS di scribing con matita fissa
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMatitaFissa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS78;

        /// <summary>
        /// TS di fresatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsFresa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS61;

        /// <summary>
        /// TS di incavatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsIncav(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS62;

        /// <summary>
        /// TS di incavatura inferiore (da sotto)
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsIncavInf(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS75;

        /// <summary>
        /// TS generico di fresatura/incavatura/scribing
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsFreInc(this ToolTypeEnum ts)
            => ts.IsTsFresa()
            || ts.IsTsIncav()
            || ts.IsTsIncavInf()
            || ts.IsTsMaschiaFresa()
            || ts.IsTsScribing();

        /// <summary>
        /// TS di punzonatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPunzo(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS11
            || ts == ToolTypeEnum.TS12
            || ts == ToolTypeEnum.TS13
            || ts == ToolTypeEnum.TS14
            || ts == ToolTypeEnum.TS15
            || ts == ToolTypeEnum.TS16
            || ts == ToolTypeEnum.TS17
            || ts == ToolTypeEnum.TS18
            || ts == ToolTypeEnum.TS19
            || ts == ToolTypeEnum.TS20;

        /// <summary>
        /// TS di punzonatura con codice punzone
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPunzoCodice(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS12
            || ts == ToolTypeEnum.TS13
            || ts == ToolTypeEnum.TS14;

        /// <summary>
        /// TS di marcatura con punzone
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMarcaPunzo(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS16;

        /// <summary>
        /// TS di punzonatura speciale per impianti TipoC
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPunzoTipoC(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS18
            || ts == ToolTypeEnum.TS19
            || ts == ToolTypeEnum.TS20;

        /// <summary>
        /// TS di taglio al plasma
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPla(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS51;

        /// <summary>
        /// TS di scribing al plasma
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPlaScr(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS53;

        /// <summary>
        /// TS di taglio con ossitaglio
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsOxy(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS52
            || ts == ToolTypeEnum.TS54;

        /// <summary>
        /// TS di taglio con ossitaglio bevel (probabile da eliminare !!!)
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsOxyBevel(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS54;

        /// <summary>
        /// TS di marcatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMarca(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS87
            || ts == ToolTypeEnum.TS88
            || ts == ToolTypeEnum.TS89;

        /// <summary>
        /// TS di marcatura a disco
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMarcaDisco(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS88;

        /// <summary>
        /// TS di marcatura a cassetti
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMarcaCassetti(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS87;

        /// <summary>
        /// TS di marcatura a getto d'inchiostro
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsMarcaJet(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS89;

        /// <summary>
        /// TS di palpatura
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool IsTsPalpa(this ToolTypeEnum ts)
            => ts == ToolTypeEnum.TS70
            || ts == ToolTypeEnum.TS80;

        /// <summary>
        ///  Get ToolRange Type from ToolType
        /// </summary>
        /// <param name="toolType"></param>
        /// <returns></returns>
        //public static ToolRangeTypeEnum GetToolRangeType(this ToolTypeEnum toolType)
        //{
        //    var plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
        //    var toolRangeType = ToolRangeTypeEnum.None;
        //    switch (plantUnit)
        //    {
        //        case PlantUnitEnum.DrillingMachine:
        //            toolRangeType = ToolRangeTypeEnum.Drill;
        //            break;
        //        case PlantUnitEnum.PlasmaTorch:
        //        case PlantUnitEnum.OxyCutTorch:
        //            toolRangeType = ToolRangeTypeEnum.Cut;
        //            break;
        //    }

        //    return toolRangeType;
        //}
    }
}