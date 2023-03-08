namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using System.Collections.Generic;

    public class RemoteProgramCodeService : IRemoteProgramCodeService
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public RemoteProgramCodeService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        public Result CreateProgramCodeLines(IEnumerable<ProgramCodeLineItem> codeLines)
        {
            // L'aggiornamento remoto del program code non è consentito.
            return Result.Ok();
        }

        public Result DeleteAllProgramCodeLines()
        {
            // L'aggiornamento remoto del program code non è consentito.
            throw new System.NotSupportedException("L'aggiornamento remoto del program code non è consentito.");
        }

        public Result<ProgramCodeLineItem> GetProgramCodeLine(CodeLineItemFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public Result<ProgramCodeLineItem> GetProgramCodeNextLine(long jobId, int currentLineNumber)
        {
            throw new System.NotImplementedException();
        }

        public Result UpdateProgramCodeLine(ProgramCodeLineToUpdateItem lineToUpdate)
        {
            throw new System.NotSupportedException("L'aggiornamento remoto del program code non è consentito.");
        }
    }
}
