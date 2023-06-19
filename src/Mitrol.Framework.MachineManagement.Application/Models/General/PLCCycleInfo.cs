namespace Mitrol.Framework.MachineManagement.Application.Handlers.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Application.Enums;


    public class PLCCycleInfo
    {
        private PlantUnitEnum plantUnit;

        public PLCCycleEnum PLC_Cycle { get; internal set; }

        public PLCCycleInfo(PLCCycleEnum cycle)
        {
            PLC_Cycle = cycle;
        }

        public PLCCycleInfo(PlantUnitEnum plantUnit, UnitEnum unit, PLCCycleEnum cycle, short slot)
            :this(cycle)
        {
            Unit = unit;
            UnitType = plantUnit;
            IsManualAction = false;
            ToolIndex = 0;
        }

        public PLCCycleInfo(PlantUnitEnum plantUnit, UnitEnum unit, PLCCycleEnum cycle, short slot
            , bool isManualAction) :
            this(plantUnit, unit, cycle, slot)
        {
            this.plantUnit = plantUnit;
            Unit = unit;
            Slot = slot;
            IsManualAction = isManualAction;
        }

        public PLCCycleInfo(PlantUnitEnum plantUnit
                        , UnitEnum unit
                        , short slot
                        , PLCCycleEnum cycle
                        , int toolIndex
                        , bool isManualAction) : this(plantUnit, unit, cycle, slot, isManualAction)
        {
            ToolIndex = toolIndex;
        }

        public PlantUnitEnum UnitType { get; set; }
        public UnitEnum Unit { get; set; }
        /// <summary>
        /// Tool Index (= 0 => Scarico, > 0 => Carico)
        /// </summary>
        public int ToolIndex { get; set; }
        public short Slot { get; set; }
        public bool IsManualAction { get; set; }

        public override string ToString()
        {
            return $"{PLC_Cycle}_{UnitType.ToString().ToUpper()}_{Unit}";
        }
    }
}
