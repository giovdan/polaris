namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class MachineParameterGroupConfigurationComparer : IEqualityComparer<MachineParameterGroupConfiguration>
    {
        public static MachineParameterGroupConfigurationComparer Default => s_comparer.Value;

        private static readonly Lazy<MachineParameterGroupConfigurationComparer> s_comparer = new();

        public MachineParameterGroupConfigurationComparer()
        {
        }

        public bool Equals(MachineParameterGroupConfiguration first, MachineParameterGroupConfiguration second)
        {

#pragma warning disable IDE0041
            var is_first_null = ReferenceEquals(first, null);
            var is_second_null = ReferenceEquals(second, null);
#pragma warning restore IDE0041

            if (is_first_null && is_second_null)
                return true;
            else if (is_first_null || is_second_null)
                return false;
            else
            {
                return first.GroupName == second.GroupName;
            }
        }

        public int GetHashCode([DisallowNull] MachineParameterGroupConfiguration obj)
        {
            var hashCode = 792638326;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(obj.GroupName);
            return hashCode;
        }
    }

    public class MachineParameterGroupConfiguration
    {
        [JsonProperty("GroupName")]
        public string GroupName { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("ParameterCodes")]
        public List<string> ParameterCodes { get; set; }

        [JsonProperty("Priority")]
        public short Priority { get; set; }
    }

    public class MachineParameterGroupConfigurationValidator
        : AbstractValidator<MachineParameterGroupConfiguration>
    {
        public MachineParameterGroupConfigurationValidator()
        {
            RuleFor(x => x.GroupName)
                .NotEmpty()
                .WithErrorCode(ErrorCodesEnum.ERR_PAR008.ToString());

            RuleFor(x => x.Priority)
                .Must(x => x > 0)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR011.ToString());

            RuleFor(x => x.ParameterCodes.Count)
                .GreaterThan(0)
                .WithErrorCode(ErrorCodesEnum.ERR_PAR010.ToString());
        }
    }
}
