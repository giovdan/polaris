using Mitrol.Framework.Domain.Core.Enums;
using Mitrol.Framework.Domain.Enums;
using Newtonsoft.Json;
using System.Drawing;

namespace Mitrol.Framework.MachineManagement.Application.Models
{
    /// <summary>
    /// Classe di definizione degli overlay (poligoni in grafica ISO)
    /// </summary>
    public class CodeControllerGraphicOverlays
    {
        /// <summary>
        /// Tipo di overlay
        /// </summary>
        [JsonProperty("OverlayType")]
        public OverlayTypeEnum Type { get; set; }

        /// <summary>
        /// Vertici del poligono
        /// </summary>
        [JsonProperty("Vertices")]
        public PointF[] Vertices = new PointF[4];

        /// <summary>
        /// Colore dell'overlay
        /// </summary>
        [JsonProperty("OverlayColor")]
        public StatusColorEnum Color { get; set; }

        /// <summary>
        /// Opacità dell'overlay
        /// </summary>
        [JsonProperty("Opacity")]
        public short Opacity { get; set; }
    }
}
