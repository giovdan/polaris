namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadOnlyRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindBy<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy);

        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        T Get(long id);

        IEnumerable<T> GetAll();

        Task<T> GetAsync(long id);
    }
}