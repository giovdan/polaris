namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDefinitionEnum
    {
        None = 0,
        // Tool, ToolTable
        [Description("Tipo di materiale")]
        [EnumSerializationName("MaterialType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaterialType)]
        [AttributeInfo(
                            attributeType: AttributeTypeEnum.Identifier
                            , clientControlType: ClientControlTypeEnum.Combo
                            , level: ProtectionLevelEnum.Medium
                            , attributeDataFormat: AttributeDataFormatEnum.AsIs
                            , attributeKind: AttributeKindEnum.Enum
                            , typeName: typeof(MaterialTypeEnum))]
        MaterialType = 1,

        [Description("n° di revisione")]
        [EnumSerializationName("RevNum")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RevNum)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Label
            , level: ProtectionLevelEnum.ReadOnly)]
        RevNum = 2,

        [Description("Spessore minimo")]
        [EnumSerializationName("MinThickness")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinThickness)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        MinThickness = 3,

        [Description("Spessore max")]
        [EnumSerializationName("MaxThickness")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxThickness)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Identifier
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium)]
        MaxThickness = 4,

        [Description("Diametro nominale [mm]")]
        [EnumSerializationName("DN")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DN)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium
                    )]
        NominalDiameter = 5,

        [Description("Codice Fornitore")]
        [EnumSerializationName("COD")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.COD)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.String
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        Code = 6,

        [Description("Tipo di materiale abilitato")]
        [EnumSerializationName("MaterialTypeEnable")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaterialTypeEnable)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Generic
           , attributeDataFormat: AttributeDataFormatEnum.AsIs
           , clientControlType: ClientControlTypeEnum.Combo
           , level: ProtectionLevelEnum.Medium
           , attributeKind: AttributeKindEnum.Enum
           , typeName: typeof(MaterialTypeCombinationEnum)
           )]
        MaterialTypeEnable = 7,

        [Description("Soglia affilatura punta [mt]")]
        [EnumSerializationName("WarningToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WarningToolLife)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Distance
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Medium)]
        WarningToolLife = 8,

        [Description("Massima vita utensile corrente [mt]")]
        [EnumSerializationName("MaxToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxToolLife)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
               , attributeKind: AttributeKindEnum.Number
               , attributeDataFormat: AttributeDataFormatEnum.Distance
               , clientControlType: ClientControlTypeEnum.Edit
               , level: ProtectionLevelEnum.Medium)]
        MaxToolLife = 9,

        [Description("Vita utensile corrente [mt]")]
        [EnumSerializationName("ToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolLife)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Distance
                , clientControlType: ClientControlTypeEnum.Label
                , level: ProtectionLevelEnum.ReadOnly)]
        ToolLife = 10,

        [Description("Limite Raffreddamento punta [mt]")]
        [EnumSerializationName("CoolingLimit")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CoolingLimit)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Distance
            , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal
            )]
        CoolingLimit = 11,

        [Description("Raffreddamento punta (valore corrente) [mt]")]
        [EnumSerializationName("CoolingValue")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CoolingValue)]
        [AttributeInfo(
                       attributeType: AttributeTypeEnum.Generic
                       , attributeKind: AttributeKindEnum.Number
                       , attributeDataFormat: AttributeDataFormatEnum.Distance
                       , clientControlType: ClientControlTypeEnum.Label
                       , level: ProtectionLevelEnum.ReadOnly)]
        CoolingValue = 12,

        [Description("Prenotazione automatica del sensitivo [n]")]
        [EnumSerializationName("AutoSensitiveEnable")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AutoSensitiveEnable)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal)]
        AutoSensitiveEnable = 13,

        [Description("Utensile con caricamento manuale dall'operatore [0/1]")]
        [EnumSerializationName("ManualLoadingTool")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ManualLoadingTool)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Generic
           , clientControlType: ClientControlTypeEnum.Check
           , level: ProtectionLevelEnum.Normal
           , attributeKind: AttributeKindEnum.Bool)]
        ManualLoadingTool = 14,

        [Description("Utensile ad assegnazione manuale nel magazzino [0/1]")]
        [EnumSerializationName("ManualMagazineAssignmentTool")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ManualMagazineAssignmentTool)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                     , clientControlType: ClientControlTypeEnum.Check
                     , level: ProtectionLevelEnum.Normal
                    , attributeKind: AttributeKindEnum.Bool)]
        ManualMagazineAssignmentTool = 15,

        [Description("Abilitazione ciclo con aspiratore trucioli [0/1]")]
        [EnumSerializationName("ChipExtractionEnable")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ChipExtractionEnable)]
        [AttributeInfo( attributeType: AttributeTypeEnum.Generic
                        , clientControlType: ClientControlTypeEnum.Check
                        , level: ProtectionLevelEnum.Normal
                       , attributeKind: AttributeKindEnum.Bool)]
        ChipExtractionEnable = 16,

        [Description("Tipologia di ciclo dell'utensile (custom) [n]")]
        [EnumSerializationName("CustomToolType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CustomToolType)]
        [AttributeInfo(
               attributeType: AttributeTypeEnum.Generic
               , attributeKind: AttributeKindEnum.Number
               , attributeDataFormat: AttributeDataFormatEnum.AsIs
               , clientControlType: ClientControlTypeEnum.Edit
               , level: ProtectionLevelEnum.Medium)]
        CustomToolType = 17,

        [Description("Diametro reale [mm]")]
        [EnumSerializationName("RealDn")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RealDn)]
        [AttributeInfo(
                      attributeType: AttributeTypeEnum.Geometric
                      , attributeKind: AttributeKindEnum.Number
                      , attributeDataFormat: AttributeDataFormatEnum.Diameter
                      , clientControlType: ClientControlTypeEnum.Override
                      , level: ProtectionLevelEnum.Normal)]
        RealDiameter = 18,

        [Description("Lunghezza utensile [mm]")]
        [EnumSerializationName("ToolLength")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolLength)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , attributeScope: AttributeScopeEnum.Fundamental)]
        ToolLength = 19,

        [Description("Codice del tool holder")]
        [EnumSerializationName("ToolHolderCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolHolderCode)]
        [AttributeInfo(
                       attributeType: AttributeTypeEnum.Geometric
                       , attributeKind: AttributeKindEnum.Enum
                       , attributeDataFormat: AttributeDataFormatEnum.AsIs
                       , clientControlType: ClientControlTypeEnum.ListBox
                       , level: ProtectionLevelEnum.Normal
                       , valueType: ValueTypeEnum.DynamicEnum
                       , url: "api/v1/OData/ToolHolders/listbox")]
        ToolHolderCode = 20,

        [Description("Posizione di start foro [mm]")]
        [EnumSerializationName("StartPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.StartPosition)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                        , clientControlType: ClientControlTypeEnum.Edit
                        , level: ProtectionLevelEnum.Normal
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.Length
                        , attributeScope: AttributeScopeEnum.Fundamental)]
        StartPosition = 21,

        [Description("Posizione di accostamento rapido [mm]")]
        [EnumSerializationName("FastApproachPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FastApproachPosition)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Normal)]
        FastApproachPosition = 22,

        [Description("Posizione di fine foro [mm]")]
        [EnumSerializationName("EndPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.EndPosition)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , attributeScope: AttributeScopeEnum.Fundamental
                , level: ProtectionLevelEnum.Normal)]
        EndPosition = 23,

        [Description("Angolo di affilatura punta [gra]")]
        [EnumSerializationName("GrindingAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.GrindingAngle)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Angle
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium
                    )]
        GrindingAngle = 24,

        [Description("Preset tool sbavatore [mm]")]
        [EnumSerializationName("PresetDeburringTool")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PresetDeburringTool)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            , attributeDataFormat: AttributeDataFormatEnum.Length)]
        PresetDeburringTool = 25,

        [Description("Diametro placchette [mm]")]
        [EnumSerializationName("MillingInsertInscribedCircle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingInsertInscribedCircle)]
        [AttributeInfo(
                           attributeType: AttributeTypeEnum.Geometric
                           , attributeKind: AttributeKindEnum.Number
                           , attributeDataFormat: AttributeDataFormatEnum.Diameter
                           , clientControlType: ClientControlTypeEnum.Edit
                           , level: ProtectionLevelEnum.Medium)]
        MillingInsertInscribedCircle = 26,

        [Description("Raggio spigolo placchette quadre [mm]")]
        [EnumSerializationName("MillingCornerRadius")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingCornerRadius)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Geometric
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Medium)]
        MillingCornerRadius = 27,

        [Description("Diametro gambo fresa [mm]")]
        [EnumSerializationName("MillingShankDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingShankDiameter)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        MillingShankDiameter = 28,

        [Description("Altezza gambo fresa [mm]")]
        [EnumSerializationName("MillingShankHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingShankHeight)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Geometric
           , attributeKind: AttributeKindEnum.Number
           , attributeDataFormat: AttributeDataFormatEnum.Length
           , clientControlType: ClientControlTypeEnum.Edit
           , level: ProtectionLevelEnum.Medium)]
        MillingShankHeight = 29,

        [Description("Lunghezza del tagliente [mm]")]
        [EnumSerializationName("CuttingEdge")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingEdge)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Geometric
           , attributeKind: AttributeKindEnum.Number
           , attributeDataFormat: AttributeDataFormatEnum.Length
           , clientControlType: ClientControlTypeEnum.Edit
           , level: ProtectionLevelEnum.Medium)]
        CuttingEdge = 30,

        [Description("Offset tagliente [mm]")]
        [EnumSerializationName("CuttingEdgeOff")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingEdgeOff)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Geometric
           , attributeKind: AttributeKindEnum.Number
           , attributeDataFormat: AttributeDataFormatEnum.Length
           , clientControlType: ClientControlTypeEnum.Edit
           , level: ProtectionLevelEnum.Medium)]
        CuttingEdgeOff = 31,

        [Description("Max. angolo rampa [gra]")]
        [EnumSerializationName("MaxRampAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxRampAngle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Angle
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium)]
        MaxRampAngle = 32,

        [Description("Ciclo con Rotazione inversa [bool]")]
        [EnumSerializationName("ReverseRotationCycle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ReverseRotationCycle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Medium
            , attributeKind: AttributeKindEnum.Bool)]
        ReverseRotationCycle = 33,

        [Description("Passo min maschiatura con fresa [mm]")]
        [EnumSerializationName("MinTappingPitch")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinTappingPitch)]
        [AttributeInfo(
                  attributeType: AttributeTypeEnum.Geometric
                  , attributeKind: AttributeKindEnum.Number
                  , attributeDataFormat: AttributeDataFormatEnum.Length
                  , clientControlType: ClientControlTypeEnum.Edit
                  , level: ProtectionLevelEnum.Medium
                  )]
        MinTappingPitch = 34,

        [Description("Passo max maschiatura con fresa [mm]")]
        [EnumSerializationName("MaxTappingPitch")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxTappingPitch)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium
                    )]
        MaxTappingPitch = 35,

        [Description("Numero di denti fresa ad inserti per maschiatura")]
        [EnumSerializationName("MillingCutterTeeth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingCutterTeeth)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium)]
        MillingCutterTeeth = 36,

        [Description("Angolo di affilatura svasatore [gra]")]
        [EnumSerializationName("CountersinkAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkAngle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Angle
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            )]
        CountersinkAngle = 37,

        [Description("Altezza punta svasatore [mm]")]
        [EnumSerializationName("CountersinkHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkHeight)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            )]
        CountersinkHeight = 38,

        [Description("Diametro della punta per forare svasatore [mm]")]
        [EnumSerializationName("CountersinkDrillingDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkDrillingDiameter)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Diameter
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            )]
        CountersinkDrillingDiameter = 39,

        [Description("Accostamento sbavatura esterna [mm]")]
        [EnumSerializationName("ApproachExternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ApproachExternalDeburr)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                  , attributeKind: AttributeKindEnum.Number
                  , attributeDataFormat: AttributeDataFormatEnum.Length
                  , clientControlType: ClientControlTypeEnum.Edit
                  , level: ProtectionLevelEnum.Medium)]
        ApproachExternalDeburr = 40,

        [Description("Lavoro sbavatura esterna [mm]")]
        [EnumSerializationName("WorkExternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WorkExternalDeburr)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium)]
        WorkExternalDeburr = 41,

        [Description("Accostamento sbavatura interna [mm]")]
        [EnumSerializationName("ApproachInternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ApproachInternalDeburr)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , level: ProtectionLevelEnum.Medium
            , clientControlType: ClientControlTypeEnum.Edit)]
        ApproachInternalDeburr = 42,

        [Description("Lavoro sbavatura interna [mm]")]
        [EnumSerializationName("WorkInternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WorkInternalDeburr)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        WorkInternalDeburr = 43,

        [Obsolete]
        [Description("Velocità di rotazione [rpm]")]
        [EnumSerializationName("RevolutionSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RevolutionSpeed)]
        [AttributeInfo(
          attributeType: AttributeTypeEnum.Process
          , attributeKind: AttributeKindEnum.Number
          , attributeDataFormat: AttributeDataFormatEnum.RotationalSpeed
          , clientControlType: ClientControlTypeEnum.Override
          , attributeScope: AttributeScopeEnum.Fundamental
          , level: ProtectionLevelEnum.Normal)]
        SpindleRevolutionSpeed = 44,

        [Obsolete]
        [Description("Velocità di alimentazione [mm/min]")]
        [EnumSerializationName("FeedRateSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FeedRateSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                        , attributeScope: AttributeScopeEnum.Fundamental
                        , clientControlType: ClientControlTypeEnum.Override
                        , level: ProtectionLevelEnum.Normal)]
        FeedRateSpeed = 45,

        [Description("Sforzo sulla punta [%]")]
        [EnumSerializationName("TorqueFeedRate")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TorqueFeedRate)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Percentage
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        TorqueFeedRate = 46,

        [Description("Coefficiente vita utensile [n]")]
        [EnumSerializationName("ToolLifeCoefficient")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolLifeCoefficient)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ToolLifeCoefficient = 47,

        [Description("Tipo di Lubrificazione [n]")]
        [EnumSerializationName("LubrificationType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LubrificationType)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , clientControlType: ClientControlTypeEnum.Combo
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Normal
                    , typeName: typeof(LubrificationTypeEnum))]
        LubrificationType = 48,

        [Description("Frequenza di Lubrificazione [n]")]
        [EnumSerializationName("LubrificationFrequency")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LubrificationFrequency)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , clientControlType: ClientControlTypeEnum.Combo
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , attributeScope: AttributeScopeEnum.Fundamental
            , level: ProtectionLevelEnum.Normal
            , typeName: typeof(LubrificationFrequencyEnum))]
        LubrificationFrequency = 49,

        [Description("Scarico Trucciolo [n]")]
        [EnumSerializationName("PeckFeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PeckFeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Normal
            , attributeKind: AttributeKindEnum.Bool)]
        PeckFeed = 50,

        [Description("Posizione di interruzione [mm]")]
        [EnumSerializationName("FirstChipBreakagePosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FirstChipBreakagePosition)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal)]
        FirstChipBreakagePosition = 51,

        [Description("Passo rottura truciolo [mm]")]
        [EnumSerializationName("ChipPitchPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ChipPitchPosition)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal)]
        ChipPitchPosition = 52,

        [Description("Quota di passaggio in vel. nom. di lavoro [mm]")]
        [EnumSerializationName("ApproachPositionNominalSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ApproachPositionNominalSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal)]
        ApproachPositionNominalSpeed = 53,

        [Description("Pos. velocità fine foro [mm]")]
        [EnumSerializationName("EndPositionFinalSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.EndPositionFinalSpeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal)]
        EndPositionFinalSpeed = 54,


        [Description("Velocità di contornitura [mm/min]")]
        [EnumSerializationName("ContouringSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ContouringSpeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
            , level: ProtectionLevelEnum.Medium)]
        ContouringSpeed = 56,

        [Description("Profondità di passata [mm]")]
        [EnumSerializationName("CuttingDepth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingDepth)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        CuttingDepth = 57,

        [Description("Diametro min foro [mm]")]
        [EnumSerializationName("MinHoleDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinHoleDiameter)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium
                    )]
        MinHoleDiameter = 58,

        [Description("Diametro max foro [mm]")]
        [EnumSerializationName("MaxHoleDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxHoleDiameter)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal
                    )]
        MaxHoleDiameter = 59,

        [Description("Ricoprimento radiale [mm]")]
        [EnumSerializationName("RadialOverlap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RadialOverlap)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        RadialOverlap = 60,

        [Description("Velocità di avanzamento radiale [mm/min]")]
        [EnumSerializationName("RadialFeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RadialFeed)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.PeripheralSpeed
                , clientControlType: ClientControlTypeEnum.Override
                , level: ProtectionLevelEnum.Normal)]
        RadialFeed = 61,

        [Description("Profondità di passata radiale [mm]")]
        [EnumSerializationName("RadialDepth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RadialDepth)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal)]
        RadialDepth = 62,

        [Obsolete]
        [Description("Velocità rotazione svasatore [rpm]")]
        [EnumSerializationName("CountersinkRevolutionSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkRevolutionSpeed)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.RotationalSpeed
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        CountersinkRevolutionSpeed = 63,

        [Obsolete]
        [Description("Velocità di alimentazione svasatore [mm/min]")]
        [EnumSerializationName("CountersinkFeedRateSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkFeedRateSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        CountersinkFeedrateSpeed = 64,

        [Description("Diametro torcia [mm]")]
        [EnumSerializationName("TorchDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TorchDiameter)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        TorchDiameter = 65,

        [Description("Lunghezza torcia [mm]")]
        [EnumSerializationName("TorchLength")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TorchLength)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        TorchLength = 66,

        [Description("Velocità di taglio tangenziale [mm/min]")]
        [EnumSerializationName("TangentialSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TangentialSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Edit
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        TangentialCuttingSpeed = 67,

        [Description("Diametro Kerf [mm]")]
        [EnumSerializationName("Kerf")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Kerf)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Override
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Normal)]
        KerfDiameter = 68,

        [Description("Distanza di Taglio Nominale [mm]")]
        [EnumSerializationName("CutHt")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CutHt)]
        [AttributeInfo( 
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        NominalCuttingDistance = 69,

        [Description("Tempo di ritardo [sec]")]
        [EnumSerializationName("PierceDelay")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PierceDelay)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        DelayTime = 70,

        [Description("Calcolo della posizione di accensione % TD")]
        [EnumSerializationName("PierceHt")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PierceHt)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Edit, attributeDataFormat: AttributeDataFormatEnum.Percentage
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        StartingPositionCalculation = 71,

        [Description("Tempo di esecuzione creep [sec]")]
        [EnumSerializationName("CreepTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CreepTime)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        CreepExecutionTime = 72,

        [Description("Percentuale velocità taglio creep")]
        [EnumSerializationName("CreepSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CreepSpeed)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Edit, attributeDataFormat: AttributeDataFormatEnum.Percentage
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        CreepCutSpeedPercentage = 73,

        [Description("Range angolo Bevel minimo [grad]")]
        [EnumSerializationName("MinBevelAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinBevelAngle)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Angle
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MinBevelAngle = 74,

        [Description("Range angolo Bevel massimo [grad]")]
        [EnumSerializationName("MaxBevelAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxBevelAngle)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Angle
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MaxBevelAngle = 75,

        [Description("Tipologie di tagli Bevel")]
        [EnumSerializationName("BevelType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelType)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , clientControlType: ClientControlTypeEnum.Combo
                    , level: ProtectionLevelEnum.Medium
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeKind: AttributeKindEnum.Enum
                    , typeName: typeof(BevelTypeEnum))]
        BevelType = 76,

        [Description("Correttore Angolo Bevel [grad]")]
        [EnumSerializationName("AngAdj")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AngAdj)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Angle
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        AngleAdjustment = 77,

        [Description("Corrente nominale [amp]")]
        [EnumSerializationName("AM")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AM)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.ElectricCurrent
                    , valueType: ValueTypeEnum.PlasmaAM
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        PlasmaCurrent = 78,

        [Description("Codice cappuccio protezione")]
        [EnumSerializationName("ShieldRetCap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldRetCap)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        ShieldRetCap = 79,

        [Description("Codice schermo")]
        [EnumSerializationName("Shield")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Shield)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Shield = 80,

        [Description("Codice cappuccio tenuta")]
        [EnumSerializationName("NozzleRetCap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleRetCap)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        NozzleRetCap = 81,

        [Description("Codice anello diffusore")]
        [EnumSerializationName("SwirlRing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SwirlRing)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        SwirlRing = 82,

        [Description("Codice elettrodo")]
        [EnumSerializationName("Electrode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Electrode)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Electrode = 83,

        [Description("Identificatore dell'ugello")]
        [EnumSerializationName("Nozzle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Nozzle)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.Medium)]
        Nozzle = 84,

        [Description("Codice tubo dell'acqua")]
        [EnumSerializationName("WaterTube")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Watertube)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        WaterTube = 85,

        [Description("Tempo di vita ugello [sec]")]
        [EnumSerializationName("NozzleLifeTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeTime)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Label
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , attributeKind: AttributeKindEnum.Number
                    , level: ProtectionLevelEnum.ReadOnly)]
        NozzleLifeTime = 86,

        [Description("Vita ugello nr. accensioni [n]")]
        [EnumSerializationName("NozzleLifeIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitions)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        NozzleLifeIgnitions = 87,

        [Description("N° accensioni OK [n]")]
        [EnumSerializationName("NozzleLifeIgnitionsSuccess")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitionsSuccess)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        NozzleLifeIgnitionsSuccess = 88,

        [Description("N° accensioni non OK [n]")]
        [EnumSerializationName("NozzleLifeIgnitionsFailed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitionsFailed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        NozzleLifeIgnitionsFailed = 89,

        [Description("N° massimo accensioni [n]")]
        [EnumSerializationName("NozzleLifeMaxIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeMaxIgnitions)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        NozzleLifeMaxIgnitions = 90,

        [Description("Soglia di attenzione nr. accensioni [n]")]
        [EnumSerializationName("NozzleLifeWarningLimitIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeWarningLimitIgnitions)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        NozzleLifeWarningLimitIgnitions = 91,

        [Description("Tipo di gas plasma(Air, Ar, F5, Mix, N2, O2) [n]")]
        [EnumSerializationName("PlasmaGas")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaGas)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Enum
                    , clientControlType: ClientControlTypeEnum.Combo
                    , level: ProtectionLevelEnum.Medium
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , typeName: typeof(GasTypeXprEnum))]
        PlasmaGas = 92,

        [Description("Tipo di gas protezione (Air, H2O, N2, O2) [n]")]
        [EnumSerializationName("ShieldGas")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldGas)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Enum
                    , clientControlType: ClientControlTypeEnum.Combo
                    , level: ProtectionLevelEnum.Medium
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , typeName: typeof(GasTypeXprEnum))]
        ShieldGas = 93,

        [Description("Pressione di pierce gas protezione [psi]")]
        [EnumSerializationName("ShieldPiercePressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldPiercePressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        PierceGasProtectionPressure = 94,

        [Description("Pressione di taglio gas plasma [psi]")]
        [EnumSerializationName("PlasmaCutflowPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaCutflowPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        PlasmaGasCuttingPressure = 95,

        [Description("Pressione di taglio gas protezione [psi]")]
        [EnumSerializationName("ShieldCutflowPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldCutflowPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ProtectionGasCuttingPressure = 96,

        [Description("Gas 1 (N2) mix setpoint [psi]")]
        [EnumSerializationName("MixGas1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MixGas1)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MixGas1 = 97,

        [Description("Gas 2 mix setpoint [psi]")]
        [EnumSerializationName("MixGas2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MixGas2)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MixGas2 = 98,

        [Description("Gas 3 mix setpoint [psi]")]
        [EnumSerializationName("MixGas3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MixGas3)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MixGas3 = 99,

        [Description("Tensione d'Arco Nominale [volt]")]
        [EnumSerializationName("ArcVoltageNominal")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ArcVoltageNominal)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.ElectricalPotential
                    , clientControlType: ClientControlTypeEnum.Override
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Normal)]
        ArcVoltageNominal = 100,

        [Description("Tensione d'Arco Finale [volt]")]
        [EnumSerializationName("ArcVoltageFinal")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ArcVoltageFinal)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.ElectricalPotential
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ArcVoltageFinal = 101,

        [Description("Consumo elettrodo [mm]")]
        [EnumSerializationName("NozzleConsumption")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleConsumption)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        NozzleConsumption = 102,

        [Description("Tempo accessione equivalente [sec]")]
        [EnumSerializationName("EquivalentTimePierce")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.EquivalentTimePierce)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        EquivalentTimePierce = 103,

        [Description("% della Distanza di trasferimento arco rispetto a TD [%]")]
        [EnumSerializationName("TransferHt")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TransferHt)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Percentage
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ArcTransferDistanceFromTD = 104,

        [Description("Distanza Uscita Sfondamento per Discesa Torcia [mm]")]
        [EnumSerializationName("PierceExitDistance")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PierceExitDistance)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        PierceExitDistance = 105,

        [Description("Minimo tipo di console da utilizzare")]
        [EnumSerializationName("MinRequiredConsole")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinRequiredConsole)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Enum
                    , clientControlType: ClientControlTypeEnum.Combo
                    , level: ProtectionLevelEnum.Medium
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , typeName: typeof(MinRequiredConsoleTypeEnum))]
        MinRequiredConsole = 106,

        [Description("Altezza minima bottom (Bevel 'Y' e 'K') [mm]")]
        [EnumSerializationName("MinBevelLand")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MinBevelLand)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MinLand = 107,

        [Description("Altezza massima bottom (Bevel 'Y' e 'K') [mm]")]
        [EnumSerializationName("MaxBevelLand")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxBevelLand)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MaxLand = 108,

        [Description("TrueHoleDiameter [0/1]")]
        [EnumSerializationName("TrueHoleDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleDiameter)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.ReadOnly
                    , attributeKind: AttributeKindEnum.Bool)]
        TrueHoleDiameter = 109,

        [Description("ProcessID XPR [n]")]
        [EnumSerializationName("XprProcessID")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.XprProcessID)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.Medium)]
        XprProcessID = 110,

        [Description("Level A - kerf width [mm]")]
        [EnumSerializationName("TrueHoleLevelAKerf")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleLevelAKerf)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        LevelAKerfWidth = 111,

        [Description("TrueHole Circle Kerf Width [mm]")]
        [EnumSerializationName("TrueHoleCircleKerfWidth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleCircleKerfWidth)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        TrueHoleCircleKerfWidth = 112,

        [Description("2 sigma clearance [mm]")]
        [EnumSerializationName("TrueHoleTwoSigmaClearance")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleTwoSigmaClearance)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        TwoSigmaClearance = 113,

        [Description("Cut-off time [sec]")]
        [EnumSerializationName("TrueHoleCutOffTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleCutOffTime)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        CutOffTime = 114,

        [Description("Speed1 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed1_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed1_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed1_C3 = 115,

        [Description("Speed1 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed1_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed1_C2)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed1_C2 = 116,

        [Description("Speed1 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed1_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed1_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed1_C1 = 117,

        [Description("Speed1 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed1_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed1_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label, level: ProtectionLevelEnum.ReadOnly)]
        Speed1_C0 = 118,

        [Description("Speed2 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed2_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed2_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed2_C3 = 119,

        [Description("Speed2 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed2_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed2_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed2_C2 = 120,

        [Description("Speed2 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed2_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed2_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed2_C1 = 121,

        [Description("Speed2 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed2_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed2_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed2_C0 = 122,

        [Description("Speed3 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed3_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed3_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed3_C3 = 123,

        [Description("Speed3 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed3_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed3_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed3_C2 = 124,

        [Description("Speed3 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed3_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed3_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed3_C1 = 125,

        [Description("Speed3 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed3_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed3_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed3_C0 = 126,

        [Description("Speed4 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed4_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed4_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed4_C3 = 127,

        [Description("Speed4 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed4_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed4_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed4_C2 = 128,

        [Description("Speed4 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed4_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed4_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed4_C1 = 129,

        [Description("Speed4 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed4_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed4_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed4_C0 = 130,

        [Description("Speed5 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed5_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed5_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed5_C3 = 131,

        [Description("Speed5 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed5_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed5_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed5_C2 = 132,

        [Description("Speed5 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed5_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed5_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed5_C1 = 133,

        [Description("Speed5 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed5_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed5_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed5_C0 = 134,

        [Description("Speed6 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed6_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed6_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed6_C3 = 135,

        [Description("Speed6 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed6_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed6_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed6_C2 = 136,

        [Description("Speed6 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed6_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed6_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed6_C1 = 137,

        [Description("Speed6 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed6_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed6_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed6_C0 = 138,

        [Description("Speed7 C3 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed7_C3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed7_C3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed7_C3 = 139,

        [Description("Speed7 C2 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed7_C2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed7_C2)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed7_C2 = 140,

        [Description("Speed7 C1 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed7_C1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed7_C1)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed7_C1 = 141,

        [Description("Speed7 C0 [mm/min]")]
        [EnumSerializationName("TrueHoleSpeed7_C0")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleSpeed7_C0)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.ReadOnly)]
        Speed7_C0 = 142,

        [Description("Soglia di attenzione Tempo di vita ugello [sec]")]
        [EnumSerializationName("NozzleLifeWarningTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeWarningTime)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        NozzleLifeAttentionThreshold = 143,

        [Description("Tempo di vita massima uggello")]
        [EnumSerializationName("NozzleLifeMaxTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeMaxTime)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        MaxNozzleLifeTime = 144,

        [Description("Pressione O2 PreRiscaldo [psi]")]
        [EnumSerializationName("OxygenPreHeatingPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.OxygenPreHeatingPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Pressure
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        PreCutOxygenAdjustment = 145,

        [Description("Pressione O2 Taglio [psi]")]
        [EnumSerializationName("OxygenCutPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.OxygenCutPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Pressure
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        OxygenCutAdjustment = 146,

        [Description("Pressione Gas Taglio [psi]")]
        [EnumSerializationName("GasCutPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.GasCutPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Pressure
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        GasCutAdjustment = 147,

        [Description("Pressione Gas PreRiscaldo [bar]")]
        [EnumSerializationName("GasPreHeatingPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.GasPreHeatingPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Pressure
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        GasPreCutAdjustment = 148,

        [Description("Pressione O2 PreRiscaldo alto [bar]")]
        [EnumSerializationName("OxygenPreHeatingHighPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.OxygenPreHeatingHighPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Pressure
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        OxygenCutHighAdjustment = 149,

        // Probabilmente andrà eliminato perchè non esisteranno più ugelli dritti (saranno solo bevel)
        [Description("Forma dell'ugello")]
        [EnumSerializationName("NozzleShape")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleShape)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Combo
                    , typeName: typeof(NozzleShapeEnum)
                    , level: ProtectionLevelEnum.Medium)]
        NozzleShape = 150,

        [Description("Tempo di Preriscaldo [sec]")]
        [EnumSerializationName("PreHeatTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHeatTime)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , attributeDataFormat: AttributeDataFormatEnum.TimeInSeconds
                    , level: ProtectionLevelEnum.Medium)]
        PreHeatTime = 151,

        [Description("Pressione di pre-flow gas protezione HPR [psi]")]
        [EnumSerializationName("ShieldPreFlowPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldPreFlowPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ShieldPreFlowPressure = 152,

        [Description("Pressione pre-flow plasma HPR [psi]")]
        [EnumSerializationName("PlasmaPreFlowPressure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaPreFlowPressure)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process, attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PlasmaPressure
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        PlasmaPreFlowPressure = 153,


        [Description("Velocità di attacco tangenziale true hole [mm/min]")]
        [EnumSerializationName("TrueHoleLeadTangentialSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleLeadTangentialSpeed)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        TrueHoleLeadTangentialSpeed = 155,

        [Description("Velocità di taglio tangenziale true hole [mm/min]")]
        [EnumSerializationName("TrueHoleCutTangentialSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleCutTangentialSpeed)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                    , clientControlType: ClientControlTypeEnum.Edit, attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Medium)]
        TrueHoleCutTangentialSpeed = 156,

        [Description("Area minima true hole [mm**2]")]
        [EnumSerializationName("TrueHoleAreaMin")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleAreaMin)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Area
                    , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        MinTrueHoleArea = 157,

        [Description("Area massima true hole [mm**2]")]
        [EnumSerializationName("TrueHoleAreaMax")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TrueHoleAreaMax)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Area
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        MaxTrueHoleArea = 158,

        [Description("Utensile abilitato per unità A")]
        [EnumSerializationName("ToolEnableA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableA)]
        [BitEnableFor(UnitEnum.A)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.Normal
                    , attributeKind: AttributeKindEnum.Bool
                    )]
        ToolEnableA = 159,

        [Description("Utensile abilitato per unità B")]
        [EnumSerializationName("ToolEnableB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableB)]
        [BitEnableFor(UnitEnum.B)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.Normal
                    , attributeKind: AttributeKindEnum.Bool
                    )]
        ToolEnableB = 160,

        [Description("Utensile abilitato per unità C")]
        [EnumSerializationName("ToolEnableC")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableC)]
        [BitEnableFor(UnitEnum.C)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.Normal
                    , attributeKind: AttributeKindEnum.Bool
                    )]
        ToolEnableC = 161,

        [Description("Utensile abilitato per unità D")]
        [EnumSerializationName("ToolEnableD")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableD)]
        [BitEnableFor(UnitEnum.D)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.Normal
                    , attributeKind: AttributeKindEnum.Bool
                    )]
        ToolEnableD = 162,

        [Description("Corrente di marcatura [amp]")]
        [EnumSerializationName("MarkingCurrent")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MarkingCurrent)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.ElectricCurrent
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        MarkingCurrent = 163,

        [Description("Identificatore ugello ossitaglio")]
        [EnumSerializationName("NS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NS)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.String
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Medium)]
        NS = 164,

        [Description("Offset Origine X")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Ox)]
        [EnumSerializationName("Ox")]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , clientControlType: ClientControlTypeEnum.Edit
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , level: ProtectionLevelEnum.Normal)]
        XOffsetOrigin = 165,

        [Description("Offset Origine Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Oy)]
        [EnumSerializationName("Oy")]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , clientControlType: ClientControlTypeEnum.Edit
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , level: ProtectionLevelEnum.Normal)]
        YOffsetOrigin = 166,

        [Description("Tipo Origine X")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TypeOX)]
        [EnumSerializationName("TypeOX")]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(OperationOriginTypeEnum)
                , level: ProtectionLevelEnum.Medium)]
        XOriginType = 168,

        [Description("Tipo Origine Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TypeOY)]
        [EnumSerializationName("TypeOY")]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(OperationOriginTypeEnum)
                , level: ProtectionLevelEnum.Medium)]
        YOriginType = 169,

        #region <Material Item>
        [Description("Peso Specifico")]
        [EnumSerializationName("SpecificWeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SpecificWeight)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Geometric
                        , clientControlType: ClientControlTypeEnum.Edit
                        , level: ProtectionLevelEnum.Medium
                        , attributeDataFormat: AttributeDataFormatEnum.SpecificWeight
                        , attributeKind: AttributeKindEnum.Number
                        )]
        SpecificWeight = 170,

        [Description("Tipo di materiale")]
        //Definizione duplicata (da rimuovere in futuro): introdotta perché ho bisogno di un attributo non identificatore per la tabella Materiali 
        [EnumSerializationName("MaterialTypeAttribute")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaterialTypeAttribute)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Generic
                        , clientControlType: ClientControlTypeEnum.Combo
                        , level: ProtectionLevelEnum.Medium
                        , attributeDataFormat: AttributeDataFormatEnum.AsIs
                        , attributeKind: AttributeKindEnum.Enum
                        , typeName: typeof(MaterialTypeEnum)
                        )]
        MaterialTypeAttribute = 171,
        #endregion <Material Item>

        #region < Profile >

        [Description("Larghezza anima / larghezza ala destra angolare [mm]")]
        [EnumSerializationName("ProfileSA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileSA)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.Length
                        , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium
                        )]
        Sa = 202,

        [Description("Spessore anima / spessore ala destra angolare [mm]")]
        [EnumSerializationName("ProfileTA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileTA)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Ta = 203,

        [Description("Larghezza ala destra [mm]")]
        [EnumSerializationName("ProfileSBA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileSBA)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Sba = 204,

        [Description("Spessore ala destra [mm]")]
        [EnumSerializationName("ProfileTBA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileTBA)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Tba = 205,

        [Description("Larghezza ala sinistra [mm]")]
        [EnumSerializationName("ProfileSBB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileSBB)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Sbb = 206,

        [Description("Spessore ala sinistra [mm]")]
        [EnumSerializationName("ProfileTBB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileTBB)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Tbb = 207,

        [Description("Raggio di raccordo del profilo [mm]")]
        [EnumSerializationName("ProfileR")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileR)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        ProfileRadius = 208,

        [Description("Angolo di curvatura profilo angolare V [gra]")]
        [EnumSerializationName("ProfileANG")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileANG)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Angle
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Medium)]
        ProfileAngle = 209,

        [Description("Larghezza delle linguette superiori [mm]")]
        [EnumSerializationName("ProfileSAB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileSAB)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Medium)]
        Sab = 210,

        [Description("Profili U di tipo 'Bended' [0/1]")]
        [EnumSerializationName("ProfileBended")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileBended)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Medium)]
        Bended = 211,

        [Description("Disassamento ala destra (profili D) [mm]")]
        [EnumSerializationName("ProfileDSA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileDSA)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Medium)]
        Dsa = 212,

        [Description("Disassamento ala sinistra (profili D) [mm]")]
        [EnumSerializationName("ProfileDSB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileDSB)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        Dsb = 213,

        [Description("Peso lineare [Kg/m]")]
        [EnumSerializationName("LinearWeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LinearWeight)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.LinearWeight
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        LinearWeight = 214,

        [Description("Superficie lineare [m²/m]")]
        [EnumSerializationName("LinearSurface")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LinearSurface)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.LinearSurface
                , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium)]
        LinearSurface = 215,

        //[Description("Superficie per peso [m²/ton]")]
        //[EnumSerializationName("RevNum")][DatabaseDisplayName(DatabaseDisplayNameEnum.WeightSurface)]
        //[AttributeInfo(attributeType: AttributeTypeEnum.Geometric
        //        , attributeKind: AttributeKindEnum.Number
        //        , attributeDataFormat: AttributeDataFormatEnum.WeightSurface
        //        , clientControlType: ClientControlTypeEnum.Edit,level: ProtectionLevelEnum.Medium)]
        //WeightSurface = 216,

        #endregion < Profile >

        #region < Attributes For Program Code Line >
        [EnumSerializationName("Text")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Text)]
        [AppliesToProgramCodeLine(LineTypeEnum.Comment)]
        [AppliesToProgramCodeLine(LineTypeEnum.Message)]
        [Description("Testo per linea di codice programma di tipo commento/messaggio")]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , clientControlType: ClientControlTypeEnum.Label
                , level: ProtectionLevelEnum.ReadOnly
                , attributeKind: AttributeKindEnum.String)]
        LineText = 217,

        [EnumSerializationName("ToolManagementId")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolManagementId)]
        [Description("Tool Management Id per linea di codice programma di tipo single/path")]
        [AppliesToProgramCodeLine(LineTypeEnum.StartIPathFre)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Generic
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Label
                , level: ProtectionLevelEnum.ReadOnly)]
        ToolManagementId = 218,
        #endregion < Attributes For Program Code Line >

        #region < Attributes For Program >

        //[Description("Modalità di elaborazione")]
        //[EnumSerializationName("ProcessModality")]
        //[DatabaseDisplayName(DatabaseDisplayNameEnum.Prc)]
        //[AttributeInfo(
        //        attributeType: AttributeTypeEnum.Process
        //        , attributeKind: AttributeKindEnum.Number
        //        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        //        , clientControlType: ClientControlTypeEnum.Edit
        //        , level: ProtectionLevelEnum.Normal
        //        , group: AttributeDefinitionGroupEnum.Modes)]
        //ProcessingMode = 219,

        [Description("Flag Modalità di lavorazione a diametri multipli")]
        [EnumSerializationName("MultipleTool")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MultipleTool)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal)]
        MultipleTool = 220,

        [Description("Modalità di eliminazione trucioli")]
        [EnumSerializationName("ChipRemovalModality")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ChipRemovalModality)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , level: ProtectionLevelEnum.Normal
                , attributeKind: AttributeKindEnum.Enum
                , typeName: typeof(ChipRemovalModalityEnum))]
        ChipRemovalModality = 221,

        [Description("Criterio scelta utensile foratura")]
        [EnumSerializationName("DrillingToolSelection")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DrillingToolSelection)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , level: ProtectionLevelEnum.Normal
                , typeName: typeof(DrillingToolSelectionEnum)
                )]
        DrillingToolSelection = 222,

        [Description("Limite n° di fori per spazzolatura")]
        [EnumSerializationName("HolesNumberBrushing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.HolesNumberBrushing)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        HolesNumberBrushing = 223,


        [Description("Modalità di passata finale di tagli (usata per MultiStazione Gemini)")]
        [EnumSerializationName("FinalCutModality")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FinalCutModality)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal)]
        FinalCutModality = 225,

        [Description("Modalità di esecuzione dei pre fori (usata solo per Gemini/TipoG):0=nel gruppo che precede, 1=attaccato al taglio")]
        [EnumSerializationName("PreHolesOrderModality")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHolesOrderModality)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , level: ProtectionLevelEnum.Normal
                , typeName: typeof(PreHolesOrderModalityEnum))]
        PreHolesOrderModality = 226,

        [Description("Tipo di marcatura con scribing (0=standard, 1=con i caratteri tutti collegati tra loro)")]
        [EnumSerializationName("ScribingMarkingType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ScribingMarkingType)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , level: ProtectionLevelEnum.Normal
                , typeName: typeof(ScribingMarkingTypeEnum))]
        ScribingMarkingType = 227,

        [Description("Tipo di marcatura al plasma (0=standard, 1=con i caratteri tutti collegati tra loro)")]
        [EnumSerializationName("PlasmaMarkingType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaMarkingType)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , level: ProtectionLevelEnum.Normal
                , typeName: typeof(ScribingMarkingTypeEnum))]
        PlasmaMarkingType = 228,

        [Description("Flag di abilitazione della eccedenza in rimanenze magazzino")]
        [EnumSerializationName("SurplusEnableStock")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusEnableStock)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal)]
        SurplusEnableStock = 230,

        [Description("Lunghezza Minima Recuperabile di eccedenza")]
        [EnumSerializationName("SurplusMinLengthStock")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusMinLengthStock)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        SurplusMinLengthStock = 231,

        [Description("Posizione Pinza A")]
        [EnumSerializationName("PincherAPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PincherAPosition)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        PincherAPosition = 232,

        [Description("Posizione Pinza B")]
        [EnumSerializationName("PincherBPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PincherBPosition)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        PincherBPosition = 233,

        [Description("Posizione Pinza C")]
        [EnumSerializationName("PincherCPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PincherCPosition)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        PincherCPosition = 234,

        [Description("Spessore")]
        [EnumSerializationName("Thickness")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Thickness)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal)]
        Thickness = 235,

        [Description("Larghezza")]
        [EnumSerializationName("Width")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Width)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Geometric
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Normal)]
        Width = 236,

        [Description("Lunghezza")]
        [EnumSerializationName("Length")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Length)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal)]
        Length = 237,

        [Description("Tipo Eccedenza")]
        [EnumSerializationName("SurplusType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusType)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            , typeName: typeof(SurplusTypeEnum))]
        SurplusType = 238,

        [Description("Tipo di motrice")]
        [EnumSerializationName("CarriageType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CarriageType)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            , typeName: typeof(CarriageTypeEnum))]
        CarriageType = 239,

        [Description("Sfrido di intestatura barra")]
        [EnumSerializationName("LeadingEdgeScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LeadingEdgeScrap)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        LeadingEdgeScrap = 240,

        [Description("Sfrido di taglio (separazione pezzi)")]
        [EnumSerializationName("CutScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CutScrap)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        CutScrap = 241,

        [Description("Sfrido di coda (pinza)")]
        [EnumSerializationName("FinalEdgeScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FinalEdgeScrap)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        FinalEdgeScrap = 242,

        [Description("Sfrido rotazione sega")]
        [EnumSerializationName("SawingRotationScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SawingRotationScrap)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        SawingRotationScrap = 243,

        [Description("Ampiezza di scarico")]
        [EnumSerializationName("UnloadingWidth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingWidth)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        UnloadingWidth = 244,

        [Description("Eccedenza percentuale")]
        [EnumSerializationName("MaxPercentageScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxPercentageScrap)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Percentage
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        MaxPercentageScrap = 245,

        [Description("Offset X del pezzo")]
        [EnumSerializationName("OffsetX")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.XOffset)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        XOffset = 246,

        [Description("Offset Y del pezzo")]
        [EnumSerializationName("OffsetY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.YOffset)]
        [AttributeInfo(
                         attributeType: AttributeTypeEnum.Process
                         , attributeKind: AttributeKindEnum.Number
                         , attributeDataFormat: AttributeDataFormatEnum.Length
                         , clientControlType: ClientControlTypeEnum.Edit
                         , level: ProtectionLevelEnum.Normal
                        )]
        YOffset = 247,

        [Description("Offset angolo del pezzo")]
        [EnumSerializationName("OffsetAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AngleOffset)]
        [AttributeInfo(
                         attributeType: AttributeTypeEnum.Process
                         , attributeKind: AttributeKindEnum.Number
                         , attributeDataFormat: AttributeDataFormatEnum.Angle
                         , clientControlType: ClientControlTypeEnum.Edit
                         , level: ProtectionLevelEnum.Normal
                        )]
        AngleOffset = 248,

        #endregion < Attributes For Program >

        #region < Attributes For Piece Operation>

        [Description("Piano di lavoro")]
        [EnumSerializationName("Side")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Side)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Combo
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , level: ProtectionLevelEnum.Normal
                    , typeName: typeof(OperationSideEnum))]
        Side = 249,

        [Description("Diametro del foro")]
        [EnumSerializationName("Diameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Diameter)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Geometric
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Diameter
                , clientControlType: ClientControlTypeEnum.Edit
                , attributeScope: AttributeScopeEnum.Fundamental
                , level: ProtectionLevelEnum.Normal
                )]
        Diameter = 250,

        [Description("Coordinata longitudinale assoluta [mm]")]
        [EnumSerializationName("X")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.X)]
        [AttributeInfo(
                 attributeType: AttributeTypeEnum.Geometric
                 , attributeKind: AttributeKindEnum.Number
                 , attributeDataFormat: AttributeDataFormatEnum.Length
                 , clientControlType: ClientControlTypeEnum.Edit
                 , attributeScope: AttributeScopeEnum.Fundamental
                 , level: ProtectionLevelEnum.Normal)]
        X = 251,

        [Description("Coordinata trasversale assoluta [mm]")]
        [EnumSerializationName("Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Y)]
        [AttributeInfo(
                 attributeType: AttributeTypeEnum.Geometric
                 , attributeKind: AttributeKindEnum.Number
                 , attributeDataFormat: AttributeDataFormatEnum.Length
                 , clientControlType: ClientControlTypeEnum.Edit
                 , attributeScope: AttributeScopeEnum.Fundamental
                 , level: ProtectionLevelEnum.Normal)]
        Y = 252,

        [Description("Profondità [mm]")]
        [EnumSerializationName("Depth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Depth)]
        [AttributeInfo(
                 attributeType: AttributeTypeEnum.Geometric
                 , attributeKind: AttributeKindEnum.Number
                 , attributeDataFormat: AttributeDataFormatEnum.Length
                 , clientControlType: ClientControlTypeEnum.Edit
                 
                 , attributeScope: AttributeScopeEnum.Fundamental
                 , level: ProtectionLevelEnum.Normal)]
        Depth = 253,

        [Description("Tipologia di tool")]
        [EnumSerializationName("TS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TS)]
        [AttributeInfo(
                 attributeType: AttributeTypeEnum.Generic
                 , attributeKind: AttributeKindEnum.Enum
                 , attributeDataFormat: AttributeDataFormatEnum.Length
                 , clientControlType: ClientControlTypeEnum.Combo
                 , typeName: typeof(ToolTypeEnum)
                 , attributeScope: AttributeScopeEnum.Fundamental
                 , level: ProtectionLevelEnum.ReadOnly)]
        ToolType = 254,

        [Description("passo tra i fori del pattern lungo la direttrice longitudinale")]
        [EnumSerializationName("PatternPPH")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternPPH)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        LongitudinalPatternStep = 255,

        [Description("numero di ripetizioni pattern lungo la direttrice longitudinale")]
        [EnumSerializationName("PatternRPH")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternRPH)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        LongitudinalPatternRepetitions = 256,

        [Description("passo tra i fori del pattern lungo la direttrice trasversale")]
        [EnumSerializationName("PatternPPK")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternPPK)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        TransversalPatternStep = 257,

        [Description("numero di ripetizioni pattern lungo la direttrice trasversale")]
        [EnumSerializationName("PatternRPK")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternRPK)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        TransversalPatternRepetitions = 258,

        [Description("inclinazione del pattern")]
        [EnumSerializationName("PatternAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternAngle)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Angle
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal
                )]
        PatternInclinationAngle = 259,

        [Description("diametro del pre foro")]
        [EnumSerializationName("PreHoleDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHoleDiameter)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Diameter
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        PreHoleDiameter = 260,

        [Description("sbavatura in entrata foro (solo per TS74)")]
        [EnumSerializationName("ExternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ExternalDeburr)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        ExternalDeburr = 261,

        [Description("sbavatura in uscita foro (solo per TS74)")]
        [EnumSerializationName("InternalDeburr")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.InternalDeburr)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Bool
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Normal
            )]
        InternalDeburr = 262,

        [Description("passo di maschiatura (solo per TS71))")]
        [EnumSerializationName("TappingPitch")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TappingPitch)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        TappingPitch = 263,

        [Description("Immagine associata ad una entità")]
        [EnumSerializationName("Image")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Image)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Image
            , level: ProtectionLevelEnum.ReadOnly
            )]
        Image = 264,

        [Description("Codice palpatura")]
        [EnumSerializationName("ProbeCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbeM)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Enum
             , attributeDataFormat: AttributeDataFormatEnum.AsIs
             , clientControlType: ClientControlTypeEnum.Combo
             , level: ProtectionLevelEnum.Normal
             , typeName: typeof(ProbeCodeEnum)
             )]
        ProbeCode = 265,

        [Description("Assegnazione della lavorazione alla testa di foratura")]
        [EnumSerializationName("HeadCycle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.HeadCycle)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(HeadCycleEnum)
                , level: ProtectionLevelEnum.Normal)]
        HeadCycle = 266,

        [Description("Modalità di utilizzo degli assi ausiliari")]
        [EnumSerializationName("AuxiliariesCycle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AuxiliariesCycle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , typeName: typeof(AuxiliariesCycleEnum)
            , level: ProtectionLevelEnum.Normal)]
        AuxiliariesCycle = 267,

        [Description("Ciclo con dispositivi di contrasto (M90-M9). Solo per impianti Gantry")]
        [EnumSerializationName("CounteractingCycle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CounteractingCycle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Bool
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Normal
            )]
        CounteractingCycle = 268,

        [Description("Indice della sequenza (per gli ordinamenti)")]
        [EnumSerializationName("SequenceIndex")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SequenceIndex)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        SequenceIndex = 269,

        [Description("Tipo di ordinamento")]
        [EnumSerializationName("OrderType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.OrderType)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            , typeName: typeof(SequenceOrderTypeEnum)
            )]        OrderType = 270,

        #endregion < Attributes For Piece Operation >

        [Description("Tipologia di stock")]
        #region < Stock and Piece Item >				   
        [EnumSerializationName("StockItemType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.StockItemType)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                   , attributeKind: AttributeKindEnum.Enum
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Combo
                   , typeName: typeof(StockTypeEnum)
                   , level: ProtectionLevelEnum.Normal)]
        StockItemType = 271,

        [Description("Tipologia di profilo")]
        [EnumSerializationName("ProfileType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileType)] //TO DO: Eliminare gestione attributo
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Combo
                    , typeName: typeof(ProfileTypeEnum)
                    , level: ProtectionLevelEnum.Normal)]
        ProfileType = 272,

        [Description("Colata")]
        [EnumSerializationName("HeatNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.HeatNumber)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                  , attributeKind: AttributeKindEnum.String
                  , attributeDataFormat: AttributeDataFormatEnum.AsIs
                  , clientControlType: ClientControlTypeEnum.Edit
                  , level: ProtectionLevelEnum.Normal
                  )]
        HeatNumber = 273,

        [Description("Fornitore")]
        [EnumSerializationName("Supplier")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Supplier)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                  , attributeKind: AttributeKindEnum.String
                  , attributeDataFormat: AttributeDataFormatEnum.AsIs
                  , clientControlType: ClientControlTypeEnum.Edit
                  , level: ProtectionLevelEnum.Normal
                  
                  )]
        Supplier = 274,

        [EnumSerializationName("MaterialCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaterialCode)]
        [Description("Codice del materiale")]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                      , attributeKind: AttributeKindEnum.Enum
                      , attributeDataFormat: AttributeDataFormatEnum.AsIs
                      , clientControlType: ClientControlTypeEnum.ListBox
                      , level: ProtectionLevelEnum.Normal
                      , valueType: ValueTypeEnum.DynamicEnum
                      , url: "api/v1/OData/Materials")]
        MaterialCode = 275,

        [EnumSerializationName("ProfileCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProfileCode)]
        [Description("Codice del profilo")]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.ListBox
                    , level: ProtectionLevelEnum.Normal
                    , valueType: ValueTypeEnum.DynamicEnum
                    , url: "api/v1/OData/FilteredProfiles/:profileType")]
        ProfileCode = 276,

        #endregion < Stock and Piece Item >

        #region < Attributes For Piece Operation >

        [Description("Nome di entità generica (usata principalmente per le operazioni legate al pezzo)")]
        [EnumSerializationName("Name")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Name)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        Name = 277,

        [Description("Raggio")]
        [EnumSerializationName("Radius")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Radius)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        Radius = 278,

        [Description("Angolo")]
        [EnumSerializationName("Angle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Angle)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Angle
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        Angle = 279,

        [Description("Tipo di compensazione raggio utensile")]
        [EnumSerializationName("CompensationType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CompensationType)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            
            , typeName: typeof(CompensationTypeEnum))]
        CompensationType = 280,

        [Description("Indica se è un Taglio interno (false, default) o esterno (true)")]
        [EnumSerializationName("IsExternalCut")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsExternalCut)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Bool
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Check
                   )]
        IsExternalCut = 281,

        [Description("Override Velocità tangenziale")]
        [EnumSerializationName("TangentialSpeedOverride")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TangentialSpeedOverride)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]
        TangentialSpeedOverride = 282,

        [Description("Disabilitazione controllo altezza torcia plasma")]
        [EnumSerializationName("DisableTorchControlHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DisableTorchControlHeight)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Bool
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Check
                   )]
        DisableTorchControlHeight = 283,

        [Description("Attivazione rate speed torcia plasma (solo per TS51)")]
        [EnumSerializationName("EnableRateSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.EnableRateSpeed)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Bool
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Check
                   )]
        EnableRateSpeed = 284,

        [Description("Tipo di scarico")]
        [EnumSerializationName("UnloadingType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingType)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Enum
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Combo
                   , typeName: typeof(UnloadingTypeEnum))]
        UnloadingType = 285,

        [Description("Coordinata longitudinale assoluta di scarico")]
        [EnumSerializationName("UnloadingX")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingX)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]
        UnloadingX = 286,

        [Description("Coordinata trasversale assoluta di scarico")]
        [EnumSerializationName("UnloadingY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingY)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]
        UnloadingY = 287,

        [Description("Codice della zona di scarico")]
        [EnumSerializationName("UnloadingCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingCode)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]
        UnloadingCode = 288,

        [Description("Distanza trasversale tra il primo ed il secondo PATH parallelo")]
        [EnumSerializationName("DeltaY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DeltaY)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]        PathDeltaY = 289,

        [Description("Distanza trasversale tra il primo ed il terzo PATH parallelo")]
        [EnumSerializationName("Delta2Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Delta2Y)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit
                   )]        PathDelta2Y = 290,

        [Description("Distanza trasversale tra il primo ed il quarto PATH parallelo")]
        [EnumSerializationName("Delta3Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Delta3Y)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit)]
        PathDelta3Y = 291,

        [Description("Profondità del Bevel superiore")]
        [EnumSerializationName("BevelTopHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelTopHeight)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit)]
        BevelTopHeight = 292,

        [Description("Angolo di inclinazione Bevel superiore")]
        [EnumSerializationName("BevelTopAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelTopAngle)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Angle
                   , clientControlType: ClientControlTypeEnum.Edit)]
        BevelTopAngle = 293,

        [Description("Profondità del Bevel inferiore")]
        [EnumSerializationName("BevelBottomHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelBottomHeight)]
        [AttributeInfo(
               attributeType: AttributeTypeEnum.Process
               , attributeKind: AttributeKindEnum.Number
               , attributeDataFormat: AttributeDataFormatEnum.Length
               , clientControlType: ClientControlTypeEnum.Edit)]
        BevelBottomHeight = 294,

        [Description("Angolo di inclinazione Bevel inferiore")]
        [EnumSerializationName("BevelBottomAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelBottomAngle)]
        [AttributeInfo(
               attributeType: AttributeTypeEnum.Process
               , attributeKind: AttributeKindEnum.Number
               , attributeDataFormat: AttributeDataFormatEnum.Angle
               , clientControlType: ClientControlTypeEnum.Edit
               )]
        BevelBottomAngle = 295,

        [Description("Taglio Bevel con rotazione asse A bloccata")]
        [EnumSerializationName("BevelRotationBlocked")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelRotationBlocked)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Process
           , attributeKind: AttributeKindEnum.Bool
           , attributeDataFormat: AttributeDataFormatEnum.AsIs
           , clientControlType: ClientControlTypeEnum.Check
           )]
        BevelRotationBlocked = 296,

        [Description("Velocità di rotazione angolare della testa Bevel")]
        [EnumSerializationName("BevelAngularRotationSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelAngularRotationSpeed)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Process
           , attributeKind: AttributeKindEnum.Number
           , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
           , clientControlType: ClientControlTypeEnum.Edit
           )]
        BevelAngularRotationSpeed = 297,

        [Description("Tool Type per Probe Technology")]
        [EnumSerializationName("ProbeTS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbeTS)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(ToolTypeEnum)
                )]
        ProbeTS = 298,

        [Description("Diametro tool per Probe Technology")]
        [EnumSerializationName("ProbeDN")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbeDN)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        ProbeDN = 299,

        [Description("Codice per Probe Technology")]
        [EnumSerializationName("ProbeCOD")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbeCOD)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.String
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        ProbeCOD = 300,

        [Description("Tool Type per Tecnologia pre foro")]
        [EnumSerializationName("PreHoleTS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHoleTS)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(ToolTypeEnum)
                )]
        PreHoleTS = 301,

        [Description("Diametro tool per PreHole Technology")]
        [EnumSerializationName("PreHoleDN")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHoleDN)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        PreHoleDN = 302,

        [Description("Codice fornitore tool per Probe Technology")]
        [EnumSerializationName("PreHoleCOD")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHoleCOD)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.String
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        PreHoleCOD = 303,

        [Description("Tipo di interpolazione")]
        [EnumSerializationName("InterpolationType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.InterpolationType)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                , typeName: typeof(InterpolationTypeEnum)
                )]
        InterpolationType = 304,

        [Description("Riduzione corrente torcia plasma")]
        [EnumSerializationName("CurrentReduction")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CurrentReduction)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        CurrentReduction = 305,

        [Description("Spegnimento anticipato torcia plasma")]
        [EnumSerializationName("CutOffEnable")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CutOffEnable)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        CutOffEnable = 306,

        [Description("Riduzione velocità torcia plasma")]
        [EnumSerializationName("SpeedReduction")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SpeedReduction)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        SpeedReduction = 307,

        [Description("Coordinata X di uscita fresa dal basso in sicurezza")]
        [EnumSerializationName("XOut")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.XOut)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit)]
        XOut = 308,

        [Description("Coordinata Y di uscita fresa dal basso in sicurezza")]
        [EnumSerializationName("YOut")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.YOut)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit)]
        YOut = 309,

        [Description("Stringa dei caratteri di marcatura")]
        [EnumSerializationName("MarkingCodeString")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MarkingCodeString)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.String
                        , attributeDataFormat: AttributeDataFormatEnum.AsIs
                        , clientControlType: ClientControlTypeEnum.Edit
                        )]
        MarkingCodeString = 310,

        [Description("Tipo di font")]
        [EnumSerializationName("FontType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FontType)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Enum
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Combo
                
                , valueType: ValueTypeEnum.EnumFromFile)]
        FontType = 311,

        [Description("Flag per la marcatura a codice Matrix")]
        [EnumSerializationName("IsMatrixMarking")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsMatrixMarking)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        IsMatrixMarking = 312,

        [Description("Numero di ripetizioni lungo la X")]
        [EnumSerializationName("RepetitionOnX")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RepetitionOnX)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        RepetitionOnX = 313,

        [Description("Numero di ripetizioni lungo la Y")]
        [EnumSerializationName("RepetitionOnY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RepetitionOnY)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        RepetitionOnY = 314,

        [Description("Distanza fra le ripetizioni")]
        [EnumSerializationName("RepetitionDistance")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RepetitionDistance)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        RepetitionDistance = 315,

        [Description("Parametro A")]
        [EnumSerializationName("A")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.A)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit)]
        A = 316,

        [Description("Parametro B")]
        [EnumSerializationName("B")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.B)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        B = 317,

        [Description("Parametro C")]
        [EnumSerializationName("C")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.C)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        C = 318,

        [Description("Parametro D")]
        [EnumSerializationName("D")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.D)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        D = 319,

        [Description("Parametro E")]
        [EnumSerializationName("E")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.E)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        E = 320,

        [Description("Parametro F")]
        [EnumSerializationName("F")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.F)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        F = 321,

        [Description("Parametro G")]
        [EnumSerializationName("G")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.G)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        G = 322,

        [Description("Parametro H")]
        [EnumSerializationName("H")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.H)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        H = 323,

        [Description("Parametro I")]
        [EnumSerializationName("I")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.I)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        I = 324,

        [Description("Parametro J")]
        [EnumSerializationName("J")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.J)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        J = 325,

        [Description("Parametro R")]
        [EnumSerializationName("R")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.R)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        R = 326,

        [Description("Coordinata X della marcatura (con plasma) della figura")]
        [EnumSerializationName("MarkX")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MarkX)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.Length
                        , clientControlType: ClientControlTypeEnum.Edit)]
        MarkX = 327,

        [Description("Coordinata Y della marcatura (con plasma) della figura")]
        [EnumSerializationName("MarkY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MarkY)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.Length
                        , clientControlType: ClientControlTypeEnum.Edit
                        )]
        MarkY = 328,

        [Description("Inclinazione della marcatura (con plasma) della figura")]
        [EnumSerializationName("MarkAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MarkAngle)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.Angle
                        , clientControlType: ClientControlTypeEnum.Edit)]
        MarkAngle = 329,

        [Description("Nome Macro")]
        [EnumSerializationName("MacroName")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MacroName)]
        [AttributeInfo(
                        attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.String
                        , attributeDataFormat: AttributeDataFormatEnum.AsIs
                        , clientControlType: ClientControlTypeEnum.Label
                        , level: ProtectionLevelEnum.ReadOnly
                        )]
        MacroName = 330,

        [Description("Immagine del pattern")]
        [EnumSerializationName("PatternImage")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PatternImage)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Image
            , level: ProtectionLevelEnum.ReadOnly
            )]
        PatternImage = 331,

        [Description("Parametro K")]
        [EnumSerializationName("K")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.K)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        K = 332,

        [Description("Parametro L")]
        [EnumSerializationName("L")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.L)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        L = 333,

        [Description("Parametro N")]
        [EnumSerializationName("N")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.N)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        N = 334,

        [Description("Parametro O")]
        [EnumSerializationName("O")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.O)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        O = 335,

        [Description("Parametro P")]
        [EnumSerializationName("P")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.P)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        P = 336,

        [Description("Parametro Q")]
        [EnumSerializationName("Q")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Q)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        Q = 337,

        [Description("Parametro S")]
        [EnumSerializationName("S")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.S)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        S = 338,

        [Description("Parametro T")]
        [EnumSerializationName("T")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.T)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            )]
        T = 339,

        [Description("Parametro Alfa")]
        [EnumSerializationName("Alfa")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Alfa)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Angle
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        Alfa = 340,

        [Description("Parametro Beta")]
        [EnumSerializationName("Beta")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Beta)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Angle
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        Beta = 341,

        [Description("Coordinata longitudinale assoluta di riposizionamento")]
        [EnumSerializationName("CX")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CX)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        CX = 342,

        [Description("Coordinata trasversale assoluta di riposizionamento")]
        [EnumSerializationName("CY")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CY)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                )]
        CY = 343,

        [Description("quota di riposizionamento")]
        [EnumSerializationName("CR")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CR)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Length
             , clientControlType: ClientControlTypeEnum.Edit
             )]
        CR = 344,

        //[Description("Codice della funzione M")]
        //[EnumSerializationName("M")]
        //[DatabaseDisplayName(DatabaseDisplayNameEnum.M)]
        //[AttributeInfo(
        //     attributeType: AttributeTypeEnum.Process
        //     , attributeKind: AttributeKindEnum.Enum
        //     , attributeDataFormat: AttributeDataFormatEnum.AsIs
        //     , clientControlType: ClientControlTypeEnum.Combo
        //     , typeName: typeof(FunctionMTypeEnum)
        //     )]
        //M = 345,

        #endregion

        [Description("Modalità di elaborazione")]
        [EnumSerializationName("ProcessingModality")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProcessingModality)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            , typeName: typeof(ProcessingModalityEnum)
            
            )]
        ProcessingModality = 346,

        [Description("Distanza di palpatura dall'attacco taglio")]
        [EnumSerializationName("ProbingDistance")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbingDistance)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        ProbingDistance = 347,

        [EnumSerializationName("SurplusMarkingText1")]
        [Description("Stringa 1 di marcatura dell'eccedenza")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusMarkingText1)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        SurplusMarkingText1 = 348,

        [EnumSerializationName("SurplusMarkingText2")]
        [Description("Stringa 2 di marcatura dell'eccedenza")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusMarkingText2)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        SurplusMarkingText2 = 349,

        [EnumSerializationName("SurplusMarkingText3")]
        [Description("Stringa 3 di marcatura dell'eccedenza")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusMarkingText3)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        SurplusMarkingText3 = 350,

        [EnumSerializationName("SurplusMarkingText4")]
        [Description("Stringa 4 di marcatura dell'eccedenza")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SurplusMarkingText4)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.String
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        SurplusMarkingText4 = 351,

        [EnumSerializationName("PitchStart")]
        [Description("Profondità di passata")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PitchStart)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        PitchStart = 352,

        [EnumSerializationName("PitchDepth")]
        [Description("Profondità di passata")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PitchDepth)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        PitchDepth = 353,

        [EnumSerializationName("FlangesInitialAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FlangesInitialAngle)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Angle
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            
            )]
        FlangesInitialAngle = 354,

        [EnumSerializationName("FlangesFinalAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FlangesFinalAngle)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Angle
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        FlangesFinalAngle = 355,

        [EnumSerializationName("PlasmaGasCut")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaGasCut)]
        [AttributeInfo(
           attributeType: AttributeTypeEnum.Process
           , attributeKind: AttributeKindEnum.Enum
           , attributeDataFormat: AttributeDataFormatEnum.AsIs
           , clientControlType: ClientControlTypeEnum.Combo
           , level: ProtectionLevelEnum.Normal
           , typeName: typeof(GasTypeCutSelectionEnum))]
        PlasmaGasCut = 356,

        [EnumSerializationName("ShieldGasCut")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ShieldGasCut)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Enum
             , attributeDataFormat: AttributeDataFormatEnum.AsIs
             , clientControlType: ClientControlTypeEnum.Combo
             , level: ProtectionLevelEnum.Normal
             , typeName: typeof(GasTypeCutSelectionEnum))]
        ShieldGasCut = 357,

        [EnumSerializationName("GasMarking")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.GasMarking)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Enum
             , attributeDataFormat: AttributeDataFormatEnum.AsIs
             , clientControlType: ClientControlTypeEnum.Combo
             , level: ProtectionLevelEnum.Normal
             , typeName: typeof(GasTypeMarkingSelectionEnum))]
        GasMarking = 358,

        [EnumSerializationName("ProgramPlasmaCurrent")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PlasmaCurrent)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Identifier
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.ElectricCurrent
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        ProgramPlasmaCurrent = 359,

        [EnumSerializationName("BevelPathType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelPathType)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.ReadOnly
            , typeName: typeof(BevelPathTypeEnum))]
        BevelPathType = 360,

        [EnumSerializationName("BevelPathEnumeration")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BevelPathEnumeration)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.ReadOnly
            )]
        BevelPathEnumeration = 361,

        [EnumSerializationName("FunctionCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FunctionCode)]
        [Description("Funzioni miscellanee specifiche del CNC")]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            
            , typeName: typeof(Func))]
        FunctionCode = 362,

        [EnumSerializationName("FlareCountersinkAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FlareCounterSinkAngle)]
        [Description("Angolo di Svasatura")]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        FlareCounterSinkAngle = 363,

        [Description("Velocità di taglio [mt/min]")]
        [EnumSerializationName("CuttingSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingSpeed)]
        [AttributeInfo(
          attributeType: AttributeTypeEnum.Process
          , attributeDataFormat: AttributeDataFormatEnum.PeripheralSpeed
          , clientControlType: ClientControlTypeEnum.Override
          , attributeScope: AttributeScopeEnum.Fundamental
          , attributeKind: AttributeKindEnum.Number
          , level: ProtectionLevelEnum.Normal)]
        CuttingSpeed = 364,

        [Description("Velocità di avanzamento [mm/rev]")]
        [EnumSerializationName("ForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ForwardSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                        , attributeKind: AttributeKindEnum.Number
                        , attributeDataFormat: AttributeDataFormatEnum.RevolutionSpeed
                        , attributeScope: AttributeScopeEnum.Fundamental
                        , clientControlType: ClientControlTypeEnum.Override
                        , level: ProtectionLevelEnum.Normal)]
        ForwardSpeed = 365,

        [Description("Velocità di avanzamento in entrata [mm/rev]")]
        [EnumSerializationName("StartForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.StartForwardSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
           , attributeKind: AttributeKindEnum.Number
           , attributeDataFormat: AttributeDataFormatEnum.RevolutionSpeed
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal)]
        StartForwardSpeed = 366,

        [Description("Velocità di taglio svasatore [mt/min]")]
        [EnumSerializationName("CountersinkCuttingSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkCuttingSpeed)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.PeripheralSpeed
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        CountersinkCuttingSpeed = 367,

        [Description("Velocità di avanzamento svasatore [mm/rev]")]
        [EnumSerializationName("CountersinkForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CountersinkForwardSpeed)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.RevolutionSpeed
                    , clientControlType: ClientControlTypeEnum.Override
                    , level: ProtectionLevelEnum.Normal)]
        CountersinkForwardSpeed = 368,

        [Description("Fulcro")]
        [EnumSerializationName("Pivot")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Pivot)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , clientControlType: ClientControlTypeEnum.Edit
                    
                    , level: ProtectionLevelEnum.Medium)]
        Pivot = 369,

        [Description("Max. altezza di carotatura [mm]")]
        [EnumSerializationName("MaxTrepanningHeight")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxTrepanningHeight)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                  , attributeKind: AttributeKindEnum.Number
                  , attributeDataFormat: AttributeDataFormatEnum.Length
                  , clientControlType: ClientControlTypeEnum.Edit
                  
                  , level: ProtectionLevelEnum.Medium)]
        MaxTrepanningHeight = 370,

        [Description("Maschiatura sinistra")]
        [EnumSerializationName("LeftTapping")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LeftTapping)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Bool
                    , clientControlType: ClientControlTypeEnum.Check
                    , level: ProtectionLevelEnum.Medium)]
        LeftTapping = 371,

        [Description("Tipo di lavorazione")]
        [EnumSerializationName("ToolWorkType")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolWorkType)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Enum
             , attributeDataFormat: AttributeDataFormatEnum.AsIs
             , clientControlType: ClientControlTypeEnum.Combo
             , level: ProtectionLevelEnum.Normal
             , typeName: typeof(ToolWorkTypeEnum))]
        ToolWorkType = 372,


        [Description("Tecnologia di processo. Usata per Record di marcatura PLASMA")]
        [EnumSerializationName("ProcessTechnology")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProcessTechnology)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Identifier
             , attributeKind: AttributeKindEnum.Enum
             , attributeDataFormat: AttributeDataFormatEnum.AsIs
             , clientControlType: ClientControlTypeEnum.Combo
             , level: ProtectionLevelEnum.High
             , typeName: typeof(ProcessingTechnologyEnum))]
        ProcessTechnology = 373,

        [Description("Angolo iniziale Anima")]
        [EnumSerializationName("WebInitialAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WebInitialAngle)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Angle
             , clientControlType: ClientControlTypeEnum.Edit
             , level: ProtectionLevelEnum.Normal
             
            )]
        WebInitialAngle = 374,

        [Description("Angolo Finale Anima")]
        [EnumSerializationName("WebFinalAngle")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WebFinalAngle)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Process
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Angle
             , clientControlType: ClientControlTypeEnum.Edit
             , level: ProtectionLevelEnum.Normal
             
            )]
        WebFinalAngle = 375,

        [Description("Soglia preallarme vita lama")]
        [EnumSerializationName("WarningBladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WarningBladeLife)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Generic
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Percentage
             , clientControlType: ClientControlTypeEnum.Edit
             , level: ProtectionLevelEnum.Medium
             
            )]
        WarningBladeLife = 376,

        [Description("Massima vita lama")]
        [EnumSerializationName("MaxBladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxBladeLife)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Generic
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Section
             , clientControlType: ClientControlTypeEnum.Edit
             , level: ProtectionLevelEnum.Medium
             
            )]
        MaxBladeLife = 377,

        [Description("Vita lama")]
        [EnumSerializationName("BladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeLife)]
        [AttributeInfo(
             attributeType: AttributeTypeEnum.Generic
             , attributeKind: AttributeKindEnum.Number
             , attributeDataFormat: AttributeDataFormatEnum.Section
             , clientControlType: ClientControlTypeEnum.Edit
             , level: ProtectionLevelEnum.ReadOnly
             
            )]
        BladeLife = 378,

        [Description("Rodaggio lama")]
        [EnumSerializationName("BladeRunningIn")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeRunningIn)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Section
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            
           )]
        BladeRunningIn = 379,

        [Description("Diametro lama")]
        [EnumSerializationName("BladeDiameter")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeDiameter)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Diameter
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Medium
            
           )]
        BladeDiameter = 380,

        [Description("Numero denti lama")]
        [EnumSerializationName("BladeTeeth")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeTeeth)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Geometric
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            
           )]
        BladeTeeth = 381,

        [Description("Velocità di rotazione della lama")]
        [EnumSerializationName("BladeRotationSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeRotationSpeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.RotationalSpeed
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal
            
           )]
        BladeRotationSpeed = 382,

        [Description("Velocità di avanzamento [mm/min]")]
        [EnumSerializationName("LinearBladeForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LinearBladeForwardSpeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.LinearSpeed
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal
            
           )]
        LinearBladeForwardSpeed = 383,

        [Description("Velocità di avanzamento [mm/dente]")]
        [EnumSerializationName("TeethBladeForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TeethBladeForwardSpeed)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.LinearForwardSpeed
            , clientControlType: ClientControlTypeEnum.Override
            , level: ProtectionLevelEnum.Normal
            
           )]
        TeethBladeForwardSpeed = 384,

        [Description("Velocità di avanzamento [cm²/min]")]
        [EnumSerializationName("SectionBladeForwardSpeed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SectionBladeForwardSpeed)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.SectionForwardSpeed
                , clientControlType: ClientControlTypeEnum.Override
                , level: ProtectionLevelEnum.Normal
                
               )]
        SectionBladeForwardSpeed = 385,

        [Description("Posizione Macro")]
        [EnumSerializationName("MacroPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MacroPosition)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
            , level: ProtectionLevelEnum.Normal
            
            , typeName: typeof(MacroPositionEnum)
           )]
        MacroPosition = 386,

        [Description("Lunghezza rimanente del pezzo dopo tagli di separazione")]
        [EnumSerializationName("RemainingPieceLength")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RemainingPieceLength)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            
            )]
        RemainingPieceLength = 387,

        [Description("Barre Prenotate")]
        [EnumSerializationName("ReservedBars")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ReservedBars)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.Normal)]
        ReservedBars = 388,

        [Description("Barre Caricate")]
        [EnumSerializationName("LoadedBars")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LoadedBars)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Label
                    , level: ProtectionLevelEnum.Normal)]
        LoadedBars = 389,

        [Description("Angolo origine")]
        [EnumSerializationName("AngleOrigin")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AngleOrigin)] //Attributo Fittizio associato alla stazione
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        AngleOrigin = 900,

        [Description("Quantità totale")]
        [EnumSerializationName("TotalQuantity")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TotalQuantity)]
        TotalQuantity = 901,

        [Description("Quantità eseguita")]
        [EnumSerializationName("ExecutedQuantity")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ExecutedQuantity)]
        ExecutedQuantity = 902,

        [Description("Pezzo a misura")]
        [EnumSerializationName("IsPieceToMeasure")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsPieceToMeasure)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Bool
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Normal)]
        IsPieceToMeasure = 903,

        [Description("Produzione frequente")]
        [EnumSerializationName("IsFrequentlyManufactured")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsFrequentlyManufactured)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Bool
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Check
            , level: ProtectionLevelEnum.Normal)]
        IsFrequentlyManufactured = 904,

        [Description("origine X")]
        [EnumSerializationName("XOrigin")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.XOrigin)] //Attributo Fittizio associato alla stazione
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal)]
        XOrigin = 905,

        [Description("origine Y")]
        [EnumSerializationName("YOrigin")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.YOrigin)] //Attributo Fittizio associato alla stazione
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Normal)]
        YOrigin = 906,

        [Description("Program Type")]
        ProgramType = 907,

        [Description("ToolRange Type")]
        ToolRangeType = 908,

        [Description("Tool Types related to operation")]
        OperationRelatedToolTypes = 909,

        //Attributo relativo al tipo di Macro
        [Description("Macro Type")]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit, level: ProtectionLevelEnum.Medium
            , typeName: typeof(MacroTypeEnum))]
        MacroType = 910,

        OperationType = 911,

        ParentOperationType = 912,

        MacroConfigurationItems = 913,

        [Description("Lunghezza di taglio con torcia plasma")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingPlaLength)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.High)]
        CuttingPlaLength = 914,

        [Description("Lunghezza di taglio con torcia ossitaglio")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.CuttingOxyLength)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.High)]
        CuttingOxyLength = 915,

        [Description("Lunghezza di scribing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ScribingLength)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.High)]
        ScribingLength = 916,

        [Description("Lunghezza di fresatura")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MillingLength)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.High)]
        MillingLength = 917,

        [Description("Lunghezza rimanente")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Remnant)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Generic
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.Length
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.High)]
        Remnant = 918,

        [Description("Multivalore TS-DN-COD per la tecnologia")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolForTechnology)]
        [RelatedFields(ToolType, NominalDiameter, Code)]
        [AttributeInfo(
                       attributeType: AttributeTypeEnum.Generic
                       , attributeKind: AttributeKindEnum.Enum
                       , attributeDataFormat: AttributeDataFormatEnum.AsIs
                       , clientControlType: ClientControlTypeEnum.MultiValue
                       , level: ProtectionLevelEnum.Normal
                       , valueType: ValueTypeEnum.MultiValue)]
        ToolForTechnology = 919,

        [Description("Multivalore TS-DN-COD per la tecnologia di preforo")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolForPreholeTechnology)]
        [RelatedFields(PreHoleTS, PreHoleDN, PreHoleCOD)]
        [AttributeInfo(
                       attributeType: AttributeTypeEnum.Generic
                       , attributeKind: AttributeKindEnum.Enum
                       , attributeDataFormat: AttributeDataFormatEnum.AsIs
                       , clientControlType: ClientControlTypeEnum.MultiValue
                       , level: ProtectionLevelEnum.Normal
                       
                       , valueType: ValueTypeEnum.MultiValue)]
        ToolForPreholeTechnology = 920,

        [Description("Multivalore TS-DN-COD per la tecnologia di palpatura")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolForProbeTechnology)]
        [RelatedFields(ProbeTS, ProbeDN, ProbeCOD)]
        [AttributeInfo(
               attributeType: AttributeTypeEnum.Generic
               , attributeKind: AttributeKindEnum.Enum
               , attributeDataFormat: AttributeDataFormatEnum.AsIs
               , clientControlType: ClientControlTypeEnum.MultiValue
               , level: ProtectionLevelEnum.Normal
               , valueType: ValueTypeEnum.MultiValue)]
        ToolForProbeTechnology = 921,

        [Description("Multivalore corrente plasma per processo taglio Plasma")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolForPlasmaCutProcessing)]
        [RelatedFields(ProgramPlasmaCurrent)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Generic
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.ElectricCurrent
                    , clientControlType: ClientControlTypeEnum.MultiValue
                   , level: ProtectionLevelEnum.Normal
                   , valueType: ValueTypeEnum.MultiValue)]
        ToolForPlasmaCutProcessing = 922,

        [Description("Identificativo magazzino")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WarehouseId)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Identifier
            , attributeKind: AttributeKindEnum.Enum
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Combo
           , level: ProtectionLevelEnum.Normal
           , valueType: ValueTypeEnum.DynamicEnum)]
        WarehouseId = 923,

        [Description("Distanza trasversale tra il primo ed il quinto PATH parallelo")]
        [EnumSerializationName("Delta4Y")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Delta4Y)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Process
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , clientControlType: ClientControlTypeEnum.Edit)]
        PathDelta4Y = 924,

        [Description("Flag per la marcatura automatica del codice fornitore")]
        [EnumSerializationName("IsSupplierMarking")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsSupplierMarking)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        IsSupplierMarking = 925,

        [Description("Flag per la marcatura automatica del codice colata")]
        [EnumSerializationName("IsHeatNumberMarking")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.IsHeatNumberMarking)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                )]
        IsHeatNumberMarking = 926,

        [Description("Modalità tagli simulati")]
        [EnumSerializationName("SimulatedCuts")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SimulatedCuts)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        SimulatedCuts = 927,

        [Description("Profile U - posizione delle ali")]
        [EnumSerializationName("PuFlangesPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PuFlangesPosition)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Process
                    , attributeKind: AttributeKindEnum.Enum
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , clientControlType: ClientControlTypeEnum.Combo
                    
                    , level: ProtectionLevelEnum.Normal
                    , typeName: typeof(PuTypeEnum)
                    , valueType: ValueTypeEnum.StaticEnum)]
        PuFlangesPosition = 928,

        [Description("Lavorazione profilo trasversale")]
        [EnumSerializationName("TransverseSectionsProfile")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TransverseSectionsProfile)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        TransverseSectionsProfile = 929,

        [Description("Esecuzione dei tagli inclinati sulle ali con doppia passata")]
        [EnumSerializationName("DoublePassSlantedCutsFlanges")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DoublePassSlantedCutsFlanges)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        DoublePassSlantedCutsFlanges = 930,

        [Description("Esecuzione dei profili tubi con doppia passata")]
        [EnumSerializationName("DoublePassTubeProfile")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DoublePassTubeProfile)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        DoublePassTubeProfile = 931,

        [Description("Rielaborazione automatica")]
        [EnumSerializationName("AutomaticReprocessing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AutomaticReprocessing)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        AutomaticReprocessing = 932,

        [Description("Processo automatico delle quantità pezzi")]
        [EnumSerializationName("AutomaticPiecesQuantityProcessing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AutomaticPiecesQuantityProcessing)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        AutomaticPiecesQuantityProcessing = 933,


        [Description("Limite di raffreddamento punte")]
        [EnumSerializationName("DrillsCoolingLimit")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DrillsCoolingLimit)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Distance
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal
                )]
        DrillsCoolingLimit = 935,

        [Description("Lunghezza accumulata di nesting (calcolata)")]
        [EnumSerializationName("NestingLength")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NestingLength)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.Length
        , clientControlType: ClientControlTypeEnum.Label
        , level: ProtectionLevelEnum.Normal)]
        NestingLength = 936,

        [Description("Eccedenza di nesting (calcolata)")]
        [EnumSerializationName("NestingScrap")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NestingScrap)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.Length
        , clientControlType: ClientControlTypeEnum.Label
        , level: ProtectionLevelEnum.Normal)]
        NestingScrap = 937,

        [Description("Codice di destinazione")]
        [EnumSerializationName("DestinationCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DestinationCode)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        ProgramDestinationCode = 938,

        [Description("Numero di barra del lotto")]
        [EnumSerializationName("BatchNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BatchNumber)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        BatchNumber = 939,

        [Description("Numero di barre del lotto")]
        [EnumSerializationName("BatchTotalNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BatchTotalNumber)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        BatchTotalNumber = 940,

        [Description("Numero di pacco")]
        [EnumSerializationName("PackageNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PackageNumber)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal
        )]
        PackageNumber = 941,

        [Description("Codice di sabbiatura")]
        [EnumSerializationName("SandblustingCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SandblustingCode)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal
        )]
        SandblustingCode = 942,

        [Description("Numero di ripetizioni del pezzo nel nesting")]
        [EnumSerializationName("RepetitionsNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.RepetitionsNumber)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal
            )]
        RepetitionsNumber = 943,

        [Description("Rotazione longitudinale del pezzo")]
        [EnumSerializationName("LongitudinalRotation")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LongitudinalRotation)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        LongitudinalRotation = 944,

        [Description("Rotazione trasversale del pezzo")]
        [EnumSerializationName("TransverseRotation")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TransverseRotation)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Bool
                , attributeDataFormat: AttributeDataFormatEnum.AsIs
                , clientControlType: ClientControlTypeEnum.Check
                , level: ProtectionLevelEnum.Normal
                )]
        TransverseRotation = 945,

        [Description("Delta iniziale del pezzo")]
        [EnumSerializationName("InitialDelta")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.InitialDelta)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.Length
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        InitialDelta = 946,

        [Description("Delta finale del pezzo")]
        [EnumSerializationName("FinalDelta")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FinalDelta)]
        [AttributeInfo(
                attributeType: AttributeTypeEnum.Process
                , attributeKind: AttributeKindEnum.Number
                , attributeDataFormat: AttributeDataFormatEnum.Length
                , clientControlType: ClientControlTypeEnum.Edit
                , level: ProtectionLevelEnum.Normal)]
        FinalDelta = 947,

        [Description("Codice di zona scarico del pezzo")]
        [EnumSerializationName("UnloadingCodeArea")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.UnloadingCodeArea)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        UnloadingCodeArea = 948,

        [Description("Lotto")]
        [EnumSerializationName("Lot")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Lot)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.String
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        Lot = 949,

        [Description("Numero di pezzi nel lotto")]
        [EnumSerializationName("LotNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LotNumber)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal
        )]
        LotNumber = 950,

        [Description("Numero totale di pezzi nel lotto")]
        [EnumSerializationName("LotTotalNumber")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LotTotalNumber)]
        [AttributeInfo(
            attributeType: AttributeTypeEnum.Process
            , attributeKind: AttributeKindEnum.Number
            , attributeDataFormat: AttributeDataFormatEnum.AsIs
            , clientControlType: ClientControlTypeEnum.Edit
            , level: ProtectionLevelEnum.Normal)]
        LotTotalNumber = 951,

        [Description("Codice di destinazione del pezzo")]
        [EnumSerializationName("DestinationCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.DestinationCode)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal
        )]
        PieceDestinationCode = 952,

        [Description("Codice delle lavorazioni extra sul pezzo (es. verniciatura)")]
        [EnumSerializationName("ExtraProcessingCode")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ExtraProcessingCode)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.Number
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal)]
        ExtraProcessingCode = 953,

        [Description("Nome della barra generata dopo il taglio del pezzo")]
        [EnumSerializationName("GeneratedBarName")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.GeneratedBarName)]
        [AttributeInfo(
        attributeType: AttributeTypeEnum.Process
        , attributeKind: AttributeKindEnum.String
        , attributeDataFormat: AttributeDataFormatEnum.AsIs
        , clientControlType: ClientControlTypeEnum.Edit
        , level: ProtectionLevelEnum.Normal
        )]
        GeneratedBarName = 954,

        [Description("Quantità schedulabile")]
        [EnumSerializationName("SchedulableQuantity")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.SchedulableQuantity)]
        SchedulableQuantity = 955,

        [Description("Numero barre")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BarsNumber)]
        BarsNumber = 956,

        [Description("Nome della barra")]
        [EnumSerializationName("BarName")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BarName)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Identifier
                   , attributeKind: AttributeKindEnum.String
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , attributeScope: AttributeScopeEnum.Fundamental
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Normal
                   )]
        BarName = 957,

        [Description("Contratto")]
        [EnumSerializationName("Contract")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Contract)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Identifier
                   , attributeKind: AttributeKindEnum.String
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , attributeScope: AttributeScopeEnum.Fundamental
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Normal)]
        Contract = 958,

        [Description("Assembly")]
        [EnumSerializationName("Assembly")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Assembly)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Identifier
                   , attributeKind: AttributeKindEnum.String
                   , attributeDataFormat: AttributeDataFormatEnum.AsIs
                   , attributeScope: AttributeScopeEnum.Fundamental
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Normal
                   )]
        Assembly = 959,

        [Description("Part")]
        [EnumSerializationName("Part")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Part)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.String
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        Part = 960,

        [Description("Drawing")]
        [EnumSerializationName("Drawing")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Drawing)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.String
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        Drawing = 961,

        [Description("Project")]
        [EnumSerializationName("Project")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Project)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Identifier
                    , attributeKind: AttributeKindEnum.String
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        Project = 962,

        [Description("Allineamento elemento handling")]
        [EnumSerializationName("Alignment")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Alignment)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.AsIs
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        Alignment = 963,

        [Description("Posizione longitudinale handling")]
        [EnumSerializationName("LongitudinalPosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.LongitudinalPosition)]
        [AttributeInfo(
                   attributeType: AttributeTypeEnum.Geometric
                   , attributeKind: AttributeKindEnum.Number
                   , attributeDataFormat: AttributeDataFormatEnum.Length
                   , attributeScope: AttributeScopeEnum.Fundamental
                   , clientControlType: ClientControlTypeEnum.Edit
                   , level: ProtectionLevelEnum.Normal
                   )]
        LongitudinalPosition = 964,

        [Description("Posizione trasversale handling")]
        [EnumSerializationName("TransversePosition")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TransversePosition)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        TransversePosition = 965,

        [Description("Custom1 handling")]
        [EnumSerializationName("Custom1")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom1)]
        [AttributeInfo(
                    attributeType: AttributeTypeEnum.Geometric
                    , attributeKind: AttributeKindEnum.Number
                    , attributeDataFormat: AttributeDataFormatEnum.Length
                    , attributeScope: AttributeScopeEnum.Fundamental
                    , clientControlType: ClientControlTypeEnum.Edit
                    , level: ProtectionLevelEnum.Normal
                    )]
        Custom1 = 966,

        [Description("Custom2 handling")]
        [EnumSerializationName("Custom2")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom2)]
        [AttributeInfo(
                     attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal
                     )]
        Custom2 = 967,

        [Description("Custom3 handling")]
        [EnumSerializationName("Custom3")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom3)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal
                     )]
        Custom3 = 968,

        [Description("Custom4 handling")]
        [EnumSerializationName("Custom4")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom4)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal
                     )]
        Custom4 = 969,

        [Description("Custom5 handling")]
        [EnumSerializationName("Custom5")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom5)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal
                     )]
        Custom5 = 970,

        [Description("Custom6 handling")]
        [EnumSerializationName("Custom6")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Custom6)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.AsIs
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal
                     )]
        Custom6 = 971,

        [Description("Handling FlangeThickness")]
        [EnumSerializationName("FlangeThickness")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.FlangeThickness)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                     , attributeKind: AttributeKindEnum.Number
                     , attributeDataFormat: AttributeDataFormatEnum.Length
                     , attributeScope: AttributeScopeEnum.Fundamental
                     , clientControlType: ClientControlTypeEnum.Edit
                     , level: ProtectionLevelEnum.Normal)]
        FlangeThickness = 972,

        [Description("Altezza")]
        [EnumSerializationName("Height")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.Height)]
        [AttributeInfo(attributeType: AttributeTypeEnum.Geometric
                  , attributeKind: AttributeKindEnum.Number
                  , attributeDataFormat: AttributeDataFormatEnum.Length
                  , clientControlType: ClientControlTypeEnum.Edit
                  , level: ProtectionLevelEnum.Normal)]
        Height = 973,
    }

}
