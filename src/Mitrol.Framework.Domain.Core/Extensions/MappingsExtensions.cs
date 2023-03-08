namespace Mitrol.Framework.Domain.Core.Extensions
{
    using AutoMapper;

    public static class MappingsExtensions
    {
        public static void IgnoreIfSourceIsNull<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt)
        {
            opt.Condition(IgnoreIfSourceIsNull);
        }

        private static bool IgnoreIfSourceIsNull<TSource, TDestination, TMember>(TSource source,
                                                                         TDestination destination,
                                                                         TMember sourceMember,
                                                                         TMember destinationMember,
                                                                         ResolutionContext context)
        {
            return sourceMember != null;
        }
    }
}
