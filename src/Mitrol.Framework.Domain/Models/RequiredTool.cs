namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public interface IRequiredTool
    {
        int ToolIndex { get; set; }
        PlantUnitEnum PlantUnit { get; set; }
    }

    public class RequiredTool: Observable, IRequiredTool
    {
        [JsonProperty("ToolIndex")]
        public int ToolIndex
        {
            get => _toolIndex;
            set => SetProperty(equal: () => _toolIndex == value
            , action: () =>_toolIndex = value);
        }
        private int _toolIndex;

        [JsonProperty("PlantUnit")]
        public PlantUnitEnum PlantUnit { 
            get => _plantUnit;
            set => SetProperty(equal: () => _plantUnit == value,
                action: () => _plantUnit = value);
        }
        private PlantUnitEnum _plantUnit;

        public RequiredTool(int toolIndex, PlantUnitEnum plantUnit)
        {
            PlantUnit = plantUnit;
            ToolIndex = toolIndex;
        }

    }
}
