namespace Mitrol.Framework.Domain.Models
{
    using System;
    using System.Diagnostics;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Base class for members implementing <see cref="IDisposable" />.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="Disposable" /> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        [JsonIgnore]
        public bool IsDisposed { get; protected set; } = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Disposable" /> is disposing.
        /// </summary>
        /// <value><c>true</c> if is disposing; otherwise, <c>false</c>.</value>
        [JsonIgnore]
        public bool IsDisposing { get; protected set; } = false;

        #region < Helper methods >

        [DebuggerHidden]
        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        #endregion < Helper methods >

        /// <summary>
        /// Disposes the managed resources implementing <see cref="IDisposable" />.
        /// </summary>
        protected virtual void DisposeManaged()
        {
        }

        #region < IDisposable pattern - Do not change this code >

        /// <summary>
        /// Performs application-defined tasks associated with freeing or releasing resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose all managed and unmanaged resources.
            Dispose(true);

            // Take this object off the finalization queue and prevent finalization code for this
            // object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources, called from the finalizer only.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!IsDisposed)
            {

                // If disposing managed and unmanaged resources.
                if (disposing)
                {
                    IsDisposing = true;
                    DisposeManaged();
                    IsDisposing = false;
                }

                IsDisposed = true;
            }
        }

        #endregion < IDisposable pattern - Do not change this code >
    }

    /// <summary>
    /// Base class for members implementing <see cref="IDisposable" />.
    /// </summary>
    /// <remarks>
    /// Adding a Finalize method to your object means that it will always be called by the GC, but
    /// be careful because when you add a Finalize method to an object, the object will always
    /// survive the first generational garbage collection. Therefore, all finalized objects live
    /// longer. Since you are trying to let the GC clean up as efficiently as possible, only use
    /// finalization when you have unmanaged resources to clean up or in special cases where the
    /// object is expensive to create (object pools).
    /// </remarks>
    public abstract class DisposableWithFinalizer : Disposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable" /> class. Releases unmanaged
        /// resources and performs other cleanup operations before the <see cref="Disposable" /> is
        /// reclaimed by garbage collection. Will run only if the Dispose method does not get
        /// called.
        /// </summary>
        ~DisposableWithFinalizer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources, called from the finalizer only.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!IsDisposed)
            {
                // If disposing managed and unmanaged resources.
                if (disposing)
                {
                    DisposeManaged();
                }

                DisposeUnmanaged();

                IsDisposed = true;
            }
        }

        /// <summary>
        /// Disposes the unmanaged resources implementing <see cref="IDisposable" />.
        /// </summary>
        protected virtual void DisposeUnmanaged()
        {
        }
    }
}
