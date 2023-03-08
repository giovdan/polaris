namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.Collections.Generic;
    public class DependencyAttributeListComparer : IEqualityComparer<KeyValuePair<ImportExportObjectEnum, long>>
    {
        public bool Equals(KeyValuePair<ImportExportObjectEnum, long> x, KeyValuePair<ImportExportObjectEnum, long> y)
        {
            var xAttribute = x.Key;
            var yAttribute = y.Key;
            if (x.Key == y.Key && x.Value == y.Value)
            {
                return true;
            }
             
            return false;
        }

        public int GetHashCode(KeyValuePair<ImportExportObjectEnum, long> obj)
        {
            var hashCode = 792638326;
            
            hashCode = hashCode * -1521134295 + EqualityComparer<ImportExportObjectEnum>.Default.GetHashCode(obj.Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<long>.Default.GetHashCode(obj.Value);
            return hashCode;
        }
    }


    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Class,AllowMultiple = true)]
    public class DependencyObjectForExportAttribute : Attribute
    {
        public ImportExportObjectEnum DependencyObject { get; internal set; }
        public DependencyObjectForExportAttribute(ImportExportObjectEnum dependencyObject)
        {
            DependencyObject = dependencyObject;  
        }
    }
}
