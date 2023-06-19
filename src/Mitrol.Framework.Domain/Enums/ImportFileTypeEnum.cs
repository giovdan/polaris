namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;
    
    [DefaultValue("PLR")]
    public enum ImportFileTypeEnum:uint
    {
        //[Extension("nc")]
        //[Description("Tipo file DSTV con estensione .nc")]
        //DSTV_V1 = 1,

        //[Extension("nc1")]
        //[Description("Tipo file DSTV con estensione .nc1")]
        //DSTV_V2 = 2,

        [Extension("polaris")] 
        [Description("Tipo file POLARIS con estensione .Polaris")]
        PLR = 3
    }
}
