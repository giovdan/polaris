namespace Mitrol.Framework.Domain.Models
{
    using System.Threading;
    public class CumulativeCancellationToken
    {
        public CancellationToken ShutdownToken { get; private set; }
        private CancellationTokenSource abortingTokenSource;

        public bool IsCancellationRequested()
        {
            if (ShutdownToken.IsCancellationRequested || abortingTokenSource.IsCancellationRequested)
                return true;
            else
                return false;
        }

        public CumulativeCancellationToken()
        {
            abortingTokenSource = new CancellationTokenSource();
        }
        public void CancellationRequested()
        {
            abortingTokenSource.Cancel();
        }

        public void SetShutDownCancellationToken(CancellationToken token)
        {
            ShutdownToken = token;
        }
    }
}
