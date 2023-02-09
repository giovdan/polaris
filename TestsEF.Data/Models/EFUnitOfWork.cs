﻿namespace RepoDbVsEF.EF.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Data;
    using RepoDbVsEF.EF.Data.Interfaces;
    using RepoDbVsEF.Domain.Interfaces;

    public class EFUnitOfWork : IUnitOfWork<IEFDatabaseContext>
    {
        public EFUnitOfWork(IDatabaseContextFactory databaseContextFactory)
        {
            UserSession = null;
            Id = Guid.NewGuid().ToString();
            Context = databaseContextFactory.Create() as IEFDatabaseContext;
        }

        public IEFDatabaseContext Context { get; set; }

        IUserSession _session;
        public IUserSession Session
        {
            get => _session;
            set
            {
                _session = value;
                Context?.SetSession(Session);
            }
        }


        public bool IsFinished { get; set; }

        public string Id { get; private set; }

        public IUserSession UserSession { get; set; }
        public IDbTransaction CurrentTransaction { get; set; }

        public void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            if (IsFinished)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            try
            {
                int result = -1;
                result = Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.Entries?.Count == 0)
                    throw;

                foreach (var entry in ex.Entries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            // added on client, non in store - store wins
                            entry.State = EntityState.Modified;
                            break;

                        case EntityState.Deleted:
                            //deleted on client, modified in store
                            entry.Reload();
                            entry.State = EntityState.Deleted;
                            break;

                        case EntityState.Modified:
                            PropertyValues currentValues = entry.CurrentValues.Clone();
                            //Modified on client, Modified in store
                            entry.Reload();
                            entry.State = EntityState.Modified;
                            entry.CurrentValues.SetValues(currentValues);

                            break;

                        default:
                            //For good luck
                            entry.Reload();
                            break;
                    }
                }

                int result = -1;
                result = Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
