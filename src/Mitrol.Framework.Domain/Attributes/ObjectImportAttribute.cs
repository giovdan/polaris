

namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    
   
    [AttributeUsage(AttributeTargets.All)]
    public class ImportExportAttribute : Attribute
    {
        public ImportExportObjectEnum ObjectName { get; internal set; }

        public Type ServiceToBeUsed { get; internal set; }

        public Type FilterType { get; set; }

        public ImportExportAttribute(ImportExportObjectEnum objectName, System.Type serviceType,Type filterType)
        {
            ObjectName = objectName;
            ServiceToBeUsed = serviceType;
            FilterType = filterType;
        }
    }
}
