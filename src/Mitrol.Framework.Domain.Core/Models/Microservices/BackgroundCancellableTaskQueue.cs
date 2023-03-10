
namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;


    public class BackgroundCancellableTaskQueue : Observable, ICancellableBackgroundTaskQueue
    {

        private readonly ConcurrentQueue<IdentifiedTask> _workItems = new ConcurrentQueue<IdentifiedTask>();
        private IdentifiedTask _workingTask;

        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        private readonly object lockCancellationRequest = new object();

        protected override void DisposeManaged()
        {
            _signal.Dispose();
            base.DisposeManaged();
        }

        //public TaskInfo QueueWorkItem(Func<IServiceScope, IdentifiedTask, Task> workItem,List<OperationInfo> operations)
        //{
        //    lock (lockCancellationRequest)
        //    {
        //        if (workItem == null)
        //        {
        //            throw new ArgumentNullException(nameof(workItem));
        //        }
        //        var workItemToAdd = new IdentifiedTask(workItem,operations);
        //        var guid = workItemToAdd.Identifier;
        //        _workItems.Enqueue(workItemToAdd);
        //        _signal.Release();
        //        var tsk = new TaskInfo() { Guid = workItemToAdd.Identifier, Operations = operations }; 
        //        return tsk;
        //    }
        //}

        public async Task<IdentifiedTask> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            lock (lockCancellationRequest)
            {
                if (_workItems.TryDequeue(out var workItem))
                {
                    workItem.CumulativeCancellationToken.SetShutDownCancellationToken(cancellationToken);
                    _workingTask = workItem;
                }
                return workItem;
            }
        }

        public void CancellationRequest(List<string> guids)
        {
            lock (lockCancellationRequest)
            {
                foreach (string guid in guids) //ciclo su tutti i guid forniti che vanno interrotti
                {
                    //verificare che il guid sia lo stesso del processo processato
                    if (_workingTask!=null&& _workingTask.Identifier.CompareTo(guid) == 0)
                        _workingTask.CancellationRequired();
                    else
                    {
                        //cerco nella coda il workItem che voglio interrompere
                        var workItem = _workItems.SingleOrDefault(item => item.Identifier.CompareTo(guid) == 0);
                        if (workItem != null)
                            workItem.CancellationRequired();
                    }
                }
            }
        }
    }
}
