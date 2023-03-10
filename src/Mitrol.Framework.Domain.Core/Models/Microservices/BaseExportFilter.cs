
namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseFilterExport : IBaseFilterExport
    {
        [JsonProperty("Indexes")]
        public long[] Indexes { get; set; }

        //public virtual void ConvertFilter(ImportExportFilter filter)
        //{
        //    if (filter.ObjectIdentifiers != null && filter.ObjectIdentifiers.Count > 0)
        //    {
        //        Indexes = new long[filter.ObjectIdentifiers.Count];

        //        for (int index = 0; index < filter.ObjectIdentifiers.Count; index++)
        //            Indexes[index] = Convert.ToInt64(filter.ObjectIdentifiers[index]);
        //    }
        //}
    }

    public static class BaseFilterExportExtension
    {
        //public static Result<List<IExportObject>> OrderByUserRequest(this BaseFilterExport filter,List<ExportImportObject> objectToExports)
        //{
        //    var orderedObjects = objectToExports;
        //    var ids = objectToExports.OrderBy(o => o.Id).Select(o => o.Id).ToList();
        //    var idFilters = filter.Indexes.OrderBy(o => o).ToList();
        //    if (ids.SequenceEqual(idFilters))   //utilizzo dei soli Indexes
        //    {
        //        orderedObjects = new List<ExportImportObject>();
        //        foreach (var id in filter.Indexes)
        //        {
        //            orderedObjects.AddRange(objectToExports.Where(o => o.Id == id));
        //        }
        //        return Result.Ok(orderedObjects.ToList<IExportObject>());
        //    }

        //    return Result.Fail<List<IExportObject>>(ErrorCodesEnum.ERR_GEN002.ToString());
        //}
    }

}
