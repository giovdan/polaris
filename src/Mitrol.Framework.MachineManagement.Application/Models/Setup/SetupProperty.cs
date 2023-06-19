namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    //public static class Utilities
    //{
    //    public static object GetDefaultValue(Type type)
    //    {
    //        var typeConverter = TypeDescriptor.GetConverter(type);
    //        if (type.IsEnum)
    //        {
    //            DefaultValueAttribute[] attributes = (DefaultValueAttribute[])type.GetCustomAttributes(typeof(DefaultValueAttribute), false);
    //            if (attributes != null &&
    //                attributes.Length > 0)
    //            {
    //                return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, attributes[0].Value);
    //            }
    //        }
    //        return null;
    //    }
    //}
    public interface ISetupData
    {
        object GetValueDependingByStatus();
        SetupActionEnum GetStatus();
        void SetRequiredValue(object requiredValue);
        void SetCurrentValue(object currentValue);
    }

    public class SetupPropertyWithTolerance<T> : SetupProperty<T>
    {
        public SetupPropertyWithTolerance(ToleranceConfiguration tolerance) : base()
        {
            Tolerance = tolerance;
        }

        [JsonIgnore]
        public ToleranceConfiguration Tolerance { get; set; }

        /// <summary>
        /// Faccio l'aggiornamento della Action (Stato) a seconda delle proprietà Value e Required, indicando una tolleranza 
        /// </summary>
        public override void UpdateStatus()
        {
            if (float.TryParse(RequiredValue.ToString(), out var required) && float.TryParse(Value.ToString(), out var currentvalue))
            {
                if (Tolerance.IsInTolerance(required, currentvalue))
                    Action = SetupActionEnum.Ok;
                else
                    Action = SetupActionEnum.RequiredConfirm;
            }
        }
    }

    public class SetupPropertyWithCustomComparer<T> : SetupProperty<T>
    {
        public SetupPropertyWithCustomComparer() : base()
        {
        }

        /// <summary>
        /// Faccio l'aggiornamento della Action (Stato) a seconda delle proprietà Value e Required, indicando una tolleranza 
        /// </summary>
        public override void UpdateStatus()
        {
            if (float.TryParse(RequiredValue.ToString(), out var required) && float.TryParse(Value.ToString(), out var currentvalue))
            {
                if (DomainExtensions.CompareFloatWithInchTolerance(required, currentvalue, false))
                    Action = SetupActionEnum.Ok;
                else
                    Action = SetupActionEnum.RequiredConfirm;
            }
        }
    }

    public class SetupProperty<TProperty> : Observable, ISetupData
    {
        public SetupProperty()
        {
            Action = SetupActionEnum.NotRequired;
            Value = GetDefault();
            RequiredValue = GetDefault();
        }

        /// <summary>
        /// Fornisce il valore di default del tipo generico dell' oggetto corrente
        /// </summary>     
        private TProperty GetDefault()
        {
            var defaultT = DomainExtensions.GetDefaultValue(typeof(TProperty));
            if (defaultT != null)
                return (TProperty)Convert.ChangeType(defaultT, typeof(TProperty));
            return default;
        }

        /// <summary>
        /// Ritorna true se il la proprietà Required assume il valore di Default altrimenti ritorna false
        /// </summary>
        public bool IsRequiredValueDefault()
        {
            return EqualityComparer<TProperty>.Default.Equals(RequiredValue, GetDefault());
        }

        /// <summary>
        /// Action può assumere i seguenti valori
        /// * NotRequired: quando è un parametro non necessario e non deve essere visualizzato
        /// * NotUsed: quando è un parametro necessario, è stato assegnato
        /// * Ok: la verifica tra Value e Required ha indicato che l'eventuale scostamento tra i due valori non deve generare errore
        /// * RequiredConfirm:la verifica tra Value e Required ha indicato che l'eventuale scostamento tra i due valori deve obbligare una conferma
        /// </summary>
        [JsonProperty("Action")]
        public SetupActionEnum Action
        {
            get => _action;
            set => SetProperty(ref _action, value);
        }
        private SetupActionEnum _action;

        [JsonProperty("Value")]
        public TProperty Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
        private TProperty _value;

        [JsonProperty("RequiredValue")]
        public TProperty RequiredValue
        {
            get => _requiredValue;
            set => SetProperty(ref _requiredValue, value);
        }
        private TProperty _requiredValue;

        /// <summary>
        /// Viene restituita la proprietà Required nel caso in cui lo stato sia "RequiredConfirm", altrimenti viene restituita la proprietà Value. 
        /// </summary>
        public object GetValueDependingByStatus()
        {
            if (Action == SetupActionEnum.RequiredConfirm)
                return RequiredValue;
            else
                return Value;
        }

        /// <summary>
        /// Resetto la proprietà Required al suo valore di default(e riporto lo stato della proprietà allo stato iniziale)
        /// </summary>
        public void ResetRequiredValue()
        {
            Action = SetupActionEnum.NotRequired;
            RequiredValue = GetDefault();
        }

        public SetupActionEnum GetStatus()
        {
            return Action;
        }

        /// <summary>
        /// Assegno alla proprietà Value il valore Required
        /// </summary>
        public void SetValueToRequired()
        {
            Value = RequiredValue;
        }

        /// <summary>
        /// Faccio l'aggiornamento della Action (Stato) a seconda delle proprietà Value e Required, indicando una tolleranza 
        /// </summary>
        public virtual void UpdateStatus()
        {
            // Verifica di una discrepanza tra valore richiesto e valore corrente
            if (object.Equals(Value, RequiredValue))
            {
                Action = SetupActionEnum.Ok;
            }
            else
            {
                Action = SetupActionEnum.RequiredConfirm;
            }
        }


        /// <summary>
        /// Assegno il valore della proprietà Required al valore indicato e modifico lo stato per visualizzare la proprietà.
        /// </summary>
        public void SetRequiredValueForAction(TProperty value)
        {
            Action = SetupActionEnum.NotUsed;
            RequiredValue = value;
        }
      
        /// <summary>
        /// Assegno il valore della proprietà RequiredValue senza modificare lo stato
        /// </summary>
        public void SetRequiredValue(object requiredValue)
        {
            RequiredValue = (TProperty)requiredValue;
        }

        /// <summary>
        /// Assegno il valore della proprietà Value senza modificare lo stato
        /// </summary>
        public void SetCurrentValue(object currentValue)
        {
            Value = (TProperty)currentValue;
        }
    }
}
