namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    

    /// <summary>
    /// Gestisce la relazione fra le tipologie di tabelle e le plant unit
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RelatedToolRangeTypeAttribute: Attribute
    {
        public bool IsSubRange => SubRangeType == SubRangeTypeEnum.TrueHole || SubRangeType == SubRangeTypeEnum.Bevel;
        /// <summary>
        /// Tipologia di tabella
        /// </summary>
        public ToolRangeTypeEnum Type { get; set; }
        /// <summary>
        /// Tipo di sotto tabella associato (None nel caso di tabella principale)
        /// </summary>
        public SubRangeTypeEnum SubRangeType { get; set; }
        public RelatedToolRangeTypeAttribute(ToolRangeTypeEnum toolTableType
                , SubRangeTypeEnum subRangeType = SubRangeTypeEnum.None)
        {
            Type = toolTableType;
            SubRangeType = subRangeType;
        }
    }
}
