﻿namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IEntityBaseValidator<TModel> where TModel:class
    {
        void Init(IServiceFactory serviceFactory, Dictionary<DatabaseDisplayNameEnum, object> additionalInfos);
        void Init(Dictionary<DatabaseDisplayNameEnum, object> additionalInfos);
        Result Validate(TModel model);
    }
}
