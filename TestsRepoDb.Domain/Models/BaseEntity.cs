namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        [Key()]
        public ulong Id { get; set; }
    }
}
