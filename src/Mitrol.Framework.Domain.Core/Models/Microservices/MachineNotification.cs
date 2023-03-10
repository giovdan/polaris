namespace Mitrol.Framework.Domain.Core.Models
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for Alarm translation
    /// </summary>
    public class MachineNotification
    {
        #region < Construction >

        public MachineNotification()
        {
            AdditionalInfos = new();
            Arguments = new();
            NotificationDate = DateTime.UtcNow;
        }

        public MachineNotification(NotificationTypeEnum type, int number, int parameter) : this()
        {
            Type = type;
            Number = number;
            Parameter = parameter;

            //var notificationCodePrefix = type.GetEnumAttribute<DatabaseDisplayNameAttribute>().DisplayName;
            //var notificationCode = $"{notificationCodePrefix}{number:D3}-{parameter:D3}";

            //NotificationCode = notificationCode;
            //LocalizationKey = notificationCode;
            //DescriptionLocalizationKey = $"{notificationCode}_DESCRIPTION";
            //CausesAndSolutions = new CauseSolutionPair[]
            //{
            //    new ($"{notificationCode}_CAUSE1", $"{notificationCode}_SOLUTION1_DESC"),
            //    new ($"{notificationCode}_CAUSE2", $"{notificationCode}_SOLUTION2_DESC"),
            //    new ($"{notificationCode}_CAUSE3", $"{notificationCode}_SOLUTION3_DESC")
            //};
        }

        public MachineNotification(NotificationTypeEnum type, int number, int parameter, DateTime notificationDate)
            : this(type, number, parameter)
        {
            NotificationDate = notificationDate;
        }

        public MachineNotification(NotificationTypeEnum type, int number, int parameter, DateTime notificationDate
                            , Dictionary<string, object> attributes)
            : this(type, number, parameter, notificationDate)
        {
            AdditionalInfos = attributes;
        }

        #endregion < Construction >

        [JsonProperty("AdditionalInfos")]
        public Dictionary<string, object> AdditionalInfos { get; set; }

        [JsonProperty("Arguments")]
        public Dictionary<string, string> Arguments { get; set; }

        //[JsonProperty("CausesAndSolutions")]
        //public CauseSolutionPair[] CausesAndSolutions { get; set; }

        [JsonProperty("DescriptionLocalizationKey")]
        public string DescriptionLocalizationKey { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("NotificationCode")]
        public string NotificationCode { get; set; }

        [JsonProperty("NotificationDate")]
        public DateTime NotificationDate { get; set; }

        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Parameter")]
        public int Parameter { get; set; }

        [JsonProperty("Type")]
        public NotificationTypeEnum Type { get; set; }
    }

    public class MachineNotificationComparer : EqualityComparer<MachineNotification>
    {
        public static IEqualityComparer<MachineNotification> Default => s_comparer.Value;

        private static readonly Lazy<MachineNotificationComparer> s_comparer = new(Creator);

        private static MachineNotificationComparer Creator() => new();

        public override bool Equals(MachineNotification first, MachineNotification second)
        {
            if (first == null && second == null)
                return true;
            else if (first == null || second == null)
                return false;

            return first.Type == second.Type
                && first.Number == second.Number
                && first.Parameter == second.Parameter
            ;
        }

        public override int GetHashCode(MachineNotification obj)
            => ((int)obj.Type ^ obj.Number ^ obj.Parameter).GetHashCode();
    }
}
