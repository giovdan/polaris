namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Station : Observable
        //, IReadOnlyStation
    {
        public Station()
        {
            ResetToDefaults();
        }

        public Station(int id, double nx, double ny, double na, Dictionary<int, bool> originsPointsValidity = null)
            : this()
        {
            Id = id;
            NX = nx;
            NY = ny;
            NA = na;
            if (originsPointsValidity != null)
            {
                OriginPointsValidity = originsPointsValidity;
            }
        }

        /// <summary>
        /// Numero della stazione
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Origine longitudinale
        /// </summary>
        [JsonProperty("OX")]
        public double NX
        {
            get => _nx;
            set => SetProperty(ref _nx, value);
        }
        private double _nx;

        /// <summary>
        /// Origine trasversale
        /// </summary>
        [JsonProperty("OY")]
        public double NY
        {
            get => _ny;
            set => SetProperty(ref _ny, value);
        }
        private double _ny;

        /// <summary>
        /// Origine inclinazione
        /// </summary>
        [JsonProperty("OA")]
        public double NA
        {
            get => _na;
            set => SetProperty(ref _na, value);
        }
        private double _na;

        /// <summary>
        /// Stato dei punti dell'origine (da P1 a P4)
        /// </summary>
        [JsonProperty("OriginPointsValidity")]
        public Dictionary<int, bool> OriginPointsValidity { get; set; }

        /// <summary>
        /// Origini valide
        /// </summary>
        [JsonProperty("IsOriginValid")]
        public bool IsOriginValid => !double.IsNaN(NX)
            && !double.IsNaN(NY)
            && !double.IsNaN(NA);

        [JsonProperty("SelectedAxis")]
        public OriginAxisEnum SelectedAxis { get; set; }

        /// <summary>
        /// Reset dei valori (stato di orgini non valide)
        /// </summary>
        public void ResetToDefaults()
        {
            NX = float.NaN;
            NY = float.NaN;
            NA = float.NaN;

            OriginPointsValidity = new Dictionary<int, bool>
            {
                { 1, false }, { 2, false }, { 3, false }, { 4, false },
            };
        }
    }
}
