namespace Mitrol.Framework.Domain.Bus.Events
{
    using System;
    using System.Collections.Generic;

    public interface IProgressEventCollection : IEnumerable<ProgressEvent>
    {
        int Count { get; }

        Action OnAdd { get; set; }

        void Add(ProgressEvent progressEvent);
    }
}