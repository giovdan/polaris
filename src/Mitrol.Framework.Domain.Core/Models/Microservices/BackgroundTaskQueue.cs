namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;


    public class BackgroundTaskQueue : Observable, IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<IServiceScope, CancellationToken, Task>> _workItems = new();

        private readonly SemaphoreSlim _signal = new(0);

        protected override void DisposeManaged()
        {
            _workItems.Clear();
            _signal.Dispose();
            base.DisposeManaged();
        }

        public void QueueWorkItem(Func<IServiceScope, CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            
            if(!IsDisposed) _signal.Release();
        }

        public async Task<Func<IServiceScope, CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);
            return workItem;
        }
    }

    public class BackgroundTasksHostedService : BackgroundService
    {
        public BackgroundTasksHostedService(IBackgroundTaskQueue taskQueue, IServiceScopeFactory serviceScopeFactory)
        {
            TaskQueue = taskQueue;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public IBackgroundTaskQueue TaskQueue { get; }
        public IServiceScopeFactory ServiceScopeFactory { get; }


        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                using var scope = ServiceScopeFactory.CreateScope();

                try
                {
                    await workItem(scope, cancellationToken);
                }
                catch (Exception) { /* do not let Exception exceptions to propagate...*/ }
            }
        }
    }

    public class BackgroundIdentifiedTasksHostedService : BackgroundService
    {
        public BackgroundIdentifiedTasksHostedService(ICancellableBackgroundTaskQueue taskQueue, IServiceScopeFactory serviceScopeFactory)
        {
            TaskQueue = taskQueue;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public ICancellableBackgroundTaskQueue TaskQueue { get; }
        public IServiceScopeFactory ServiceScopeFactory { get; }


        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var _currentWorkingTask = await TaskQueue.DequeueAsync(cancellationToken);

                //se _currentWorkingTask==null allora vuol dire che non ci sono task perchè quelli che c'erano sono stati già abortiti
                if (_currentWorkingTask != null)
                {
                    using (var scope = ServiceScopeFactory.CreateScope())
                    {
                        try
                        {             
                            await _currentWorkingTask.WorkItem(scope, _currentWorkingTask);
                        }
                        catch (Exception ex) { /* do not let Exception exceptions to propagate...*/ }
                    }
                }
            }
        }
    }
}
