namespace Mitrol.Framework.Domain.Core.CustomExceptions
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;

    public class BusinessValidationException : Exception
    {
        public EventContextEnum EventContext { get; set; }
        public List<ErrorDetail> ErrorDetails { get; internal set; }

        public BusinessValidationException(EventContextEnum eventContext)
        {
            EventContext = eventContext;
        }

        public BusinessValidationException(List<ErrorDetail> errorDetails, EventContextEnum eventContext = EventContextEnum.InternalServerError):
            this(eventContext)
        {
            ErrorDetails = errorDetails;
        }

        public BusinessValidationException(ErrorDetail errorDetail, EventContextEnum eventContext = EventContextEnum.InternalServerError):
            this(eventContext)
        {
            ErrorDetails = new List<ErrorDetail>();
            ErrorDetails.Add(errorDetail);
        }

        public BusinessValidationException(string errorCode, EventContextEnum eventContext = EventContextEnum.InternalServerError):
            this(eventContext)
        {
            ErrorDetails = new List<ErrorDetail>();
            ErrorDetails.Add(new ErrorDetail(errorCode));
        }
    }
}
