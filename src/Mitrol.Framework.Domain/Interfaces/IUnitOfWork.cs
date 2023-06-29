﻿namespace Mitrol.Framework.Domain.Interfaces
{
    using System;
    using System.Data;

    public interface IUnitOfWork<T> : IDisposable
    {
        T Context { get; set; }
        void Commit();
        /// <summary>
        /// Indicates that work is completed (Commit for Dispose)
        /// </summary>
        bool IsFinished { get; }
        bool IsDisposed { get; }
        string Id { get; }
        IUserSession UserSession { get; set; }
        IDbTransaction CurrentTransaction { get; set; }
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollBackTransaction();
    }
}