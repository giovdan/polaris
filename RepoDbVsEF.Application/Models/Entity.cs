namespace RepoDbVsEF.Application.Models
{
    using RepoDbVsEF.Domain.Enums;

    public class Entity
    {
        public ulong Id { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityType { get; set; }
    }
}
