using System;
using System.Collections.Generic;
using System.Text;

namespace Mitrol.Framework.Domain.Enums
{
    [Flags()]
    //[TypeConverter(typeof(EnumCustomNameTypeConverter))]
    //[DefaultValue("Default")]
    public enum ProcessingTechnologyEnum
    {
        //[TechnologyRelatedTo(PlantUnitEnum.All)]
        //[EnumSerializationName("Default")]
        //[EnumField("Default", true, "LBL_PROCESSINGTECHNOLOGY_DEFAULT")]
        Default = 1,

        //[TechnologyRelatedTo(PlantUnitEnum.PlasmaTorch)]
        //[EnumSerializationName("HPR")]
        //[EnumCustomName("HPR")]
        //[EnumField("HPR", true, "LBL_PROCESSINGTECHNOLOGY_HPR")]
        PlasmaHPR = 2,

        //[TechnologyRelatedTo(PlantUnitEnum.PlasmaTorch)]
        //[EnumSerializationName("XPR")]
        //[EnumCustomName("XPR")]
        //[EnumField("XPR", true, "LBL_PROCESSINGTECHNOLOGY_XPR")]
        PlasmaXPR = 4
    }
}
