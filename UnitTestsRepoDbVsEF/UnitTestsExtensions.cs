

namespace UnitTests
{
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Models;
    using System;
    using UnitTests.Models;

    public static class UnitTestsExtensions
    {
        public static AttributeValue SetAttributeValue(this AttributeValue dbAttribute, AttributeItem a)
        {
            switch(a.AttributeKind)
            {
                case AttributeKindEnum.Enum:
                    {
                        dbAttribute.Value = a.Value.CurrentValueId;
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Bool:
                    {
                        dbAttribute.Value = Convert.ToBoolean(a.Value.CurrentValue) ? 1: 0;
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Number:
                
                    {
                        dbAttribute.Value = Convert.ToDecimal(a.Value.CurrentValue);
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Date:
                case AttributeKindEnum.String:
                    {
                        dbAttribute.TextValue = a.Value.CurrentValue.ToString();
                        dbAttribute.Value = 0;
                    }
                    break;
            }

            return dbAttribute;
        }
    }
}
