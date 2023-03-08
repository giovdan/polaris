namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private IUserSession _session;

        protected override void DisposeManaged()
        {
            Context?.Dispose();
            base.DisposeManaged();
        }

        public UnitOfWork(IDatabaseContextFactory databaseContextFactory)
        {
            Session = NullUserSession.Instance;
            Id = CoreExtensions.GenerateGUID();
            Context = databaseContextFactory.Create();
        }

        public IDatabaseContext Context { get; }

        public bool IsFinished => IsDisposed;

        public string Id { get; private set; }

        public IUserSession Session
        {
            get => _session;
            set {
                _session = value;
                Context?.SetSession(Session);
            }
        }

        public int Commit()
        {
            if (IsFinished)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            try
            {
                int result = -1;
                result = Context.SaveChanges();
                return result;
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
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<int> CommitAsync()
        {
            return Task.Factory.StartNew(() => Commit());
        }
    }
}