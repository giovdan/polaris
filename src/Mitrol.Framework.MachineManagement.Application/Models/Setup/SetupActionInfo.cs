namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ManualSetupActionInfoValidator: AbstractValidator<ManualSetupActionInfo>
    {
        public ManualSetupActionInfoValidator()
        {
            RuleFor(x => x.UnitType)
                .IsInEnum()
                .WithMessage(ErrorCodesEnum.ERR_STP001.ToString());
            RuleFor(x => x.Slot)
                .GreaterThan((short)0)
                .WithMessage(ErrorCodesEnum.ERR_STP003.ToString());

            RuleFor(x => x.Unit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP005.ToString());

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_STP002.ToString());

        }
    }

    public class SetupActionInfoValidator : AbstractValidator<SetupActionInfo>
    {
        public SetupActionInfoValidator()
        {
            RuleFor(x => x.UnitType)
                .IsInEnum()
                .WithMessage(ErrorCodesEnum.ERR_STP001.ToString());
            RuleFor(x => x.Slot)
                .GreaterThan((short)0)
                .WithMessage(ErrorCodesEnum.ERR_STP003.ToString());

            RuleFor(x => x.Unit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP005.ToString());

        }

    }

    public class SetupActionInfo: SpindleActionInfo
    {
        [JsonProperty("UnitType")]
        public PlantUnitEnum UnitType { get; set; }

        public override string ToString()
        {
            return $"{UnitType.ToString().ToUpper()}_{Unit.ToString()}";
        }
    }

    public class ManualSetupActionInfo: SetupActionInfo
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
    }


}
