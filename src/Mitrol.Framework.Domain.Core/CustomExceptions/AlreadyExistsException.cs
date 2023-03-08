using System;

namespace Mitrol.Framework.Domain.Core.CustomExceptions
{
    public class AlreadyExistsException<T> : Exception
    {
        public T Entity { get; internal set; }
        public AlreadyExistsException(T entity)
        {
            Entity = entity;
        }
    }
}