namespace RepoDbVsEF.Application.Models
{
    using RepoDbVsEF.Domain.Enums;

    public class Entity
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityType { get; set; }
    }
}
