namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di utensili
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotDefined")]
    public enum ToolTypeEnum : int
    {
        [EnumSerializationName("ALL")]
        All = -1,
        
        [EnumSerializationName("NotDefined")]
        [PlantUnit(PlantUnitEnum.None)]
        [EnumField("NotDefined", true, "LBL_TSNOTDEFINED", AttributeDefinitionEnum.ToolType)]
        [EnumField("NotDefined", true, "LBL_TSNOTDEFINED", AttributeDefinitionEnum.PreHoleTS)]
        [EnumField("NotDefined", true, "LBL_TSNOTDEFINED", AttributeDefinitionEnum.ProbeTS)]
        NotDefined = 0,
        
        /// <summary>
        /// Punzonatura circolare
        /// </summary>
        [DatabaseDisplayName("TS11")]
        [EnumCustomName("TS_11")]
        [Description("Punzonatura circolare")]
        [EnumSerializationName("PUNCH")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        
        TS11 = 11,

        /// <summary>
        /// Punzonatura ovale
        /// </summary>
        [DatabaseDisplayName("TS12")]
        [EnumCustomName("TS_12")]
        [Description("Punzonatura ovale")]
        [EnumSerializationName("PUNCH_12")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        TS12 = 12,

        /// <summary>
        /// Punzonatura rettangolare
        /// </summary>
        [DatabaseDisplayName("TS13")]
        [EnumCustomName("TS_13")]
        [Description("Punzonatura rettangolare")]
        [EnumSerializationName("PUNCH_13")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        TS13 = 13,

        /// <summary>
        /// Punzonatura triangolare
        /// </summary>
        [DatabaseDisplayName("TS14")]
        [EnumCustomName("TS_14")]
        [Description("Punzonatura triangolare")]
        [EnumSerializationName("PUNCH_14")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        TS14 = 14,

        /// <summary>
        /// Bulinatura con punzone
        /// </summary>
        [DatabaseDisplayName("TS15")]
        [EnumCustomName("TS_15")]
        [Description("Bulinatura con punzone")]
        [EnumSerializationName("PUNCH_15")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Bulinatura con punzone", true, "LBL_TS15", AttributeDefinitionEnum.ToolType)]
        TS15 = 15,

        /// <summary>
        /// Marcatura con punzone
        /// </summary>
        [DatabaseDisplayName("TS16")]
        [EnumCustomName("TS_16")]
        [Description("Marcatura con punzone")]
        [EnumSerializationName("PUNCH_MK")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Marcatura con punzone", true, "LBL_TS16", AttributeDefinitionEnum.ToolType)]
        TS16 = 16,

        /// <summary>
        /// Stozzatura con punzone
        /// </summary>
        [DatabaseDisplayName("TS17")]
        [EnumCustomName("TS_17")]
        [Description("Stozzatura con punzone")]
        [EnumSerializationName("PUNCH_NOTCH")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Stozzatura con punzone", true, "LBL_TS17", AttributeDefinitionEnum.ToolType)]
        TS17 = 17,

        /// <summary>
        /// Punzonatura speciale (TIPOC o TIPOB251)
        /// </summary>
        [DatabaseDisplayName("TS18")]
        [EnumCustomName("TS_18")]
        [Description("Punzonatura speciale (TIPOC o TIPOB251)")]
        [EnumSerializationName("PUNCH_18")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Punzonatura speciale", true, "LBL_TS18", AttributeDefinitionEnum.ToolType)]
        TS18 = 18,

        /// <summary>
        /// Imbutitura speciale (TIPOC o TIPOB251)
        /// </summary>
        [DatabaseDisplayName("TS19")]
        [EnumCustomName("TS_19")]
        [Description("Imbutitura  speciale (TIPOC o TIPOB251)")]
        [EnumSerializationName("PUNCH_19")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Imbutitura speciale", true, "LBL_TS19", AttributeDefinitionEnum.ToolType)]
        TS19 = 19,

        /// <summary>
        /// Bulinatura con punzone speciale (TIPOC o TIPOB251)
        /// </summary>
        [DatabaseDisplayName("TS20")]
        [EnumCustomName("TS_20")]
        [Description("Bulinatura con punzone speciale (TIPOC o TIPOB251)")]
        [EnumSerializationName("PUNCH_POINT")]
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [ToolCategory(ToolCategoryEnum.Punch)]
        [EnumField("Bulinatura con punzone speciale", true, "LBL_TS20", AttributeDefinitionEnum.ToolType)]
        TS20 = 20,

        /// <summary>
        /// Foratura a inserti programmata
        /// </summary>
        [DatabaseDisplayName("TS32")]
        [EnumCustomName("TS_32")]
        [Description("Foratura a inserti programmata")]
        [EnumSerializationName("DRILL_32")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Foratura a inserti programmata", true, "LBL_TS32",AttributeDefinitionEnum.ToolType)]
        [EnumField("Foratura a inserti programmata", true, "LBL_TS32", AttributeDefinitionEnum.PreHoleTS)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS32 = 32,

        /// <summary>
        /// Foratura elicoidale programmata
        /// </summary>
        [DatabaseDisplayName("TS33")]
        [EnumCustomName("TS_33")]
        [Description("Foratura elicoidale")]
        [EnumSerializationName("DRILL_33")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Foratura elicoidale programmata", true, "LBL_TS33",AttributeDefinitionEnum.ToolType)]
        [EnumField("Foratura elicoidale programmata", true, "LBL_TS33", AttributeDefinitionEnum.PreHoleTS)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS33 = 33,

        /// <summary>
        /// Sbavatura con utensile tradizionale
        /// </summary>
        [DatabaseDisplayName("TS34")]
        [EnumCustomName("TS_34")]
        [Description("Sbavatura tradizionale")]
        [EnumSerializationName("DEBURR")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Sbavatura con utensile tradizionale", true, "LBL_TS34", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS34 = 34,

        /// <summary>
        /// Svasatura
        /// </summary>
        [DatabaseDisplayName("TS35")]
        [EnumCustomName("TS_35")]
        [Description("Svasatura")]
        [EnumSerializationName("FLARE")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Svasatura", true, "LBL_TS35", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard, ToolWorkTypeEnum.Heavy)]
        TS35 = 35,

        /// <summary>
        /// Sbavatura con utensile speciale
        /// </summary>
        [DatabaseDisplayName("TS36")]
        [EnumCustomName("TS_36")]
        [Description("Sbavatura con utensile speciale")]
        [EnumSerializationName("DEBURR_36")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Sbavatura con utensile speciale", true, "LBL_TS36", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS36 = 36,

        /// <summary>
        /// Lamatura
        /// </summary>
        [DatabaseDisplayName("TS38")]
        [EnumCustomName("TS_38")]
        [Description("Lamatura")]
        [EnumSerializationName("BORE")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Lamatura", true, "LBL_TS38", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard, ToolWorkTypeEnum.Heavy)]
        TS38 = 38,

        /// <summary>
        /// Bulinatura con bulino
        /// </summary>
        [DatabaseDisplayName("TS39")]
        [EnumCustomName("TS_39")]
        [Description("Bulinatura con bulino")]
        [EnumSerializationName("POINT")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Bulinatura con bulino", true, "LBL_TS39", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS39 = 39,

        /// <summary>
        /// Carotatura
        /// </summary>
        [DatabaseDisplayName("TS40")]
        [EnumCustomName("TS_40")]
        [Description("Carotatura")]
        [EnumSerializationName("DRILL_40")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Carotatura", true, "LBL_TS40", AttributeDefinitionEnum.ToolType)]
        [EnumField("Carotatura", true, "LBL_TS40", AttributeDefinitionEnum.PreHoleTS)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS40 = 40,

        /// <summary>
        /// Maschiatura ISO metrico
        /// </summary>
        [DatabaseDisplayName("TS41")]
        [EnumCustomName("TS_41")]
        [Description("Maschiatura")]
        [EnumSerializationName("TAP")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Tapping)]
        [EnumField("Maschiatura ISO metrico", true, "LBL_TS41", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS41 = 41,

        /// <summary>
        /// Taglio con cesoia
        /// </summary>
        [DatabaseDisplayName("TS50")]
        [EnumCustomName("TS_50")]
        [Description("Taglio con cesoia")]
        [EnumSerializationName("SHEAR")]
        [PlantUnit(PlantUnitEnum.Shear)]
        [ToolCategory(ToolCategoryEnum.Cut)]
        [EnumField("Taglio con cesoia", true, "LBL_TS50", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS50 = 50,

        /// <summary>
        /// Taglio al plasma
        /// </summary>
        [DatabaseDisplayName("TS51")]
        [EnumCustomName("TS_51")]
        [Description("Taglio al plasma")]
        [EnumSerializationName("PLA")]
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        [ToolCategory(ToolCategoryEnum.Cut)]
        [EnumField("Taglio al plasma", true, "LBL_TS51", AttributeDefinitionEnum.ToolType)]
        TS51 = 51,

        /// <summary>
        /// Taglio con ossitaglio
        /// </summary>
        [DatabaseDisplayName("TS52")]
        [EnumCustomName("TS_52")]
        [Description("Taglio con ossitaglio")]
        [EnumSerializationName("OXY")]
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        [ToolCategory(ToolCategoryEnum.Cut)]
        [EnumField("Taglio con ossitaglio", true, "LBL_TS52", AttributeDefinitionEnum.ToolType)]
        TS52 = 52,

        /// <summary>
        /// Marcatura al plasma (marcature lettere o LEAD-CUT)
        /// </summary>
        [DatabaseDisplayName("TS53")]
        [EnumCustomName("TS_53")]
        [Description("Marcatura al plasma")]
        [EnumSerializationName("PLA_MK")]
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Marcatura al plasma", true, "LBL_TS53", AttributeDefinitionEnum.ToolType)]
        TS53 = 53,

        /// <summary>
        /// Taglio con ossitaglio bevel tripla (vedremo se in futuro dovrà esistere)
        /// </summary>
        [DatabaseDisplayName("TS54")]
        [EnumCustomName("TS_54")]
        [Description("Taglio con ossitaglio bevel tripla")]
        [EnumSerializationName("OXY_BEV")]
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        [ToolCategory(ToolCategoryEnum.Cut)]
        [EnumField("Taglio con ossitaglio bevel tripla", true, "LBL_TS54", AttributeDefinitionEnum.ToolType)]
        TS54 = 54,

        /// <summary>
        /// Lama a disco
        /// </summary>
        [DatabaseDisplayName("TS55")]
        [EnumCustomName("TS_55")]
        [Description("Lama a disco")]
        [EnumSerializationName("DISC_BLADE")]
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        [ToolCategory(ToolCategoryEnum.Saw)]
        [EnumField("Lama a disco", true, "LBL_TS55", AttributeDefinitionEnum.ToolType)]
        TS55 = 55,

        /// <summary>
        /// Lama a nastro
        /// </summary>
        [DatabaseDisplayName("TS56")]
        [EnumCustomName("TS_56")]
        [Description("Lama a nastro")]
        [EnumSerializationName("BAND_BLADE")]
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        [ToolCategory(ToolCategoryEnum.Saw)]
        [EnumField("Lama a nastro", true, "LBL_TS56", AttributeDefinitionEnum.ToolType)]
        TS56 = 56,

        /// <summary>
        /// Lama rotante
        /// </summary>
        [DatabaseDisplayName("TS57")]
        [EnumCustomName("TS_57")]
        [Description("Lama rotante")]
        [EnumSerializationName("ROTATING_BLADE")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Saw)]
        [EnumField("Lama rotante", true, "LBL_TS57", AttributeDefinitionEnum.ToolType)]
        TS57 = 57,

        /// <summary>
        /// Fresatura utensile cilindrico verticale (dall'alto)
        /// </summary>
        [DatabaseDisplayName("TS61")]
        [EnumCustomName("TS_61")]
        [Description("Fresatura utensile cilindrico verticale (dall'alto)")]
        [EnumSerializationName("MILL")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mill)]
        [EnumField("Fresatura utensile cilindrico verticale", true, "LBL_TS61", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS61 = 61,

        /// <summary>
        /// Incavatura (fresatura senza sfondamento) utensile cilindrico verticale (dall'alto)
        /// </summary>
        [DatabaseDisplayName("TS62")]
        [EnumCustomName("TS_62")]
        [Description("Incavatura (fresatura senza sfondamento) utensile cilindrico verticale dall'alto")]
        [EnumSerializationName("POCKET")]
        [WorkTypes(ToolWorkTypeEnum.Plunging, ToolWorkTypeEnum.Ramping)]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mill)]
        [EnumField("Incavatura", true, "LBL_TS62", AttributeDefinitionEnum.ToolType)]
        TS62 = 62,

        /// <summary>
        /// Utensile per scribing
        /// </summary>
        [DatabaseDisplayName("TS68")]
        [EnumCustomName("TS_68")]
        [Description("Utensile per scribing")]
        [EnumSerializationName("POCKET_68")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Utensile per scribing", true, "LBL_TS68", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS68 = 68,

        /// <summary>
        /// Incavatura utensile per foro inclinato
        /// </summary>
        [DatabaseDisplayName("TS69")]
        [EnumCustomName("TS_69")]
        [Description("Incavatura utensile per foro inclinato")]
        [EnumSerializationName("POCKET_69")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mill)]
        [EnumField("Incavatura utensile per foro inclinato", true, "LBL_TS69", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS69 = 69,

        /// <summary>
        /// Palpatura con utensile specifico
        /// </summary>
        [DatabaseDisplayName("TS70")]
        [EnumCustomName("TS_70")]
        [Description("Palpatura con utensile specifico")]
        [EnumSerializationName("SENSE_70")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Sense)]
        [EnumField("Palpatura con utensile specifico", true, "LBL_TS70",AttributeDefinitionEnum.ToolType)]
        [EnumField("Palpatura con utensile specifico", true, "LBL_TS70", AttributeDefinitionEnum.ProbeTS)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS70 = 70,

        /// <summary>
        /// Maschiatura con fresa ad inserti
        /// </summary>
        [DatabaseDisplayName("TS71")]
        [EnumCustomName("TS_71")]
        [Description("Maschiatura con fresa ad inserti")]
        [EnumSerializationName("TAP_71")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Tapping)]
        [EnumField(" Maschiatura con fresa ad inserti", true, "LBL_TS71", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS71 = 71,

        /// <summary>
        /// Utensile foratura+svasatura
        /// </summary>
        [DatabaseDisplayName("TS73")]
        [EnumCustomName("TS_73")]
        [Description("Utensile per foratura e svasatura")]
        [EnumSerializationName("DFLARE")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Utensile foratura+svasatura", true, "LBL_TS73", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard, ToolWorkTypeEnum.Heavy)]
        TS73 = 73,

        /// <summary>
        /// Utensile foratura+doppia sbavatura
        /// </summary>
        [DatabaseDisplayName("TS74")]
        [EnumCustomName("TS_74")]
        [Description("Utensile per foratura e doppia sbavatura")]
        [EnumSerializationName("DDEBURR")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Drill)]
        [EnumField("Utensile foratura+doppia sbavatura", true, "LBL_TS74", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS74 = 74,

        /// <summary>
        /// Fresatura generica dal basso (utensile che si imprigiona !)
        /// </summary>
        [DatabaseDisplayName("TS75")]
        [EnumCustomName("TS_75")]
        [Description("Fresatura generica dal basso")]
        [EnumSerializationName("POCKET_75")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mill)]
        [EnumField("Fresatura generica dal basso", true, "LBL_TS75", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS75 = 75,

        /// <summary>
        /// Scribing rotante (anima e lato interno ali)
        /// </summary>
        [DatabaseDisplayName("TS76")]
        [EnumCustomName("TS_76")]
        [Description("Scribing rotante (anima e lato interno ali)")]
        [EnumSerializationName("POCKET_76")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Scribing rotante", true, "LBL_TS76", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS76 = 76,

        /// <summary>
        /// Matita rotante (anima e lato interno/esterno ali)
        /// </summary>
        [DatabaseDisplayName("TS77")]
        [EnumCustomName("TS_77")]
        [Description("Matita rotante (anima e lato interno/esterno ali)")]
        [EnumSerializationName("PENCIL_77")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Matita rotante", true, "LBL_TS77", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS77 = 77,

        /// <summary>
        /// Matita fissa (anima e lato interno/esterno ali)
        /// </summary>
        [DatabaseDisplayName("TS78")]
        [EnumCustomName("TS_78")]
        [Description("Matita fissa")]
        [EnumSerializationName("PENCIL_78")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Matita fissa", true, "LBL_TS78", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS78 = 78,

        /// <summary>
        /// Scribing pneumatico
        /// </summary>
        [DatabaseDisplayName("TS79")]
        [EnumCustomName("TS_79")]
        [Description("Scribing pneumatico")]
        [EnumSerializationName("POCKET_79")]
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Scribing pneumatico", true, "LBL_TS79", AttributeDefinitionEnum.ToolType)]
        [WorkTypes(ToolWorkTypeEnum.Standard)]
        TS79 = 79,

        /// <summary>
        /// Palpatura con dispositivo meccanico
        /// </summary>
        [DatabaseDisplayName("TS80")]
        [EnumCustomName("TS_80")]
        [Description("Palpatura con dispositivo meccanico")]
        [EnumSerializationName("SENSE_80")]
        [PlantUnit(PlantUnitEnum.PalpingMachine)]
        [ToolCategory(ToolCategoryEnum.Sense)]
        [EnumField("Palpatura con dispositivo meccanico", true, "LBL_TS80", AttributeDefinitionEnum.ToolType)]
        [EnumField("Palpatura con dispositivo meccanico", true, "LBL_TS80", AttributeDefinitionEnum.ProbeTS)]
        TS80 = 80,

        /// <summary>
        /// Marcatura di caratteri a percussione
        /// </summary>
        [DatabaseDisplayName("TS86")]
        [EnumCustomName("TS_86")]
        [Description("Marcatura caratteri a percussione")]
        [EnumSerializationName("MARK_86")]
        [PlantUnit(PlantUnitEnum.CharMarker)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Marcatura di caratteri a percussione", true, "LBL_TS86", AttributeDefinitionEnum.ToolType)]
        TS86 = 86,

        /// <summary>
        /// Marcatura di caratteri a cassetti
        /// </summary>
        [DatabaseDisplayName("TS87")]
        [EnumCustomName("TS_87")]
        [Description("Marcatura caratteri a cassetti")]
        [EnumSerializationName("MARK_87")]
        [PlantUnit(PlantUnitEnum.CharMarker)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Marcatura di caratteri a cassetti", true, "LBL_TS87", AttributeDefinitionEnum.ToolType)]
        TS87 = 87,

        /// <summary>
        /// Marcatura a scrittura
        /// </summary>
        [DatabaseDisplayName("TS88")]
        [EnumCustomName("TS_88")]
        [Description("Marcatura caratteri a disco")]
        [EnumSerializationName("MARK_88")]
        [PlantUnit(PlantUnitEnum.CharMarker)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Marcatura a scrittura", true, "LBL_TS88", AttributeDefinitionEnum.ToolType)]
        TS88 = 88,

        /// <summary>
        /// Marcatura a getto d'inchiostro
        /// </summary>
        [DatabaseDisplayName("TS89")]
        [EnumCustomName("TS_89")]
        [Description("Marcatura a getto di inchiostro")]
        [EnumSerializationName("MARK_JET")]
        [PlantUnit(PlantUnitEnum.InkJetMarker)]
        [ToolCategory(ToolCategoryEnum.Mark)]
        [EnumField("Marcatura a getto d'inchiostro", true, "LBL_TS89", AttributeDefinitionEnum.ToolType)]
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
        public static ToolRangeTypeEnum ToToolRangeType(this ToolTypeEnum toolType)
        {
            var plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
            var toolRangeType = ToolRangeTypeEnum.None;
            switch (plantUnit)
            {
                case PlantUnitEnum.DrillingMachine:
                    toolRangeType = ToolRangeTypeEnum.Drill;
                    break;
                case PlantUnitEnum.PlasmaTorch:
                case PlantUnitEnum.OxyCutTorch:
                    toolRangeType = ToolRangeTypeEnum.Cut;
                    break;
            }

            return toolRangeType;
        }

        /// <summary>
        /// Get related entity type from toolType
        /// </summary>
        /// <param name="toolType"></param>
        /// <returns></returns>
        public static EntityTypeEnum ToEntityType(this ToolTypeEnum toolType)
        {
            return (EntityTypeEnum)toolType;
        }
    }
}