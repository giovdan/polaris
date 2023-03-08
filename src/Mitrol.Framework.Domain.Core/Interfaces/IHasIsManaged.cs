namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public interface IHasIsManaged
    {
        [Required()]
        [DefaultValue(false)]
        bool IsManaged { get; set; }
    }
}
