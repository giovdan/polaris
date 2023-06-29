namespace Mitrol.Framework.MachineManagement.Application.Mappings
{
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Linq;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Configuration.License;
    using Mitrol.Framework.Domain.Configuration.Models;

    public class ServiceProfile: AutoMapper.Profile
    {
        public ServiceProfile()
        {
            CreateMap<Entity, EntityItem>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<EntityItem, Entity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<Entity, EntityListItem>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<AttributeValue, AttributeItem>()
                .ForPath(dest => dest.Value.CurrentValue, opt => opt.MapFrom(s => s.Value))
                .ForPath(dest => dest.Value.CurrentValueId, opt => opt.MapFrom(s => s.Value))
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.EnumId));

            CreateMap<AttributeItem, AttributeValue>()
                .ForMember(dest => dest.Value, opt => opt.Ignore())
                .ForMember(dest => dest.TextValue, opt => opt.Ignore());

            CreateMap<AttributeDefinition, AttributeItem>()
                .ForMember(dest => dest.AttributeDefinitionId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.EnumId));

            CreateMap<IGrouping<Entity, AttributeValue>, EntityItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Key.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(s => s.Key.DisplayName))
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.Key.EntityTypeId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(s => s.AsEnumerable()));

            CreateMap<Entity, ToolListItem>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(s => s.DisplayName))
                .ForMember(dest => dest.CodeGenerators, opt => opt.Ignore())
                .ForMember(dest => dest.InnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.SecondaryKey))
                .ForMember(dest => dest.ToolType, opt => opt.MapFrom(s => s.EntityTypeId.ToToolType()))
                .ForMember(dest => dest.PlantUnit, opt => opt.MapFrom(s => s.EntityTypeId.GetPlantUnit()))
                .ForMember(dest => dest.InnerId, opt => opt.MapFrom(s => s.SecondaryKey));

            CreateMap<Entity,ToolDetailItem>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(s => s.DisplayName))
                .ForMember(dest => dest.InnerId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.SecondaryKey))
                .ForMember(dest => dest.ToolType, opt => opt.MapFrom(s => s.EntityTypeId.ToToolType()))
                .ForMember(dest => dest.ImageCode, opt => opt.MapFrom(s => s.EntityTypeId.DisplayName()))
                .ForMember(dest => dest.Identifiers, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.Ignore())
                .ForMember(dest => dest.PlantUnit, opt => opt.MapFrom(s => s.EntityTypeId.GetPlantUnit()))
                .ForMember(dest => dest.UnitTypeLocalizationKey, opt => opt.Ignore())
                .ForMember(dest => dest.StatusLocalizationKey, opt => opt.Ignore());

            CreateMap<DetailIdentifierMaster, IdentifierDetailItem>()
                .ForMember(dest => dest.LocalizationKey, opt => opt.MapFrom(s => $"{MachineManagementExtensions.LABEL_IDENTIFIER}_{s.DisplayName.ToUpper()}"))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Priority))
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.DataFormat));

            CreateMap<DetailIdentifierMaster, CodeGeneratorItem>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Priority))
                .ForMember(dest => dest.UMLocalizationKey, opt => opt.MapFrom(s => $"{DomainExtensions.GENERIC_LABEL}_{s.DataFormat.ToString().ToUpper()}"))
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.DataFormat))
                .ForMember(dest => dest.InnerValue, opt => opt.MapFrom(s => s.Value));

            CreateMap<DetailIdentifierMaster, AttributeDetailItem>()
                .ForMember(dest => dest.Value, opt => opt.Ignore())
                .ForMember(dest => dest.LocalizationKey,
                            opt => opt.MapFrom(s => $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{s.DisplayName.ToUpper()}"))
                .ForMember(dest => dest.UMLocalizationKey, opt => opt.MapFrom(s => $"{DomainExtensions.GENERIC_LABEL}_{s.DataFormat.ToString().ToUpper()}"));

            CreateMap<AttributeDetailItem, ToolStatusAttribute>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(s => 
                                                        s.AttributeKind == AttributeKindEnum.Enum
                                                        ? s.Value.CurrentValueId
                                                        : s.AttributeKind != AttributeKindEnum.String 
                                                            ? decimal.Parse(s.Value.CurrentValue.ToString())
                                                            : 0))
                .ForMember(dest => dest.TextValue, opt => opt.MapFrom(s =>
                                                    s.AttributeKind == AttributeKindEnum.String
                                                    ? s.Value.CurrentValue.ToString()
                                                    : string.Empty));

            CreateMap<AttributeValue, AttributeDetailItem>()
                .ForMember(dest => dest.Value, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeKind
                    , opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.AttributeKind))
                .ForMember(dest => dest.DisplayName
                    , opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.DisplayName))
                .ForMember(dest => dest.EnumId
                    , opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.EnumId))
                .ForMember(dest => dest.ControlType
                    , opt => opt.MapFrom(s => s.AttributeDefinitionLink.ControlType))
                .ForMember(dest => dest.AttributeType, opt => opt.MapFrom(s => s.AttributeDefinitionLink
                                        .AttributeDefinition.AttributeType))
                .ForMember(dest => dest.IsCodeGenerator, opt => opt.MapFrom(s => s.AttributeDefinitionLink.IsCodeGenerator))
                .ForMember(dest => dest.IsStatusAttribute, opt => opt.MapFrom(s => s.AttributeDefinitionLink.IsStatusAttribute))
                .ForMember(dest => dest.AttributeScopeId, opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeScopeId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(s => s.AttributeDefinitionLink.GroupId))
                .ForMember(dest => dest.HelpImage, opt => opt.MapFrom(s => s.AttributeDefinitionLink.HelpImage))
                .ForMember(dest => dest.IsReadonly, opt => opt.Ignore())
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.DataFormat))
                .ForMember(dest => dest.LocalizationKey,
                            opt => opt.MapFrom(s => $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{s.AttributeDefinitionLink.AttributeDefinition.DisplayName.ToString().ToUpper()}"))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.AttributeDefinitionLink.Priority))
                .ForMember(dest => dest.UMLocalizationKey, opt => opt.MapFrom(s => $"{DomainExtensions.GENERIC_LABEL}_{s.AttributeDefinitionLink.AttributeDefinition.DataFormat.ToString().ToUpper()}"))
                .ForMember(dest => dest.ProtectionLevel, opt => opt.MapFrom(s => s.AttributeDefinitionLink.ProtectionLevel));

            CreateMap<AttributeDefinitionLink, AttributeDetailItem>()
                .ForMember(dest => dest.AttributeDefinitionLinkId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.AttributeKind, opt => opt.MapFrom(s => s.AttributeDefinition.AttributeKind))
                .ForMember(dest => dest.AttributeStatus, opt => opt.Ignore())
                .ForMember(dest => dest.AttributeType, opt => opt.MapFrom(s => s.AttributeDefinition.AttributeType))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(s => s.AttributeDefinition.DisplayName))
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.AttributeDefinition.EnumId))
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.AttributeDefinition.DataFormat))
                .ForMember(dest => dest.LocalizationKey,
                            opt => opt.MapFrom(s => $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{s.AttributeDefinition.DisplayName.ToString().ToUpper()}"))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Priority))
                .ForMember(dest => dest.UMLocalizationKey, 
                            opt => opt.MapFrom(s => $"{DomainExtensions.GENERIC_LABEL}_{s.AttributeDefinition.DataFormat.ToString().ToUpper()}"));

            #region < Configuration >

            CreateMap<RootConfiguration, RootConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<SetupConfiguration, SetupConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<PlantConfiguration, PlantConfiguration>()
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<LicenseConfiguration, LicenseConfiguration>()
               .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ReaJetConfiguration, ReaJetConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<MachineConfiguration, MachineConfiguration>()
                .ForMember(dest => dest.Consoles, opt => opt.MapFrom((s, dest) => dest.Consoles.Merge(s.Consoles)))
                .ForMember(dest => dest.ToolTypes, opt => opt.MapFrom((s, dest) => dest.ToolTypes.Merge(s.ToolTypes)))
                .ForMember(dest => dest.SkippedToolTypes, opt => opt.MapFrom((s, dest) => dest.SkippedToolTypes.Merge(s.SkippedToolTypes)))
                .ForMember(dest => dest.Profiles, opt => opt.MapFrom((s, dest) => dest.Profiles.Merge(s.Profiles)))
                .ForMember(dest => dest.Operations, opt => opt.MapFrom((s, dest) => dest.Operations.Merge(s.Operations)))
                .ForMember(dest => dest.ProbeCodes, opt => opt.MapFrom((s, dest) => dest.ProbeCodes.Merge(s.ProbeCodes)))
                .ForMember(dest => dest.ProgramTypes, opt => opt.MapFrom((s, dest) => dest.ProgramTypes.Merge(s.ProgramTypes)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<GeneralConfiguration, GeneralConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<MacroTypeConfiguration, MacroTypeConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<MitrolConfiguration, MitrolConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<FanucConfiguration, FanucConfiguration>()
                .ForMember(dest => dest.GFunctions, opt => opt.MapFrom((s, dest) => dest.GFunctions.Merge(s.GFunctions)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<PlcConfiguration, PlcConfiguration>()
               .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<CncConfiguration, CncConfiguration>()
                .ForMember(dest => dest.AxisGroups, opt => opt.MapFrom((s, dest) => dest.AxisGroups.Merge(s.AxisGroups, AxisGroupConfigurationComparer.Default)))
                .ForMember(dest => dest.Axes, opt => opt.MapFrom((s, dest) => dest.Axes.Merge(s.Axes)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<EtherCATConfiguration, EtherCATConfiguration>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom((s, dest) => dest.Nodes.Merge(s.Nodes)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<AxisGroupConfiguration, AxisGroupConfiguration>()
                .ForMember(dest => dest.AxisNames, opt => opt.MapFrom((s, dest) => dest.AxisNames.Merge(s.AxisNames)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<AxisConfiguration, AxisConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<OverrideConfiguration, OverrideConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ImportExportConfiguration, ImportExportConfiguration>()
                .ForMember(dest => dest.Folders, opt => opt.MapFrom((s, dest) => dest.Folders.Merge(s.Folders)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<MarkConfiguration, MarkConfiguration>()
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<FontDefinitionConfiguration, FontDefinitionConfiguration>()
                .ForMember(dest => dest.Fonts, opt => opt.MapFrom((s, dest) => dest.Fonts.Merge(s.Fonts)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<FontPathDefinitionConfiguration, FontPathDefinitionConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<DrillConfiguration, DrillConfiguration>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom((s, dest) => dest.Units.Merge(s.Units)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<DrillUnitConfiguration, DrillUnitConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<GridPosition, GridPosition>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<PlaConfiguration, PlaConfiguration>()
                .ForMember(dest => dest.Torches, opt => opt.MapFrom((s, dest) => dest.Torches.Merge(s.Torches)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<SawConfiguration, SawConfiguration>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom((s, dest) => dest.Units.Merge(s.Units)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<TorchUnitConfiguration, TorchUnitConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<NodeReference, NodeReference>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<OxyConfiguration, OxyConfiguration>()
                .ForMember(dest => dest.Torches, opt => opt.MapFrom((s, dest) => dest.Torches.Merge(s.Torches)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ProgrammingConfiguration, ProgrammingConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<BacklogsConfiguration, BacklogsConfiguration>()
                .ForMember(dest => dest.Timers, opt => opt.MapFrom((s, dest) => dest.Timers.Merge(s.Timers)))
                .ForAllOtherMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<BacklogTimerConfiguration, BacklogTimerConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<CleanUpConfiguration, CleanUpConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<RobotConfiguration, RobotConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ProductionConfiguration, ProductionConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ToleranceConfiguration, ToleranceConfiguration>()
                .ForAllMembers(opt => opt.IgnoreIfSourceIsNull());

            CreateMap<ApplicationSetting, ApplicationSettingItem>()
                .ForMember(dest => dest.SettingKey, opt => opt.Ignore())
                .ForMember(dest => dest.Value, opt => opt.MapFrom(s => s.DefaultValue));

            CreateMap<BaseInfoItem<int, string>, ListBoxItem>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(s => s.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Id));

            #endregion < Configuration >
        }
    }
}
