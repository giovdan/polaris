namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;
    using System.Collections.Generic;

    [Obsolete]
    public interface IEntityRulesHandler
    {
        void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo);

        IEnumerable<T> HandleAll<T>(IEnumerable<T> attributes);
    }

    public interface IEntityRulesHandler<TModel>
    {
        TModel Handle(TModel model);
    }

    public interface IEntityRulesHandler<TModel, TData>
    {
        //non usata per ora.
        void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo);
        TModel Handle(TModel model);
        TModel CreateModel(TData creationData, MeasurementSystemEnum conversionSystem, IUserSession userSession);

        TModel UpdateModel(TModel model, MeasurementSystemEnum conversionSystem, IUserSession userSession);
    }
}
