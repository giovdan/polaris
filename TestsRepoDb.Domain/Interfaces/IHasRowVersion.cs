﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoDbVsEF.Domain.Interfaces
{
    public interface IHasRowVersion
    {
        string RowVersion { get; set; }
    }
}