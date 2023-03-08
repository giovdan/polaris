using Mitrol.Framework.Domain.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Mitrol.Framework.Domain.Core.Models
{
    public class BaseEntity : IEntity
    {
        [Key()]
        public long Id { get; set; }
    }
}