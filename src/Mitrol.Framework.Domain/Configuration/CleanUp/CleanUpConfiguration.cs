namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class CleanUpConfiguration
    {
        internal const string s_maintenanceMaxRecordCountJsonName = "MaintenanceLimit";
        internal const string s_pieceMaxRecordCountJsonName = "PiecesLimit";
        internal const string s_programMaxRecordCountJsonName = "ProgramsLimit";
        internal const string s_stockMaxRecordCountJsonName = "StocksLimit";

        public CleanUpConfiguration()
        { }

        [JsonConstructor]
        public CleanUpConfiguration([JsonProperty(s_maintenanceMaxRecordCountJsonName)] int maintenanceLimit,
                                    [JsonProperty(s_pieceMaxRecordCountJsonName)] int piecesLimit,
                                    [JsonProperty(s_programMaxRecordCountJsonName)] int programsLimit,
                                    [JsonProperty(s_stockMaxRecordCountJsonName)] int stocksLimit)
        {
            MaintenanceLimit = maintenanceLimit;
            PiecesLimit = piecesLimit;
            ProgramsLimit = programsLimit;
            StocksLimit = stocksLimit;
        }

        [JsonProperty(s_maintenanceMaxRecordCountJsonName)]
        public int MaintenanceLimit { get; protected set; }

        [JsonProperty(s_pieceMaxRecordCountJsonName)]
        public int PiecesLimit { get; protected set; }

        [JsonProperty(s_programMaxRecordCountJsonName)]
        public int ProgramsLimit { get; protected set; }

        [JsonProperty(s_stockMaxRecordCountJsonName)]
        public int StocksLimit { get; protected set; }
    }
}