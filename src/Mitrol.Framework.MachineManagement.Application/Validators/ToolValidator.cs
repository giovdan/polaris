namespace Mitrol.Framework.MachineManagement.Application.Validators
{
    using FluentValidation;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Class for tool validation
    /// </summary>
    public class ToolValidator : IEntityValidator<ToolDetailItem>
    {
        private Dictionary<DatabaseDisplayNameEnum, object> AdditionalInfos { get; set; }
        private IServiceFactory ServiceFactory { get; set; }
        #region < Private Methods >
        private Result ValidateGenericAttributes(IEnumerable<AttributeDetailItem> attributes)
        {
            var errorDetails = new List<ErrorDetail>();

            // Validazione attributi generici
            var startHolPositionAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.StartPosition);
            var endHolPositionAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.EndPosition);
            var fastApproachPositionAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.FastApproachPosition);

            if (startHolPositionAttribute != null
                && decimal.TryParse(startHolPositionAttribute.Value.CurrentValue.ToString()
                        , out var startHolPosition)
                && startHolPosition == 0)
            {
                errorDetails.Add(new ErrorDetail(startHolPositionAttribute.DisplayName, ErrorCodesEnum.ERR_TLM009.ToString()));
            }

            if (endHolPositionAttribute != null
                && decimal.TryParse(endHolPositionAttribute.Value.CurrentValue.ToString()
                    , out var endHolPosition)
                && endHolPosition == 0)
            {
                errorDetails.Add(new ErrorDetail(endHolPositionAttribute.DisplayName, ErrorCodesEnum.ERR_TLM010.ToString()));
            }

            if (fastApproachPositionAttribute != null
                && decimal.TryParse(fastApproachPositionAttribute.Value.CurrentValue.ToString()
                    , out var fastApproachPosition)
                && fastApproachPosition == 0)
            {
                errorDetails.Add(new ErrorDetail(fastApproachPositionAttribute.DisplayName, ErrorCodesEnum.ERR_TLM011.ToString()));
            }

            return errorDetails.Any() ? Result.Fail(errorDetails) : Result.Ok();
        }

        private Result ValidateGeometricAttributes(IEnumerable<AttributeDetailItem> attributes, ToolTypeEnum toolType)
        {
            var errorDetails = new List<ErrorDetail>();
            // Validazione attributi geometrici
            var toolLengthAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ToolLength);
            var realDiameterAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.RealDiameter);

            if (toolLengthAttribute != null
                && decimal.TryParse(toolLengthAttribute.Value.CurrentValue.ToString()
                    , out var toolLength)
                && toolLength == 0)
            {
                errorDetails.Add(new ErrorDetail(toolLengthAttribute.DisplayName, ErrorCodesEnum.ERR_TLM012.ToString()));
            }

            if (realDiameterAttribute != null
                && decimal.TryParse(realDiameterAttribute.Value.CurrentValue.ToString()
                    , out var realDiameter)
                && realDiameter == 0)
            {
                errorDetails.Add(new ErrorDetail(realDiameterAttribute.DisplayName, ErrorCodesEnum.ERR_TLM013.ToString()));
            }

            #region < Casi particolari >
            switch(toolType)
            {
                case ToolTypeEnum.TS55:
                case ToolTypeEnum.TS56:
                case ToolTypeEnum.TS57:
                    {
                        var bladeDiameterAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.BladeDiameter);
                        var theetAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.BladeTeeth);

                        if (bladeDiameterAttribute != null && decimal.TryParse(bladeDiameterAttribute.Value.CurrentValue.ToString(), 
                                                        out var bladeDiameter)
                            && bladeDiameter == 0)
                        {
                            errorDetails.Add(new ErrorDetail(bladeDiameterAttribute.DisplayName, ErrorCodesEnum.ERR_TLM013.ToString()));
                        }

                        if (theetAttribute != null && decimal.TryParse(theetAttribute.Value.CurrentValue.ToString(), 
                                        out var theet)
                            && theet == 0)
                        {
                            errorDetails.Add(new ErrorDetail(theetAttribute.DisplayName, ErrorCodesEnum.ERR_TLM028.ToString()));
                        }

                    }
                    break;

                case ToolTypeEnum.TS71:
                    {
                        #region < Validazione TAP_71 >
                        // Validazione diametro gambo fresa
                        var millingShankDiameterAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MillingShankDiameter);
                        // Validazione altezza gambo fresa
                        var millingShankHeightAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MillingShankHeight);
                        // Validazione Passo minimo maschiatura
                        var minTappingPitchAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MinTappingPitch);
                        // Validazione Passo massimo maschiatura
                        var maxTappingPitchAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaxTappingPitch);
                        // Validazione numero denti fresa per maschiatura
                        var millingCutterTeethAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MillingCutterTeeth);

                        if (millingShankDiameterAttribute != null
                            && decimal.TryParse(millingShankDiameterAttribute.Value.CurrentValue.ToString()
                            , out var millingShankDiameter)
                            && millingShankDiameter == 0)
                        {
                            errorDetails.Add(new ErrorDetail(millingShankDiameterAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM016.ToString()));
                        }

                        if (millingShankHeightAttribute != null
                            && decimal.TryParse(millingShankHeightAttribute.Value.CurrentValue.ToString()
                            , out var millingShankHeight)
                            && millingShankHeight == 0)
                        {
                            errorDetails.Add(new ErrorDetail(millingShankHeightAttribute.DisplayName
                                            , ErrorCodesEnum.ERR_TLM017.ToString()));
                        }


                        if (minTappingPitchAttribute != null
                            && decimal.TryParse(minTappingPitchAttribute.Value.CurrentValue.ToString()
                            , out var minTappingPitch)
                            && minTappingPitch == 0)
                        {
                            errorDetails.Add(new ErrorDetail(minTappingPitchAttribute.DisplayName
                                            , ErrorCodesEnum.ERR_TLM018.ToString()));
                        }


                        if (maxTappingPitchAttribute != null
                            && decimal.TryParse(maxTappingPitchAttribute.Value.CurrentValue.ToString()
                            , out var maxTappingPitch)
                            && maxTappingPitch == 0)
                        {
                            errorDetails.Add(new ErrorDetail(maxTappingPitchAttribute.DisplayName
                                            , ErrorCodesEnum.ERR_TLM019.ToString()));
                        }

                        if (millingCutterTeethAttribute != null
                                && decimal.TryParse(millingCutterTeethAttribute.Value.CurrentValue.ToString()
                                , out var millingCutterTeeth)
                                && millingCutterTeeth == 0)
                        {
                            errorDetails.Add(new ErrorDetail(millingCutterTeethAttribute.DisplayName
                                            , ErrorCodesEnum.ERR_TLM020.ToString()));
                        } 
                        #endregion
                    }
                    break;
                case ToolTypeEnum.TS33:
                case ToolTypeEnum.TS62:
                case ToolTypeEnum.TS68:
                    {
                        var grindingAngleAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.GrindingAngle);

                        if (grindingAngleAttribute != null
                            && decimal.TryParse(grindingAngleAttribute.Value.CurrentValue.ToString(), out var grindingAngle)
                            && grindingAngle == 0)
                        {
                            errorDetails.Add(new ErrorDetail(grindingAngleAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM021.ToString()));
                        }
                    }
                    break;
                case ToolTypeEnum.TS35:
                    {
                        var grindingAngleAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.GrindingAngle);
                        var minHoleDiameterAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MinHoleDiameter);
                        var maxHoleDiameterAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaxHoleDiameter);

                        if (grindingAngleAttribute != null
                            && decimal.TryParse(grindingAngleAttribute.Value.CurrentValue.ToString(), out var grindingAngle)
                            && grindingAngle == 0)
                        {
                            errorDetails.Add(new ErrorDetail(grindingAngleAttribute.DisplayName
                                            , ErrorCodesEnum.ERR_TLM021.ToString()));
                        }

                        if (minHoleDiameterAttribute != null
                            && decimal.TryParse(minHoleDiameterAttribute.Value.CurrentValue.ToString()
                                    , out var minHoleDiameter)
                            && minHoleDiameter == 0)
                        {
                            errorDetails.Add(new ErrorDetail(minHoleDiameterAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM022.ToString()));
                        }

                        if (maxHoleDiameterAttribute != null
                                && decimal.TryParse(maxHoleDiameterAttribute.Value.CurrentValue.ToString()
                                        , out var maxHoleDiameter)
                                && maxHoleDiameter == 0)
                        {
                            errorDetails.Add(new ErrorDetail(maxHoleDiameterAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM023.ToString()));
                        }


                    }
                    break;

            }
            #endregion

            return errorDetails.Any() ? Result.Fail(errorDetails) : Result.Ok();
        }
        /// <summary>
        /// Validazione attributi di processo
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private Result ValidateProcessAttributes(IEnumerable<AttributeDetailItem> attributes, ToolTypeEnum toolType)
        {
            var errorDetails = new List<ErrorDetail>();

            // Validazione attributi di processo
            var forwardSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ForwardSpeed);
            var cuttingSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.CuttingSpeed);

            if (forwardSpeedAttribute != null
                    && decimal.TryParse(forwardSpeedAttribute.Value.CurrentValue.ToString()
                    , out var forwardSpeed)
                && forwardSpeed == 0)
            {
                errorDetails.Add(new ErrorDetail(forwardSpeedAttribute.DisplayName, ErrorCodesEnum.ERR_TLM014.ToString()));
            }

            if (cuttingSpeedAttribute != null
                    && decimal.TryParse(cuttingSpeedAttribute.Value.CurrentValue.ToString()
                    , out var cuttingSpeed)
                && cuttingSpeed == 0)
            {
                errorDetails.Add(new ErrorDetail(cuttingSpeedAttribute.DisplayName, ErrorCodesEnum.ERR_TLM015.ToString()));
            }

            #region < Casi particolari >
            switch(toolType)
            {
                case ToolTypeEnum.TS55:
                case ToolTypeEnum.TS56:
                case ToolTypeEnum.TS57:
                    {
                        #region < Controllo Velocità di rotazione e velocità di avanzamento >
                        var bladeRotationSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId 
                                    == AttributeDefinitionEnum.BladeRotationSpeed);
                        var linearBladeForwardSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId 
                                    == AttributeDefinitionEnum.LinearBladeForwardSpeed);
                        var theetBladeForwardSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId
                                    == AttributeDefinitionEnum.TeethBladeForwardSpeed);
                        var sectionBladeForwardSpeedAttribute = attributes.SingleOrDefault(a => a.EnumId
                                    == AttributeDefinitionEnum.SectionBladeForwardSpeed);

                        if (bladeRotationSpeedAttribute != null && 
                            decimal.TryParse(bladeRotationSpeedAttribute.Value.CurrentValue.ToString()
                                , out var bladeRotationSpeed)
                            && bladeRotationSpeed == 0)
                        {
                            errorDetails.Add(new ErrorDetail(bladeRotationSpeedAttribute.DisplayName
                                , ErrorCodesEnum.ERR_TLM028.ToString()));
                        }


                        if (linearBladeForwardSpeedAttribute != null &&
                            decimal.TryParse(linearBladeForwardSpeedAttribute.Value.CurrentValue.ToString()
                                , out var linearBladeForwardSpeed)
                            && linearBladeForwardSpeed == 0)
                        {
                            errorDetails.Add(new ErrorDetail(linearBladeForwardSpeedAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM029.ToString()));
                        }


                        //if (theetBladeForwardSpeedAttribute != null &&
                        //    decimal.TryParse(theetBladeForwardSpeedAttribute.Value.CurrentValue.ToString()
                        //        , out var theetBladeForwardSpeed)
                        //    && theetBladeForwardSpeed == 0)
                        //{
                        //    errorDetails.Add(new ErrorDetail(theetBladeForwardSpeedAttribute.DisplayName, ErrorCodesEnum.ERR_TLM028.ToString()));
                        //}

                        //if (sectionBladeForwardSpeedAttribute != null &&
                        //    decimal.TryParse(sectionBladeForwardSpeedAttribute.Value.CurrentValue.ToString()
                        //        , out var sectionBladeForwardSpeed)
                        //    && sectionBladeForwardSpeed == 0)
                        //{
                        //    errorDetails.Add(new ErrorDetail(sectionBladeForwardSpeedAttribute.DisplayName, ErrorCodesEnum.ERR_TLM028.ToString()));
                        //}
                        #endregion < Controllo Velocità di rotazione e velocità di avanzamento >
                    }
                    break;
                case ToolTypeEnum.TS62:
                case ToolTypeEnum.TS68:
                    {
                        var contouringSpeedAttribute = attributes.SingleOrDefault(a => 
                                    a.EnumId == AttributeDefinitionEnum.ContouringSpeed);

                        // Controllo velocità di contornitura
                        if (contouringSpeedAttribute != null
                                && decimal.TryParse(contouringSpeedAttribute.Value.CurrentValue.ToString()
                                , out var contourningSpeed)
                                && contourningSpeed == 0)
                        {
                            errorDetails.Add(new ErrorDetail(contouringSpeedAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM024.ToString()));
                        }

                        // Controllo profondità di passata
                        if (toolType == ToolTypeEnum.TS62)
                        {
                            var cuttingDepthAttribute = attributes
                                        .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.CuttingDepth);

                            if (cuttingDepthAttribute != null
                                && decimal.TryParse(cuttingDepthAttribute.Value.CurrentValue.ToString()
                                , out var cuttingDepth)
                                && cuttingDepth == 0)
                            {
                                errorDetails.Add(new ErrorDetail(cuttingDepthAttribute.DisplayName
                                                , ErrorCodesEnum.ERR_TLM025.ToString()));
                            }
                        }

                    }
                    break;
            }
            #endregion
            return errorDetails.Any() ? Result.Fail(errorDetails) : Result.Ok();
        }

        
      
        #endregion < Private Methods >

        private class ToolDetailItemValidator : AbstractValidator<ToolDetailItem>
        {
            public ToolDetailItemValidator()
            {
                RuleFor(x => x.ToolType)
                    .IsInEnum()
                    .WithErrorCode(ErrorCodesEnum.ERR_TLM001.ToString());
            }
        }

        public ToolValidator()
        {
            AdditionalInfos = new Dictionary<DatabaseDisplayNameEnum, object>();
        }

        public void Init(Dictionary<DatabaseDisplayNameEnum, object> additionalInfos)
        {
            AdditionalInfos = additionalInfos;
            // Aggiunta dei tooltypes da validare
            AdditionalInfos.Add(DatabaseDisplayNameEnum.ToolTypesToValidate, new List<ToolTypeEnum>
            { ToolTypeEnum.TS33, ToolTypeEnum.TS34, ToolTypeEnum.TS35, ToolTypeEnum.TS38, ToolTypeEnum.TS39,
                ToolTypeEnum.TS41 , ToolTypeEnum.TS55, ToolTypeEnum.TS56, ToolTypeEnum.TS57
                , ToolTypeEnum.TS62, ToolTypeEnum.TS68, ToolTypeEnum.TS71});
        }

        public void Init(IServiceFactory serviceFactory
                , Dictionary<DatabaseDisplayNameEnum, object> additionalInfos)
        {
            ServiceFactory = serviceFactory;
            AdditionalInfos = additionalInfos;
        }

        public Result Validate(ToolDetailItem tool)
        {
            var result = Result.Ok();

            // Validazione tool management Id
            if (tool.Id == 0)
                return Result.Fail(new ErrorDetail(nameof(tool.Id), ErrorCodesEnum.ERR_TLM002.ToString()));

            // Validazione tool type
            if (!Enum.IsDefined(tool.ToolType))
                return Result.Fail(new ErrorDetail(nameof(tool.ToolType), ErrorCodesEnum.ERR_TLM001.ToString()));

            if (!Enum.IsDefined(tool.ConversionSystem))
                return Result.Fail(new ErrorDetail(nameof(tool.ConversionSystem), ErrorCodesEnum.ERR_GEN010.ToString()));

            // Validazione identificatori
            if (tool.Identifiers.Any())
            {
                result = Result.Combine(result, ValidateIdentifiers(tool.Identifiers));
            }

            // Se il tooltype del tool in input non è presente nella lista dei tipi di tool da validare allora
            // la validazione deve essere bypassata
            if (AdditionalInfos.TryGetValue(DatabaseDisplayNameEnum.ToolTypesToValidate, out var additionalInfo)
                && additionalInfo is List<ToolTypeEnum> toolTypesToValidate
                && !toolTypesToValidate.Contains(tool.ToolType))
            { 
                return result;  
            }

            // Fluent Validation
            var validationResults = new ToolDetailItemValidator().Validate(tool);
            if (!validationResults.IsValid)
            {
                result = Result.Fail(validationResults.Errors
                                    .GetErrorDetails());
            }

 
            // Validazione attributi
            if (tool.Attributes.Any())
            {
                result = Result.Combine(result, ValidateAttributes(tool.Attributes));
            }

            return result;
        }

        /// <summary>
        /// Validazione identificatori
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="toolType"></param>
        /// <returns></returns>
        public Result ValidateIdentifiers(IEnumerable<AttributeDetailItem> identifiers)
        {
            if (identifiers.Any())
            {
                var errorDetails = identifiers.Select(identifier =>
                {
                    var identifierValue = identifier.GetAttributeValue();
                    ErrorDetail errorDetail = null;

                    // Gitea #512 (Disabilitare momentaneamente validazione magazzino)
                    if (identifier.AttributeKind == AttributeKindEnum.Enum && !string.IsNullOrEmpty(identifier.TypeName))
                    {
                        var enumType = Type.GetType(identifier.TypeName);
                        var converter = TypeDescriptor.GetConverter(enumType);
                        var value = converter.ConvertFrom(identifierValue);
                        // Se non è stato convertito correttamente il valore non è valido
                        if (enumType != value.GetType())
                        {
                            errorDetail = new ErrorDetail(identifier.DisplayName, ErrorCodesEnum.ERR_TLM026.ToString());
                        }
                    }
                    else if (identifier.AttributeKind != AttributeKindEnum.String
                            && Convert.ToDecimal(identifierValue) <= 0
                            && identifier.EnumId != AttributeDefinitionEnum.WarehouseId)
                    {
                        errorDetail = new ErrorDetail(identifier.DisplayName, ErrorCodesEnum.ERR_TLM026.ToString());
                    }
                    else if ((identifier.AttributeKind == AttributeKindEnum.String && identifier.EnumId != AttributeDefinitionEnum.Code)
                        && (identifierValue?.ToString().IsNullOrWhiteSpace() ?? true))
                    {
                        errorDetail = new ErrorDetail(identifier.DisplayName, ErrorCodesEnum.ERR_TLM026.ToString());
                    }

                    return errorDetail;
                })
                .Where(errorDetail => errorDetail != null);

                return errorDetails.Any() ? Result.Fail(errorDetails) : Result.Ok();
            }

            return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
        }


        /// <summary>
        /// Validazione attributi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result ValidateAttributes(IEnumerable<AttributeDetailItem> attributes)
        {
            if (AdditionalInfos.TryGetValue(DatabaseDisplayNameEnum.TS, out var toolTypeInfo)
                && Enum.TryParse<ToolTypeEnum>(toolTypeInfo.ToString(), out var toolType))
            {

                return Result.AggregateIfFails(ValidateGenericAttributes(attributes)
                                    , ValidateGeometricAttributes(attributes, toolType)
                                    , ValidateProcessAttributes(attributes, toolType));
            }

            return Result.Fail(ErrorCodesEnum.ERR_GEN001.ToString());
        }

        public Result ValidateIdentifiers(ToolDetailItem model)
        {
            throw new NotImplementedException();
        }
    }
}
