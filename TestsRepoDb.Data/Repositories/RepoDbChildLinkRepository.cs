namespace RepoDbVsEF.RepoDb.Data.Repositories
{
    using global::RepoDb;
    using RepoDbVsEF.Data.Repositories;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.RepoDb.Data.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class RepoDbChildLinkRepository:  InternalBaseRepository<ChildLink>, IChildLinkRepository
    {
        public RepoDbChildLinkRepository(string connectionString): base(connectionString)
        {

        }

        public int BulkInsert(IEnumerable<ChildLink> childLinks)
        {
            StringBuilder insertQuery = new($"INSERT INTO `ChildLink` (`ParentId`, `ChildId`, `Level`, `RowNumber`) VALUES ");

            foreach (var item in childLinks)
            {
                insertQuery.Append($"({item.ParentId},{item.ChildId},{item.Level},{item.RowNumber}");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Connection.ExecuteNonQuery(insertQuery.ToString());
            return result;
        }

        public void Remove(ChildLink entity)
        {
            Delete(entity, transaction: UnitOfWork.CurrentTransaction);
        }

        public void RemoveLinks(long parentId)
        {
            UnitOfWork.Context.Connection.ExecuteNonQuery($"DELETE FROM ChildLink WHERE ParentId = {parentId}");
        }

        public ChildLink Update(ChildLink entity)
        {
            var result = Update(entity, transaction: UnitOfWork.CurrentTransaction);
            return result > 0 ? entity : null;
        }
    }
}
