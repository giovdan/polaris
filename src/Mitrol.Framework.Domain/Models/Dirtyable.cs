namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Attributes;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Default implementation for Dirtyable pattern
    /// </summary>
    public abstract class Dirtyable : Observable
    {
        /// <summary>
        /// Initializes a new instance of the Dirtyable class.
        /// </summary>
        public Dirtyable() { }

        /// <summary>
        /// Gets or sets a value that indicates whether an object's property value has changed.
        /// </summary>
        [JsonIgnore]
        public bool IsDirty { get; set; }

        /// <summary>
        /// Recursively binds the OnPropertyChanged method to the PropertyChangedEvent of this object
        /// and all childs objects marked with a DirtyableChildAttribute.
        /// </summary>
        public void HandlePropertyChangedNotifications() => HandlePropertyChangedNotifications(this);

        /// <summary>
        /// Recursively binds the OnPropertyChanged method to the PropertyChangedEvent of this object
        /// and all childs objects marked with a DirtyableChildAttribute.
        /// </summary>
        /// <exception cref="InvalidOperationException">A property has DirtyableChildAttribute but does not implement INotifyPropertyChanged.</exception>
        private void HandlePropertyChangedNotifications(INotifyPropertyChanged objectToScan)
        {
            // Binds the OnPropertyChanged event handler.
            objectToScan.PropertyChanged += OnPropertyChanged;

            // Gets all the public properties ...
            var properties = objectToScan.GetType().GetProperties()
                // ... with DirtyableChildAttribute.
                .Where(property => property.GetCustomAttributes(typeof(DirtyableChildAttribute), false).Length > 0).ToList();
            
            foreach (var property in properties)
            {
                // If property implements INotifyPropertyChanged.
                if (property.GetValue(objectToScan) is INotifyPropertyChanged notifyProperty)
                {
                    // Binds the OnPropertyChanged event handler.
                    notifyProperty.PropertyChanged += OnPropertyChanged;
                    var valueOfObject = property.GetValue(objectToScan);
                    var propertiesOfObject = (valueOfObject).GetType().GetProperties()
                        .Where(prop => prop.GetCustomAttributes(typeof(DirtyableChildAttribute), false).Length > 0);
                    // Le proprietà dell'oggetto possono a loro volta contenere oggetti con Attributo DirtyableChildAttribute
                    if (propertiesOfObject.Count() > 0)
                    {
                        foreach (var pro in propertiesOfObject)
                        {
                            if (pro.GetValue(valueOfObject) is INotifyPropertyChanged notifyPropertySub)
                                HandlePropertyChangedNotifications(notifyPropertySub);
                        }            
                    }
                }
                // If property is a INotifyPropertyChanged enumerable.
                else if ((property.GetValue(objectToScan) is IEnumerable<INotifyPropertyChanged> observables))
                {
                    foreach (var observable in observables)
                    {
                        HandlePropertyChangedNotifications(observable);
                    }
                }
                // If property has DirtyableChildAttribute but does not implement INotifyPropertyChanged
                else { throw new InvalidOperationException($"{objectToScan.GetType()} - Property {property.Name} has {nameof(DirtyableChildAttribute)} but does not implement {nameof(INotifyPropertyChanged)}."); }
            }
        }

        /// <summary>
        /// The method that will handle the PropertyChanged event raised when a property is changed on a component.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => IsDirty = true;
    }
}
