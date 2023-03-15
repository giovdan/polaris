namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Enumerato che definisce lo scope di appartenenza di un attributo (sostituisce la logica del flag IsQuickAccess)
    /// </summary>
    public enum AttributeScopeEnum: int
    {
        Optional = 0,
        Fundamental = 1,
        Preview = 2,
    }

    /// <summary>
    /// Enumerato che definisce come valorizzare il default dell'attributo
    /// </summary>
    public enum AttributeBehavior: int
    {
        DataDefault = 0, 
        LastInserted = 1
    }
}
