namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class SpindleActionInfoValidator : AbstractValidator<SpindleActionInfo>
    {
        public SpindleActionInfoValidator()
        {
            RuleFor(x => x.Slot)
                .LessThan((short)0)
                .WithMessage(ErrorCodesEnum.ERR_STP010.ToString());

            RuleFor(x => x.Unit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP005.ToString());
        }
    }

    public class SpindleActionInfo
    {
        [JsonProperty("Slot")]
        public short Slot { get; set; }
        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }
    }
}
