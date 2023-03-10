namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;


    public interface IBackgroundTaskQueue
    {
        void QueueWorkItem(Func<IServiceScope, CancellationToken, Task> workItem);
        Task<Func<IServiceScope, CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

    public interface ICancellableBackgroundTaskQueue
    {
        //TaskInfo QueueWorkItem(Func<IServiceScope, IdentifiedTask, Task> workItem,List<OperationInfo> operations);

        Task<IdentifiedTask> DequeueAsync(CancellationToken cancellationToken);

        void CancellationRequest(List<string> guids);

    }
}
