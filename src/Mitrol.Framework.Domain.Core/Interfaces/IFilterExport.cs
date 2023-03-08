

namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Linq.Expressions;

    public interface IFilterExportAdapter<T>
    {
        Expression<Func<T, bool>> GetFilter();    
    }

    public interface IBaseFilterExport
    {
        long[] Indexes { get; set; }
        void ConvertFilter(ImportExportFilter filter);
    }

}
