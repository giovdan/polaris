
namespace Mitrol.Framework.Domain.Enums
{

    /// <summary>
    /// Enumerato che indica da quale operazione è stato generato il taglio
    /// </summary>
    public enum SeparationCutOperationEnum
    {
        // Taglio generato da tagli semplici
        Standard = 1,

        // Taglio generato da MSaw iniziale
        MSawInitial = 2,

        // Taglio generato da MSaw finale
        MSawFinal = 3,

    }
}
