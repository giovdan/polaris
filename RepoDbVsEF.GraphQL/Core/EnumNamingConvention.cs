

namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate;
    using HotChocolate.Types.Descriptors;
    using Humanizer;

    public class EnumNamingConvention: DefaultNamingConventions
    {
        public override NameString GetEnumValueName(object value) => value.ToString().Underscore().ToUpperInvariant();
    }
}
