using Mitrol.Framework.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitrol.Framework.MachineManagement.Application.Attributes
{
    /// <summary>
    /// Attribute to handle dynamic types
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AttributeInfoAttribute: Attribute
    {
        public string Url { get; set; }
        public ValueTypeEnum ValueType { get; set; }
        public string TypeName { get; set; }
        public AttributeDataFormatEnum AttributeDataFormat { get; set; }
        public AttributeKindEnum AttributeKind { get; set; }
        public AttributeTypeEnum AttributeType { get; set; }
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        public ClientControlTypeEnum ControlType { get; set; }
        public AttributeScopeEnum AttributeScope { get; set; }

        public AttributeInfoAttribute(ValueTypeEnum valueType)
        {
            ValueType = valueType;
            Url = string.Empty;
        }

        public AttributeInfoAttribute(ValueTypeEnum valueType, string url):this(valueType)
        {
            Url = url;
        }


        public AttributeInfoAttribute(ValueTypeEnum valueType, string url, Type typeName) : this(valueType, url)
        {
            Url = url;
            TypeName = typeName?.AssemblyQualifiedName;
        }

        public AttributeInfoAttribute(AttributeKindEnum attributeKind,
                                    AttributeTypeEnum attributeType,
                                    AttributeDataFormatEnum attributeDataFormat = AttributeDataFormatEnum.AsIs,
                                    AttributeScopeEnum attributeScope = AttributeScopeEnum.Optional,
                                    ClientControlTypeEnum clientControlType = ClientControlTypeEnum.None,
                                    ProtectionLevelEnum level = ProtectionLevelEnum.None,
                                    ValueTypeEnum valueType = ValueTypeEnum.Flat,
                                    string url = null,
                                    Type typeName = null): this(valueType,url, typeName)
        {
            AttributeDataFormat = attributeDataFormat;
            AttributeKind = attributeKind;
            AttributeType = attributeType;
            ControlType = clientControlType;
            ProtectionLevel = level;
            AttributeScope = attributeScope;
        }

        // Special constructors
        public AttributeInfoAttribute(Type typeName): this(ValueTypeEnum.Flat, string.Empty
                                        ,typeName)
        {

        }

    }
}
