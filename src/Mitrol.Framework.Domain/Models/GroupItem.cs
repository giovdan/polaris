
namespace Mitrol.Framework.Domain.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupDetaiItemValidator : AbstractValidator<GroupDetailItem>
    {
        public GroupDetaiItemValidator()
        {
            RuleFor(g => g.Code)
                .NotEmpty()
                .WithErrorCode(ErrorCodesEnum.ERR_GRP001.ToString());

            RuleFor(g => g.Code)
                .Must(code => code.Length <= 32)
                .WithErrorCode(ErrorCodesEnum.ERR_GRP001.ToString());

            RuleFor(g => g.Permissions)
                .Must(permissions => permissions.Any())
                .WithErrorCode(ErrorCodesEnum.ERR_GRP002.ToString());
        }
    }

    public class GroupItem
    {
        [JsonConstructor()]
        public GroupItem()
        {

        }

        public GroupItem(long id, string code, GroupTypeEnum type)
        {
            Id = id;
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Type = type;
        }

        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Type")]
        public GroupTypeEnum Type { get; set; }
    }

    /// <summary>
    /// Item for Update
    /// </summary>
    public class GroupToUpdateItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Permissions")]
        public IEnumerable<PermissionItem> Permissions { get; set; }
        [JsonProperty("Users")]
        public IEnumerable<BaseInfoItem<long, string>> Users { get; set; }
    }

    public class GroupDetailItem : GroupItem
    {
        [JsonConstructor()]
        public GroupDetailItem()
        {
            Permissions = Enumerable.Empty<PermissionItem>();
            Users = Enumerable.Empty<BaseInfoItem<long, string>>();
        }

        public GroupDetailItem(long id, string code, GroupTypeEnum type) : base(id, code, type)
        {
            Permissions = Enumerable.Empty<PermissionItem>();
            Users = Enumerable.Empty<BaseInfoItem<long, string>>();
        }

        [JsonProperty("Permissions")]
        public IEnumerable<PermissionItem> Permissions { get; set; }
        [JsonIgnore()]
        public IEnumerable<ProtectionLevelEnum> ProtectionLevels => this.GetProtectionLevels();
        [JsonProperty("Users")]
        public IEnumerable<BaseInfoItem<long, string>> Users { get; set; }
    }

    public static class GroupItemExtensions
    {
        public static IEnumerable<ProtectionLevelEnum> GetProtectionLevels(this GroupDetailItem group)
        {
            var protectionLevels = new HashSet<ProtectionLevelEnum>();
            var groupPermissions = group.Permissions
                .Where(p => p.Value)
                .Select(p => (PermissionEnum)p.Id);

            if (groupPermissions.Contains(PermissionEnum.MACHINEPARAMETERS_READ_CRITICAL))
            {
                protectionLevels.Add(ProtectionLevelEnum.Critical);
            }

            if (groupPermissions.Contains(PermissionEnum.MACHINEPARAMETERS_READ_HIGH))
            {
                protectionLevels.Add(ProtectionLevelEnum.High);
            }

            if (groupPermissions.Contains(PermissionEnum.MACHINEPARAMETERS_READ_MEDIUM))
            {
                protectionLevels.Add(ProtectionLevelEnum.Medium);
            }

            if (groupPermissions.Contains(PermissionEnum.MACHINEPARAMETERS_READ_NORMAL))
            {
                protectionLevels.Add(ProtectionLevelEnum.Normal);
            }

            return protectionLevels;
        }

    }
}