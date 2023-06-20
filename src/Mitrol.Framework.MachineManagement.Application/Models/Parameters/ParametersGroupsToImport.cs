namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using System;
    using System.Collections.Generic;

    public class ParametersGroupsImportResult
    {
        public ParametersGroupsImportResult()
        {

        }

        public ParametersGroupsImportResult(HashSet<string> parameters, HashSet<string> groups, HashSet<long> links)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
            Links = links ?? throw new ArgumentNullException(nameof(links));
        }

        public HashSet<string> Parameters { get; set; }
        public HashSet<string> Groups { get; set; }
        public HashSet<long> Links { get; set; }

        public IEnumerable<long> Parameters2 { get; set; }
        public IEnumerable<long> Groups2 { get; set; }
        public IEnumerable<long> Links2 { get; set; }
    }
}