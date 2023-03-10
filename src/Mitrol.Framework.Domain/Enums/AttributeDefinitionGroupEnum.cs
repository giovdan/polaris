namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;

    public enum AttributeDefinitionGroupEnum
    {
        None = -1,
        Offset = 1,
        //[OperationTypeOrder((int)OperationTypeEnum.Node, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Slot, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 8)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 4)]
        Origin = 2,

        //[ProgramTypeOrder(priority: 1)]
        Identifiers = 3,

        //[OperationTypeOrder((int)OperationTypeEnum.Node, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Slot, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathS, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathVertex, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Stiff, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Notch, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Misc, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.ToolTechnology, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.Shf, 1)]
        //[OperationTypeOrder((int)OperationTypeEnum.MSaw, 1)]
        Generic = 4,
        Others = 5,

        //[ProgramTypeOrder(priority: 2)]
        Geometric = 6,

        Dimensions = 7,
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Misc, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Notch, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.MSaw,2)]
        Parameters = 9,
        TorchConfiguration = 10,
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Slot, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathS, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 3)]
        Technology = 12,
        //[OperationTypeOrder((int)OperationTypeEnum.Node, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 3)]
        Sequence = 13,
        //[OperationTypeOrder((int)OperationTypeEnum.Node, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Slot, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathS, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 10)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 8)]
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 5)]
        Probe = 14,
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 3)]
        Pattern = 15,
        //[OperationTypeOrder((int)OperationTypeEnum.Point, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Node, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Hol, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.BHol, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Tap, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Bore, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Flare, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.FHol, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.DHol, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, 4)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathS, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 9)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.Deburr, 6)]
        Cycle = 16,
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 2)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 3)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathVertex, 2)]
        Bevel = 17,
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 4)]
        Unload = 18,
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 5)]
        Delta = 19,
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 5)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 3)]
        PreHoleTechnology = 20,
        //[OperationTypeOrder((int)OperationTypeEnum.PathC, 7)]
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 6)]
        //[OperationTypeOrder((int)OperationTypeEnum.Mill, 4)]
        ProbeTechnology = 21,
        //[Obsolete("Solo per il Mill. Utilizzo degli additionalItem")]
        ToolTechnology = 22,
        //[OperationTypeOrder((int)OperationTypeEnum.PathM, 4)]
        OutCoordinates = 23,
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, 6)]
        MacroMark = 24,
        //[ProgramTypeOrder(priority: 5)]
        Scraps = 25,
        ProcessingToolFilter = 26,
        //[ProgramTypeOrder(priority: 3)]
        Processing = 27,
        //[ProgramTypeOrder(priority: 4)]
        PlasmaProcessing = 28,
        //[OperationTypeOrder((int)OperationTypeEnum.MCut, priority: 3)]
        Repetitions = 29,
        //[OperationTypeOrder((int)OperationTypeEnum.Mark, priority: 3)]
        Font = 30,
        Traceability = 31,
        //[ProgramTypeOrder(priority: 6)]
        Informations = 32,
        Properties = 33,
    }
}
