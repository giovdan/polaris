


namespace Mitrol.Framework.MachineManagement.Application
{
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System.Globalization;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Extensions;
    using System.ComponentModel;

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

        public static DetailIdentifierMaster Convert(this DetailIdentifierMaster identifier,
                          MeasurementSystemEnum conversionSystem)
        {
            //Se l'identificativo è un numero allora lo converto se
            //il sistema di conversione è diverso da Metrico Decimale
            if (identifier.AttributeKind == AttributeKindEnum.Number)
            {
                var decimalValue = decimal.Parse(identifier.Value, CultureInfo.InvariantCulture);
                var convertedItem = ConvertToHelper.Convert(conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                , conversionSystemTo: conversionSystem
                                                , identifier.DataFormat
                                                , decimalValue);
                identifier.Value = convertedItem.Value.ToString($"F{convertedItem.DecimalPrecision}", CultureInfo.InvariantCulture); ;
            }
            return identifier;
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
    }

}
