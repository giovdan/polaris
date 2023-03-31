namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;
    /// <summary>
    /// Implements null object pattern for <see cref="IUserSession"/>.
    /// </summary>
    public class NullUserSession : UserSession
    {
        public NullUserSession(string username, string fullName, long userId, string culture, IEnumerable<GroupItem> groups, IEnumerable<string> permissions, string machineName)
            : base(username, fullName, userId, culture, groups, permissions, machineName, string.Empty, MeasurementSystemEnum.MetricSystem)
        { }

        public static NullUserSession Instance { get; } = new NullUserSession(username: "null.user"
                                                                            , fullName: "null.user"
                                                                            , userId: 0
                                                                            , culture: "it-IT"
                                                                            , groups: new[] { new GroupItem((long)BuiltInGroupEnum.USERS, BuiltInGroupEnum.USERS.ToString(), GroupTypeEnum.BuiltIn) }
                                                                            , permissions: null
                                                                            , machineName: null);

        public static NullUserSession InternalSessionInstance { get; } = new NullUserSession(username: "internal.user"
                                                                            , fullName: "internal.user"
                                                                            , userId: 0
                                                                            , culture: "it-IT"
                                                                            , groups: new[] { new GroupItem((long)BuiltInGroupEnum.ADMINS, BuiltInGroupEnum.ADMINS.ToString(), GroupTypeEnum.BuiltIn) }
                                                                            , permissions: null
                                                                            , machineName: null);
    }
}