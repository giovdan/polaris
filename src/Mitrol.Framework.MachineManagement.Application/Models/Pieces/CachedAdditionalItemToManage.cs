namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AdditionalItemValidator : AbstractValidator<CachedAdditionalItemToManage>
    {
        public AdditionalItemValidator()
        {
            RuleFor(ai => ai.Index)
                .GreaterThan(-1)
                .WithErrorCode(ErrorCodesEnum.ERR_OPE006.ToString());
            RuleFor(ai => ai.Attributes.Count)
                .GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_ATT003.ToString());
            RuleFor(ai => ai.ConversionSystem)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_GEN010.ToString());
        }
    }

    public class CachedAdditionalItemToManage: CachedAdditionalItemToRemove
    {
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
        [JsonProperty("ConversionSystem")]
        public MeasurementSystemEnum ConversionSystem { get; set; }
        [JsonProperty("ToBeAdded")]
        public bool ToBeAdded { get; set; }
    }

    public class CachedAdditionalItemToRemove
    {
        [JsonProperty("Index")]
        public int Index { get; set; }
        [JsonProperty("ParentOperation")]
        public CachedPieceOperation ParentOperation { get; set; }
    }
}
