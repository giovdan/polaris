namespace Mitrol.Framework.Domain.Extensions
{
    using System;
    using System.ComponentModel;

    public static class FormExtensions
    {
        public static TResult InvokeIfRequired<TResult>(this ISynchronizeInvoke obj, Func<TResult> func)
            => obj.InvokeRequired ? (TResult)obj.Invoke(func, Array.Empty<object>()) : func();

        public static void InvokeIfRequired(this ISynchronizeInvoke obj, Action action)
        {
            if (obj.InvokeRequired)
            {
                try
                {
                    obj.Invoke(action, Array.Empty<object>());
                }
                catch (ObjectDisposedException)
                {
                    // Eccezione lanciata quando il metodo cerca di essere invocato su un oggetto in
                    // Disposing. La race-condition non è gestibile. Intercetto l'eccezione senza
                    // fare nulla.
                }
            }
            else
            {
                action();
            }
        }
    }
}
