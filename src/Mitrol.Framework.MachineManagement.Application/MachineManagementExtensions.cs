namespace Mitrol.Framework.MachineManagement.Application
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Execution;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Application.Models.Setup;
    using Mitrol.Framework.MachineManagement.Application.Transformations;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Production;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    public static class MachineManagementExtensions
    {
        #region < Costants >

        public const string LABEL_IDENTIFIER = "LBL_IDENT";
        public const string LABEL_ATTRIBUTE = "LBL_ATTR";
        public const string LABEL_MATERIALTYPE = "LBL_MATERIALTYPE";
        public const string LABEL_PROFILETYPE = "LBL_PROFILETYPE";
        public const string LABEL_ALARM = "LBL_ALARM";
        public const string LABEL_GROUP = "LBL_GROUPS";
        public const string LABEL_PLANTUNIT = "LBL_PLANTUNIT";
        public const string LABEL_TOOLSTATUS = "LBL_TOOLSTATUS";
        public const string LABEL_TOOLHOLDERSTATUS = "LBL_TOOLHSTATUS";
        public const string LABEL_MACHINESTATUS = "LBL_MACHINESTATUS";
        public const string LABEL_PRODUCTIONSTATUS = "LBL_PRODUCTIONSTATUS";
        public const string LABEL_PRODUCTINFO_GROUP = "LBL_PRODUCTINFO_GROUP";
        public const string LABEL_PIECEIDENTIFIER_GROUP = "LABEL_PIECEIDENTIFIER_GROUP";
        public const string LBL_PROGRAMDETAIL_PIECEIDENTIFIERS = "LBL_PROGRAMDETAIL_PIECEIDENTIFIERS";
        public const string LABEL_QUANTITIES_GROUP = "LBL_QUANTITIES_GROUP";
        public const string LABEL_TABLETYPES = "LBL_TABLE_TYPES";
        public const string LABEL_SETUPUNIT = "LBL_SETUPUNIT";
        public const string LABEL_STOCKTYPE = "LBL_STOCKTYPE";
        public const string LABEL_SUPPORTACTIONS = "LBL_SUPPORTACTIONS";
        public const string LABEL_SOLUTIONS = "LBL_SOLUTIONS";
        public const string LABEL_PROGRAMTYPE = "LBL_PROGRAMTYPE";
        public const string LABEL_OPERATIONTYPE = "LBL_OPERATIONTYPE";
        public const string LABEL_MAINTENANCES_STATUS = "LBL_MAINTENANCES_STATUS";
        public const string LABEL_MAINTENANCES_STATUS_GROUP = "LBL_MAINTENANCES_STATUS_GROUP";
        public const string LABEL_MAINTENANCES_CATEGORY = "LBL_MAINTENANCES_CATEGORY";
        public const string LABEL_REPEATITIONMODE = "LBL_REPEATITIONMODE";
        public const string LABEL_REPEATITIONMODE_EVERY = "LBL_REPEATITIONMODE_EVERY";
        public const string LABEL_ACTION = "LBL_ACTIONTYPE";
        public const string LABEL_DELTAMODE = "LBL_DELTAMODE";
        public const string LABEL_TITLE = "LBL_TITLE";
        public const string LABEL_DESC = "LBL_DESC";


        public const string MAINTENANCE_PREFIX_FICEP = "FCP";
        public const string MAINTENANCE_PREFIX_POLARIS = "MNT";

        public const string MAINTENANCE_VALIDATIONKEY = "MAINTENANCEMANAGEMENT";

        #endregion < Costants >

        /// <summary>
        /// Calculate Tool HashCode
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        public static string CalculateHash<T>(this IEnumerable<T> values)
        {
            string hashCode = string.Empty;
           
            return hashCode;
        }
        // TODO
        public static void SetSetupInfo(this ToolDetailItem toolDetail
            , IExecution execution, IRootConfiguration configurationRoot, PlantUnitEnum plantUnit)
        {
            //In base alla tipologia di unità associata al tool 
            //recupera le informazioni della posizione dal Setup corrente
            //switch (plantUnit)
            //{
            //    case PlantUnitEnum.DrillingMachine:
            //        {

            //            var heads = execution.Setup?.DrillHeads?.Where(x => x.Slots.Any());
            //            if (heads == null)
            //                return;

            //            foreach (var head in heads)
            //            {
            //                //Cicla sulle teste di foratura configurate per trovare il tool
            //                var founded = head.Slots.SingleOrDefault(x => x.CurrentToolId == toolDetail.Id);
            //                //Associa l'unità e la posizione dello slot
            //                if (founded != null)
            //                {
            //                    toolDetail.Unit = head.Unit;
            //                    toolDetail.Slot = founded.SlotPosition;
            //                    toolDetail.MachineName = configurationRoot.Name;
            //                    toolDetail.PlantName = configurationRoot.Plant.Name;
            //                    break;
            //                }
            //            }
            //        }
            //        break;
            //    case PlantUnitEnum.OxyCutTorch:
            //        {
            //            var oxyTorchFounded = execution.Setup?.OxyTorches.SingleOrDefault(x => x.CurrentToolId == toolDetail.Id);
            //            if (oxyTorchFounded != null)
            //            {
            //                toolDetail.Unit = oxyTorchFounded.Unit;
            //                toolDetail.Slot = 1;
            //                toolDetail.MachineName = configurationRoot.Name;
            //                toolDetail.PlantName = configurationRoot.Plant.Name;
            //            }
            //        }
            //        break;
            //    case PlantUnitEnum.PlasmaTorch:
            //        var plasmaTorch = execution.Setup?.PlaTorches.SingleOrDefault(x => x.CurrentToolId == toolDetail.Id);
            //        if (plasmaTorch != null)
            //        {
            //            toolDetail.Unit = plasmaTorch.Unit;
            //            toolDetail.Slot = 1;
            //            toolDetail.MachineName = configurationRoot.Name;
            //            toolDetail.PlantName = configurationRoot.Plant.Name;
            //        }
            //        break;
            //}
        }

        public static void SetInfoByToolManagementId(this SetupToolListItem setupToolItem
                    , ToolDetailItem tool
                    , int toolMngId
                    , UnitToolsFilter filter
                    , SetupActionEnum action
                    , bool isAdditionalSlot = false)
        {
            // Stabilisco le azioni che posso fare
            // in modalità MANUALE posso fare tutto: se utensile presente posso rimuoverlo altrimenti posso caricarlo
            if (filter.ManualAction && !isAdditionalSlot)
                setupToolItem.SetupAction = toolMngId > 0 ? SetupActionEnum.RemoveFromSlot : SetupActionEnum.ManualLoadOnSlot;
            else
                setupToolItem.SetupAction = action;

            // Controllo se è specificato un toolManagementId e setto le informazioni del tool
            if (toolMngId > 0)
            {
                setupToolItem.Id = toolMngId;
                if (tool != null)
                {
                    setupToolItem.Code = tool.Code;
                    setupToolItem.Percentage = tool.Percentage;
                    setupToolItem.LocalizationKey = tool.LocalizationCode;
                }
                else
                {
                    setupToolItem.Percentage = 0;
                    setupToolItem.LocalizationKey = $"{LABEL_TOOLSTATUS}_INVALIDTOOL";
                    setupToolItem.SetupAction = SetupActionEnum.ManualActionNeeded;
                }
            }
        }

        /// <summary>
        /// Convert identifiers list value in specified conversionSystem 
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="conversionSystem"></param>
        /// <returns></returns>
        public static IdentifierDetailItem ConvertIdentifier(
                            this IdentifierDetailItem identifier
                            , MeasurementSystemEnum conversionSystem)
        {

            //Se l'identificativo è un numero allora lo converto se
            //il sistema di conversione è diverso da Metrico Decimale
            if (identifier.AttributeKind == AttributeKindEnum.Number)
            {
                var decimalValue = decimal.Parse(identifier.Value, CultureInfo.InvariantCulture);
                var convertedItem = ConvertToHelper.Convert(conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                , conversionSystemTo: conversionSystem, identifier.ItemDataFormat, decimalValue);
                identifier.Value = convertedItem.Value.ToString($"F{convertedItem.DecimalPrecision}", CultureInfo.InvariantCulture); ;
            }
            return identifier;
        }

        public static DetailIdentifier Convert(this DetailIdentifier identifier,
                        MeasurementSystemEnum conversionSystem)
        {
            //Se l'identificativo è un numero allora lo converto se
            //il sistema di conversione è diverso da Metrico Decimale
            if (identifier.AttributeDefinitionLink.AttributeDefinition.AttributeKind == AttributeKindEnum.Number)
            {
                var decimalValue = decimal.Parse(identifier.Value, CultureInfo.InvariantCulture);
                var convertedItem = ConvertToHelper.Convert(conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                , conversionSystemTo: conversionSystem
                                                , identifier.AttributeDefinitionLink.AttributeDefinition.DataFormat
                                                , decimalValue);
                identifier.Value = convertedItem.Value.ToString($"F{convertedItem.DecimalPrecision}", CultureInfo.InvariantCulture); ;
            }
            return identifier;
        }

        #region < ToString Methods >

        public static string ToString(this IEnumerable<IdentifierDetailItem> identifierDetails
                    , string separator = "-")
        {
            if (identifierDetails == null)
            {
                throw new System.ArgumentNullException(nameof(identifierDetails));
            }

            var orderedIdentifiers = identifierDetails.OrderBy(x => x.Order);
            string code = string.Empty;
            foreach (var identifier in identifierDetails)
            {
                if (identifier.Value == null)
                    continue;

                string value = identifier.Value.ToString();
                if (decimal.TryParse(identifier.Value.ToString(), out var decimalValue))
                    value = decimalValue.ToString("F2");

                if (code.IsNullOrEmpty())
                {
                    code = value;
                }
                else if (!string.IsNullOrEmpty(value))
                    code += $" {separator} {value}";
            }

            return code.Trim();
        }

        public static CodeGeneratorItem ApplyCustomMapping(this CodeGeneratorItem item,
                                           MeasurementSystemEnum conversionSystem
                                        , bool doConversion = true)
        {
            switch (item.AttributeKind)
            {
                case AttributeKindEnum.Number:
                    {
                        var conversionSystemTo = doConversion ? conversionSystem : MeasurementSystemEnum.MetricSystem;
                        var convertedItem = ConvertToHelper.ConvertForLabel(
                                                           conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                                           , conversionSystemTo: conversionSystemTo
                                                           , item.ItemDataFormat
                                                           , decimal.Parse(item.Value.ToString()));

                        if (convertedItem.Value.IsInteger() || convertedItem.DecimalPrecision == 0)
                            item.Value = decimal.ToInt32(convertedItem.Value).ToString();
                        else
                        {
                            //string format = "F2";
                            //if (conversionSystem is MeasurementSystemEnum.ImperialSystem
                            //                        or MeasurementSystemEnum.FractionalImperialSystem)
                            //{
                            //    format = $"F{convertedItem.DecimalPrecision}";
                            //}

                            //item.Value = convertedItem.Value.ToString(format, CultureInfo.InvariantCulture);
                            item.Value = Math.Round(convertedItem.Value, convertedItem.DecimalPrecision);

                        }
                    }
                    break;
                case AttributeKindEnum.Enum:
                    {
                        if (item.TypeName.IsNotNullOrEmpty())
                        {
                            var enumType = Type.GetType(item.TypeName);

                            var typeConverter = TypeDescriptor.GetConverter(enumType);
                            item.Value = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture
                                    , item.Value);

                            //CASO PARTICOLARE NozzleShapeEnum
                            if (enumType == typeof(NozzleShapeEnum))
                            {
                                if (Enum.TryParse<NozzleShapeEnum>(item.Value.ToString(), out var enumValue)
                                    && enumValue == NozzleShapeEnum.Bevel)
                                {
                                    item.Value = string.Empty;
                                }
                                else
                                {
                                    item.Value = item.Value.ToString().ToUpper();
                                }
                            }
                        }
                    }
                    break;
            }

            return item;
        }


        ///// <summary>
        ///// Costruisce il codice in funzione dei code generators
        ///// </summary>
        ///// <param name="codeGenerators"></param>
        ///// <param name="separator">Separatore</param>
        ///// <returns></returns>
        public static string ToString(this IEnumerable<CodeGeneratorItem> codeGenerators
            , string separator = "-")
        {
            if (codeGenerators == null)
            {
                throw new ArgumentNullException(nameof(codeGenerators));
            }

            var orderedIdentifiers = codeGenerators.OrderBy(x => x.Order);
            string code = string.Empty;
            foreach (var identifier in orderedIdentifiers)
            {
                if (identifier.Value == null)
                    continue;

                string value = identifier.Value.ToString();
                if (code.IsNullOrEmpty())
                {
                    code = value;
                }
                else if (!string.IsNullOrEmpty(value))
                    code += $" {separator} {value}";
            }

            return code.Trim();
        }

        #endregion < ToString Methods >



        /// <summary>
        /// Conversione json generica per gli elementi della configurazione
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object FromJson(this PropertyInfo property, string json)
        {
            object value = null;
            if (property.PropertyType.IsArray)
            {
                value = FluentSwitch.On<Type, object>(property.PropertyType.GetElementType())
                    .Case(typeof(string), _ => JsonConvert.DeserializeObject<string[]>(json))
                .End();
            }
            else
            {
                value = FluentSwitch.On<Type, object>(property.PropertyType)
                    .Case(typeof(ToleranceConfiguration), _ => JsonConvert.DeserializeObject<ToleranceConfiguration>(json)) //Tollerance
                    .Case(typeof(TorchConfigurationCollection), _ => JsonConvert.DeserializeObject<TorchConfigurationCollection>(json)) //PlaTorches
                .End();
            }
            return value;
        }


        /// <summary>
        /// Send Parameters value to CNC
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="parameterHandler"></param>
        /// <returns></returns>
        public static Result SendToCNC(this IEnumerable<MachineParameterLink> parameterLinks, float value
                                    , IParameterHandler parameterHandler)
        {
            var result = Result.Ok();
            foreach (var parameter in parameterLinks)
            {
                result = Result.Combine(result, parameterHandler.SetParameter(parameter, value));
                if (result.Failure) break;
            }
            return result;
        }

        public static PLCCycleEnum GetPLCCycle(this ConfirmSetupAction action)
        {
            var pLCCycle = PLCCycleEnum.None;

            switch (action.Action)
            {
                case SetupActionEnum.LoadOnSlot:
                case SetupActionEnum.ManualLoadOnSlot:
                    pLCCycle = PLCCycleEnum.LoadOnSlot;
                    break;
                case SetupActionEnum.RemoveFromSlot:
                    pLCCycle = PLCCycleEnum.RemoveFromSlot;
                    break;
                case SetupActionEnum.LoadOnUnit:
                    pLCCycle = PLCCycleEnum.LoadOnUnit;
                    break;
                case SetupActionEnum.RemoveFromUnit:
                    pLCCycle = PLCCycleEnum.RemoveFromUnit;
                    break;
            }

            return pLCCycle;
        }

        //public static void SetXPR(this IFanucPlaTool tool, PlaToolProcessDataXPR plaToolProcessDataXPR)
        //{
        //    /// Attributo di processo: Pressione di pierce gas protezione [psi]
        //    tool.ShieldPiercePressure = plaToolProcessDataXPR.ShieldPiercePressure;
        //    /// Attributo di processo: Pressione di taglio gas plasma [psi]
        //    tool.PlasmaCutflow = plaToolProcessDataXPR.PlasmaCutflowPressure;
        //    /// Attributo di processo: Pressione di taglio gas protezione [psi]
        //    tool.ShieldCutflow = plaToolProcessDataXPR.ShieldCutflowPressure;
        //}

        //public static void SetHPR(this IFanucPlaTool tool, PlaToolProcessDataHPR plaToolProcessDataHPR)
        //{
        //    /// Attributo di processo: Pressione di pierce gas protezione [psi]
        //    tool.ShieldPiercePressure = plaToolProcessDataHPR.ShieldPreFlowPressure;
        //    /// Attributo di processo: (solo HPR) Pressione di pre-flow gas plasma [psi]
        //    tool.PlasmaPiercePressure = plaToolProcessDataHPR.PlasmaPreFlowPressure;
        //    /// Attributo di processo: Pressione di taglio gas plasma [psi]
        //    tool.PlasmaCutflow = plaToolProcessDataHPR.PlasmaCutflowPressure;
        //    /// Attributo di processo: Pressione di taglio gas protezione [psi]
        //    tool.ShieldCutflow = plaToolProcessDataHPR.ShieldCutflowPressure;
        //}

        /// <summary>
        /// Condition for MinRequiredConsole Filter
        /// </summary>
        /// <param name="consoleTypeSource"></param>
        /// <param name="consoleTypeToCheck"></param>
        /// <returns></returns>
        public static bool Contains(this MinRequiredConsoleTypeEnum consoleTypeSource
                                , MinRequiredConsoleTypeEnum consoleTypeToCheck)
        {
            bool condition = false;
            switch (consoleTypeSource)
            {
                case MinRequiredConsoleTypeEnum.OptiMix:
                    condition = consoleTypeToCheck == MinRequiredConsoleTypeEnum.OptiMix ||
                                consoleTypeToCheck == MinRequiredConsoleTypeEnum.Vwi ||
                                consoleTypeToCheck == MinRequiredConsoleTypeEnum.Core;
                    break;
                case MinRequiredConsoleTypeEnum.Vwi:
                    condition = consoleTypeToCheck == MinRequiredConsoleTypeEnum.Vwi ||
                                consoleTypeToCheck == MinRequiredConsoleTypeEnum.Core;
                    break;
                case MinRequiredConsoleTypeEnum.Core:
                    condition = consoleTypeToCheck == MinRequiredConsoleTypeEnum.Core;
                    break;
            }

            return condition;
        }

        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.Next(),
                               rng.Next(),
                               rng.Next(),
                               sign,
                               scale);
        }

        /// <summary>
        /// Get ParentType Extension for ToolRangeTypeEnum
        /// </summary>
        /// <param name="rangeType"></param>
        /// <returns></returns>
        public static ParentTypeEnum GetParentType(this ToolRangeTypeEnum rangeType)
        {
            var parentType = ParentTypeEnum.ToolRange;
            switch (rangeType)
            {
                case ToolRangeTypeEnum.Drill:
                case ToolRangeTypeEnum.Cut:
                    parentType = ParentTypeEnum.ToolRange;
                    break;
                case ToolRangeTypeEnum.Bevel:
                    parentType = ParentTypeEnum.ToolSubRangeBevel;
                    break;
                case ToolRangeTypeEnum.Mark:
                    parentType = ParentTypeEnum.ToolRangeMarking;
                    break;
                case ToolRangeTypeEnum.TrueHole:
                    parentType = ParentTypeEnum.ToolSubRangeTrueHole;
                    break;
            }

            return parentType;
        }

        public static SubRangeTypeEnum GetSubRangeType(this ToolRangeTypeEnum rangeType)
        {
            var subRangeType = SubRangeTypeEnum.None;
            switch (rangeType)
            {
                case ToolRangeTypeEnum.Bevel:
                    subRangeType = SubRangeTypeEnum.Bevel;
                    break;
                case ToolRangeTypeEnum.TrueHole:
                    subRangeType = SubRangeTypeEnum.TrueHole;
                    break;
            }

            return subRangeType;
        }


        /// <summary>
        /// Get Next Expiration date
        /// </summary>
        /// <param name="type"></param>
        /// <param name="repeatitionMode"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static long GetNextTime(MaintenanceTypeEnum type,
                                            long repeatitionMode
                                            , long currentTime)
        {
            long nextTime = currentTime;

            if (type == MaintenanceTypeEnum.Calendar)
            {
                var expirationDate = nextTime.FromUnixTime();

                //Ricavo la data della prossima esecuzione in base al RepeatMode impostato
                switch ((MaintenanceActionRepeatEachEnum)repeatitionMode)
                {
                    case MaintenanceActionRepeatEachEnum.Weekly:
                        {
                            expirationDate = expirationDate.AddDays(7);
                        }
                        break;
                    case MaintenanceActionRepeatEachEnum.Monthly:
                        {
                            expirationDate = expirationDate.AddDays(30);
                        }
                        break;
                    case MaintenanceActionRepeatEachEnum.Yearly:
                        {
                            expirationDate = expirationDate.AddDays(365);
                        }
                        break;
                }

                nextTime = expirationDate.ToUnixTime();
            }
            else
            {
                nextTime += repeatitionMode;
            }

            return nextTime;
        }


        /// <summary>
        /// Check if cached operation can handle Additional Items
        /// </summary>
        /// <param name="cachedOperation"></param>
        /// <returns></returns>
        public static OperationTypeEnum? CanHandleAdditionalItems(this CachedPieceOperation cachedOperation)
        {
            return DomainExtensions.GetEnumAttribute<OperationTypeEnum, RelatedAdditionalItemTypeAttribute>
                    (cachedOperation.Type)?.AdditionalItemType ?? null;
        }

        public static void SetNextNoticeDatesFromPostPone(
                            this MaintenanceConfiguration configuration
                           , long postPoneMode)
        {

            if (configuration.Maintenance.Type == MaintenanceTypeEnum.Calendar)
            {
                var nextExpirationDate = configuration.ExpirationTime.FromUnixTime();
                //Ricavo la data della prossima esecuzione in base al RepeatMode impostato
                switch ((MaintenanceDeltaModesEnum)postPoneMode)
                {
                    case MaintenanceDeltaModesEnum.OneDay:
                        nextExpirationDate = nextExpirationDate.AddDays(1);
                        break;
                    case MaintenanceDeltaModesEnum.TwoDays:
                        nextExpirationDate = nextExpirationDate.AddDays(2);
                        break;
                    case MaintenanceDeltaModesEnum.ThreeDays:
                        nextExpirationDate = nextExpirationDate.AddDays(3);
                        break;
                    case MaintenanceDeltaModesEnum.OneWeek:
                        nextExpirationDate = nextExpirationDate.AddDays(7);
                        break;
                }
                configuration.ExpirationTime = nextExpirationDate.ToUnixTime();
            }
            else
            {
                configuration.ExpirationTime += postPoneMode;
            }
        }

        /// <summary>
        /// Get Next execution time by maintenance Configuration and repeatMode
        /// </summary>
        /// <param name="maintenanceConfiguration"></param>
        /// <returns></returns>
        public static void SetNextNoticeDates(
                    this MaintenanceConfiguration maintenanceConfiguration
                    , DateTime refDate)
        {
            long nextExpirationTime = maintenanceConfiguration.ExpirationTime;
            if (maintenanceConfiguration.Maintenance.Type == MaintenanceTypeEnum.Calendar)
            {
                var expirationDate = refDate;
                //Ricavo la data della prossima esecuzione in base al RepeatMode impostato
                switch ((MaintenanceActionRepeatEachEnum)maintenanceConfiguration.RepeatEach)
                {
                    case MaintenanceActionRepeatEachEnum.Weekly:
                        {
                            expirationDate = expirationDate.AddDays(7);
                        }
                        break;
                    case MaintenanceActionRepeatEachEnum.Monthly:
                        {
                            expirationDate = expirationDate.AddDays(30);
                        }
                        break;
                    case MaintenanceActionRepeatEachEnum.Yearly:
                        {
                            expirationDate = expirationDate.AddDays(365);
                        }
                        break;
                }
                //La nuova data di notifica è data dalla somma della differenza giorni e la nuova data 
                //di scadenza
                maintenanceConfiguration.ExpirationTime = expirationDate.Date.ToUnixTime();
            }
            else
            {
                //Aggiornamento tempi
                maintenanceConfiguration.ExpirationTime += maintenanceConfiguration.RepeatEach;
            }
        }

        public static IEnumerable<IEntityRulesHandler> GetEntityRulesHandlers(this IServiceFactory serviceFactory
                , ParentTypeEnum parentType)
        {
            var resolver = serviceFactory.GetService<IEntityHandlerFactory>();
            return resolver.ResolveHandlers(parentType);
        }

        #region < GetFilteredAttributes >
        public static IEnumerable<AttributeDetailItem> GetFilteredAttributes(
             this IEnumerable<IEntityRulesHandler> rulesHandlers
            , IEnumerable<AttributeDetailItem> attributeDetails
            , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        {
            IEnumerable<AttributeDetailItem> attributes = attributeDetails.ToList();
            foreach (var handler in rulesHandlers)
            {
                handler.Init(additionalInfos);
                attributes = handler.HandleAll(attributes);
            }

            return attributes;
        }

        //public static IEnumerable<OperationTypeAttributeDefinitionMaster> GetFilteredAttributes(
        //     this IEnumerable<IEntityRulesHandler> rulesHandlers
        //    , IEnumerable<OperationTypeAttributeDefinitionMaster> attributeValues
        //    , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        //{
        //    //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
        //    //per evitare l'errore "MySQL connection is already in use"
        //    IEnumerable<OperationTypeAttributeDefinitionMaster> attributes = attributeValues.ToList();
        //    foreach (var handler in rulesHandlers)
        //    {
        //        handler.Init(additionalInfos);
        //        attributes = handler.HandleAll(attributes);
        //    }

        //    return attributes;
        //}

        public static IEnumerable<EntityAttribute> GetFilteredAttributes(
             this IEnumerable<IEntityRulesHandler> rulesHandlers
            , IEnumerable<EntityAttribute> attributeValues
            , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        {
            //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
            //per evitare l'errore "MySQL connection is already in use"
            IEnumerable<EntityAttribute> attributes = attributeValues.ToList();
            foreach (var handler in rulesHandlers)
            {
                handler.Init(additionalInfos);
                attributes = handler.HandleAll(attributes);
            }

            return attributes;
        }

        //public static IEnumerable<OperationAttributeValueMaster> GetFilteredAttributes(
        //             this IEnumerable<IEntityRulesHandler> rulesHandlers
        //            , IEnumerable<OperationAttributeValueMaster> attributeValues
        //            , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        //{
        //    //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
        //    //per evitare l'errore "MySQL connection is already in use"
        //    IEnumerable<OperationAttributeValueMaster> attributes = attributeValues.ToList();
        //    foreach (var handler in rulesHandlers)
        //    {
        //        handler.Init(additionalInfos);
        //        attributes = handler.HandleAll(attributes);
        //    }

        //    return attributes;
        //}

        //public static IEnumerable<ProfileTypeAttributeValueMaster> GetFilteredAttributes(
        //     this IEnumerable<IEntityRulesHandler> rulesHandlers
        //    , IEnumerable<ProfileTypeAttributeValueMaster> attributeValues
        //    , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        //{
        //    //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
        //    //per evitare l'errore "MySQL connection is already in use"
        //    IEnumerable<ProfileTypeAttributeValueMaster> attributes = attributeValues.ToList();
        //    foreach (var handler in rulesHandlers)
        //    {
        //        handler.Init(additionalInfos);
        //        attributes = handler.HandleAll(attributes);
        //    }

        //    return attributes;
        //}

        //public static IEnumerable<ProfileTypeAttributeDefinitionMaster> GetFilteredAttributes(
        //     this IEnumerable<IEntityRulesHandler> rulesHandlers
        //    , IEnumerable<ProfileTypeAttributeDefinitionMaster> attributeDefinitions
        //    , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        //{
        //    //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
        //    //per evitare l'errore "MySQL connection is already in use"
        //    IEnumerable<ProfileTypeAttributeDefinitionMaster> attributes = attributeDefinitions.ToList();
        //    foreach (var handler in rulesHandlers)
        //    {
        //        handler.Init(additionalInfos);
        //        attributes = handler.HandleAll(attributes);
        //    }

        //    return attributes;
        //}

        public static IEnumerable<AttributeSource> GetFilteredAttributes(
                this IEnumerable<IEntityRulesHandler> rulesHandlers
                , IEnumerable<AttributeSource> attributeSources
                , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        {
            //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
            //per evitare l'errore "MySQL connection is already in use"
            IEnumerable<AttributeSource> attributes = attributeSources.ToList();
            foreach (var handler in rulesHandlers)
            {
                handler.Init(additionalInfos);
                attributes = handler.HandleAll(attributes);
            }

            return attributes;
        }

        public static IEnumerable<ClusteredAttributeDetailItem> GetFilteredAttributes(
            this IEnumerable<IEntityRulesHandler> rulesHandlers
           , IEnumerable<ClusteredAttributeDetailItem> attributeValues
           , Dictionary<AttributeDefinitionEnum, object> additionalInfos)
        {
            //La lista degli attributi prima di eseguire gli handlers deve essere concretizzata
            //per evitare l'errore "MySQL connection is already in use"
            IEnumerable<ClusteredAttributeDetailItem> attributes = attributeValues.ToList();
            foreach (var handler in rulesHandlers)
            {
                handler.Init(additionalInfos);
                attributes = handler.HandleAll(attributes);
            }

            return attributes;
        }

        #endregion < GetFilteredAttributes >


        public static bool IsMacro(this OperationTypeEnum operationType)
        {
            return
                operationType == OperationTypeEnum.Mill || operationType == OperationTypeEnum.Cope
                    || operationType == OperationTypeEnum.MCut;
        }

        public static bool IsMainFilter(this ToolRangeDetailIdentifier detailIdentifier,
                DatabaseDisplayNameEnum mainFilterDisplayName)
        {
            return Enum.TryParse<DatabaseDisplayNameEnum>(detailIdentifier.DisplayName
                                                , out var databaseDisplayName)
                                        && databaseDisplayName == mainFilterDisplayName;
        }

        public static MarkingUnitTypeEnum GetMarkingUnitType(this ToolTypeEnum tool)
        {
            switch (tool)
            {
                case ToolTypeEnum.TS51:
                case ToolTypeEnum.TS53:
                    return MarkingUnitTypeEnum.Plasma;

                case ToolTypeEnum.TS68:
                case ToolTypeEnum.TS77:
                case ToolTypeEnum.TS78:
                case ToolTypeEnum.TS79:
                    return MarkingUnitTypeEnum.Drill;

                case ToolTypeEnum.TS89:
                    return MarkingUnitTypeEnum.Reajet;

                default: //TS87, TS88 //caso TS non definito
                    return MarkingUnitTypeEnum.NotSpecified;
            }
        }

        #region < RM management >
        public static void SetRTMValues(this RTMBackLog backLogRecord, long[] values)
        {
            Type backlogType = typeof(RTMBackLog);
            values
                .Select((value, index) =>
                {
                    PropertyInfo rtmProperty = backlogType.GetProperty($"RTM{index + 1}");
                    rtmProperty.SetValue(backLogRecord, value);
                    return true;
                }).ToList();
        }

        public static long[] GetRTMValues(this RTMBackLog backLogRecord)
        {
            Type backlogType = typeof(RTMBackLog);

            //Recupera le proprietà RTM
            var rtmProperties = backlogType.GetProperties()
                                .Where(p => p.Name.ToUpper().StartsWith("RTM"))
                                .OrderNatural(p => p.Name);

            return rtmProperties.Select(p =>
            {
                var value = p.GetValue(backLogRecord);
                if (value != null)
                    return System.Convert.ToInt64(value);
                else
                    return (long)0;
            }).ToArray();
        }

        public static void SetRCMValues(this RCMBackLog backLogRecord, decimal[] values)
        {
            Type backlogType = typeof(RCMBackLog);
            values
                .Select((value, index) =>
                {
                    PropertyInfo rtmProperty = backlogType.GetProperty($"RCM{index + 1}");
                    rtmProperty.SetValue(backLogRecord, value);
                    return true;
                }).ToList();
        }


        public static decimal[] GetRCMValues(this RCMBackLog backLogRecord)
        {
            Type backlogType = typeof(RCMBackLog);

            //Recupera le proprietà RTM
            var rmProperties = backlogType.GetProperties()
                                .Where(p => p.Name.ToUpper().StartsWith("RCM"))
                                .OrderNatural(p => p.Name);

            return rmProperties.Select(p =>
            {
                var value = p.GetValue(backLogRecord);
                if (value != null)
                    return System.Convert.ToDecimal(value);
                else
                    return (decimal)0;
            }).ToArray();
        }
        #endregion < RM management >

        /// <summary>
        /// ToDictionary Method
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ToIdentifiersDictionary(this PieceItemIdentifier pieceItemIdentifier)
        {
            var identifiers = new Dictionary<string, string>();

            identifiers.Add("Assembly", pieceItemIdentifier.Assembly);
            identifiers.Add("Contract", pieceItemIdentifier.Contract);
            identifiers.Add("Drawing", pieceItemIdentifier.Drawing);
            identifiers.Add("Part", pieceItemIdentifier.Part);
            identifiers.Add("Project", pieceItemIdentifier.Project);

            return identifiers;
        }

        public static Dictionary<string, string> ToIdentifiersDictionary(this Entity entity)
        {
            var identifiers = new Dictionary<string, string>();

            //identifiers.Add("Assembly", piece.Assembly);
            //identifiers.Add("Contract", piece.Contract);
            //identifiers.Add("Drawing", piece.Drawing);
            //identifiers.Add("Part", piece.Part);
            //identifiers.Add("Project", piece.Project);

            return identifiers;
        }

        ///
        public static void SetUnitsEnabledMask(this ToolDetailItem tool)
        {
            var unitMaskAttributesEnumIds = new[]
            {
                AttributeDefinitionEnum.ToolEnableA,
                AttributeDefinitionEnum.ToolEnableB,
                AttributeDefinitionEnum.ToolEnableC,
                AttributeDefinitionEnum.ToolEnableD
            };

            var toolMask = ToolUnitMaskEnum.None;

            var attributes = tool.Attributes.Where(a => unitMaskAttributesEnumIds.Contains(a.EnumId))
                        .OrderBy(a => a.Order);

            foreach(var attribute in attributes)
            {
                var intValue = System.Convert.ToInt32(attribute.Value.CurrentValue);

                if (intValue == 1)
                {
                    switch (attribute.EnumId)
                    {
                        case AttributeDefinitionEnum.ToolEnableA:
                            toolMask |= ToolUnitMaskEnum.UnitA;
                            break;
                        case AttributeDefinitionEnum.ToolEnableB:
                            toolMask |= ToolUnitMaskEnum.UnitB;
                            break;
                        case AttributeDefinitionEnum.ToolEnableC:
                            toolMask |= ToolUnitMaskEnum.UnitC;
                            break;
                        case AttributeDefinitionEnum.ToolEnableD:
                            toolMask |= ToolUnitMaskEnum.UnitD;
                            break;
                    }

                }


            }

            tool.UnitEnablingMask = toolMask;
                    
        }




    }
}
