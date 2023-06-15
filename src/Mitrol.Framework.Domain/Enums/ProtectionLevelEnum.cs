
namespace Mitrol.Framework.Domain.Enums
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Enum per le gestire il livello di protezione dei parametri ed attributi
    /// Corrispondenti ai gruppi di sistema per mantenere la retro compatibilità
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum ProtectionLevelEnum
    {
        [Priority(-1)]
        None,
        [Priority(0)]
        [EnumCustomName("NONE")]
        [EnumSerializationName("READONLY")]
        ReadOnly = 999,
        [Priority(3)]
        [EnumCustomName("ADMINS")]
        [EnumSerializationName("HIGH")]
        High = 1,
        [Priority(2)]
        [EnumCustomName("SUPERUSERS")]
        [EnumSerializationName("MEDIUM")]
        Medium = 2,
        [Priority(1)]
        [EnumCustomName("USERS")]
        [EnumSerializationName("NORMAL")]
        Normal = 3,
        [Priority(4)]
        [EnumCustomName("FICEP")]
        [EnumSerializationName("CRITICAL")]
        Critical = 6
    }

    public class PriorityAttribute: Attribute
    {
        public int Priority { get; private set; }
        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
