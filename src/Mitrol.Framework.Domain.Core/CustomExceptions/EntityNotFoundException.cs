using Mitrol.Framework.Domain.Core.Enums;
using Mitrol.Framework.Domain.Enums;
using System;

namespace Mitrol.Framework.Domain.Core.CustomExceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
            EventContext = EventContextEnum.InternalServerError;
        }

        public EntityNotFoundException(string entityCode)
        {
            EntityCode = entityCode;
        }

        public EntityNotFoundException(string entityCode, EventContextEnum eventContext) : this(entityCode)
        {
            EventContext = eventContext;
        }

        public string EntityCode { get; set; }
        public EventContextEnum EventContext { get; set; }
    }

    public class GroupNotFoundException : EntityNotFoundException
    {
        public GroupNotFoundException()
        {
            EventContext = EventContextEnum.InternalServerError;
        }

        public GroupNotFoundException(string entityCode, long groupID)
            : base(entityCode)
        {
            GroupID = groupID;
        }

        public GroupNotFoundException(string entityCode, long groupID, EventContextEnum eventContext)
                : base(entityCode, eventContext)
        {
            GroupID = groupID;
        }

        public long GroupID { get; set; }
    }
}