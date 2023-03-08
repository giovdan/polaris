
namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class ExtensionAttribute:Attribute
    {
        public string FileExtension { get; set; }

        public ExtensionAttribute(string fileExtension)
        {
            FileExtension = fileExtension;
        }
    }
   
}
