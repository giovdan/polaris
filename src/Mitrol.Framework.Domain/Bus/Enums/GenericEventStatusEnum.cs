namespace Mitrol.Framework.Domain.Bus
{
    public enum GenericEventStatusEnum
    {
        NotActive = -1,
        Started = 0,
        InProgress = 1,
        Running = 1,
        Aborted = 2,
        Completed = 3,
        Failed = 4,
        Waiting = 5,
        Aborting = 6,
    }
}
