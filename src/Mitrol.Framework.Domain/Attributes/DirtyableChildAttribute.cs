namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    /// <summary>
    /// Definisce la proprietà associata come dirtyable.
    /// </summary>
    /// <remarks>
    /// Le proprietà dichiarate come dirtyable vengono intercettate
    /// dal modello <see cref="Observable"/> per potersi agganciare all'evento PropertyChanged
    /// dell'oggetto che implementa <see cref="INotifyPropertyChanged"/> contenuto nella proprietà.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DirtyableChildAttribute : Attribute { }
}
