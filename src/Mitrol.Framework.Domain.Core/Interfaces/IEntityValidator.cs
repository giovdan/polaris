namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IHasAttributes
    {
        IEnumerable<AttributeDetailItem> Attributes { get; set; }
        IEnumerable<AttributeDetailItem> Identifiers { get; set; }
    }


    /// <summary>
    /// Interfaccia per gestire la validazione delle entità
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IEntityValidator<TModel>: IEntityBaseValidator<TModel> where TModel: class
                                , IHasAttributes
    {
        Result ValidateAttributes(IEnumerable<AttributeDetailItem> attributes);
        Result ValidateIdentifiers(TModel model);
    }
}
