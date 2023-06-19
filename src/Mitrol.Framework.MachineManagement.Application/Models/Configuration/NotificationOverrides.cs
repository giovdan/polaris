namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Dizionario informazioni addizionali per le notifiche (Allarmi/Messaggi)
    /// </summary>
    public class NotificationOverrides
    {
        private readonly Dictionary<string, MachineNotificationConfiguration> _internalDictionary = new Dictionary<string, MachineNotificationConfiguration>();

        public void Add(string key, MachineNotificationConfiguration value) => _internalDictionary.Add(key, value);

        /// <summary>
        /// Reset dictionary
        /// </summary>
        public void Reset()
        {
            _internalDictionary.Clear();
        }

        /// <summary>
        /// Get MachineConfiguration value of tuple (Context,Key)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MachineNotificationConfiguration Get(string code)
        {
            foreach(var key in _internalDictionary.Keys)
            {
                if (System.IO.Enumeration.FileSystemName.MatchesSimpleExpression(key, code))
                {
                    return _internalDictionary[key];
                }
            }

            return default;
        }

        public static NotificationOverrides Instance { get; } = new NotificationOverrides();
    }
}
