namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class MachineParameterRepository : BaseRepository<MachineParameter>, IMachineParameterRepository
    {
        public MachineParameterRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {
        }

        public IEnumerable<MachineParameter> FindBy(Expression<Func<MachineParameter, bool>> predicate)
        {
            return UnitOfWork.Context.MachineParameters.Where(predicate);
        }

        public IEnumerable<MachineParameter> FindBy<TKey>(Expression<Func<MachineParameter, bool>> predicate, Expression<Func<MachineParameter, TKey>> orderBy)
        {
            return UnitOfWork.Context.MachineParameters
                    .Include(x => x.Links)
                    .Where(predicate)
                    .OrderBy(orderBy);
        }

        public Task<IEnumerable<MachineParameter>> FindByAsync(Expression<Func<MachineParameter, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public MachineParameter Get(long id)
        {
            return UnitOfWork.Context.MachineParameters.Include(x => x.Links).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<MachineParameter> GetAll()
        {
            return UnitOfWork.Context.MachineParameters;
        }

        public Task<MachineParameter> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public MachineParameter Get(string code)
        {
            return UnitOfWork.Context.MachineParameters
                        .Include(x => x.Links).SingleOrDefault(x => x.Code == code);
        }

        public MachineParameter Get(string code, ParameterCategoryEnum category)
        {
            return UnitOfWork.Context.MachineParameters.Include(x => x.Links).SingleOrDefault(x => x.Code == code &&
                                                                                        x.Category == category);
        }

        public Task<MachineParameter> GetAsync(string code)
        {
            return Task.Factory.StartNew(() => Get(code));
        }

        public Task<MachineParameter> GetAsync(string code, ParameterCategoryEnum category)
        {
            return Task.Factory.StartNew(() => Get(code, category));
        }

        public MachineParameterLink GetMachineParameterLink(long id)
        {
            return UnitOfWork.Context.MachineParameterLinks.SingleOrDefault(x => x.Id == id);
        }

        public Task<MachineParameterLink> GetMachineParameterLinkAsync(long id)
        {
            return Task.Factory.StartNew(() => GetMachineParameterLink(id));
        }

        public IEnumerable<MachineParameterLink> GetMachineParameterLinks(long parameterId)
        {
            return UnitOfWork.Context.MachineParameterLinks.Where(x => x.ParameterId == parameterId);
        }

        public Task<IEnumerable<MachineParameterLink>> GetMachineParameterLinksAsync(long parameterId)
        {
            return Task.Factory.StartNew(() => GetMachineParameterLinks(parameterId));
        }

        /// <summary>
        /// Async Add new parameter
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<MachineParameter> AddAsync(MachineParameter machineParameter)
        {
            return Task.Factory.StartNew(() => Add(machineParameter));
        }

        /// <summary>
        /// Add new parameter
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MachineParameter Add(MachineParameter entity)
        {
            return UnitOfWork.Context.MachineParameters.Add(entity).Entity;
        }

        public bool Update(MachineParameter entity)
        {
            UnitOfWork.Context.SetEntity(entity, EntityState.Modified);
            return true;
        }

        /// <summary>
        /// Get Parameter with CNC Links List 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MachineParameter> GetMachineParameterWithLinks()
        {
            return UnitOfWork.Context.MachineParameters.Include(x => x.Links)
                        .Where(x => x.Links.Any());
        }

        /// <summary>
        /// Cancellazione Gruppi non associati a parametri
        /// </summary>
        /// <param name="groupIndexes"></param>
        /// <param name="paramIndexes"></param>
        public void CleanUp(HashSet<long> parametersToKeep, HashSet<long> parameterLinksToKeep)
        {
            var parametersToDelete = UnitOfWork.Context.MachineParameters
                            .Where(parameter => !parametersToKeep.Contains(parameter.Id));

            foreach (var parameterToDelete in parametersToDelete)
            {
                UnitOfWork.Context.SetEntity(parameterToDelete, EntityState.Deleted);
            }

            var parameterLinksToDelete = UnitOfWork.Context.MachineParameterLinks
                            .Where(parameter => !parameterLinksToKeep.Contains(parameter.Id));

            foreach (var linkToDelete in parameterLinksToDelete)
            {
                UnitOfWork.Context.SetEntity(linkToDelete, EntityState.Deleted);
            }
        }

        /// <summary>
        /// Remove parameter
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(MachineParameter entity)
        {
            if (entity != null)
            {
                UnitOfWork.Context.SetEntity(entity, EntityState.Deleted);
                var links = UnitOfWork.Context.MachineParameterLinks.Where(x => x.ParameterId == entity.Id);
                links.ForEach(l => UnitOfWork.Context.SetEntity(l, EntityState.Deleted));
            }
        }

        public IEnumerable<MachineParameterLink> FindLinkBy(Expression<Func<MachineParameterLink, bool>> predicate)
        {
            return UnitOfWork.Context.MachineParameterLinks.Where(predicate);
        }

        public MachineParameterLink AddOrUpdateLink(MachineParameterLink machineParameterLink)
        {
            var link = UnitOfWork.Context.MachineParameterLinks
                .SingleOrDefault(x => x.ParameterId == machineParameterLink.ParameterId
                    && x.LinkId == machineParameterLink.LinkId);

            if (link != null)
            {
                link.CncTypeId = machineParameterLink.CncTypeId;
                link.Variable = machineParameterLink.Variable;
            }


            if (link == null)
            {
                link = UnitOfWork.Context.MachineParameterLinks.Add(machineParameterLink).Entity;
            }

            return link;
        }

        public void RemoveLinks(Expression<Func<MachineParameterLink, bool>> predicate)
        {
            foreach (var link in UnitOfWork.Context.MachineParameterLinks.Where(predicate))
            {
                UnitOfWork.Context.SetEntity(link, EntityState.Deleted);
            }
        }

        #region < Bulk operations >
        /// <summary>
        /// Bulk updates for exiting parameters
        /// </summary>
        /// <param name="parameters"></param>
        public void UpdateBulk(IEnumerable<MachineParameter> parameters)
        {
            StringBuilder updateQuery = new();

            foreach(var parameter in parameters)
            {
                updateQuery.Append($"UPDATE machineparameter SET Value = {parameter.Value}, `MinimumValue` = {parameter.MinimumValue},`MaximumValue` = {parameter.MaximumValue} WHERE Id = {parameter.Id};");
            }

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(updateQuery.ToString());
        }

        /// <summary>
        /// Bulk Insert
        /// </summary>
        /// <param name="parameters"></param>
        public int BulkInsert(IEnumerable<MachineParameter> parameters)
        {
            StringBuilder insertQuery = new($"INSERT INTO `machineparameter` (`Code`, `CategoryId`, `DescriptionLocalizationKey`, `HelpLocalizationKey`, `DefaultValue`, `Value`, `MinimumValue`,`MaximumValue`, `DataFormatId`, `ImageCode`, `IconCode`,`ProtectionLevel` ) VALUES ");

            foreach (var item in parameters)
            {
                insertQuery.Append($"('{item.Code}',{(int)item.Category}, '{item.DescriptionLocalizationKey}', '{item.HelpLocalizationKey}', {item.DefaultValue}, {item.Value}, {item.MinimumValue}, {item.MaximumValue}, {item.DataFormatId}, '{item.ImageCode}', '{item.IconCode}','{item.ProtectionLevel}' ),");
            }

            insertQuery.Length -= 1;

            return UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
        }

        public void BulkUpdate(IEnumerable<MachineParameter> parameters)
        {
            throw new NotImplementedException();
        }

        MachineParameter IRepository<MachineParameter, IMachineManagentDatabaseContext>.Update(MachineParameter entity)
        {
            throw new NotImplementedException();
        }

        public int BatchInsert(IEnumerable<MachineParameter> items)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
