namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Extensions;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Class that aggregates all piece identifiers values.
    /// </summary>
    public class PieceItemIdentifier
    {
        /// <summary>
        /// Default constructor for PieceItemIdentifier.
        /// </summary>
        /// <param name="contract">String that represents the contract.</param>
        /// <param name="project">String that represents the project.</param>
        /// <param name="drawing">String that represents the drawing.</param>
        /// <param name="assembly">String that represents the assembly.</param>
        /// <param name="part">String that represents the part.</param>
        public PieceItemIdentifier(string contract, string project, string drawing, string assembly, string part)
        {
            Contract = contract ?? throw new ArgumentNullException(nameof(contract));
            Project = project ?? throw new ArgumentNullException(nameof(project));
            Drawing = drawing ?? throw new ArgumentNullException(nameof(drawing));
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            Part = part ?? throw new ArgumentNullException(nameof(part));
        }

        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public PieceItemIdentifier()
        {
        }

        /// <summary>
        /// Assembly identifier (old N: in Pegaso)
        /// </summary>
        [JsonProperty("Assembly")]
        public string Assembly { get; set; }

        /// <summary>
        /// Contract identifier
        /// </summary>
        [JsonProperty("Contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Drawing identifier (old D: in Pegaso)
        /// </summary>
        [JsonProperty("Drawing")]
        public string Drawing { get; set; }

        /// <summary>
        /// Part identifier (old POS: in Pegaso)
        /// </summary>
        [JsonProperty("Part")]
        public string Part { get; set; }

        /// <summary>
        /// Project identifier (old C: in Pegaso)
        /// </summary>
        [JsonProperty("Project")]
        public string Project { get; set; }

        public string CalculateHash()
        {
            return Contract.ToHex() + Project.ToHex() + Drawing.ToHex() + Assembly.ToHex()
                        + Part.ToHex();
        }

        public bool IsValid()
        {
            return !Assembly.IsNullOrEmpty() ||
                !Contract.IsNullOrEmpty() ||
                !Drawing.IsNullOrEmpty() ||
                !Project.IsNullOrEmpty() ||
                !Part.IsNullOrEmpty();
        }
        

        ///// <summary>
        ///// Phase identifier (optional)
        ///// </summary>
        //[JsonProperty("Phase")]
        //public string Phase { get; set; }

        ///// <summary>
        ///// Job identifier (optional)
        ///// </summary>
        //[JsonProperty("Job")]
        //public string Job { get; set; }

        ///// <summary>
        ///// Sub assembly identifier (optional)
        ///// </summary>
        //[JsonProperty("SubAssembly")]
        //public string SubAssembly { get; set; }

        ///// <summary>
        ///// GUID identifier
        ///// </summary>
        //[JsonProperty("Guid")]
        //public string Guid { get; set; }

    }
}
