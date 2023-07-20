namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
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
    public interface IEntityWithAttributesValidator<TModel> where TModel: class
                                , IHasAttributes
    {
        Result ValidateAttributes(IEnumerable<AttributeDetailItem> attributes);
        Result ValidateIdentifiers(TModel model);
        void Init(IServiceFactory serviceFactory, Dictionary<DatabaseDisplayNameEnum, object> additionalInfos);
        void Init(Dictionary<DatabaseDisplayNameEnum, object> additionalInfos);
        Result Validate(TModel model);

    }
}
