namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitrol.Framework.Domain.Models;

    public class StockItemInfo
    {
        /// <summary>
        /// Attributi dello stock
        /// </summary>
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
        /// <summary>
        /// Attributi del profilo associato allo stock
        /// </summary>
        public Dictionary<DatabaseDisplayNameEnum, object> ProfileAttributes { get; set; }

        public StockItemInfo(StockDetailItem stockDetail, MeasurementSystemEnum conversionSystemFrom)
        {
            Attributes = stockDetail.Groups.SelectMany(group => group.Details)
                            .ToDictionary(a => (DatabaseDisplayNameEnum)Enum.Parse(typeof(DatabaseDisplayNameEnum)
                                        , a.DisplayName),
                                    a => a.GetAttributeValue(conversionSystemFrom: conversionSystemFrom
                                                    , checkTypeNameForEnum: false));

            
            Attributes.Add(DatabaseDisplayNameEnum.ProfileType, (ProfileTypeEnum)stockDetail.ProfileTypeId);
            Attributes.Add(DatabaseDisplayNameEnum.MaterialType, stockDetail.MaterialTypeId);

            ProfileAttributes = stockDetail.ProfileAttributes
                            .ToDictionary(a => (DatabaseDisplayNameEnum)Enum.Parse(typeof(DatabaseDisplayNameEnum)
                                        , a.DisplayName),
                                    a => a.GetAttributeValue(conversionSystemFrom: conversionSystemFrom
                                                    , checkTypeNameForEnum: false));
        }

        public StockItemInfo()
        {
            Attributes = new Dictionary<DatabaseDisplayNameEnum, object>();

            ProfileAttributes = new Dictionary<DatabaseDisplayNameEnum, object>();
        }
    }
}
