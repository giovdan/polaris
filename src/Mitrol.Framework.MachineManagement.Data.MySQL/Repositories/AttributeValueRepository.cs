namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    

    public class AttributeValueRepository : BaseRepository<AttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }


        public void Remove(AttributeValue entity)
        {
            UnitOfWork.Context.AttributeValues.Remove(entity);
        }

        public int Remove(Expression<Func<AttributeValue, bool>> predicate)
        {
            var attributeValues = UnitOfWork.Context.AttributeValues.Where(predicate);
            UnitOfWork.Context.AttributeValues.RemoveRange(attributeValues);
            return attributeValues.Count();
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

        public int BatchInsertOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues)
        {
            UnitOfWork.Context.AttributeOverrideValues.AddRange(attributeOverrideValues);
            return attributeOverrideValues.Count();
        }

        public void BatchUpdateOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues)
        {
            UnitOfWork.Context.AttributeOverrideValues.UpdateRange(attributeOverrideValues);
        }

        public IEnumerable<EntityStatusAttribute> GetEntityStatusAttributes(Expression<Func<EntityStatusAttribute, bool>> predicate)
        {
            return UnitOfWork.Context.EntityStatusAttributes.Where(predicate);
        }

        public IEnumerable<AttributeValue> FindBy(Expression<Func<AttributeValue, bool>> predicate)
        {
            var query = UnitOfWork.Context.AttributeValues
                        .Include(a => a.Entity)
                        .Include(a => a.AttributeDefinitionLink)
                        .ThenInclude(a => a.AttributeDefinition)
                        .Where(predicate);

            Debug.WriteLine(query.ToQueryString());

            return query;
        }

        public IEnumerable<EntityAttribute> FindEntityAttributes(Expression<Func<EntityAttribute, bool>> predicate)
        {
            var query = UnitOfWork.Context.EntityAttributes
                    .Where(predicate);

            Debug.WriteLine(query.ToQueryString());

            return query;
        }

        public IEnumerable<AttributeOverrideValue> FindAttributeOverridesBy(Expression<Func<AttributeOverrideValue, bool>> predicate)
        {
            return UnitOfWork.Context.AttributeOverrideValues
                .Include(aov => aov.AttributeValue)
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
                        .Include(a => a.AttributeDefinitionLink)
                        .ThenInclude(a => a.AttributeDefinition)
                        .SingleOrDefault(item => item.Id == id);
        }

        public Task<AttributeValue> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public int BulkInsert(IEnumerable<AttributeValue> attributeValues)
        {
            StringBuilder insertQuery = new();

            insertQuery.AppendLine("INSERT INTO attributevalue (AttributeDefinitionLinkId, DataFormatId, EntityId, Priority, Value, TextValue) VALUES");
            foreach(var attributeValue in attributeValues)
            {
                insertQuery.AppendLine($"({attributeValue.AttributeDefinitionLinkId}, '{attributeValue.DataFormatId}', {attributeValue.EntityId}, {attributeValue.Priority}, {attributeValue.Value}, '{attributeValue.TextValue}'),");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;
        }

        public int BulkInsertOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues)
        {
            StringBuilder insertQuery = new();

            insertQuery.AppendLine("INSERT INTO attributeoverridevalue (AttributeValueId, OverrideType, Value) VALUES");
            foreach (var attributeOverrideValue in attributeOverrideValues)
            {
                insertQuery.AppendLine($"({attributeOverrideValue.AttributeValueId}, {attributeOverrideValue.OverrideType}, {attributeOverrideValue.Value}),");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;
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

        public IEnumerable<AttributeValue> FindBy<TKey>(Expression<Func<AttributeValue, bool>> predicate, Expression<Func<AttributeValue, TKey>> orderBy)
        {
            return UnitOfWork.Context.AttributeValues
                    .Include(a => a.AttributeDefinitionLink)
                    .ThenInclude(a => a.AttributeDefinition)
                    .Include(a => a.Entity)
                    .Where(predicate)
                    .OrderBy(orderBy);
        }

        public AttributeOverrideValue GetOverrideValue(long attributeValueId)
        {
            return
                UnitOfWork.Context.AttributeOverrideValues.SingleOrDefault(x => x.AttributeValueId == attributeValueId);
        }
    }
}
