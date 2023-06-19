namespace Mitrol.Framework.Domain.Enums
{
    using System;
    /// <summary>
    /// Enumerato operazioni (per usi generici) CRUD
    /// </summary>
    [Flags()]
    public enum CRUD_OperationTypeEnum
    {
        Create = 1,
        Remove = 2,
        Update = 4,
        Read = 8
    }
}