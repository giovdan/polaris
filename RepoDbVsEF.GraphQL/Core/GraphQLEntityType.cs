namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate.Types;
    using RepoDbVsEF.Application.Models;

    public class GraphQLEntityType : ObjectType<Entity>
    {
        protected override void Configure(IObjectTypeDescriptor<Entity> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            base.Configure(descriptor);
        }
    }
}
