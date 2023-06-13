namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Setup;
    using System.Collections.Generic;

    public interface IExecutionService : IApplicationService, IBootableService
    {
        Result StartExecution(IUserSession userSession);
        Result StopExecution(IUserSession userSession);
        IEnumerable<UnitSetupListItem> GetUnitSetupList();
        //IEnumerable<ToolForSpindleListItem> GetToolForSpindleListItems(UnitEnum unit);
        //IEnumerable<SetupToolListItem> GetSetupToolListItems(UnitToolsFilter filter);
        //IEnumerable<ConfirmSetupAction> ManualLoadOnSlot(ManualSetupActionInfo actionInfo);
        //IEnumerable<ConfirmSetupAction> LoadOnSlot(SetupActionInfo baseSetupAction);
        //IEnumerable<ConfirmSetupAction> ChangePosition(SetupActionInfo baseSetupAction);
        //IEnumerable<ConfirmSetupAction> RemoveFromSlot(SetupActionInfo baseSetupAction);
        //IEnumerable<ConfirmSetupAction> LoadOnUnit(SpindleActionInfo action);
        //IEnumerable<ConfirmSetupAction> RemoveFromUnit(SpindleActionInfo action);
        //IEnumerable<PlasmaToolComponentItem> GetPlasmaToolComponentItems(UnitEnum unit);
        //IEnumerable<PlasmaToolComponentWizardStep> GetPlasmaToolComponentWizardSteps(string plasmaToolComponent);
        //Result SetupActionsAcknowledge(IEnumerable<ConfirmSetupAction> setupActions);
        //Result<GenericEventInfo> MoveTo(SetupActionInfo moveToAction);
        //Result ManageSlot(SetupSlotManagementItem slotManagement);
        //Task<IEnumerable<PLMNotificationLogItem>> GetMachineAlarmsAsync();
        //Task<IEnumerable<MessageOperatorListItem>> GetMachineOperatorMessagesAsync();
        //Task<Result<MachineNotification>> GetHighestPriorityAlarmAsync();
        //Task<IEnumerable<PLMNotificationLogItem>> GetActiveAlarmsAsync();
        //Result ResetAlarms();
        //Result SetExecutionSession(UserSession currentSession);
        //Result<SetupStatusInfo> GetSetupStatus();
    }
}
