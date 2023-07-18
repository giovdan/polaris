namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Application;
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Models;
    using System;
    using Newtonsoft.Json;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System.Linq;

    internal class StockIdentifierOrder
    {
        public DatabaseDisplayNameEnum DatabaseDisplayName { get; set; }
        public bool IsText { get; set; }

        public int Order { get; set; }
    }
    public abstract class StockHashCodeGenerator : IHashCodeGenerator
    {

        private static List<StockIdentifierOrder> _stockIdentifiers = new List<StockIdentifierOrder>() {
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.Length,IsText=false,Order=1},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.Thickness,IsText=false,Order=2},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.Width,IsText=false,Order=3},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.HeatNumber,IsText=true,Order=4},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.Supplier,IsText=true,Order=5},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.MaterialCode,IsText=true,Order=6},
                                    new StockIdentifierOrder(){DatabaseDisplayName=DatabaseDisplayNameEnum.ProfileCode,IsText=true,Order=7}};

        public abstract Dictionary<DatabaseDisplayNameEnum, object> GetAttributes();

        public static string CalculateHashcode<T>(Dictionary<DatabaseDisplayNameEnum, T> attributes)
        {
            List<object> attrs = new List<object>();

            var stockIdentifiers = _stockIdentifiers.OrderBy(i => i.Order);
            foreach (var identifier in stockIdentifiers)
            {
                if (attributes.TryGetValue(identifier.DatabaseDisplayName, out var attributeValue) && attributeValue != null)
                {
                    if (typeof(T)==typeof(AttributeValue))
                    {
                        AttributeValue a = (AttributeValue)Convert.ChangeType(attributeValue, typeof(AttributeValue)); 
                        if (identifier.IsText)
                            attrs.Add(a.TextValue);
                        else
                            attrs.Add(a.Value);
                    }
                    else
                    {
                        if (identifier.DatabaseDisplayName == DatabaseDisplayNameEnum.MaterialCode || identifier.DatabaseDisplayName == DatabaseDisplayNameEnum.ProfileCode)
                        {
                            var valueTuple = JsonConvert.DeserializeObject<BaseInfoItem<long, string>>(attributes[identifier.DatabaseDisplayName].ToString());
                            attrs.Add(valueTuple.Value);
                        }
                        else
                            attrs.Add(attributes[identifier.DatabaseDisplayName]);
                    }
                }
            }
            return attrs.CalculateHash();
        }

        public string CalculateHashcode()
        {
            return StockHashCodeGenerator.CalculateHashcode(this.GetAttributes());
        }
    }
}
