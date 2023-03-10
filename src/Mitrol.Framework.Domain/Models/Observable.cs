namespace Mitrol.Framework.Domain.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Concrete implementation of INotifyPropetyChaged pattern.
    /// </summary>
    public class Observable : Disposable, INotifyPropertyChanged
    {
        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            // Removes all events subscriptions
            if (_propertyChanged != null)
            {
                foreach (PropertyChangedEventHandler eventHandlerDelegate in _propertyChanged.GetInvocationList())
                {
                    _propertyChanged -= eventHandlerDelegate;
                }
            }

            base.DisposeManaged();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        private event PropertyChangedEventHandler _propertyChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove
            {
                _propertyChanged -= value;
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException("propertyNames");
            }

            foreach (string propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProperty>(ref TProperty currentValue, TProperty newValue, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (!object.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">
        /// A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.
        /// </param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(Func<bool> equal, Action action, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            action();

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(Func<bool> equal, Action action, params string[] propertyNames)
        {
            ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            action();
            OnPropertyChanged(propertyNames);

            return true;
        }
    }
}
