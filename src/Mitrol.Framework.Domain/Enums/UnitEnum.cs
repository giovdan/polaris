namespace Mitrol.Framework.Domain.Enums
{
    public enum UnitEnum
    {
        /// <summary>
        /// Non definito
        /// </summary>
        None = 0,

        /// <summary>
        /// Lato destro
        /// </summary>
        A = 1,

        /// <summary>
        /// Lato sinistro
        /// </summary>
        B = 2,

        /// <summary>
        /// Lato superiore
        /// </summary>
        C = 3,

        /// <summary>
        /// Lato inferiore
        /// </summary>
        D = 4,

        /// <summary>
        /// Lato destro e sinistro
        /// </summary>
        AB = 5,

        /// <summary>
        /// Tutti i lati (segatrice)
        /// </summary>
        S = 6
    }

    public static class UnitEnumExtensions
    {
        public static AttributeDefinitionEnum GetRelatedAttribute(this UnitEnum unitEnum)
        {
            var attributeDefinition = AttributeDefinitionEnum.None;

            switch (unitEnum)
            {
                case UnitEnum.A:
                    {
                        attributeDefinition = AttributeDefinitionEnum.ToolEnableA;
                    }
                    break;
                case UnitEnum.B:
                    {
                        attributeDefinition = AttributeDefinitionEnum.ToolEnableB;
                    }
                    break;
                case UnitEnum.C:
                    {
                        attributeDefinition = AttributeDefinitionEnum.ToolEnableC;
                    }
                    break;
                case UnitEnum.D:
                    {
                        attributeDefinition = AttributeDefinitionEnum.ToolEnableD;
                    }
                    break;
             }

            return attributeDefinition;
         }

        public static ToolUnitMaskEnum GetToolUnitMaskEnum(this UnitEnum unitEnum)
        {
            var mask = ToolUnitMaskEnum.None;
            switch (unitEnum)
            {
                case UnitEnum.A:
                    mask = ToolUnitMaskEnum.UnitA;
                    break;
                case UnitEnum.B:
                    mask = ToolUnitMaskEnum.UnitB;
                    break;
                case UnitEnum.C:
                    mask = ToolUnitMaskEnum.UnitC;
                    break;
                case UnitEnum.D:
                    mask = ToolUnitMaskEnum.UnitD;
                    break;
            }

            return mask;
        }
    }
}
