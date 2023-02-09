namespace RepoDbVsEF.Domain.Interfaces
{
    public interface IResolver
    {
        object Resolve();
    }

    public interface IResolver<TService> : IResolver
    {
        new TService Resolve();
    }

    public interface IResolver<TService, TServiceKindEnum>
    {
        TService Resolve(TServiceKindEnum serviceKind);
    }
}
