using System.Collections.Generic;

namespace Mitrol.Framework.Domain.Enums
{

    public enum RecordTypeEnum
    {
        Everything = -1,
        NotSpecified = 0,
        Drill = 1,
        Plasma = 2,
        Oxycut = 4,
        Technology = 8,
        Punch = 16
    }

    public static class RecordTypeEnumExtensions
    {
        public static PlantUnitEnum GetRelatedPlantUnit(this RecordTypeEnum recordType)
        {
            PlantUnitEnum plantUnit = PlantUnitEnum.None;
            switch (recordType)
            {
                case RecordTypeEnum.Drill:
                    plantUnit = PlantUnitEnum.DrillingMachine;
                    break;
                case RecordTypeEnum.Plasma:
                    plantUnit = PlantUnitEnum.PlasmaTorch;
                    break;
                case RecordTypeEnum.Oxycut:
                    plantUnit = PlantUnitEnum.OxyCutTorch;
                    break;
            }

            return plantUnit;
        }

        /// <summary>
        /// Get PLM Attribute's display names based on <paramref name="recordType"/>
        /// </summary>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAttributesForPLMTables(this RecordTypeEnum recordType)
        {
            var attributes = new List<string>();

            switch (recordType)
            {
                case RecordTypeEnum.Drill:
                    {
                        attributes.Add(DatabaseDisplayNameEnum.ForwardSpeed.ToString());        //FS
                        attributes.Add(DatabaseDisplayNameEnum.EndPosition.ToString());
                    }
                    break;
                case RecordTypeEnum.Plasma:
                    {
                        attributes.Add(DatabaseDisplayNameEnum.Kerf.ToString());               //KF
                        attributes.Add(DatabaseDisplayNameEnum.TangentialSpeed.ToString());    //F
                        attributes.Add(DatabaseDisplayNameEnum.PierceDelay.ToString());        //TP

                    }
                    break;
                case RecordTypeEnum.Oxycut:
                    {
                        attributes.Add(DatabaseDisplayNameEnum.Kerf.ToString());               //KF
                        attributes.Add(DatabaseDisplayNameEnum.TangentialSpeed.ToString());    //F
                        attributes.Add(DatabaseDisplayNameEnum.PreHeatTime.ToString());        //TP
                    }
                    break;
            }

            return attributes;
        }
    }
}
