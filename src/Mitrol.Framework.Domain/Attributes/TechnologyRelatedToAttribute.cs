namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    /// <summary>
    /// Descrive la relazione tra tipologia di unità ed una tecnologia.
    /// Permette di gestire una serie di attributi diversa in base alla tecnologia.
    /// </summary>
    /// <remarks>
    /// Viene utilizzato per gestire le tecnologie Hpr e Xpr che sono
    /// applicabili solamente per unità di tipo <see cref="PlasmaTorch"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class TechnologyRelatedToAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PlantUnitEnum PlantUnit { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plantUnit"></param>
        public TechnologyRelatedToAttribute(PlantUnitEnum plantUnit) => PlantUnit = plantUnit;
    }
}