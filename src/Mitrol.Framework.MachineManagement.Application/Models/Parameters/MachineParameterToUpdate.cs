namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class MachineParameterToUpdateValidator : AbstractValidator<MachineParameterToUpdate>
    {
        public MachineParameterToUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR012.ToString());

            RuleFor(x => x.CurrentConversionSystem)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_GEN010.ToString());

        }
    }

    public class MachineParameterToUpdate
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Value")]
        public decimal Value { get; set; }
        [JsonIgnore()]
        public MeasurementSystemEnum CurrentConversionSystem { get; set; }

        public MachineParameterToUpdate()
        {
            CurrentConversionSystem = MeasurementSystemEnum.MetricSystem;
        }
    }

    

}