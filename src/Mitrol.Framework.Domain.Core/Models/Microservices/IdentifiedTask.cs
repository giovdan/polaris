
namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Bus;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IdentifiedTask
    {
        public string Identifier { get; private set; }
        public Func<IServiceScope, IdentifiedTask, Task> WorkItem { get; set; }

        //se lo tolgo da qui non posso cancellare il task(la cancellazione avviene dalla )
        public CumulativeCancellationToken CumulativeCancellationToken { get; set; }

        public GenericEventStatusEnum Status { get; private set; }

        private readonly object lockStatus = new object();

        public List<OperationInfo> Operations { get; private set; }


        public IdentifiedTask(Func<IServiceScope,IdentifiedTask, Task> workItem, List<OperationInfo> opers)
        {
            WorkItem = workItem;
            Identifier = CoreExtensions.GenerateGUID();
            CumulativeCancellationToken = new CumulativeCancellationToken();
            Status = GenericEventStatusEnum.Waiting; //in attesa di esecuzione
            Operations = opers;
            foreach (var o in Operations)
                o.SetStatus(Status);
        }

        public void CancellationRequired()
        {
            lock (lockStatus)
            {
                if (Status == Bus.GenericEventStatusEnum.Running || Status == Bus.GenericEventStatusEnum.Waiting)
                {
                    Status = GenericEventStatusEnum.Aborting;
                    foreach (var o in Operations)
                        o.SetStatus(Status);
                    
                    CumulativeCancellationToken.CancellationRequested();
                }
            }
        }
    }
}
