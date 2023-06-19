namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Azioni da intraprendere in base alle informazioni ricavate dal setup
    /// </summary>
    // SetupActionEnum 
    public enum SetupActionEnum
    {
        // Utensile non utilizzato
        NotUsed = 0,
        // Utensile richiesto al posto giusto
        Ok = 1,
        // Utensile da caricare prelevandolo dal carrello
        LoadOnSlot = 2,
        // Utensile da caricare prelevandolo da un'altro magazzino (posizione sbagliata)
        ChangePosition = 3,
        // Utensile WARNING
        ManualActionNeeded = 4,
        // Caricamento manuale del tool
        ManualLoadOnSlot = 5,
        // Rimozione del tool dallo Slot        
        RemoveFromSlot = 6,
        // Richiesta conferma (Setup generale)
        RequiredConfirm = 7,
        // Carica sul mandrino
        LoadOnUnit = 8,
        // Rimuovi dal mandrino
        RemoveFromUnit = 9,
        // Elemento non richiesto (Setup generale)
        NotRequired = 10,
        // Elemento da validare (Setup generale)
        ToValidate = 11,
    }
}
