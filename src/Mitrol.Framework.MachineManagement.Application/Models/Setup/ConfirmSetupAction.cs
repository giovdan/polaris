namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;

    public class SetupActionValidator : AbstractValidator<ConfirmSetupAction>
    {
        public SetupActionValidator()
        {
            RuleFor(x => x.UnitType)
                .IsInEnum()
                .WithMessage(ErrorCodesEnum.ERR_STP001.ToString());

            RuleFor(x => x.Unit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP005.ToString());

            RuleFor(x => x.Slot)
                .NotEqual((short)0)
                .When(x => x.Action != SetupActionEnum.LoadOnUnit && x.Action != SetupActionEnum.RemoveFromUnit)
                .WithErrorCode(ErrorCodesEnum.ERR_STP010.ToString());

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_STP002.ToString());
        }
    }

    /// <summary>
    /// Classe che rappresenta la conferma di un azione di setup <see cref="SetupActionEnum"/>
    /// </summary>
    public class ConfirmSetupAction: SetupActionInfo, IEntityWithImage
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("ToolLocalizationKey")]
        public string ToolLocalizationKey { get; set; }
        [JsonProperty("Action")]
        public SetupActionEnum Action { get; set; }
        [JsonProperty("CartId")]
        public int CartId { get; set; } 
        [JsonProperty("Percentage")]
        public int? Percentage { get; set; }
        [JsonProperty("ColorValue")]
        public  StatusColorEnum ColorValue { get; set; }
        [JsonProperty("ShowButton")]
        public bool ShowButton { get; set; }
        [JsonProperty("ActionLocalizationKey")]
        public string ActionLocalizationKey => $"LBL_ACTION_{Action.ToString().ToUpper()}";
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }
        [JsonProperty("IsManual")]
        public bool IsManual { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonIgnore()]
        public string MachineName { get; set; }
        public ConfirmSetupAction()
        {
            IsManual = false;
            CartId = 1;
            ShowButton = false;
        }
    }
 
}
