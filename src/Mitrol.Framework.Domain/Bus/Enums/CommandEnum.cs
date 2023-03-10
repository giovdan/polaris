namespace Mitrol.Framework.Domain.Bus.Enums
{
    public enum CommandEnum
    {
        NoCommand = 0,
        AlarmsUpdated = 1, // done
        MachineStateChanged = 2,
        SetupChanged = 4,
        ToastNotification = 5,
        OriginsCalculated = 6,
        ProductionListChanged = 7,
        CheckTools = 8,
        ProgramCodeInfoChanged = 9,
        NestingChanged = 11,
        ToolDataChanged = 12,
        UnitDataChanged = 13,
        OperatorMessagesUpdated = 14,
        ProgressEvent = 15,
        MaintenancesUpdated = 16,
        ProgramListChanged = 19,
        PieceListChanged = 20,
        StockListChanged = 21
    }


    public enum SubscribableEventEnum
    {
        ToolDataChanged = CommandEnum.ToolDataChanged,
        UnitDataChanged = CommandEnum.UnitDataChanged,
        ProgressEvent = CommandEnum.ProgressEvent,

        OriginsChanged = CommandEnum.OriginsCalculated,
    }
}
