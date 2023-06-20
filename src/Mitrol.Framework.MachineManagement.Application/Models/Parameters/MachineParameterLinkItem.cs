namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Cnc;
    using Newtonsoft.Json;

    public class MachineParameterLinkValidator: AbstractValidator<MachineParameterLinkToImport>
    {
        public MachineParameterLinkValidator()
        {
            RuleFor(x => x.Type).IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR003.ToString());
            RuleFor(x => x.Variable)
                .NotEmpty()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR006.ToString());
            RuleFor(x => x.Variable)
                .Matches(Constants.ParameterCodePattern)
                .When(x => x.Type == CncTypeEnum.Fanuc)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR005.ToString());
        }
    }

    public class MachineParameterLinkItem: MachineParameterLinkToImport
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("ParentId")]
        public long ParentId { get; set; }
    }


    public class MachineParameterLinkToImport
    {
        [JsonProperty("Type")]
        public CncTypeEnum Type { get; set; }
        [JsonProperty("Variable")]
        public string Variable { get; set; }
    }
}
