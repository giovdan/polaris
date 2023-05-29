﻿using System;

namespace Mitrol.Framework.Domain.Enums
{
    public enum DatabaseDisplayNameEnum
    {
        PlasmaGas = 3,
        ShieldGas = 4,
        MixGas1 = 5,
        TS = 6,
        DN = 7,
        COD = 8,
        RealDn = 9,
        ToolLength = 10,
        ToolHolderCode = 11,
        GrindingAngle = 12,
        PresetDeburringTool = 13,
        MillingInsertInscribedCircle = 14,
        MillingCornerRadius = 15,
        MillingShankDiameter = 16,
        MillingShankHeight = 17,
        CuttingEdge = 18,
        CuttingEdgeOff = 19,
        MaxRampAngle = 20,
        ReverseRotationCycle = 21,
        MinTappingPitch = 22,
        MaxTappingPitch = 23,
        MillingCutterTeeth = 24,
        CountersinkAngle = 25,
        CountersinkHeight = 26,
        CountersinkDrillingDiameter = 27,
        ApproachExternalDeburr = 28,
        WorkExternalDeburr = 29,
        ApproachInternalDeburr = 30,
        WorkInternalDeburr = 31,
        [Obsolete]
        RevolutionSpeed = 32,
        [Obsolete]
        FeedRateSpeed = 33,
        TorqueFeedRate = 34,
        ToolLifeCoefficient = 35,
        LubrificationType = 36,
        LubrificationFrequency = 37,
        PeckFeed = 38,
        FirstChipBreakagePosition = 39,
        ChipPitchPosition = 40,
        ApproachPositionNominalSpeed = 41,
        EndPositionFinalSpeed = 42,
        ContouringSpeed = 44,
        CuttingDepth = 45,
        MinHoleDiameter = 46,
        MaxHoleDiameter = 47,
        RadialOverlap = 48,
        RadialFeed = 49,
        RadialDepth = 50,
        [Obsolete]
        CountersinkRevolutionSpeed = 51,
        [Obsolete]
        CountersinkFeedRateSpeed = 52,
        StartPosition = 53,
        FastApproachPosition = 54,
        EndPosition = 55,
        MaterialTypeEnable = 56,
        WarningToolLife = 57,
        MaxToolLife = 58,
        ToolLife = 59,
        CoolingLimit = 60,
        CoolingValue = 61,
        AutoSensitiveEnable = 62,
        ManualLoadingTool = 63,
        ManualMagazineAssignmentTool = 64,
        ChipExtractionEnable = 65,
        CustomToolType = 66,
        MaterialType = 67,
        MinThickness = 68,
        MaxThickness = 69,
        Nozzle = 70,
        TangentialSpeed = 71,
        Kerf = 72,
        OxygenPreHeatingPressure = 73,
        OxygenCutPressure = 74,
        GasCutPressure = 75,
        GasPreHeatingPressure = 76,
        OxygenPreHeatingHighPressure = 77,
        CutHt = 78,
        PierceHt = 79,
        PreHeatTime = 80,
        CreepTime = 81,
        CreepSpeed = 82,
        NozzleLifeWarningTime = 83,
        NozzleLifeMaxTime = 84,
        NozzleLifeTime = 85,
        TorchDiameter = 86,
        TorchLength = 87,
        RevNum = 88,
        MinBevelAngle = 90,
        MaxBevelAngle = 91,
        BevelType = 92,
        MinBevelLand = 93,
        MaxBevelLand = 94,
        AM = 95,
        ShieldRetCap = 96,
        Shield = 97,
        NozzleRetCap = 98,
        SwirlRing = 99,
        Electrode = 100,
        Watertube = 101,
        MixGas2 = 102,
        MixGas3 = 103,
        ArcVoltageFinal = 104,
        ArcVoltageNominal = 105,
        TransferHt = 106,
        NozzleConsumption = 107,
        EquivalentTimePierce = 108,
        PierceDelay = 109,
        PierceExitDistance = 110,
        AngAdj = 111,
        ShieldPiercePressure = 112,
        PlasmaCutflowPressure = 113,
        ShieldCutflowPressure = 114,
        ShieldPreFlowPressure = 115,
        PlasmaPreFlowPressure = 116,
        NozzleLifeIgnitions = 117,
        NozzleLifeIgnitionsSuccess = 118,
        NozzleLifeIgnitionsFailed = 119,
        NozzleLifeMaxIgnitions = 120,
        NozzleLifeWarningLimitIgnitions = 121,
        XprProcessFeatures = 122,
        XprProcessID = 123,
        TrueHoleDiameter = 124,
        TrueHoleCircleKerfWidth = 125,
        TrueHoleLevelAKerf = 126,
        TrueHoleTwoSigmaClearance = 127,
        TrueHoleCutOffTime = 128,
        TrueHoleSpeed1_C3 = 129,
        TrueHoleSpeed1_C2 = 130,
        TrueHoleSpeed1_C1 = 131,
        TrueHoleSpeed1_C0 = 132,
        TrueHoleSpeed2_C3 = 133,
        TrueHoleSpeed2_C2 = 134,
        TrueHoleSpeed2_C1 = 135,
        TrueHoleSpeed2_C0 = 136,
        TrueHoleSpeed3_C3 = 137,
        TrueHoleSpeed3_C2 = 138,
        TrueHoleSpeed3_C1 = 139,
        TrueHoleSpeed3_C0 = 140,
        TrueHoleSpeed4_C3 = 141,
        TrueHoleSpeed4_C2 = 142,
        TrueHoleSpeed4_C1 = 143,
        TrueHoleSpeed4_C0 = 144,
        TrueHoleSpeed5_C3 = 145,
        TrueHoleSpeed5_C2 = 146,
        TrueHoleSpeed5_C1 = 147,
        TrueHoleSpeed5_C0 = 148,
        TrueHoleSpeed6_C3 = 149,
        TrueHoleSpeed6_C2 = 150,
        TrueHoleSpeed6_C1 = 151,
        TrueHoleSpeed6_C0 = 152,
        TrueHoleSpeed7_C3 = 153,
        TrueHoleSpeed7_C2 = 154,
        TrueHoleSpeed7_C1 = 155,
        TrueHoleSpeed7_C0 = 156,
        TrueHoleLeadTangentialSpeed = 157,
        TrueHoleCutTangentialSpeed = 158,
        TrueHoleAreaMin = 159,
        TrueHoleAreaMax = 160,
        MinRequiredConsole = 161,
        NozzleShape = 162,
        ToolEnableA = 163,
        ToolEnableB = 164,
        ToolEnableC = 165,
        ToolEnableD = 166,
        MarkingCurrent = 167,
        NS = 168,
        ProfileSA = 169,
        ProfileTA = 170,
        ProfileSBA = 171,
        ProfileTBA = 172,
        ProfileSBB = 173,
        ProfileTBB = 174,
        ProfileR = 175,
        ProfileANG = 176,
        ProfileSAB = 177,
        ProfileBended = 178,
        ProfileDSA = 179,
        ProfileDSB = 180,
        LinearWeight = 181,
        LinearSurface = 182,
        WeightSurface = 183,
        ProfileType = 184,
        ToolHolderLength = 186,
        ToolHolderDiameter = 187,
        ToolInterference = 188,
        BevelThickness = 189,
        ProcessTechnology = 191,
        Text = 192,
        ToolManagementId = 193,
        Prc = 194,
        MultipleTool = 195,
        ChipRemovalModality = 196,
        HolesNumberBrushing = 198,
        NumRaf = 199,
        FinalCutModality = 200,
        PreHolesOrderModality = 201,
        ScribingMarkingType = 202,
        MkPla = 203,
        DSense = 204,
        SurplusEnableStock = 205,
        SurplusMinLengthStock = 206,
        PospA = 207,
        PincherCPosition = 208,
        PincherBPosition = 209,
        Tec = 210,
        CarriageType = 211,
        LeadingEdgeScrap = 212,
        CutScrap = 213,
        Sp = 214,
        SawingRotationScrap = 215,
        Et = 216,
        MaxPercentageScrap = 217,
        Width = 218,
        Thickness = 219,
        Length = 220,
        Side = 221,
        Diameter = 222,
        PatternPPH = 223,
        X = 224,
        Y = 225,
        Depth = 226,
        PatternRPH = 227,
        TypeOY = 228,
        R = 229,
        DY = 230,
        MatrixCode = 231,
        FontType = 232,
        MarkingCodeString = 233,
        PatternPPK = 234,
        PatternRPK = 235,
        PatternAngle = 236,
        PreHoleDiameter = 237,
        ExternalDeburr = 238,
        XOffset = 239,
        YOffset = 240,
        AngleOffset = 241,
        AngleOrigin = 242,
        Ox = 243,
        Oy = 244,
        SpecificWeight = 245,
        ProfileCode = 248,
        MaterialCode = 249,
        StockItemType = 250,
        HeatNumber = 251,
        Supplier = 252,
        Quantity = 253,
        ToolHolderName = 254,
        PlasmaTorchTableIdUnitC = 255,
        PlasmaTorchTableIdUnitD = 256,
        OxyCutTorchTableIdUnitC = 257,
        OxyCutTorchTableIdUnitD = 258,
        PlasmaTorchMarkIdUnitC = 259,
        PlasmaTorchMarkIdUnitD = 260,
        CClampPosition = 261,
        InternalDeburr = 262,
        TappingPitch = 263,
        TypeOX = 265,
        ProbeM = 266,
        HeadCycle = 267,
        AuxiliariesCycle = 268,
        CounteractingCycle = 269,
        [Obsolete]
        MaterialTypeAttribute = 270, //da rimuovere in futuro, è un doppione
        TotalQuantity = 271,
        ExecutedQuantity = 272,
        IsPieceToMeasure = 273,
        IsFrequentlyManufactured = 274,
        SequenceIndex = 275,
        OrderType = 276,
        Name = 277,
        Radius = 278,
        Angle = 279,
        CompensationType = 280,
        IsExternalCut = 281,
        PCA = 282,
        ANGA = 283,
        PCB = 284,
        ANGB = 285,
        BRA = 286,
        TangentialSpeedOverride = 287,
        DisableTorchControlHeight = 288,
        EnableRateSpeed = 289,
        UnloadingType = 290,
        UnloadingX = 291,
        UnloadingY = 292,
        UnloadingCode = 293,
        DeltaY = 294,
        Delta2Y = 295,
        Delta3Y = 296,
        BevelTopHeight = 297,
        BevelTopAngle = 298,
        BevelBottomHeight = 299,
        BevelBottomAngle = 300,
        BevelRotationBlocked = 301,
        BevelAngularRotationSpeed = 302,
        ProbeTS = 303,
        ProbeDN = 304,
        ProbeCOD = 305,
        PreHoleTS = 306,
        PreHoleCOD = 307,
        PreHoleDN = 308,
        InterpolationType = 309,
        CurrentReduction = 310,
        CutOffEnable = 311,
        SpeedReduction = 312,
        XOut = 313,
        YOut = 314,
        IsMatrixMarking = 315,
        RepetitionOnX = 316,
        RepetitionOnY = 317,
        RepetitionDistance = 318,
        Image = 319,
        A = 320,
        B = 321,
        C = 322,
        D = 323,
        E = 324,
        F = 325,
        G = 326,
        H = 327,
        I = 328,
        J = 329,
        MarkX = 330,
        MarkY = 331,
        MacroName = 332,
        MarkAngle = 333,
        PatternImage = 334,
        Z = 335,
        L = 336,
        Q = 337,
        S = 338,
        T = 339,
        Alfa = 340,
        Beta = 341,
        K = 342,
        N = 343,
        O = 344,
        P = 345,
        XOrigin = 346,
        YOrigin = 347,
        CX = 348,
        CY = 349,
        CR = 350,
        M = 351,
        PincherAPosition = 352,
        DrillingToolSelection = 353,
        ProcessingModality = 354,
        PlasmaMarkingType = 355,
        ProbingDistance = 356,
        SurplusType = 357,
        FinalEdgeScrap = 358,
        UnloadingWidth = 359,
        SurplusMarkingText1 = 360,
        SurplusMarkingText2 = 361,
        SurplusMarkingText3 = 362,
        SurplusMarkingText4 = 363,
        PitchStart = 364,
        PitchDepth = 365,
        FlangesInitialAngle = 366,
        FlangesFinalAngle = 367,
        PlasmaGasCut = 368,
        GasMarking = 369,
        ShieldGasCut = 370,
        PlasmaCurrent = 371,
        BevelPathType = 372,
        BevelPathEnumeration = 373,
        FunctionCode = 374,
        FlareCounterSinkAngle = 375,
        CuttingPlaLength = 376,
        CuttingOxyLength = 377,
        ScribingLength = 378,
        MillingLength = 379,
        Remnant = 380,
        CuttingSpeed = 381,
        ForwardSpeed = 382,
        StartForwardSpeed = 383,
        CountersinkCuttingSpeed = 384,
        CountersinkForwardSpeed = 385,
        ToolForTechnology = 386,
        ToolForPreholeTechnology = 387,
        ToolForProbeTechnology = 388,
        Pivot = 389,
        MaxTrepanningHeight = 390,
        LeftTapping = 391,
        ToolWorkType = 392,
        ToolForPlasmaCutProcessing = 393,
        Zprobed = 394,
        ToolCategory = 395,
        ArcVoltageReal = 396,
        WebInitialAngle = 397,
        WebFinalAngle = 398,
        ToolTypesToValidate = 399,
        WarehouseId = 400,
        EntityOperation = 401,
        Delta4Y = 402,
        IsSupplierMarking = 403,
        IsHeatNumberMarking = 404,
        HasBevelRows = 405,
        HasTrueHoleRows = 406,
        WarningBladeLife = 407,
        MaxBladeLife = 408,
        BladeLife = 409,
        BladeRunningIn = 410,
        BladeDiameter = 411,
        BladeTeeth = 412,
        BladeRotationSpeed = 413,
        LinearBladeForwardSpeed = 414,
        TeethBladeForwardSpeed = 415,
        SectionBladeForwardSpeed = 416,
        SubRangeType = 417,
        SimulatedCuts = 418,
        TransverseSectionsProfile = 419,
        DoublePassSlantedCutsFlanges = 420,
        DoublePassTubeProfile = 421,
        AutomaticReprocessing = 422,
        AutomaticPiecesQuantityProcessing = 423,
        DrillsCoolingLimit = 425,
        NestingLength = 426,
        NestingScrap = 427,
        DestinationCode = 428,
        BatchNumber = 429,
        BatchTotalNumber = 430,
        PackageNumber = 431,
        SandblustingCode = 432,
        RepetitionsNumber = 433,
        LongitudinalRotation = 434,
        TransverseRotation = 435,
        InitialDelta = 436,
        FinalDelta = 437,
        UnloadingCodeArea = 438,
        Lot = 439,
        LotNumber = 440,
        LotTotalNumber = 441,
        ExtraProcessingCode = 442,
        PuFlangesPosition = 443,
        GeneratedBarName = 444,
        WebAngle = 445,             // Attributi SeparationCutItem
        FlangeAngle = 446,          // Attributi SeparationCutItem
        Offset = 447,               // Attributi SeparationCutItem
        MacroPosition = 448,
        SchedulableQuantity = 449,
        RemainingPieceLength = 450,
        BarsNumber = 451,
        ReservedBars = 452,
        LoadedBars = 453,
        Contract = 454,
        Project = 455,
        Drawing = 456,
        Assembly = 457,
        Part = 458,
        OriginsCalculated = 459,
        ExecutionDate = 460
    }
}