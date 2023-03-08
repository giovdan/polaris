namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public partial class PlantUnitAttribute : Attribute
    {
        public PlantUnitEnum PlantUnit { get; private set; }

        public PlantUnitAttribute(PlantUnitEnum plantUnit)
        {
            PlantUnit = plantUnit;
        }
    }
}