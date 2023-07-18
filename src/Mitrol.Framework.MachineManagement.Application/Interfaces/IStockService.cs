using Mitrol.Framework.Domain.Core.Interfaces;
using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.Domain.Models;
using Mitrol.Framework.Domain.Production.Models;
using Mitrol.Framework.MachineManagement.Application.Models;
using Mitrol.Framework.MachineManagement.Application.Models.Production;
using Mitrol.Framework.MachineManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    public interface IStockService: IMachineManagementService, IBootableService
    {
        Dictionary<string, string> GetFilteredMaterialCodes();
        Dictionary<string, string> GetFilteredProfileCodes(ProfileTypeEnum profileTypeId);
        Dictionary<string, string> GetFilteredThickness(MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        Result<long> CreateStockItem(StockItemToAdd stockItem);
        IEnumerable<StockListItem> GetStockItemList(MeasurementSystemEnum conversionSystem);
        Task<IEnumerable<StockListItem>> GetStockItemListAsync(MeasurementSystemEnum conversionSystem);
        Result<StockDetailItem> GetStockItem(StockItemFilter filter);
        Result<Entity> GetStockItemByHashCode(string hashcode);
        Result RemoveStockItem(long id);
        Result RemoveStockItems(long[] ids);
        Task<IEnumerable<LastUpdatedStockItem>> GetLastUpdatedStockItemsAsync(MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        Result UpdateStockItem(StockItemToUpdate stockItem);
        IEnumerable<AttributeDetailItem> GetStockItemIdentifiers();
        Task<IEnumerable<BaseGroupItem<AttributeDetailItem>>> GetStockAttributeDefinitionsAsync(MeasurementSystemEnum conversionSystem, ProfileTypeEnum profileTypeId);
        Task<IEnumerable<InfoItem<string>>> GetStockProfileTypesAsync();
        Task<IEnumerable<BaseInfoItem<int, string>>> GetStockTypesAsync();
        Task<IEnumerable<SubFilterItem>> GetStockSubFiltersAsync(ProfileTypeFilters filters);
    }
}
