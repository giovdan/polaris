namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    

    public class EFAttributeValueRepository : BaseRepository<AttributeValue>, IEFAttributeValueRepository
    {
        public EFAttributeValueRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public int BulkInsert(IEnumerable<AttributeValue> items)
        {
            try
            {
                StringBuilder insertQuery = new($"REPLACE INTO `AttributeValue` (`EntityId`, `AttributeDefinitionId`, `Value`, `TextValue`) VALUES ");

                foreach (var item in items)
                {
                    insertQuery.Append($"({item.EntityId},{item.AttributeDefinitionId}, {item.Value ?? 0}, '{item.TextValue}'),");
                }

                insertQuery.Length -= 1;

                var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Remove(AttributeValue entity)
        {
            UnitOfWork.Context.AttributeValues.Remove(entity);
        }

        public AttributeValue Update(AttributeValue entity)
        {
            return UnitOfWork.Context.AttributeValues.Update(entity).Entity;
        }

        public AttributeValue Add(AttributeValue entity)
        {
            try
            {
                return UnitOfWork.Context.AttributeValues.Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        public Task<AttributeValue> AddAsync(AttributeValue entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<AttributeValue> items)
        {
            UnitOfWork.Context.AttributeValues.AddRange(items);
            return items.Count();
        }

        public IEnumerable<AttributeValue> FindBy(Expression<Func<AttributeValue, bool>> predicate)
        {
            return UnitOfWork.Context.AttributeValues
                        .Include(a => a.AttributeDefinition)
                        .Include(a => a.Entity)
                        .Where(predicate);
        }

        public Task<IEnumerable<AttributeValue>> FindByAsync(Expression<Func<AttributeValue, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public IEnumerable<AttributeValue> GetAll()
        {
            return UnitOfWork.Context.AttributeValues.AsEnumerable();
        }

        public AttributeValue Get(long id)
        {
            return UnitOfWork.Context.AttributeValues
                        .Include(a => a.AttributeDefinition)
                        .SingleOrDefault(item => item.Id == id);
        }

        public Task<AttributeValue> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public int BulkUpdate(IEnumerable<AttributeValue> attributeValues)
        {
            StringBuilder updateQuery = new();

            foreach (var item in attributeValues)
            {
                updateQuery.Append($"UPDATE `AttributeValue` SET Value = {item.Value ?? 0}, TextValue = '{item.TextValue}', UpdatedOn = CURRENT_TIMESTAMP() WHERE Id = {item.Id};");
            }

            updateQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(updateQuery.ToString());
            return result;
        }

        public void BatchUpdate(IEnumerable<AttributeValue> attributeValues)
        {
            UnitOfWork.Context.AttributeValues.UpdateRange(attributeValues);
        }
    }
}
