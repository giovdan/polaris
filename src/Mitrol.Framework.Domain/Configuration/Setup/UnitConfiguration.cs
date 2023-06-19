namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public interface IUnitConfiguration
    {
        GridPosition GridPosition { get; }
        UnitEnum Id { get; }
        bool IsPresent { get; }
        UnitEnum? Unit { get; }
    }

    public abstract class UnitConfiguration : IUnitConfiguration
    {
        internal const string s_gridPositionJsonName = "GridPosition";
        internal const string s_unitJsonName = "Unit";

        public UnitConfiguration()
        { }

        [JsonConstructor]
        public UnitConfiguration([JsonProperty(s_unitJsonName)] UnitEnum? unitId,
                                 [JsonProperty(s_gridPositionJsonName)] GridPosition gridPosition)
        {
            Unit = unitId;
            GridPosition = gridPosition;
        }

        [JsonProperty(s_gridPositionJsonName)]
        public GridPosition GridPosition { get; protected set; }

        [JsonIgnore]
        public UnitEnum Id => Unit ?? UnitEnum.None;

        [JsonIgnore]
        public abstract bool IsPresent { get; }

        [JsonProperty(s_unitJsonName)]
        public UnitEnum? Unit { get; protected set; }
    }
}