namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class MachineParameterToExportValidator: AbstractValidator<MachineParameterToExport>
    {
        public MachineParameterToExportValidator()
        {
            RuleFor(x => x.Category)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR001.ToString());

            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR002.ToString());
        }
    }

    public class MachineParameterToExport
    {
        [JsonProperty("Category")]
        public ParameterCategoryEnum Category { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Value")]
        public decimal Value { get; set; }
    }
}
