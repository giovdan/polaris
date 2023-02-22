using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoDbVsEF.Domain.Enums
{
    public enum ErrorCodesEnum
    {
        /// <summary>
        /// GENERIC_INPUT_INCONSISTENT
        /// </summary>
        ERR_GEN001 = 1,
        /// <summary>
        /// GENERIC_ITEM_NOTFOUND
        /// </summary>
        ERR_GEN002,
        /// <summary>
        /// GENERIC_ITEM_ALREADYEXISTS
        /// </summary>
        ERR_GEN003,
        /// <summary>
        /// GENERIC_ITEM_ALREADYDELETED
        /// </summary>
        ERR_GEN004,
        /// <summary>
        /// GENERIC_ID_INVALID
        /// </summary>
        ERR_GEN005,
        /// <summary>
        /// GENERIC_JSON_INVALIDFORMAT
        /// </summary>
        ERR_GEN006,
        /// <summary>
        /// GENERIC_OPERATION_FAILED
        /// </summary>
        ERR_GEN007,
        /// <summary>
        /// GENERIC_INPUT_INVALID
        /// </summary>
        ERR_GEN008,
        /// <summary>
        /// GENERIC_OPERATION_CANCELLED
        /// </summary>
        ERR_GEN009,
        /// <summary>
        /// GENERIC_CONVERSIONSSYTEM_INVALID
        /// </summary>
        ERR_GEN010,
        /// <summary>
        /// GENERIC_DISPLAYNAME_INVALID
        /// </summary>
        ERR_GEN011,
        /// <summary>
        /// NAME_MACHINENAMEVALIDATION_EMPTY
        /// </summary>
        ERR_GEN012,
        /// <summary>
        /// GENERIC_BOOTPARAMETERS_FAIL
        /// </summary>
        ERR_GEN013,
        /// <summary>
        /// GENERIC_VALUE_NOTSPECIFIED
        /// </summary>
        ERR_GEN014,
        /// <summary>
        /// GENERIC_PROFILETYPEENUM_INVALID
        /// </summary>
        ERR_GEN015,
        /// <summary>
        /// GENERIC_PERMISSION_INVALID
        /// </summary>
        ERR_GEN016,
        /// <summary>
        /// GENERIC_OPERATION_IMPOSSIBLE
        /// </summary>
        ERR_GEN017,
        /// <summary>
        /// EXECUTION_NOT_LOADED
        /// </summary>
        ERR_GEN018,
    }
}
