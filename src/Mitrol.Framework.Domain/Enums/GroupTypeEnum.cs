namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Enum for Group Types
    /// BuiltIn and Hidden are predefined Polaris groups
    /// </summary>
    public enum GroupTypeEnum
    {
        [Display(Name = "BuiltIn")]
        BuiltIn = 1,
        [Display(Name = "Hidden")]
        Hidden = 2,
        [Display(Name = "Application")]
        Application = 4
    }
}
