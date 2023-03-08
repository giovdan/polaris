namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class for Import Result
    /// </summary>
    public class ImportResult<T>
    {
        [JsonProperty("ProcessingResult")]
        public ImportProcessingResultEnum ProcessingResult { get; set; }
        [JsonProperty("Result")]
        public T Result { get; set; }
        [JsonProperty("ErrorDetails")]
        public List<ErrorDetail> ErrorDetails { get; set; }

        public ImportResult()
        {
            ErrorDetails = new List<ErrorDetail>();
            ProcessingResult = ImportProcessingResultEnum.NoOp;
        }

        public ImportResult(T result, string errorCode):base()
        {
            Result = result;
            ProcessingResult = ImportProcessingResultEnum.Failed;
            ErrorDetails.Add(new ErrorDetail(errorCode));
        }

    }
}
