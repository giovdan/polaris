﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitrol.Framework.Domain.Interfaces
{
    public interface IHasSessionId
    {
        string SessionId { get; set; }
    }
}
