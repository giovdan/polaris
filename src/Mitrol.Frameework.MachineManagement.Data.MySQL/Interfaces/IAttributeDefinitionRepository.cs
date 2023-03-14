﻿namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.Domain.Core.Interfaces;

    public interface IAttributeDefinitionRepository : IRepository<AttributeDefinition, IEFDatabaseContext>
    {
        
    }
}