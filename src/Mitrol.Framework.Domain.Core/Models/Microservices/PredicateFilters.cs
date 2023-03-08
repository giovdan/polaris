namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using System;
    using System.Linq.Expressions;

    public class PredicateFilters<T> where T : class
    {
        public Func<T, DateTime> GroupBy { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
    }
}
