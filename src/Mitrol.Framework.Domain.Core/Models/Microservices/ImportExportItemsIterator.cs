namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Bus;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ImportExportItemsEnumerator
    {
        //private int? _currentElement = null;
        //private List<OperationInfo> _objectToExports;
        //private IEventHubClient _progressEventHubClient;
        //public ImportExportItemsEnumerator(List<OperationInfo> objectToExports
        //    , IEventHubClient progressEventHubClient)
        //{
        //    _objectToExports = objectToExports;
        //    _progressEventHubClient = progressEventHubClient;
        //}


        //public OperationInfo GetNextObjectToExport()
        //{
        //    if (!_currentElement.HasValue)
        //        _currentElement = 0;
        //    else
        //    {
        //        _currentElement++;
        //        if (_currentElement >= _objectToExports.Count)
        //        {
        //            return default; // Ho completato l'esecuzione di tutti gli oggetti da esportare.
        //        }
        //    }

        //    var objectToExport = _objectToExports.ElementAt(_currentElement.Value);
        //    if (objectToExport.Status == GenericEventStatusEnum.Waiting) 
        //        objectToExport.SetStatus(GenericEventStatusEnum.Running);
        //    return objectToExport;         
        //}

        //public void CurrentObjectAborted()
        //{
        //    //scorro tutta la lista degli oggetti da esportare e li metto in stato di abort. 
        //    while (_currentElement < _objectToExports.Count)
        //    {
        //        var objectToExport = _objectToExports.ElementAt(_currentElement.Value);
        //        if (objectToExport.Status != GenericEventStatusEnum.Completed && objectToExport.Status != GenericEventStatusEnum.Failed)
        //        {// Evento di progressione della fase di Export
        //            objectToExport.SetStatus(GenericEventStatusEnum.Aborted);
        //            _progressEventHubClient.StatusEvent(new StatusEvent(status: GenericEventStatusEnum.Aborted
        //           , localizationKey: ErrorCodesEnum.ERR_GEN009.ToString()
        //           , Id: objectToExport.Identifier));
        //        }
        //        _currentElement++;
        //    }
        //}

        //public void CurrentObjectCompleted()
        //{
        //    var objectToExport = _objectToExports.ElementAt(_currentElement.Value);       
        //    objectToExport.SetStatus(GenericEventStatusEnum.Completed);
        //    //Notifico completamento export corrente (Completed)
        //    _progressEventHubClient.StatusEvent(new StatusEvent(status: objectToExport.Status
        //                , Id: _objectToExports.ElementAt(_currentElement.Value).Identifier));
        //}

        //public void CurrentObjectFailed(string error)
        //{
        //    var objectToExport = _objectToExports.ElementAt(_currentElement.Value);
        //    objectToExport.SetStatus(GenericEventStatusEnum.Failed);
        //    // Evento di progressione della fase di Export
        //    _progressEventHubClient.StatusEvent(new StatusEvent(status: objectToExport.Status
        //        , localizationKey: error
        //        , Id: objectToExport.Identifier));
        //}
    }
}
