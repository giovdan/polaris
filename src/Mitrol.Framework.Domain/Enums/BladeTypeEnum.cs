namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Tipologia di lama
    /// </summary>
    public enum BladeTypeEnum
    {
        /// <summary>
        /// Nessuna
        /// </summary>
        None=0,
        
        /// <summary>
        /// A Nastro
        /// </summary>
        BandSaw,
        
        /// <summary>
        /// A disco
        /// </summary>
        DiscSaw
    }

    /// <summary>
    /// Tipologia di velocità di avanzamento (da configurazione)
    /// </summary>
    public enum BladeForwardSpeedTypeEnum
    {
        None = 0,
        
        /// <summary>
        /// mm/min
        /// </summary>
        Linear = 1,
        
        /// <summary>
        /// mm/dente
        /// </summary>
        Teeth,
        
        /// <summary>
        /// cm2/min
        /// </summary>
        Section
    }


    /// <summary>
    /// Configurazione della tipologia di funzionamento del software di gestione dei tagli.
    /// Tale parametro identifica la posizione del riferimento su cui il software basa tutti i
    /// posizionamenti delle quote X per effettuare i tagli.
    /// </summary>
    public enum BladeReferenceOriginEnum
    {
        /// <summary>
        /// Riferimento sulla mezzaria della lama
        /// </summary>
        Center = 0,
        
        /// <summary>
        /// Riferimento sul bordo esterno della lama (lato quote X crescenti)
        /// </summary>
        EdgeSideXincreasing = 1,
        
        /// <summary>
        /// Riferimento sul bordo esterno della lama (lato quote X decrescenti)
        /// </summary>
        EdgeSideXdecreasing = 2,
    }
}
