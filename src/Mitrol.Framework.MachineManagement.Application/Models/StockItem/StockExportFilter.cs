

namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class StockExportFilter : BaseFilterExport, IFilterExportAdapter<EntityAttribute>
    {
        public Expression<Func<EntityAttribute, bool>> GetFilter()
        {
            var stockEntityTypes = ParentTypeEnum.StockItem.GetEntityTypes();

            Expression<Func<EntityAttribute, bool>> predicate = x => stockEntityTypes.Contains(x.EntityTypeId);

            if (Indexes!=null && Indexes.Length > 0)
            {
                Expression<Func<EntityAttribute, bool>> predicateIndex = null;
                for (int index = 0; index < Indexes.Length; index++)
                {
                    long stockIndex = Indexes[index];
                    if (predicateIndex == null)
                        predicateIndex = x => x.EntityId == stockIndex;
                    else
                        predicateIndex = predicateIndex.OrElse(x => x.EntityId == stockIndex);

                }

                if (predicateIndex != null)
                {
                    predicate = predicate.AndAlso(predicateIndex);
                }
            }
            
            return predicate;
        }
    }
}
