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
}
