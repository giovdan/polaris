namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Mitrol.Framework.Domain.Core.Interfaces;

    // TODO: @Michele - Questo repository non usa correttamente il pattern asincrono
    // e non utilizza il TaskProcessor per l'accesso concorrenziale
    public class QuantityBackLogRepository : BaseRepository<QuantityBackLog>, IQuantityBackLogRepository
    {
        public QuantityBackLogRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {
        }

        public QuantityBackLog Add(QuantityBackLog entity)
        {
            return UnitOfWork.Context.QuantityBackLogs.Add(entity).Entity;
        }

        public Task<QuantityBackLog> AddAsync(QuantityBackLog entity)
        {
            return Task.FromResult(Add(entity));
        }

        public int BatchInsert(IEnumerable<QuantityBackLog> items)
        {
            throw new NotImplementedException();
        }

        public int BulkInsert(IEnumerable<QuantityBackLog> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuantityBackLog> FindBy(Expression<Func<QuantityBackLog, bool>> predicate)
        {
            return UnitOfWork.Context.QuantityBackLogs.Where(predicate);
        }

        public IEnumerable<QuantityBackLog> FindBy<TKey>(Expression<Func<QuantityBackLog, bool>> predicate, Expression<Func<QuantityBackLog, TKey>> orderBy)
        {
            return UnitOfWork.Context.QuantityBackLogs.Where(predicate).OrderBy(orderBy);
        }

        public Task<IEnumerable<QuantityBackLog>> FindByAsync(
                    Expression<Func<QuantityBackLog, bool>> predicate)
        {
            return Task.FromResult(FindBy(predicate));
        }

        public QuantityBackLog Get(long id) => UnitOfWork.Context.QuantityBackLogs.SingleOrDefault(x => x.Id == id);

        public IEnumerable<QuantityBackLog> GetAll() => UnitOfWork.Context.QuantityBackLogs;

        public Task<QuantityBackLog> GetAsync(long id) => Task.FromResult(Get(id));

        public void Remove(QuantityBackLog entity)
        {
            UnitOfWork.Context.SetEntity(entity, Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        public Result Remove(Expression<Func<QuantityBackLog, bool>> predicate)
        {
            return RemoveRange(predicate);
        }

        public Result RemoveRange(Expression<Func<QuantityBackLog, bool>> predicate)
        {
            var quantityBackLogs = UnitOfWork.Context.QuantityBackLogs.Where(predicate);

            UnitOfWork.Context.QuantityBackLogs.RemoveRange(quantityBackLogs);
            return Result.Ok();
        }

        public QuantityBackLog Update(QuantityBackLog entity)
        {
            UnitOfWork.Context.SetEntity(entity, Microsoft.EntityFrameworkCore.EntityState.Modified);
            return entity;
        }

    }
}
