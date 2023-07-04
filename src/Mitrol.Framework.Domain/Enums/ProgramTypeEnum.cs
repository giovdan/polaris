namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Program Type Enumeration
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue(G_97)]
    public enum ProgramTypeEnum : int
    {
        None = 0,
        /// <summary>
        /// CAM program
        /// </summary>
        [EnumSerializationName("CAM")]
        G_92 = 92,
        /// <summary>
        /// Sequenza pezzi
        /// </summary>
        [EnumSerializationName("PiecesSequence")]
        G_93 = 93,
        /*
        /// <summary>
        /// Nesting di tagli (solo per linee di taglio)
        /// </summary>
        G_94 = 94,
        */
        /// <summary>
        /// Nesting lineare
        /// </summary>
        [EnumSerializationName("LinearNesting")]
        G_95 = 95,

        /// <summary>
        /// Pezzo a misura
        /// </summary>
        [EnumSerializationName("PieceToMeasure")]
        G_97 = 97,

        /// <summary>
        /// Nesting piastre
        /// </summary>
        [EnumSerializationName("PlateNesting")]
        G_98 = 98,
    }

    public static class ProgramTypeEnumExtensions
    {
        public static IEnumerable<KeyValuePair<MachineFeaturesEnum, object>> ToFeatures(
                this IEnumerable<ProgramTypeEnum> managedProgramTypes)
        {
            if (managedProgramTypes is null)
            {
                throw new ArgumentNullException(nameof(managedProgramTypes));
            }

            // Ritorna true nel caso in cui nella lista dei tipi di programmi gestiti un valore diverso da G_92
            return new Dictionary<MachineFeaturesEnum, object>
            { { MachineFeaturesEnum.ShowPiecesList, managedProgramTypes
                                                    .Any(type => type != ProgramTypeEnum.G_92)},
              { MachineFeaturesEnum.CanAddEditPrograms, managedProgramTypes
                                                    .Any(type => type != ProgramTypeEnum.G_92)}};


        }
    }
}
