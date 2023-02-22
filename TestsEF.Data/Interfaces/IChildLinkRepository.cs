namespace RepoDbVsEF.EF.Data.Interfaces
{
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    

    public interface IChildLinkRepository: IRepository<ChildLink, IEFDatabaseContext>
    {
        void RemoveLinks(long parentId);
    }
}
