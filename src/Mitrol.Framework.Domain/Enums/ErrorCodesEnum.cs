// ***********************************************************************
// Assembly         : Mitrol.Framework.Domain
// Author           : giovanni.dantonio
// Created          : 06-17-2021
//
// Last Modified By : giovanni.dantonio
// Last Modified On : 10-08-2021
// ***********************************************************************
// <copyright file="ErrorCodesEnum.cs" company="Mitrol S.r.l.">
//     Copyright © 2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Mitrol.Framework.Domain.Core.Enums
{
    /// <summary>
    ///   <para>
    ///     <b>Definitions of all application errors</b>
    ///   </para>
    ///   <para>For defining the error codes using <b>ERR_CONTXNR</b> template where:</para>
    ///   <list type="bullet">
    ///     <item>
    ///       <b>CONTX</b> is the context with length between 3 and 4 characters</item>
    ///     <item>
    ///       <b>XNR</b> is the enumeration of errors in specific context (3 or 2 character i.e. from 0 to 999 or form 0 to 99)</item>
    ///   </list>
    /// </summary>
    public enum ErrorCodesEnum : int
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
        /// <summary>
        /// USERNAME_USERLOGIN_EMPTY
        /// </summary>
        ERR_LGN001,
        /// <summary>
        /// USERNAME_USERLOGIN_INVALIDLENGTH
        /// </summary>
        [System.Obsolete("Questo error code per login non è utilizzato.")]
        ERR_LGN002,
        /// <summary>
        /// PASSWORD_USERLOGIN_EMPTY
        /// </summary>
        ERR_LGN003,
        /// <summary>
        /// USERNAMEORPASSWORD_USERLOGIN_INVALID
        /// </summary>
        ERR_LGN004,
        /// <summary>
        /// REFRESHTOKEN_USERLOGIN_EMPTY
        /// </summary>
        ERR_LGN005,
        /// <summary>
        /// REFRESHTOKEN_USERLOGIN_INVALID
        /// </summary>
        ERR_LGN006,
        /// <summary>
        /// REFRESHTOKEN_USERLOGIN_EXPIRED
        /// </summary>
        ERR_LGN007,
        /// <summary>
        /// GRANTTYPE_USERLOGIN_NOTSUPPORTED
        /// </summary>
        ERR_LGN008,
        /// <summary>
        /// FIRSTNAME_USERMANAGEMENT_EMPTY
        /// </summary>
        ERR_USR001,
        /// <summary>
        /// FIRSTNAME_USERMANAGEMENT_INVALIDLENGTH
        /// </summary>
        ERR_USR002,
        /// <summary>
        /// GENERIC_USERMANAGEMENT_DISABLEDUSER
        /// </summary>
        ERR_USR003,
        /// <summary>
        /// GENERIC_USERMANAGEMENT_SYSTEMUSER
        /// </summary>
        ERR_USR004,
        /// <summary>
        /// GROUP_USERMANAGEMENT_INVALID
        /// </summary>
        ERR_USR005,
        /// <summary>
        /// GROUP_USERMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_USR006,
        /// <summary>
        /// LASTNAME_USERMANAGEMENT_EMPTY
        /// </summary>
        ERR_USR007,
        /// <summary>
        /// LASTNAME_USERMANAGEMENT_INVALIDLENGTH
        /// </summary>
        ERR_USR008,
        /// <summary>
        /// USERID_USERMANAGEMENT_EMPTY
        /// </summary>
        ERR_USR009,
        /// <summary>
        /// USERSTATUS_USERMANAGEMENT_INVALID
        /// </summary>
        ERR_USR010,
        /// <summary>
        /// USERNAME_USERMANAGEMENT_INVALID
        /// </summary>
        ERR_USR011,
        /// <summary>
        /// USERNAME_USERMANAGEMENT_ALREADY_EXISTS
        /// </summary>
        ERR_USR012,
        /// <summary>
        /// PASSWORD_USERMANAGEMENT_EMPTY
        /// </summary>
        ERR_USR013,
        /// <summary>
        /// PASSWORD_USERMANAGEMENT_INVALID_LENGTH
        /// </summary>
        ERR_USR014,
        /// <summary>
        /// PASSWORDS_USERMANAGEMENT_UNEQUAL
        /// </summary>
        ERR_USR015,
        /// <summary>
        /// SESSIONS_USERMANAGEMENT_OPENED
        /// </summary>
        ERR_USR016,
        /// <summary>
        /// MYSELF_USERDELETE_INVALID
        /// </summary>
        ERR_USR017,
        /// <summary>
        /// OLDPASSWORD_CHANGEPASSWORD_INVALID
        /// </summary>
        ERR_PWD001,
        /// <summary>
        /// OLDPASSWORD_CHANGEPASSWORD_WRONG
        /// </summary>
        ERR_PWD002,
        /// <summary>
        /// NEWPASSWORD_CHANGEPASSWORD_INVALID
        /// </summary>
        ERR_PWD003,
        /// <summary>
        /// NEWPASSWORD_CHANGEPASSWORD_EQUALTOOLD
        /// </summary>
        ERR_PWD004,
        /// <summary>
        /// CONFIRMPASSWORD_CHANGEPASSWORD_INVALID
        /// </summary>
        ERR_PWD005,
        /// <summary>
        /// CONFIRMPASSWORD_CHANGEPASSWORD_NOTEQUALTONEW
        /// </summary>
        ERR_PWD006,
        #region < Tool Validation >
        /// <summary>
        /// TOOLTYPE_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM001,
        /// <summary>
        /// ID_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM002,
        /// <summary>
        /// CODE_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM003,
        /// <summary>
        /// IDENTIFIERLIST_TOOLMANAGEMENT_EMPTY
        /// </summary>
        ERR_TLM004,
        /// <summary>
        /// SETUP_TOOLMANAGEMENT_INCLUDED
        /// </summary>
        ERR_TLM005,
        /// <summary>
        /// WAREHOUSEID_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM006,
        /// <summary>
        /// SOURCETOOL_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM007,
        /// <summary>
        /// PLANTUNIT_TOOLMANAGEMENT_INVALID
        /// </summary>
        ERR_TLM008,

        /// <summary>
        /// TOOL_VALIDATION_STARTPOSITION_INVALID
        /// </summary>
        ERR_TLM009,
        /// <summary>
        /// TOOL_VALIDATION_ENDPOSITION_INVALID
        /// </summary>
        ERR_TLM010,
        /// <summary>
        /// TOOL_VALIDATION_FASTAPPROACHPOSITION_INVALID
        /// </summary>
        ERR_TLM011,
        /// <summary>
        /// TOOL_VALIDATION_TOOLLENGTH_INVALID
        /// </summary>
        ERR_TLM012,
        /// <summary>
        /// TOOL_VALIDATION_REALDIAMETER_INVALID
        /// </summary>
        ERR_TLM013,
        /// <summary>
        /// TOOL_VALIDATION_FORWARDSPEED_INVALID
        /// </summary>
        ERR_TLM014,
        /// <summary>
        /// TOOL_VALIDATION_CUTTINGSPEED_INVALID
        /// </summary>
        ERR_TLM015,
        /// <summary>
        /// TOOL_VALIDATION_MILLINGSHANKDIAMETER_INVALID
        /// </summary>
        ERR_TLM016,
        /// <summary>
        /// TOOL_VALIDATION_MILLINGSHANKHEIGHT_INVALID
        /// </summary>
        ERR_TLM017,
        /// <summary>
        /// TOOL_VALIDATION_MINTAPPINGPITCH_INVALID
        /// </summary>
        ERR_TLM018,
        /// <summary>
        /// TOOL_VALIDATION_MAXTAPPINGPITCH_INVALID
        /// </summary>
        ERR_TLM019,
        /// <summary>
        /// TOOL_VALIDATION_MILLINGCUTTERTEETH_INVALID
        /// </summary>
        ERR_TLM020,
        /// <summary>
        /// TOOL_VALIDATION_GRINDINGANGLE_INVALID
        /// </summary>
        ERR_TLM021,
        /// <summary>
        /// TOOL_VALIDATION_MINHOLEDIAMETER_INVALID
        /// </summary>
        ERR_TLM022,
        /// <summary>
        /// TOOL_VALIDATION_MAXHOLEDIAMETER_INVALID
        /// </summary>
        ERR_TLM023,
        /// <summary>
        /// TOOL_VALIDATION_CONTOURNINGSPEED_INVALID
        /// </summary>
        ERR_TLM024,
        /// <summary>
        /// TOOL_VALIDATION_CUTTINGDEPTH_INVALID
        /// </summary>
        ERR_TLM025,
        /// <summary>
        /// TOOL_VALIDATION_IDENTIFIERS_INVALID
        /// </summary>
        ERR_TLM026,
        /// <summary>
        /// TOOL_VALIDATION_PROGRAMLINK_PRESENCE
        /// </summary>
        ERR_TLM027,
        /// <summary>
        /// TOOL_VALIDATION_BLADETEETH_INVALID
        /// </summary>
        ERR_TLM028,
        /// <summary>
        /// TOOL_VALIDATION_BLAREROTATIONSPEED_INVALID
        /// </summary>
        ERR_TLM029,
        /// <summary>
        /// TOOL_VALIDATION_FORWARDSPEED_INVALID
        /// </summary>
        ERR_TLM030,
        #endregion
        /// <summary>
        /// ATTRIBUTEVALUE_TOOLMANAGEMENT_EMPTY
        /// </summary>
        ERR_ATT001,
        /// <summary>
        /// ATTRIBUTEDISPLAYNAME_TOOLMANAGEMENT_EMPTY
        /// </summary>
        ERR_ATT002,
        /// <summary>
        /// ATTRIBUTES_TOOLMANAGEMENT_NOTSPECIFIED
        /// </summary>
        ERR_ATT003,
        /// <summary>
        /// ROWS_RANGEMANAGEMENT_EMPTY
        /// </summary>
        ERR_TRM001,
        /// <summary>
        /// ROWFILTERS_RANGEMANAGEMENT_EMPTY
        /// </summary>
        ERR_TRM002,
        /// <summary>
        /// TYPE_RANGEMANAGEMENT_INVALID
        /// </summary>
        ERR_TRM003,
        /// <summary>
        /// SUBROWS_RANGEMANAGEMENT_FOUNDED
        /// </summary>
        ERR_TRM004,
        /// <summary>
        /// TOOLRANGE_SUBRANGEMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_TRM005,
        /// <summary>
        /// PARENT_SUBRANGEMANAGEMENT_INVALID
        /// </summary>
        ERR_TRM006,
        /// <summary>
        /// TOOLID_SUBRANGEMANAGEMENT_INVALID
        /// </summary>
        ERR_TRM007,
        /// <summary>
        /// PARENT_IMPORTSUBRANGE_NOTFOUND
        /// </summary>
        ERR_TRM008,
        /// <summary>
        /// CODE_TOOLHOLDERMANAGEMENT_EMPTY
        /// </summary>
        ERR_THM001,
        /// <summary>
        /// DIAMETER_TOOLHOLDERMANAGEMENT_INVALID
        /// </summary>
        ERR_THM002,
        /// <summary>
        /// LENGTH_TOOLHOLDERMANAGEMENT_INVALID
        /// </summary>
        ERR_THM003,
        /// <summary>
        /// ID_TOOLHOLDERMANAGEMENT_INVALID
        /// </summary>
        ERR_THM004,
        /// <summary>
        /// LINK_TOOLHOLDERMANAGEMENT_PRESENT
        /// </summary>
        ERR_THM005,
        /// <summary>
        /// CODE_MATERIALMANAGEMENT_INVALID
        /// </summary>
        ERR_MTM001,
        /// <summary>
        /// LINK_MATERIALMANAGEMENT_PRESENT
        /// </summary>
        ERR_MTM002,
        /// <summary>
        /// IP_PLANTMACHINEMANAGEMENT_INVALID
        /// </summary>
        ERR_PTM001,
        /// <summary>
        /// STATUS_PLANTMACHINEMANAGEMENT_INVALID
        /// </summary>
        ERR_PTM002,
        /// <summary>
        /// CULTURE_LOCALIZATION_INVALID
        /// </summary>
        ERR_LOC001,
        /// <summary>
        /// CONTEXT_LOCALIZATION_INVALID
        /// </summary>
        ERR_LOC002,
        /// <summary>
        /// TRANSLATION_LOCALIZATION_EMPTY
        /// </summary>
        ERR_LOC003,
        /// <summary>
        /// CODE_LOCALIZATION_INVALID
        /// </summary>
        ERR_LOC004,
        /// <summary>
        /// CATEGORY_MANAGEPARAMETERS_INVALID
        /// </summary>
        ERR_PAR001,
        /// <summary>
        /// CODE_MANAGEPARAMETERS_EMPTY
        /// </summary>
        ERR_PAR002,
        /// <summary>
        /// CNCTYPE_ADDLINK_INVALID
        /// </summary>
        ERR_PAR003,
        /// <summary>
        /// VALUE_MANAGEPARAMETERS_INVALID
        /// </summary>
        ERR_PAR004,
        /// <summary>
        /// VARIABILE_ADDLINK_INVALIDCODE
        /// </summary>
        ERR_PAR005,
        /// <summary>
        /// VARIABILE_ADDLINK_NOTSPECIFIED
        /// </summary>
        ERR_PAR006,
        /// <summary>
        /// CNCVARIABILE_MANAGEPARAMETERS_NOTSET
        /// </summary>
        ERR_PAR007,
        /// <summary>
        /// GROUPNAME_MANAGEPARAMETERS_EMPTY
        /// </summary>
        ERR_PAR008,
        /// <summary>
        /// INDEX_MANAGEPARAMETERS_INVALID
        /// </summary>
        [Obsolete] ERR_PAR009,
        /// <summary>
        /// GROUPASSOCIATION_MANAGEPARAMETERS_NOTFOUND
        /// </summary>
        ERR_PAR010,
        /// <summary>
        /// GROUPPRIORITY_MANAGEPARAMETERS_INVALID
        /// </summary>
        ERR_PAR011,
        /// <summary>
        /// ID_MANAGEPARAMETERS_INVALID
        /// </summary>
        ERR_PAR012,
        /// <summary>
        /// FILEORDIRECTORY_MANAGESETTINGS_INVALID
        /// </summary>
        ERR_STG001,
        /// <summary>
        /// FILE_MANAGESETTINGS_NOTFOUND
        /// </summary>
        ERR_STG002,
        /// <summary>
        /// FILE_MANAGESETTINGS_EMPTY
        /// </summary>
        ERR_STG003,
        /// <summary>
        /// CONTEXT_MANAGESETTINGS_INVALID
        /// </summary>
        ERR_STG004,
        /// <summary>
        /// KEY_MANAGESETTINGS_INVALID
        /// </summary>
        ERR_STG005,
        /// <summary>
        /// SECTION_IMPORTCONFIGURATION_EMPTY
        /// </summary>
        ERR_STG006,
        /// <summary>
        /// SETTINGS_IMPORTCONFIGURATION_EMPTY
        /// </summary>
        ERR_STG007,
        /// <summary>
        /// SETTINGS_MACROCONFIGURATIONFILE_NOTFOUND
        /// </summary>
        ERR_STG008,
        /// <summary>
        /// UNITTYPE_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP001,
        /// <summary>
        /// TOOLID_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP002,
        /// <summary>
        /// DRILLHEAD_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP003,
        /// <summary>
        /// UNIT_SETUPMANAGEMENT_BUSY
        /// </summary>
        ERR_STP004,
        /// <summary>
        /// UNIT_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP005,
        /// <summary>
        /// ACTION_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP006,
        /// <summary>
        /// SLOTFORCHANGE_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP007,
        /// <summary>
        /// SPINDLE_SETUPMANAGEMENT_EMPTY
        /// </summary>
        ERR_STP008,
        /// <summary>
        /// SLOT_SETUPMANAGEMENT_DISABLE
        /// </summary>
        ERR_STP009,
        /// <summary>
        /// SLOT_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP010,
        /// <summary>
        /// REQUIREDTOOL_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP011,
        /// <summary>
        /// SETUP_SETUPMANAGEMENT_INVALID
        /// </summary>
        ERR_STP012,
        /// <summary>
        /// TOOL_SETUPMANAGEMENT_ONSPINDLE
        /// </summary>
        ERR_STP013,
        /// <summary>
        /// TOOL_REMOVEFROMSLOT_ONSPINDLE
        /// </summary>
        ERR_STP014,
        /// <summary>
        /// ACTION_STARTPLCCYCLE_FAILED
        /// </summary>
        ERR_STP015,
        /// <summary>
        /// GENERALSETUP_PAREMETERUPDATE_FAILED
        /// </summary>
        ERR_STP016,
        /// <summary>
        /// NAME_WAREHOUSEMANAGEMENT_EMPTY
        /// </summary>
        ERR_WRH001,
        /// <summary>
        /// CARTID_WAREHOUSEMANAGEMENT_EMPTY
        /// </summary>
        ERR_WRH002,
        /// <summary>
        /// STATUS_WAREHOUSEMANAGEMENT_INVALID
        /// </summary>
        ERR_WRH003,
        /// <summary>
        /// TYPE_WAREHOUSEMANAGEMENT_INVALID
        /// </summary>
        ERR_WRH004,

        /// <summary>
        /// CONTEXT_EVENTFILTER_INVALID
        /// </summary>
        ERR_EVT001,
        /// <summary>
        /// TYPE_EVENTFILTER_INVALID
        /// </summary>
        ERR_EVT002,
        /// <summary>
        /// FIRSTLINE_PROGRAMCODEMANAGEMENT_INVALID
        /// </summary>
        ERR_PGC001,
        /// <summary>
        /// PRODUCTIONJOBID_PROGRAMCODEMANAGEMENT_NOTSPECIFIED
        /// </summary>
        ERR_PGC002,
        /// <summary>
        /// PIECEID_PROGRAMCODEMANAGEMENT_NOTSPECIFIED
        /// </summary>
        ERR_PGC003,
        /// <summary>
        /// PRODUCTIONROW_PROGRAMCODEMANAGEMENT_LINK
        /// </summary>
        ERR_PGC004,
        /// <summary>
        /// LASTLINE_PROGRAMCODEMANAGEMENT_INVALID
        /// </summary>
        ERR_PGC005,
        /// <summary>
        /// LINESTATUS_PROGRAMCODEMANAGEMENT_INVALID
        /// </summary>
        ERR_PGC006,
        /// <summary>
        /// LINETYPE_PROGRAMCODEMANAGEMENT_INVALID
        /// </summary>
        ERR_PGC007,
        /// <summary>
        /// MACHINESTATUS_PROGRAMCODEMANAGEMENT_NOTPROCESSING
        /// </summary>
        ERR_PGC008,
        /// <summary>
        /// PROCESSDATA_PROGRAMMANAGEMENT_NOTSPECIFIED
        /// </summary>
        ERR_PRG001,
        /// <summary>
        /// NAME_PROGRAMMANAGEMENT_EMPTY
        /// </summary>
        ERR_PRG002,
        /// <summary>
        /// ID_PROGRAMMANAGEMENT_EMPTY
        /// </summary>
        ERR_PRG003,
        /// <summary>
        /// MATERIAL_PROGRAMMANAGEMENT_INVALID
        /// </summary>
        ERR_PRG004,
        /// <summary>
        /// PROGRAMTYPE_PROGRAMMANAGEMENT_INVALID
        /// </summary>
        ERR_PRG005,
        /// <summary>
        /// PIECELIST_PROGRAMMANAGEMENT_NOTSPECIFIED
        /// </summary>
        ERR_PRG006,
        /// <summary>
        /// BACKLOG_PROGRAMMANAGEMENT__UNDEFINED
        /// </summary>
        ERR_PRG007,
        /// <summary>
        /// SCHEDULEDQUANTITY_PROGRAMMANAGEMENT__INVALID
        /// </summary>
        ERR_PRG008,
        /// <summary>
        /// TOTALQUANTITY_PROGRAMMANAGEMENT__INVALID
        /// </summary>
        ERR_PRG009,
        /// <summary>
        /// ADDPIECELINK_PROGRAMMANAGEMENT_FAILED
        /// </summary>
        ERR_PRG010,
        /// <summary>
        /// REPETITIONSNUMBER_PROGRAMMANAGEMENT_INVALID
        /// </summary>
        ERR_PRG011,
        /// <summary>
        /// ASSEMBLY_PIECEMANAGEMENT_EMPTY
        /// </summary>
        ERR_PIE001,
        /// <summary>
        /// CONTRACT_PIECEMANAGEMENT_EMPTY
        /// </summary>
        ERR_PIE002,
        /// <summary>
        /// PROJECT_PIECEMANAGEMENT_EMPTY
        /// </summary>
        ERR_PIE003,
        /// <summary>
        /// PART_PIECEMANAGEMENT_EMPTY
        /// </summary>
        ERR_PIE004,
        /// <summary>
        /// DRAWING_PIECEMANAGEMENT_EMPTY
        /// </summary>
        ERR_PIE005,
        /// <summary>
        /// TOTALQUANTITY_PIECEMANAGEMENT_INVALID
        /// </summary>
        ERR_PIE006,
        /// <summary>
        /// PROGRAM_PIECEMANAGEMENT_RELATED
        /// </summary>
        ERR_PIE007,
        /// <summary>
        /// PIECE_IDENTIFIERS_EMPTY
        /// </summary>
        ERR_PIE008,
        /// <summary>
        /// ATTRIBUTE_PROFILETYPEID_EMPTY
        /// </summary>
        ERR_STK001,
        /// <summary>
        /// MATERIAL_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK002,
        /// <summary>
        /// PROFILE_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK003,
        /// <summary>
        /// QUANTITY_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK004,
        /// <summary>
        /// QUANTITY_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK005,
        /// <summary>
        /// MATERIAL_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK006,
        /// <summary>
        /// PROFILE_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK007,
        /// <summary>
        /// LENGTH_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK008,
        /// <summary>
        /// LENGTH_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK009,
        /// <summary>
        /// THICKNESS_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK010,
        /// <summary>
        /// THICKNESS_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK011,
        /// <summary>
        /// WIDTH_STOCKMANAGEMENT_NOTFOUND
        /// </summary>
        ERR_STK012,
        /// <summary>
        /// WIDTH_STOCKMANAGEMENT_INVALID
        /// </summary>
        ERR_STK013,
        /// <summary>
        /// STOCK_PROGRAM_RELATED
        /// </summary>
        ERR_STK014,
        /// <summary>
        /// STOCKITEM_DUPLICATE_P
        /// </summary>
        ERR_STK015,
        /// <summary>
        /// STOCKITEM_DUPLICATE_NP
        /// </summary>
        ERR_STK016,
        /// <summary>
        /// DATERANGE_FILTER_INVALID
        /// </summary>
        ERR_ALL001,
        /// <summary>
        /// MATERIALTYPE_MATERIALMANAGEMENT_INVALID
        /// </summary>
        ERR_MAT001,
        /// <summary>
        /// SPECIFICWEIGHT_MATERIALMANAGEMENT_INVALID
        /// </summary>
        ERR_MAT002,
        /// <summary>
        /// ID_MATERIALMANAGEMENT_INVALID
        /// </summary>
        ERR_MAT003,
        /// <summary>
        /// LIST_PRODUCTIONPROCESSING_EMPTY
        /// </summary>
        ERR_PRD001,
        /// <summary>
        /// STATION_ORIGINMANAGEMENT_INVALID
        /// </summary>
        ERR_PRD002,

        /// <summary>
        /// LINENUMBER_OPERATIONMANAGEMENT_INVALID
        /// </summary>
        ERR_OPE001,
        /// <summary>
        /// OPERATIONTYPE_OPERATIONMANAGEMENT_INVALID
        /// </summary>
        ERR_OPE002,
        /// <summary>
        /// LEVEL_OPERATIONMANAGEMENT_MAXEXCEEDED
        /// </summary>
        ERR_OPE003,
        /// <summary>
        /// SOURCELINENUMBER_MOVEOPERATION_INVALID
        /// </summary>
        ERR_OPE004,
        /// <summary>
        /// LINENUMBERSTOMOVE_MOVEOPERATION_INVALID
        /// </summary>
        ERR_OPE005,
        /// <summary>
        /// CREATE_ADDITIONALITEM_ORDER
        /// </summary>
        ERR_OPE006,

        /// <summary>
        /// EXPIRATIONTIME_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT001,
        /// <summary>
        /// EXPIRATIONTIME_MAINTENANCEMANAGEMENT_GREATERTHANUNTIL
        /// </summary>
        ERR_MNT002,
        /// <summary>
        /// EXPIRATIONTIME_MAINTENANCEMANAGEMENT_LESSTHANNOW
        /// </summary>
        ERR_MNT003,
        /// <summary>
        /// UNTILTIME_MAINTENANCEMANAGEMENT_LESSTHANNOW
        /// </summary>
        ERR_MNT004,
        /// <summary>
        /// UNTILTIME_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT005,
        /// <summary>
        /// GIVENNOTICEMODE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT006,
        /// <summary>
        /// GIVENNOTICETIME_MAINTENANCEMANAGEMENT_GRETATERTHANUNTIL
        /// </summary>
        ERR_MNT007,
        /// <summary>
        /// ID_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT008,
        /// <summary>
        /// TITLE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT009,
        /// <summary>
        /// CODE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT010,
        /// <summary>
        /// POSTPONEMODE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT011,
        /// <summary>
        /// ESTIMATEDTIME_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT012,
        /// <summary>
        /// STATUS_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT013,
        /// <summary>
        /// CATEGORY_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT014,
        /// <summary>
        /// TYPE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT015,
        /// <summary>
        /// REPEATITIONMODE_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT016,
        /// <summary>
        /// WEEKNUMBER_MAINTENANCEMANAGEMENT_INVALID
        /// </summary>
        ERR_MNT017,

        //IMPORT EXPORT  
        /// <summary>
        /// PROFILE_IMPORT_INVALIDFORMAT
        /// </summary>
        ERR_PRFI01,
        /// <summary>
        /// PROFILECODE_IMPORT_INVALID
        /// </summary>
        ERR_PRFI02,
        /// <summary>
        /// MATERIAL_IMPORT_INVALIDFORMAT
        /// </summary>
        ERR_MATI01,
        /// <summary>
        /// MATERIALCODE_IMPORT_INVALID
        /// </summary>
        ERR_MATI02,
        /// <summary>
        /// DIRECTORY_EXPORT_INVALID
        /// </summary>
        ERR_EXP001,
        /// <summary>
        /// FILENAME_EXPORT_INVALID
        /// </summary>
        ERR_EXP002,
        /// <summary>
        /// DIRECTORY_EXPORT_INEXISTENT
        /// </summary>
        ERR_EXP003,
        /// <summary>
        /// OBJECTENUM_IMPEXP_INVALID
        /// </summary>
        ERR_IMEX01,
        /// <summary>
        /// PIECEOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX02,
        /// <summary>
        /// PROGRAMOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX03,
        /// <summary>
        /// TOOLOBJECT_IMPEXP_TOOLMANAGEMENTINVALID
        /// </summary>
        ERR_IMEX04,
        /// <summary>
        /// TOOLOBJECT_IMPEXP_INVALID_IDENTIFIERS
        /// </summary>
        ERR_IMEX05,
        /// <summary>
        /// TOOLOBJECT_IMPEXP_INVALID_PARENTIDENTIFIERS
        /// </summary>
        ERR_IMEX06,
        /// <summary>
        /// TOOLHOLDEROBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX07,
        /// <summary>
        /// TOOLOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX08,
        /// <summary>
        /// TOOLTABLEOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX09,
        /// <summary>
        /// STOCKOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX10,
        /// <summary>
        /// MAINTENANCEOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX11,
        /// <summary>
        /// MATERIALOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX12,
        /// <summary>
        /// PROFILEOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX13,
        /// <summary>
        /// ACCOUNTOBJECT_IMPEXP_NOTFOUND
        /// </summary>
        ERR_IMEX14,
        /// <summary>
        /// FILETOIMPORT_IMPEXP_INVALIDENCRYPTIONKEY
        /// </summary>
        ERR_IMEX15,
        /// <summary>
        /// PIECEATTRIBUTES_IMPORT_INVALID
        /// </summary>
   		ERR_PIEI01,
        /// <summary>
        /// OPERATIONTYPE_IMPORT_INVALID
        /// </summary>
        ERR_PIEI02,
        /// <summary>
        /// PROGRAMTYPE_PROGRAMIMPORT_INVALID
        /// </summary>
        ERR_PRGI01,
        /// <summary>
        /// RELATEDPIECES_PROGRAMIMPORT_INVALID
        /// </summary>
        ERR_PRGI02,
        /// <summary>
        /// STOCK_PROGRAMIMPORT_NULL
        /// </summary>
        ERR_PRGI03,
        /// <summary>
        /// NAME_PROGRAMIMPORT_INVALID
        /// </summary>
        ERR_PRGI04,
        /// <summary>
        /// PROGRAMTYPE_PROGRAMIMPORT_INVALID
        /// </summary>
        ERR_PRGI05,
        /// <summary>
        /// LINKEDPIECE_PROGRAMIMPORT_NOTEXIST
        /// </summary>
        ERR_PRGI06,
        /// <summary>
        /// PROGRAMPIECETYPE_PROGRAMIMPORT_INVALID
        /// </summary>
        ERR_PRGIO7,
        /// <summary>
        /// QUANTITYBACKLOG_SCHEDULEDQUANTITY_INVALID
        /// </summary>
        ERR_QBL001,
        /// <summary>
        /// QUANTITYBACKLOG_TOTALQUANTITY_LESSTHAN_SCHEDULED
        /// </summary>
        ERR_QBL002,
        /// <summary>
        /// QUANTITYBACKLOG_TOTALQUANTITY_INVALID
        /// </summary>
        ERR_QBL003,
        /// <summary>
        /// TOOLRANGE_VALIDATION_MINTHICKNESS_GT_MAXTHICKNESS
        /// </summary>
        ERR_TTV001,
        /// <summary>
        /// TOOLRANGE_VALIDATION_MINTHICKNESS_NEGATIVE
        /// </summary>
        ERR_TTV002,
        /// <summary>
        /// TOOLRANGE_VALIDATION_MAXTHICKNESS_NEGATIVE
        /// </summary>
        ERR_TTV003,
        /// <summary>
        /// PROFILE_PROFILECODE_INVALID
        /// </summary>
        ERR_PRO001,

        /// <summary>
        /// GROUP_CODE_INVALID
        /// </summary>
        ERR_GRP001,
        /// <summary>
        /// GROUP_PERMISSIONS_NOTSPECIFIED
        /// </summary>
        ERR_GRP002,
    }

    public static class ErrorCodesEnumExtensions
    {
        public static System.Collections.Generic.Dictionary<string, string> ToErrorResponseBody(this ErrorCodesEnum errorCode)
            => new System.Collections.Generic.Dictionary<string, string> {
                {"error", errorCode.ToString()}
            };
    }
}