namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di utensili
    /// </summary>
    [DefaultValue("NotDefined")]
    public enum ToolTypeEnum : int
    {
        All = 1,
        [PlantUnit(PlantUnitEnum.None)]
        NotDefined = 0,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS11 = 11,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS12 = 12,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS13 = 13,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS14 = 14,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS15 = 15,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS16 = 16,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS17 = 17,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS18 = 18,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS19 = 19,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        TS20 = 20,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS32 = 32,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS33 = 33,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS34 = 34,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS35 = 35,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS36 = 36,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS38 = 38,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS39 = 39,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS40 = 40,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS41 = 41,
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        TS50 = 50,
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        TS51 = 51,
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        TS52 = 52,
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        TS53 = 53,
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        TS54 = 54,
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        TS55 = 55,
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        TS56 = 56,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS57 = 57,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS61 = 61,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS62 = 62,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS68 = 68,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS69 = 69,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS70 = 70,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS71 = 71,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS73 = 73,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS74 = 74,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS75 = 75,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS76 = 76,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS77 = 77,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS78 = 78,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        TS79 = 79,
        [PlantUnit(PlantUnitEnum.PalpingMachine)]
        TS80 = 80,
        [PlantUnit(PlantUnitEnum.CharMarker)]
        TS86 = 86,
        [PlantUnit(PlantUnitEnum.CharMarker)]
        TS87 = 87,
        [PlantUnit(PlantUnitEnum.CharMarker)]
        TS88 = 88,
        [PlantUnit(PlantUnitEnum.InkJetMarker)]
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

        public static EntityTypeEnum ToEntityType(this ToolTypeEnum ts, ProcessingTechnologyEnum processTechonology)
        {
            var entityType = EntityTypeEnum.NotDefined;
            switch(ts)
            {
                case ToolTypeEnum.TS51:
                    {
                        if (processTechonology == ProcessingTechnologyEnum.PlasmaHPR)
                        {
                            entityType = EntityTypeEnum.ToolTS51HPR;
                        } else if (processTechonology == ProcessingTechnologyEnum.PlasmaXPR)
                        {
                            entityType = EntityTypeEnum.ToolTS51XPR;
                        }
                        else
                        {
                            entityType = EntityTypeEnum.ToolTS51;
                        }
                    }
                    break;
                case ToolTypeEnum.TS53:
                    {
                        if (processTechonology == ProcessingTechnologyEnum.PlasmaHPR)
                        {
                            entityType = EntityTypeEnum.ToolTS53HPR;
                        }
                        else if (processTechonology == ProcessingTechnologyEnum.PlasmaXPR)
                        {
                            entityType = EntityTypeEnum.ToolTS53XPR;
                        }
                        else
                        {
                            entityType = EntityTypeEnum.ToolTS53;
                        }
                    }
                    break;
                default:
                    entityType = (EntityTypeEnum)ts;
                    break;
            }

            return entityType;
        }
    }
}