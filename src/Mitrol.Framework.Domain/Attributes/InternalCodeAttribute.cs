namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    //TODO: rename/retarget InternalCodeAttribute 
    // Il nome InternalCode non rende chiara la sua applicazione.
    // Questo attributo è usato per avere una rappresentazione
    // del ToolType diversa dal membro dell'enumerato.
    // Inoltre rende più manutenibile il database nel quale i ToolType
    // sono rappresentati con il valore numerico dell'enumerato.
    // Valutare l'eventuale definizione di un secondo attributo per 
    // limitarne l'uso ad un unica funzione.
    [AttributeUsage(AttributeTargets.All)]
    public class InternalCodeAttribute : Attribute
    {
        public string Code { get; private set; }

        public InternalCodeAttribute()
        {
            Code = string.Empty;
        }

        public InternalCodeAttribute(string code)
        {
            Code = code;
        }
    }
}