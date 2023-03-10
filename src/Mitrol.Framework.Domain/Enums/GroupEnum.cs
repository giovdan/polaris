using System;
using System.Collections.Generic;
using System.Text;

namespace Mitrol.Framework.Domain.Enums
{
    public enum GroupEnum
    {
        ADMINS = 1,
        SUPERUSERS = 2,
        USERS = 3,
        BOOTUSERS = 4,
        STEELPROJECTS = 5,
        FICEP = 6,
        NONE = 999, //Gruppo usato per indicare attributi/parametri READ ONLY
    }
}
