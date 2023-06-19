namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;

    public class GraphicOverlayComparer : IEqualityComparer<GraphicOverlay>
    {
        public static GraphicOverlayComparer Default => s_comparer.Value;

        private static readonly Lazy<GraphicOverlayComparer> s_comparer
            = new Lazy<GraphicOverlayComparer>(Creator);

        private static GraphicOverlayComparer Creator() => new GraphicOverlayComparer();

        public bool Equals([AllowNull] GraphicOverlay first, [AllowNull] GraphicOverlay second)
        {
#pragma warning disable IDE0041
            var is_first_null = ReferenceEquals(first, null);
            var is_second_null = ReferenceEquals(second, null);
#pragma warning restore IDE0041

            if (is_first_null && is_second_null)
                return true;
            else if (is_first_null || is_second_null)
                return false;
            else
            {
                return first.Type == second.Type
                    && first.Color == second.Color
                    && first.Opacity == second.Opacity
                    && first.Vertices.SequenceEqual(second.Vertices, EqualityComparer<PointF>.Default)
                ;
            }
        }

        public int GetHashCode([DisallowNull] GraphicOverlay obj)
        {
            var hashCode = 792638326;

            hashCode = hashCode * -1521134295 + EqualityComparer<OverlayTypeEnum>.Default.GetHashCode(obj.Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<StatusColorEnum>.Default.GetHashCode(obj.Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.Opacity);
            foreach (var vertex in obj.Vertices)
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<PointF>.Default.GetHashCode(vertex);
            }

            return hashCode;
        }
    }

    /// <summary>
    /// Classe di definizione degli overlay (poligoni in grafica ISO)
    /// </summary>
    public class GraphicOverlay
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
        public PointF[] Vertices { get; set; } = new PointF[4];

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
