namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRequiredToolTable: IRequiredToolChildTable
    {
        List<RequiredToolChildTable> Children { get; }
        Result AddChild(RequiredToolChildTable requiredItem);
    }

    public interface IRequiredToolChildTable
    {
        long Id { get; set; }
        ToolRangeTypeEnum Type { get; set; }
    }

    public class RequiredToolChildTable: Observable, IRequiredToolChildTable
    {
        public RequiredToolChildTable(long id, ToolRangeTypeEnum type)
        {
            Id = id;
            Type = type;
        }

        [JsonProperty("Id")]
        public long Id
        {
            get => _id;
            set => SetProperty(equal: () => _id == value, action: () => _id = value);
        }
        private long _id;

        [JsonProperty("Type")]
        public ToolRangeTypeEnum Type
        {
            get => _toolRangeType;
            set => SetProperty(equal: () => _toolRangeType == value, action: () => _toolRangeType = value);
        }
        private ToolRangeTypeEnum _toolRangeType;
    }

    public class RequiredToolTable: Observable, IRequiredToolTable
    {
        [JsonProperty("ToolIndex")]
        public int ToolIndex
        {
            get => _toolIndex;
            set => SetProperty(equal: () => _toolIndex == value, action: () => _toolIndex = value);

        }
        private int _toolIndex;

        [JsonProperty("Id")]
        public long Id { 
            get => _id ;
            set => SetProperty(equal:() => _id == value, action: () => _id = value); 
        }
        private long _id;
        
        [JsonProperty("Type")]
        public ToolRangeTypeEnum Type
        {
            get => _toolRangeType;
            set => SetProperty(equal: () => _toolRangeType == value, action: () => _toolRangeType = value);
        }
        private ToolRangeTypeEnum _toolRangeType;

        [JsonProperty("Children")]
        [DirtyableChild]
        public List<RequiredToolChildTable> Children { get; }

        public RequiredToolTable()
        {
            Children = new List<RequiredToolChildTable>();
        }

        public RequiredToolTable(long id, ToolRangeTypeEnum toolType, int toolIndex): this()
        {
            Id = id;
            Type = toolType;

            ToolIndex = toolIndex;
        }

        /// <summary>
        /// Add SubRange child to Required Table
        /// </summary>
        /// <param name="requiredItem"></param>
        /// <returns></returns>
        public Result AddChild(RequiredToolChildTable requiredItem)
        {
            if (requiredItem.Id == 0)
            {
                return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
            }


            var child = Children.SingleOrDefault(c => c.Id == requiredItem.Id && c.Type == requiredItem.Type);

            if (child != null)
            {
                return Result.Fail(ErrorCodesEnum.ERR_GEN003.ToString());
            }

            Children.Add(requiredItem);
            return Result.Ok();    
        }

    }
}
