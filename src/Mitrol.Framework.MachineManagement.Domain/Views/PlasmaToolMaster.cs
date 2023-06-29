namespace Mitrol.Framework.MachineManagement.Domain.Models.Views
{
    using Mitrol.Framework.Domain.Enums;

    /// <summary>
    /// Vista contenente hashcode collegati con tool di plasma 
    /// </summary>
    public class PlasmaToolMaster
    {
        public string HashCode { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public long PlasmaCurrent { get; set; }
        public string DisplayValue { get; set; }
        /// <summary>
        /// Flag che indica se esiste almeno un tool associato all'HashCode
        /// </summary>
        public bool AtLeastOneTool { get; set; }
    }
}
