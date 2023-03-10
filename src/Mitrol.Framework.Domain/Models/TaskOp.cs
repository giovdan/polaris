
namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Bus;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class OperationInfo
    {
        [JsonProperty("Identifier")]
        // Identificativo dell'oggetto da esportare(nel caso di import sarà il nome del file, nel caso di export sarà l'Id dell'oggetto)
        public object Identifier { get; set; }

        [JsonProperty("Status")]
        // Stato dell'operazione  
        public GenericEventStatusEnum Status { get; private set; } = GenericEventStatusEnum.NotActive;

        [JsonProperty("StatusLocalizationKey")]
        // Identificativo del task avviato per l'import
        public string StatusLocalizationKey { get; set; } = string.Empty;

        [JsonIgnore]
        public object Content { get; set; }

        private readonly object lockStatus = new object();

        // Il cambiamento di stato è permesso solo in determinate condizioni
        // da "Invalid" vado in "Waiting"
        // da "Waiting" vado in "Running" con esecuzione del task
        //              vado in "Aborting" con interruzione manuale del task
        // da "Running" vado in "Aborting" con interruzione manuale del task
        //              vado in "Completed" con esecuzione completata del task
        //              vado in "Failed" con esecuzione fallita del task
        // da "Aborting" vado in "Aborted" con interruzione manuale completata del task

        public void SetStatus(GenericEventStatusEnum newstatus)
        {
            lock (lockStatus)
            {
                switch (Status)
                {
                    case GenericEventStatusEnum.NotActive:
                        if (newstatus == GenericEventStatusEnum.Waiting)
                            Status = newstatus;
                        break;
                    case GenericEventStatusEnum.Waiting:
                        if (newstatus == GenericEventStatusEnum.Running
                            || newstatus == GenericEventStatusEnum.Aborting
                            || newstatus == GenericEventStatusEnum.Failed)
                            Status = newstatus;
                        break;

                    case GenericEventStatusEnum.Running:
                        if (newstatus == GenericEventStatusEnum.Completed
                            || newstatus == GenericEventStatusEnum.Failed
                            || newstatus == GenericEventStatusEnum.Aborting)
                            Status = newstatus;
                        break;

                    case GenericEventStatusEnum.Aborting:
                        if (newstatus == GenericEventStatusEnum.Aborted)
                            Status = newstatus;
                        break;
                    default:
                        break;

                }
            }
        }
    }
    public class TaskInfo
    {
        [JsonProperty("Guid")]
        // Identificativo del task 
        public string Guid { get; set; }

        [JsonProperty("Elements")]
        public List<OperationInfo> Operations { get; set; }
    }
}
