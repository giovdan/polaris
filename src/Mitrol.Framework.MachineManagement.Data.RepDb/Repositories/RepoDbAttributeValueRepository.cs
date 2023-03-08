using RepoDb;
namespace Mitrol.Framework.MachineManagement.Data.RepDb.Repositories
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces;
    using RepoDbVsEF.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    

    public class RepoDbAttributeValueRepository : InternalBaseRepository<AttributeValue>, IRepoDbAttributeValueRepository
    {
        public RepoDbAttributeValueRepository(string connectionString): base(connectionString)
        {

        }

        public int BulkInsert(IEnumerable<AttributeValue> items)
        {
            StringBuilder insertQuery = new($"REPLACE INTO `AttributeValue` (`EntityId`, `AttributeDefinitionId`, `Value`, `TextValue`) VALUES ");

            foreach (var item in items)
            {
                insertQuery.Append($"({item.EntityId},{item.AttributeDefinitionId}, {item.Value ?? 0}, '{item.TextValue}'),");
            }

            insertQuery.Length -= 1;

            return UnitOfWork.Context.Connection.ExecuteNonQuery(insertQuery.ToString());
        }

        public new int BatchInsert(IEnumerable<AttributeValue> items)
        {
            return UnitOfWork.Context.Connection.InsertAll(items, transaction: UnitOfWork.CurrentTransaction);
        }

        public void Remove(AttributeValue entity)
        {
            UnitOfWork.Context.Connection.Delete(entity);
        }

        public AttributeValue Update(AttributeValue entity)
        {
            var result = UnitOfWork.Context.Connection.Update(entity);
            return result > 0 ? Get(entity.Id): null;
        }

        public int BulkUpdate(IEnumerable<AttributeValue> attributeValues)
        {
            StringBuilder insertQuery = new();

            foreach (var item in attributeValues)
            {
                insertQuery.Append($"UPDATE `AttributeValue` SET Value = {item.Value ?? 0}, TextValue = '{item.TextValue}' WHERE Id = {item.Id};");
            }

            insertQuery.Length -= 1;

            return UnitOfWork.Context.Connection.ExecuteNonQuery(insertQuery.ToString());
        }

        public int BatchUpdate(IEnumerable<AttributeValue> attributeValues)
        {
            attributeValues.SetAuditableFields(UnitOfWork.UserSession?.Username ?? "MITROL");
            return UnitOfWork.Context.Connection.UpdateAll(attributeValues,transaction: UnitOfWork.CurrentTransaction);
        }

        public new IEnumerable<AttributeValue> FindBy(Expression<Func<AttributeValue, bool>> predicate)
        {
            return UnitOfWork.Context.Connection.Query(predicate)
                        .Select(a => {
                            a.AttributeDefinition = UnitOfWork.Context.Connection.ExecuteQuery<AttributeDefinition>($"SELECT * FROM attributedefinition WHERE Id = {a.AttributeDefinitionId}")
                                        .SingleOrDefault();
                            return a;
                        });
        }
    }
}
