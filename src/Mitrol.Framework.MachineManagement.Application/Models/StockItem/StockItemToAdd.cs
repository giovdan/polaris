namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class StockItemToAddValidator : AbstractValidator<StockItemToAdd>
    {
        public StockItemToAddValidator()
        {
            RuleFor(x => x.Attributes.Count).GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_ATT003.ToString());
            RuleFor(x => (ProfileTypeEnum)x.ProfileTypeId).IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_GEN015.ToString());
        }
    }

    public class StockItemToAdd: StockHashCodeGenerator
    {
        [JsonProperty("ProfileTypeId")]
        public long ProfileTypeId { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }

        public override Dictionary<DatabaseDisplayNameEnum, object> GetAttributes() { return Attributes; }
    }
}
