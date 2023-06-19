namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Classe del singolo oggetto e relativi attributi della macro per il passaggio di dati (enumerativi di tipo ExternalInterfaceNameEnum) tra BE e libreria esterna. 
    /// </summary>
    public class MacroItemAttributes : IDataExternalInterface
    {

        /// <summary>
        /// Tipo di macro a cui si applica la classe corrente
        /// </summary>
        protected MacroTypeEnum macroTypeEnum;

        [JsonProperty("Attributes")]
        public Dictionary<ExternalInterfaceNameEnum, object> Attributes { get; set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="macroTypeEnum">tipo di macro (Cut, Mill, Robot)</param>
        public MacroItemAttributes(MacroTypeEnum macroTypeEnum)
        {
            this.macroTypeEnum = macroTypeEnum;
            Attributes = new Dictionary<ExternalInterfaceNameEnum, object>();
        }

        /// <summary>
        /// Aggiunge o aggiorna il valore di un attributo contenuto nella lista degli attributi del oggetto 
        /// </summary>
        /// <param name="externalInterfaceNameEnum">attributo da aggiungere</param>
        /// <param name="value">valore dell'attributo</param>
        /// <param name="contextExternalInterfaceNameEnum">attributo che permette di fare una validazione rispetto al contesto dove si vuole inserire l'attributo
        /// Nel caso non sia definito viene sempre inserito o aggiornato; in caso contrario si verifica sei sia possibile inserirlo attraverso le regole(CustomAttribute)
        /// definite per l'eunumerativo "externalInterfaceNameEnum"</param>
        /// <returns></returns>
        public Result Add(ExternalInterfaceNameEnum externalInterfaceNameEnum, object value,ExternalInterfaceNameEnum contextExternalInterfaceNameEnum)
        {
            if (contextExternalInterfaceNameEnum != ExternalInterfaceNameEnum.NotDefined) //l'attributo da inserire deve rispettare le regole di relazione dell'oggetto contesto
            {
                //Nel contesto considerato e per il tipo di macro dato è possibile inserire l'attributo richiesto 
                if (DomainExtensions.ExistExternalAttributesInThisContext(contextExternalInterfaceNameEnum, macroTypeEnum, externalInterfaceNameEnum))
                {
                    if (Attributes.ContainsKey(externalInterfaceNameEnum))
                    {
                        Attributes.Remove(externalInterfaceNameEnum);
                    }
                    Attributes.Add(externalInterfaceNameEnum, value);
                    return Result.Ok();
                } 
                return Result.Fail("Attributo non consentito");
            }
            else
            {
                //l'attributo da inserire deve rispettare le regole di relazione dell'oggetto contesto, quindi non devo fare nessun controllo sull'esistenza di una relazione
                if (Attributes.ContainsKey(externalInterfaceNameEnum))
                {
                    Attributes.Remove(externalInterfaceNameEnum);
                }
                Attributes.Add(externalInterfaceNameEnum, value);
                return Result.Ok();
            }
        }

        public Result Add(ExternalInterfaceNameEnum externalInterfaceNameEnum, object value)
        {
            return Add(externalInterfaceNameEnum, value, ExternalInterfaceNameEnum.NotDefined);
        }
    }
}
