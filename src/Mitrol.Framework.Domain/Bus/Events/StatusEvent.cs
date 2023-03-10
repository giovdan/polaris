namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Bus.Enums;
    using System;
    using System.Collections.Generic;

    public enum StatusDataEnum
    {
        id=0,
        localizedText,
        status,
        errorLocalizationKey,
        progress,
        total
    }
    public class StatusEvent : SubscribleEvent<Dictionary<StatusDataEnum, object>>
    {
        private int _total;
        private int _progress;



        public StatusEvent(GenericEventStatusEnum status, string localizationKey = "", int total = 0, int progress = 0, string percentualProgress = "", object Id = null) 
                            : base(SubscribableEventEnum.ProgressEvent)
        {
            if (Data == null)
                Data = new Dictionary<StatusDataEnum, object>();
            // La localizationKey è diventata solo quella relativa all'errore
            Data.Add(StatusDataEnum.errorLocalizationKey, localizationKey);
            Data.Add(StatusDataEnum.status, status);
        
            _progress = progress;
            _total = total;
            var text = string.Empty;
            if (_progress!=0 && _total!=0)
                text = $"{_progress}/{_total}";

            if (string.Compare(percentualProgress, string.Empty) != 0)
            {
                if (string.Compare(text, string.Empty) != 0)
                    text += ", ";
                text += $"{percentualProgress}%";
                
            }
            // progress è un testo con la percentuale di progressione relativo all'oggetto che si sta importando/esportando
            Data.Add(StatusDataEnum.progress, text);
            Data.Add(StatusDataEnum.id, Id);
        }

        public int GetProgress()
        {
            return _progress;
        }

        public int GetTotal()
        {
            return _total;
        }

        public string GetLocalizationkey()
        {
            var text = string.Empty;
            if (Data.TryGetValue(StatusDataEnum.errorLocalizationKey, out var progressObj))
            {
                text = progressObj.ToString();
            }
            return text;
        }
        public string GetLocalizatedText()
        {
            var text = string.Empty;
            if (Data.TryGetValue(StatusDataEnum.localizedText, out var _text))
            {
                text = _text.ToString();
            }
            return text;
        }

        public object GetId()
        {
            object o = null;
            if (Data.TryGetValue(StatusDataEnum.id, out var Obj))
            {
                o = Obj;
            }
            return o;
        }
        public GenericEventStatusEnum GetStatus()
        {
            var status = GenericEventStatusEnum.NotActive;
            if (Data.TryGetValue(StatusDataEnum.status, out var progressObj))
            { 
                var o=(GenericEventStatusEnum)Enum.Parse(typeof(GenericEventStatusEnum), progressObj.ToString());
                status =o;
            }
            return status;
        }
    }
}
